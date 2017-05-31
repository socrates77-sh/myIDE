;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Mar  2 2015) (MINGW32)
; This file was generated Fri Apr 17 17:29:38 2015
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"main.c"
	list	p=3111
	radix dec
	include "3111.inc"
;--------------------------------------------------------
; external declarations
;--------------------------------------------------------
	extern	_STATUSbits
	extern	_MCRbits
	extern	_INTECONbits
	extern	_INTFLAGbits
	extern	_IOP0bits
	extern	_OEP0bits
	extern	_PUP0bits
	extern	_DKWP0bits
	extern	_IOP1bits
	extern	_OEP1bits
	extern	_PUP1bits
	extern	_DKWP1bits
	extern	_DKWbits
	extern	_INDF
	extern	_FSR
	extern	_PCL
	extern	_STATUS
	extern	_MCR
	extern	_INTECON
	extern	_INTFLAG
	extern	_IOP0
	extern	_OEP0
	extern	_PUP0
	extern	_DKWP0
	extern	_IOP1
	extern	_OEP1
	extern	_PUP1
	extern	_DKWP1
	extern	_DKW
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
;--------------------------------------------------------
; initialized data
;--------------------------------------------------------

;@Allocation info for local variables in function 'main'
;@main main                      Allocated to registers ;size:2
;@main STATUSbits                Allocated to registers ;size:1
;@main MCRbits                   Allocated to registers ;size:1
;@main INTECONbits               Allocated to registers ;size:1
;@main INTFLAGbits               Allocated to registers ;size:1
;@main IOP0bits                  Allocated to registers ;size:1
;@main OEP0bits                  Allocated to registers ;size:1
;@main PUP0bits                  Allocated to registers ;size:1
;@main DKWP0bits                 Allocated to registers ;size:1
;@main IOP1bits                  Allocated to registers ;size:1
;@main OEP1bits                  Allocated to registers ;size:1
;@main PUP1bits                  Allocated to registers ;size:1
;@main DKWP1bits                 Allocated to registers ;size:1
;@main DKWbits                   Allocated to registers ;size:1
;@main INDF                      Allocated to registers ;size:1
;@main FSR                       Allocated to registers ;size:1
;@main PCL                       Allocated to registers ;size:1
;@main STATUS                    Allocated to registers ;size:1
;@main MCR                       Allocated to registers ;size:1
;@main INTECON                   Allocated to registers ;size:1
;@main INTFLAG                   Allocated to registers ;size:1
;@main IOP0                      Allocated to registers ;size:1
;@main OEP0                      Allocated to registers ;size:1
;@main PUP0                      Allocated to registers ;size:1
;@main DKWP0                     Allocated to registers ;size:1
;@main IOP1                      Allocated to registers ;size:1
;@main OEP1                      Allocated to registers ;size:1
;@main PUP1                      Allocated to registers ;size:1
;@main DKWP1                     Allocated to registers ;size:1
;@main DKW                       Allocated to registers ;size:1
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
code_main	code
;***
;  pBlock Stats: dbName = M
;***
;entry:  _main	;Function start
; 2 exit points
;has an exit
;; Starting pCode block
_main	;Function start
; 2 exit points
;	.line	11; "main.c"	GIE=0;
	BCLR	_MCRbits,7
;	.line	12; "main.c"	IOP0=0XFF;
	MOVAI	0xff
	MOVRA	_IOP0
;	.line	13; "main.c"	IOP1=0XFF;
	MOVAI	0xff
	MOVRA	_IOP1
;	.line	14; "main.c"	OEP0=0XFF;
	MOVAI	0xff
	MOVRA	_OEP0
;	.line	15; "main.c"	OEP1=0XFF;
	MOVAI	0xff
	MOVRA	_OEP1
;	.line	16; "main.c"	OEP01 = 1;
	BSET	_OEP0bits,1
_00106_DS_
;	.line	21; "main.c"	IOP1=~IOP1;       
	COMAR	_IOP1
	MOVRA	_IOP1
;	.line	22; "main.c"	OEP0=0;
	CLRR	_OEP0
	GOTO	_00106_DS_
	RETURN	
; exit point of _main


;	code size estimation:
;	   15+    0 =    15 instructions (   30 byte)

	end
