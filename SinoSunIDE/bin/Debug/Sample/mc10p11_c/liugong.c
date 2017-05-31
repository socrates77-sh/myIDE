
/************
 *
 *
 * ************/

#define _36KHZ	1		//0--38K 1--36K
#include<common.h>
#include"reg_mc10p11.h"

union bit_char	work_flag;
#define	flag_send_code	work_flag.bitn.bit0
#define	flag_parity_bit	work_flag.bitn.bit1
#define	flag_rc_6	work_flag.bitn.bit2

union bit_int	key_buffer;
unsigned char key_value;	//扫描按键前为0，检测到按键后是按键的位号，按键处理后是要发的数据
unsigned char key_temp;
unsigned char key_bak;		//记录上一次按键，判断TR位翻转
unsigned char key_data;		//key的数据码

const unsigned char key_table[]={
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,	//前面补13个按键
0xff, 0xff, 0x0C,							//K15(K1)和K56（42）为3010
0xff, 0x83, 0x0C,
0xff, 0x0D, 0xCC, 0xF2,
0xff, 0x54, 0x10, 0x58, 0x20, 
0xff, 0x5A, 0x5C, 0x5B, 0x11, 0x59, 
0xff, 0x21, 0xA9, 0x6D, 0x6E, 0x6F, 0x70, 
0xff, 0x4B, 0xD8, 0xF3, 0x84, 0x0F, 0x29, 0x30, 
0xff, 0x28, 0x37, 0x2C, 0x31, 0x01, 0x02, 0x03, 0x04, 
0xff, 0x05, 0x06, 0x07, 0x08, 0x09, 0x38, 0x00, 0xFB,0xff,
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 	//后面没用的填充ff
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
};

#define IO_LED	S0
#define	LED_OFF		S0=1
#define	LED_ON		S0=0
#define	MODE_CODE	0x06<<4		//模式只有4位
#define	SYSTEM_CODE_H	0x80
#define	SYSTEM_CODE_L	0x56
#define	CONTROL_CODE	0xa7

#define	SEND_TIME_HEAD_H	95
#define	SEND_TIME_HEAD_L	32
#define	SEND_TIME_BIT_L		15
#define	SEND_TIME_BIT_H		15
/****************3010*************/
#define	USER_CODE_3010		0x00
#define	SYSTEM_CODE_3010	(USER_CODE_3010|0x80)	//最高位为1
#define	SEND_TIME		31
/*********************************
 * ******36/38KHZ********************
 * ******************************/
//i=1有载波，i=0无载波
#if	_36KHZ
#define	OUT_38K(i)	{\
    OUTC=~i;\
    OUTC=~i;\
    NOP(); \
    NOP(); \
    NOP(); \
    OUTC=1;\
    OUTC=1;\
    OUTC=1;\
    OUTC=1;\
    OUTC=1;\
    NOP(); \
}
#elif
#define	OUT_38K(i)	{\
    OUTC=~i;\
    OUTC=~i;\
    NOP(); \
    NOP(); \
    NOP(); \
    OUTC=1;\
    OUTC=1;\
    OUTC=1;\
    OUTC=1;\
    NOP(); \
    NOP(); \
    NOP(); \
}
#endif
/*******************************
 * ****delay_100us**************
 * ****************************/
void delay_100us(unsigned char time_100us)
{
    do{
#asm
	ldx	#31
delay_100us_loop:
	    decx				;3
	    bne	delay_100us_loop	;3
#endasm
    }while(time_100us--);
}
/************************/
void delay_1ms(unsigned char time_1ms)
{
    while(time_1ms)
    {
	delay_100us(10);
	time_1ms--;
    }
}
/*******************************
 * ****sys init ****************
 * ****************************/
void sys_init()
{
    OUTC=1;
    delay_100us(255);
    flag_send_code=0;	//不发码
    S0_AS_IO;
    S0_OUTPUT;
    LED_OFF;
    S11_AS_KEY;
}

/****************************
 * 读取按键，j是多***********
 * 少就读取多少按键**********
 * **************************/
void read_key(unsigned char j)		
{
    while(j)
    {
	if(!(key_buffer.byte.lo&0x01))	   //为0有按键
	{
	    key_value=key_temp;
	}
	key_buffer.word>>=1;
	key_temp++;
	j--;
    }	
}

void scan_key()
{
    unsigned char i;
    key_temp=1;		                  //用来赋值，从1开始
    key_value=0;	                	//若没赋值为0
    /****scan_gnd*****/
    KBIF=0;			                   //重置
    key_buffer.byte.lo=KEYL;
    key_buffer.byte.hi=KEYH;
    read_key(13);		               //S0--S12
    //	if(key_value)return;			 //GND有键就直接退出
    /********scan S0--S12***********/
    for(i=0;i<=12;i++)	           //key_temp从13开始，1--12是GND
    {
	KEY=0xff;		                     //对KEY写任意值，开始扫描
	NOP();
	NOP();
	key_buffer.byte.lo=KEYL;
	key_buffer.byte.hi=KEYH;
	KEY=0xff;		                     //再次写KEY时停止，下一次写时扫描下一组
	//j=i;
	read_key(i);
    }
}

/***********************************
 * ******key deal******************
 * *******************************/
