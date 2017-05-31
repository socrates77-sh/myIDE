


	org 0x00
        goto start
        
        
start:
	include 2.asm
	nop
        movai	0x55
        movra	0x20;
        
loop:
	nop
	goto loop       


	end;       