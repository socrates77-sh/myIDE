;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Aug  7 2015) (MINGW32)
; This file was generated Thu Jun 23 15:01:25 2016
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"initSys.c"
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
	global	_InitialSys
	global	_InitalRAM

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

;@Allocation info for local variables in function 'InitialSys'
;@InitialSys InitialSys                Allocated to registers ;size:2
;@InitialSys STATUSbits                Allocated to registers ;size:1
;@InitialSys P0bits                    Allocated to registers ;size:1
;@InitialSys P1bits                    Allocated to registers ;size:1
;@InitialSys MCRbits                   Allocated to registers ;size:1
;@InitialSys KBIMbits                  Allocated to registers ;size:1
;@InitialSys PDCONbits                 Allocated to registers ;size:1
;@InitialSys ODCONbits                 Allocated to registers ;size:1
;@InitialSys PUCONbits                 Allocated to registers ;size:1
;@InitialSys INTECONbits               Allocated to registers ;size:1
;@InitialSys INTFLAGbits               Allocated to registers ;size:1
;@InitialSys T0CRbits                  Allocated to registers ;size:1
;@InitialSys DDR0bits                  Allocated to registers ;size:1
;@InitialSys DDR1bits                  Allocated to registers ;size:1
;@InitialSys TMCRbits                  Allocated to registers ;size:1
;@InitialSys T1CRbits                  Allocated to registers ;size:1
;@InitialSys ABuf                      Allocated to registers ;size:1
;@InitialSys StatusBuf                 Allocated to registers ;size:1
;@InitialSys T1s                       Allocated to registers ;size:1
;@InitialSys TRedCnt                   Allocated to registers ;size:1
;@InitialSys TRed                      Allocated to registers ;size:1
;@InitialSys TGreen                    Allocated to registers ;size:1
;@InitialSys TBlue                     Allocated to registers ;size:1
;@InitialSys IRTmr                     Allocated to registers ;size:1
;@InitialSys Custom                    Allocated to registers ;size:1
;@InitialSys CustomRev                 Allocated to registers ;size:1
;@InitialSys IRCode1                   Allocated to registers ;size:1
;@InitialSys IRCodeRev1                Allocated to registers ;size:1
;@InitialSys BitCnt                    Allocated to registers ;size:1
;@InitialSys delay                     Allocated to registers ;size:1
;@InitialSys Tmr                       Allocated to registers ;size:2
;@InitialSys TStop                     Allocated to registers ;size:1
;@InitialSys T40ms                     Allocated to registers ;size:1
;@InitialSys ModeCnt                   Allocated to registers ;size:1
;@InitialSys Step                      Allocated to registers ;size:1
;@InitialSys delay05ms                 Allocated to registers ;size:1
;@InitialSys Flag1                     Allocated to registers ;size:1
;@InitialSys Flag2                     Allocated to registers ;size:1
;@InitialSys INDF                      Allocated to registers ;size:1
;@InitialSys T0CNT                     Allocated to registers ;size:1
;@InitialSys PCL                       Allocated to registers ;size:1
;@InitialSys STATUS                    Allocated to registers ;size:1
;@InitialSys FSR                       Allocated to registers ;size:1
;@InitialSys P0                        Allocated to registers ;size:1
;@InitialSys P1                        Allocated to registers ;size:1
;@InitialSys GPR                       Allocated to registers ;size:1
;@InitialSys MCR                       Allocated to registers ;size:1
;@InitialSys KBIM                      Allocated to registers ;size:1
;@InitialSys PCLATH                    Allocated to registers ;size:1
;@InitialSys PDCON                     Allocated to registers ;size:1
;@InitialSys ODCON                     Allocated to registers ;size:1
;@InitialSys PUCON                     Allocated to registers ;size:1
;@InitialSys INTECON                   Allocated to registers ;size:1
;@InitialSys INTFLAG                   Allocated to registers ;size:1
;@InitialSys T0CR                      Allocated to registers ;size:1
;@InitialSys DDR0                      Allocated to registers ;size:1
;@InitialSys DDR1                      Allocated to registers ;size:1
;@InitialSys TMCR                      Allocated to registers ;size:1
;@InitialSys T1CR                      Allocated to registers ;size:1
;@InitialSys T1CNT                     Allocated to registers ;size:1
;@InitialSys T1LOAD                    Allocated to registers ;size:1
;@InitialSys T1DATA                    Allocated to registers ;size:1
;@end Allocation info for local variables in function 'InitialSys';
;@Allocation info for local variables in function 'InitalRAM'
;@end Allocation info for local variables in function 'InitalRAM';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; code
;--------------------------------------------------------
code_initSys	code
;***
;  pBlock Stats: dbName = C
;***
;entry:  _InitalRAM	;Function start
; 2 exit points
;has an exit
;; Starting pCode block
_InitalRAM	;Function start
; 2 exit points
;	.line	56; "initSys.c"	__endasm;
	movai 0x15
	movra FSR
InitalRAM0:
	clrr INDF
	incr FSR
	movai 0xc0
	rsubar FSR
	jbset STATUS,0
	goto InitalRAM0
	
;	.line	58; "initSys.c"	T1s = 250;
	MOVAI	0xfa
	MOVRA	_T1s
	RETURN	
; exit point of _InitalRAM

;***
;  pBlock Stats: dbName = C
;***
;entry:  _InitialSys	;Function start
; 2 exit points
;has an exit
;; Starting pCode block
_InitialSys	;Function start
; 2 exit points
;	.line	10; "initSys.c"	pRed = 1;
	BSET	_P1bits,0
;	.line	11; "initSys.c"	pGreen = 1;
	BSET	_P1bits,1
;	.line	12; "initSys.c"	pBlue = 1;
	BSET	_P1bits,2
;	.line	14; "initSys.c"	DDR0 = 0;
	CLRR	_DDR0
;	.line	15; "initSys.c"	DDR1 = 0;
	CLRR	_DDR1
;	.line	16; "initSys.c"	DDR1 |= 0x08;
	BSET	_DDR1,3
;	.line	17; "initSys.c"	PDCON = 0xFF;                    
	MOVAI	0xff
	MOVRA	_PDCON
;	.line	18; "initSys.c"	PUCON = 0xFF;
	MOVAI	0xff
	MOVRA	_PUCON
;	.line	19; "initSys.c"	ODCON = 0;
	CLRR	_ODCON
;	.line	23; "initSys.c"	T0CR = 1;
	MOVAI	0x01
	MOVRA	_T0CR
;	.line	24; "initSys.c"	T0CNT = CT0;
	MOVAI	0xf0
	MOVRA	_T0CNT
;	.line	25; "initSys.c"	T0IE = 1;  	   	//;开定时中断
	BSET	_INTECONbits,0
;	.line	31; "initSys.c"	TMCR = 0;
	CLRR	_TMCR
;	.line	32; "initSys.c"	T1CR = 0x86;
	MOVAI	0x86
	MOVRA	_T1CR
;	.line	33; "initSys.c"	T1LOAD = 249;
	MOVAI	0xf9
	MOVRA	_T1LOAD
;	.line	34; "initSys.c"	T1DATA = 0;
	CLRR	_T1DATA
;	.line	37; "initSys.c"	MCR = 0x80;                    
	MOVAI	0x80
	MOVRA	_MCR
;	.line	39; "initSys.c"	KBIM = 0;                      
	CLRR	_KBIM
	RETURN	
; exit point of _InitialSys


;	code size estimation:
;	   29+    0 =    29 instructions (   58 byte)

	end
