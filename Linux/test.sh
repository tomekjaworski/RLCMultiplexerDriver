#!/bin/bash
echo "Ustawianie kierunkow...";



# MOSI
MOSI=12;
gpio mode $MOSI out

# MISO
MISO=13;
gpio mode $MISO in

# !LA
LA=2;
gpio mode $LA out
gpio write $LA 1

# OE
OE=3;
gpio mode $OE out
gpio write $OE 0

# CLK
CLK=14;
gpio mode $CLK out


################
#gpio write $MOSI 0


while :;
do
	gpio write 14 0;
	sleep 0.01;
	gpio write 14 1;
	sleep 0.01;
done



