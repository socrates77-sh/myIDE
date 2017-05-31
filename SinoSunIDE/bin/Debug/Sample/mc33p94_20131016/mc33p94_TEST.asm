

#include "mc33p94.inc"



cblock 0x00
r0
r1
r2
r3
acc_temp
status_temp
flag



endc

#define  stop_enable         flag,0
#define  clk_enable          flag,1


        org 0x0
        goto    main

        org 0x8        
start_just_int:
        jbclr    T0IE
        jbset    T0IF
        goto     lab_if_timer1_int
;----timer0-----------------------
        bclr    T0IF
        
        jbclr       IOP1,4
        goto        lab_p01_4
        bset        IOP1,4
        goto        end_time0_interrupt
lab_p01_4:    
        bclr        IOP1,4

end_time0_interrupt:
;---------------------------------
lab_if_timer1_int:        
        jbclr    T1IE
        jbset    T1IF
        goto    just_int0        
;*********timer1***********
    bclr    T1IF
    ;incr    time_125us
    ;jbset    time_125us,3

end_time1_interrupt:
;**********end_timer1******
just_int0:
    jbclr    INT0IE
    jbset    INT0IF
    goto     just_int1
;**********int0************
    bclr    INT0IF
    

    
    ;goto   end_time1_interrupt
end_int0_interrupt:    
;*********end int0*********
just_int1:
    jbclr    INT1IE
    jbset    INT1IF
    goto     end_just_int
;**********int0************
    bclr    INT1IF


;**********end_kbim********
end_just_int:
exit_interrupt:

    retie     
        
        ;p16 use for int0
        ;p14 clk out       
main: 
        ;fill    0x0000,370      
        bclr    GIE
        movai   0xff
        movra   OEP0 ;set as output port
        ;movra   OEP1
        movra   OEP2
        movra   PUP1 ;p1 push up R
        movai   0xbf
        movra   OEP1 ;
        
        movai   0x70
        movra   ADCR0

        movai   0xff
        movra   FSR0

lab_clr_ram:        
        clrr    INDF0
        ;decr    FSR0
        djzr    FSR0 ;FRS0-1=0,skip next instruction
        goto    lab_clr_ram 
        clrr    INDF0   ;clr 0x00 addr        
        
        ;lcd 
        movai   0x85
        movra   LCDCR0
        movai   0x05
        movra   LCDCR1
        movai   0xff
        movra   LCDIOS
        
        movai   0x02
        movra   OSCM
        
        movai   0x8b ; enable t0,pwm t0pts=0,t0pr=0
        movra   T0CR
        ;movai   11000111b ; enable t0,pwm t0pts=0,t0pr=0
        ;movra   T1CR
        
        movai   100
        movra   T0LOAD
        
        movai   0x01
        movra   MCR        
        
        bset    INT0IE
        ;bset    T0IE
        bset    GIE
        
        bset stop_enable 
        stop
        
        
loop:
        
        call    keyscan
        jbclr   stop_enable
        goto    main
        bset    T0IE
        
        goto loop
        
keyscan:
        jbclr   IOP1,6 ;
        goto    end_keyscan
        call    delay10ms
        call    delay10ms
        call    delay10ms
        jbclr   IOP1,6
        goto    end_keyscan

        jbclr       stop_enable
        goto        lab_p00_0
        bset        stop_enable
        bclr        clk_enable
        goto        cancle_end_keyscan
lab_p00_0:    
        bclr        stop_enable
        bset        clk_enable        
                                
cancle_end_keyscan:
        jbset   IOP1,6 
        goto    cancle_end_keyscan
        call    delay10ms
        ;call    delay10ms
        call    delay10ms
        jbset   IOP1,6
        goto    cancle_end_keyscan
end_keyscan:        
        return

delay10ms:
        movai   0xff
        movra   r1
re_load:
        movai   0xff        
        movra   r0
del_dec:        
        djzr    r0
        goto    del_dec
        djzr    r1
        goto    re_load
        nop
        return                
        

                
        
        end