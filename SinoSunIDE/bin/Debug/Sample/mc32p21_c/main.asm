;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Nov 24 2015) (MINGW32)
; This file was generated Mon Apr 18 09:49:34 2016
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"main.c"
	list	p=3221
	radix dec
	include "3221.inc"
;--------------------------------------------------------
; external declarations
;--------------------------------------------------------
	extern	_STATUSbits
	extern	_MCRbits
	extern	_INTEbits
	extern	_INTFbits
	extern	_IOP0bits
	extern	_OEP0bits
	extern	_PUP0bits
	extern	_ANSELbits
	extern	_IOP1bits
	extern	_OEP1bits
	extern	_PUP1bits
	extern	_KBIMbits
	extern	_T0CRbits
	extern	_T1CRbits
	extern	_LVDCRbits
	extern	_OSCMbits
	extern	_ADCR0bits
	extern	_ADCR1bits
	extern	_INDF
	extern	_INDF0
	extern	_INDF1
	extern	_INDF2
	extern	_HIBYTE
	extern	_FSR
	extern	_FSR0
	extern	_FSR1
	extern	_PCL
	extern	_STATUS
	extern	_MCR
	extern	_INDF3
	extern	_INTE
	extern	_INTF
	extern	_IOP0
	extern	_OEP0
	extern	_PUP0
	extern	_ANSEL
	extern	_IOP1
	extern	_OEP1
	extern	_PUP1
	extern	_KBIM
	extern	_T0CR
	extern	_T0CNT
	extern	_T0LOAD
	extern	_T0DATA
	extern	_T1CR
	extern	_T1CNT
	extern	_T1LOAD
	extern	_T1DATA
	extern	_LVDCR
	extern	_OSCM
	extern	_ADCR0
	extern	_ADCR1
	extern	_ADRH
	extern	_ADRL
	extern	_OSCAL
	extern	__gptrget1
;--------------------------------------------------------
; global declarations
;--------------------------------------------------------
	global	_Init_sys
	global	_Display
	global	_Multiplication
	global	_Division
	global	_main
	global	_int_isr
	global	_ABuf
	global	_StatusBuf
	global	_R_r0
	global	_R_r1
	global	_R_r2
	global	_R_r3
	global	_R_r4
	global	_R_r5
	global	_R_r6
	global	_R_r7
	global	_R_test
	global	_R_disp_mode
	global	_R_disp_H
	global	_R_disp_M
	global	_R_disp_L
	global	_R_ADC_Filt
	global	_R_ADC_temp
	global	_R_ADC_const
	global	_R_16_ADC
	global	_R_ADC_bak
	global	_Flag1
	global	_Flag2
	global	_TAB_sega
	global	_TAB_segb
	global	_TAB_segc
	global	_TAB_segd
	global	_TAB_sege
	global	_TAB_segf
	global	_TAB_segg

	global STK06
	global STK05
	global STK04
	global STK03
	global STK02
	global STK01
	global STK00

sharebank udata_ovr 0x0000
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
UD_main_0	udata
_ABuf	res	1

UD_main_1	udata
_StatusBuf	res	1

UD_main_2	udata
_R_r0	res	1

UD_main_3	udata
_R_r1	res	1

UD_main_4	udata
_R_r2	res	1

UD_main_5	udata
_R_r3	res	1

UD_main_6	udata
_R_r4	res	1

UD_main_7	udata
_R_r5	res	1

UD_main_8	udata
_R_r6	res	1

UD_main_9	udata
_R_r7	res	1

UD_main_10	udata
_R_test	res	1

UD_main_11	udata
_R_disp_mode	res	1

UD_main_12	udata
_R_disp_H	res	1

UD_main_13	udata
_R_disp_M	res	1

UD_main_14	udata
_R_disp_L	res	1

UD_main_15	udata
_R_ADC_Filt	res	1

UD_main_16	udata
_R_ADC_temp	res	1

UD_main_17	udata
_R_ADC_const	res	1

UD_main_18	udata
_R_16_ADC	res	2

