;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Jan 13 2014) (MSVC)
; This file was generated Thu Mar 06 14:24:43 2014
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"main.c"
	list	p=30p011
	radix dec
	include "mc30p011.inc"
;--------------------------------------------------------
; external declarations
;--------------------------------------------------------
	extern	_initial
	extern	_abc
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
	extern	_temp2
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
	extern	__sdcc_gsinit_startup
;--------------------------------------------------------
; global declarations
;--------------------------------------------------------
	global	_main

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
;--------------------------------------------------------
; absolute symbol definitions
;--------------------------------------------------------
;--------------------------------------------------------
; compiler-defined variables
;--------------------------------------------------------
UDL_main_0	udata
r0x1002	res	1
_main_t1_1_2	res	1
;--------------------------------------------------------
; initialized data
;--------------------------------------------------------

;@Allocation info for local variables in function 'main'
;@main t1                        Allocated to registers 
;@end Allocation info for local variables in function 'main';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; reset vector 
;--------------------------------------------------------
STARTUP	code 0x0000
	goto	__sdcc_gsinit_startup
;--------------------------------------------------------
; code
;--------------------------------------------------------
code_main	code
;***
;  pBlock Stats: dbName = M
;***
;entry:  _main	;Function start
; 2 exit points
;has an exit
;functions called:
;   _initial
;   _abc
;   _initial
;   _abc
;2 compiler assigned registers:
;   r0x1002
;   STK00
;; Starting pCode block
_main	;Function start
; 2 exit points
;	.line	12; "main.c"	GIE=0;
	BCLR	_INTECONbits,7
	clrwdt
;	.line	14; "main.c"	DDR0=0Xaa;
	MOVAI	0xaa
	MOVRA	_DDR0
;	.line	16; "main.c"	P0=0;
	CLRR	_P0
;	.line	17; "main.c"	P1=1;
	MOVAI	0x01
	MOVRA	_P1
;	.line	18; "main.c"	T0CNT=0;
	CLRR	_T0CNT
;	.line	19; "main.c"	initial();
	CALL	_initial
;	.line	20; "main.c"	while(1)
	MOVAR	_main_t1_1_2
	MOVRA	r0x1002
_00106_DS_
;	.line	22; "main.c"	P0 =~P0;
	COMAR	_P0
	MOVRA	_P0
;	.line	23; "main.c"	t1++;
	INCR	r0x1002
;	.line	24; "main.c"	temp2++;
	INCR	_temp2
;	.line	25; "main.c"	abc(t1,temp2);    
	MOVAR	_temp2
	MOVRA	STK00
	MOVAR	r0x1002
	CALL	_abc
	GOTO	_00106_DS_
	RETURN	
; exit point of _main


;	code size estimation:
;	   20+    0 =    20 instructions (   40 byte)

	end
