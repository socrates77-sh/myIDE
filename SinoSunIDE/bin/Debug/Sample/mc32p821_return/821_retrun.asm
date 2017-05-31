

#include "mc32p821.inc"


#define     RAM_first_size  0x80
  


cblock  0x0
    acc_temp            ;save the ACC value
    status_temp             ;save the ACC status
 work01
 work02
 work03 

endc

        org 0x00
        goto    main

        org 0x8
intport:
        movra   acc_temp            ;save the ACC status
        swapar  STATUS              ;save the CPU status
        movra   status_temp
int_t2:
        jbset   T2IE
        goto    int_t0
        jbset   T2IF
        goto    int_t0
        bclr    T2IF
        ;----
        jbset   IOP0,2
        goto    set_p02_h
        bclr    IOP0,2
        goto    int_t0
set_p02_h:        
        bset    IOP0,2         
int_t0:
        jbset   T0IE
        goto    int_t1
        jbset   T0IF
        goto    int_t1
        bclr    T0IF
        ;---
 

int_t1:        
        jbset   T1IE
        goto    int_kbi
        jbset   T1IF
        goto    int_kbi
        bclr    T1IF
        ;---
        jbset   IOP0,3
        goto    set_p13_h
        bclr    IOP0,3
        goto    int_kbi
set_p13_h:        
        bset    IOP0,3        

int_kbi:
        jbset   KBIE
        goto    int_int0
        jbset   KBIF
        goto    int_int0        
        bclr    KBIF    
        incr    work01
         
int_int0:
        jbset   INT0IE
        goto    int_int1
        jbset   INT0IF
        goto    int_int1
        bclr    INT0IF
        ;-----

 
int_int1:
        jbset   INT1IE
        goto    end_int
        jbset   INT1IF
        goto    end_int
        bclr    INT1IF
        ;----        
;        jbset   IOP1,2
;        goto    set_p12_h
;        bclr    IOP1,2
;        goto    end_int
;set_p12_h:        
;        bset    IOP1,2        
end_int:        
        swapar  status_temp
        movra   STATUS
        movar   acc_temp
        retie

main:
        bclr    GIE
        
        movai   0xff
        movra   IOP0
        movra   IOP1
        movra   OEP0
        movra   OEP1
        
        movai   0xff
        movra   PUP0
        movra   PUP1

        call    fn_timer_initial
        
        bset    GIE        
        
loop:
        ;;wdt test
        ;stop
        ;comr    IOP0
        ;clrwdt
       
        goto    loop        
        
fn_timer_initial:
                
        clra    ; 
        orai    T0PR_DIV4 ;T0PR_DIV1,2,4,8,16,32,64,128
        orai    T0PTS_FCPU ;
        movra   T0CR
        
        ;bset    BUZ0OE ;
        ;bset    PWM0OE
        
        ;INT0 AS INPUT
        bclr    OEP1,0
        
        movai   0x80
        movra   T0LOAD
        bset    TC0EN
        
        ;movai   0x40     ;PWM
        ;movra   T0DATA        
        ;bset    T0IE
        ;T1----------------------------------------
        clra    ; 
        orai    T0PR_DIV4 ;T0PR_DIV1,2,4,8,16,32,64,128
        orai    T0PTS_FCPU ;FCPU,FHOSC,FLOSC,INT0/INT1
        movra   T1CR
        
        ;set int1 as input
        bclr    OEP1,1
        
        ;bset    BUZ1OE ;
        bset    PWM1OE
        
        movai   0x20
        movra   T1LOAD
        bset    TC1EN
        
        movai   0x10     ;PWM
        movra   T1DATA
        
        movai   0x80
        movra   PWMCR
        
        ;bset    T1IE
        ;T2 initial
        clra    ; 
        orai    T0PR_DIV8 ;T0PR_DIV1,2,4,8,16,32,64,128
        orai    T0PTS_FCPU ;FCPU,FHOSC,FLOSC,INT0/INT1
        movra   T2CR               
        movai   0x20
        movra   T2LOAD
        bset    TC2EN
        ;nop
        call    fun001
        bset    T2IE

        
        return        
fun001:
    nop
        NOP
        return    


        end        