UD_main_19	udata
_R_ADC_bak	res	2

UD_main_20	udata
_Flag1	res	1

UD_main_21	udata
_Flag2	res	1

;--------------------------------------------------------
; absolute symbol definitions
;--------------------------------------------------------
;--------------------------------------------------------
; compiler-defined variables
;--------------------------------------------------------
UDL_main_0	udata
r0x101B	res	1
r0x101C	res	1
r0x101D	res	1
r0x101E	res	1
r0x1013	res	1
r0x1014	res	1
r0x1017	res	1
r0x1018	res	1
r0x101A	res	1
;--------------------------------------------------------
; initialized data
;--------------------------------------------------------

ID_main_0	code
_TAB_sega
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01


ID_main_1	code
_TAB_segb
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x00
	retai 0x00
	retai 0x01
	retai 0x01
	retai 0x01


ID_main_2	code
_TAB_segc
	retai 0x01
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01


ID_main_3	code
_TAB_segd
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x01


ID_main_4	code
_TAB_sege
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x00
	retai 0x00
	retai 0x00
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x00


ID_main_5	code
_TAB_segf
	retai 0x01
	retai 0x00
	retai 0x00
	retai 0x00
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x01


ID_main_6	code
_TAB_segg
	retai 0x00
	retai 0x00
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x01
	retai 0x00
	retai 0x01
	retai 0x01


