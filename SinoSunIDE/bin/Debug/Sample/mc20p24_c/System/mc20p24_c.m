
C source file D:\WorkDir\IDE\SinoSunIDE\SinoSunIDE\bin\Debug\WinC_V\H6805\reg_mc20p24.h:

unsigned char T0CNT at 0x0000
unsigned char T0DATA at 0x0001
union t0con at 0x0002
union mcr at 0x0003
union btcon at 0x000c
unsigned char BTCNT at 0x000d
union p0 at 0x0010
union p1 at 0x0011
union p2 at 0x0012
union p0conh at 0x0016
union p0conl at 0x0017
union p0pnd at 0x0018
union p1con at 0x0019
union p2conh at 0x001a
union p2conl at 0x001b
unsigned char PWMDATA at 0x0022
union pwmcon at 0x0023
union adcon at 0x0027
unsigned char ADDATAH at 0x0028
unsigned char ADDATAL at 0x0029

C source file var.h:

unsigned char table_0_9[15] at 0x1312
unsigned char temperature_table[220] at 0x1321
union bit_char Temp_P0 at 0x0053
union bit_char Temp_P2 at 0x0052
unsigned char R_KEY1 at 0x0051
unsigned char R_KEY1_BAK at 0x0050
union bit_char R_KEY1_TRU at 0x004f
unsigned char R_K_LB at 0x004e
union bit_char FLAG at 0x004d
unsigned char Timer_press_125us at 0x004c
unsigned char Timer_press_25ms at 0x004b
unsigned char Timer_refresh_125us at 0x004a
unsigned char Timer_refresh_25ms at 0x0049
unsigned char Timer_125us at 0x0048
unsigned char Timer_250us at 0x0047
unsigned char Timer_50ms at 0x0046
unsigned char Timer_10s at 0x0045
unsigned char Timer_1min at 0x0044
unsigned char Timer_1hour at 0x0043
union bit_int Buffer_Temper at 0x0041
unsigned char AD_Cnt at 0x0040
unsigned char Temperature at 0x003f
unsigned char Temp at 0x003e
unsigned char Buzzer_H_Timer at 0x003d
unsigned char Buzzer_L_Timer at 0x003c
unsigned char Buzzer_Counter at 0x003b
unsigned char com_cnter at 0x003a
unsigned char com1_data at 0x0039
unsigned char com2_data at 0x0038
union bit_char led_com_data at 0x0037
unsigned char Led_L at 0x0036
unsigned char Led_H at 0x0035
unsigned char Quotient at 0x0034

C source file int_mc20p24.h:

(no globals)

void SWI_ISR() lines 3 to 5 at 0x1003-0x1003
    (no locals)

void INT1_ISR() lines 7 to 10 at 0x1004-0x1004
    (no locals)

void PWMINT_ISR() lines 12 to 15 at 0x1005-0x1005
    (no locals)

void T0INT_ISR() lines 16 to 57 at 0x1006-0x104b
    (no locals)

void INT0_ISR() lines 59 to 62 at 0x104c-0x104c
    (no locals)

C source file funtion.c:

(no globals)

void InitRst() lines 9 to 29 at 0x104d-0x1077
    (no locals)

C source file mc20p24_c.c:

(no globals)

void main() lines 14 to 35 at 0x1078-0x109d
    (no locals)

void InitRam() lines 59 to 69 at 0x109e-0x10a5
    (no locals)

void error() lines 71 to 76 at 0x10a6-0x10b1
    static argument unsigned char i at 0x0056

void startbibi() lines 78 to 87 at 0x10b2-0x10cc
    static argument unsigned char i at 0x0057
    static argument unsigned char j at 0x0058

void K_LB() lines 89 to 104 at 0x10cd-0x10e6
    (no locals)

void buzzer_deal() lines 106 to 137 at 0x10e7-0x110e
    (no locals)

void ad_check() lines 139 to 187 at 0x110f-0x1180
    static auto union bit_int ad_result at 0x0054

void check_button() lines 189 to 254 at 0x1181-0x11ed
    (no locals)

void hextobcd() lines 258 to 269 at 0x11ee-0x1208
    static argument unsigned char data at 0x0056

void mn_cycle() lines 273 to 302 at 0x1209-0x1243
    (no locals)

void display() lines 304 to 383 at 0x1244-0x1311
    static auto unsigned char i[3] at 0x0059