void key_deal()
{
    unsigned char i;
    flag_send_code=0;	           //不发码
    if(key_value)
    {
	if((key_value==16)||(key_value==57))flag_rc_6=0;
	else flag_rc_6=1;
	if(key_bak!=key_value)	flag_parity_bit=!flag_parity_bit;	//按键不同时奇偶位翻转
	key_data=key_table[key_value-1];
	if(key_data!=0xff)
	{
	    flag_send_code=1;
	    if(!flag_rc_6)key_data<<=2;		//3010码只发低6位
	}
    }
    else			                 //no key
    {
	OUTC=1;
	KBIF=0;
	KBIE=1;			                 //开键盘中断
	LED_OFF;
	STOP();
	KBIE=0;		                 	//关闭键盘中断
	sys_init();
    }
}

/**********头码的发送********/
void send_head()
{
    unsigned char j;
    j=SEND_TIME_HEAD_H;      	 //发送头码载波时间
    do{
	OUT_38K(1);
    }while(j--);

    j=SEND_TIME_HEAD_L;	      //发送头码无载波时间
    do{
	OUT_38K(0);
    }while(j--);
}
/***********************************
 * ******send a byte****************
 * **第一位分三段发送***************
 * ********************************/
void send_byte(unsigned char send_data,unsigned char count)
{
    unsigned char j;
    while(count)
    {
	if(send_data&0x80)	       //先发高位
	{
	    j=SEND_TIME_BIT_H;
	    do{
		OUT_38K(1);
	    }while(j--);

	    j=SEND_TIME_BIT_L;
	    do{
		OUT_38K(0);
	    }while(j--);
	}
	else
	{
	    j=SEND_TIME_BIT_L;
	    do{
		OUT_38K(0);
	    }while(j--);
	    j=SEND_TIME_BIT_H;
	    do{
		OUT_38K(1);
	    }while(j--);
	}
	count--;
	send_data<<=1;
    }
}

/**********发送TR位**********/
void send_tr_bit()
{
    unsigned char j;
    j=SEND_TIME_BIT_H<<1;	       //TR位的时间是正常位的两倍
    if(flag_parity_bit)		       //奇偶位
    {
	do{
	    OUT_38K(1);
	}while(j--);
	j=SEND_TIME_BIT_L<<1;
	do{
	    OUT_38K(0);
	}while(j--);
    }
    else{
	do{
	    OUT_38K(0);
	}while(j--);
	j=SEND_TIME_BIT_L<<1;
	do{
	    OUT_38K(1);
	}while(j--);
    }
}
/************************************/
void send_byte_3010(unsigned char send_data,unsigned char count)
{
    register unsigned char j;
    while(count)
    {
	if(send_data&0x80)
	{
	    j=SEND_TIME;
	    do{
		OUT_38K(0);
	    }while(j--);

	    j=SEND_TIME;
	    do{
		OUT_38K(1);
	    }while(j--);
	}
	else
	{
	    j=SEND_TIME;
	    do{
		OUT_38K(1);
	    }while(j--);
	    j=SEND_TIME;
	    do{
		OUT_38K(0);
	    }while(j--);
	}
	count--;
	send_data<<=1;
    }
}
/************************************
 * ********send code****************
 * *********************************/
void send_code()
{
    unsigned char send_data_temp;
    if(flag_send_code)
    {
	IO_LED=!IO_LED;		//LED口取反
	if(flag_rc_6)
	{
	    send_head();		                //头码
	    send_byte(MODE_CODE,4);		      //模式码只有4位
	    /************TR bit************/
	    send_tr_bit();
	    /***********system code*********/
	    send_byte(SYSTEM_CODE_H,8);   	//系统码高8位
	    send_byte(SYSTEM_CODE_L,8);   	//系统码低8位
	    /***********control code*********/
	    send_byte(CONTROL_CODE,8);	    //控制码有8位
	    /***********data code*********/
	    send_byte(key_data,6);		      //数据码有8位
	    delay_100us(200);	              //20ms	加上程序的延时等于间隔时间
	    delay_100us(200);	              //20ms
	    delay_100us(200);	              //20ms
	}
	else	//3010
	{
	    send_data_temp=SYSTEM_CODE_3010;
	    if(key_data<=(0x3f<<2)) send_data_temp|=0x40;		//<=0x3f时，第二位为1	bit1
	    if(flag_parity_bit) send_data_temp|=0x20;			//奇偶键，改变第三位	bit2
	    /***********system code*********/
	    send_byte_3010(send_data_temp,8);

	    /***********data code*********/
	    send_byte_3010(key_data,6);
	    //delay_100us(100);	//5.9ms	加上程序的延时等于间隔时间
	    delay_1ms(71);
	}
    }
}

/***********************************
 * *********main*******************
 * ********************************/
void main(){
    {
#asm
	sei
clr_ram:
	    ldx	#$e0
clr_loop:
	    clr	,x
	    incx
	    bne	clr_loop
	    rsp		            ;若程序比较复杂，不能复位堆栈
#endasm
	    sys_init();
    }

    while(1)
    {
	scan_key();
	key_deal();
	send_code();
    }
}

@nosvf @interrupt void SWI_ISR(void)
{
    return;
}

@nosvf @interrupt void KBI_ISR(void)
{
    KBIF=0;		//清中断标志
}
