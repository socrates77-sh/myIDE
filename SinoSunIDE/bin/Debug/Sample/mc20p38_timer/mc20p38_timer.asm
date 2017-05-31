
PA      equ     $00
PB      equ     $01
PC      equ     $02
DDRA    equ     $03
DDRB    equ     $04

TDR0    equ     $07
TLOAD0  equ     $07
TCR0    equ     $08
TDR1    equ     $09
TLOAD1  equ     $09
TCR1    equ     $0a
TDR2    equ     $0b
TLOAD2  equ     $0b 
TCR2    equ     $0c

INTC0   equ     $17
INTC1   equ     $18

T0F     equ     6,INTC0
T0E     equ     7,INTC0
T1F     equ     4,INTC0
T1E     equ     5,INTC0
T2F     equ     2,INTC0
T2E     equ     3,INTC0

        org $3FEE
        fdb     int_timer2
        fdb     int_timer1
        fdb     int_timer0
        fdb     int_cmp1
        fdb     int_lvd
        fdb     int_int
        fdb     int_cmp2
        fdb     int_swi
        fdb     int_reset
        
        org     $2c00
int_timer2:
        inc     PB
        bclr    T2F
        rti
int_timer1:
      
        bclr    T1F
  ;      lda     #%01101111
  ;     sta     intc0
        inc     PA
        cli
        rti
int_timer0:
        bclr    T0F
        com     PC
        rti
int_cmp1:
int_cmp2:
int_int:
int_lvd:
int_swi:
        rti      
int_reset:
       ; lda     #124;125
       ; sta     TLOAD0
        lda     #251;250
        sta     TLOAD1
        lda     #124;125
        sta     TLOAD2
        
        lda     #$15
        sta     TCR0
        lda     #$15
        sta     TCR1
        lda     #$15
        sta     TCR2
        bset    T2E
            bset    T1E
        cli
main_loop:
        lda     #$ff
        sta     DDRA
        sta     DDRB
        bra     main_loop       