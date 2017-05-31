keyl	equ	$00
keyh	equ	$01
mcr		equ $02
p0		equ $03
ddr0	equ $04
p0hcon	equ $05
tload0l	equ $06
tload0h equ $07
tlatl equ $08
tlath equ $09
tcr0	equ $0a
tlatll equ $0b
tdata1	equ $0b
tload1	equ $0c
tcr1	equ $0d
intc	equ $0e
opa	equ $0f

	org $c0
t_16ms		equ $c0
t_3s			equ $c1
learn_num equ $c2
send_num 	equ $c3
times 		equ $c4        ;�洢tlatll
timeh 		equ $c5        ;�洢tlath
timel 		equ $c6        ;�洢tlatl

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
               
learn_index equ $100   ;0x100-0x1ff�����洢��������

time_def equ $7f

set_learn_num equ $44

	ORG $c000
start:
	lda #$ff
	sta ddr0
			
ram_clrl:          ;���RAM
	ldx #$c0
ram_lpl:	
	clr ,x
	incx
	bne ram_lpl

ram_clrh:          ;���RAM
	clrx
	lda #$ff
ram_lph:	
	sta $100,x
	incx
	bne ram_lph	
	
	bra opa_end 		 ;������
	
opa_adjust:				;OPA У׼
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
	bra learn_start

delay_100us:
	lda	#$32
delay_100us_lp:
	nop
	nop
	deca
	bne	delay_100us_lp
	rts

learn_start:
	clr t_16ms
	lda #time_def         ;timer0 1��Ƶ ��ʱ65535*0.25u=16ms	
	sta tload0h
	lda #time_def
	sta tload0l
	lda #$01        ;tlath tlatl��¼IR������ʱ�䣬tlatll��¼IR������ʱ��
	sta tcr0
	
	clr tdata1
	lda #5
	sta tload1			
	lda #$02				;timer1 �ⲿ����ģʽ(IR�½��ؼ���)����¼4���ز�ʱ�䳤��
	sta tcr1
	
	bset 2,tcr0     ;timer0����
	bset 2,tcr1     ;timer1����
	
	bclr 3,tcr0
	
	lda #$80        ;IRIN�������ж�ʹ��
	sta intc
	
	clr send_num
 
	cli	
	;ѧϰģʽ
	;ѧϰģʽ��ʼ��timer0���ڶ�ʱģʽ��ÿ16ms��������ж�1��,���ڲ���ʱ��
	;���ο�ʼ��ʱIRIN����������timer0��������int0�жϽ���int0�жϷ��������int0�жϱ�־
	;�ڴ�Լ70u�ڲ�ͣ��ȡint0�жϱ�־��Ϊ�߱�ʾ���ز�,����Ϊ���ز�
	;���Ϊ���ز�����ֻ��int0�ж��Ͻ��ؼ�¼ÿ�ε�ʱ��ֱ��ѧϰ����
	;����ڴ��ز����ر�int1�жϣ���4�μ�����������tmr1�жϷ������
	;tmr1�ж��м����ز���ȣ����ز���ȸ�ֵ��t1load������timer1���óɰ���ģʽ������int1�������ж�ʹ��
	;int1�ж��м�¼ÿ�ε�ʱ�䣬ͬʱ�ı�tlath/tlatl�������غ�int1���жϴ�����ֱ��ѧϰ����
	
learn_ing:
	brset learn_err,flag0,learn_error
	brclr learn_end,flag0,learn_ing
	
	;����ģʽ
	;���ز���timer1���ó�pwmģʽ����ز��źţ�������timer0�ж��źſ��ƣ�timer0����ʱ�䣬mcr����irout��pwm���
	;timer0Ϊ�Լ��ؼ�����������ʱд����������Ч���������ż��أ�����ʱ��Ҫ��ǰһ���ж���д
	;�����ʱʱ�䳬��16ms���ȼ�ʱ�������֣�Ȼ��ر�TMR0EN���ر�timer0����timer1���أ��ȳ������ּ�ʱ����ٴ�
	;timer1ͬ�ϣ�ֻ����Ҫ��TCLR���ó�1��ʹpwm�����Թر�״̬
	
	bclr 7,opa                    ;�ر�opa
 bset 5,mcr
 bclr 0,p0

