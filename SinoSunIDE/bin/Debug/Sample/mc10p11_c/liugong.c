
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
unsigned char key_value;	//ɨ�谴��ǰΪ0����⵽�������ǰ�����λ�ţ������������Ҫ��������
unsigned char key_temp;
unsigned char key_bak;		//��¼��һ�ΰ������ж�TRλ��ת
unsigned char key_data;		//key��������

const unsigned char key_table[]={
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,	//ǰ�油13������
0xff, 0xff, 0x0C,							//K15(K1)��K56��42��Ϊ3010
0xff, 0x83, 0x0C,
0xff, 0x0D, 0xCC, 0xF2,
0xff, 0x54, 0x10, 0x58, 0x20, 
0xff, 0x5A, 0x5C, 0x5B, 0x11, 0x59, 
0xff, 0x21, 0xA9, 0x6D, 0x6E, 0x6F, 0x70, 
0xff, 0x4B, 0xD8, 0xF3, 0x84, 0x0F, 0x29, 0x30, 
0xff, 0x28, 0x37, 0x2C, 0x31, 0x01, 0x02, 0x03, 0x04, 
0xff, 0x05, 0x06, 0x07, 0x08, 0x09, 0x38, 0x00, 0xFB,0xff,
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 	//����û�õ����ff
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 
};

#define IO_LED	S0
#define	LED_OFF		S0=1
#define	LED_ON		S0=0
#define	MODE_CODE	0x06<<4		//ģʽֻ��4λ
#define	SYSTEM_CODE_H	0x80
#define	SYSTEM_CODE_L	0x56
#define	CONTROL_CODE	0xa7

#define	SEND_TIME_HEAD_H	95
#define	SEND_TIME_HEAD_L	32
#define	SEND_TIME_BIT_L		15
#define	SEND_TIME_BIT_H		15
/****************3010*************/
#define	USER_CODE_3010		0x00
#define	SYSTEM_CODE_3010	(USER_CODE_3010|0x80)	//���λΪ1
#define	SEND_TIME		31
/*********************************
 * ******36/38KHZ********************
 * ******************************/
//i=1���ز���i=0���ز�
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
    flag_send_code=0;	//������
    S0_AS_IO;
    S0_OUTPUT;
    LED_OFF;
    S11_AS_KEY;
}

/****************************
 * ��ȡ������j�Ƕ�***********
 * �پͶ�ȡ���ٰ���**********
 * **************************/
void read_key(unsigned char j)		
{
    while(j)
    {
	if(!(key_buffer.byte.lo&0x01))	   //Ϊ0�а���
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
    key_temp=1;		                  //������ֵ����1��ʼ
    key_value=0;	                	//��û��ֵΪ0
    /****scan_gnd*****/
    KBIF=0;			                   //����
    key_buffer.byte.lo=KEYL;
    key_buffer.byte.hi=KEYH;
    read_key(13);		               //S0--S12
    //	if(key_value)return;			 //GND�м���ֱ���˳�
    /********scan S0--S12***********/
    for(i=0;i<=12;i++)	           //key_temp��13��ʼ��1--12��GND
    {
	KEY=0xff;		                     //��KEYд����ֵ����ʼɨ��
	NOP();
	NOP();
	key_buffer.byte.lo=KEYL;
	key_buffer.byte.hi=KEYH;
	KEY=0xff;		                     //�ٴ�дKEYʱֹͣ����һ��дʱɨ����һ��
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
    flag_send_code=0;	           //������
    if(key_value)
    {
	if((key_value==16)||(key_value==57))flag_rc_6=0;
	else flag_rc_6=1;
	if(key_bak!=key_value)	flag_parity_bit=!flag_parity_bit;	//������ͬʱ��żλ��ת
	key_data=key_table[key_value-1];
	if(key_data!=0xff)
	{
	    flag_send_code=1;
	    if(!flag_rc_6)key_data<<=2;		//3010��ֻ����6λ
	}
    }
    else			                 //no key
    {
	OUTC=1;
	KBIF=0;
	KBIE=1;			                 //�������ж�
	LED_OFF;
	STOP();
	KBIE=0;		                 	//�رռ����ж�
	sys_init();
    }
}

/**********ͷ��ķ���********/
void send_head()
{
    unsigned char j;
    j=SEND_TIME_HEAD_H;      	 //����ͷ���ز�ʱ��
    do{
	OUT_38K(1);
    }while(j--);

    j=SEND_TIME_HEAD_L;	      //����ͷ�����ز�ʱ��
    do{
	OUT_38K(0);
    }while(j--);
}
/***********************************
 * ******send a byte****************
 * **��һλ�����η���***************
 * ********************************/
void send_byte(unsigned char send_data,unsigned char count)
{
    unsigned char j;
    while(count)
    {
	if(send_data&0x80)	       //�ȷ���λ
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

/**********����TRλ**********/
void send_tr_bit()
{
    unsigned char j;
    j=SEND_TIME_BIT_H<<1;	       //TRλ��ʱ��������λ������
    if(flag_parity_bit)		       //��żλ
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
	IO_LED=!IO_LED;		//LED��ȡ��
	if(flag_rc_6)
	{
	    send_head();		                //ͷ��
	    send_byte(MODE_CODE,4);		      //ģʽ��ֻ��4λ
	    /************TR bit************/
	    send_tr_bit();
	    /***********system code*********/
	    send_byte(SYSTEM_CODE_H,8);   	//ϵͳ���8λ
	    send_byte(SYSTEM_CODE_L,8);   	//ϵͳ���8λ
	    /***********control code*********/
	    send_byte(CONTROL_CODE,8);	    //��������8λ
	    /***********data code*********/
	    send_byte(key_data,6);		      //��������8λ
	    delay_100us(200);	              //20ms	���ϳ������ʱ���ڼ��ʱ��
	    delay_100us(200);	              //20ms
	    delay_100us(200);	              //20ms
	}
	else	//3010
	{
	    send_data_temp=SYSTEM_CODE_3010;
	    if(key_data<=(0x3f<<2)) send_data_temp|=0x40;		//<=0x3fʱ���ڶ�λΪ1	bit1
	    if(flag_parity_bit) send_data_temp|=0x20;			//��ż�����ı����λ	bit2
	    /***********system code*********/
	    send_byte_3010(send_data_temp,8);

	    /***********data code*********/
	    send_byte_3010(key_data,6);
	    //delay_100us(100);	//5.9ms	���ϳ������ʱ���ڼ��ʱ��
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
	    rsp		            ;������Ƚϸ��ӣ����ܸ�λ��ջ
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
    KBIF=0;		//���жϱ�־
}
