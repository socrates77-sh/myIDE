
;Test 10p02r ccr reg



PA	EQU $00
PB	EQU $01
DDRA	EQU	$04
DDRB	EQU	$05
TDR		EQU	$08
TCR		EQU	$09
KBIM	EQU	$0B
MCR		EQU	$0C
PC		EQU	$0D
DDRC	EQU	$0E

;RAM ADD $E0---$FF


	
	
	ORG $1c00
RESET:
	RSP
	CLI

 CLRA
 
 sta $e2
 
 
 ldx #$e0
ram_wr:
 sta ,x
 inca
 incx
 bne ram_wr

  
 
 clra
 ldx #$e0
ram_rd:
 cmp ,x
 bne test_ram_error
 inca
 incx
 bne ram_rd 
 
 ;bra *
 
 
 
 LDA	#$FF
	STA	DDRA
	STA	DDRB
	LDA	#$FF
	STA	PA
	STA	PB
	LDA	#$00
	STA	PA
	STA	PB
 bra loop
 
 nop
 nop
test_ram_error:
 nop
 nop
 bra *
	
LOOP:
 	   inc $E0
     LDA $e0
     STA        PA
     sta        PB

    jsr fun001
    jsr fun002
    jsr fun003
    JMP LOOP

fun001:
    lda $e0
    inca
    nop
    inca
    sta $e0
    jsr fun011
    nop
    rts
    
fun011:
    nop
    lda $e6
    inca
    sta $e6
    ;brset 2,$30,loop
    jsr fun111
    lda $e7
    inca
    sta $e7
    rts
    
fun111:
    nop
    nop
    rts

fun002:
fun003:
    nop
    nop
    rts

;INT VECTOR
	ORG	$1FFE
	FDB	RESET