send_loop:
 lda send_num                  ;�ѷ�������ݴ�,�����жϷ������
	sta learn_num      
	clr send_num

send_next: 		
	brclr no_carry,flag0,carry_on
carry_off:	
	lda times
	sub #time_def
	coma
	sta tload1
	clr tdata1             
	lda #$40                      ;���ز���������
	sta tcr1	
	bset 0,tcr1                   ;�Թر�ģʽ
	bra send_start	
carry_on:	
	lda times
	jsr set_pwm	
	lda #$40                      ;���ز���������
	sta tcr1	
send_start:
	lda #$01 
	sta tload0h
 lda #$20
	sta tload0l                    ;tload0h�����壬дtload0l����tload0h��tload0l��ֵͬʱ���뵽Ԥ���ؼĴ�����	
	lda #$00         
	sta tcr0	
	;bset 3,tcr1
	bclr 3,tcr1
	bset 2,tcr0
	bset 2,tcr1
	bset send_begin,flag0
	bset pwm_flag,flag0
 bset 0,p0
	lda #$20   ;#$30                      
	sta mcr	

send_ing:
	brclr send_end,flag0,send_ing
	

 lda learn_num
 sta send_num
 bclr send_end,flag0
 bclr 0,p0
 bclr 4,mcr
	bra send_loop                            ;�˴������������					

set_pwm:                           ;�����ز�����
	deca
	sta tload1
	inca
	lsra
	sta tdata1
	rts

learn_error:
	bclr learn_err,flag0	
	bra *

kbi:
	bclr 6,mcr	
	rti

tmi0:																
	bclr 7,tcr0	
	brset learn_begin,flag0,tmi0_learn
	brset send_begin,flag0,tmi0_send
	brset send_end,flag0,tmi0_send_end_over
	inc t_16ms                          ;�źŵȴ���ʱ
	lda t_16ms
	cmp #200
	bne tmi0_end
	clr t_16ms
	inc t_3s
	lda t_3s
	cmp #40
	bne tmi0_end
	clr t_3s                            ;2�������źų�ʱ
	;bset learn_err,flag0                ;��λѧϰ�����־
tmi0_end:		
	rti
tmi0_learn:                           ;ѧϰ������������������в�������ֵFF
	inc send_num	
tmi0_learn_index_next:	
	inc t_16ms                         
	lda t_16ms		
	cmp #20
	beq tmi0_learn_end	                ;�ѧϰ320ms
	rti
tmi0_learn_end:	
	bset learn_end,flag0
	bclr learn_begin,flag0
	clr intc	      
	rti	

tmi0_send_end_over:
	bset 6,tcr0
	clr tload0h
	clr tload0l
	rti	

tmi0_send:                           ;����	
	bset 3,tcr1
	bset 4,mcr
	ldx send_num
	lda learn_index,x
	coma
	beq tmi0_send_unique
	brset unique_flag,flag0,tmi0_send_unique_end
	
	lda #time_def
	sub learn_index+1,x
	sta tload_tmp
	lda #time_def
	sbc learn_index,x
	sta tload0h
	lda tload_tmp
	sta tload0l	
	inc send_num
	inc send_num
	bset 2,tcr1	                        ;�����������	
	bra tmi0_send_next

tmi0_send_unique:
	brset unique_flag,flag0,tmi0_send_unique_loop
	lda #time_def											
	sta tload0h
	sta tload0l	
	inc send_num
	bset 2,tcr1
	bset unique_flag,flag0
	bra tmi0_send_next		

tmi0_send_unique_loop:
	lda #time_def											
	sta tload0h
	sta tload0l	
	inc send_num
	bclr 2,tcr1
	bra tmi0_send_next

tmi0_send_unique_end:
	lda #time_def
	sub learn_index+1,x
	sta tload_tmp
	lda #time_def
	sbc learn_index,x
	sta tload0h
	lda tload_tmp
	sta tload0l	
	inc send_num
	inc send_num 
	bclr 2,tcr1
	bclr unique_flag,flag0
	bra tmi0_send_next		

tmi0_send_next:                       ;�Ƿ���	
	ldx send_num
	cpx learn_num
	beq tmi0_send_end
	rti	
