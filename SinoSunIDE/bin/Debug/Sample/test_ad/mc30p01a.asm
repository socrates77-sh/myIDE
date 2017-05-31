#include mc32p21a.inc
#define	 coma	xorai	0xff

;*************ram****************
cblock    0x0
r0
r1
r2
r3
r4
r5
r6
r7
r8
acc_temp
status_temp
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

work_time
funtion_valu

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
test_io
endc

;***********************
#define R0 r0

#define    RAM_COUNT    127    ;48个RAM
#define    FIR_RAM_ADDR    0x01    ;RAM首地址
;#undefine  INDF
;#define    INDF		INDF3
#define    MAX_DIS_DATA    18

#define    RAM_E    0x00        ;RAM错误报E0

#define	   TF_FCPU	(0<<3)	;使用CPU频率
#define	   TF_FHOSC	(1<<3)	;使用内部16M
#define	   TF_FLOSC	(2<<3)	;使用内部28K
#define	   TF_INT	(3<<3)	;使用INT0输入
#define	   TR_1		0	;1分频
#define	   TR_2		1	;2分频
#define	   TR_4		2	;4分频
#define	   TR_8		3	;8分频
#define	   TR_16	4	;16分频
#define	   TR_32	5	;32分频
#define	   TR_64	6	;64分频
#define	   TR_128	7	;128分频


#define    IO_TM1628_STB    P0,4
#define    IO_TM1628_CLK    P1,4
#define    IO_TM1628_DATA    P1,5
#define    IO_TM1628_DDR    DDR1,5
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
#define flag_test_timer0    work_flag1,7

#define    flag_disp0_point    work_flag2,0
#define    flag_disp1_point    work_flag2,1
#define    flag_disp2_point    work_flag2,2
#define    flag_disp3_point    work_flag2,3
    org     0x3ff        ;复位向量
    bclr    GIE    ;关中断
    org     0x00


    goto    start

    ORG 0x08        ;其它中断向量
interrupt_sub:
    movra    acc_temp
    swapar    STATUS
    movra    status_temp
;*******判断是哪个中断*****
start_just_int:
    jbclr    T0IE
    jbset    T0IF
    goto    just_t1
;**********timer0**********
    bclr    T0IF

    bclr	DDR1,2
;    incr    time_125us
;    jbset    time_125us,3
;    goto	end_time0_interrupt
;
;    clrr    time_125us
;    incr    time_1ms
;    movar    time_1ms
;    asubai    10        ;time_1ms-10
;    jbset    C
;    goto    end_time0_interrupt
;    clrr    time_1ms
;    bset    flag_time_10ms
end_time0_interrupt:
;**********end_time0*******
just_t1:
    jbclr    T1IE
    jbset    T1IF
    goto    just_int0
;*********timer1***********
    bclr    T1IF
    incr    time_125us
    jbset    time_125us,3
    goto	end_time1_interrupt

    clrr    time_125us
    incr    time_1ms
    movar    time_1ms
    asubai    10        ;time_1ms-10
    jbset    C
    goto    end_time1_interrupt
    clrr    time_1ms
    bset    flag_time_10ms
   ; movra   P0
   ; movra   P1    
;    incr    test_io
;    movar   test_io
;    movra   P3
    ;comr   P3
end_time1_interrupt:
;**********end_timer1******
just_int0:
    jbclr    INT0IE
    jbset    INT0IF
    goto     just_int1
;**********int0************
    bclr    INT0IF
    comr	P1
;*********end int0*********
just_int1:
    jbclr    INT1IE
    jbset    INT1IF
    goto     just_adc
;**********int0************
    bclr    INT1IF
    comr	P0
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
    comr	P0
    incr    dis_buffer1
;**********end_kbim********
end_just_int:
exit_interrupt:
    swapar    status_temp
    movra    STATUS
    swapr    acc_temp
    swapar    acc_temp
    retie


;*******程序开始*************
;*********start***************

