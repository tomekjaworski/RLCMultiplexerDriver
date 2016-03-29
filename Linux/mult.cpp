#include <stdio.h>
#include <syslog.h>
#include <unistd.h>
#include <stdlib.h>
#include <string.h>
#include <errno.h>
#include <sys/time.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <assert.h>
#include <time.h>
#include <bcm2835.h>

#define BOARD_COUNT	16

void output_enable(bool is_enabled);
void got_error(const char* msg, int error_code = 1);
int init_hardware(void);
void done_hardware(void);
bool selftest(void);

void LA(bool state);
void OE(bool state);
void LA_pulse(void);

void init_server(void);

void store_matrix(bool switch_outputs_off);
void reset_matrix(void);

/* konfiguracja */
bool store_debug = 0;
bool mirror = true;
//bool use_timeout = false;
int timeout_value = 10; // rozlaczenie przy braku aktywnosci [sek]; -1 - wylacz timeout
/*************/

uint16_t output_matrix[BOARD_COUNT], input_matrix[BOARD_COUNT]; // macierz przelacznikow
int server_socket = -1;
int client_socket = -1;
sockaddr_in remote_client;


#define SOCKET_D_H	(1 << 15)
#define SOCKET_D_L	(1 << 14)
#define SOCKET_D_GND	(1 << 13)

#define SOCKET_C_H	(1 << 12)
#define SOCKET_C_L	(1 << 11)
#define SOCKET_C_GND	(1 << 10)

#define SOCKET_B_H	(1 << 9)
#define SOCKET_B_L	(1 << 8)
#define SOCKET_B_GND	(1 << 7)

#define SOCKET_A_H	(1 << 6)
#define SOCKET_A_L	(1 << 5)
#define SOCKET_A_GND	(1 << 4)

#define LED				(1 << 3)
#define ALL_OFF		(0)


// polecenia protokolu

#define CMD_PUSH		1
#define CMD_PING		2



void init_server(void)
{
	client_socket = -1;
	server_socket = socket(AF_INET, SOCK_STREAM, 0);
	assert(server_socket != -1);


	int on = 1;
	int ret = setsockopt(server_socket, SOL_SOCKET, SO_REUSEADDR, &on, sizeof(on));
	assert(ret == 0);

	sockaddr_in server;
	server.sin_family = AF_INET;
	server.sin_port = htons(14000);	
	server.sin_addr.s_addr = INADDR_ANY;

	int result = bind(server_socket, (struct sockaddr*)&server, sizeof(sockaddr_in));
	assert(result == 0);

	result = listen(server_socket, 5);
	assert(result == 0);
}

