/*******************************************************************************
	Function : Define var 
*******************************************************************************/
#ifndef _EXTVAR_H
#define _EXTVAR_H

typedef  struct
{
	uchar bit0		   : 1;
	uchar bit1		   : 1;
	uchar bit2		   : 1;
	uchar bit3		   : 1;
	uchar bit4		   : 1; 
	uchar bit5		   : 1; 
	uchar bit6		   : 1; 	
	uchar bit7		   : 1; 	
} bitn;
	

extern volatile bitn Flag1,Flag2;

#define	FTempOver			Flag1.bit0
#define	FKeyAck				Flag1.bit1
#define	FFlash50			Flag1.bit2
#define	FFlash100			Flag1.bit3
#define	FADOver				Flag1.bit4
#define	FERR				Flag1.bit5
#define	F32V				Flag1.bit6
#define	F38V				Flag1.bit7
//extern volatile bitn Flag2;
#define	F2ms				Flag2.bit0
#define	FVIn				Flag2.bit1

extern uchar	KeyCnt;
extern uchar	ADCCnt;
extern uchar	TempAD;
extern ushort	TempADBuf;
extern uchar	VAD;
extern ushort	VADBuf;
extern uchar	Stauts;
extern uchar	KeyLong;
extern uchar	T50ms;
extern uchar	T100ms;
extern uchar	CellV;
extern uchar	T500ms;
extern uchar 	TempOpDly;
extern uchar	TempShDly;
extern uchar	FlashCnt;
extern uchar	V38CNT;
extern uchar	V32CNT;
extern uchar	CellDly;

extern uchar	CellLow@0xF1;

#endif