;@Allocation info for local variables in function 'Init_sys'
;@Init_sys Init_sys                  Allocated to registers ;size:2
;@Init_sys STATUSbits                Allocated to registers ;size:1
;@Init_sys MCRbits                   Allocated to registers ;size:1
;@Init_sys INTEbits                  Allocated to registers ;size:1
;@Init_sys INTFbits                  Allocated to registers ;size:1
;@Init_sys IOP0bits                  Allocated to registers ;size:1
;@Init_sys OEP0bits                  Allocated to registers ;size:1
;@Init_sys PUP0bits                  Allocated to registers ;size:1
;@Init_sys ANSELbits                 Allocated to registers ;size:1
;@Init_sys IOP1bits                  Allocated to registers ;size:1
;@Init_sys OEP1bits                  Allocated to registers ;size:1
;@Init_sys PUP1bits                  Allocated to registers ;size:1
;@Init_sys KBIMbits                  Allocated to registers ;size:1
;@Init_sys T0CRbits                  Allocated to registers ;size:1
;@Init_sys T1CRbits                  Allocated to registers ;size:1
;@Init_sys LVDCRbits                 Allocated to registers ;size:1
;@Init_sys OSCMbits                  Allocated to registers ;size:1
;@Init_sys ADCR0bits                 Allocated to registers ;size:1
;@Init_sys ADCR1bits                 Allocated to registers ;size:1
;@Init_sys ABuf                      Allocated to registers ;size:1
;@Init_sys StatusBuf                 Allocated to registers ;size:1
;@Init_sys R_r0                      Allocated to registers ;size:1
;@Init_sys R_r1                      Allocated to registers ;size:1
;@Init_sys R_r2                      Allocated to registers ;size:1
;@Init_sys R_r3                      Allocated to registers ;size:1
;@Init_sys R_r4                      Allocated to registers ;size:1
;@Init_sys R_r5                      Allocated to registers ;size:1
;@Init_sys R_r6                      Allocated to registers ;size:1
;@Init_sys R_r7                      Allocated to registers ;size:1
;@Init_sys R_test                    Allocated to registers ;size:1
;@Init_sys R_disp_mode               Allocated to registers ;size:1
;@Init_sys R_disp_H                  Allocated to registers ;size:1
;@Init_sys R_disp_M                  Allocated to registers ;size:1
;@Init_sys R_disp_L                  Allocated to registers ;size:1
;@Init_sys R_ADC_Filt                Allocated to registers ;size:1
;@Init_sys R_ADC_temp                Allocated to registers ;size:1
;@Init_sys R_ADC_const               Allocated to registers ;size:1
;@Init_sys R_16_ADC                  Allocated to registers ;size:2
;@Init_sys R_ADC_bak                 Allocated to registers ;size:2
;@Init_sys Flag1                     Allocated to registers ;size:1
;@Init_sys Flag2                     Allocated to registers ;size:1
;@Init_sys INDF                      Allocated to registers ;size:1
;@Init_sys INDF0                     Allocated to registers ;size:1
;@Init_sys INDF1                     Allocated to registers ;size:1
;@Init_sys INDF2                     Allocated to registers ;size:1
;@Init_sys HIBYTE                    Allocated to registers ;size:1
;@Init_sys FSR                       Allocated to registers ;size:1
;@Init_sys FSR0                      Allocated to registers ;size:1
;@Init_sys FSR1                      Allocated to registers ;size:1
;@Init_sys PCL                       Allocated to registers ;size:1
;@Init_sys STATUS                    Allocated to registers ;size:1
;@Init_sys MCR                       Allocated to registers ;size:1
;@Init_sys INDF3                     Allocated to registers ;size:1
;@Init_sys INTE                      Allocated to registers ;size:1
;@Init_sys INTF                      Allocated to registers ;size:1
;@Init_sys IOP0                      Allocated to registers ;size:1
;@Init_sys OEP0                      Allocated to registers ;size:1
;@Init_sys PUP0                      Allocated to registers ;size:1
;@Init_sys ANSEL                     Allocated to registers ;size:1
;@Init_sys IOP1                      Allocated to registers ;size:1
;@Init_sys OEP1                      Allocated to registers ;size:1
;@Init_sys PUP1                      Allocated to registers ;size:1
;@Init_sys KBIM                      Allocated to registers ;size:1
;@Init_sys T0CR                      Allocated to registers ;size:1
;@Init_sys T0CNT                     Allocated to registers ;size:1
;@Init_sys T0LOAD                    Allocated to registers ;size:1
;@Init_sys T0DATA                    Allocated to registers ;size:1
;@Init_sys T1CR                      Allocated to registers ;size:1
;@Init_sys T1CNT                     Allocated to registers ;size:1
;@Init_sys T1LOAD                    Allocated to registers ;size:1
;@Init_sys T1DATA                    Allocated to registers ;size:1
;@Init_sys LVDCR                     Allocated to registers ;size:1
;@Init_sys OSCM                      Allocated to registers ;size:1
;@Init_sys ADCR0                     Allocated to registers ;size:1
;@Init_sys ADCR1                     Allocated to registers ;size:1
;@Init_sys ADRH                      Allocated to registers ;size:1
;@Init_sys ADRL                      Allocated to registers ;size:1
;@Init_sys OSCAL                     Allocated to registers ;size:1
;@end Allocation info for local variables in function 'Init_sys';
;@Allocation info for local variables in function 'Display'
;@Display i                         Allocated to registers r0x101B ;size:1
;@end Allocation info for local variables in function 'Display';
;@Allocation info for local variables in function 'Multiplication'
;@Multiplication R_Multiplicand            Allocated to registers ;size:1
;@Multiplication R_Multiplier              Allocated to registers ;size:1
;@end Allocation info for local variables in function 'Multiplication';
;@Allocation info for local variables in function 'Division'
;@Division R_sor                     Allocated to registers ;size:1
;@Division R_dend                    Allocated to registers r0x1014 r0x1013 ;size:2
;@end Allocation info for local variables in function 'Division';
;@Allocation info for local variables in function 'main'
;@end Allocation info for local variables in function 'main';
;@Allocation info for local variables in function 'int_isr'
;@end Allocation info for local variables in function 'int_isr';

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
;   _Display
;   _Display
;   _Display
;   _Display
;   _Display
;   _Display
;; Starting pCode block
_int_isr	;Function start
; 0 exit points
;	.line	199; "main.c"	__endasm;
	movra _ABuf
	swapar _STATUS
	movra _StatusBuf
	
;	.line	200; "main.c"	T0IF  = 0;
	BCLR	_INTFbits,0
