
;Test 10p02r ccr reg



PA      EQU $10
PB      EQU $11
DDRA    EQU     $04
P0CONH  EQU     $16
DDRB    EQU     $05
TDR             EQU     $08
TCR             EQU     $09
KBIM    EQU     $0B
MCR             EQU     $0C
PC              EQU     $12
DDRC    EQU     $0E
BTCON EQU $0C


;RAM ADD $E0---$FF


        
        
        ORG $1c00
RESET:
        RSP
        CLI

        lda #$aa
        sta $ff
        sta $fe
        sta BTCON
  clra      
  ldx #$80
ram_clr:
 sta ,x
 inca
 incx
 bne ram_clr
        
 clra 
 ldx #$80
ram_wr:
 sta ,x
 inca
 incx
 bne ram_wr
 clra
 ldx #$80
ram_rd:
 cmp ,x
 bne test_ram_error
 inca
 incx
 bne ram_rd 
 

 
 LDA    #$AA
        STA     P0CONH
  LDA #$0A
  STA $19
  LDA #$4A
  STA   $1A
  LDA #$aa
  STA   $1B
  
        STA      $17
        LDA     #$FF
        STA     PA
        STA     PB
        LDA     #$00
        STA     PA
        STA     PB
 
 
 nop
 nop
test_ram_error:
 nop
 nop
        
LOOP:
           INC $C0
     LDA $C0
     STA PA 
     sta        PB
     STA        PC

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

;INT VECTOR
        ORG     $1FFE
        FDB     RESET