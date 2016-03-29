all: mult bcast

mult: mult.cpp
	gcc -g -Wall mult.cpp -lrt -lbcm2835 -o mult

bcast: bcast.cpp
	gcc -g -Wall bcast.cpp -lrt -lbcm2835 -o bcast


clean:
	rm -f mult
	rm -f bcast