;	.line	201; "main.c"	R_disp_mode++;
	INCR	_R_disp_mode
;;swapping arguments (AOP_TYPEs 1/3)
;;unsigned compare: left >= lit(0x3=3), size=1
;	.line	202; "main.c"	if(R_disp_mode>2)R_disp_mode = 0;                
	MOVAI	0x03
	RSUBAR	_R_disp_mode
	JBSET	STATUS,0
	GOTO	_00165_DS_
;;genSkipc:3251: created from rifx:0028608C
	CLRR	_R_disp_mode
_00165_DS_
;	.line	203; "main.c"	if(R_disp_mode==0){Display(R_disp_H);COM0=0;}
	MOVAI	0x00
	ORAR	_R_disp_mode
	JBSET	STATUS,2
	GOTO	_00167_DS_
	MOVAR	_R_disp_H
	CALL	_Display
	BCLR	_IOP0bits,0
_00167_DS_
;	.line	204; "main.c"	if(R_disp_mode==1){Display(R_disp_M);COM1=0;}
	MOVAR	_R_disp_mode
	XORAI	0x01
	JBSET	STATUS,2
	GOTO	_00169_DS_
	MOVAR	_R_disp_M
	CALL	_Display
	BCLR	_IOP0bits,1
_00169_DS_
;	.line	205; "main.c"	if(R_disp_mode==2){Display(R_disp_L);COM2=0;}       
	MOVAR	_R_disp_mode
	XORAI	0x02
	JBSET	STATUS,2
	GOTO	_00171_DS_
	MOVAR	_R_disp_L
	CALL	_Display
	BCLR	_IOP0bits,2
_00171_DS_
;	.line	211; "main.c"	__endasm;
	swapar _StatusBuf
	movra _STATUS
	swapr _ABuf
	swapar _ABuf
	
END_OF_INTERRUPT
	RETIE	

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
;   _Init_sys
;   _Multiplication
;   _Multiplication
;   _Multiplication
;   _Division
;   _Division
;   _Init_sys
;   _Multiplication
;   _Multiplication
;   _Multiplication
;   _Division
;   _Division
;6 compiler assigned registers:
;   r0x1017
;   r0x1018
;   r0x1019
;   r0x101A
;   STK00
;   STK01
;; Starting pCode block
_main	;Function start
; 2 exit points
;	.line	142; "main.c"	GIE = 0;
	BCLR	_MCRbits,7
;	.line	143; "main.c"	ClrWdt();
	clrwdt
;	.line	154; "main.c"	__endasm;    
	movai 0x7f
	movra 0x00
	movar 0x00
	movra FSR
	clrr INDF
	djzr 0x00
	goto $-4
	clrr INDF
	clrr 0x00
	
;	.line	156; "main.c"	Init_sys();
	CALL	_Init_sys
;	.line	157; "main.c"	GIE = 1;
	BSET	_MCRbits,7
_00138_DS_
;	.line	160; "main.c"	R_16_ADC = 0;
	CLRR	_R_16_ADC
	CLRR	(_R_16_ADC + 1)
;	.line	161; "main.c"	for(R_ADC_Filt=0;R_ADC_Filt<255;R_ADC_Filt++)
	CLRR	_R_ADC_Filt
;;unsigned compare: left < lit(0xFF=255), size=1
_00140_DS_
	MOVAI	0xff
	RSUBAR	_R_ADC_Filt
	JBCLR	STATUS,0
	GOTO	_00143_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	163; "main.c"	ADEOC = 0;//start ADC
	BCLR	_ADCR0bits,1
_00131_DS_
;	.line	164; "main.c"	do{Nop();}while(ADEOC==0);       
	nop
	JBSET	_ADCR0bits,1
	GOTO	_00131_DS_
;	.line	165; "main.c"	R_16_ADC = R_16_ADC+ADRH;                    
	MOVAR	_ADRH
	MOVRA	r0x1017
	CLRR	r0x1018
;;102	MOVAR	r0x1017
;;100	MOVRA	r0x1019
	MOVAI	0x00
	MOVRA	r0x101A
