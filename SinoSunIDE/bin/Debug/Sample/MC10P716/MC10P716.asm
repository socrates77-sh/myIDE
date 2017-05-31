
;2012.12.27
;test mc10p716

        ;REG $00-$0f
        ;ram    $c0-$01ff
        ;rom    c000--$ffff
        KEYL	           equ	$00
        KEYH	           equ	$01
        MCR		           equ $02
        P0		            equ $03
        DDR0	           equ $04
        P0HCON	         equ $05
        TLOAD0L	        equ $06
        TLOAD0H         equ $07
        TLATL           equ $08
        TLATH           equ $09
        TCR0	           equ $0a
        TLATLL          equ $0b
        TDATA1	         equ $0b
        TLOAD1	         equ $0c
        TCR1	           equ $0d
        INTC	           equ $0e
        OPA	            equ $0f 
        P0LCON          equ $10
        P0KCON          equ $11 ;key & port control reg
        
      ;MCR 
      KBIE define  7,MCR
      KBIF define       6,MCR
      OUTC define       5,MCR
      IROUTS    define  4,MCR
      IROUTE    define  3,MCR
      IRIN      define  2,MCR
      
      ;timer0
      TIF0       define  7,TCR0
      TIM0E      define  6,TCR0
      T0START    define  2,TCR0
      ;timer1
      TIF1      define  7,TCR1
      TIM1E     define  6,TCR1
      T1START   define  2,TCR1
      
      
      
      
      
 	org $c0
timer_flag		            equ $c0
timer_1ms_counter			    equ $c1
timer_100ms_counter     equ $c2
timer_1s_counter 	      equ $c3
timer_10s_counter 		    equ $c4        ;´æ´¢tlatll
key_buffer0             equ $c5
key_buffer1             equ $c6
key_scan_counter        equ $c7
key_gnd_value0           equ $c8
key_io_value0            equ     $c9
key_gnd_value1          equ     $ca
key_io_value1           equ     $cb


tload_tmp equ $c7
pwmdata         equ     $c8


        time_100us_flag define  0,timer_flag
        time_1ms_flag   define  1,timer_flag
        time_100ms_flag define  2,timer_flag
        time_1s_flag    define  3,timer_flag
        time_10s_flag   define  4,timer_flag
        key_gnd_flag    define  5,timer_flag
        key_io_flag     define  6,timer_flag    

        ;----------------------------------------       
        
        org $c000
reset:        
  rsp     
  sei     

ram_wrh: ;write $1xx rom
        lda     #$c0
        ldx #$c0   
ram_wph:
        sta  ,x
        INCX
        inca    
        bne ram_wph 
  
ram_clrl:          ;ÇåµÍRAM
	ldx #$c0
ram_lpl:	
	clr ,x
	incx
	bne ram_lpl

ram_wrl: ;write $1xx rom
        lda     #$00
        clrx    
ram_wpl:
        sta     $100,x
        INCX
        inca    
        bne ram_wpl       


ram_clrh:          ;Çå¸ßRAM
	clrx
	lda #$00
ram_lph:	
	sta $100,x
	incx
	bne ram_lph
	bra start


delay_100us:
	lda	#$32
delay_100us_lp:
	nop
	nop
	deca
	bne	delay_100us_lp
	rts

start:
        lda #$85 ;key interrupt enable & irout low logic
        ;cmp=70mv ,S0 as p00
        sta MCR
        lda #$fe
        sta     P0KCON; set port as io
        clr     P0LCON ;push registor low disable

        lda     #$ff
        sta     DDR0 ;as input
        lda     P0        
        clr     opa        
        ; timer0 initial
        lda #$ff
        sta     TLOAD0L
        lda #$ff
        sta     TLOAD0H
        lda #%00010000
        sta TCR0
        bset    T0START ;
        ;timer1 initial
        lda #255        ;4mhz, 100us 
        sta     TLOAD1 ;timer1 8bit
        lda #$80
        sta     TDATA1 ;pwm output H
        sta     pwmdata
        sta     $0B
        lda #%00010000
        sta     TCR1
        bset    IROUTS  ;irout pin as pwm
        bset    T1START
        
        bset    IROUTE
        
        cli
