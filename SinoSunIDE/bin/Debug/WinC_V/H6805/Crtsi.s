;	C STARTUP FOR MC68HC05
;	WITH AUTOMATIC DATA INITIALISATION
;	Copyright (c) 1995 by COSMIC Software
;
	xref	_main, __memory, __idesc__
	xdef	_exit, __stext, c_h, c_reg
;
	switch	.text
__stext:
	rsp			; reset stack pointer
	lda	#$81		; RTS
	sta	c_h+2		; opcode in place
	lda	#$D6		; LDA IX2
	sta	c_h-1		; opcode in place
	clrx			; start index
ibcl:
	lda	__idesc__+2,x	; test flag byte
	beq	prog		; no more segment
	lda	__idesc__+1,x	; compute start
	sub	__idesc__+4,x	; offset by ram address
	sta	c_h+1		; in read vector
	lda	__idesc__,x	; because sharing
	sbc	#0		; the same
	sta	c_h		; index
	lda	__idesc__+6,x	; compute length
	sub	__idesc__+1,x	; of segment
	add	__idesc__+4,x	; ram end address
	sta	c_reg		; save for compare
	stx	c_reg+1		; save index
	ldx	__idesc__+4,x	; load ram address
dbcl:
	jsr	c_h-1		; load byte
	sta	0,x		; store it
	incx			; next byte
	cpx	c_reg		; end address
	bne	dbcl		; no, loop back
	lda	c_reg+1		; get back index
	add	#5		; next descriptor
	tax			; in place
	bra	ibcl		; and loop
prog:
	jsr	_main		; execute main
_exit:
	bra	_exit		; and stay here
;
;	area for external memory access
;
	switch	.ubsct
	ds.b	1		; opcode
c_h:
	ds.b	3		; MSB + LSB + rts
c_reg:
	ds.b	2		; extra accumulator
;
	end