;;101	MOVAR	r0x1019
	MOVAR	r0x1017
	ADDRA	_R_16_ADC
	MOVAR	r0x101A
	JBCLR	STATUS,0
	JZAR	r0x101A
	ADDRA	(_R_16_ADC + 1)
;	.line	161; "main.c"	for(R_ADC_Filt=0;R_ADC_Filt<255;R_ADC_Filt++)
	INCR	_R_ADC_Filt
	GOTO	_00140_DS_
_00143_DS_
;	.line	167; "main.c"	R_ADC_temp  = R_16_ADC>>8;                
	MOVAR	(_R_16_ADC + 1)
	MOVRA	_R_ADC_temp
	MOVRA	r0x1017
	CLRR	r0x1018
;;105	MOVAR	r0x1017
;	.line	168; "main.c"	R_ADC_const = 200;//若要滤波效果好，这里需要一个算法去确定数值。
	MOVAI	0xc8
	MOVRA	_R_ADC_const
;	.line	169; "main.c"	if(R_ADC_bak>R_ADC_temp)
	MOVAR	_R_ADC_temp
	MOVRA	r0x1017
	CLRR	r0x1018
	MOVAR	(_R_ADC_bak + 1)
	RSUBAR	r0x1018
	JBSET	STATUS,2
	GOTO	_00159_DS_
	MOVAR	_R_ADC_bak
	RSUBAR	r0x1017
_00159_DS_
	JBCLR	STATUS,0
	GOTO	_00135_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	171; "main.c"	R_ADC_temp = R_ADC_bak-R_ADC_temp;
	MOVAR	_R_ADC_bak
	MOVRA	r0x1017
	MOVAR	_R_ADC_temp
	RSUBAR	r0x1017
	MOVRA	_R_ADC_temp
;	.line	172; "main.c"	Multiplication(R_ADC_temp,R_ADC_const); 
	MOVAI	0xc8
	MOVRA	STK00
	MOVAR	_R_ADC_temp
	CALL	_Multiplication
;	.line	173; "main.c"	R_ADC_temp = R_ADC_bak-R_r1; 
	MOVAR	_R_ADC_bak
	MOVRA	r0x1017
	MOVAR	_R_r1
	RSUBAR	r0x1017
	MOVRA	_R_ADC_temp
	GOTO	_00136_DS_
_00135_DS_
;	.line	176; "main.c"	R_ADC_temp = R_ADC_temp-R_ADC_bak;
	MOVAR	_R_ADC_bak
	MOVRA	r0x1017
	MOVAR	r0x1017
	RSUBRA	_R_ADC_temp
;	.line	177; "main.c"	Multiplication(R_ADC_temp,R_ADC_const);
	MOVAI	0xc8
	MOVRA	STK00
	MOVAR	_R_ADC_temp
	CALL	_Multiplication
;	.line	178; "main.c"	R_ADC_temp = R_ADC_bak+R_r1;   
	MOVAR	_R_ADC_bak
	MOVRA	r0x1017
	MOVAR	_R_r1
	ADDAR	r0x1017
	MOVRA	_R_ADC_temp
_00136_DS_
;	.line	180; "main.c"	R_ADC_bak = R_ADC_temp;
	MOVAR	_R_ADC_temp
	MOVRA	_R_ADC_bak
	CLRR	(_R_ADC_bak + 1)
;	.line	181; "main.c"	Multiplication(R_ADC_temp,200);
	MOVAI	0xc8
	MOVRA	STK00
	MOVAR	_R_ADC_temp
	CALL	_Multiplication
;	.line	182; "main.c"	R_ADC_temp = R_r1;//取高8位
	MOVAR	_R_r1
	MOVRA	_R_ADC_temp
;	.line	183; "main.c"	Division(R_ADC_temp,100);//取百位
	MOVAR	_R_ADC_temp
	MOVRA	r0x1017
	CLRR	r0x1018
	MOVAI	0x64
	MOVRA	STK01
	MOVAR	r0x1017
	MOVRA	STK00
	MOVAI	0x00
	CALL	_Division
