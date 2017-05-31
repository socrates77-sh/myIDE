;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Jan  7 2016) (MINGW32)
; This file was generated Fri Apr 08 14:30:51 2016
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"YKQ3011.c"
	list	p=0311
	radix dec
	include "0311.inc"
;--------------------------------------------------------
; external declarations
;--------------------------------------------------------
	extern	_InitialSys
	extern	_InitalRAM
	extern	_Mode2Sbr
	extern	_MColorSbr
	extern	_T1sSbr
	extern	_STATUSbits
	extern	_P0bits
	extern	_P1bits
	extern	_MCRbits
	extern	_KBIMbits
	extern	_PDCONbits
	extern	_ODCONbits
	extern	_PUCONbits
	extern	_INTECONbits
	extern	_INTFLAGbits
	extern	_T0CRbits
	extern	_DDR0bits
	extern	_DDR1bits
	extern	_TMCRbits
	extern	_T1CRbits
	extern	_INDF
	extern	_T0CNT
	extern	_PCL
	extern	_STATUS
	extern	_FSR
	extern	_P0
	extern	_P1
	extern	_GPR
	extern	_MCR
	extern	_KBIM
	extern	_PCLATH
	extern	_PDCON
	extern	_ODCON
	extern	_PUCON
	extern	_INTECON
	extern	_INTFLAG
	extern	_T0CR
	extern	_DDR0
	extern	_DDR1
	extern	_TMCR
	extern	_T1CR
	extern	_T1CNT
	extern	_T1LOAD
	extern	_T1DATA
	extern	__gptrget1
;--------------------------------------------------------
; global declarations
;--------------------------------------------------------
	global	_main
	global	_ABuf
	global	_StatusBuf
	global	_T1s
	global	_TRedCnt
	global	_TRed
	global	_TGreen
	global	_TBlue
	global	_IRTmr
	global	_Custom
	global	_CustomRev
	global	_IRCode1
	global	_IRCodeRev1
	global	_BitCnt
	global	_delay
	global	_Tmr
	global	_TStop
	global	_T40ms
	global	_ModeCnt
	global	_Step
	global	_delay05ms
	global	_Flag1
	global	_Flag2
	global	_time_125us
	global	_time_10ms
	global	_time_1s
	global	_display_1
	global	_display_1_data
	global	_i
	global	_CoilTab

	global STK06
	global STK05
	global STK04
	global STK03
	global STK02
	global STK01
	global STK00

sharebank udata_ovr 0x0010
STK06	res 1
STK05	res 1
STK04	res 1
STK03	res 1
STK02	res 1
STK01	res 1
STK00	res 1

;--------------------------------------------------------
; global definitions
;--------------------------------------------------------
UD_YKQ3011_0	udata
_ABuf	res	1

UD_YKQ3011_1	udata
_StatusBuf	res	1

UD_YKQ3011_2	udata
_T1s	res	1

UD_YKQ3011_3	udata
_TRedCnt	res	1

UD_YKQ3011_4	udata
_TRed	res	1

UD_YKQ3011_5	udata
_TGreen	res	1

UD_YKQ3011_6	udata
_TBlue	res	1

UD_YKQ3011_7	udata
_IRTmr	res	1

UD_YKQ3011_8	udata
_Custom	res	1

UD_YKQ3011_9	udata
_CustomRev	res	1

UD_YKQ3011_10	udata
_IRCode1	res	1

UD_YKQ3011_11	udata
_IRCodeRev1	res	1

UD_YKQ3011_12	udata
_BitCnt	res	1

UD_YKQ3011_13	udata
_delay	res	1

UD_YKQ3011_14	udata
_Tmr	res	2

UD_YKQ3011_15	udata
_TStop	res	1

UD_YKQ3011_16	udata
_T40ms	res	1

UD_YKQ3011_17	udata
_ModeCnt	res	1

UD_YKQ3011_18	udata
_Step	res	1

UD_YKQ3011_19	udata
_delay05ms	res	1

UD_YKQ3011_20	udata
_Flag1	res	1

UD_YKQ3011_21	udata
_Flag2	res	1

UD_YKQ3011_22	udata
_time_125us	res	1

UD_YKQ3011_23	udata
_time_10ms	res	1

UD_YKQ3011_24	udata
_time_1s	res	1

UD_YKQ3011_25	udata
_display_1	res	1

UD_YKQ3011_26	udata
_display_1_data	res	1

UD_YKQ3011_27	udata
_i	res	2

