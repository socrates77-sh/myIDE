;$0000-$002F:Control registers
T0CNT    EQU    $00    ;timer0
T0DATA    EQU    $01
T0CON    EQU    $02
MCR    EQU    $03
BTCON    EQU    $0c    ;basic time
BTCLR    equ    1
BTCNT    EQU    $0d
P0    EQU    $10    ;port0-2
P1    EQU    $11
P2    EQU    $12
P0CONH    EQU    $16
P0CONL    EQU    $17
P0PND    EQU    $18
P1CON    EQU    $19
P2CONH    EQU    $1a
P2CONL    EQU    $1b
PWMDATA    EQU    $22    ;PWM
PWMCON    EQU    $23
ADCON    EQU    $27    ;A/D
ADDATAH    EQU    $28
ADDATAL    EQU    $29
 

INT1E  define  3,P0PND
INT1F  define  2,P0PND
INT0E  define  1,P0PND
INT0F  define  0,P0PND

ADC_EOC define 3,ADCON;adc convert finish
ADC_EN  define 0,ADCON;adc start control bit 1=enable


    org $0030
    adch    rmb 1
adcl    rmb 1

    org $1c00
reset:
    sei
    rsp
    
    ldx #$03; disp_ptr
    

    
    lda #$a8
    sta $0c
    sta $fd
    sta $fe
    sta $ff
    sta $31
    
    lda #$a5 ;p00,01 input&push up / p02,p03 output
    sta P0CONL
    lda #$d0 ;p07--ADC input,p06--pwm, p05,04 input
    sta P0CONH
    lda #$fe ;
    
    lda #$ff
    sta $04
    sta $03
;loop2:
;    inc $f0
;    lda $f0
;    sta $03    
;    bra loop2
    
fnClearAllRam:
        ldx    #$80                ;
Lab_ClearAllRamLoop:
        clr    ,x                    ;initial and clear ram
        incx
        cpx    #$ff
        bne    Lab_ClearAllRamLoop
        
        bset INT1E;
        BSET INT0E;
    
    ;timer initial
    lda #$f2 ;t=Fsys, disable interrupt
    sta T0CON
    lda #$aa
    sta T0DATA
    cli
    

    
loop:
    lda $30
    inca
    sta $30
    lda $31
    lda $30
    INC P0
    ;adc sample
    bset ADC_EN;
adc_wait:
    ;    brclr ADC_EOC,adc_wait ;wait adc completed
    ;stop
no_stop:
    jsr fun001
    jsr fun002
    jsr fun003
    bra loop

fun001:
    lda $40
    inca
    sta $40
    jsr fun011
    nop
    rts
    
fun011:
    nop
    lda #$55
    sta $46
    ;brset 2,$30,loop
    jsr fun111
    lda #03
    sta $47
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
    
    
fn_int0:
    bclr INT0F;
    LDA $41
    INCA 
    STA $41
    rti

fn_int1:
    bclr INT1F
    LDA $42
    INCA 
    STA $42
    RTI
    
fn_time0:
    lda #$fe
    sta T0CON
    LDA $43
    INCA
    STA $43
    rti
    
    
    
    org $1ff4 ;int1 index
    fdb fn_int1
    
    org $1ff6;timer0 index
    fdb fn_time0
    
    org $1ffa; int0 index
    fdb fn_int0
    
    org $1ffe; reset index
    fdb reset