int main(int argc, char** argv)
{
	syslog(LOG_INFO, "Start MULT: %s\n", argv[0]);
	printf("Sterownik multipleksera dla tomografu pojemnosciowego, 2013\n");
	printf("STORE_DEBUG=%d; MIRROR=%d\n\n", store_debug, mirror);

//	------------
	printf("Inicjowanie sieci...\n");
	init_server();


	printf("Inicjowanie sprzetu...\n");
	init_hardware();

	OE(true);
	LA(false);

	// jesli na przekaznikach sa jakies smieci
	// to wyzeruj wszystkie stany (wszystko jest wylaczone)
	memset(output_matrix, 0, sizeof(uint16_t) * BOARD_COUNT);
	memset(input_matrix, 0, sizeof(uint16_t) * BOARD_COUNT);
	store_matrix(true);

	LA_pulse();
	OE(false);
	printf("Ok\n");

	// test
	printf("Test sprzetu... ");
	if (selftest())
	{
		printf("Ok\n");
		syslog(LOG_INFO, "Sprzet ok");
	}
	else
	{
		syslog(LOG_ERR, "Sprzet BLAD");
		printf("Blad.\n");
		exit(1);
	}

	//	--------------
	printf("Gotowy. Oczekiwanie na polaczenie sieciowe...\n");


	while(true)
	{
		uint8_t recv_buffer[sizeof(uint16_t) + BOARD_COUNT * sizeof(uint16_t)];
		int recvd;

		socklen_t slen = sizeof(sockaddr_in);
		client_socket = accept(server_socket, (sockaddr*)&remote_client, &slen);
		printf("Otrzymano polaczenie... \n");

		if (client_socket == -1)
			continue;

		char ip[32];
		inet_ntop(AF_INET, &(remote_client.sin_addr), ip, INET_ADDRSTRLEN);
		printf("Host zdalny: %s:%d\n", ip, ntohs(remote_client.sin_port));


		fd_set rfd, wfd, efd;
		

		FD_ZERO(&rfd);
		FD_ZERO(&wfd);
		FD_ZERO(&efd);

		bool error = false;
		bool reset_receiver = true;
		uint8_t* ptr;
		size_t to_receive;
		time_t last_time = time(NULL);

		while(!error)
		{

			if ((timeout_value != -1) && (time(NULL) - last_time > timeout_value))
			{
				printf("Timeout; przerwanie polaczenia ze strony multipleksera\n");
				error = true;
				continue;
			}

			if (reset_receiver)
			{
				ptr = recv_buffer;
				to_receive = sizeof(uint16_t) /* polecenie */ + BOARD_COUNT * sizeof(uint16_t);
				reset_receiver = false;
			}
			
			FD_ZERO(&rfd); FD_ZERO(&wfd); FD_ZERO(&efd);
			FD_SET(client_socket, &rfd);

			struct timeval tv = { 1, 0 };
			int ret = select(client_socket + 1, &rfd, NULL, NULL, &tv);
			if (ret == -1)
			{
				printf("select == -1\n");
				error = true;
				break;
			}

			if (ret == 0 || !FD_ISSET(client_socket, &rfd))
				continue; // nie ma czego odczytywac


			recvd = recv(client_socket, ptr, to_receive, 0);
			if (recvd == 0 || recvd == -1)
			{
				// blad polaczenia
				printf("recv == %d\n", recvd);
				error = true;
				break;
			}
			
			to_receive -= recvd;
			ptr += recvd;
			last_time = time(NULL);

			if (to_receive != 0)
				continue;

			// ok, odebrano dane :D
			uint16_t* pptr = (uint16_t*)recv_buffer;
			uint16_t command = ntohs(*pptr++);


			if (command == CMD_PUSH)
			{
				for (int i = 0; i < BOARD_COUNT; i++)
					output_matrix[i] = ntohs(pptr[i]);
				store_matrix(false);
			}

			if (command == CMD_PING)
			{
				// nic nie rob :)
			}

			printf("cmd == %d\n", command);
			reset_receiver = true;
		}
	

		// koniec polaczenia
		reset_matrix();


		close(client_socket);
		printf("Polaczenie zamkniete.\n");
	}

	done_hardware();
	return 0;
}

// ####################################


bool selftest(void)
{
	srand(time(NULL));

	for (int j = 0; j < 20; j++)
	{
		for (int i = 0; i < BOARD_COUNT; i++)
			output_matrix[i] = (rand() % 2 == 0) ? ALL_OFF : LED;

		store_matrix(false);
		store_matrix(false);

	//bcm2835_delay(200);

		if (memcmp(input_matrix, output_matrix, BOARD_COUNT * sizeof(uint16_t)) != 0)
		{
			printf("-");
			return false; // dupa....
		} else
			printf("+");
	}

	for (int i = 0; i < BOARD_COUNT; i++)
		output_matrix[i] = SOCKET_A_GND | SOCKET_B_GND | SOCKET_C_GND | SOCKET_D_GND;

	store_matrix(false);
	store_matrix(false);

	printf("\n");


	usleep(750 * 1000); // 750 ms przerwy

	reset_matrix();

	return true;
}


void reset_matrix(void)
{
	memset(output_matrix, 0, sizeof(uint16_t) * BOARD_COUNT);
	store_matrix(false);
	store_matrix(false);
}

