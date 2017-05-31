;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Aug  7 2015) (MINGW32)
; This file was generated Thu Jun 23 15:01:26 2016
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"IR.c"
	list	p=0311
	radix dec
	include "0311.inc"
;--------------------------------------------------------
; external declarations
;--------------------------------------------------------
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
	extern	_ABuf
	extern	_StatusBuf
	extern	_T1s
	extern	_TRedCnt
	extern	_TRed
	extern	_TGreen
	extern	_TBlue
	extern	_IRTmr
	extern	_Custom
	extern	_CustomRev
	extern	_IRCode1
	extern	_IRCodeRev1
	extern	_BitCnt
	extern	_delay
	extern	_Tmr
	extern	_TStop
	extern	_T40ms
	extern	_ModeCnt
	extern	_Step
	extern	_delay05ms
	extern	_Flag1
	extern	_Flag2
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

	extern STK06
	extern STK05
	extern STK04
	extern STK03
	extern STK02
	extern STK01
	extern STK00
;--------------------------------------------------------
; global declarations
;--------------------------------------------------------
	global	_IRSbr4
	global	_IRSbr
	global	_IROffKeySbr
	global	_BlueTbl
	global	_GreenTbl
	global	_RedTbl

;--------------------------------------------------------
; global definitions
;--------------------------------------------------------
;--------------------------------------------------------
; absolute symbol definitions
;--------------------------------------------------------
;--------------------------------------------------------
; compiler-defined variables
;--------------------------------------------------------
UDL_IR_0	udata
r0x1012	res	1
r0x1013	res	1
r0x1014	res	1
;--------------------------------------------------------
; initialized data
;--------------------------------------------------------

ID_IR_0	code
_BlueTbl
	retai 0xff
	retai 0xff
	retai 0x0e
	retai 0xfc
	retai 0xf7
	retai 0x26
	retai 0xff
	retai 0xae
	retai 0xd6
	retai 0x23
	retai 0xd6
	retai 0x75


ID_IR_1	code
_GreenTbl
	retai 0xff
	retai 0x0e
	retai 0xff
	retai 0x8b
	retai 0x18
	retai 0xf7
	retai 0x26
	retai 0x1f
	retai 0x26
	retai 0x58
	retai 0xf7
	retai 0xdd


ID_IR_2	code
_RedTbl
	retai 0x0e
	retai 0xff
	retai 0xff
	retai 0x1a
	retai 0xa0
	retai 0xe3
	retai 0x00
	retai 0xae
	retai 0x31
	retai 0xe3
	retai 0x27
	retai 0xae


