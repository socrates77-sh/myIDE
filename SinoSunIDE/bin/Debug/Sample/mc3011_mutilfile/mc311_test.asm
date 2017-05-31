#include mc30p011.inc
#include var.inc


#define sks 0x20a5

;extern akbk;
.code
    
    org 3ffh
    clrr INTE ; disable all interrupt
              
    org         00h ;ksks
    goto        lab_start
    movai       low(sks)
    movai high(sks)

    ;movar akbk
   
    org 08h
fn_interrupt_sub:
    movra    acc_temp
    movar    STATUS
    movra    status_temp
;*******判断是哪个中断*****
start_just_int:
 ;int0
    jbset    INT0IF
    goto    exit_interrupt
    bclr    INT0IF
 nop
 nop
exit_interrupt:
    movar    status_temp
    movra    STATUS
    movar    acc_temp
    retie
;;*******软中断代码***********
;soft_interrupt:
;    movar    status_temp
;    movra    STATUS
;    movar    acc_temp
;    retie
; rti  


  
lab_start:
    ;fill    0x0000,5
    
    ; dt "abcde"
    movai   4|6;
    movai   '\t'
    movra   time_10ms
    movra   time_100ms
    movra   P5IOC ;p0 output
    movai   0xFF
    movra   P6IOC ;p1 input
    bclr    PUCON,0    ;开P10上拉    bclr    INT0M
    movra   r1
      ;movai 0x12
      ;movra FSR
      ;movai 0x0a
      ;movra INDF
      ;incr INDF
      ;movai 0x20
      ;movra FSR
      ;incr INDF
      ;movai 0x34
      ;movra FSR
      ;incr INDF
      ;incr INDF
      
      
      
      bset GIE
      
loop: 
    movai 0x55
    movra 0x10
    movra 0x4e
    incr  0x10
 ;   goto $-1
    
    call int0_cfg_l
    ;sleep;    
    movai 0xfe
     movra   P0
    nop
    call fun001
    nop
    movai 0x00
    movra P0
    nop
    nop
    call int0_cfg_h
    ;sleep
    ;movai 0xfe
    ;movra   P0
    ;nop
    ;nop
    ;movai 0x00
    ;movra P0
    ;nop
    ;nop
    goto loop
    
fun001:
    nop
    call fun002
    nop
    return
fun002:
    nop
    call fun003
    nop
    return
fun003:
    nop
    return    
;*****int0 cfg************************
;*****下降沿触发**********************
int0_cfg_l:    
    bset    DDR1,0    ;P10输入    
    bclr    PUCON,0    ;开P10上拉    
    bclr    INT0M    
    bset    EIS    
    bset    INT0IE    ;开中断    
    bclr    WDTEN    
    ;sleep    
    return;
;**************************************
int0_cfg_h:    
    bset    DDR1,0    ;P10输入    
    bclr    PUCON,0    ;开P10上拉    
    bset    INT0M    
    bset    EIS    
    bset    INT0IE    ;开中断    
    bclr    WDTEN    
    ;sleep    
    return;
    
    end
    
    bsset DDR1,0
    