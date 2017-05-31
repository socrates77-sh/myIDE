
DDR1    EQU     $04
P1      EQU     $03
DDR0    EQU     $01
p0      EQU     $00
MCR     EQU     $0F

        org $1c00
reset:        
        lda $aa
        sta     $c0
        lda     #$ff
        sta     DDR1
        sta     DDR0
        
        clr     $07 ; disable OD
        
loop:
        INC     $C0
        LDA     $c0
        STA     P1
        sta     P0
        ;bset    0,P1
        ;
        ;nop
        ;bclr    0,P1
        NOP
        NOP
        bset    4,MCR
        bra     loop        
        
        
        
        
        
        org $1ffe
        fdb     reset
        
        