;@Allocation info for local variables in function 'IRSbr'
;@IRSbr IRSbr4                    Allocated to registers ;size:2
;@IRSbr IRSbr                     Allocated to registers ;size:2
;@IRSbr STATUSbits                Allocated to registers ;size:1
;@IRSbr P0bits                    Allocated to registers ;size:1
;@IRSbr P1bits                    Allocated to registers ;size:1
;@IRSbr MCRbits                   Allocated to registers ;size:1
;@IRSbr KBIMbits                  Allocated to registers ;size:1
;@IRSbr PDCONbits                 Allocated to registers ;size:1
;@IRSbr ODCONbits                 Allocated to registers ;size:1
;@IRSbr PUCONbits                 Allocated to registers ;size:1
;@IRSbr INTECONbits               Allocated to registers ;size:1
;@IRSbr INTFLAGbits               Allocated to registers ;size:1
;@IRSbr T0CRbits                  Allocated to registers ;size:1
;@IRSbr DDR0bits                  Allocated to registers ;size:1
;@IRSbr DDR1bits                  Allocated to registers ;size:1
;@IRSbr TMCRbits                  Allocated to registers ;size:1
;@IRSbr T1CRbits                  Allocated to registers ;size:1
;@IRSbr ABuf                      Allocated to registers ;size:1
;@IRSbr StatusBuf                 Allocated to registers ;size:1
;@IRSbr T1s                       Allocated to registers ;size:1
;@IRSbr TRedCnt                   Allocated to registers ;size:1
;@IRSbr TRed                      Allocated to registers ;size:1
;@IRSbr TGreen                    Allocated to registers ;size:1
;@IRSbr TBlue                     Allocated to registers ;size:1
;@IRSbr IRTmr                     Allocated to registers ;size:1
;@IRSbr Custom                    Allocated to registers ;size:1
;@IRSbr CustomRev                 Allocated to registers ;size:1
;@IRSbr IRCode1                   Allocated to registers ;size:1
;@IRSbr IRCodeRev1                Allocated to registers ;size:1
;@IRSbr BitCnt                    Allocated to registers ;size:1
;@IRSbr delay                     Allocated to registers ;size:1
;@IRSbr Tmr                       Allocated to registers ;size:2
;@IRSbr TStop                     Allocated to registers ;size:1
;@IRSbr T40ms                     Allocated to registers ;size:1
;@IRSbr ModeCnt                   Allocated to registers ;size:1
;@IRSbr Step                      Allocated to registers ;size:1
;@IRSbr delay05ms                 Allocated to registers ;size:1
;@IRSbr Flag1                     Allocated to registers ;size:1
;@IRSbr Flag2                     Allocated to registers ;size:1
;@IRSbr INDF                      Allocated to registers ;size:1
;@IRSbr T0CNT                     Allocated to registers ;size:1
;@IRSbr PCL                       Allocated to registers ;size:1
;@IRSbr STATUS                    Allocated to registers ;size:1
;@IRSbr FSR                       Allocated to registers ;size:1
;@IRSbr P0                        Allocated to registers ;size:1
;@IRSbr P1                        Allocated to registers ;size:1
;@IRSbr GPR                       Allocated to registers ;size:1
;@IRSbr MCR                       Allocated to registers ;size:1
;@IRSbr KBIM                      Allocated to registers ;size:1
;@IRSbr PCLATH                    Allocated to registers ;size:1
;@IRSbr PDCON                     Allocated to registers ;size:1
;@IRSbr ODCON                     Allocated to registers ;size:1
;@IRSbr PUCON                     Allocated to registers ;size:1
;@IRSbr INTECON                   Allocated to registers ;size:1
;@IRSbr INTFLAG                   Allocated to registers ;size:1
;@IRSbr T0CR                      Allocated to registers ;size:1
;@IRSbr DDR0                      Allocated to registers ;size:1
;@IRSbr DDR1                      Allocated to registers ;size:1
;@IRSbr TMCR                      Allocated to registers ;size:1
;@IRSbr T1CR                      Allocated to registers ;size:1
;@IRSbr T1CNT                     Allocated to registers ;size:1
;@IRSbr T1LOAD                    Allocated to registers ;size:1
;@IRSbr T1DATA                    Allocated to registers ;size:1
;@end Allocation info for local variables in function 'IRSbr';
;@Allocation info for local variables in function 'IROffKeySbr'
;@end Allocation info for local variables in function 'IROffKeySbr';
;@Allocation info for local variables in function 'IRSbr4'
;@IRSbr4 i                         Allocated to registers r0x1012 ;size:1
;@end Allocation info for local variables in function 'IRSbr4';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; code
;--------------------------------------------------------
code_IR	code
;***
;  pBlock Stats: dbName = C
;***
;entry:  _IRSbr4	;Function start
; 2 exit points
;has an exit
;functions called:
;   _IROffKeySbr
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   _IROffKeySbr
;   __gptrget1
;   __gptrget1
;   __gptrget1
;5 compiler assigned registers:
;   r0x1012
;   r0x1013
;   r0x1014
;   STK01
;   STK00
;; Starting pCode block
_IRSbr4	;Function start
; 2 exit points
;	.line	92; "IR.c"	FLead = 0;
	BCLR	_Flag1,4
;	.line	94; "IR.c"	i = ~IRCode1;
	COMAR	_IRCode1
	MOVRA	r0x1012
;	.line	95; "IR.c"	if (IRCodeRev1 != i) return;
	MOVAR	r0x1012
	XORAR	_IRCodeRev1
	JBSET	STATUS,2
	GOTO	_00227_DS_
	GOTO	_00127_DS_
_00227_DS_
	GOTO	_00175_DS_
_00127_DS_
;	.line	97; "IR.c"	i = ~Custom;
	COMAR	_Custom
	MOVRA	r0x1012
;	.line	98; "IR.c"	if (CustomRev != i) return;
	MOVAR	r0x1012
	XORAR	_CustomRev
	JBSET	STATUS,2
	GOTO	_00228_DS_
	GOTO	_00129_DS_
_00228_DS_
	GOTO	_00175_DS_
_00129_DS_
;	.line	99; "IR.c"	if (Custom != CIDCode) return;
	MOVAI	0x00
	ORAR	_Custom
	JBCLR	STATUS,2
	GOTO	_00131_DS_
	GOTO	_00175_DS_
