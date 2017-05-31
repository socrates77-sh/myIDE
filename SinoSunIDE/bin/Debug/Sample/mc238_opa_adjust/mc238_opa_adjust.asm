
include mc20p38.h

;DEFINE RAM $40~FF
R_TopTEMP EQU $40
R_temp1   EQU $41
OPACTEMP  EQU $42
opaof     equ $43
keycounter equ $44


sendcode      equ $45

OPACTEMP1 EQU $46

EOC DEFINE 3,ADCON
ADCE DEFINE 0,ADCON

KEY DEFINE 1,PCC
FREQ DEFINE 0,PCC

PST DEFINE 7,PPGC
C0CMPOP DEFINE 7,MCRX
C1CMPOP DEFINE 6,MCRX
C2CMPOP DEFINE 5,MCRX
OPAMPOP DEFINE 4,MCRX

C0COFM  DEFINE 7,CMP0C
C0CRS   DEFINE 6,CMP0C

C1COFM  DEFINE 7,CMP1C
C1CRS   DEFINE 6,CMP1C

C2COFM  DEFINE 7,CMP2C
C2CRS   DEFINE 6,CMP2C

OPAOFM  DEFINE 7,OPAC
OPARS   DEFINE 6,OPAC

CMP0EN  DEFINE 7,MCR
CMP1EN  DEFINE 6,MCR
CMP2EN  DEFINE 5,MCR
OPAEN   DEFINE 4,MCR

WDTC    DEFINE 0,MCR
PA0     DEFINE 0,PA
PA3			DEFINE   3,PA
PB3     DEFINE  3,PB


OPAOF5  DEFINE 3,PB
OPAOF4  DEFINE 2,PB
OPAOF3  DEFINE 0,PB
OPAOF2  DEFINE 0,PA
OPAOF1  DEFINE 1,PA
OPAOF0  DEFINE 2,PA



        org $3f00
reset:
        rsp
        nop
        bset    3,DDR2
        bclr    3,PB
        NOP
        nop
        bset    3,PB
        nop
        nop
        bclr    3,PB
        nop
        nop
        bset    3,PB
        nop
        nop
        bclr    3,PB
        
        
        
        jsr     cal_opa_auto
        bra *        
        
;===============================================тк╥е				
cal_opa_auto:
    lda         #$6d
    sta         NETCTL 
    bset    4,MCR   ; ENABLE OPA
    
				clr         opac
				bset		      opaofm	; set as offset adj model					
				bset		      opars
				jsr         delay100us
				lda		       mcrx
				and		       #%00010000
				sta		       r_temp1				
cal_opalp:		
        lda     opac
        and     #%00111111
        eor     #%00111111
        beq     cal_opa_error ; if OPAOF[5:0]=3F, jmp error
        
        inc     opac ;OFAOF[5:0] inc 1
        jsr     delay100us
        lda     mcrx
        and     #%00010000
        eor     r_temp1
        beq     cal_opalp
        
        bset    PB3 ;
        
        lda     opac
        and     #%00111111
        eor     #%00100000 ; if OPAOF[5:0]== 6'B100000,
        bne     lab_next
        bclr    5,OPAC
        bra     cal_opa_end
lab_next:
        brclr   5,OPAC,cal_opa_end
        dec     opac
        bra     cal_opa_end
cal_opa_error:
        bclr    PB3
cal_opa_end:
        bclr    OPAOFM ;EXIT OPA ADJ MODEL
        rts        
        
				rts


;delay funtion
delay100us:
        lda   #30
        sta   r_toptemp
delay100usloop:        
        nop
        nop
        nop
        nop
        dec   r_toptemp
        bne   delay100usloop
		rts		        
        
        org $3ffe
        fdb     reset