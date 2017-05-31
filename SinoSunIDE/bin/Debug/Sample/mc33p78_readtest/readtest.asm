

#include "mc33p78.inc"

    org     0x00
    goto    main
    
    
    
main:
        nop
        movai   0xaa;
ram_w:
        clrr     FSR0
        ;clrr    FSR1
ram_w_first:        
        incr    FSR0        
        movar   FSR0
        movra   INDF0        
        isubai  0xff
        jbset   Z
        goto    ram_w_first
ram_clr:
        clrr     FSR0
        ;clrr    FSR1
ram_clr_first:        
        incr    FSR0        
        movar   FSR0
        clrr   INDF0        
        isubai  0xff
        jbset   Z
        goto    ram_clr_first        
        
        goto    main
    end    