start:
    ;movai    0x01;high test_table
    movai	    high test_table
    movra    FSR1
    ;movai    0x07;low  test_table
    movai	      low	test_table
    movra    FSR0
    movar    INDF3
    movar    INDF2
    movar    INDF1
    incr     FSR0
    movar    INDF3
    nop
    nop
;    bclr    WDTEN        ;关看门狗
  ;  bclr    EIS

    movai    low FIR_RAM_ADDR
    movra    FSR        ;首地址
    movai    high FIR_RAM_ADDR
    movra    FSR1
    movai    RAM_COUNT
    movra    R0
clr_ram_loop:
    movai    0xff
    movra    INDF        ;把FF写到FSR所指的地址
    nop
    movar    INDF        ;写完后读出来
    isubai    0xff
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
    goto    $-1
;*****************************************
sys_init:
    movai    (TF_FLOSC+TR_8)    ;内部16M,16分频
    ;movai    (TF_FCPU+TR_8)
    movra    T0CR        ;写入T0CR 
    movai    249	;125us中断
    movra    T0LOAD
    movra    T0CNT
    bset     TC0EN		;启动T0
    bset     T0IE    ;开定时中断

    movai    (TF_FLOSC+TR_128)    ;内部16M,16分频
    movra    T1CR        ;写入T0CR 
    movai    249	;500us中断
    movra    T1LOAD
    movra    T1CNT
    bset     TC1EN		;启动T1
    bset     T1IE    ;开定时中断

    movai 	0xfe
    movra 	T1DATA

    movai	0xfc
    movra	DDR0
    movra	DDR1
    clrr    	P1
    ;bset	BUZ0OUT
    ;bset	PWM0OUT
    movai	150
    movra	T0DATA
    ;bset	BUZ1OUT
    ;bset	PWM1OUT
    movai	210
    movra	T1DATA

   ; bset	INT0IE
   ; bset	INT1IE
    bclr	MCR,3
    bclr	MCR,1
    bclr	MCR,2
    bclr	MCR,0
    bset	DDR0,0

;***************************************
    clrwdt
 ;   bset    WDTEN
    bset    GIE    ;开全局中断
;************main loop***************
main_loop:
    clrwdt
    bclr	GIE

    call	scan_adc
    goto	main_loop
    ;goto $-2
    ;sleep
    ;bclr    T1MEN
    ;bset    BUZOUT
    ;bset    PWMOUT

    call    time_deal
   ; goto    main_loop
    ;call    tm_1628_read_sub    ;读按键
    ;call    key_scan        ;整理读回来的按键数据
    ;call    key_deal        ;按键处理
    ;call    work_deal
    call    display
    call    tm_1628_write_sub    ;写显示数据
    goto    main_loop
;-------adc------------
scan_adc:
    clrr	R0
adc_loop:
    movai	high	adc_hcs_table
    movra	FSR1
    movai	low	adc_hcs_table
    addar	R0
    movra	FSR0
    movra	INDF3

    addai	ADCKS_16	;16分频
    movra	ADCR0
    
    bset	ADEN		;启动AD转换
    bset	ADEOC
    goto	$-1

    nop
end_scan_adc:
	return

;************time sub*************
time_deal:
    jbset    flag_time_10ms
    goto    end_time_deal

    bclr    flag_time_10ms
    incr    time_10ms
    movar    time_10ms
    isubai    10
    jbset    Z
    goto    end_time_deal

    clrr    time_10ms
    incr    time_100ms
    movar    time_100ms
    isubai    10
    jbset    Z
    goto    end_time_deal

    clrr    time_100ms

    movr    work_time
    jbset    Z
    decr    work_time

    incr    time_1s
    ;incr    T1DATA
    movar    time_1s
    asubai    10
    jbset    C
    goto    end_time_deal

    clrr    time_1s
    movra   time_1s
    incr	r8
    bclr	r8,3
    movar	T0CR
    andai	0xf8
    addar	r8
    movra	T0CR
 
 ;   bclr    GIE
 ;   incr    T1CR
 ;   bclr    T1CR,5
 ;   bset    GIE
    ;movai   0x04
  ;  xorra    OSCM 
    ;bset    HFEN    
    incr    time_1min
    movar    time_1min
    isubai    60
    jbset    Z
    goto    end_time_deal
    clrr    time_1min
    incr    time_1hour
