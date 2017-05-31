PA	EQU	$00
PB	EQU	$01
PCC	EQU	$02
DDRA	EQU		$03
DDRB	EQU		$04
PAHCON	EQU		$05
PBHCON	EQU		$06
TDR0	EQU	$07
TLOAD0	EQU		$07
TCR0	EQU	$08
TDR1	EQU	$09
TLOAD1	EQU		$09
TCR1	EQU	$0A
TDR2	EQU	$0B
TLOAD2	EQU		$0B
TCR2	EQU		$0C
PPGC	EQU	$0D
PPGCNT	EQU	$0E
PPGT	EQU	$0E
NETCTL	EQU		$0F
INTDB	EQU		$10
CVREF	EQU		$11
PPGDL	EQU		$12
CMP0C	EQU	$13
CMP1C	EQU	$14
CMP2C	EQU	$15
OPAC	EQU	$16
INTC0	EQU	$17
INTC1	EQU	$18
ADCON	EQU	$19
ADCM	EQU	$1A
ADDATAH	EQU		$1B
ADDATAL	EQU		$1C
MCR	EQU	$1D
RSTFR	EQU	$1E
MCRX EQU $1F
;DEFINE RAM $40~FF
R_TopTEMP EQU $40
R_temp1   EQU $41
OPACTEMP  EQU $42
opaof     equ $43
keycounter equ $44

sendcode      equ $45

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
PA2		DEFINE 2,PA
;PB7,PB6,PB3~PB0输出OPA校准值
;key按键按一次校准值加一

ORG $2000
POWER_ON:
         bset wdtc
		 CLR TCR0
		 ;CLR TCR1
		 CLR TCR2
		 LDA #%11111111
		 STA DDRA
		 CLR PAHCON
		 LDA #%11111111
		 STA DDRB
		 CLR PBHCON
		 
;		 brclr 3,rstfr,inirst
		
;         bclr 3,rstfr
;		 brset freq,clrfreq
;		 bset freq
;		 bra next
;clrfreq:
;         bclr freq
;         bra next
;inirst:		 
		 LDA #%00100100
		 STA PCC

;next:		 
		 CLR INTC0
		 CLR INTC1
		 ;CLR PPGC 
		 LDA #$6D
		 STA NETCTL
		 ;CLR INTDB
		 LDA #$FF
		 STA CVREF
		 
		 ;CLR PPGDL
		 ;CLR CMP0C
		 ;CLR CMP1C
		 ;CLR CMP2C
		 ;CLR OPAC
		 CLR MCR
		 ;CLR MCRX
		 ;CLR RSTFR
		 ;LDA #%00000110
		 ;STA ADCON
		 CLR ADCON
		 ;LDA #%00000001
		 ;STA ADCM
		 CLR ADCM
		 
         CLR PA
		 CLR PB
		 
SET_PPG:
         LDA #%01111111
		 STA PPGC
		 LDA #$FF
		 STA PPGT
         LDA #%11111111
		 STA INTDB
		 LDA #%11111111
		 STA PPGDL
		 LDA #$FF
		 STA TDR1
		 LDA #%11111111
		 STA TCR1
         
		 ;BSET CMP0EN
		 ;BSET CMP1EN
		 ;BSET CMP2EN
		 BSET OPAEN
		 
		 ;JSR CAL_C0
         ;JSR CAL_C1
		 ;JSR CAL_C2



		 JSR CAL_OPA_AUTO
         
		 LDA OPAC
		 AND #$30
		 LSLA
		 LSLA
		 STA OPACTEMP
		 LDA OPAC
		 AND #$0F
		 ora OPACTEMP
		 STA PB
		 
		 clr opaof
		 clr keycounter

		 LDA #%00000110
		 STA ADCON
		 LDA #%00000001
		 STA ADCM
		 
;FREQLOOP2:	
FREQLOOP: 

          BSET ADCE      ;START AD CONVERT
		  NOP
 AD_CLOOP:
          BRCLR EOC,AD_CLOOP  ;AD CONVERTED END?
          
		  JSR DELAY100US
		  lda ADDATAH
		  JSR SendAD2Pa0
          lda ADDATAL
		  JSR SendAD2Pa0
          JSR DELAY100US
		  
          BRSET FREQ,CLR_FREQ;5
		  BSET FREQ;5
		  BRA keyscan;3
CLR_FREQ:
          BCLR FREQ;5
          BRA keyscan;3

keyscan:
          bset wdtc
		  brset key,keydo
		  lda keycounter
		  cmp #250
		  bhi freqloop
		  inc keycounter
		  bra freqloop

keydo:
          lda keycounter
		  cmp #240
		  bhi opa_key
		  clr keycounter
		  bra freqloop

opa_key:
		 clr keycounter
		 JSR CAL_OPA_KEY
		 LDA OPAC
		 AND #$30
		 LSLA
		 LSLA
		 STA OPACTEMP
		 LDA OPAC
		 AND #$0F
		 ora OPACTEMP
		 STA PB         
         inc opaof
		 bra freqloop
		  

;CMP_C0:		  
;		  BRSET C0CMPOP,BSET_PB2
;		  BCLR PB2
;		  BRA  CMP_C1
;BSET_PB2:		  
;		  BSET PB2
;		  BRA  CMP_C1
;CMP_C1:		  
;		  BRSET C1CMPOP,BSET_PB3
;		  BCLR PB3
;		  BRA  CMP_C2
;BSET_PB3:		  
;		  BSET PB3
;		  BRA  CMP_C2
;CMP_C2:		  
;		  BRSET C2CMPOP,BSET_PB7
;		  BCLR PB7
;		  BRA  CMP_END
;BSET_PB7:		  
;		  BSET PB7
;		  BRA  CMP_END
;CMP_END:
		  