;	.line	184; "main.c"	R_disp_H   = R_r0;
	MOVAR	_R_r0
	MOVRA	_R_disp_H
;	.line	185; "main.c"	R_ADC_temp = R_r2;
	MOVAR	_R_r2
	MOVRA	_R_ADC_temp
;	.line	186; "main.c"	Division(R_ADC_temp,10);//取十位
	MOVAR	_R_ADC_temp
	MOVRA	r0x1017
	CLRR	r0x1018
	MOVAI	0x0a
	MOVRA	STK01
	MOVAR	r0x1017
	MOVRA	STK00
	MOVAI	0x00
	CALL	_Division
;	.line	187; "main.c"	R_disp_M = R_r0;  
	MOVAR	_R_r0
	MOVRA	_R_disp_M
;	.line	188; "main.c"	R_disp_L = R_r2; 
	MOVAR	_R_r2
	MOVRA	_R_disp_L
	GOTO	_00138_DS_
	RETURN	
; exit point of _main

;***
;  pBlock Stats: dbName = C
;***
;entry:  _Division	;Function start
; 2 exit points
;has an exit
;6 compiler assigned registers:
;   r0x1013
;   STK00
;   r0x1014
;   STK01
;   r0x1015
;   r0x1016
;; Starting pCode block
_Division	;Function start
; 2 exit points
	MOVRA	_R_r3
;	.line	95; "main.c"	void  Division(unsigned int R_dend,uchar R_sor)
	MOVRA	r0x1013
	MOVAR	STK00
	MOVRA	_R_r4
	MOVRA	r0x1014
	MOVAR	STK01
	MOVRA	_R_r5
;	.line	97; "main.c"	R_r0 = 0;
	CLRR	_R_r0
;	.line	98; "main.c"	R_r1 = 0;
	CLRR	_R_r1
;	.line	99; "main.c"	R_r2 = 0; 
	CLRR	_R_r2
;;104	MOVAR	r0x1013
;;1	MOVRA	r0x1015
;;1	CLRR	r0x1016
;;99	MOVAR	r0x1015
;	.line	101; "main.c"	R_r4 = R_dend&0xff;
	CLRR	r0x1013
;;103	MOVAR	r0x1014
;	.line	103; "main.c"	R_r7 = 0;                       
	CLRR	_R_r7
;	.line	104; "main.c"	if(R_r5==0)
	MOVAI	0x00
	ORAR	_R_r5
	JBSET	STATUS,2
	GOTO	_00121_DS_
;	.line	106; "main.c"	R_r0 = 0;
	CLRR	_R_r0
;	.line	107; "main.c"	R_r1 = 0;
	CLRR	_R_r1
;	.line	108; "main.c"	R_r2 = 0;        
	CLRR	_R_r2
	GOTO	_00126_DS_
_00121_DS_
;	.line	112; "main.c"	for(R_r6=0;R_r6<16;R_r6++)
	MOVAI	0x10
	MOVRA	_R_r6
_00125_DS_
;	.line	134; "main.c"	__endasm;   
	;
	bclr _STATUS,0
	rlr _R_r4
	rlr _R_r3
	rlr _R_r2
	rlr _R_r7
	movar _R_r5
	rsubar _R_r2
	jbclr _STATUS,0
	goto updata_div
	jbclr _R_r7,0
	goto updata_div
	bclr _STATUS,0
	goto r0_shift
updata_div:
	movra _R_r2
	bset _STATUS,0
r0_shift:
	rlr _R_r0
	rlr _R_r1
	
	MOVAR	_R_r6
	MOVRA	r0x1014
	DECAR	r0x1014
	MOVRA	_R_r6
;	.line	112; "main.c"	for(R_r6=0;R_r6<16;R_r6++)
	MOVAI	0x00
	ORAR	_R_r6
	JBSET	STATUS,2
	GOTO	_00125_DS_
	MOVAI	0x10
	MOVRA	_R_r6
