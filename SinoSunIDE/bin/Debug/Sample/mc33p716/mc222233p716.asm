
MCR     EQU     0X1B8

IOP0    EQU     0X1C0
OEP0    EQU     0X1C1
PUP0    EQU     0X1C2
PUPS0   EQU     0X1C3
PDP0    EQU     0X1C4
PDPS0   EQU     0X1C5
DKWP0   EQU     0X1C6

IOP1    EQU     0X1C8
OEP1    EQU     0X1C9
PUP1    EQU     0X1CA
PUPS1   EQU     0X1CB
PDP1    EQU     0X1CC
PDPS1   EQU     0X1CD
DKWP1   EQU     0X1CE

IOP2    EQU     0X1D0
OEP2    EQU     0X1D1
PUP2    EQU     0X1D2
PUPS2   EQU     0X1D3
PDP2    EQU     0X1D4
PDPS2   EQU     0X1D5
DKWP2   EQU     0X1D6
KBIM    EQU     0X1D7

        org 0x0
        goto    main
        
        org     0x08
funiton_int:
        NOP
        nop
        retie 
        
main:
        bclr     7,MCR 
        movai   0xff
        movra   OEP0
        movra   OEP1
        movra   OEP2
        
LOOP:
        INCR    IOP0        
        INCR    IOP1
        INCR    IOP2
        stop
        sleep
        end        


