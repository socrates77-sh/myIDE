;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Aug  7 2015) (MINGW32)
; This file was generated Thu Jun 23 15:01:26 2016
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"T1sSbr.c"
	list	p=0311
	radix dec
	include "0311.inc"
;--------------------------------------------------------
; external declarations
;--------------------------------------------------------
	extern	_IROffKeySbr
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
	global	_T1sSbr

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

;@Allocation info for local variables in function 'T1sSbr'
;@T1sSbr IROffKeySbr               Allocated to registers ;size:2
;@T1sSbr T1sSbr                    Allocated to registers ;size:2
;@T1sSbr STATUSbits                Allocated to registers ;size:1
;@T1sSbr P0bits                    Allocated to registers ;size:1
;@T1sSbr P1bits                    Allocated to registers ;size:1
;@T1sSbr MCRbits                   Allocated to registers ;size:1
;@T1sSbr KBIMbits                  Allocated to registers ;size:1
;@T1sSbr PDCONbits                 Allocated to registers ;size:1
;@T1sSbr ODCONbits                 Allocated to registers ;size:1
;@T1sSbr PUCONbits                 Allocated to registers ;size:1
;@T1sSbr INTECONbits               Allocated to registers ;size:1
;@T1sSbr INTFLAGbits               Allocated to registers ;size:1
;@T1sSbr T0CRbits                  Allocated to registers ;size:1
;@T1sSbr DDR0bits                  Allocated to registers ;size:1
;@T1sSbr DDR1bits                  Allocated to registers ;size:1
;@T1sSbr TMCRbits                  Allocated to registers ;size:1
;@T1sSbr T1CRbits                  Allocated to registers ;size:1
;@T1sSbr ABuf                      Allocated to registers ;size:1
;@T1sSbr StatusBuf                 Allocated to registers ;size:1
;@T1sSbr T1s                       Allocated to registers ;size:1
;@T1sSbr TRedCnt                   Allocated to registers ;size:1
;@T1sSbr TRed                      Allocated to registers ;size:1
;@T1sSbr TGreen                    Allocated to registers ;size:1
;@T1sSbr TBlue                     Allocated to registers ;size:1
;@T1sSbr IRTmr                     Allocated to registers ;size:1
;@T1sSbr Custom                    Allocated to registers ;size:1
;@T1sSbr CustomRev                 Allocated to registers ;size:1
;@T1sSbr IRCode1                   Allocated to registers ;size:1
;@T1sSbr IRCodeRev1                Allocated to registers ;size:1
;@T1sSbr BitCnt                    Allocated to registers ;size:1
;@T1sSbr delay                     Allocated to registers ;size:1
;@T1sSbr Tmr                       Allocated to registers ;size:2
;@T1sSbr TStop                     Allocated to registers ;size:1
;@T1sSbr T40ms                     Allocated to registers ;size:1
;@T1sSbr ModeCnt                   Allocated to registers ;size:1
;@T1sSbr Step                      Allocated to registers ;size:1
;@T1sSbr delay05ms                 Allocated to registers ;size:1
;@T1sSbr Flag1                     Allocated to registers ;size:1
;@T1sSbr Flag2                     Allocated to registers ;size:1
;@T1sSbr INDF                      Allocated to registers ;size:1
;@T1sSbr T0CNT                     Allocated to registers ;size:1
;@T1sSbr PCL                       Allocated to registers ;size:1
;@T1sSbr STATUS                    Allocated to registers ;size:1
;@T1sSbr FSR                       Allocated to registers ;size:1
;@T1sSbr P0                        Allocated to registers ;size:1
;@T1sSbr P1                        Allocated to registers ;size:1
;@T1sSbr GPR                       Allocated to registers ;size:1
;@T1sSbr MCR                       Allocated to registers ;size:1
;@T1sSbr KBIM                      Allocated to registers ;size:1
;@T1sSbr PCLATH                    Allocated to registers ;size:1
;@T1sSbr PDCON                     Allocated to registers ;size:1
;@T1sSbr ODCON                     Allocated to registers ;size:1
;@T1sSbr PUCON                     Allocated to registers ;size:1
;@T1sSbr INTECON                   Allocated to registers ;size:1
;@T1sSbr INTFLAG                   Allocated to registers ;size:1
;@T1sSbr T0CR                      Allocated to registers ;size:1
;@T1sSbr DDR0                      Allocated to registers ;size:1
;@T1sSbr DDR1                      Allocated to registers ;size:1
;@T1sSbr TMCR                      Allocated to registers ;size:1
;@T1sSbr T1CR                      Allocated to registers ;size:1
;@T1sSbr T1CNT                     Allocated to registers ;size:1
;@T1sSbr T1LOAD                    Allocated to registers ;size:1
;@T1sSbr T1DATA                    Allocated to registers ;size:1
;@end Allocation info for local variables in function 'T1sSbr';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; code
;--------------------------------------------------------
code_T1sSbr	code
;***
;  pBlock Stats: dbName = C
;***
;entry:  _T1sSbr	;Function start
; 2 exit points
;has an exit
;functions called:
;   _IROffKeySbr
;   _IROffKeySbr
;   _IROffKeySbr
;   _IROffKeySbr
;; Starting pCode block
_T1sSbr	;Function start
; 2 exit points
;	.line	9; "T1sSbr.c"	if (--T1s != 0) return;
	DECR	_T1s
	MOVAI	0x00
	ORAR	_T1s
	JBCLR	STATUS,2
	GOTO	_00106_DS_
	GOTO	_00115_DS_
_00106_DS_
;	.line	10; "T1sSbr.c"	T1s = 250;
	MOVAI	0xfa
	MOVRA	_T1s
;	.line	12; "T1sSbr.c"	if (!FOn)
	JBCLR	_Flag1,0
	GOTO	_00110_DS_
;	.line	14; "T1sSbr.c"	if (--TStop == 0) IROffKeySbr();
	DECR	_TStop
	MOVAI	0x00
	ORAR	_TStop
	JBSET	STATUS,2
	GOTO	_00108_DS_
	CALL	_IROffKeySbr
_00108_DS_
;	.line	15; "T1sSbr.c"	return;
	GOTO	_00115_DS_
_00110_DS_
;	.line	17; "T1sSbr.c"	if (!FTmr) return;
	JBCLR	_Flag1,5
	GOTO	_00112_DS_
	GOTO	_00115_DS_
_00112_DS_
;	.line	18; "T1sSbr.c"	if (--Tmr == 0) IROffKeySbr();
	MOVAI	0xff
	ADDRA	_Tmr
	JBSET	STATUS,0
	DECR	(_Tmr + 1)
	MOVAR	_Tmr
	ORAR	(_Tmr + 1)
	JBSET	STATUS,2
	GOTO	_00115_DS_
	CALL	_IROffKeySbr
_00115_DS_
	RETURN	
; exit point of _T1sSbr


;	code size estimation:
;	   30+    0 =    30 instructions (   60 byte)

	end
