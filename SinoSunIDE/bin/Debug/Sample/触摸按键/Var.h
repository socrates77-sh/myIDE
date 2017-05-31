/*******************************************************************************
	Function : Define var 
*******************************************************************************/
#ifndef _VARIABLES_H
#define _VARIABLES_H

typedef  struct
{
    uchar bit0         : 1;
    uchar bit1         : 1;
    uchar bit2         : 1;
    uchar bit3         : 1;
    uchar bit4         : 1;	
    uchar bit5         : 1; 
    uchar bit6         : 1; 	
    uchar bit7         : 1; 	
} bitn;

volatile bitn	Flag1;
//volatile bitn DispData[6];	
#define	F2ms				Flag1.bit0
#define	FCtinuKey			Flag1.bit1
#define	F500ms				Flag1.bit2
#define	FFlash				Flag1.bit3
#define	FBUZ				Flag1.bit4
#define	FADOver				Flag1.bit5
#define	FKeyAck				Flag1.bit6

//#define ushort unsigned short
uchar	BUZCnt,DisCnt,T1msCnt;
uchar	LongKey;
uchar	KeyCtnuCNT;
uchar	Heat1min;				//加热1分钟计时(用于判断热敏电阻开路)
uchar 	T500ms,T2ms; // =0x20;

uchar	KeyCode,KeyBuf;
uchar 	KeyCnt;
uchar	DispData[5];//0:LED1~LED6;1:LED7~12;2:no;3:LL;4:HH;5:HL;6:LH
uchar	BUZOnTime;
uchar 	BUZOffTime;
uchar	Buf1;
uchar 	Buf2;
uchar 	Deley2s;

  
uchar	ADCBuf[8];
ushort	ADCBuf1[8];
uchar	BomAvr;
uchar 	ADCCNT;
uchar 	ADCValue[8];

uchar 	BUZTime;
uchar 	Menu;
uchar 	ADDef[4];	//ad差值
uchar 	T2s;
uchar 	T2sCnt;
uchar	TSec;
uchar	TMin;

#endif

