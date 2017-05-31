
;********ADAM27P16*************
;******delay600us是延时560us****
;******芯片：10p11 sop-16******
;******频率38K*****************
;******日期:2012.06.11*********
;******校验:ff02H**************

KEY	equ	$00
MCR	equ	$01
IOR	equ	$02

OUTC	define	5,MCR
KBIF	define	6,MCR
KBIE	define	7,MCR
LED_IO	define	0,IOR

R0	equ	$e1
R1	equ	$e2

USR_CODE_L	equ	$04
USR_CODE_H	equ	$cb
key_count	equ	$e3
key_value1	equ	$e4
key_value2	equ	$e5
key_buffer_l	equ	$e6
key_buffer_h	equ	$e7
send_bit_time	equ	$e8
;send_data1	equ	$e9
;send_data2	equ	$ea
send_data3	equ	$eb
send_data	equ	$ed
send_count	equ	$ee
key_value1_bak	equ	$ef
;send_bit1_count	equ	$eb

work_flag	equ	$e0
send_code_flag	define	0,work_flag
gnd_key_flag	define	1,work_flag
send_over_flag	define	2,work_flag


	org	$1ff4
	fdb	k_int

	org	$1ffc
	fdb	swi_int

	org	$1ffe
	fdb	reset

	org	$1c00

k_int:
	bclr	KBIF
swi_int:
	rti

reset:
	sei
	rsp
	bset	OUTC	;不发码
 bclr OUTC
 bset OUTC
 bclr OUTC
 ;jmp reset
	ldx	#$e0
clr_ram:
	clr	,x
	incx
	bne	clr_ram
	bset	OUTC	;不发码


	ldx	#255
loop_delay:			;等待上电稳定
	jsr	delay600us
	jsr	delay600us
	bset	OUTC	;不发码
	decx
	bne	loop_delay
	bset	send_code_flag
	bset	LED_IO
;***************************************
main_loop:
	sei
	rsp
 ;jsr sp_test
	lda	#$81
	sta	IOR
key_scan:
	lda	#13
	sta	R0

	ldx	#0
	bclr	KBIF	;重置

	clr	R0
	clr	key_count
	clr	key_value1
	clr	key_value2
	bclr	gnd_key_flag
scan_gnd:
	lda	KEY
	sta	key_buffer_l
	lda	MCR
	ora	#$e0
	sta	key_buffer_h

	cmp	#$ff
	bne	L000
	lda	key_buffer_l
	cmp	#$ff
	bne	L000

	lda	#13
	sta	key_count
	clr	R0
	bra	scan_loop
L000:
	lda	#13
	sta	R1
	bset	gnd_key_flag
	bra	get_key

scan_loop:
	sta	KEY		;开始扫描
	inc	R0
	nop	
	nop
;	ldx	R0
;	jsr	delay600us	;延时消抖 子函数用到了R0
;	stx	R0
	nop	
	lda	KEY
	sta	key_buffer_l
	lda	MCR
	ora	#$e0
	sta	key_buffer_h

	sta	KEY
	lda	#13
	sub	R0
	sta	R1

	ldx	R0
shift_loop:
	ror	key_buffer_h
	ror	key_buffer_l
	decx
	bne	shift_loop
get_key:
	inc	key_count		;键值
	brset	0,key_buffer_l,end_stor_value
	lda	key_count
	tst	key_value1
	bne	stor_value2
	sta	key_value1		;第一个按键存放在key_value1
	bra	end_stor_value
stor_value2:
	bra	mult_key	;无组合键
	tst	key_value2
	bne	mult_key		;多键处理(超过两个按键)
	sta	key_value2		;第二个按键存放在key_value2
end_stor_value:
	dec	R1
	beq	L1111
	ror	key_buffer_h
	ror	key_buffer_l
	bra	get_key
L1111:
	brset	gnd_key_flag,end_key_scan
	lda	R0
	cmp	#11
	blo	scan_loop
	bra	end_key_scan
mult_key:
	bset	send_code_flag
	clr	key_value1
	clr	key_value2
end_key_scan:
;***********************************************
check_key_value:
	bset	LED_IO		;关闭LED
	tst	key_value1
	;beq	end_key_deal	;key_value1为0,key_value2也应该为0
	bne	key_deal1
;******************no key*************
	bset	send_code_flag
	bset	OUTC	;不发码
	bclr	KBIF
	bset	KBIE
	rsp
	stop
	nop
	rsp
	bclr	KBIE
	lda	#$ff
	sta	key_value1_bak
	bra	end_key_deal
;****************single key********************
key_deal1:
	lda	key_value1
	cmp	key_value1_bak
	beq	end_key_deal

	sta	key_value1_bak
cmp_key_type:
	ldx	key_value1
	decx
	lda	key_value_table,x
	sta	send_data3
	inca
	beq	end_key_deal

	;lda	#$45
	;sta	send_data1		;45BAH
	;lda	#$BA
	;sta	send_data2
	bclr	send_code_flag
	bclr	send_over_flag