_00131_DS_
;	.line	101; "IR.c"	if (FIRAck) return;
	JBSET	_Flag1,3
	GOTO	_00133_DS_
	GOTO	_00175_DS_
_00133_DS_
;	.line	102; "IR.c"	FIRAck = 1;
	BSET	_Flag1,3
;	.line	104; "IR.c"	if (IRCodeRev1 == CIRON)
	MOVAR	_IRCodeRev1
	XORAI	0x45
	JBSET	STATUS,2
	GOTO	_00137_DS_
;	.line	106; "IR.c"	if (FOn) return;
	JBSET	_Flag1,0
	GOTO	_00135_DS_
	GOTO	_00175_DS_
_00135_DS_
;	.line	107; "IR.c"	FOn = 1;
	BSET	_Flag1,0
;	.line	108; "IR.c"	FTmr = 0;
	BCLR	_Flag1,5
_00137_DS_
;	.line	110; "IR.c"	if (!FOn) return;
	JBCLR	_Flag1,0
	GOTO	_00139_DS_
	GOTO	_00175_DS_
_00139_DS_
;	.line	112; "IR.c"	if (IRCodeRev1 == CIROFF)
	MOVAR	_IRCodeRev1
	XORAI	0x46
	JBSET	STATUS,2
	GOTO	_00141_DS_
;	.line	114; "IR.c"	IROffKeySbr();
	CALL	_IROffKeySbr
;	.line	115; "IR.c"	return;
	GOTO	_00175_DS_
_00141_DS_
;	.line	117; "IR.c"	if (IRCodeRev1 == CIRMODE)
	MOVAR	_IRCodeRev1
	XORAI	0x47
	JBSET	STATUS,2
	GOTO	_00143_DS_
;	.line	123; "IR.c"	__endasm;
	movai 0x20
	xorra _Flag2
	
;	.line	124; "IR.c"	Step = 10;
	MOVAI	0x0a
	MOVRA	_Step
;	.line	125; "IR.c"	FDirection = 0;
	BCLR	_Flag1,1
;	.line	126; "IR.c"	ModeCnt = 0;
	CLRR	_ModeCnt
;	.line	127; "IR.c"	T40ms = 0xff;
	MOVAI	0xff
	MOVRA	_T40ms
;	.line	128; "IR.c"	return;
	GOTO	_00175_DS_
_00143_DS_
;	.line	130; "IR.c"	if (IRCodeRev1 == CIR4H)
	MOVAR	_IRCodeRev1
	XORAI	0x44
	JBSET	STATUS,2
	GOTO	_00146_DS_
;	.line	132; "IR.c"	Tmr = 0x3840;
	MOVAI	0x40
	MOVRA	_Tmr
	MOVAI	0x38
	MOVRA	(_Tmr + 1)
_00144_DS_
;	.line	134; "IR.c"	T1s = 250;
	MOVAI	0xfa
	MOVRA	_T1s
;	.line	135; "IR.c"	FTmr = 1;
	BSET	_Flag1,5
;	.line	136; "IR.c"	return;
	GOTO	_00175_DS_
_00146_DS_
;	.line	138; "IR.c"	if (IRCodeRev1 == CIR8H)
	MOVAR	_IRCodeRev1
	XORAI	0x40
	JBSET	STATUS,2
	GOTO	_00148_DS_
;	.line	140; "IR.c"	Tmr = 0x7080;
	MOVAI	0x80
	MOVRA	_Tmr
	MOVAI	0x70
	MOVRA	(_Tmr + 1)
;	.line	141; "IR.c"	goto    IR4HKeySbr1;
	GOTO	_00144_DS_
_00148_DS_
;	.line	143; "IR.c"	if (IRCodeRev1 == CIRMCOLR)
	MOVAR	_IRCodeRev1
	XORAI	0x43
	JBSET	STATUS,2
	GOTO	_00150_DS_
;	.line	149; "IR.c"	__endasm;
	movai 0x10
	xorra _Flag2
	
;	.line	150; "IR.c"	return;
	GOTO	_00175_DS_
_00150_DS_
;	.line	152; "IR.c"	if (IRCodeRev1 == CIRCOLR1) {i = 0;}
	MOVAR	_IRCodeRev1
	XORAI	0x16
	JBSET	STATUS,2
	GOTO	_00152_DS_
	CLRR	r0x1012