;		  BRSET KEY,FREQLOOP;5
;          BSET PST
;		  BRA FREQLOOP2;3


;===============================================比较器
;CAL_C0:
;				CLR    CMP0C
;				BSET		C0COFM						
;				BSET		C0CRS
;				JSR  DELAY100US
;				LDA		MCRX
;				AND		#%10000000
;				STA		R_temp1				
;CAL_C0LP:		
;				INC		CMP0C
;				JSR  DELAY100US
;				LDA		CMP0C
;				AND		#%00111111
;				EOR		#%00111111
;				BEQ     CAL_C0_END
;				JSR  DELAY100US
;				LDA		MCRX				
;				AND		#%10000000
;				EOR		R_temp1
;				BEQ     CAL_C0LP				
;CAL_C0_END:				
;				BCLR		C0COFM	
;				RTS				
;				
				

;CAL_C1:
;				CLR    CMP1C
;				BSET		C1COFM						
;				BSET		C1CRS
;				JSR  DELAY100US
;				LDA		MCRX
;				AND		#%01000000
;				STA		R_temp1				
;CAL_C1LP:		
;				INC		CMP1C
;				JSR  DELAY100US
;				LDA		CMP1C
;				AND		#%00111111
;				EOR		#%00111111
;				BEQ     CAL_C1_END
;				JSR  DELAY100US
;				LDA		MCRX				
;				AND		#%01000000
;				EOR		R_temp1
;				BEQ     CAL_C1LP				
;CAL_C1_END:				
;				BCLR		C1COFM	
;				RTS				
				
				
;CAL_C2:
;				CLR    CMP2C
;				BSET		C2COFM						
;				BSET		C2CRS
;				JSR  DELAY100US
;				LDA		MCRX
;				AND		#%00100000
;				STA		R_temp1				
;CAL_C2LP:		
;				INC		CMP2C
;				JSR  DELAY100US
;				LDA		CMP2C
;				AND		#%00111111
;				EOR		#%00111111
;				BEQ     CAL_C2_END
;				JSR  DELAY100US
;				LDA		MCRX				
;				AND		#%00100000
;				EOR		R_temp1
;				BEQ     CAL_C2LP				
;CAL_C2_END:				
;				BCLR		C2COFM	
;				RTS				
				
;===============================================运放
;*************************************************
;*	MC20P38 OPA自动校准例程
;*	2012-09-11
;*	Liang Huifeng
;*************************************************				
CAL_OPA_AUTO:
				CLR    OPAC;设置OPAOF[5:0]=00
				BSET		OPAOFM;S3闭合，进入输入失调消除模式						
				BSET		OPARS;S2闭合，选择OPAP作为校准基准端

				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US 
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US    
				LDA		MCRX
				AND		#%00010000
				STA		R_temp1;读取OPAO当前状态				
CAL_OPALP:		
				LDA		OPAC;判断OPAOF[5:0]=3F仍未检测到OPAO变化则视为校准失败
				AND		#%00111111
				EOR		#%00111111
				BEQ     CAL_OPA_ERROR
				
				INC		OPAC;设置OPAOF[5:0]=00
				JSR 	DELAY100US;等待至少100US
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US 
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US
				JSR  DELAY100US;等待至少100US
    JSR  DELAY100US;等待至少100US
				LDA		MCRX				
				AND		#%00010000
				EOR		R_temp1;判读OPAO是否发生变化
				BEQ     CAL_OPALP	
				
				BSET    PA2;校准成功，PA2输出高
				
                BRCLR	5,OPAC,CAL_OPA_END; 判断OPAOF[5]，如果OPAOF[5]=0，则将当前值为校准值，
                DEC		OPAC;如果OPAOF[5]=1，则将当前值减1后为校准值
				BRA		CAL_OPA_END
CAL_OPA_ERROR:
                BCLR   PA2;校准失败，PA2输出低电平
CAL_OPA_END:
				BCLR		OPAOFM;退出输入失调消除模式	
				RTS				


                


CAL_OPA_KEY:
				;CLR    OPAC
				lda opaof
				and #$3f
				sta opac
				BSET		OPAOFM						
				BSET		OPARS
				JSR  DELAY100US
				;LDA		MCRX
				;AND		#%00010000
				;STA		R_temp1				
;CAL_OPALP_KEY:		
				;INC		OPAC
				;JSR  DELAY100US
				;LDA		OPAC
				;AND		#%00111111
				;EOR		#%00111111
				;BEQ     CAL_OPA_END_KEY
				;JSR  DELAY100US
				;LDA		MCRX				
				;AND		#%00010000
				;EOR		R_temp1
				;BEQ     CAL_OPALP_KEY				
;CAL_OPA_END_KEY:				
				BCLR		OPAOFM	
				RTS	

	
DELAY100US:
        LDA   #$FF
        STA   R_TopTEMP
DELAY100USLOOP:        
        NOP
        NOP
        NOP
        NOP
        DEC   R_TopTEMP
        BNE   DELAY100USLOOP
		RTS	 

SendAD2Pa0:
        STA sendcode
		ldx #8		 
outPA0loop:
		 brset 7,sendcode,bsetPA0out
		 bset PA0
		 bclr PA0
		 bclr PA0
		 bclr PA0
		 bclr PA0
		 bra  outPA0loop1
bsetPA0out:
         bset PA0
		 bset PA0
		 bset PA0
		 bset PA0
		 bclr PA0
		 bra  outPA0loop1
outPA0loop1:		 
		 lsL sendcode
		 decx
		 bne outPA0loop		

        RTS

		 
         ORG $3FFE
   		 FDB POWER_ON