end_time_deal:
    return
;************key_scan*************
key_scan:
;**********k1***********
scan_key1
    jbset    key_data1
    goto    clr_key1_bak
    
    jbclr    flag_key1_effect_bak
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
    
    jbclr    flag_key2_effect_bak
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
    
    jbclr    flag_key3_effect_bak
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
    jbset    flag_key1_effect
    goto    deal_k2
    bclr    flag_key1_effect

    incr    dis_buffer0
    movai    5
    movra    work_time
    incr    funtion_valu

deal_k2:
    jbset    flag_key2_effect
    goto    deal_k3
    bclr    flag_key2_effect

    incr    dis_buffer1
deal_k3:
    jbset    flag_key3_effect
    goto    end_key_deal
    bclr    flag_key3_effect

    ;incr    dis_buffer2
    jbset    flag_test_timer0
    goto    end_key_deal

    bclr    GIE    ;disable interrupt
    incr    T0CR
    bclr    T0CR,4
    bset    GIE

end_key_deal:
    return

;********************************
display:
;*************查表开始***************
    clrr    r3
    movar    time_1s
    movra    r4

    movai    10        ;十进制
    movra    r5

    call    div_x

    movar    r2        ;余数
    movra    dis_buffer3
    movar    r0        ;商低位
    movra    dis_buffer2

    clrr	r3
    movar    time_1min
    movra    r4
    movai    10
    movra    r5
    call    div_x

    movar    r2
    movra    dis_buffer1
    movar    r0
    movra    dis_buffer0

    movai    0xff
    movra    dis_buffer4

;    movai	0
;    movra	dis_buffer0
;    inca	
;    movra	dis_buffer1
;    inca	
;    movra	dis_buffer2
;    inca
;    movra	dis_buffer3
    clrr    R0                ;计数4次
load_table_loop:
    movai    dis_buffer0
    addar    R0
    movra    FSR                ;每次循环更新一次FSR指针
    
   ; movai   HIGH table_dis_code
   ; movra    PCLATH

    movai    MAX_DIS_DATA        
    asubar   INDF
    jbset    C
    clrr    INDF

    movar    INDF
    addai    0x02                
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
    bset    IO_TM1628_DDR
    movar    r1
    movra    INDF
     
    incr    FSR
    djzr    r2
    goto    tm1628_read_key_loop
end_tm_1628_read:
    return
;***********1628 写显示数据***************
tm_1628_write_sub:
;    movai    0x55
;    movra	tm_data0
;    movra	tm_data1
;    movra	tm_data2
;    movra	tm_data3
;    movra	tm_data4
;    movra	tm_data5
;    movra	tm_data6

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
    incr    r2
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
    ;bclr    IO_TM1628_DDR    
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
;***************************************
;**********math**************
;*************商r1r0,余r2,计数r6,被除数r3,r4
;*************除数r5
div_x:
    clrr    R0    ;商
    clrr    r1
    clrr    r2    ;余数

    movr    r5
    jbclr    Z
    return        ;除数为0，退出

    movai    16
    movra    r6    ;计数=被除数位

    movr    r3
    jbset    Z
    goto    div_loop
    movr    r4
    jbclr    Z
    return        ;被数为0直接返回
div_loop:
    bclr    C
    rlr    r4    ;低8位
    rlr    r3    ;高8位
    rlr    r2    ;余
    rlr    r7

    movar    r5
    rsubar    r2
    jbclr    C
    goto     updata_div
    jbclr    r7,0
    goto    $+3
    bclr    C
    goto    r0_shift
updata_div:        ;更新余数
    movra    r2
    bset    C
r0_shift:
    rlr    R0
    rlr    r1
    djzr    r6
    goto    div_loop
    return    
;****************************************
;********io test************************
io_out_h:
    clrwdt
    clrr    DDR1
    movai    0xff
    movra    P1
    goto    io_out_h
    return
;*****************************************
io_out_l:
    clrwdt
    clrr    DDR1
    movai    0x00
    movra    P1
    goto    io_out_l
    return
