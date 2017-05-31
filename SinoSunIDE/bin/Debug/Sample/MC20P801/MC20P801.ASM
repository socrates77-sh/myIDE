
;Test 20p801
;reg define 
        P1              EQU     $03
        DDR1            EQU     $04
        P1HCON          EQU     $05
        P1LCON          EQU     $06
        KBIM            EQU     $08
        TCNT0           EQU     $09
        TCR0            EQU     $0A
        DATA1           EQU     $0B
        TCNT1           EQU     $0C
        TCR1            EQU     $0D
        INTC            EQU     $0E
        MCR             EQU     $0F
        RSTFR           EQU     $10
        

;RAM ADD $E0---$FF


	
	
	ORG $1c00
RESET:
	RSP
	CLI 
 CLRA

 ldx #$C0
ram_wr:
 sta ,x 
 inca
 incx
 bne ram_wr
 
 clra
 ldx #$C0
ram_rd:
 cmp ,x
 bne test_ram_error
 inca
 
 incx
 bne ram_rd 

 lda #$aa
 sta DDR1
 lda #$bb
 sta P1HCON
 ldx #$cc
 stx P1HCON

 LDA	#$FF
 	STA	DDR1
  ;STA   ddr0
 LDA	#$FF
	STA	P1
	LDA	#$00
	STA	P1
 nop
 nop
 
 
 
 
test_ram_error: 
 ;nop
 nop
	
        bset 2,MCR ; WDT enable interrupt
        bclr 3,MCR 
        bset 5,MCR
       ; stop  
       lda #$ff
       sta TCNT0
       lda #%00101100 ; enable timer interrupt and no division
       sta TCR0
       cli      
LOOP:
       COM P1
       
        ;bset 4,MCR ;clear wdt counter
        ;jsr fun001
        ;jsr fun002
        ;jsr fun003
    JMP LOOP

fun001:
    lda $40
    inca
    nop
    inca
    sta $e0
    jsr fun011
    nop
    rts
    
fun011:
    nop
    lda #$55
    sta $e6
    ;brset 2,$30,loop
    jsr fun111
    lda #03
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

timer_int:
    nop
    lda #93
    sta TCNT0
    bclr 7,TCR0 ; clear TIF
    com P1
    rti
    
    
wdt_int:
    bclr 3,MCR 
    rti    

;INT VECTOR
        org $1ff2
        fdb wdt_int
        org $1ff6
        fdb timer_int
	ORG	$1FFE
	FDB	RESET