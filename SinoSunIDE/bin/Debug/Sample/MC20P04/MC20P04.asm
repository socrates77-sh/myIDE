
;Test 20p04
;reg define 
        P0      EQU     $00
        PA      EQU     $00
        PB      EQU     $04
        PC      EQU     $08
        DDR0    EQU     $01
        P0HCON  EQU     $02
        P0LCON  EQU     $03
        P1      EQU     $04
        DDR1    EQU     $05
        P1HCON  EQU     $06
        BKIM    EQU     $07
        P2      EQU     $08
        DDR2    EQU     $09
        TDR     EQU     $0A
        TCR     EQU     $0B
        INTC    EQU     $0C
        MCR     EQU     $0D
        RSTFR   EQU     $0E
        CMPC    EQU     $0F
        CMP0A   EQU     $10
        CMP1A   EQU     $11




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
 sta ddr0
 lda #$bb
 sta p0hcon
 ldx #$cc
 stx p0lcon

 LDA    #$FF
    STA DDR0
    STA DDR1
 LDA    #$FF
    STA P1
    STA P2
    LDA #$00
    STA P1
    STA P2
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
       sta TDR
       lda #%00101100 ; enable timer interrupt and no division
       sta TCR
       cli      
LOOP:
       COM PA
        ;bset 4,MCR ;clear wdt counter
    jsr fun001
    jsr fun002
    jsr fun003
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
    sta TDR
    bclr 7,TCR ; clear TIF
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
    ORG $1FFE
    FDB RESET