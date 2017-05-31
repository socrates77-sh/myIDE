#include mc30p01.inc

#define    RAM_COUNT    48    ;48个RAM
#define    FIR_RAM_ADDR    0x10    ;RAM首地址

#define    RAM_E    0x00        ;RAM错误报E0
;***************特殊RAM***********
R0    equ    0x07
T0DATA    equ    0x41
DDR0    equ    0x45
DDR1    equ    0x46

#define    IO_TM1628_STB    P0,0
#define    IO_TM1628_CLK    P0,1
#define    IO_TM1628_DATA    P0,2
#define    IO_TM1628_DDR    DDR0,2
;*************ram****************
cblock    0x00
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

dis_buffer0
dis_buffer1
dis_buffer2
dis_buffer3
tm_data0
tm_data1
tm_data2
tm_data3
tm_data4
tm_data5
tm_data6
tm_data7
key_data_buffer0
key_data_buffer1
key_data_buffer2
key_data_buffer3
key_data_buffer4
endc
;***************end ram*************
#define    dis_buffer4    tm_data4
#define    dis_buffer5    tm_data5
#define    dis_buffer6    tm_data6
#define    dis_buffer7    tm_data7
#define    disp0_point    tm_data0,4
#define    disp1_point    tm_data1,4
#define    disp2_point    tm_data2,4
#define    disp3_point    tm_data3,4
#define    key_data1    key_data_buffer0,0    ;k1
#define    key_data2    key_data_buffer0,3    ;k2
#define    key_data3    key_data_buffer3,0    ;k3

#define flag_time_10ms         work_flag1,0
#define flag_key1_effect    work_flag1,1
#define flag_key2_effect    work_flag1,2
#define flag_key3_effect    work_flag1,3
#define flag_key1_effect_bak    work_flag1,4
#define flag_key2_effect_bak    work_flag1,5
#define flag_key3_effect_bak    work_flag1,6

#define    flag_disp0_point    work_flag2,0
#define    flag_disp1_point    work_flag2,1
#define    flag_disp2_point    work_flag2,2
#define    flag_disp3_point    work_flag2,3

    org     0x3ff        ;复位向量
 goto   start
     ;bclr    GIE    ;关中断
    org     0x00
    goto    start
    nop         ;0001H
    NOP         ;0002H
    
;    org     0x002        ;软中断向量
;    movra    acc_temp    ;02
;    movar    STATUS        ;03
;    movra    status_temp    ;04
;
;    goto    soft_interrupt    ;07

    ORG 0x08        ;其它中断向量
interrupt_sub:
    movra    acc_temp
    movar    STATUS
    movra    status_temp
;*******判断是哪个中断*****
start_just_int:
    jbset    T0IF
    goto    just_int0
;**********timer0**********
    bclr    T0IF
    incr    time_125us
    jbset    time_125us,3
    goto    end_time0_interrupt
    clrr    time_125us
    incr    time_1ms
    movar    time_1ms
    ;subai    10        ;10-acc
    jbclr    Z
    goto    end_time0_interrupt
    clrr    time_1ms
    bset    flag_time_10ms
end_time0_interrupt:
;**********end_time0*******
just_int0:
    jbset    INT0IF
    goto    just_kbim
;**********int0************
    bclr    INT0IF
;*********end int0*********
just_kbim:
    jbset    KBIF
    goto    end_just_int
;**********kbim************
    bclr    INT0IF
;**********end_kbim********
end_just_int:
    movar    INTF
    andai    0x07        ;低三位
    jbclr    C
    goto    start_just_int
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

;*******程序开始*************
;*********start***************
start:
    bclr    WDTEN        ;关看门狗
    bclr    EIS
 movra T0DATA
 ;goto ram_error
clr_ram:
     movai    0x55
    movra    R0        ;07地址也开放给用户
    nop
    movar    R0
 ;   nop
    ;    nop
    ; subai    0xff
  ;bclr C
    jbset    C
    goto    ram_error
    clrr    R0
    nop
    movar    R0
 ;BCLR C
    jbset    C
    goto    ram_error
    ;goto    ram_error
    movai    FIR_RAM_ADDR
    movra    FSR        ;首地址
    movai    RAM_COUNT
    movra    R0
clr_ram_loop:
    movai    0xff
    movra    INDF        ;把FF写到FSR所指的地址
    nop
    movar    INDF        ;写完后读出来
    ; subai    0xff
    jbset    Z        ;读出来后如果是FF，相减后为0，Z为1
    goto    ram_error    ;先写FF

    incr    INDF
    movar    INDF        ;0xff+1后为0
    jbset    Z
    goto    ram_error    ;若不为0则是出错

    incr    FSR        ;FSR指向下一个地址
    djzr    R0        ;R0-1==0则跳过一行
    goto    clr_ram_loop
    goto    sys_init
ram_error:
    movar    RAM_E
    movra    error_flag
;*****************************************
sys_init:
    movai    1    ;8M,2T,4分频
    movra    T0DATA        ;写入T0CR 
    movai    (256-125)
    movra    T0CNT    ;定时125us
    bset    T0IE    ;开定时中断

    clrr    DDR0
    clrr    DDR1    ;全部作为输入口
    clrr    P0
    clrr    P1
;***************************************
    clrwdt
    bset    WDTEN
    bset    GIE    ;开全局中断
