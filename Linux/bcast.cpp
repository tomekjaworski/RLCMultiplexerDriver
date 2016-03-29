#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <errno.h>
#include <sys/time.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <assert.h>
#include <time.h>
#include <bcm2835.h>

#define NETERR(__x) __check_network_error(__x, __FILE__, __LINE__);

void __check_network_error(int result, const char* f, int l)
{
	if (result != -1)
		return;

	int err = 0;
	printf("Network error %s:%d #%d (%s)\n", f, l, err, strerror(err));
	exit(1);
}

int main(void)
{
	int sock = socket(AF_INET, SOCK_DGRAM, 0);
	NETERR(sock);

	int t = true;
	int result = setsockopt(sock, SOL_SOCKET, SO_BROADCAST, (char*)&t, sizeof(int));
	NETERR(result);

	sockaddr_in recv_addr;
	recv_addr.sin_family = AF_INET;
	recv_addr.sin_port = htons(14005);
	recv_addr.sin_addr.s_addr = INADDR_ANY;

	result = bind(sock, (sockaddr*)&recv_addr, sizeof(sockaddr_in));
	NETERR(result);

	printf("BCAST: Oczekiwanie na zapytania na 255.255.255.255:14005...\n");

	while(1)
	{
		char buffer[1024];
		socklen_t sockaddr_size = sizeof(sockaddr_in);
		int recvlen  = recvfrom(sock, buffer, sizeof(buffer) - 1, 0, (sockaddr*)&recv_addr, &sockaddr_size);
		buffer[recvlen] = '\x0';


		if (strcasecmp(buffer, "Halo! Multiplekserku!") != 0)
			continue;

		const char* response = "Czego?? Grzecznie sie pytam.";
		size_t sendlen  = sendto(sock, (void*)response, strlen(response), 0, (sockaddr*)&recv_addr, sockaddr_size);

		printf("BCAST: Wyslano odpowiedz do %s...\n", inet_ntoa(recv_addr.sin_addr));
		fflush(stdout);

	}

	return 0;
}



