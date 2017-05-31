
IOP0	EQU	0X1C0
OEP0	EQU	0X1C1
	


	org 0x00
	goto	main


main:
	movai	0xff
       	movra	OEP0		
	
loop:
       	movai	0xff
       	movra	IOP0
        nop
        nop
        clrr	IOP0
 nop
        goto	loop
	end       