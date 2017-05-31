
; mc32p21 timer test funtion
; timer.asm


#include "mc32p21.inc"

cblock 0x00
r0
r1
r2
r3
acc_temp
status_temp
model_flag1
model_flag2



endc

#define model00         model_flag1,0
#define model01         model_flag1,1
#define model02         model_flag1,2
#define model03         model_flag1,3
#define model04         model_flag1,4
#define model05         model_flag1,5
#define model06         model_flag1,6
#define model07         model_flag1,7
#define model08         model_flag2,0
#define model09         model_flag2,1
#define model0a         model_flag2,2
#define model0b         model_flag2,3
#define model0c         model_flag2,4
#define model0d         model_flag2,5
#define model0e         model_flag2,6
#define model0f         model_flag2,7

        org 0x0000       
        goto start

        org 0x08
fun_interrupt:
        movra   acc_temp
        swapar  STATUS
        movra   status_temp
;judgement int source
start_just_int:
        jbclr    T0IE
        jbset    T0IF
        goto     lab_if_timer1_int
;----timer0-----------------------
        bclr    T0IF
        jbclr       IOP0,1
        goto        lab_p01_0
        bset        IOP0,1
        goto        end_time0_interrupt
lab_p01_0:    
        bclr        IOP0,1
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
    jbclr       IOP0,0
    goto        lab_p00_0
    bset        IOP0,0
    goto        end_time1_interrupt
lab_p00_0:    
    bclr        IOP0,0
    
    goto	end_time1_interrupt

end_time1_interrupt:
;**********end_timer1******
just_int0:
    jbclr    INT0IE
    jbset    INT0IF
    goto     just_int1
;**********int0************
    bclr    INT0IF
    
;*********end int0*********
just_int1:
    jbclr    INT1IE
    jbset    INT1IF
    goto     just_adc
;**********int0************
    bclr    INT1IF

;*********end int1*********
just_adc:
     jbclr	ADIE
     jbset	ADIF
     goto	just_kbim
;***********adc ***********
     bclr	ADIF
;***********end adc********
just_kbim:
    jbclr    KBIE
    jbset    KBIF
    goto    end_just_int
;**********kbim************
    bclr    KBIF

;**********end_kbim********
end_just_int:
exit_interrupt:
    swapar    status_temp
    movra    STATUS
    swapr    acc_temp
    swapar    acc_temp
    retie

start:
        bclr     GIE
        movai   0xff
        movra   0x195    ;as output
        movai   0x3f
        movra   OEP1    ; p16 as input 
        movai   0xff       
        movra  PUP0
        movra  PUP1
        
        movai   0x7f
        movra   FSR0
        movai   0x00
lab_set_ram:
        inca       
        movra    INDF0
        ;decr    FSR0
        djzr    FSR0 ;FRS0-1=0,skip next instruction
        goto    lab_set_ram 
        movra    INDF0   ;clr 0x00 addr

lab_clr_ram:        
        clrr    INDF0
        ;decr    FSR0
        djzr    FSR0 ;FRS0-1=0,skip next instruction
        goto    lab_clr_ram 
        clrr    INDF0   ;clr 0x00 addr
        
        bset    model00;
        bset    GIE
        
loop:
        call    keyscan
        call    fun_timer
        ;call    fun_timer

        goto loop
        
keyscan:
        jbclr   IOP1,6 ;
        goto    end_keyscan
        call    delay10ms
        call    delay10ms
        call    delay10ms
        jbclr   IOP1,6
        goto    end_keyscan
        
        movar   model_flag2
        jbset   Z ;if model_flag2=0,skip next intr
        goto    change_model2
        ;model1 reg
        bclr    C
        rlr     model_flag1
        jbclr   C
        bset    model08 ; turn to flag2
        goto    cancle_end_keyscan
change_model2:
        bclr    C
        rlr     model_flag2
        jbclr   C
        bset    model00 ; turn to flag2                                
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
        
fun_timer:
        call    fun_timer0001
        call    fun_timer0002
        call    fun_timer0003
        call    fun_timer0004
        call    fun_timer0005
        call    fun_timer0006
        call    fun_timer0007
        call    fun_timer0008
        ;call    fun_timer0003        

        return        
        
fun_timer0001
        jbset   model00
        goto    end_timer001
        ;bclr    model00
        ;t0:64us, t1:8.2ms, pwm1(p13)=1/2t1, pwm0(p12)=1/2t0
        movai   11000000b ; enable t0,pwm t0pts=0,t0pr=0
        movra   T0CR
        movai   11000111b ; enable t0,pwm t0pts=0,t0pr=0
        movra   T1CR
        
        movai   0xff
        movra   T0LOAD
        movra   T1LOAD
        movai   0x7f
        movra   T0DATA ;pwm H time seting
        movra   T1DATA
        
        bset    T0IE
        bset    T1IE
        
end_timer001:        
        return
        
fun_timer0002
        jbset   model01
        goto    end_timer002
        ;bclr    model01
        ; clk=FHOSC ,t0=t1=32,pwm0=pwm1=1/2t0
        movai   11001000b ; enable t0,pwm t0pts=0,t0pr=0
        movra   T0CR
        movra   T1CR
                
end_timer002:        
        return
fun_timer0003
        jbset   model02
        goto    end_timer003
        ;bclr    model02
        ; t0=t1=9.5ms ,~27KHZ
        movai   11010000b ; FLOSC
        movra   T0CR
        movra   T1CR
                
end_timer003:        
        return

fun_timer0004
        jbset   model03
        goto    end_timer004
        ;bclr    model02
        ;INT0(P10),8mhz,32us INT1
        movai   11011000b ; INT0
        movra   T0CR
        movra   T1CR
        bclr    OEP1,0
        bclr    OEP1,1    
                
end_timer004:        
        return

fun_timer0005    ;div
        jbset   model04
        goto    end_timer005
        ;bclr    model03
        ;8mhz osc ,t0=64us,t1=32us
        movai   11001001b ; HFOSC DIV=2
        movra   T0CR
        movai   11001000b ; HFOSC DIV=1
        movra   T1CR
                
end_timer005:        
        return 
        
fun_timer0006
        jbset   model05
        goto    end_timer006
        ;bclr    model02
        ;8mhz, t0=256us, t0=1/2t0
        movai   11001011b ; HFOSC DIV=8
        movra   T0CR
        movai   11001010b ; HFOSC DIV=4
        movra   T1CR
                
end_timer006:        
        return
fun_timer0007
        jbset   model06
        goto    end_timer007
        ;bclr    model02
        
        movai   11001101b ; HFOSC DIV=32
        movra   T0CR
        movai   11001100b ; HFOSC DIV=16
        movra   T1CR
                
end_timer007:        
        return
        
fun_timer0008
        jbset   model06
        goto    end_timer008
        ;bclr    model02
        
        movai   11001111b ; HFOSC DIV=128
        movra   T0CR
        movai   11001110b ; HFOSC DIV=128
        movra   T1CR
                
end_timer008:        
        return         

        end    