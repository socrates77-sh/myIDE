;----------------------------------------------------------------------------------------------
;MC32p21+CP4050
;
;----------------------------------------------------------------------------------------------

#include "mc32p21.inc"

;=================================IO Define============================
#define BatVolIn    IOP0,0      ;VREF in for check battery voltage
#define ChargeIn    IOP0,1      ;Charge in for check Power insert
#define ENBCtrl     IOP0,2      ;GPIO out for Enable/Disable the ENB of CP4050
#define BatTemIn    IOP0,3      ;Voltage in for check battery tmperature    
#define DeviceIn    IOP0,4      ;Voltage in for check device insert and OCP

#define MosCtrl     IOP1,0      ;GPIO out for control the Mos Enable/Disable
#define LEDCtrl     IOP1,3      ;GPIO out for control the LED Open/Close
#define ENCCtrl     IOP1,4      ;GPIO out for Enable/Disable the ENC of CP4050
#define KeyIN       IOP1,5      ;Key in

;=================================Register Define======================
cblock 0x00
    acc_temp            ;save the ACC value
    status_temp             ;save the ACC status
    timer0_10ms         ;
    timer1_20ms         ;
    system_flag         ;
    abc0
    abc1

endc








;=================================OTP COde=============================
    ORG 0x0000
    bclr    GIE
    goto    MainProcEntry
    movai  00001111B

    
    ORG 0x0008
IntProcEntry:
    movra   acc_temp            ;save the ACC status
    swapar  STATUS              ;save the CPU status
    movra   status_temp
    ;movar  STATUS
    ;movra  status_temp


;Timer0 interrupt
timer0_int:         
    jbset   T0IF                ;1:INT , 0:nothing
    goto    timer1_int
    bclr    T0IF



end_timer0_int:

timer1_int:                 ;Timer1 interrupt
    jbset   T1IF                ;1:INT, 0:nothing
    goto    ADC_int 
    bclr    T1IF
 movai  0x02
    movra   ADCR1               ;VRS=VDD
    movai   0x5F
    movra   ADCR0
 bclr  ADEOC

end_timer1_int:

ADC_int:                    ;ADC interrupt
    jbset   ADIF                ;1:INT, 0:nothing
    goto    kbim_int
    bclr    ADIF
 jbset  ENCCtrl
 goto  _set_1
_set_0
 bclr   ENCCtrl
 goto end_ADC_int
_set_1
 bset   ENCCtrl
end_ADC_int:

kbim_int:                   ;Keyboard interrupt
    jbset   KBIF                ;1:INT, 0:nothing
    goto    end_int
    bclr    KBIF
end_kbim_int:

end_int:
    swapar  status_temp
    movra   STATUS
    ;movar  status_temp
    ;movra  STATUS
    movar   acc_temp
    retie


;-----------------------------------------------------------------------
;Func: Hw_init()
;Decr:
;   1.OSC module config
;   2.IO port module config
;   3.Timer0 module config
;   4.Timer1 module config
;   5.ADC module config
;   6.Interrupt module config
;   7.KeyBoard module config
;
;-----------------------------------------------------------------------
Hw_init:
    ;----------------1:OSC model config------------------
    bclr    CLKS
    bset    LFEN
    bclr    HFEN
    ;----------------2:IO port model config--------------
    movai   0x00
    movra   IOP0
    movra   IOP1
    movai   0x04                ;0000 0100, 1:Output 0:Input
    movra   OEP0
    movai   0x59                ;0101 1001, 1:Output 0:Input
    movra   OEP1

    ;movai  0x1B
    movai 0x00
 movra  PUP0
    movai   0x20                ;0010 0000, 1:push up
    movra   PUP1
    ;----------------3:Timer0 model config---------------
    ; 35*8*1/28ms = 10ms
    movai   0x14                ;TOPR=3(8 pref), T0PTS=10(FLOSC)    
    movra   T0CR
    movai   128
    movra   T0LOAD
    ;----------------4:Timer1 model config---------------
    ; 35*16*1/28ms = 20ms
    movai   0x14                ;T1PR=3(8 pref), T1PTS=10(FLOSC)
    movra   T1CR
    movai   128
    movra   T1LOAD  
    ;----------------5:ADC model config------------------
    movai   0x38                ;0011 1000, 1:A Input 0: IO
    movra   ANSEL
    ;----------------6:Interrupt model config------------
    movai   0x13                ;0101 0011
    movra   INTE
    ;----------------7:KeyBoard model config-------------
    movai   0x20                ;0010 0000, 1:Enable 0:Disable
    movra   KBIM

    return


;-------------------------------- Main ----------------------------------
MainProcEntry:
    ;--------init the hardware and software-----
    call    Hw_init
 bset   TC1EN               ;enable the Timer1
 bset   TC0EN               ;enable the Timer1
 bset GIE
  bclr  OEP1,6
LoopBegin:

    ;--------Main loop sw timer----------------
 NOP
    incr    timer1_20ms
    incr    abc0
    incr    abc1
    goto    LoopBegin
;SleepMode: ;ÐÝÃß
;   jbset   SleepFlag
;   goto    start_1
;   bclr    SleepFlag
;   nop
;   nop
;   nop
;   nop
;   nop
;   stop
;   nop
;   nop
;   nop
;   nop
;   goto    start_1
    end
