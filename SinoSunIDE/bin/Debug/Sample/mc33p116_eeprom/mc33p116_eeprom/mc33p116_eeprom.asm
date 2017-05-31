;p30--SCL
;p27--SDA

#include "mc33p116.inc"
;====����RAM=============================
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
cblock        0x10 ;��������������
    flag1

    temp            ;��ʱ�Ĵ���
    temp1           ;��ʱ�Ĵ���
    address         ;��ַ
    data0           ;����    
    count 
    

endc
;===end ram=================================================

;================================================================
    org         0x3ff     ;��λ����
    bclr        GIE       ;�����ж�
    org         0x00 
    goto        sys_start
;================================================================
    ORG         0x08      ;�жϵ�ַ
;----------------------------------------------------------------

;==start����ʼ===============================================
sys_start:     
     bclr        GIE       ;�����ж�    
sys_init:    
    ;==��ʼ��IO==============     ; 
    movai     0xff
    movra     OEP4     ;p3��Ϊ�����
    movra     OEP3     ;p3��Ϊ�����
    movra     OEP2     ;p2��Ϊ�����
    movra     OEP1     ;p1��Ϊ����� 
    movra     OEP0     ;p0��Ϊ����� 
    clrr      IOP3           
    clrr      IOP2      

    
     bset      sda
     bset      scl
     bset      PUP2,7     ;���� 
    movai     15
    movra     address

    movai     0x6a
    movra   data0     
    call      write24c02      ;д����
    call      delay10ms       ;ȷ��������д��
    ;У��д���Ƿ���ȷ
    movai     0x00
    movra     temp
    call      read_24c02      ;��ȡ����
    call      aknow_end_out   ;����������ݷ���aknow_out
    call      stopa           ;����������
    movar     data0
    rsubar    temp            ;У��д��������������Ƿ�һ��
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
       bclr        errflag          ;���־λ
       call        starti2c         ;��������д�ź�
       movai       0xa0            ;����IC��ַ
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;���ȷ���ź�
       goto        write24c02       ;ʧ�ܵĻ����·���
 
       movar       address
       movra       temp             ;���͵�ַ
       call        byte_write
       call        aknowledge
       jbclr       errflag
       goto        write24c02
       movar       data0
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;���ȷ���ź�
       goto        write24c02
       call        stopa
       return
;==========================================================
;==========================================================
read_24c02:
       call        starti2c          ;��������д24c02���ź�
       movai       0a0h             ;
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;���ȷ���ź�
       goto        read_24c02       ;ʧ�ܵĻ����·���
       movar       address         ;���͵�ַ
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;���ȷ���ź�
       goto        read_24c02       ;ʧ�ܵĻ����·���
       call        starti2c
       movai       0a1h            ;���Ͷ��ֽ�����
       movra       temp
       call        byte_write
       call        aknowledge
       jbclr       errflag         ;���ȷ���ź�
       goto        read_24c02      ;ʧ�ܵĻ����·���
       call        byte_read        ;����������
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
byte_write:  ;дһ��byte
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
starti2c:   ;��ʼ�ź�
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
stopa:   ;�����ź�
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
