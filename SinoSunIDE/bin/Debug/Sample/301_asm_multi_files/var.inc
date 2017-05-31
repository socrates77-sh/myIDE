
;------------------------------------------------
;定义一个头文件用来存放程序中的变量和位定义，所有变量均为全局变量
;
;-------------------------------------------------

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
flag_work0
flag_work1
flag_work2
counter
endc
;***************end ram*************
#define         timer_10ms      flag_work0,0
#define         timer_100ms     flag_work0,1


#define         led0            flag_work1,0
#define         led1            flag_work1,1