_00126_DS_
	RETURN	
; exit point of _Division

;***
;  pBlock Stats: dbName = C
;***
;entry:  _Multiplication	;Function start
; 2 exit points
;has an exit
;2 compiler assigned registers:
;   STK00
;   r0x1013
;; Starting pCode block
_Multiplication	;Function start
; 2 exit points
;	.line	67; "main.c"	void  Multiplication(uchar R_Multiplier,uchar R_Multiplicand)
	MOVRA	_R_r3
	MOVAR	STK00
	MOVRA	_R_r4
;	.line	69; "main.c"	R_r1 = 0;      //结果高8位
	CLRR	_R_r1
;	.line	70; "main.c"	R_r0 = 0;      //结果低8位
	CLRR	_R_r0
;	.line	73; "main.c"	for(R_r2=0;R_r2<8;R_r2++)   
	MOVAI	0x08
	MOVRA	_R_r2
_00115_DS_
;	.line	84; "main.c"	__endasm;      
	;
	rrr _R_r3
	jbset _STATUS,0
	goto Mul_loop
	movar _R_r4
	addra _R_r1
Mul_loop:
	rrr _R_r1
	rrr _R_r0
	
	MOVAR	_R_r2
	MOVRA	r0x1013
	DECAR	r0x1013
	MOVRA	_R_r2
;	.line	73; "main.c"	for(R_r2=0;R_r2<8;R_r2++)   
	MOVAI	0x00
	ORAR	_R_r2
	JBSET	STATUS,2
	GOTO	_00115_DS_
	MOVAI	0x08
	MOVRA	_R_r2
	RETURN	
; exit point of _Multiplication

;***
;  pBlock Stats: dbName = C
;***
;entry:  _Display	;Function start
; 2 exit points
;has an exit
;functions called:
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;   __gptrget1
;6 compiler assigned registers:
;   r0x101B
;   r0x101C
;   r0x101D
;   STK01
;   STK00
;   r0x101E
;; Starting pCode block
_Display	;Function start
; 2 exit points
;	.line	48; "main.c"	void  Display(uchar i)
	MOVRA	r0x101B
;	.line	50; "main.c"	COM0 = 1;
	BSET	_IOP0bits,0
;	.line	51; "main.c"	COM1 = 1;
	BSET	_IOP0bits,1
;	.line	52; "main.c"	COM2 = 1; 
	BSET	_IOP0bits,2
