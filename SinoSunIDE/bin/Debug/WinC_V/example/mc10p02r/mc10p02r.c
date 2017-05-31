/*****************************
Program:	WX0107(康佳码型，38k，6*6键盘，3.64M晶振)
Author :	Liang HuiFeng
Date   :	2011.04.18
Email  :  tony@0e2.org
******************************/
//PB0,PA1,PA2,PA3,PA4,PA5 输入
//GND,PA0,PB4,PB5,PB6,PB7 输出

#include <common.h>
#include  <reg_mc10p02r.h>

//****************************
#define SysCode 0X02//
uchar KeyValue;
volatile uchar Debounce;

const unsigned char TVCode[36]=//实际36个按键
{
  0X14,0X0b,0X01,0X02,0X03,0X04,  //;k01,02,03,04,05,06
	0X05,0X06,0X07,0X08,0X09,0X0A,	//;k07,08,09,10,11,12
	0X00,0X0E,0X0D,0X1E,0X1D,0X15,	//;k13,14,15,16,17,18
	0X11,0X30,0X12,0X2F,0X13,0X10,	//;k19,20,21,22,23,24
	0X1C,0X1A,0X19,0X20,0X0C,0X1F,	//;k25,26,27,28,29,30
	0X21,0X16,0X17,0X18,0X0F,0X1B   //;k31,32,33,34,35,36	
};

#include  "int_mc10p02r.h"
//*****************************

void  InitRst(void)//寄存器初始化
{

    DDRA=0XC1;
    KBIM=0X3E;

    DDRB=0XFF;//PB0中断打开
    DDRC=0XFF;
    MCR=0X00;
    PA=0;
    PB=0;
    PC=0;

    TCR=0x0b;//3.64M	pre-Scale=8 1ms
    TDR=227;

}

void KeyScan(void)//按键扫描子函数
{
    uchar i,temp,KeyCounter,Cycle;
    KeyValue=0x3f;	//没有按键
    KeyCounter=0;
    temp=0x04;
    i=0;
    Cycle=0;
    while((i<6)&&(KeyValue!=0x3e)&&(KeyCounter<2))
    {
        PA0=1;
        PB=0XFF;
        if(i==1) PA0=0; 
        if(i>1) PB=~temp;
        switch((PA&0X3E)|PB0)
        {
        case 0x3e:
            KeyValue=Cycle+0;
            KeyCounter++;
            break;
        case 0x3D:
            KeyValue=Cycle+1;
            KeyCounter++;
            break;
        case 0x3B:
            KeyValue=Cycle+2;
            KeyCounter++;
            break;
        case 0x37:
            KeyValue=Cycle+3;
            KeyCounter++;
            break;
        case 0x2f:
            KeyValue=Cycle+4;
            KeyCounter++;
            break;
        case 0x1f:
            KeyValue=Cycle+5;
            KeyCounter++;
            break;
        case 0x3f:
            break;//没有按键，keyValue=0x3f
        default:
            KeyValue=0x3e;
            break;//异常，KeyValue=0x3e
        }
        if((i==0)&&(KeyCounter==1)) break;//先扫描GND
        
        temp=temp<<1;
        i++;
        Cycle=Cycle+6;
    }
    if(KeyCounter>1) KeyValue=0x3e;
}

void Unit38K1(uchar Unit)
{
    uchar j;
    for(j=0; j<Unit; j++)
    {
        OUTC=1;//15--5
        NOP();//2
        NOP();//2
        NOP();//2
        NOP();//2
        NOP();//2
        NOP();//2
        OUTC=0;//32--5
        NOP();
        NOP();

        NOP();//2
        NOP();//2
        NOP();//2
        NOP();//2
    }
}

void Unit38K0(uchar Unit)
{
    uchar j;
    for(j=0; j<Unit; j++)
    {
        OUTC=0;//15--5
        NOP();//2
        NOP();//2
        NOP();//2
        NOP();//2
        NOP();//2
        NOP();//2
        OUTC=0;//32--5
        NOP();
        NOP();

        NOP();//2
        NOP();//2
        NOP();//2
        NOP();//2
    }
}

void IrOutHeadH(void)//3ms
{
    Unit38K1(114);
}

void IrOutHeadL(void)//3ms
{
    Unit38K0(114);
}

void SendBit1(void)//0.5ms H,2.5ms L
{
    Unit38K1(19);
    Unit38K0(95);
}

void SendBit0(void)//0.5ms H,1.5ms L
{
    Unit38K1(19);
    Unit38K0(57);
}

void IrOut(uchar CodeData,uchar Count)
{
    uchar i;
    for(i=0; i<Count; i++)
    {
        if(CodeData&0x80) SendBit1();
        else SendBit0();
        CodeData=CodeData<<1;
    }
}

void  main(void)
{
    SEI();
    InitRst();
    CLI();
    while(1)
    {
        KeyScan();

        if(KeyValue==0x3f)
        {
            PB=0;
            PA=0;
            Debounce=0;
            KBIE=1;
            NOP();
            NOP();
            STOP();
            NOP();
            NOP();
            KBIE=0;
        }
        else if(KeyValue==0x3e)  Debounce=0;

        else if(TVCode[KeyValue]!=0XFF)//23ms
        {
            SEI();
            IrOutHeadH();//3ms
            IrOutHeadL();//3ms
            IrOut(SysCode,8);
            IrOut(TVCode[KeyValue],8);
            Unit38K1(19);//0.5ms H
            Unit38K0(152);//4ms L
            Unit38K1(19);//0.5ms H
            Debounce=0; 
            TCR=0x0b;//3.64M	pre-Scale=8 1ms
            TDR=227;
            CLI();
            while(Debounce<23) ;//
        }
    }
}