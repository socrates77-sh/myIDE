


        PA      EQU     $00
        PB      EQU     $01
        DDRA    EQU     $04
        DDRB    EQU     $05
        MCR     EQU     $0C
        DDRC    EQU     $0E
        PC      equ     $0d
        
        org $1e00
reset:        
        rsp
        cli
        
         CLRA

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
        
        
        lda     #$ff
        sta     DDRC
        bset    0,MCR
        bset    3,MCR ;PB3 REGISTER UP ENABLE
        bclr    1,MCR
        lda     #$00
        sta     DDRA
        sta     DDRB
test_ram_error:
         nop
        
loop:
        bset    1,PC 
        bclr    1,PC
        brset   3,PB,pc0_setH
        bclr    0,PC
        ;bset    0,PC
        bra     loop
pc0_setH:
        bset    0,PC
        bra loop


        org $1ffe
        fdb     reset        
       
        
        