;*****************************************
io_in_p1_l:
    movai    0xff
    movra    DDR1

    ;swapar    P1
    movar    P1
    clrwdt
    goto    $-3
    return
;*****************************************
io_in_p1_h:
    movai    0xff
    movra    DDR1
   ; clrr    PHCON    ;上拉输入
    ;clrr    PDCON    ;下拉

    swapar    P1
  ;  movra    P0    ;P14--P17的状态从P0输出
    clrwdt
    goto    $-3
    return
;******************************************
io_in_p0:
    clrr    DDR1    ;P1口输出
    movai    0xff
;    movra    DDR0
;    clrr    PDCON    ;下拉    ;P0没有上拉

    movra    P1    ;P00-P03的状态从P1口输出
    clrwdt
    goto    $-3
    return
;*********************************
;********键盘中断和sleep共用一个子程序*****
;****************************************
key_bim:
    bclr    GIE
;    bclr    WDTEN
;    clrr     DDR0
    movai    0xff
    movra    DDR1    ;P1作为输入
;    movra    KBIM    ;开启键盘中断位
;    clrr    PHCON    ;使能上拉

;    bset    KBIE    ;使能键盘中断
;    sleep
;    bclr    KBIE
 ;   clrr    DDR1
    return

;**************************************
;*****int0 cfg************************
;*****下降沿触发**********************
int0_cfg_l:
    bset    DDR1,0    ;P10输入
;    bclr    PHCON,0    ;开P10上拉
;    bset    EIS
    bset    INT0IE    ;开中断
;    bclr    WDTEN
;    sleep
    return;
;**************************************
int0_cfg_h:
    bset    DDR1,0    ;P10输入
;    bclr    PHCON,0    ;开P10上拉
;    bset    EIS
    bset    INT0IE    ;开中断
;    bclr    WDTEN
;    sleep
    return;

;*************************************
;***********output high od*************
;********开漏输出高*******************
io_out_h_od:
    clrr    DDR1
    movai    0xff
    movra    P1
    clrwdt
    goto    io_out_h_od
    return
;*************************************
;***********output low od*************
;********开漏输出低*******************
io_out_l_od:
    clrr    DDR1
    movai    0x00
    movra    P1
    movai    0xff
    clrwdt
    goto    io_out_l_od
    return

;*************************************
;***********上拉输入******************
io_in_pn_pull:
    movai    0xff
    movra    DDR1
    ;clrr    PHCON    ;上拉输入
    clrwdt
    goto    io_in_pn_pull
    return
;*************************************
;***********下拉输入******************
io_in_pn_down:
    movai    0xff
    movra    DDR1
    clrwdt
    goto    io_in_pn_down
    return
    org 0x300
test_table:
    dw  0x8877
    dw  0x9802
    dw  0x0980
adc_hcs_table:
 	dw	ADCHS_0
 	dw	ADCHS_1
 	dw	ADCHS_2
 	dw	ADCHS_3
 	dw	ADCHS_4
 	dw	ADCHS_5
 	dw	ADCHS_6
;**********************************
;********timer*********************
timer0_test:
    bset    flag_test_timer0
    clrr    DDR1
    return

;***********work sub**************
work_deal:
    movr    work_time
    jbset    Z
    goto    end_work_deal

    movar    funtion_valu
    addra    PCL
    goto    end_work_deal
    goto    io_out_h    ;1
    goto    io_out_l    ;2
    goto    io_in_p1_l    ;3
    goto    io_in_p1_h    ;4
    goto    io_in_p0    ;5
    goto    key_bim        ;6
    goto    int0_cfg_l    ;7
    goto    int0_cfg_h    ;8

    goto    io_out_h_od    ;9
    goto    io_out_l_od    ;a

    goto    io_in_pn_pull    ;b
    goto    io_in_pn_down    ;c

    goto    timer0_test    ;d
end_work_deal:
    return    
;*******表格**********************
table:
    org (0x3fe-MAX_DIS_DATA)
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
    rsubra    PCL
    end
