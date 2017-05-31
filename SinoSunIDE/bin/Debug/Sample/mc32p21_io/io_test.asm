

#include mc32p21.inc


        org 0x00
        goto    main
        
        
main:
        movai   0xff
        movra   STATUS
        movra   OEP0
        movra   OEP1
        ;movra   OPE2        
        
LOOP:
        incr    0x00
        movar   0x00
        movra   IOP0
        movra   IOP1
        goto    LOOP
        
        end        