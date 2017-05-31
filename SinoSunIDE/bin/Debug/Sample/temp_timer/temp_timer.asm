
; mc32p21 timer test funtion
; timer.asm

#define SECONDS_PER_YEAR (60*60*24*365)UL

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




        org  0x0000
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
        bclr    IOP0,1
        bset    IOP0,1
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
    bclr        IOP0,0
    bset        IOP0,0
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
        movai   0xa0
        movra   r0
        movai   0xa3
        movra   r1
        
        
        bclr     GIE
        movai   0Xff
        movra   OEP0    ;as output
        movai   0x3f
        movra   OEP1    ; p16 as input 
        movai   0xff       
        movra  PUP0
        movra  PUP1
        
        movai   0x7f
        movra   FSR0

lab_clr_ram:        
        clrr    INDF0
        ;decr    FSR0
        djzr    FSR0 ;FRS0-1=0,skip next instruction
        goto    lab_clr_ram 
        clrr    INDF0   ;clr 0x00 addr
        
                movai   11000000b ; enable t0,pwm t0pts=0,t0pr=0
        movra   T0CR
        ;movai   11000001b ; enable t0,pwm t0pts=0,t0pr=0
        movra   T1CR
        
        movai   0xff
        movra   T0LOAD
        movra   T1LOAD
        movai   0x7f
        movra   T0DATA ;pwm H time seting
        movra   T1DATA
        
        bset    T0IE
        ;bset    T1IE
        bset    GIE
        
loop:
        nop

        goto loop
        

        end    