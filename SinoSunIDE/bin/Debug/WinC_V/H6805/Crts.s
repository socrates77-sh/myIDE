;	C STARTUP FOR MC68HC05
;	Copyright (c) 1995 by COSMIC Software
;
	xref	_main	;定义文件调用的外部函数
	xdef	_exit, __stext, c_h	;定义外部可以调用的函数

	switch	.text
__stext:
	lda	#$81		; RTS
	sta	c_h+2		; opcode in place
	rsp			; reset stack pointer
	jsr	_main		; execute main
_exit:
	bra	_exit		; and stay here

	area for external memory access

	switch	.ubsct
	ds.b	1		; opcode
c_h:
	ds.b	3		; MSB + LSB + rts
;
	end
