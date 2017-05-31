;-----------------------------------------------------------------------
ADCSbr:
	BRSET	pPwr,ADCSbr0
	BRSET	pVIn,ADCSbr0
	CLR	ADCnt
	CLR	TempADBuf
	CLR	TempADBuf+1
	CLR	VADBuf
	CLR	VADBuf+1
	BCLR	FADOver
	RTS
ADCSbr0:
	LDA	#$16
	JSR	ADCovSbr
	LDA	ADTH
	;LDA	Test
	ADD	TempADBuf
	STA	TempADBuf
	CLRA
	ADC	TempADBuf+1
	STA	TempADBuf+1

	LDA	#$36
	JSR	ADCovSbr
	LDA	ADTH
	;LDA	Test1
	ADD	VADBuf
	STA	VADBuf
	CLRA
	ADC	VADBuf+1
	STA	VADBuf+1

	BCLR	ADEN		;关闭ADC功能，降低功耗

	INC	ADCnt
	LDA	ADCnt
	CMP	#8
	BHS	ADCSbr1
	RTS
ADCSbr1:
	ASR	TempADBuf+1
	ROR	TempADBuf
	ASR	TempADBuf+1
	ROR	TempADBuf
	ASR	TempADBuf+1
	ROR	TempADBuf
	LDA	TempADBuf
	STA	TempAD

	ASR	VADBuf+1
	ROR	VADBuf
	ASR	VADBuf+1
	ROR	VADBuf
	ASR	VADBuf+1
	ROR	VADBuf
	LDA	VADBuf
	STA	VAD
	
	CLR	ADCnt
	CLR	TempADBuf
	CLR	TempADBuf+1
	CLR	VADBuf
	CLR	VADBuf+1
	BRSET	FADOver,ADCSbr2
	JSR	CellChkSbr	
ADCSbr2:
	BSET	FADOver
	
	;-----------------------------------------------------------
	LDA	TempAD
	CMP	#5
	BLO	TempOpen
	CLR	TempOpDly
	CMP	#$FA
	BHS	TempShort
	CLR	TempShDly
	BCLR	FERR
	RTS
	;-----------------------------------------------------------
TempOpen:
	INC	TempOpDly
	LDA	TempOpDly
	CMP	#5
	BLO	TempOpenR
	BSET	FERR
	DEC	TempOpDly
TempOpenR:
	RTS
	;-----------------------------------------------------------
TempShort:
	INC	TempShDly
	LDA	TempShDly
	CMP	#5
	BLO	TempShort
	DEC	TempShDly
	BSET	FERR
TempShortR:
	RTS
	
;-----------------------------------------------------------------------
ADCovSbr:
	STA	ADCON
	BSET	ADCE
ADCovSbr0:
	BRCLR	EOC,ADCovSbr0
	RTS

