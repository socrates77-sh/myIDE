;=======================================================================
;“∆∂ØµÁ‘¥
;MC20P22-SOP14
;=======================================================================
	include	MC20P22.H
	include	var.H
	include	sysInit.asm
	include	T500msSbr.asm
	include	key.asm
	include	displaysbr.asm
	include	interrupt.asm
	include	ADCSbr.asm
Rest:
	SEI
	RSP
	JSR	SysInitSbr
RestA:    
	CLI
	BRCLR	pVIn,RestA1
	CLR	CellLow
RestA1:	
	LDA	CellLow
	CMP	#$55
	BNE	RestA2
	BCLR	ADEN
	BSET	KBIE
	BSET	KBIC
	JSR	LEDAllOff
	BCLR	pHeat
	BCLR	pCharge
	NOP
	NOP
	NOP
	WAIT
	NOP
	NOP
	NOP
RestA2:	
	BRCLR	F2ms,RestA    ;
	BCLR	F2ms
	BSET	WDTC
	LDA	CellLow
	CMP	#$55
	BEQ	RestA
	
	JSR	ADCSbr
	JSR	KeySbr
	JSR	LEDAllOff
	BRCLR	FADOver,RestA
	JSR	DisplaySbr
	JSR	T100msSbr
	BRA	RestA
    
	ORG	$1FEE
	DW	WDTISbr
	DW	KBISbr
	DW	TMI2Sbr
	DW	TMI1Sbr
	DW	TMI0Sbr
	DW	INT1Sbr
	DW	INT0Sbr
	DW	SWISbr
	DW	Rest

