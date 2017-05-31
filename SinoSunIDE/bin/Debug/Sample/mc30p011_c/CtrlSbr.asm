;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Aug  7 2015) (MINGW32)
; This file was generated Thu Jun 23 15:01:24 2016
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"CtrlSbr.c"
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
	global	_MColorSbr
	global	_Mode2Sbr

;--------------------------------------------------------
; global definitions
;--------------------------------------------------------
;--------------------------------------------------------
; absolute symbol definitions
;--------------------------------------------------------
;--------------------------------------------------------
; compiler-defined variables
;--------------------------------------------------------
;--------------------------------------------------------
; initialized data
;--------------------------------------------------------

;@Allocation info for local variables in function 'MColorSbr'
;@MColorSbr MColorSbr                 Allocated to registers ;size:2
;@MColorSbr STATUSbits                Allocated to registers ;size:1
;@MColorSbr P0bits                    Allocated to registers ;size:1
;@MColorSbr P1bits                    Allocated to registers ;size:1
;@MColorSbr MCRbits                   Allocated to registers ;size:1
;@MColorSbr KBIMbits                  Allocated to registers ;size:1
;@MColorSbr PDCONbits                 Allocated to registers ;size:1
;@MColorSbr ODCONbits                 Allocated to registers ;size:1
;@MColorSbr PUCONbits                 Allocated to registers ;size:1
;@MColorSbr INTECONbits               Allocated to registers ;size:1
;@MColorSbr INTFLAGbits               Allocated to registers ;size:1
;@MColorSbr T0CRbits                  Allocated to registers ;size:1
;@MColorSbr DDR0bits                  Allocated to registers ;size:1
;@MColorSbr DDR1bits                  Allocated to registers ;size:1
;@MColorSbr TMCRbits                  Allocated to registers ;size:1
;@MColorSbr T1CRbits                  Allocated to registers ;size:1
;@MColorSbr ABuf                      Allocated to registers ;size:1
;@MColorSbr StatusBuf                 Allocated to registers ;size:1
;@MColorSbr T1s                       Allocated to registers ;size:1
;@MColorSbr TRedCnt                   Allocated to registers ;size:1
;@MColorSbr TRed                      Allocated to registers ;size:1
;@MColorSbr TGreen                    Allocated to registers ;size:1
;@MColorSbr TBlue                     Allocated to registers ;size:1
;@MColorSbr IRTmr                     Allocated to registers ;size:1
;@MColorSbr Custom                    Allocated to registers ;size:1
;@MColorSbr CustomRev                 Allocated to registers ;size:1
;@MColorSbr IRCode1                   Allocated to registers ;size:1
;@MColorSbr IRCodeRev1                Allocated to registers ;size:1
;@MColorSbr BitCnt                    Allocated to registers ;size:1
;@MColorSbr delay                     Allocated to registers ;size:1
;@MColorSbr Tmr                       Allocated to registers ;size:2
;@MColorSbr TStop                     Allocated to registers ;size:1
;@MColorSbr T40ms                     Allocated to registers ;size:1
;@MColorSbr ModeCnt                   Allocated to registers ;size:1
;@MColorSbr Step                      Allocated to registers ;size:1
;@MColorSbr delay05ms                 Allocated to registers ;size:1
;@MColorSbr Flag1                     Allocated to registers ;size:1
;@MColorSbr Flag2                     Allocated to registers ;size:1
;@MColorSbr INDF                      Allocated to registers ;size:1
;@MColorSbr T0CNT                     Allocated to registers ;size:1
;@MColorSbr PCL                       Allocated to registers ;size:1
;@MColorSbr STATUS                    Allocated to registers ;size:1
;@MColorSbr FSR                       Allocated to registers ;size:1
;@MColorSbr P0                        Allocated to registers ;size:1
;@MColorSbr P1                        Allocated to registers ;size:1
;@MColorSbr GPR                       Allocated to registers ;size:1
;@MColorSbr MCR                       Allocated to registers ;size:1
;@MColorSbr KBIM                      Allocated to registers ;size:1
;@MColorSbr PCLATH                    Allocated to registers ;size:1
;@MColorSbr PDCON                     Allocated to registers ;size:1
;@MColorSbr ODCON                     Allocated to registers ;size:1
;@MColorSbr PUCON                     Allocated to registers ;size:1
;@MColorSbr INTECON                   Allocated to registers ;size:1
;@MColorSbr INTFLAG                   Allocated to registers ;size:1
;@MColorSbr T0CR                      Allocated to registers ;size:1
;@MColorSbr DDR0                      Allocated to registers ;size:1
;@MColorSbr DDR1                      Allocated to registers ;size:1
;@MColorSbr TMCR                      Allocated to registers ;size:1
;@MColorSbr T1CR                      Allocated to registers ;size:1
;@MColorSbr T1CNT                     Allocated to registers ;size:1
;@MColorSbr T1LOAD                    Allocated to registers ;size:1
;@MColorSbr T1DATA                    Allocated to registers ;size:1
;@end Allocation info for local variables in function 'MColorSbr';
;@Allocation info for local variables in function 'Mode2Sbr'
;@end Allocation info for local variables in function 'Mode2Sbr';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; code
;--------------------------------------------------------
code_CtrlSbr	code
;***
;  pBlock Stats: dbName = C
;***
;entry:  _Mode2Sbr	;Function start
; 2 exit points
;has an exit
;; Starting pCode block
_Mode2Sbr	;Function start
; 2 exit points
;	.line	66; "CtrlSbr.c"	if (ModeCnt >= Step)
	MOVAR	_Step
	RSUBAR	_ModeCnt
	JBSET	STATUS,0
	GOTO	_00131_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	68; "CtrlSbr.c"	pBlueC = 1;
	BSET	_DDR1bits,2
