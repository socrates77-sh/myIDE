#include mc30p01a.inc



cblock 0x10 
acc_temp
status_temp
r1
r2
work_flag1
work_flag2
error_flag

time_125us
time_1ms
time_10ms
time_100ms
time_1s
time_1min
time_1hour
endc

#define r10 r1,0
#define r11 r1,1

#define sks 0x20a5

;extern akbk;
.code
    org 3ffh
    clrr INTE ; disable all interrupt
              
    org 00h ;ksks
    goto lab_start
    movai low(sks)
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
 nop
 nop
 nop
 NOP
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
    movra   P5IOC ;p0 output
    movai   0xFF
    movra   P6IOC ;p1 input
    bclr    PHCON,0    ;开P10上拉    bclr    INT0M
    movai       0xaa
    movra      r1 
    bset      r10
    nop
    nop
    nop
    
    
      movai 0x12
      movra FSR
      movai 0x0a
      movra INDF
      incr INDF
      movai 0x20
      movra FSR
      ;incr INDF
      ;movai 0x34
      ;movra FSR
      ;incr INDF
      ;incr INDF
      
      movai 0x10
      movra 020h
      bclr  020h,1
      nop
      NOP
      jbset 020h,1
      nop 
      bset  0x3f,0
      jbclr 020h,1
      nop 
      nop
      
      
      bset GIE
      
loop: 
    movai 0x55
    movra 0x10
    incr  0x10
 ;   goto $-1
        NOP
        nop
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
    bclr    PHCON,0    ;开P10上拉    
    bclr    INT0M    
    bset    EIS    
    bset    INT0IE    ;开中断    
    bclr    WDTEN    
    ;sleep    
    return;
;**************************************
int0_cfg_h:    
    bset    DDR1,0    ;P10输入    
    bclr    PHCON,0    ;开P10上拉    
    bset    INT0M    
    bset    EIS    
    bset    INT0IE    ;开中断    
    bclr    WDTEN    
    ;sleep    
    return;
    
    end
    
    bsset DDR1,0
    