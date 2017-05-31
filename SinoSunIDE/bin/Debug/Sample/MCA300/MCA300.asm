
#include "mc32p7212.inc"



   	org 0x00
   	goto   	main



main:
   	movai  	0xaa
   	movra  	OEP1
   	movai  	0x55;
   	movra  	IOP1



   	end