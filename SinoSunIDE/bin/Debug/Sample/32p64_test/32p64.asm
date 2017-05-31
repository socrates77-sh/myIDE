

#include "mc32p64.inc"





	org 0x00
start:
	movai	0xaa
	movai	0x55
	movra	0x00

loop:
	nop
	goto	loop
 
 	end      