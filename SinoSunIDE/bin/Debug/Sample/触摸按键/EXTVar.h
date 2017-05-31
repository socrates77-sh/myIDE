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

extern volatile bitn	Flag1;
//volatile bitn DispData[6];	
#define	F2ms				Flag1.bit0
#define	FCtinuKey			Flag1.bit1
#define	F500ms				Flag1.bit2
#define	FFlash				Flag1.bit3
#define	FBUZ				Flag1.bit4
#define	FADOver				Flag1.bit5
#define	FKeyAck				Flag1.bit6

//#define ushort unsigned short
extern uchar	BUZCnt,DisCnt;
extern uchar	MinCnt;
extern uchar	LongKey;
extern uchar	KeyCode,KeyCtnuCNT;
extern uchar	Heat1min;				//加热1分钟计时(用于判断热敏电阻开路)

extern uchar 	T500ms,T2ms;	// =0x20;

extern uchar	KeyBuf;
extern uchar 	KeyCnt;
extern uchar	DispData[7];
extern uchar	BUZOnTime;
extern uchar 	BUZOffTime;
extern uchar	Buf1;
extern uchar 	Buf2;
extern uchar 	Deley2s;

  
extern uchar	ADCBuf[8];
extern ushort	ADCBuf1[8];
extern uchar	BomAvr;
extern uchar 	ADCCNT;
extern uchar 	ADCValue[8];

extern uchar 	BUZTime;
extern uchar	Menu;
extern uchar	ADDef[4];
extern uchar 	T2s;
extern uchar 	T2sCnt;
extern uchar	TSec;
extern uchar	TMin;

 

#endif

