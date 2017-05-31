

#include "mc30p011.inc"


		org 0x3ff
		goto    main
        org 0x00
        goto    main
        
        
main:
        movai   0x00
        movra   DDR0
        movra   DDR1
        
loop:
        
        incr    0x10
        movar   0x10
        movra   P0
        incr    0x11
        movar   0x11
        movra   P1
        ;comr    P1
        goto    loop 

 
        END       
        