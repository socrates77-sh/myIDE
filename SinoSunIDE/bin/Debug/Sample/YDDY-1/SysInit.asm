    ORG    $1800
;-----------------------------------------------------------------------
SysInitSbr:
	CLR	P0
	LDA	#4
	STA	DDR0    
    	CLR	P0HCON
	CLR	P1
	LDA	#$3F
	STA	DDR1
    	CLR	P1HCON
	CLR	KBIM
	LDA	#%01100011
    	STA 	ADCM        ;参考电压为Vdd
	;---------------------------------------------------------------
	;T0 2ms
	;2M/2/16=16us 16us*125=2ms
	;---------------------------------------------------------------
    	LDA	#125    
    	STA	TLOAD0
    	LDA	#%00011000            ;64分频
    	STA	TCR0
	;---------------------------------------------------------------
	;T1 stop
	;---------------------------------------------------------------
	STA    TLOAD1
	LDA    #1
	STA    TDATA1
	LDA    #%01000100
	CLR    TCR1
	;T2 noused
	CLR    TLOAD2H
	LDA    #1
	STA    TLOAD2L
	LDA    #1
	STA    TDATA2L
	CLR    TDATA2H
	LDA    #%01010100
	STA    TCR2

	CLR    INTC
	LDA    #%01000000
	STA    MCR
     
	;RSTFR        EQU    $17
;-----------------------------------------------------------------------
SysInitRamSbr:
ram_clear:            
	LDX	#$71
ram_clear1:
	CLR	$7F,X
	DECX
	BNE	ram_clear1
	RTS
;-------------------------------------------------------------------
LEDAllOff:
	BCLR	pLED1
	BCLR	pLED2
	BCLR	pLED3
	BCLR	pLED4
	RTS