_00152_DS_
;	.line	153; "IR.c"	if (IRCodeRev1 == CIRCOLR2) {i = 1;}
	MOVAR	_IRCodeRev1
	XORAI	0x19
	JBSET	STATUS,2
	GOTO	_00154_DS_
	MOVAI	0x01
	MOVRA	r0x1012
_00154_DS_
;	.line	154; "IR.c"	if (IRCodeRev1 == CIRCOLR3) {i = 2;}
	MOVAR	_IRCodeRev1
	XORAI	0x0d
	JBSET	STATUS,2
	GOTO	_00156_DS_
	MOVAI	0x02
	MOVRA	r0x1012
_00156_DS_
;	.line	155; "IR.c"	if (IRCodeRev1 == CIRCOLR4) {i = 3;}
	MOVAR	_IRCodeRev1
	XORAI	0x0c
	JBSET	STATUS,2
	GOTO	_00158_DS_
	MOVAI	0x03
	MOVRA	r0x1012
_00158_DS_
;	.line	156; "IR.c"	if (IRCodeRev1 == CIRCOLR5) {i = 4;}
	MOVAR	_IRCodeRev1
	XORAI	0x18
	JBSET	STATUS,2
	GOTO	_00160_DS_
	MOVAI	0x04
	MOVRA	r0x1012
_00160_DS_
;	.line	157; "IR.c"	if (IRCodeRev1 == CIRCOLR6) {i = 5;}
	MOVAR	_IRCodeRev1
	XORAI	0x5e
	JBSET	STATUS,2
	GOTO	_00162_DS_
	MOVAI	0x05
	MOVRA	r0x1012
_00162_DS_
;	.line	158; "IR.c"	if (IRCodeRev1 == CIRCOLR7) {i = 6;}
	MOVAR	_IRCodeRev1
	XORAI	0x08
	JBSET	STATUS,2
	GOTO	_00164_DS_
	MOVAI	0x06
	MOVRA	r0x1012
_00164_DS_
;	.line	159; "IR.c"	if (IRCodeRev1 == CIRCOLR8) {i = 7;}
	MOVAR	_IRCodeRev1
	XORAI	0x1c
	JBSET	STATUS,2
	GOTO	_00166_DS_
	MOVAI	0x07
	MOVRA	r0x1012
_00166_DS_
;	.line	160; "IR.c"	if (IRCodeRev1 == CIRCOLR9) {i = 8;}
	MOVAR	_IRCodeRev1
	XORAI	0x5a
	JBSET	STATUS,2
	GOTO	_00168_DS_
	MOVAI	0x08
	MOVRA	r0x1012
_00168_DS_
;	.line	161; "IR.c"	if (IRCodeRev1 == CIRCOLRA) {i = 9;}
	MOVAR	_IRCodeRev1
	XORAI	0x42
	JBSET	STATUS,2
	GOTO	_00170_DS_
	MOVAI	0x09
	MOVRA	r0x1012
_00170_DS_
;	.line	162; "IR.c"	if (IRCodeRev1 == CIRCOLRB) {i = 10;}
	MOVAR	_IRCodeRev1
	XORAI	0x52
	JBSET	STATUS,2
	GOTO	_00172_DS_
	MOVAI	0x0a
	MOVRA	r0x1012
_00172_DS_
;	.line	163; "IR.c"	if (IRCodeRev1 == CIRCOLRC) {i = 11;}
	MOVAR	_IRCodeRev1
	XORAI	0x4a
	JBSET	STATUS,2
	GOTO	_00174_DS_
	MOVAI	0x0b
	MOVRA	r0x1012
_00174_DS_
;	.line	165; "IR.c"	FMultiColor = 0;
	BCLR	_Flag2,4
