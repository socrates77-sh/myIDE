/*****************************
Name:	MC20P02 Head File     
Author:	lxf                   
Date:	2011.01.07            
*****************************/

#ifndef	_REG_MC20P04_H
#define	_REG_MC20P04_H

#include "common.h"
#include "reg_mc20p02.h"

BYTE	rcmpc @0x0f;
#define	CMPC	rcmpc.byte
#define	C1VO	rcmpc.bitn.bit3
#define	C1EN	rcmpc.bitn.bit2
#define	C0VO	rcmpc.bitn.bit1
#define	C0EN	rcmpc.bitn.bit0

union rcmpna	
{
	uchar byte;
	struct bitns
	{
	  uchar bit0_5:6; 
	  uchar bit6:1; 
	  uchar bit7:1; 
	} bitn;
};
union rcmpna rcmp0a @0x10;
#define	CMP0A	rcmp0a.byte
#define	C0OFM	rcmp0a.bitn.bit7
#define	C0CRS	rcmp0a.bitn.bit6
#define	C0OF	rcmp0a.bitn.bit0_5

union rcmpna rcmp1a @0x11;
#define	CMP1A	rcmp1a.byte
#define	C1OFM	rcmp1a.bitn.bit7
#define	C1CRS	rcmp1a.bitn.bit6
#define	C1OF	rcmp1a.bitn.bit0_5

#endif