;--------------------------------------------------------
; absolute symbol definitions
;--------------------------------------------------------
;--------------------------------------------------------
; compiler-defined variables
;--------------------------------------------------------
UDL_YKQ3011_0	udata
r0x1009	res	1
r0x100A	res	1
;--------------------------------------------------------
; initialized data
;--------------------------------------------------------

ID_YKQ3011_0	code
_CoilTab
	retai 0x3f
	retai 0x06
	retai 0x5b
	retai 0x4f
	retai 0x66
	retai 0x6d
	retai 0x7d
	retai 0x07
	retai 0x7f
	retai 0x6f
	retai 0x77
	retai 0x7c
	retai 0x39
	retai 0x5e
	retai 0x79
	retai 0x71
	retai 0x73
	retai 0x00
	retai 0x00


;@Allocation info for local variables in function 'main'
;@main InitialSys                Allocated to registers ;size:2
;@main InitalRAM                 Allocated to registers ;size:2
;@main Mode2Sbr                  Allocated to registers ;size:2
;@main MColorSbr                 Allocated to registers ;size:2
;@main T1sSbr                    Allocated to registers ;size:2
;@main main                      Allocated to registers ;size:2
;@main STATUSbits                Allocated to registers ;size:1
;@main P0bits                    Allocated to registers ;size:1
;@main P1bits                    Allocated to registers ;size:1
;@main MCRbits                   Allocated to registers ;size:1
;@main KBIMbits                  Allocated to registers ;size:1
;@main PDCONbits                 Allocated to registers ;size:1
;@main ODCONbits                 Allocated to registers ;size:1
;@main PUCONbits                 Allocated to registers ;size:1
;@main INTECONbits               Allocated to registers ;size:1
;@main INTFLAGbits               Allocated to registers ;size:1
;@main T0CRbits                  Allocated to registers ;size:1
;@main DDR0bits                  Allocated to registers ;size:1
;@main DDR1bits                  Allocated to registers ;size:1
;@main TMCRbits                  Allocated to registers ;size:1
;@main T1CRbits                  Allocated to registers ;size:1
;@main ABuf                      Allocated to registers ;size:1
;@main StatusBuf                 Allocated to registers ;size:1
;@main T1s                       Allocated to registers ;size:1
;@main TRedCnt                   Allocated to registers ;size:1
;@main TRed                      Allocated to registers ;size:1
;@main TGreen                    Allocated to registers ;size:1
;@main TBlue                     Allocated to registers ;size:1
;@main IRTmr                     Allocated to registers ;size:1
;@main Custom                    Allocated to registers ;size:1
;@main CustomRev                 Allocated to registers ;size:1
;@main IRCode1                   Allocated to registers ;size:1
;@main IRCodeRev1                Allocated to registers ;size:1
;@main BitCnt                    Allocated to registers ;size:1
;@main delay                     Allocated to registers ;size:1
;@main Tmr                       Allocated to registers ;size:2
;@main TStop                     Allocated to registers ;size:1
;@main T40ms                     Allocated to registers ;size:1
;@main ModeCnt                   Allocated to registers ;size:1
;@main Step                      Allocated to registers ;size:1
;@main delay05ms                 Allocated to registers ;size:1
;@main Flag1                     Allocated to registers ;size:1
;@main Flag2                     Allocated to registers ;size:1
;@main time_125us                Allocated to registers ;size:1
;@main time_10ms                 Allocated to registers ;size:1
;@main time_1s                   Allocated to registers ;size:1
;@main display_1                 Allocated to registers ;size:1
;@main display_1_data            Allocated to registers ;size:1
;@main i                         Allocated to registers ;size:2
;@main aaa                       Allocated to registers ;size:1
;@main INDF                      Allocated to registers ;size:1
;@main T0CNT                     Allocated to registers ;size:1
;@main PCL                       Allocated to registers ;size:1
;@main STATUS                    Allocated to registers ;size:1
;@main FSR                       Allocated to registers ;size:1
;@main P0                        Allocated to registers ;size:1
;@main P1                        Allocated to registers ;size:1
;@main GPR                       Allocated to registers ;size:1
;@main MCR                       Allocated to registers ;size:1
;@main KBIM                      Allocated to registers ;size:1
;@main PCLATH                    Allocated to registers ;size:1
;@main PDCON                     Allocated to registers ;size:1
;@main ODCON                     Allocated to registers ;size:1
;@main PUCON                     Allocated to registers ;size:1
;@main INTECON                   Allocated to registers ;size:1
;@main INTFLAG                   Allocated to registers ;size:1
;@main T0CR                      Allocated to registers ;size:1
;@main DDR0                      Allocated to registers ;size:1
;@main DDR1                      Allocated to registers ;size:1
;@main TMCR                      Allocated to registers ;size:1
;@main T1CR                      Allocated to registers ;size:1
;@main T1CNT                     Allocated to registers ;size:1
;@main T1LOAD                    Allocated to registers ;size:1
;@main T1DATA                    Allocated to registers ;size:1
;@end Allocation info for local variables in function 'main';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; reset vector 
;--------------------------------------------------------
STARTUP	code 0x0000
	goto	_main