loop:        

        ;lda     P0
        nop
        nop
        nop
        bset    KBIE ;enable  key int
        bclr    KBIF
        stop
        bclr    KBIE
        jsr     keyscan
        jsr     timer_counter
        jsr     funtion01
        bra     loop

;--------------------------------------
keyscan:
        bclr    KBIF ;clear key int,and reset port as H
        clr     key_buffer0
        clr     key_buffer1
        clr     key_gnd_value0
        clr     key_gnd_value1
        clr     key_io_value0
        clr     key_gnd_value1
        bclr    key_io_flag
        bclr    key_gnd_flag
        clr     key_scan_counter
scan_gnd:
        lda     KEYL
        sta     key_buffer0
        lda     KEYH
        sta     key_buffer1        
        cmp     #$7f
        beq     lab_key_scan_gnd_next      
        lda     key_buffer1
        sta     key_gnd_value1
        bset     key_gnd_flag
lab_key_scan_gnd_next:
        lda     key_buffer0        
        cmp     #$ff
        beq     lab_key_no_gnd        
        ;------search key value-----------------------
        bset     key_gnd_flag
        lda     KEYL
        sta     key_gnd_value0
        
lab_key_no_gnd:
        lda     #31
        sta     key_scan_counter
        jsr     scan_loop        
        rts
        
scan_loop:
        sta     KEYL
        
        lda     KEYL
        sta     key_buffer0
        lda     KEYH
        sta     key_buffer1
        cmp     #$7f
        beq     next_keyl
        bset    key_io_flag
        lda     KEYH
        sta     key_io_value1 ;save keyh value
next_keyl:        
        lda     key_buffer0
        cmp     #$ff
        beq     lab_key_no_io
        bset    key_io_flag
        lda     KEYL    
        sta     key_io_value0
lab_key_no_io:
        dec     key_scan_counter
        lda     key_scan_counter        
        bne     scan_loop        
        rts        
;--------------------------------------
; 100ms & 1 s
timer_counter:
       brclr     time_100us_flag,timer_counter_return
       bclr     time_100us_flag
       inc      timer_1ms_counter
       lda      timer_1ms_counter
       cmp      #10
       bne      timer_counter_return
       bset     time_1ms_flag;
       clr      timer_1ms_counter
       inc      timer_100ms_counter
       lda      timer_100ms_counter
       cmp      #100;10ms
       bne      timer_counter_return
       bset     time_100ms_flag
       clr      timer_100ms_counter
       inc      timer_1s_counter
       lda      timer_1s_counter
       cmp      #10
       bne      timer_counter_return
       bset     time_1s_flag
       clr      timer_1s_counter
       inc      timer_10s_counter
       lda      timer_10s_counter
       cmp      #10
       bne      timer_counter_return
       bset     time_10s_flag
       clr      timer_10s_counter

timer_counter_return:       
        rts  

;------adjust pwm ------------------------------------        
funtion01:
        brclr   time_100ms_flag,return_funtion01
        bclr    time_100ms_flag
        
        ;change pwm 
        lda pwmdata
        inca    
        ;LDA     #1
        sta     pwmdata
        sta     TDATA1 
        
return_funtion01:        
        rts

funtion0011:
        lda #$9b        
        ldx    #$01
        sta     $100,X
        ;jmp funtion01 ;test sp value
        rts

        



        
kbi_int:
        bclr    KBIF    ;clear key int
        ;bclr    KBIE
        rti
        ;timer0 interrupt funtion
timer0_int:
        bclr    TIF0; clear int flag
        brset   0,P0,lab_low
        bset    0,P0
        bra     lab_end_timer0
lab_low:
        bclr    0,P0
lab_end_timer0:        
        rti

;timer interrupt funtion        
timer1_int:
        bclr    TIF1
        brset   1,P0,lab_low1
        bset    1,P0
        bra     lab_end_timer1
lab_low1:
        bclr    1,P0
lab_end_timer1:
        bset time_100us_flag       
        rti
        
        
int0_int:
int1_int:
swi_int:
        nop
        rti        
   
   
        org     $fff2
        fdb     kbi_int
        
        org     $fff4
        fdb     timer0_int
        
        org     $fff6
        fdb     timer1_int
        
        org     $fff8
        fdb     int0_int
        
        org     $fffa
        fdb     int1_int
        
        org     $fffc
        fdb     swi_int
        
        org     $fffe
        fdb     reset   
        