
    
abc     equ     0x20
abc0    equ     0x30        
        
        org 0x3ff

        org 0x00
        movai   0x01
        movra   0x10
        movai   0x02
        rsubar  0x10
        
        movai   0x03
        movra   0x10
        movai   0x02
        rsubar  0x10

looop:        
        incr    abc
        movar   0x20
        movra   abc0
        movra   0x35
        movra   0x38
        movra   0x40
        
        
        
        
        goto    looop
        
        
        end