;	.line	53; "main.c"	SEG_A = TAB_sega[i];
	MOVAR	r0x101B
	ADDAI	(_TAB_sega + 0)
	MOVRA	r0x101C
	MOVAI	high (_TAB_sega + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x101D
	MOVAR	r0x101C
	MOVRA	STK01
	MOVAR	r0x101D
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	r0x101E
	RRAR	r0x101E
	JBSET	STATUS,0
	BCLR	_IOP1bits,0
	JBCLR	STATUS,0
	BSET	_IOP1bits,0
;	.line	54; "main.c"	SEG_B = TAB_segb[i];
	MOVAR	r0x101B
	ADDAI	(_TAB_segb + 0)
	MOVRA	r0x101C
	MOVAI	high (_TAB_segb + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x101D
	MOVAR	r0x101C
	MOVRA	STK01
	MOVAR	r0x101D
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	r0x101E
	RRAR	r0x101E
	JBSET	STATUS,0
	BCLR	_IOP1bits,1
	JBCLR	STATUS,0
	BSET	_IOP1bits,1
;	.line	55; "main.c"	SEG_C = TAB_segc[i];
	MOVAR	r0x101B
	ADDAI	(_TAB_segc + 0)
	MOVRA	r0x101C
	MOVAI	high (_TAB_segc + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x101D
	MOVAR	r0x101C
	MOVRA	STK01
	MOVAR	r0x101D
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	r0x101E
	RRAR	r0x101E
	JBSET	STATUS,0
	BCLR	_IOP1bits,2
	JBCLR	STATUS,0
	BSET	_IOP1bits,2
;	.line	56; "main.c"	SEG_D = TAB_segd[i];
	MOVAR	r0x101B
	ADDAI	(_TAB_segd + 0)
	MOVRA	r0x101C
	MOVAI	high (_TAB_segd + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x101D
	MOVAR	r0x101C
	MOVRA	STK01
	MOVAR	r0x101D
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	r0x101E
	RRAR	r0x101E
	JBSET	STATUS,0
	BCLR	_IOP1bits,3
	JBCLR	STATUS,0
	BSET	_IOP1bits,3
;	.line	57; "main.c"	SEG_E = TAB_sege[i];
	MOVAR	r0x101B
	ADDAI	(_TAB_sege + 0)
	MOVRA	r0x101C
	MOVAI	high (_TAB_sege + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x101D
	MOVAR	r0x101C
	MOVRA	STK01
	MOVAR	r0x101D
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	r0x101E
	RRAR	r0x101E
	JBSET	STATUS,0
	BCLR	_IOP1bits,4
	JBCLR	STATUS,0
	BSET	_IOP1bits,4
;	.line	58; "main.c"	SEG_F = TAB_segf[i];
	MOVAR	r0x101B
	ADDAI	(_TAB_segf + 0)
	MOVRA	r0x101C
	MOVAI	high (_TAB_segf + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x101D
	MOVAR	r0x101C
	MOVRA	STK01
	MOVAR	r0x101D
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	r0x101E
	RRAR	r0x101E
	JBSET	STATUS,0
	BCLR	_IOP1bits,5
	JBCLR	STATUS,0
	BSET	_IOP1bits,5
;	.line	59; "main.c"	SEG_G = TAB_segg[i];                                  
	MOVAR	r0x101B
	ADDAI	(_TAB_segg + 0)
	MOVRA	r0x101B
	MOVAI	high (_TAB_segg + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x101C
	MOVAR	r0x101B
	MOVRA	STK01
	MOVAR	r0x101C
	MOVRA	STK00
	MOVAI	0x80
	CALL	__gptrget1
	MOVRA	r0x101D
	RRAR	r0x101D
	JBSET	STATUS,0
	BCLR	_IOP0bits,3
	JBCLR	STATUS,0
	BSET	_IOP0bits,3
	RETURN	
; exit point of _Display

;***
;  pBlock Stats: dbName = C
;***
;entry:  _Init_sys	;Function start
; 2 exit points
;has an exit
;; Starting pCode block
_Init_sys	;Function start
; 2 exit points
;	.line	34; "main.c"	OEP0   = 0xef;
	MOVAI	0xef
	MOVRA	_OEP0
;	.line	35; "main.c"	IOP0   = 0x00;
	CLRR	_IOP0
;	.line	36; "main.c"	OEP1   = 0xff;
	MOVAI	0xff
	MOVRA	_OEP1
;	.line	37; "main.c"	IOP1   = 0x00;   
	CLRR	_IOP1
;	.line	38; "main.c"	ANSEL  = 0x10;//p04设为AD检测引脚 
	MOVAI	0x10
	MOVRA	_ANSEL
;	.line	39; "main.c"	T0CR   = 0x84;//FCPU   16T
	MOVAI	0x84
	MOVRA	_T0CR
;	.line	40; "main.c"	T0LOAD = 200;
	MOVAI	0xc8
	MOVRA	_T0LOAD
;	.line	41; "main.c"	ADCR0  = 0x43;
	MOVAI	0x43
	MOVRA	_ADCR0
;	.line	42; "main.c"	ADCR1  = 0x00;//ref=2V
	CLRR	_ADCR1
;	.line	43; "main.c"	INTE   = 0x01; 
	MOVAI	0x01
	MOVRA	_INTE
;	.line	44; "main.c"	INTF   = 0x00;
	CLRR	_INTF
;	.line	45; "main.c"	PUP0   = 0;
	CLRR	_PUP0
	RETURN	
; exit point of _Init_sys


;	code size estimation:
;	  346+    0 =   346 instructions (  692 byte)

	end