void store_matrix(bool switch_outputs_off)
{
	// przygotuj dane wyjsciowe
	uint16_t temp_output[BOARD_COUNT];
	uint16_t temp_input[BOARD_COUNT];
	for (int i = 0; i < BOARD_COUNT; i++)
	{
		// z punktu widzenia rejestru przesuwnego, pierwsza karta (kanaly 1,17,33,49,...)
		// znajduje sie po prawej stronie urzadzenia (zaraz kolo jednostki CPU)
		// ale, zeby rozklad kontaktow pasowal do istniejacych tomografow,
		// konieczne jest odbicie lustrzane

		if (mirror)
		{
			temp_output[i] = (output_matrix[i] >> 8) & 0x00FF;
			temp_output[i] |= (output_matrix[i] << 8) & 0xFF00;
		} else
		{
			temp_output[BOARD_COUNT - 1 - i] = (output_matrix[i] >> 8) & 0x00FF;
			temp_output[BOARD_COUNT - 1 - i] |= (output_matrix[i] << 8) & 0xFF00;
		}


	}

	if (switch_outputs_off)
		OE(true);

	bcm2835_spi_transfernb((char*)temp_output, (char*)temp_input, 2 * BOARD_COUNT);

	LA_pulse();

	if (switch_outputs_off)
		OE(false);

	if (store_debug)
	{
		printf("Wyslano: ");
		for(int i = 0; i < BOARD_COUNT * 2; i++)
		{
			uint8_t* data = (uint8_t*)temp_output;
			printf("%02x", data[i]);
		}
		printf("\nOdebrano:");
		for(int i = 0; i < BOARD_COUNT * 2; i++)
		{
			uint8_t* data = (uint8_t*)temp_input;
			printf("%02x", data[i]);
		}
		printf("\n");
	}	

	fflush(stdout);

	// przygotuj dane wejsciowe
	for (int i = 0; i < BOARD_COUNT; i++)
	{
		if (mirror)
		{	
			// konieczne odbicie lustrzane
			input_matrix[i] = (temp_input[i] >> 8) & 0x00FF;
			input_matrix[i] |= (temp_input[i] << 8) & 0xFF00;
		} else
		{
			input_matrix[BOARD_COUNT - 1 - i] = (temp_input[i] >> 8) & 0x00FF;
			input_matrix[BOARD_COUNT - 1 - i] |= (temp_input[i] << 8) & 0xFF00;
		}
	}
}

void OE(bool state)
{
	if (state)
		bcm2835_gpio_write(RPI_V2_GPIO_P1_15, HIGH);
	else
		bcm2835_gpio_write(RPI_V2_GPIO_P1_15, LOW);
}

void LA(bool state)
{
	if (state)
		bcm2835_gpio_write(RPI_V2_GPIO_P1_13, HIGH);
	else
		bcm2835_gpio_write(RPI_V2_GPIO_P1_13, LOW);
}

void LA_pulse(void)
{
	LA(true);
	//usleep(100 * 1000);
	bcm2835_delay(25);

	LA(false);
	//usleep(100 * 1000);
	bcm2835_delay(25);
}


void got_error(const char* msg, int error_code)
{	
	printf("Blad: %s\n", msg);
	exit(error_code);
}

int init_hardware(void)
{
	if (!bcm2835_init())
		got_error("bcm2835_init", 1);

	bcm2835_gpio_fsel(RPI_V2_GPIO_P1_13, BCM2835_GPIO_FSEL_OUTP); // !LA
	bcm2835_gpio_fsel(RPI_V2_GPIO_P1_15, BCM2835_GPIO_FSEL_OUTP); // !OE

	bcm2835_spi_begin();
	bcm2835_spi_setBitOrder(BCM2835_SPI_BIT_ORDER_LSBFIRST);
	bcm2835_spi_setDataMode(BCM2835_SPI_MODE0);
	bcm2835_spi_setClockDivider(0);

	return 1;
}

void done_hardware(void)
{
	bcm2835_spi_end();
	bcm2835_close();
}