deal_send_code:
;******************************************
end_key_deal:
;***********************************************

;******************end debug***************	
	brclr	send_code_flag,star_send_code	;有发码标志则发码，没有跳出
	jmp	end_send_code
star_send_code:
	;clr	send_bit1_count

	;bclr	LED_IO			;发码则LED亮
	lda	#USR_CODE_L
	sta	send_data
	clr	send_count
	ldx	#$ff
loop_star1:
	jsr	send_38K
	decx
	bne	loop_star1
	ldx	#90
loop_star2:			;loop_star,loop_star2共9ms
	jsr	send_38K
	decx
	bne	loop_star2
	ldx	#4
	brset	send_over_flag,loop_star3	;已经发完成则只发头
	ldx	#8
loop_star3:	
	jsr	delay600us
	decx
	bne	loop_star3
	brclr	send_over_flag,send_code
	lda	#171		;发送剩余时间
	sta	R1		
	clr	send_data	;发送一个0
;**********************************
send_code:
	ldx	#21		;38K时间
	jsr	send_bit	;调用发码程序

	brset	send_over_flag,delay_remain_loop
	inc	send_count
	lda	send_count
	cmp	#33
	bhs	send_over
	cmp	#8
	beq	load_send_data2
	cmp	#16
	beq	load_send_data3
	cmp	#24
	beq	load_send_data4
	asr	send_data	;发送下一位数据
	bra	send_code

load_send_data2:
	;lda	send_data2
	lda	#USR_CODE_H
	sta	send_data
	bra	send_code
load_send_data3:
	lda	send_data3
	sta	send_data
	bra	send_code
load_send_data4:
	lda	send_data3
	coma
	sta	send_data
	bra	send_code
;*********************************
send_over:
	bset	send_over_flag
	lda	#56
delay_remain_time:
	sta	R1
delay_remain_loop:
	jsr	delay600us
	dec	R1
	bne	delay_remain_loop	
end_send_code:
	;bclr	send_code_flag	;一次按键发一次码

	jmp	main_loop
;********************************************

sp_test:
       nop
       lda #$aa
       sta R0
       jmp main_loop


delay600us:
	;lda	#147
	lda	#137
	sta	R0
delay_loop:			;(1.5+2.5)*137=548	//(1,5+2.5)*147=588
	dec	R0		;1.5us
	bne	delay_loop	;2.5us
end_delay600us:
	rts			;3us

;**********用bclr 和nop调节时间可以以0.5us的步进微调*******
send_38K:
	bclr	OUTC		;out put	;2.5us
	nop			;1us
	nop			;1us
	bclr	OUTC		;out put	;2.5us
	;nop			;2.5us
	bset	OUTC		;2.5us
	bset	OUTC		;2.5us
	bset	OUTC		;2.5us
	;bset	OUTC		;2.5us
	nop			;1us
	nop			;1us
	nop
end_send_38K:
	rts			;3us

send_bit:
	jsr	send_38K
	;dec	send_bit_time	;检测产生38K的时间
	decx
	bne	send_bit
	bset	OUTC
	ldx	#1
	brclr	0,send_data,loop_send_low
	ldx	#3
loop_send_low:
	jsr	delay600us	;3us
	decx
	bne	loop_send_low
	rts

	

key_value_table:
	fcb	$ff, $ff, $ff, $ff, $ff,	$ff, $ff, $ff, $ff, $ff	
	fcb	$ff, $ff, $ff 	;GND						;K1-k13
	fcb	$ff, $ff, $ff, $ff, $ff, 	$ff, $ff, $ff, $ff, $ff 
	fcb	$ff, $ff	;S0						;k14-k25
	fcb	$01, $5f, $16, $50, $42, 	$46, $09, $41, $58, $03		;k26-k36
	fcb	$ff		;S1
	fcb	$ff, $1a, $4c, $56, $07, 	$55, $45, $5a, $17, $ff	;S2	;k37-k46
	fcb	$1e, $ff, $ff, $ff, $5b, 	$57, $ff, $4f, $ff	;S3	;k47-k55
	fcb	$43, $5c, $12, $53, $4b, 	$54, $0e, $ff		;S4	;k56-k63
	fcb	$02, $0a, $59, $47, $1b, 	$06, $ff 		;S5	;k64-k70
	fcb	$05, $11, $1d, $19, $0d,	$ff 		;S6	;k70-K76
	fcb	$52, $44, $51, $13, $ff		;S7	;K77-K81
	fcb	$48, $4d, $0f, $ff		;S8	;K82-K85
	fcb	$4e, $4a, $ff	;S9	;K86-K88
	fcb	$15, $ff	;S10	;K89-90
	fcb	$ff		;S11	;K91