;--------------------------------------------------------
; code
;--------------------------------------------------------
code_YKQ3011	code
;***
;  pBlock Stats: dbName = M
;***
;entry:  _main	;Function start
; 2 exit points
;has an exit
;functions called:
;   _InitialSys
;   _InitalRAM
;   __gptrget1
;   _Mode2Sbr
;   _MColorSbr
;   _T1sSbr
;   _InitialSys
;   _InitalRAM
;   __gptrget1
;   _Mode2Sbr
;   _MColorSbr
;   _T1sSbr
;4 compiler assigned registers:
;   r0x1009
;   r0x100A
;   STK01
;   STK00
;; Starting pCode block
_main	;Function start
; 2 exit points
;	.line	27; "YKQ3011.c"	GIE = 0;    
	BCLR	_INTECONbits,7
;	.line	28; "YKQ3011.c"	ClrWdt();
	clrwdt
;	.line	29; "YKQ3011.c"	InitialSys();
	CALL	_InitialSys
;	.line	30; "YKQ3011.c"	InitalRAM();
	CALL	_InitalRAM
;	.line	31; "YKQ3011.c"	FMultiColor = 1;
	BSET	_Flag2,4
;	.line	32; "YKQ3011.c"	FOn = 1;
	BSET	_Flag1,0
;	.line	33; "YKQ3011.c"	pIR = 1;
	BSET	_P1bits,3
;	.line	34; "YKQ3011.c"	KBIE = 1;
	BSET	_INTECONbits,1
;	.line	35; "YKQ3011.c"	KBIM3 = 1;
	BSET	_KBIMbits,3
;	.line	36; "YKQ3011.c"	GIE = 1;
	BSET	_INTECONbits,7
;	.line	37; "YKQ3011.c"	for(i=0;i<19;i++)
	CLRR	_i
	CLRR	(_i + 1)
;;signed compare: left < lit(0x13=19), size=2, mask=ffff
_00140_DS_
	MOVAR	(_i + 1)
	ADDAI	0x80
	ADDAI	0x80
	JBSET	STATUS,2
	GOTO	_00180_DS_
	MOVAI	0x13
	RSUBAR	_i
_00180_DS_
	JBCLR	STATUS,0
	GOTO	_00138_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	39; "YKQ3011.c"	display_1_data = i;
	MOVAR	_i
	MOVRA	_display_1_data