;	.line	168; "IR.c"	TRed = RedTbl[i];
	MOVAR	r0x1012
	ADDAI	(_RedTbl + 0)
	MOVRA	r0x1013
	MOVAI	high (_RedTbl + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x1014
	MOVAR	r0x1013
	MOVRA	STK01
	MOVAR	r0x1014
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	_TRed
;	.line	169; "IR.c"	TGreen = GreenTbl[i];
	MOVAR	r0x1012
	ADDAI	(_GreenTbl + 0)
	MOVRA	r0x1013
	MOVAI	high (_GreenTbl + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x1014
	MOVAR	r0x1013
	MOVRA	STK01
	MOVAR	r0x1014
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	_TGreen
;	.line	170; "IR.c"	TBlue = BlueTbl[i];
	MOVAR	r0x1012
	ADDAI	(_BlueTbl + 0)
	MOVRA	r0x1012
	MOVAI	high (_BlueTbl + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x1013
	MOVAR	r0x1012
	MOVRA	STK01
	MOVAR	r0x1013
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	_TBlue
_00175_DS_
;	.line	172; "IR.c"	return;
	RETURN	
; exit point of _IRSbr4

;***
;  pBlock Stats: dbName = C
;***
;entry:  _IROffKeySbr	;Function start
; 2 exit points
;has an exit
;; Starting pCode block
_IROffKeySbr	;Function start
; 2 exit points
;	.line	83; "IR.c"	FOn = 0;
	BCLR	_Flag1,0
;	.line	84; "IR.c"	FStop = 1;
	BSET	_Flag1,7
;	.line	85; "IR.c"	TStop = 3;
	MOVAI	0x03
	MOVRA	_TStop
	RETURN	
; exit point of _IROffKeySbr

;***
;  pBlock Stats: dbName = C
;***
;entry:  _IRSbr	;Function start
; 2 exit points
;has an exit
;functions called:
;   _IRSbr4
;   _IRSbr4
;; Starting pCode block
_IRSbr	;Function start
; 2 exit points
;	.line	41; "IR.c"	if (!FLead)                                 //;未收到头码
	JBCLR	_Flag1,4
	GOTO	_00110_DS_
;;unsigned compare: left < lit(0x41=65), size=1
;	.line	43; "IR.c"	if (IRTmr < CHEAD-5) return;
	MOVAI	0x41
	RSUBAR	_IRTmr
	JBCLR	STATUS,0
	GOTO	_00106_DS_
;;genSkipc:3251: created from rifx:0028608C
	GOTO	_00117_DS_
;;swapping arguments (AOP_TYPEs 1/3)
;;unsigned compare: left >= lit(0x4C=76), size=1
_00106_DS_
;	.line	44; "IR.c"	if (IRTmr > CHEAD+5) return;
	MOVAI	0x4c
	RSUBAR	_IRTmr
	JBSET	STATUS,0
	GOTO	_00108_DS_
;;genSkipc:3251: created from rifx:0028608C
	GOTO	_00117_DS_
_00108_DS_
;	.line	45; "IR.c"	FLead = 1;                      //;当前接收到的是头码
	BSET	_Flag1,4
;	.line	46; "IR.c"	Custom = 0;
	CLRR	_Custom
;	.line	47; "IR.c"	CustomRev = 0;
	CLRR	_CustomRev
;	.line	48; "IR.c"	IRCode1 = 0;
	CLRR	_IRCode1
;	.line	49; "IR.c"	IRCodeRev1 = 0;
	CLRR	_IRCodeRev1
;	.line	50; "IR.c"	FIRAck = 0;
	BCLR	_Flag1,3
;	.line	51; "IR.c"	BitCnt = 32;
	MOVAI	0x20
	MOVRA	_BitCnt
;	.line	52; "IR.c"	return;
	GOTO	_00117_DS_
;;swapping arguments (AOP_TYPEs 1/3)
;;unsigned compare: left >= lit(0x29=41), size=1
_00110_DS_
;	.line	55; "IR.c"	if (IRTmr > CCodeR+5) {FLead = 0; return;}
	MOVAI	0x29
	RSUBAR	_IRTmr
	JBSET	STATUS,0
	GOTO	_00112_DS_
;;genSkipc:3251: created from rifx:0028608C
	BCLR	_Flag1,4
	GOTO	_00117_DS_
;;unsigned compare: left < lit(0x1E=30), size=1
_00112_DS_
;	.line	56; "IR.c"	if (IRTmr >= CCodeR-5) 
	MOVAI	0x1e
	RSUBAR	_IRTmr
	JBSET	STATUS,0
	GOTO	_00114_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	66; "IR.c"	return;
	GOTO	_00117_DS_
_00114_DS_
;	.line	77; "IR.c"	__endasm;
	movai 8 +(26 -8)/2
	rsubar _IRTmr
	rrr _IRCode1
	rrr _IRCodeRev1
	rrr _Custom
	rrr _CustomRev
	
;	.line	78; "IR.c"	if (--BitCnt == 0) IRSbr4();
	DECR	_BitCnt
	MOVAI	0x00
	ORAR	_BitCnt
	JBSET	STATUS,2
	GOTO	_00117_DS_
	CALL	_IRSbr4
_00117_DS_
	RETURN	
; exit point of _IRSbr


;	code size estimation:
;	  240+    0 =   240 instructions (  480 byte)

	end
