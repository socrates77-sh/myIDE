
IF X == 0
EXTERN DATA X
ENDIF

IF H == 0
EXTERN DATA H
ENDIF

IF L == 0
EXTERN DATA L
ENDIF

DECLARE_INIT_GLOBAL = 0

INIT@global MACRO name, addr, size	
IF DECLARE_INIT_GLOBAL == 0
	DECLARE_INIT_GLOBAL = 1
	extern code __INIT@global_
ENDIF
	
	B0MOV Y, #(addr)$M
	B0MOV Z, #(addr)$L
	
IF H == 0
	MOV A, #(name)$M
	B0MOV H, A
ELSE
	B0MOV H, #(name)$M
ENDIF	

IF L == 0
	MOV A, #(name)$L
	B0MOV L, A
ELSE
	B0MOV L, #(name)$L
ENDIF		

IF X == 0
	MOV A, #(size)
	B0MOV X, A
ELSE
	B0MOV X, #(size)
ENDIF

	CALL __INIT@global_
ENDM


;-------------------------------------
; Change selecting bank by bank(name)
;-------------------------------------
__SelectBANK MACRO name
	IF RBANK != 0
	B0MOV RBANK, #(BANK (name))
	ENDIF	
ENDM

;-------------------------------------
; Change selecting bank by bank const#
;-------------------------------------
__SelectBANKCNST MACRO name
	IF RBANK != 0
	B0MOV RBANK, #(name)
	ENDIF
ENDM

_DEFINE_ISRBACKUP_DATA MACRO mode
_InterruptBackupData SEGMENT DATA BANK 0 INBANK COMMON
	_bufT ds 1
	_bufI ds 1
IF mode == 0	//backup 0x80 - 0x87 and a
	_bufA ds 1
	_bufY ds 1
	_bufZ ds 1
	_bufR ds 1
	_bufPFLAG ds 1		
IF H != 0
	_bufH ds 1
ENDIF
IF L != 0
	_bufL ds 1
ENDIF	
IF X != 0
	_bufX ds 1
ENDIF
IF RBANK != 0
	_bufRBANK ds 1
ENDIF
ENDIF //push0 end

IF mode == 1	//backup a
	_bufA ds 1
ENDIF //push1 end

IF mode == 2	//backup 0x80 - 0x87 except PFLAG
	_bufY ds 1
	_bufZ ds 1
	_bufR ds 1	
IF H != 0
	_bufH ds 1
ENDIF
IF L != 0
	_bufL ds 1
ENDIF	
IF X != 0
	_bufX ds 1
ENDIF
IF RBANK != 0
	_bufRBANK ds 1
ENDIF
ENDIF //push2 end

ENDM

__PUSH_REG MACRO mode
IF mode == 0	//backup 0x80 - 0x87 and a
	B0MOV _bufA, A
	B0MOV A, PFLAG
	B0MOV _bufPFLAG, A	
IF RBANK != 0
	B0MOV A, RBANK
	B0MOV _bufRBANK, A
ENDIF		
IF H != 0
	B0MOV A, H
	B0MOV _bufH, A
ENDIF
IF L != 0
	B0MOV A, L
	B0MOV _bufL, A
ENDIF	
IF X != 0
	B0MOV A, X
	B0MOV _bufX, A
ENDIF
	B0MOV A, Y
	B0MOV _bufY, A
	B0MOV A, Z
	B0MOV _bufZ, A
	B0MOV A, R
	B0MOV _bufR, A
ENDIF //push0 end

IF mode == 1	//backup a
	PUSH
	B0MOV _bufA, A
ENDIF //push1 end

IF mode == 2	////backup 0x80 - 0x87 except PFLAG
	PUSH
IF RBANK != 0
	B0MOV A, RBANK
	B0MOV _bufRBANK, A
ENDIF		
IF H != 0
	B0MOV A, H
	B0MOV _bufH, A
ENDIF
IF L != 0
	B0MOV A, L
	B0MOV _bufL, A
ENDIF	
IF X != 0
	B0MOV A, X
	B0MOV _bufX, A
ENDIF
	B0MOV A, Y
	B0MOV _bufY, A
	B0MOV A, Z
	B0MOV _bufZ, A
	B0MOV A, R
	B0MOV _bufR, A
ENDIF //push2 end

	B0MOV A, T
	B0MOV _bufT, A
	B0MOV A, I
	B0MOV _bufI, A
	__SelectBANKCNST 0
ENDM

__POP_REG MACRO mode
	B0MOV A, _bufT
	B0MOV T, A
	B0MOV A, _bufI
	B0MOV I, A
IF mode == 0	//pop 0x80 - 0x87 and a	
IF H != 0
	B0MOV A, _bufH
	B0MOV H, A
ENDIF
IF L != 0
	B0MOV A, _bufL	
	B0MOV L, A
ENDIF	
IF X != 0
	B0MOV A, _bufX	
	B0MOV X, A
ENDIF
	B0MOV A, _bufY
	B0MOV Y, A
	B0MOV A, _bufZ
	B0MOV Z, A
	B0MOV A, _bufR
	B0MOV R, A
	
IF RBANK != 0
	B0MOV A, _bufRBANK
	B0MOV RBANK, A
ENDIF	

	B0MOV A, _bufPFLAG
	B0MOV PFLAG, A		
	B0MOV A, _bufA
ENDIF //pop0 end

IF mode == 1	//pop a
	B0MOV A, _bufA
	POP
