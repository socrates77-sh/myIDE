;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Aug  7 2015) (MINGW32)
; This file was generated Thu Jun 23 15:01:25 2016
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"interrupt.c"
	list	p=0311
	radix dec
	include "0311.inc"
;--------------------------------------------------------
; external declarations
;--------------------------------------------------------
	extern	_IROffKeySbr
	extern	_IRSbr
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
	global	_int_isr

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

;@Allocation info for local variables in function 'int_isr'
;@int_isr IROffKeySbr               Allocated to registers ;size:2
;@int_isr IRSbr                     Allocated to registers ;size:2
;@int_isr int_isr                   Allocated to registers ;size:2
;@int_isr STATUSbits                Allocated to registers ;size:1
;@int_isr P0bits                    Allocated to registers ;size:1
;@int_isr P1bits                    Allocated to registers ;size:1
;@int_isr MCRbits                   Allocated to registers ;size:1
;@int_isr KBIMbits                  Allocated to registers ;size:1
;@int_isr PDCONbits                 Allocated to registers ;size:1
;@int_isr ODCONbits                 Allocated to registers ;size:1
;@int_isr PUCONbits                 Allocated to registers ;size:1
;@int_isr INTECONbits               Allocated to registers ;size:1
;@int_isr INTFLAGbits               Allocated to registers ;size:1
;@int_isr T0CRbits                  Allocated to registers ;size:1
;@int_isr DDR0bits                  Allocated to registers ;size:1
;@int_isr DDR1bits                  Allocated to registers ;size:1
;@int_isr TMCRbits                  Allocated to registers ;size:1
;@int_isr T1CRbits                  Allocated to registers ;size:1
;@int_isr ABuf                      Allocated to registers ;size:1
;@int_isr StatusBuf                 Allocated to registers ;size:1
;@int_isr T1s                       Allocated to registers ;size:1
;@int_isr TRedCnt                   Allocated to registers ;size:1
;@int_isr TRed                      Allocated to registers ;size:1
;@int_isr TGreen                    Allocated to registers ;size:1
;@int_isr TBlue                     Allocated to registers ;size:1
;@int_isr IRTmr                     Allocated to registers ;size:1
;@int_isr Custom                    Allocated to registers ;size:1
;@int_isr CustomRev                 Allocated to registers ;size:1
;@int_isr IRCode1                   Allocated to registers ;size:1
;@int_isr IRCodeRev1                Allocated to registers ;size:1
;@int_isr BitCnt                    Allocated to registers ;size:1
;@int_isr delay                     Allocated to registers ;size:1
;@int_isr Tmr                       Allocated to registers ;size:2
;@int_isr TStop                     Allocated to registers ;size:1
;@int_isr T40ms                     Allocated to registers ;size:1
;@int_isr ModeCnt                   Allocated to registers ;size:1
;@int_isr Step                      Allocated to registers ;size:1
;@int_isr delay05ms                 Allocated to registers ;size:1
;@int_isr Flag1                     Allocated to registers ;size:1
;@int_isr Flag2                     Allocated to registers ;size:1
;@int_isr INDF                      Allocated to registers ;size:1
;@int_isr T0CNT                     Allocated to registers ;size:1
;@int_isr PCL                       Allocated to registers ;size:1
;@int_isr STATUS                    Allocated to registers ;size:1
;@int_isr FSR                       Allocated to registers ;size:1
;@int_isr P0                        Allocated to registers ;size:1
;@int_isr P1                        Allocated to registers ;size:1
;@int_isr GPR                       Allocated to registers ;size:1
;@int_isr MCR                       Allocated to registers ;size:1
;@int_isr KBIM                      Allocated to registers ;size:1
;@int_isr PCLATH                    Allocated to registers ;size:1
;@int_isr PDCON                     Allocated to registers ;size:1
;@int_isr ODCON                     Allocated to registers ;size:1
;@int_isr PUCON                     Allocated to registers ;size:1
;@int_isr INTECON                   Allocated to registers ;size:1
;@int_isr INTFLAG                   Allocated to registers ;size:1
;@int_isr T0CR                      Allocated to registers ;size:1
;@int_isr DDR0                      Allocated to registers ;size:1
;@int_isr DDR1                      Allocated to registers ;size:1
;@int_isr TMCR                      Allocated to registers ;size:1
;@int_isr T1CR                      Allocated to registers ;size:1
;@int_isr T1CNT                     Allocated to registers ;size:1
;@int_isr T1LOAD                    Allocated to registers ;size:1
;@int_isr T1DATA                    Allocated to registers ;size:1
;@end Allocation info for local variables in function 'int_isr';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; interrupt and initialization code
;--------------------------------------------------------
c_interrupt	code	0x8
__sdcc_interrupt
;***
;  pBlock Stats: dbName = I
;***
;entry:  _int_isr	;Function start
; 0 exit points
;functions called:
;   _IRSbr
;   _IRSbr
;; Starting pCode block
_int_isr	;Function start
; 0 exit points
;	.line	13; "interrupt.c"	__endasm;
	movra _ABuf
	swapar _STATUS
	movra _StatusBuf
	
