
#define AVERAGE_CNT 128
#define C_K_LB 20
#define T_T0DATA 50
#define T_T0CON 0x82
#define T_ADCON 0x54
#define BUZ_TIME_H 0x5f
#define BUZ_TIME_L 0xff


const uchar table_0_9[]={
	//          P07 P26 P25 Pxx P03 P02 P01 P00	
	//          4		3		1		2		9		8		7		6
	//          C		E		D		DP	G		F		A		B
0xe7,       //1   1   1   0   0   1   1   1  //	;0      
0x81,       //1   0   0   0   0   0   0   1  //	;1      
0x6b,       //0   1   1   0   1   0   1   1  //	;2      
0xab,       //1   0   1   0   1   0   1   1  //	;3      
0x8d,       //1   0   0   0   1   1   0   1  //	;4      
0xae,       //1   0   1   0   1   1   1   0  //	;5      
0xee,       //1   1   1   0   1   1   1   0  //	;6      
0x83,       //1   0   0   0   0   0   1   1  //	;7      
0xef,       //1   1   1   0   1   1   1   1  //	;8      
0xaf,       //1   0   1   0   1   1   1   1  //	;9      
0x00,       //0   0   0   0   0   0   0   0  //	;A;左口 
0x00,       //0   0   0   0   0   0   0   0  //	;B;右口 
0x00,       //0   0   0   0   0   0   0   0  //	;C;不显 
0x00,       //0   0   0   0   0   0   0   0  //	;D      
0x00        //0   0   0   0   0   0   0   0  //	;E  
};

const uchar temperature_table[]={
	
	0,0,0,0,0,0,0,0,0,0,	                      //;0-9
	0,0,0,0,0,0,0,0,0,0,		                    //;10-19
	0,0,0,0,0,0,120,119,118,116,		            //;20-29
	114,113,111,110,109,107,106,105,103,102,		//;30-39
	101,100,99,98,97,96,95,94,93,92,            //;40-49   
	91,90,89,88,87,86,85,84,83,83,               //;50-59
	82,82,81,80,80,79,78,78,77,76,               //;60-69
	76,75,74,74,73,72,72,71,70,70,               //;70-79
	69,69,68,68,67,67,66,66,65,64,               //;80-89
	64,63,63,62,62,61,61,60,59,59,	             //;90-99
	58,58,57,57,56,56,55,55,54,54,		           //;100-109
	53,53,53,52,52,51,51,50,50,49,		           //;110-119
	49,48,48,47,47,47,46,46,45,45,		           //;120-129
	44,44,43,43,42,42,42,41,41,40,		           //;130-139
	40,39,39,38,38,38,37,37,36,36,		           //;140-149
	35,35,34,34,34,33,33,32,32,31,		           //;150-159
	31,30,30,30,29,29,28,28,27,27,		           //;160-169
	26,26,25,25,25,24,24,23,23,22,               //;170-179
	22,21,21,20,20,19,19,18,18,17,               //;180-189
	17,16,16,15,15,14,14,13,13,12,               //;190-199
	12,11,10,10,9,9,8,8,7,6,                     //;200-201
	6,5,5,4,3,3,2,1,1,0,                         //;210-219
};

BYTE Temp_P0;
BYTE Temp_P2;

#define LED_1	 led_com_data.bitn.bit5  //电源灯
#define LED_2	 led_com_data.bitn.bit6  //冲浪灯
#define LED_3	 led_com_data.bitn.bit7  //加热灯
#define LED_4	 led_com_data.bitn.bit2  //CW灯   
#define LED_5	 led_com_data.bitn.bit1  //振动灯  
#define LED_6	 led_com_data.bitn.bit0  //超长波灯

#define key_1	 R_KEY1_TRU.bitn.bit3  //电源按键
#define key_2	 R_KEY1_TRU.bitn.bit2  //冲浪按键
#define key_3	 R_KEY1_TRU.bitn.bit1  //加热按键
#define key_4	 R_KEY1_TRU.bitn.bit0  //CW按键
#define key_5	 R_KEY1_TRU.bitn.bit6  //振动按键
#define key_6	 R_KEY1_TRU.bitn.bit7  //超长波按键


#define LED_com   Temp_P2.bitn.bit4
#define COM2		  Temp_P0.bitn.bit6  
#define COM1		  Temp_P0.bitn.bit4  

uchar R_KEY1;
uchar R_KEY1_BAK;
BYTE  R_KEY1_TRU;
uchar R_K_LB;

BYTE FLAG;
#define FLAG_BUZ_Forever	FLAG.bitn.bit0
#define FLAG_Button				FLAG.bitn.bit1
#define FLAG_BUZ_BX				FLAG.bitn.bit2
#define FLAG_BUZ_EN				FLAG.bitn.bit3 
#define FLAG_ON_OFF       FLAG.bitn.bit4

uchar Timer_press_125us;
uchar Timer_press_25ms;

uchar Timer_refresh_125us; 
uchar Timer_refresh_25ms;  

uchar Timer_125us;
uchar Timer_250us;
uchar Timer_50ms;
uchar Timer_10s;
uchar Timer_1min;
uchar Timer_1hour;

WORD Buffer_Temper;
#define Buffer_Temper_H Buffer_Temper.byte.hi
#define Buffer_Temper_L Buffer_Temper.byte.lo

uchar AD_Cnt;
uchar Temperature;

uchar Temp;

uchar Buzzer_H_Timer; //蜂鸣计时器
uchar Buzzer_L_Timer; //蜂鸣计时器
uchar Buzzer_Counter; //鸣响次数

uchar com_cnter;
uchar com1_data; //数码管com1
uchar com2_data; //数码管com2


BYTE led_com_data; //LEDcom
uchar Led_L;
uchar Led_H;

uchar Quotient;

uchar Timer_refresh_125us; //闪烁计时器
uchar Timer_refresh_25ms;  //闪烁计时器

void  InitRst(void);
void  InitRam(void);
void  check_button(void);
void  mn_cycle(void);
void  ad_check(void);
void  buzzer_deal(void);
void  error(uchar);
void  hextobcd(uchar);
void  startbibi(uchar,uchar);
void  K_LB(void);
void  display(void);