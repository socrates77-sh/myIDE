DisplaySbr:
	;BRCLR	FADOver,DisplaySbrR
	BRCLR	pVIn,DisplaySbr1
	JSR	DispCellB
	BRSET	FERR,DisplayErrSbr
DisplaySbrR:
	RTS
	;-----------------------------------------------------------
	;δ���Դ�������ڷŵ�״̬
DisplaySbr1:
	LDA	Stauts
	BEQ	DisplaySbrR
	JSR	DispCellA
	BRSET	FERR,DisplayErrSbr
	JSR	DisplayStauts
	RTS
	;-----------------------------------------------------------
DisplayErrSbr:
	BRCLR	FFlash50,DisplaySbrR
	BSET	pLED1
	BSET	pLED2
	RTS
	;-----------------------------------------------------------
DispCellA:
	BRCLR	F32V,DispCellA2
	LDA	FlashCnt
	BEQ	DispCellA1
	BRCLR	0,FlashCnt,DispCellAR
	;BRCLR	FFlash100,DispCellAR
	BSET	pLED3
	RTS
DispCellA1:
	LDA	#$55
	STA	CellLow
	JSR	LEDAllOff
	BCLR	pHeat
	;BCLR	pPwr
	RTS
DispCellA2:
	BSET	pLED3
	BRCLR	F38V,DispCellA3
	LDA	FlashCnt
	BEQ	DispCellAR
	BRCLR	0,FlashCnt,DispCellAR
	;BRCLR	FFlash100,DispCellAR
DispCellA3:
	BSET	pLED4
DispCellAR:
	RTS
	;-----------------------------------------------------------
DispCellB:
	;-----------------------------------------------------------
	;���״̬
	;��ص�ѹС��3.8V:pLED4������pLED3��˸
	;-----------------------------------------------------------
	BRSET	FVin,DispCellB0
	BSET	pLED3
	BSET	pLED4
	RTS
DispCellB0:
	LDA	CellV
	BNE	DispCellB1
	BCLR	pLED4
	BRCLR	FFlash50,DispCellBR
	BSET	pLED3
	RTS
DispCellB1:
	;-----------------------------------------------------------
	;���״̬
	;��ص�ѹ����3.8V,С��4.1V:pLED3������pLED4��˸
	;-----------------------------------------------------------
	BSET	pLED3
	DECA	
	BNE	DispCellB2
	BRCLR	FFlash50,DispCellBR
DispCellB2:
	;-----------------------------------------------------------
	;���״̬
	;��ص�ѹ����4.1V:pLED3��pLED4����
	;-----------------------------------------------------------
	BSET	pLED4
DispCellBR:
	RTS
	;-----------------------------------------------------------
DisplayStauts:
	LDA	Stauts
	BEQ	DisplayStautsR
	DECA	
	BEQ	DisplayStauts2
	DECA
	BEQ	DisplayStauts1
	RTS
DisplayStauts1:
	BSET	pLED1
DisplayStauts2:
	BSET	pLED2
DisplayStautsR:
	RTS
	
	