tmi0_send_end:                        ;�������
	bclr send_begin,flag0
	bset send_end,flag0
	rti		



		
tmi1:	                          ;4���ز������������жϣ�����ȥ����ʱ��      
	lda tlatl
	ldx tlath
	sta timel
	stx timeh       
	         
	lda times                      ;�����ز�����
	sub timel
	sta times
	lda #time_def
	sbc timeh
	lsra                          ;��2
	ror times
	lsra                          ;��2
	ror times
	lda times                     ;�����ز�����
	add #5
	jsr set_pwm

 ;lda #$f0
 ;sta tload1
 
	lda #$41                      ;����ģʽ
	sta tcr1
	bset 2,tcr1
	bclr 0,tcr0                   ;�����ز���
	bset 1,tcr0                   ;����ѡ��IRINT
	lda #$0a                      ;���������ź�IRINT�ж�
	sta intc	
	bclr 3,tcr0                   ;����������
	rti	

int0:																   ;IRIN��һ���½��ش��������жϣ�timer0�Զ�����
	lda tlatl
	ldx tlath
	sta timel
	stx timeh  
	bclr 4,intc
 bset 0,p0
	brset no_carry,flag0,int0_no_carry_end	;���ز���֧
	bset learn_begin,flag0
	lda #set_learn_num                             ;ѧϰ����
	sta learn_num
	brset 4,intc,int0_carry							 ;���ο�ʼ��70u�ж�ȡ��־λ���ж������ز�
	lda #6
delay_70us:
	brset 4,intc,int0_carry							
	deca
	bne delay_70us
	brclr 4,intc,int0_no_carry           
int0_carry:														 ;���ز�����
	lda tlatll                           ;��¼������
	sta times
	bclr no_carry,flag0                  ;���ز�	
	bclr 7,intc													 ;�ر�IRIN�ж�	
	bclr 4,intc													 ;��int0�жϱ�־
	bclr 0,tcr0                          ;�ı�tlat�����ؿ�
	rti
int0_no_carry:
	bset no_carry,flag0                  ;�����ز�
	lda tlatll                           ;��¼������
	sta times
	bset 6,tcr1												   ;�ر�tmi1�ж�
	bclr 3,tcr0                          ;����������
	rti
int0_no_carry_end:										 ;�ж����ز������Ƿ�ѧϰ����
	jsr save_time	
	bclr 3,tcr0                          ;����������
	dec learn_num	                              
	beq int0_learn_end	
	rti
int0_learn_end:
	bset learn_end,flag0      
	bclr learn_begin,flag0           
	bclr 7,intc                          ;���ز�����ѧϰ��ɣ��ر��ж�
	bclr 4,intc
	rti

int1:                               ;��¼���ز����ͷ�תʱ��
	lda tlatl
	ldx tlath
	sta timel
	stx timeh  
	bclr 0,intc

 lda #$02
 eor p0
 sta p0
 
	jsr save_time
	dec learn_num       							;�жϴ��ز������Ƿ�ѧϰ����
	beq int1_end
	brset 1,intc,code_posedge         ;�ı�tlat�����ؿں�IRIN�ж��ؿ�
code_negedge:
	bset 1,intc                       
	bclr 0,tcr0
	bclr 3,tcr0                          ;����������	
	rti
code_posedge:	
	bclr 1,intc
	bset 0,tcr0
	bclr 3,tcr0                          ;����������	
	rti	
int1_end:
	bset learn_end,flag0
	bclr learn_begin,flag0 
	clr intc	                         ;���ز�����ѧϰ��ɣ��ر��ж�
	rti

swii:  
  rti

save_time:
	ldx send_num    
	lda timeh
	sta learn_index,x	
	incx
	lda timel	
	sta learn_index,x
	incx
	stx send_num
	rts
	
	
;interrupt vector
	ORG $fFF2
	FDB kbi	
	ORG $fFF4
	FDB tmi0
	ORG $fFF6
	FDB tmi1
	ORG $fFF8
	FDB int0	
	ORG $fFFA
	FDB int1	
	ORG $fFFC
	FDB swii
	
	ORG $fFFE
	FDB start