;	.line	15; "interrupt.c"	if (KBIF)
	JBSET	_INTFLAGbits,1
	GOTO	_00108_DS_
;	.line	17; "interrupt.c"	KBIF = 1;
	BSET	_INTFLAGbits,1
;	.line	18; "interrupt.c"	if (!pIR)
	JBCLR	_P1bits,3
	GOTO	_00109_DS_
;	.line	20; "interrupt.c"	FStop = 0;
	BCLR	_Flag1,7
;	.line	21; "interrupt.c"	IRSbr();
	CALL	_IRSbr
	GOTO	_00109_DS_
_00108_DS_
;	.line	24; "interrupt.c"	else IRTmr = 0;
	CLRR	_IRTmr
_00109_DS_
;	.line	26; "interrupt.c"	if (T0IF)
	JBSET	_INTFLAGbits,0
	GOTO	_00123_DS_
;	.line	28; "interrupt.c"	T0IF = 0;
	BCLR	_INTFLAGbits,0
;	.line	29; "interrupt.c"	T0CNT = CT0;
	MOVAI	0xf0
	MOVRA	_T0CNT
;	.line	30; "interrupt.c"	++ModeCnt;
	INCR	_ModeCnt
;	.line	31; "interrupt.c"	++TRedCnt;
	INCR	_TRedCnt
;	.line	32; "interrupt.c"	if (TRedCnt >= TRed) pRed = 0;
	MOVAR	_TRed
	RSUBAR	_TRedCnt
	JBSET	STATUS,0
	GOTO	_00111_DS_
;;genSkipc:3251: created from rifx:0028608C
	BCLR	_P1bits,0
_00111_DS_
;	.line	33; "interrupt.c"	if (TRedCnt >= TBlue) pBlue = 0;
	MOVAR	_TBlue
	RSUBAR	_TRedCnt
	JBSET	STATUS,0
	GOTO	_00113_DS_
;;genSkipc:3251: created from rifx:0028608C
	BCLR	_P1bits,2
_00113_DS_
;	.line	34; "interrupt.c"	if (TRedCnt >= TGreen) pGreen = 0;
	MOVAR	_TGreen
	RSUBAR	_TRedCnt
	JBSET	STATUS,0
	GOTO	_00115_DS_
;;genSkipc:3251: created from rifx:0028608C
	BCLR	_P1bits,1
;;unsigned compare: left < lit(0xF2=242), size=1
_00115_DS_
;	.line	35; "interrupt.c"	if (TRedCnt >= 242) 
	MOVAI	0xf2
	RSUBAR	_TRedCnt
	JBSET	STATUS,0
	GOTO	_00117_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	37; "interrupt.c"	TRedCnt = 0;
	CLRR	_TRedCnt
;	.line	38; "interrupt.c"	pBlue = 1;
	BSET	_P1bits,2
;	.line	39; "interrupt.c"	pGreen = 1;
	BSET	_P1bits,1
;	.line	40; "interrupt.c"	pRed = 1;
	BSET	_P1bits,0
_00117_DS_
;	.line	46; "interrupt.c"	__endasm;
	movai 0x04
	xorra _Flag1
	
;	.line	47; "interrupt.c"	if (FDouble)
	JBSET	_Flag1,2
	GOTO	_00123_DS_
;	.line	49; "interrupt.c"	if (++IRTmr >= 254) --IRTmr;
	INCR	_IRTmr
;;unsigned compare: left < lit(0xFE=254), size=1
	MOVAI	0xfe
	RSUBAR	_IRTmr
	JBSET	STATUS,0
	GOTO	_00123_DS_
;;genSkipc:3251: created from rifx:0028608C
	DECR	_IRTmr
_00123_DS_
;	.line	53; "interrupt.c"	if (T1IF) {T1IF = 0;F4ms = 1;}
	JBSET	_TMCRbits,0
	GOTO	_00125_DS_
	BCLR	_TMCRbits,0
	BSET	_Flag2,2
_00125_DS_
;	.line	59; "interrupt.c"	__endasm;
	swapar _StatusBuf
	movra _STATUS
	swapr _ABuf
	swapar _ABuf
	
END_OF_INTERRUPT
	RETIE	

;--------------------------------------------------------
; code
;--------------------------------------------------------
code_interrupt	code

;	code size estimation:
;	   52+    0 =    52 instructions (  104 byte)

	end
