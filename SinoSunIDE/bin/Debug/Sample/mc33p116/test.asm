

IOP0    EQU 0x1c0
OEP0    EQU 0X1C1
IOP2    EQU 0X1C8
OEP2    EQU 0X1C9

    org 0x00
    goto    main
       
       
       
main:
    movai   0x55
    movra   0x00
    movra   0x01
    movra   0x50    ;µº»Îµÿ÷∑
    movra   IOP0
    movra   OEP0

loop:
    comr    IOP0
    goto    loop
    ;lcall  loop
       
       
       
    org      0x3f00
    movai    0xaa
    movra    0x40
       
 
    end      