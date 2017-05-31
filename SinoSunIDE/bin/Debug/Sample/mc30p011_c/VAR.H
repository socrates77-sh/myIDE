
#include <mc3x-common.h>
#include "CONST.H"
/*****************************************************************
;      	Function : Define variables
;*****************************************************************/
#ifndef _VAR_H_
#define _VAR_H_

uchar   ABuf,StatusBuf;
uchar  	T1s;
uchar  	TRedCnt;
uchar  	TRed,TGreen,TBlue;
uchar  	IRTmr;
uchar  	Custom;
uchar  	CustomRev;
uchar  	IRCode1;
uchar  	IRCodeRev1;
uchar  	BitCnt;
//uchar	IRCnt;
uchar  	delay;
ushort 	Tmr;
uchar  	TStop;
uchar  	T40ms;
uchar  	ModeCnt;
uchar  	Step;
uchar  	delay05ms;

//;0x30
//IRDate       	       	20

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

volatile bitn  	Flag1,Flag2;

//;标志定义-------------------------------------
#define	FOn    	       	       	Flag1.bit0
#define	FDirection     	Flag1.bit1 
#define	FDouble	       	Flag1.bit2
#define	FIRAck 	       	Flag1.bit3
#define	FLead  	       	Flag1.bit4
#define	FTmr   	       	Flag1.bit5
#define	FLongRKey      	Flag1.bit6     	
#define	FStop  	       	Flag1.bit7

#define	F4ms   	       	Flag2.bit2
#define	FMultiColor    	Flag2.bit4
#define	FCandle	       	Flag2.bit5

#endif


