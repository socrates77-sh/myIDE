T100msSbr:
	INC	T50ms
	LDA	T50ms
	CMP	#25
	BLO	T100msSbrR
	CLR	T50ms
	BRCLR	FFlash50,T100msSbr1
	BCLR	FFlash50
	RTS
T100msSbr1:
	BSET	FFlash50
	BRCLR	FFlash100,T100msSbr2
	BCLR	FFlash100
	RTS
T100msSbr2:
	INC	CellDly
	LDA	CellDly
	CMP	#4
	BLO	T100msSbr21
	DEC	CellDly
	BSET	FVin
T100msSbr21:
	BRCLR	FADOver,T100msSbrR
	LDA	FlashCnt
	BEQ	T100msSbr3
	DEC	FlashCnt
T100msSbr3:
	INC	T500ms
	LDA	T500ms
	CMP	#5
	BLO	T100msSbrR
	CLR	T500ms
	JSR	CellChkSbr
	JSR	TemChkSbr
	JSR	OutCtrlSbr
T100msSbrR:	
	RTS
;-------------------------------------------------------------------
;µç³ØµçÑ¹¼ì²â
;-------------------------------------------------------------------
CellChkSbr:
	BRCLR	pVIn,CellChkSbrA
	;³äµç×´Ì¬---------------------------------------------------
	DEC	CellDly
	LDX	VAD
	LDA	CellV
	BEQ	CellChkSbr1
	DECA
	BEQ	CellChkSbr2
	RTS	
CellChkSbr1:
	CPX	#CV38
	BHS	CellChkSbr2
	CLR	CellV
	RTS
CellChkSbr2:
	LDA	#1
	STA	CellV
	CPX	#CV41
	BHS	CellChkSbr3
	RTS
CellChkSbr3:
	LDA	#2
	STA	CellV
	RTS
	;·Åµç×´Ì¬---------------------------------------------------
CellChkSbrA:
	CLR	CellV
	BRCLR	F38V,CellChkSbrA1
	BRCLR	F32V,CellChkSbrA2
	;LDA	FlashCnt
	;BNE	CellChkSbrR
	;LDA	#$55
	;STA	CellLow
	;BCLR	pPwr
	RTS
CellChkSbrA1:
	LDA	VAD
	CMP	#CV38
	BHS	CellChkSbrA11
	INC	V38CNT
	LDA	V38Cnt
	CMP	#10
	BLO	CellChkSbrA12
	DEC	V38CNT
	BSET	F38V
	LDA	#40
	STA	FLashCnt
	RTS
CellChkSbrA11:
	CLR	V32CNT
	CLR	V38CNT
CellChkSbrA12:
	BCLR	F38V
	BCLR	F32V
	RTS
CellChkSbrA2:
	LDA	VAD
	CMP	#CV32
	BHI	CellChkSbrA21
	INC	V32CNT
	LDA	V32CNT
	CMP	#10
	BLO	CellChkSbrA22
	DEC	V32CNT
	BSET	F32V
	LDA	#40
	STA	FLashCnt
	RTS
CellChkSbrA21:
	CLR	V32CNT
CellChkSbrA22:
	BCLR	F32V
	RTS
;-------------------------------------------------------------------
;ÎÂ¶È¼ì²â
;-------------------------------------------------------------------
TemChkSbr:
	LDA	Stauts
	BEQ	TemChkSbrR
	LDX	#CT38
	STX	Buf
	LDX	#CT41
	DECA
	BEQ	TemChkSbr1
	LDX	#CT42
	STX	Buf
	LDX	#CT45
TemChkSbr1:
	LDA	TempAD
	CMP	Buf
	BHI	TemChkSbr2
	BCLR	FTempOver
	RTS
TemChkSbr2:
	STX	Buf
	CMP	Buf
	BLO	TemChkSbrR
	BSET	FTempOver
TemChkSbrR:
	RTS
;-------------------------------------------------------------------
;Êä³ö¿ØÖÆ
;-------------------------------------------------------------------
OutCtrlSbr:
	BRSET	pVIn,OutChargSbr
	LDA	Stauts
	BEQ	OutCloseSbr
	DECA
	BEQ	OutCtrlSbr1
	DECA
	BEQ	OutCtrlSbr1
	;-----------------------------------------------------------
	;³äµçÊä³ö×´Ì¬
	BSET	pCharge
	BCLR	pHeat
	RTS	
	;-----------------------------------------------------------
	;¼ÓÈÈ×´Ì¬
OutCtrlSbr1:
	BCLR	pCharge
	BRSET	FTempOver,OutCtrlSbr2
	BRSET	FErr,OutCtrlSbr2
	BSET	pHeat
	RTS
OutCtrlSbr2:
	BCLR	pHeat
	RTS
	;-----------------------------------------------------------
	;³äµçÊäÈë×´Ì¬
OutChargSbr:
	BCLR	F32V
	BCLR	F38V
	CLR	V32CNT
	CLR	V38CNT
	CLR	Stauts
	CLR	CellLow
	BCLR	pHeat
	BCLR	pCharge
	BCLR	pPwr
	RTS
	;-----------------------------------------------------------
	;´ý»ú×´Ì¬
OutCloseSbr:
	;JSR	SysInitRamSbr
	BCLR	pCharge
	BCLR	pHeat
	BCLR	pPwr
	RTS	
	;-----------------------------------------------------------
	