;************main loop***************
main_loop:
    clrwdt
    call    time_deal
    call    tm_1628_read_sub    ;读按键
    call    key_scan        ;整理读回来的按键数据
    call    key_deal        ;按键处理
    call    work_deal
    call    display
    call    tm_1628_write_sub    ;写显示数据
    goto    main_loop
;************time sub*************
time_deal:
    jbset    flag_time_10ms
    goto    end_time_deal
    bclr    flag_time_10ms
    incr    time_10ms
end_time_deal:
    return
;************key_scan*************
key_scan:
;**********k1***********
scan_key1
    jbset    key_data1
    goto    clr_key1_bak
    
    jbclr    flag_key1_effect
    goto    scan_key2

    bset    flag_key1_effect
    bset    flag_key1_effect_bak
    goto    scan_key2
clr_key1_bak:
    bclr    flag_key1_effect_bak
;**********k2************
scan_key2:
    jbset    key_data2
    goto    clr_key2_bak
    
    jbclr    flag_key2_effect
    goto    scan_key3

    bset    flag_key2_effect
    bset    flag_key2_effect_bak
    goto    scan_key3
clr_key2_bak:
    bclr    flag_key2_effect_bak
;**********k3************
scan_key3:
    jbset    key_data3
    goto    clr_key3_bak
    
    jbclr    flag_key3_effect
    goto    end_key_scan

    bset    flag_key3_effect
    bset    flag_key3_effect_bak
    goto    end_key_scan
clr_key3_bak:
    bclr    flag_key3_effect_bak
end_key_scan:
    return
;***********key_deal**************
key_deal:
end_key_deal:
    return
;***********work sub**************
work_deal:
end_work_deal:
    return
;********************************
display:
;*************查表开始***************
    clrr    R0                ;计数4次
load_table_loop:
    movai    dis_buffer0
    addar    R0
    movra    FSR                ;每次循环更新一次FSR指针
    
    movar    INDF        
    movai    0x01                
    call    table_dis_code
    movra    r1                ;r1存放查表的临时值
    movai    dis_buffer0+4
    addar    R0
    movra    FSR
    movar    r1
    movra    INDF                ;把查表值（r1）存储到tm_data中

    incr    R0
    jbset    R0,2
    goto    load_table_loop

;*************小数点的显示************
    jbclr    flag_disp0_point
    bset    disp0_point
    jbclr    flag_disp1_point
    bset    disp1_point
    jbclr    flag_disp2_point
    bset    disp2_point
    jbclr    flag_disp3_point
    bset    disp3_point
;***************其它段的显示处理*********
end_display
    return

;***************1628读按键数据************
tm_1628_read_sub:
    movai    key_data_buffer0
    movra    FSR

    movai    5    ;循环5次
    movra    r2

    movai    0x42
    movra    r1
    call    tm_1628_write
tm1628_read_key_loop:
    call    tm_1628_read
    movar    r1
    movra    INDF
     
    incr    FSR
    djzr    r2
    goto    tm1628_read_key_loop
end_tm_1628_read:
    return
;***********1628 写显示数据***************
tm_1628_write_sub:
    movai    0x03
    movra    r1
    call    tm_1628_write
    bset    IO_TM1628_STB
    movai    0x40
    movra    r1
    call    tm_1628_write
    bset    IO_TM1628_STB
    movai    0xc0
    movra    r1
    call    tm_1628_write
    movai    tm_data0            ;首地址
    movra    FSR
    clrr    r2                ;r2计算发送buffer的次数
send_display_buffer:
    movar    INDF
    movra    r1
    call    tm_1628_write
    call    tm_1628_write
    incr    FSR
    incr    r2
    jbset    r2,3                ;够八次就跳出
    goto    send_display_buffer

    bset    IO_TM1628_STB
    movai    0x88
    movra    r1
    call    tm_1628_write
    bset    IO_TM1628_STB
end_tm_1628_write:
    return
;*********************************
;***************1628***************
;*********占用r1,R0寄存器***********
;*********r1作为参数****************
tm_1628_write:
    bclr    IO_TM1628_STB
    bset    IO_TM1628_DDR    ;data output
    clrr    R0        ;计数用
    bset    R0,3        ;R0=8
tm1628_send_data_loop:
    bclr    IO_TM1628_DATA
    bclr    IO_TM1628_CLK

    jbclr    r1,0        ;发送0位数据
    bset    IO_TM1628_DATA
tm1628_clk_rising:
    rrr    r1
    nop
    nop
    nop
    nop
    nop
    bset    IO_TM1628_CLK
    djzr    R0
    goto    tm1628_send_data_loop
    return

tm_1628_read:
    bclr    IO_TM1628_STB
    bclr    IO_TM1628_DDR    ;data input
    clrr    R0        ;计数用
    bset    R0,3        ;R0=8
tm1628_receive_data_loop:
    bclr    IO_TM1628_CLK
    nop
    nop
    nop
    bclr    C
    jbclr    IO_TM1628_DATA
    bset    C
    rrr    r1
    bset    IO_TM1628_CLK
    djzr    R0
    goto    tm1628_receive_data_loop
    return
;**********************************
table:
    org (0x3fe-18)
    retai    0x40
    retai    0x00
    retai    0xC5
    retai    0xCD
    retai    0x6E
    retai    0x8D
    retai    0x6D
    retai    0xE7
    retai    0xEB
           retai    0xEF
    retai    0xA2
    retai    0xED
    retai    0xE9
    retai    0x63
    retai    0xEA
    retai    0xCE
    retai    0x22
    retai    0xAF
    org    0x3fe
table_dis_code:
    ;subra    PCL
    end
