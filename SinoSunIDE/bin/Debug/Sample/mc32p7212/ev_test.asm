#include "MC32P7212.inc"

       	ORG    	0X00
       	goto   	main

main:
       	movai  	0xff
       	movra  	OEP0
       	movra  	OEP1
       
loop:
       	movai  	0xff
       	movra  	IOP0
       	movra  	IOP1
       	nop
       	nop
       	nop
       	clrr   	IOP0
       	clrr   	IOP1
       	nop
       	nop
       	nop
       	goto   	loop       


       	end    	