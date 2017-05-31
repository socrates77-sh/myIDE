/*******************************************************************************
		Function : Define var 
*******************************************************************************/
#ifndef _VAR_H
#define _VAR_H
	
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
	
volatile bitn Flag1,Flag2;
#define	FTempOver			Flag1.bit0
#define	FKeyAck				Flag1.bit1
#define	FFlash50			Flag1.bit2
#define	FFlash100			Flag1.bit3
#define	FADOver				Flag1.bit4
#define	FERR				Flag1.bit5
#define	F32V				Flag1.bit6
#define	F38V				Flag1.bit7
//volatile bitn Flag2;
#define	F2ms				Flag2.bit0
#define	FVin				Flag2.bit1
uchar	KeyCnt;
uchar	ADCCnt;
uchar	TempAD;
ushort	TempADBuf;
uchar	VAD;
ushort	VADBuf;
uchar	Stauts;
uchar	KeyLong;
uchar	T50ms;
uchar	T100ms;
uchar	CellV;
uchar	T500ms;
uchar 	TempOpDly;
uchar	TempShDly;
uchar	FlashCnt;
uchar	V38CNT;
uchar	V32CNT;
uchar	CellDly;

uchar	CellLow@0xF1;

#endif