;	.line	40; "YKQ3011.c"	display_1      = CoilTab[display_1_data];
	MOVAR	_display_1_data
	ADDAI	(_CoilTab + 0)
	MOVRA	r0x1009
	MOVAI	high (_CoilTab + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x100A
	MOVAR	r0x1009
	MOVRA	STK01
	MOVAR	r0x100A
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	_display_1
;	.line	37; "YKQ3011.c"	for(i=0;i<19;i++)
	INCR	_i
	JBCLR	STATUS,2
	INCR	(_i + 1)
	GOTO	_00140_DS_
_00138_DS_
;	.line	44; "YKQ3011.c"	if (FStop)
	JBSET	_Flag1,7
	GOTO	_00106_DS_
;	.line	46; "YKQ3011.c"	T1IF = 0;
	BCLR	_TMCRbits,0
;	.line	47; "YKQ3011.c"	T0IF = 0;
	BCLR	_INTFLAGbits,0
_00106_DS_
;	.line	49; "YKQ3011.c"	if ((delay05ms != 0)||(!FOn))
	MOVAI	0x00
	ORAR	_delay05ms
	JBSET	STATUS,2
	GOTO	_00112_DS_
	JBCLR	_Flag1,0
	GOTO	_00113_DS_
_00112_DS_
;	.line	51; "YKQ3011.c"	pRedC = 1;
	BSET	_DDR1bits,0
;	.line	52; "YKQ3011.c"	pGreenC = 1;
	BSET	_DDR1bits,1
;	.line	53; "YKQ3011.c"	pBlueC = 1;
	BSET	_DDR1bits,2
	GOTO	_00114_DS_
_00113_DS_
;	.line	57; "YKQ3011.c"	switch(FMultiColor)
	CLRR	r0x1009
	JBCLR	_Flag2,4
	INCR	r0x1009
;;100	MOVAR	r0x1009
;;99	MOVAR	r0x100A
	MOVAR	r0x1009
	MOVRA	r0x100A
	JBSET	STATUS,2
	GOTO	_00181_DS_
	GOTO	_00108_DS_
_00181_DS_
	MOVAR	r0x100A
	XORAI	0x01
	JBSET	STATUS,2
	GOTO	_00114_DS_
;	.line	60; "YKQ3011.c"	pRedC = 0;
	BCLR	_DDR1bits,0
;	.line	61; "YKQ3011.c"	pGreenC = 0;
	BCLR	_DDR1bits,1
;	.line	62; "YKQ3011.c"	pBlueC = 0;
	BCLR	_DDR1bits,2
;	.line	63; "YKQ3011.c"	break;
	GOTO	_00114_DS_
_00108_DS_
;	.line	66; "YKQ3011.c"	if (FCandle) Mode2Sbr();
	JBSET	_Flag2,5
	GOTO	_00114_DS_
	CALL	_Mode2Sbr
_00114_DS_
;	.line	71; "YKQ3011.c"	if (F4ms)
	JBSET	_Flag2,2
	GOTO	_00138_DS_
;	.line	73; "YKQ3011.c"	F4ms = 0;
	BCLR	_Flag2,2
;	.line	74; "YKQ3011.c"	ClrWdt();
	clrwdt
;	.line	75; "YKQ3011.c"	if (delay05ms == 0)
	MOVAI	0x00
	ORAR	_delay05ms
	JBSET	STATUS,2
	GOTO	_00133_DS_
;	.line	77; "YKQ3011.c"	if (FOn)
	JBSET	_Flag1,0
	GOTO	_00134_DS_
;	.line	79; "YKQ3011.c"	if (FMultiColor) MColorSbr();
	JBSET	_Flag2,4
	GOTO	_00128_DS_
	CALL	_MColorSbr
	GOTO	_00134_DS_
_00128_DS_
;	.line	82; "YKQ3011.c"	if (FCandle)
	JBSET	_Flag2,5
	GOTO	_00134_DS_
;	.line	85; "YKQ3011.c"	if (++T40ms > 10)
	INCR	_T40ms
;;swapping arguments (AOP_TYPEs 1/3)
;;unsigned compare: left >= lit(0xB=11), size=1
	MOVAI	0x0b
	RSUBAR	_T40ms
	JBSET	STATUS,0
	GOTO	_00134_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	87; "YKQ3011.c"	T40ms = 0;
	CLRR	_T40ms
;	.line	88; "YKQ3011.c"	if (FDirection)
	JBSET	_Flag1,1
	GOTO	_00121_DS_
;	.line	90; "YKQ3011.c"	if (++Step > 50) FDirection = 0;
	INCR	_Step
;;swapping arguments (AOP_TYPEs 1/3)
;;unsigned compare: left >= lit(0x33=51), size=1
	MOVAI	0x33
	RSUBAR	_Step
	JBSET	STATUS,0
	GOTO	_00134_DS_
;;genSkipc:3251: created from rifx:0028608C
	BCLR	_Flag1,1
	GOTO	_00134_DS_
_00121_DS_
;	.line	94; "YKQ3011.c"	if (--Step < 10) FDirection = 1;
	DECR	_Step
;;unsigned compare: left < lit(0xA=10), size=1
	MOVAI	0x0a
	RSUBAR	_Step
	JBCLR	STATUS,0
	GOTO	_00134_DS_
;;genSkipc:3251: created from rifx:0028608C
	BSET	_Flag1,1
	GOTO	_00134_DS_
_00133_DS_
;	.line	101; "YKQ3011.c"	else --delay05ms;
	DECR	_delay05ms
_00134_DS_
;	.line	102; "YKQ3011.c"	T1sSbr();
	CALL	_T1sSbr
	GOTO	_00138_DS_
	RETURN	
; exit point of _main


;	code size estimation:
;	  114+    0 =   114 instructions (  228 byte)

	end