ENDIF //pop1 end

IF mode == 2	////pop 0x80 - 0x87 except PFLAG
IF H != 0
	B0MOV A, _bufH
	B0MOV H, A
ENDIF
IF L != 0
	B0MOV A, _bufL	
	B0MOV L, A
ENDIF	
IF X != 0
	B0MOV A, _bufX	
	B0MOV X, A
ENDIF
	B0MOV A, _bufY
	B0MOV Y, A
	B0MOV A, _bufZ
	B0MOV Z, A
	B0MOV A, _bufR
	B0MOV R, A	
IF RBANK != 0
	B0MOV A, _bufRBANK
	B0MOV RBANK, A
ENDIF	
	POP
ENDIF //pop2 end
ENDM

JEQ	MACRO 	addr
	B0BTS0	FZ
	JMP	addr
ENDM

JNE	MACRO 	addr
	b0bts1	FZ
	JMP	addr
ENDM
	
JGE	MACRO 	addr
	b0bts0	FC
	JMP	addr
ENDM		
	
JLT	MACRO 	addr
	B0BTS1	FC
	JMP	addr
ENDM

JGT	MACRO 	addr
	LOCAL laddr
	B0BTS0	FZ
	JMP	laddr
	b0bts0	FC
	JMP	addr
	laddr:	
ENDM		
	
JLE	MACRO 	addr
	B0BTS0	FZ
	JMP	addr	
	B0BTS1	FC
	JMP	addr
ENDM

JZ	MACRO 	adr
	b0bts0	FZ
	JMP	adr
ENDM

JNZ	MACRO 	adr
	b0bts1	FZ
	JMP	adr
ENDM

JC	macro	adr
	b0bts0	FC
	JMP	adr
ENDM

JNC	MACRO 	adr
	b0bts1	FC
	JMP	adr
ENDM

SHL	MACRO 	mem
	b0bclr	FC
	rlcm	mem
ENDM

SHR	MACRO 	mem
	b0bclr	FC
	rrcm	mem
ENDM

;----------------------------------------------------------------------
;
;Provide Empty Loop macro and LOG for C & Assembler solution 
;----------------------------------------------------------------------

_NOP macro num
IFNB num
	REPEAT num
	NOP
	ENDM
ELSE
	NOP
ENDIF

ENDM


;----------------------------------------------------------------------
;  Provide C delay function
;----------------------------------------------------------------------

_Delay  macro N
IF _EXTERN_DELAY_CODE == 0
_EXTERN_DELAY_CODE = 1
		extern code __Delay
ENDIF
  IF N>8
		IF N%3 == 0 
			 B0MOV R,#(N/3 -2)
			 CALL __Delay		
		ELSEIF N%3 == 1
		     _NOP 1
			 B0MOV R,#((N-7)/3)
			 CALL __Delay			
		ELSEIF N%3 == 2
		     _NOP 2
			 B0MOV R,#((N-8)/3)
			 CALL __Delay
		ENDIF
  ELSE
  	_NOP N
 ENDIF
 ENDM
 
__CALLHL MACRO fptr
LOCAL _CALLHL_RET
	B0MOV A, STKP
IFDEF FSTKPB2  //the stack level
	AND A, #0x7
ELSE
	AND A, #0x3
ENDIF
	B0MOV Z, A

IFDEF FSTKPB2
	ADD A, #0xEE
ELSE
	ADD A, #0xF6
ENDIF
	B0ADD Z, A
	B0MOV Y, #0
	
	;write target address to stack
IF RBANK != 0
	B0MOV RBANK, #(fptr)$M
ENDIF	
	MOV A, (fptr)
	B0MOV @YZ, A
	__SelectBANKCNST 0
	INCMS	Z
IF RBANK != 0
	B0MOV RBANK, #(fptr)$M
ENDIF
	MOV A, (fptr)+1
	AND A, #0x7f
	B0MOV @YZ, A
	
;Write return address to stack
	__SelectBANKCNST 0
	INCMS	Z
	MOV A, #(_CALLHL_RET$L)
	B0MOV @YZ, A
	INCMS	Z
	MOV A, #(_CALLHL_RET$M)
	B0MOV @YZ, A
; STKP -= 2
	MOV A, #0xfe
	B0ADD STKP, A
; jump to target
	RET
_CALLHL_RET:	
ENDM

__CALLHLCST MACRO fptr
LOCAL _CALLHL_RET
	B0MOV A, STKP
IFDEF FSTKPB2  //the stack level
	AND A, #0x7
ELSE
	AND A, #0x3
ENDIF
	B0MOV Z, A

IFDEF FSTKPB2
	ADD A, #0xEE
ELSE
	ADD A, #0xF6
ENDIF
	B0ADD Z, A
	B0MOV Y, #0
	
	;write target address to stack
	MOV A, #(fptr &  0xff)
	B0MOV @YZ, A
	__SelectBANKCNST 0
	INCMS	Z
	MOV A, #(fptr >> 0x08)
	AND A, #0x7f
	B0MOV @YZ, A
	
;Write return address to stack
	__SelectBANKCNST 0
	INCMS	Z
	MOV A, #(_CALLHL_RET$L)
	B0MOV @YZ, A
	INCMS	Z
	MOV A, #(_CALLHL_RET$M)
	B0MOV @YZ, A
; STKP -= 2
	MOV A, #0xfe
	B0ADD STKP, A
; jump to target
	RET
_CALLHL_RET:	
ENDM