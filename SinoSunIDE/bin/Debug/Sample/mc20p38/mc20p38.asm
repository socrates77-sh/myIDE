
        PA      EQU     $00
        PB      EQU     $01
        PC      EQU     $02     
        DDRA    EQU     $03
        DDRB    EQU     $04
        PAHCON  EQU     $05
        PBHCON  EQU     $06
        TDR0    EQU     $07
        TCR0    EQU     $08
        TDR1    EQU     $09
        TCR1    EQU     $0A
        TDR2    EQU     $0B
        TCR2    EQU     $0C
        PPGC    EQU     $0D
        PPGT    EQU     $0E
        
        org $40 ;ram define
        counter0        rmb 1
        counter1        rmb 1
        
        
        
        org $2000
reset:        
        rsp 
        sei
        lda #$ff
        sta     DDRA
        sta     DDRB
        STA     PAHCON
        STA     PBHCON
        
;        ldx	#$40
;clr_ram:
;        clr	,x
;        incx
;        bne	clr_ram
;
;        ldx     #20
;lab_delay:
;        jsr delay_100ms        
;        jsr delay_100ms
;        nop
;        nop
;        decx
;        bne lab_delay     
        
main_loop:
        NOP
        nop
        inc     $d0
        lda     $d0
        sta     PA 
        STA     PB
        STA     PC
        ;com     PA
        nop
        jmp main_loop
        
;delay_100ms:
;        lda #200
;        sta     counter0
;delay_loop:
;        dec counter0
;        bne     delay_loop        
;        rts        
        
        
        
        org     $3ffe
        fdb     reset
        
        