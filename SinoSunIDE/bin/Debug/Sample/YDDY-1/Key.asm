;-------------------------------------------------------------------
KeySbr:
	BRSET	pVIn,KeyACKed	;充电状态不响应按键
	;BRCLR	FADOver,KeyACKed
	BRCLR	pKey,KeyACKed
	BRSET	FKeyAck,KeySbrR
	INC	KeyCnt
	LDA	KeyCnt
	CMP	#5
	BLO	KeySbrR
	LDX	Stauts
	BEQ	KeySbr1
	BRA	KeySbr2
KeySbr1:
	CLR	KeyCnt
	INC	KeyLong
	LDA	KeyLong
	CMP	#200
	BLO	KeySbrR
	;BCLR	FADOver
	;CLR	ADCnt
	;CLR	TempADBuf
	;CLR	TempADBuf+1
	;CLR	VADBuf
	;CLR	VADBuf+1
	
KeySbr2:
	BSET	FKeyAck
	INC	Stauts
	LDA	Stauts
	CMP	#4
	BLO	KeySbr3
	CLR	Stauts
	RTS
KeySbr3:
	BSET	pPwr
	CLR	T50ms
	BSET	FFlash50
	RTS
KeyACKed:
	BCLR	FKeyAck
	CLR	KeyCnt
	CLR	KeyLong
KeySbrR:
	RTS


