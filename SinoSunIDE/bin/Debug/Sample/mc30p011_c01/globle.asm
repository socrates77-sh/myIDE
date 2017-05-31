;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Jan 13 2014) (MSVC)
; This file was generated Thu Mar 06 14:24:44 2014
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"globle.c"
	list	p=30p011
	radix dec
	include "mc30p011.inc"
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
	global	_abc
	global	_initial
	global	_temp2

;--------------------------------------------------------
; global definitions
;--------------------------------------------------------
UD_globle_0	udata
_temp2	res	1

;--------------------------------------------------------
; absolute symbol definitions
;--------------------------------------------------------
;--------------------------------------------------------
; compiler-defined variables
;--------------------------------------------------------
UDL_globle_0	udata
r0x1004	res	1
r0x1000	res	1
r0x1001	res	1
r0x1002	res	1
r0x1003	res	1
;--------------------------------------------------------
; initialized data
;--------------------------------------------------------

;@Allocation info for local variables in function 'initial'
;@initial i                         Allocated to registers 
;@initial j                         Allocated to registers r0x1004 
;@end Allocation info for local variables in function 'initial';
;@Allocation info for local variables in function 'abc'
;@abc b                         Allocated to registers r0x1001 
;@abc a                         Allocated to registers r0x1000 
;@abc i                         Allocated to registers r0x1002 
;@abc j                         Allocated to registers r0x1003 
;@abc k                         Allocated to registers r0x1002 
;@abc l                         Allocated to registers r0x1000 
;@abc m                         Allocated to registers r0x1002 
;@end Allocation info for local variables in function 'abc';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; code
;--------------------------------------------------------
code_globle	code
;***
;  pBlock Stats: dbName = C
;***
;entry:  _abc	;Function start
; 2 exit points
;has an exit
;5 compiler assigned registers:
;   r0x1000
;   STK00
;   r0x1001
;   r0x1002
;   r0x1003
;; Starting pCode block
_abc	;Function start
; 2 exit points
;	.line	22; "globle.c"	void  abc(uchar a,uchar b)
	MOVRA	r0x1000
	MOVAR	STK00
	MOVRA	r0x1001
;	.line	26; "globle.c"	i=a+2;
	MOVAI	0x02
	ADDAR	r0x1000
	MOVRA	r0x1002
;	.line	27; "globle.c"	j=a+8;
	MOVAI	0x08
	ADDAR	r0x1000
	MOVRA	r0x1003
;	.line	28; "globle.c"	k=i+j;
	MOVAR	r0x1003
	ADDRA	r0x1002
;	.line	29; "globle.c"	l=b+a;
	MOVAR	r0x1000
	ADDAR	r0x1001
	MOVRA	r0x1000
;	.line	30; "globle.c"	m=k-l;
	MOVAR	r0x1000
	RSUBRA	r0x1002
;;swapping arguments (AOP_TYPEs 1/2)
;;unsigned compare: left >= lit(0x4=4), size=1
;	.line	32; "globle.c"	if(m>3)
	MOVAI	0x04
	RSUBAR	r0x1002
	JBSET	STATUS,0
	GOTO	_00114_DS_
;;genSkipc:3251: created from rifx:028B593C
;	.line	34; "globle.c"	P1=0X80;       
	MOVAI	0x80
	MOVRA	_P1
	GOTO	_00116_DS_
_00114_DS_
;	.line	38; "globle.c"	P1=0X00;
	CLRR	_P1
_00116_DS_
	RETURN	
; exit point of _abc

;***
;  pBlock Stats: dbName = C
;***
;entry:  _initial	;Function start
; 2 exit points
;has an exit
;1 compiler assigned register :
;   r0x1004
;; Starting pCode block
_initial	;Function start
; 2 exit points
;	.line	13; "globle.c"	j=T1CNT;
	MOVAR	_T1CNT
	MOVRA	r0x1004
;	.line	14; "globle.c"	if(j)  
	MOVAI	0x00
	ORAR	r0x1004
	JBCLR	STATUS,2
	GOTO	_00106_DS_
;	.line	15; "globle.c"	DDR0=0;
	CLRR	_DDR0
	GOTO	_00108_DS_
_00106_DS_
;	.line	17; "globle.c"	DDR0=0XFF; 
	MOVAI	0xff
	MOVRA	_DDR0
_00108_DS_
	RETURN	
; exit point of _initial


;	code size estimation:
;	   36+    0 =    36 instructions (   72 byte)

	end
