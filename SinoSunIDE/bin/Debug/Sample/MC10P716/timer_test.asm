
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
      BKIF define       6,MCR
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
t_16ms		equ $c0
t_3s			equ $c1
learn_num equ $c2
send_num 	equ $c3
times 		equ $c4        ;存储tlatll
timeh 		equ $c5        ;存储tlath
timel 		equ $c6        ;存储tlatl

tload_tmp equ $c7

flag0 equ $cf
unique_flag       equ     7
pwm_flag       		equ     6
learn_begin       equ     5
learn_end 	      equ     4
learn_err 	      equ     3
send_begin        equ     2
send_end          equ     1
no_carry          equ     0
               
learn_index equ $100   ;0x100-0x1ff用来存储码型数据

time_def equ $20       
        
        
        
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
  
ram_clrl:          ;清低RAM
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


ram_clrh:          ;清高RAM
	clrx
	lda #$00
ram_lph:	
	sta $100,x
	incx
	bne ram_lph

        	
   
   
   	bra opa_end 		 ;仿真用
	
opa_adjust:				;OPA 校准
	lda #$10
	sta opa
	jsr delay_100us
	
	lda mcr
	and #$04
	sta times

opa_lp:	
	inc opa
	jsr delay_100us	
	brset 6,opa,opa_end
	lda mcr
	and #$04
	eor times
	bne opa_lp
opa_end:
	lda opa
	ora #$c0
	sta opa	
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
        lda #$00
        sta     P0KCON; set port as io
        sta     P0LCON ;push registor low

        lda     #$ff
        sta     DDR0 ;as input
        lda     P0
        
        ; timer0 initial
        lda #$ff
        sta     TLOAD0L
        lda #$ff
        sta     TLOAD0H
        lda #%00010000
        sta TCR0
        bset    T0START ;
        ;timer1 initial
        lda #255
        sta     TLOAD1 ;timer1 8bit
        lda #$80
        sta     TDATA1 ;pwm output H
        sta     $0B
        lda #%00000000
        sta     TCR1
        bset    4,MCR
        bset    IROUTS  ;irout pin as pwm
        bset    4,MCR
        bset    T1START
        
        cli
loop:        

        ;lda     P0
        nop
        nop
        nop
        nop
        ;jsr     funtion01
        bra     loop
funtion01:
        nop
        lda #$9a
        sta     $c0
        jsr     funtion0011
        rts

funtion0011:
        lda #$9b        
        ldx    #$01
        sta     $100,X
        ;jmp funtion01 ;test sp value
        rts

        



        
kbi_int:
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
        