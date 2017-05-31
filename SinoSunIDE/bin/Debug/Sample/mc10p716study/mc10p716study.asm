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

time_def equ $7f

set_learn_num equ $44

	ORG $c000
start:
	lda #$ff
	sta ddr0
			
ram_clrl:          ;清低RAM
	ldx #$c0
ram_lpl:	
	clr ,x
	incx
	bne ram_lpl

ram_clrh:          ;清高RAM
	clrx
	lda #$ff
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
	lda #time_def         ;timer0 1分频 定时65535*0.25u=16ms	
	sta tload0h
	lda #time_def
	sta tload0l
	lda #$01        ;tlath tlatl记录IR上升沿时间，tlatll记录IR下升沿时间
	sta tcr0
	
	clr tdata1
	lda #5
	sta tload1			
	lda #$02				;timer1 外部计数模式(IR下降沿计数)，记录4个载波时间长度
	sta tcr1
	
	bset 2,tcr0     ;timer0开启
	bset 2,tcr1     ;timer1开启
	
	bclr 3,tcr0
	
	lda #$80        ;IRIN上升沿中断使能
	sta intc
	
	clr send_num
 
	cli	
	;学习模式
	;学习模式开始后，timer0处于定时模式，每16ms溢出进入中断1次,用于产生时基
	;波形开始后时IRIN上升沿重载timer0，并产生int0中断进入int0中断服务程序，清int0中断标志
	;在大约70u内不停读取int0中断标志，为高表示带载波,否则为无载波
	;如果为无载波，则只用int0中断上降沿记录每次的时间直到学习结束
	;如果在带载波，关闭int1中断，等4次计数结束进入tmr1中断服务程序
	;tmr1中断中计算载波宽度，将载波宽度赋值到t1load，并将timer1设置成包络模式，开启int1下升沿中断使能
	;int1中断中记录每次的时间，同时改变tlath/tlatl的锁存沿和int1的中断触发沿直到学习结束
	
learn_ing:
	brset learn_err,flag0,learn_error
	brclr learn_end,flag0,learn_ing
	
	;发码模式
	;带载波，timer1配置成pwm模式输出载波信号，开关受timer0中断信号控制，timer0控制时间，mcr配置irout由pwm输出
	;timer0为自加载计数器，开启时写不会立即生效，在溢出后才加载，所以时间要提前一个中断中写
	;如果定时时间超过16ms，先计时超过部分，然后关闭TMR0EN，关闭timer0触发timer1开关，等超出部分计时完毕再打开
	;timer1同上，只是需要将TCLR设置成1，使pwm处于自关闭状态
	
	bclr 7,opa                    ;关闭opa
 bset 5,mcr
 bclr 0,p0

send_loop:
 lda send_num                  ;把发码个数暂存,用于判断发码结束
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
	lda #$40                      ;无载波脉冲设置
	sta tcr1	
	bset 0,tcr1                   ;自关闭模式
	bra send_start	
carry_on:	
	lda times
	jsr set_pwm	
	lda #$40                      ;带载波脉冲设置
	sta tcr1	
send_start:
	lda #$01 
	sta tload0h
 lda #$20
	sta tload0l                    ;tload0h带缓冲，写tload0l将把tload0h和tload0l的值同时置入到预加载寄存器中	
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
	bra send_loop                            ;此处添加其他程序					

set_pwm:                           ;设置载波周期
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
	inc t_16ms                          ;信号等待定时
	lda t_16ms
	cmp #200
	bne tmi0_end
	clr t_16ms
	inc t_3s
	lda t_3s
	cmp #40
	bne tmi0_end
	clr t_3s                            ;2分钟无信号超时
	;bset learn_err,flag0                ;置位学习错误标志
tmi0_end:		
	rti
tmi0_learn:                           ;学习过程中溢出，在数列中插入特殊值FF
	inc send_num	
tmi0_learn_index_next:	
	inc t_16ms                         
	lda t_16ms		
	cmp #20
	beq tmi0_learn_end	                ;最长学习320ms
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

tmi0_send:                           ;发码	
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
	bset 2,tcr1	                        ;开启溢出触发	
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

tmi0_send_next:                       ;是否发完	
	ldx send_num
	cpx learn_num
	beq tmi0_send_end
	rti	
tmi0_send_end:                        ;发码结束
	bclr send_begin,flag0
	bset send_end,flag0
	rti		



		
tmi1:	                          ;4个载波脉冲计数溢出中断，设置去包络时间      
	lda tlatl
	ldx tlath
	sta timel
	stx timeh       
	         
	lda times                      ;计算载波周期
	sub timel
	sta times
	lda #time_def
	sbc timeh
	lsra                          ;除2
	ror times
	lsra                          ;除2
	ror times
	lda times                     ;设置载波周期
	add #5
	jsr set_pwm

 ;lda #$f0
 ;sta tload1
 
	lda #$41                      ;包络模式
	sta tcr1
	bset 2,tcr1
	bclr 0,tcr0                   ;上升沿采样
	bset 1,tcr0                   ;触发选择IRINT
	lda #$0a                      ;开启包络信号IRINT中断
	sta intc	
	bclr 3,tcr0                   ;允许触发重载
	rti	

int0:																   ;IRIN第一个下降沿触发进入中断，timer0自动重载
	lda tlatl
	ldx tlath
	sta timel
	stx timeh  
	bclr 4,intc
 bset 0,p0
	brset no_carry,flag0,int0_no_carry_end	;无载波分支
	bset learn_begin,flag0
	lda #set_learn_num                             ;学习个数
	sta learn_num
	brset 4,intc,int0_carry							 ;波形开始后70u中读取标志位，判断有无载波
	lda #6
delay_70us:
	brset 4,intc,int0_carry							
	deca
	bne delay_70us
	brclr 4,intc,int0_no_carry           
int0_carry:														 ;带载波设置
	lda tlatll                           ;记录脉冲宽度
	sta times
	bclr no_carry,flag0                  ;带载波	
	bclr 7,intc													 ;关闭IRIN中断	
	bclr 4,intc													 ;清int0中断标志
	bclr 0,tcr0                          ;改变tlat采样沿口
	rti
int0_no_carry:
	bset no_carry,flag0                  ;不带载波
	lda tlatll                           ;记录脉冲宽度
	sta times
	bset 6,tcr1												   ;关闭tmi1中断
	bclr 3,tcr0                          ;允许触发重载
	rti
int0_no_carry_end:										 ;判断无载波码型是否学习结束
	jsr save_time	
	bclr 3,tcr0                          ;允许触发重载
	dec learn_num	                              
	beq int0_learn_end	
	rti
int0_learn_end:
	bset learn_end,flag0      
	bclr learn_begin,flag0           
	bclr 7,intc                          ;无载波码型学习完成，关闭中断
	bclr 4,intc
	rti

int1:                               ;记录带载波码型翻转时间
	lda tlatl
	ldx tlath
	sta timel
	stx timeh  
	bclr 0,intc

 lda #$02
 eor p0
 sta p0
 
	jsr save_time
	dec learn_num       							;判断带载波码型是否学习结束
	beq int1_end
	brset 1,intc,code_posedge         ;改变tlat采样沿口和IRIN中断沿口
code_negedge:
	bset 1,intc                       
	bclr 0,tcr0
	bclr 3,tcr0                          ;允许触发重载	
	rti
code_posedge:	
	bclr 1,intc
	bset 0,tcr0
	bclr 3,tcr0                          ;允许触发重载	
	rti	
int1_end:
	bset learn_end,flag0
	bclr learn_begin,flag0 
	clr intc	                         ;带载波码型学习完成，关闭中断
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