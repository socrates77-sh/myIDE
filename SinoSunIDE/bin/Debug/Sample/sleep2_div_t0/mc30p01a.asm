#include mc30p011a.inc
#define	 asubra	subra
#define	 comr	comra
#define	 isubai	subai

#define    RAM_COUNT    48    ;48个RAM
#define    FIR_RAM_ADDR    0x10    ;RAM首地址

#define    MAX_DIS_DATA    18

#define    RAM_E    0x00        ;RAM错误报E0
;***************特殊RAM***********
R0    equ    0x07
DDR0    equ    0x45
DDR1    equ    0x46

#define    r0    R0
#define    IO_TM1628_STB    P0,0
#define    IO_TM1628_CLK    P0,1
#define    IO_TM1628_DATA    P0,2
#define    IO_TM1628_DDR    DDR0,2
;*************ram****************
cblock    0x10
acc_temp
status_temp
r1
r2
r3
r4
r5
r6
r7
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

key_times0
key_times1
key_times2
key_times3
key_bak
key_value
work_status

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
#define		KEY_INT1	key_value,4
#define		KEY_INT0	key_value,5
#define		KEY_RST		key_value,7

#define		IN_PUT		work_status,0
#define		IN_PULL_DOWN	work_status,1
#define		OUT_PUT		work_status,2

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

;*******程序开始*************
;*********start***************
start:
    bclr    WDTEN        ;关看门狗
    bclr    EIS

;*****************************************
sys_init:
    movai    0x00    ;8M,2T,4分频
    movra    T0CR        ;写入T0CR 
    bset    T0IE    ;开定时中断

    movai 254
    movra T1LOAD

    movai 0x01
    movra T1CR

    movai 0xfe
    movra T1DATA

    bset  T1MEN         ;开定时器1
    bset  T1IE        ;使能定时器1中断
    clrr    DDR0
    clrr    DDR1    ;全部作为输入口
    clrr    P0
    clrr    P1
;***************************************
    clrwdt
    bset    WDTEN
    bclr    GIE    ;关全局中断
    bset    TBS
    nop

;************main loop***************
main_loop:
    clrwdt
    ;bclr    T0IE
    nop
;********动态************************
;io_out_h:
;    clrr    DDR1
;    clrr    DDR0
;test_loop: 
;    comr    P0
;    comr    P1
;    comr    R0
;    clrwdt
;    goto    test_loop
;*****************************************

;;********静态*****************************
io_in_p1_l:
    clrr    DDR1
    clrr    DDR0
    clrwdt

test_loop2:
    comr    P0
    comr    P1
    sleep		;等待唤醒
    goto    test_loop2

    end