;	.line	69; "CtrlSbr.c"	pRedC = 1;
	BSET	_DDR1bits,0
;	.line	70; "CtrlSbr.c"	pGreenC = 1;
	BSET	_DDR1bits,1
;;swapping arguments (AOP_TYPEs 1/3)
;;unsigned compare: left >= lit(0x33=51), size=1
;	.line	71; "CtrlSbr.c"	if (ModeCnt > 50) ModeCnt = 0;
	MOVAI	0x33
	RSUBAR	_ModeCnt
	JBSET	STATUS,0
	GOTO	_00133_DS_
;;genSkipc:3251: created from rifx:0028608C
	CLRR	_ModeCnt
	GOTO	_00133_DS_
_00131_DS_
;	.line	75; "CtrlSbr.c"	pBlueC = 0;
	BCLR	_DDR1bits,2
;	.line	76; "CtrlSbr.c"	pRedC = 0;
	BCLR	_DDR1bits,0
;	.line	77; "CtrlSbr.c"	pGreenC = 0;
	BCLR	_DDR1bits,1
_00133_DS_
	RETURN	
; exit point of _Mode2Sbr

;***
;  pBlock Stats: dbName = C
;***
;entry:  _MColorSbr	;Function start
; 2 exit points
;has an exit
;; Starting pCode block
_MColorSbr	;Function start
; 2 exit points
;	.line	8; "CtrlSbr.c"	if (++delay >= 4) 
	INCR	_delay
;;unsigned compare: left < lit(0x4=4), size=1
	MOVAI	0x04
	RSUBAR	_delay
	JBSET	STATUS,0
	GOTO	_00123_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	10; "CtrlSbr.c"	delay = 0;
	CLRR	_delay
;	.line	11; "CtrlSbr.c"	if (--ModeCnt == 0)
	DECR	_ModeCnt
	MOVAI	0x00
	ORAR	_ModeCnt
	JBSET	STATUS,2
	GOTO	_00108_DS_
;	.line	13; "CtrlSbr.c"	TGreen = TColor;
	MOVAI	0x7f
	MOVRA	_TGreen
;	.line	14; "CtrlSbr.c"	TRed = TColor;
	MOVAI	0x7f
	MOVRA	_TRed
;	.line	15; "CtrlSbr.c"	TBlue = TColor;
	MOVAI	0x7f
	MOVRA	_TBlue
;	.line	21; "CtrlSbr.c"	if (++Step > 5) Step = 0;
	INCR	_Step
;;swapping arguments (AOP_TYPEs 1/3)
;;unsigned compare: left >= lit(0x6=6), size=1
	MOVAI	0x06
	RSUBAR	_Step
	JBSET	STATUS,0
	GOTO	_00106_DS_
;;genSkipc:3251: created from rifx:0028608C
	CLRR	_Step
_00106_DS_
;	.line	22; "CtrlSbr.c"	ModeCnt = TSteplengh;
	MOVAI	0x7f
	MOVRA	_ModeCnt
_00108_DS_
;	.line	24; "CtrlSbr.c"	if (Step == 0) {++TGreen;--TBlue;return;}
	MOVAI	0x00
	ORAR	_Step
	JBSET	STATUS,2
	GOTO	_00110_DS_
	INCR	_TGreen
	DECR	_TBlue
	GOTO	_00123_DS_
_00110_DS_
;	.line	25; "CtrlSbr.c"	if (Step == 1) {--TGreen;++TBlue;return;}
	MOVAR	_Step
	XORAI	0x01
	JBSET	STATUS,2
	GOTO	_00112_DS_
	DECR	_TGreen
	INCR	_TBlue
	GOTO	_00123_DS_
_00112_DS_
;	.line	26; "CtrlSbr.c"	if (Step == 2) {++TRed;--TBlue;return;}
	MOVAR	_Step
	XORAI	0x02
	JBSET	STATUS,2
	GOTO	_00114_DS_
	INCR	_TRed
	DECR	_TBlue
	GOTO	_00123_DS_
_00114_DS_
;	.line	27; "CtrlSbr.c"	if (Step == 3) {--TRed;++TBlue;return;}
	MOVAR	_Step
	XORAI	0x03
	JBSET	STATUS,2
	GOTO	_00116_DS_
	DECR	_TRed
	INCR	_TBlue
	GOTO	_00123_DS_
_00116_DS_
;	.line	28; "CtrlSbr.c"	if (Step == 4) {++TRed;--TGreen;return;}
	MOVAR	_Step
	XORAI	0x04
	JBSET	STATUS,2
	GOTO	_00118_DS_
	INCR	_TRed
	DECR	_TGreen
	GOTO	_00123_DS_
_00118_DS_
;	.line	29; "CtrlSbr.c"	if (Step == 5) {--TRed;++TGreen;return;}
	MOVAR	_Step
	XORAI	0x05
	JBSET	STATUS,2
	GOTO	_00123_DS_
	DECR	_TRed
	INCR	_TGreen
_00123_DS_
	RETURN	
; exit point of _MColorSbr


;	code size estimation:
;	   84+    0 =    84 instructions (  168 byte)

	end
