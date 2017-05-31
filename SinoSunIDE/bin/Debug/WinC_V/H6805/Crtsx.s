;	C STARTUP FOR MC68HC05
;	WITH AUTOMATIC DATA INITIALISATION
;	Copyright (c) 1996 by COSMIC Software
;
	xref	_main, __memory, __idesc__
	xdef	_exit, __stext, c_h, c_reg
;
	switch	.text
__stext:
	rsp			; reset stack pointer
	lda	#$81		; RTS
	sta	c_h+5		; opcode in place
	lda	#$C6		; LDA EXT
	sta	c_h-1		; opcode in place
	inca			; STA EXT
	sta	c_h+2		; opcode in place
	lda	__idesc__	; move start
	sta	c_h		; in load
	lda	__idesc__+1	; vector
	sta	c_h+1
	clrx			; start index
ibcl:
	lda	__idesc__+2,x	; test flag byte
	beq	prog		; no more segment
	lda	__idesc__+3,x	; move ram
	sta	c_h+3		; address in
	lda	__idesc__+4,x	; store
	sta	c_h+4		; vector
dbcl:
	jsr	c_h-1		; copy byte
	inc	c_h+4		; increment
	bne	dok		; destination
	inc	c_h+3		; address
dok:
	inc	c_h+1		; increment
	bne	sok		; source
	inc	c_h		; address
sok:
	lda	c_h+1		; compare
	cmp	__idesc__+6,x	; source
	bne	dbcl		; address
	lda	c_h		; against
	cmp	__idesc__+5,x	; end address
	bne	dbcl		; and loop back
	txa			; compute
	add	#5		; next descriptor
	tax			; index
	bra	ibcl		; and loop
prog:
	lda	#$81		; RTS
	sta	c_h+2		; in place
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
	ds.b	3		; extra accumulator
;
	end
