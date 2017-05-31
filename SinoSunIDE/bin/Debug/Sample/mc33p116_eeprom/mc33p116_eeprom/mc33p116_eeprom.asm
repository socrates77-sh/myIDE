;p30--SCL
;p27--SDA

#include "mc33p116.inc"
;====特殊RAM=============================
;R0      equ    0x07
DDR0    equ    0x45
DDR1    equ    0x46
PUCON   equ    0x0D

;================================================================
#define         errflag        flag1,1

#define         sda            IOP2,7
#define         scl            IOP3,0
#define         ioset          OEP2,7
;================================================================
;====ram=============================
cblock        0x10 ;变量块连续定义
    flag1

    temp            ;临时寄存器
    temp1           ;临时寄存器
    address         ;地址
    data0           ;数据    
    count 
    

endc
;===end ram=================================================

;================================================================
    org         0x3ff     ;复位向量
    bclr        GIE       ;关总中断
    org         0x00 
    goto        sys_start
;================================================================
    ORG         0x08      ;中断地址
;----------------------------------------------------------------

;==start程序开始===============================================
sys_start:     
     bclr        GIE       ;关总中断    
sys_init:    
    ;==初始化IO==============     ; 
    movai     0xff
    movra     OEP4     ;p3作为输出口
    movra     OEP3     ;p3作为输出口
    movra     OEP2     ;p2作为输出口
    movra     OEP1     ;p1作为输出口 
    movra     OEP0     ;p0作为输出口 
    clrr      IOP3           
    clrr      IOP2      

    
     bset      sda
     bset      scl
     bset      PUP2,7     ;上拉 
    movai     15
    movra     address

    movai     0x6a
    movra   data0     
    call      write24c02      ;写数据
    call      delay10ms       ;确保数据已写入
    ;校验写入是否正确
    movai     0x00
    movra     temp
    call      read_24c02      ;读取数据
    call      aknow_end_out   ;如果续读数据发送aknow_out
    call      stopa           ;结束读数据
    movar     data0
    rsubar    temp            ;校验写入与读出的数据是否一致
    jbset     Z
    goto      check_error     
 
check_ok:;bset   stepflag
       movar    temp
       movra	IOP1	;read data
       bset	IOP3,4
       bclr    	IOP3,4
       goto	check_ok
check_error:       
       movar    temp
       movra   	IOP1   	;read data
       bset    	IOP3,5
       bclr    	IOP3,5
       goto    	check_error       

write24c02:
       bclr        errflag          ;清标志位
       call        starti2c         ;发送启动写信号
       movai       0xa0            ;发送IC地址
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;检测确认信号
       goto        write24c02       ;失败的话重新发送
 
       movar       address
       movra       temp             ;发送地址
       call        byte_write
       call        aknowledge
       jbclr       errflag
       goto        write24c02
       movar       data0
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;检测确认信号
       goto        write24c02
       call        stopa
       return
;==========================================================
;==========================================================
read_24c02:
       call        starti2c          ;发送启动写24c02的信号
       movai       0a0h             ;
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;检测确认信号
       goto        read_24c02       ;失败的话重新发送
       movar       address         ;发送地址
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;检测确认信号
       goto        read_24c02       ;失败的话重新发送
       call        starti2c
       movai       0a1h            ;发送读字节命令
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;检测确认信号
       goto        read_24c02      ;失败的话重新发送
       call        byte_read        ;启动读数据
       return
;==========================================================
;==========================================================
;==========================================================
byte_read:
       movai       08h
       movra       count
       bclr        ioset
read_start:
       bclr        scl
       call        delay5us
read_out:
       bset        scl
       bclr        C
       jbclr       sda
       bset        C
       rlr         temp
       djzr        count
       goto        read_start
       bset        ioset
       bclr        scl
       return
;============================================================
;============================================================
;============================================================
aknow_out:
       bclr        sda
       nop
       nop
       nop
       nop
       bset        scl
       nop
       nop
       return
;============================================================
;============================================================
aknow_end_out:
       bclr        scl
       bset        sda
       bset        ioset  
       nop
       nop
       bset        scl
       nop
       nop
       return
;=============================================================
;=============================================================
aknowledge:
       bclr        ioset
       bset        scl
       call        delay5us
       jbclr       sda
       bset        errflag
       bset        ioset
       bclr        scl
       return
;=============================================================
;=============================================================
;=============================================================
byte_write:  ;写一个byte
       movai       08h
       movra       count
write_start:
       jbset       temp,7
       goto        write0
       bset        sda
       goto        write_count
write0:
       bclr        sda
write_count:
       rlr         temp
       bset        scl
       call        delay5us
       bclr        scl
       djzr        count
       goto        write_start
       return
;=============================================================
;=============================================================
;============================================================
starti2c:   ;开始信号
       bset        sda
       bset        scl
       call        delay5us
       bclr        sda
       call        delay5us
       bclr        scl
       call        delay5us
       return
;=============================================================
;=============================================================
stopa:   ;结束信号
       bclr        scl
       bclr        sda
       call        delay5us
       bset        scl
       call        delay5us
       bset        sda       
       return
;=============================================================
;=============================================================
;=============================================================
delay10ms:
       clrr        temp
       movai       09h
       movra       temp1
delay10ms1:
       nop
       djzr        temp
       goto        delay10ms1
       djzr        temp1
       goto        delay10ms1
       return
;=============================================================

delay5us:
       nop
       nop
       return
;=============================================================
;=============================================================


 	end
