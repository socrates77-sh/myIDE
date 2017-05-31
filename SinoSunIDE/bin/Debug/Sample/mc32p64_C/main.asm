;--------------------------------------------------------
; File Created by SN-SDCC : ANSI-C Compiler
; Version 0.0.4 (Jan  7 2016) (MINGW32)
; This file was generated Thu Apr 28 13:49:33 2016
;--------------------------------------------------------
; MC3X port for the RISC core
;--------------------------------------------------------
;	.file	"main.c"
	list	p=3264
	radix dec
	include "3264.inc"
;--------------------------------------------------------
; external declarations
;--------------------------------------------------------
	extern	_STATUSbits
	extern	_MCRbits
	extern	_INTEbits
	extern	_INTFbits
	extern	_OSCMbits
	extern	_IOP0bits
	extern	_OEP0bits
	extern	_PUP0bits
	extern	_IOP1bits
	extern	_OEP1bits
	extern	_PUP1bits
	extern	_IOP2bits
	extern	_OEP2bits
	extern	_PUP2bits
	extern	_IOP3bits
	extern	_OEP3bits
	extern	_PUP3bits
	extern	_T0CRbits
	extern	_T1CRbits
	extern	_T2CRbits
	extern	_TK0CRHbits
	extern	_TK0CRLbits
	extern	_TK1CRHbits
	extern	_TK1CRLbits
	extern	_TK2CRHbits
	extern	_TK2CRLbits
	extern	_ADCR0bits
	extern	_ADCR1bits
	extern	_I2CCRbits
	extern	_INDF
	extern	_INDF0
	extern	_INDF1
	extern	_INDF2
	extern	_HIBYTE
	extern	_FSR
	extern	_FSR0
	extern	_FSR1
	extern	_PCL
	extern	_STATUS
	extern	_MCR
	extern	_INDF3
	extern	_INTE
	extern	_INTF
	extern	_OSCM
	extern	_IOP0
	extern	_OEP0
	extern	_PUP0
	extern	_IOP1
	extern	_OEP1
	extern	_PUP1
	extern	_IOP2
	extern	_OEP2
	extern	_PUP2
	extern	_IOP3
	extern	_OEP3
	extern	_PUP3
	extern	_T0CR
	extern	_T0CNT
	extern	_T0LOAD
	extern	_T0DATA
	extern	_T1CR
	extern	_T1CNT
	extern	_T1LOAD
	extern	_T1DATA
	extern	_T2CR
	extern	_T2CNTH
	extern	_T2CNTL
	extern	_T2LOADH
	extern	_T2LOADL
	extern	_TK0CRH
	extern	_TK0CRL
	extern	_TK0CNTH
	extern	_TK0CNTL
	extern	_TK1CRH
	extern	_TK1CRL
	extern	_TK1CNTH
	extern	_TK1CNTL
	extern	_TK2CRH
	extern	_TK2CRL
	extern	_TK2CNTH
	extern	_TK2CNTL
	extern	_ADCR0
	extern	_ADCR1
	extern	_ADRH
	extern	_ADRL
	extern	_I2CCR
	extern	_I2CADDR
	extern	_I2CDATA
;--------------------------------------------------------
; global declarations
;--------------------------------------------------------
	global	_Delay_1ms
	global	_Send_byte
	global	_Send_byte_P23
	global	_int_isr
	global	_main
	global	_Read32p64
	global	_flag1
	global	_flag2
	global	_flag3
	global	_flag4
	global	_data
	global	_sdi_byte
	global	_count_bit
	global	_count_byte
	global	_need_DealData
	global	_need_test
	global	_checksum
	global	_i
	global	_j
	global	_temp1
	global	_temp2
	global	_temp
	global	_MCU_ID
	global	_OS_result
	global	_need_answer
	global	_sdo_byte

	global STK06
	global STK05
	global STK04
	global STK03
	global STK02
	global STK01
	global STK00

sharebank udata_ovr 0x0000
STK06	res 1
STK05	res 1
STK04	res 1
STK03	res 1
STK02	res 1
STK01	res 1
STK00	res 1

;--------------------------------------------------------
; global definitions
;--------------------------------------------------------
UD_main_0	udata
_flag1	res	1

UD_main_1	udata
_flag2	res	1

UD_main_2	udata
_flag3	res	1

UD_main_3	udata
_flag4	res	1

UD_main_4	udata
_data	res	50

UD_main_5	udata
_sdi_byte	res	1

UD_main_6	udata
_count_bit	res	1

UD_main_7	udata
_count_byte	res	1

UD_main_8	udata
_need_DealData	res	1

UD_main_9	udata
_need_test	res	1

UD_main_10	udata
_checksum	res	1

UD_main_11	udata
_i	res	1

UD_main_12	udata
_j	res	1

UD_main_13	udata
_temp1	res	1

UD_main_14	udata
_temp2	res	1

UD_main_15	udata
_temp	res	1

UD_main_16	udata
_MCU_ID	res	1

UD_main_17	udata
_OS_result	res	1

UD_main_18	udata
_need_answer	res	1

UD_main_19	udata
_sdo_byte	res	1

;--------------------------------------------------------
; absolute symbol definitions
;--------------------------------------------------------
;--------------------------------------------------------
; compiler-defined variables
;--------------------------------------------------------
UDL_main_0	udata
r0x1048	res	1
r0x1049	res	1
r0x1047	res	1
r0x1046	res	1
r0x1043	res	1
r0x1044	res	1
r0x1045	res	1
r0x1040	res	1
r0x1041	res	1
r0x1042	res	1
r0x103E	res	1
r0x103F	res	1
r0x103B	res	1
r0x103C	res	1
r0x103D	res	1
;--------------------------------------------------------
; initialized data
;--------------------------------------------------------

;@Allocation info for local variables in function 'int_isr'
;@int_isr Send_byte_P23             Allocated to registers ;size:2
;@int_isr Send_byte                 Allocated to registers ;size:2
;@int_isr Delay_1ms                 Allocated to registers ;size:2
;@int_isr int_isr                   Allocated to registers ;size:2
;@int_isr STATUSbits                Allocated to registers ;size:1
;@int_isr MCRbits                   Allocated to registers ;size:1
;@int_isr INTEbits                  Allocated to registers ;size:1
;@int_isr INTFbits                  Allocated to registers ;size:1
;@int_isr OSCMbits                  Allocated to registers ;size:1
;@int_isr IOP0bits                  Allocated to registers ;size:1
;@int_isr OEP0bits                  Allocated to registers ;size:1
;@int_isr PUP0bits                  Allocated to registers ;size:1
;@int_isr IOP1bits                  Allocated to registers ;size:1
;@int_isr OEP1bits                  Allocated to registers ;size:1
;@int_isr PUP1bits                  Allocated to registers ;size:1
;@int_isr IOP2bits                  Allocated to registers ;size:1
;@int_isr OEP2bits                  Allocated to registers ;size:1
;@int_isr PUP2bits                  Allocated to registers ;size:1
;@int_isr IOP3bits                  Allocated to registers ;size:1
;@int_isr OEP3bits                  Allocated to registers ;size:1
;@int_isr PUP3bits                  Allocated to registers ;size:1
;@int_isr T0CRbits                  Allocated to registers ;size:1
;@int_isr T1CRbits                  Allocated to registers ;size:1
;@int_isr T2CRbits                  Allocated to registers ;size:1
;@int_isr TK0CRHbits                Allocated to registers ;size:1
;@int_isr TK0CRLbits                Allocated to registers ;size:1
;@int_isr TK1CRHbits                Allocated to registers ;size:1
;@int_isr TK1CRLbits                Allocated to registers ;size:1
;@int_isr TK2CRHbits                Allocated to registers ;size:1
;@int_isr TK2CRLbits                Allocated to registers ;size:1
;@int_isr ADCR0bits                 Allocated to registers ;size:1
;@int_isr ADCR1bits                 Allocated to registers ;size:1
;@int_isr I2CCRbits                 Allocated to registers ;size:1
;@int_isr flag1                     Allocated to registers ;size:1
;@int_isr flag2                     Allocated to registers ;size:1
;@int_isr flag3                     Allocated to registers ;size:1
;@int_isr flag4                     Allocated to registers ;size:1
;@int_isr data                      Allocated to registers ;size:50
;@int_isr sdi_byte                  Allocated to registers ;size:1
;@int_isr count_bit                 Allocated to registers ;size:1
;@int_isr count_byte                Allocated to registers ;size:1
;@int_isr need_DealData             Allocated to registers ;size:1
;@int_isr need_test                 Allocated to registers ;size:1
;@int_isr checksum                  Allocated to registers ;size:1
;@int_isr i                         Allocated to registers ;size:1
;@int_isr j                         Allocated to registers ;size:1
;@int_isr temp1                     Allocated to registers ;size:1
;@int_isr temp2                     Allocated to registers ;size:1
;@int_isr temp                      Allocated to registers ;size:1
;@int_isr MCU_ID                    Allocated to registers ;size:1
;@int_isr OS_result                 Allocated to registers ;size:1
;@int_isr need_answer               Allocated to registers ;size:1
;@int_isr sdo_byte                  Allocated to registers ;size:1
;@int_isr INDF                      Allocated to registers ;size:1
;@int_isr INDF0                     Allocated to registers ;size:1
;@int_isr INDF1                     Allocated to registers ;size:1
;@int_isr INDF2                     Allocated to registers ;size:1
;@int_isr HIBYTE                    Allocated to registers ;size:1
;@int_isr FSR                       Allocated to registers ;size:1
;@int_isr FSR0                      Allocated to registers ;size:1
;@int_isr FSR1                      Allocated to registers ;size:1
;@int_isr PCL                       Allocated to registers ;size:1
;@int_isr STATUS                    Allocated to registers ;size:1
;@int_isr MCR                       Allocated to registers ;size:1
;@int_isr INDF3                     Allocated to registers ;size:1
;@int_isr INTE                      Allocated to registers ;size:1
;@int_isr INTF                      Allocated to registers ;size:1
;@int_isr OSCM                      Allocated to registers ;size:1
;@int_isr IOP0                      Allocated to registers ;size:1
;@int_isr OEP0                      Allocated to registers ;size:1
;@int_isr PUP0                      Allocated to registers ;size:1
;@int_isr IOP1                      Allocated to registers ;size:1
;@int_isr OEP1                      Allocated to registers ;size:1
;@int_isr PUP1                      Allocated to registers ;size:1
;@int_isr IOP2                      Allocated to registers ;size:1
;@int_isr OEP2                      Allocated to registers ;size:1
;@int_isr PUP2                      Allocated to registers ;size:1
;@int_isr IOP3                      Allocated to registers ;size:1
;@int_isr OEP3                      Allocated to registers ;size:1
;@int_isr PUP3                      Allocated to registers ;size:1
;@int_isr T0CR                      Allocated to registers ;size:1
;@int_isr T0CNT                     Allocated to registers ;size:1
;@int_isr T0LOAD                    Allocated to registers ;size:1
;@int_isr T0DATA                    Allocated to registers ;size:1
;@int_isr T1CR                      Allocated to registers ;size:1
;@int_isr T1CNT                     Allocated to registers ;size:1
;@int_isr T1LOAD                    Allocated to registers ;size:1
;@int_isr T1DATA                    Allocated to registers ;size:1
;@int_isr T2CR                      Allocated to registers ;size:1
;@int_isr T2CNTH                    Allocated to registers ;size:1
;@int_isr T2CNTL                    Allocated to registers ;size:1
;@int_isr T2LOADH                   Allocated to registers ;size:1
;@int_isr T2LOADL                   Allocated to registers ;size:1
;@int_isr TK0CRH                    Allocated to registers ;size:1
;@int_isr TK0CRL                    Allocated to registers ;size:1
;@int_isr TK0CNTH                   Allocated to registers ;size:1
;@int_isr TK0CNTL                   Allocated to registers ;size:1
;@int_isr TK1CRH                    Allocated to registers ;size:1
;@int_isr TK1CRL                    Allocated to registers ;size:1
;@int_isr TK1CNTH                   Allocated to registers ;size:1
;@int_isr TK1CNTL                   Allocated to registers ;size:1
;@int_isr TK2CRH                    Allocated to registers ;size:1
;@int_isr TK2CRL                    Allocated to registers ;size:1
;@int_isr TK2CNTH                   Allocated to registers ;size:1
;@int_isr TK2CNTL                   Allocated to registers ;size:1
;@int_isr ADCR0                     Allocated to registers ;size:1
;@int_isr ADCR1                     Allocated to registers ;size:1
;@int_isr ADRH                      Allocated to registers ;size:1
;@int_isr ADRL                      Allocated to registers ;size:1
;@int_isr I2CCR                     Allocated to registers ;size:1
;@int_isr I2CADDR                   Allocated to registers ;size:1
;@int_isr I2CDATA                   Allocated to registers ;size:1
;@end Allocation info for local variables in function 'int_isr';
;@Allocation info for local variables in function 'main'
;@end Allocation info for local variables in function 'main';
;@Allocation info for local variables in function 'Send_byte_P23'
;@Send_byte_P23 byte_data                 Allocated to registers r0x1043 ;size:1
;@Send_byte_P23 i                         Allocated to registers r0x1044 ;size:1
;@end Allocation info for local variables in function 'Send_byte_P23';
;@Allocation info for local variables in function 'Send_byte'
;@Send_byte byte_data                 Allocated to registers r0x1040 ;size:1
;@Send_byte i                         Allocated to registers r0x1041 ;size:1
;@end Allocation info for local variables in function 'Send_byte';
;@Allocation info for local variables in function 'Delay_1ms'
;@Delay_1ms num                       Allocated to registers ;size:1
;@Delay_1ms i                         Allocated to registers r0x103E ;size:1
;@Delay_1ms j                         Allocated to registers r0x103F ;size:1
;@end Allocation info for local variables in function 'Delay_1ms';
;@Allocation info for local variables in function 'Read32p64'
;@Read32p64 i                         Allocated to registers r0x103C ;size:1
;@Read32p64 a                         Allocated to registers r0x103D ;size:1
;@Read32p64 read_data                 Allocated to registers r0x103B ;size:1
;@end Allocation info for local variables in function 'Read32p64';

;--------------------------------------------------------
; overlayable items in internal ram 
;--------------------------------------------------------
;	udata_ovr
;--------------------------------------------------------
; reset vector 
;--------------------------------------------------------
STARTUP	code 0x0000
	goto	_main
;--------------------------------------------------------
; interrupt and initialization code
;--------------------------------------------------------
c_interrupt	code	0x8
__sdcc_interrupt
;***
;  pBlock Stats: dbName = I
;***
;entry:  _int_isr	;Function start
; 0 exit points
;2 compiler assigned registers:
;   r0x1048
;   r0x1049
;; Starting pCode block
_int_isr	;Function start
; 0 exit points
;	.line	39; "main.c"	if(T0IF==1)
	CLRR	r0x1048
	JBCLR	_INTFbits,0
	INCR	r0x1048
;;103	MOVAR	r0x1048
;;102	MOVAR	r0x1049
	MOVAR	r0x1048
	MOVRA	r0x1049
	XORAI	0x01
	JBSET	STATUS,2
	GOTO	_00133_DS_
;	.line	41; "main.c"	T0IF=0;
	BCLR	_INTFbits,0
;	.line	42; "main.c"	IOP2^=0x02;
	MOVAI	0x02
	XORRA	_IOP2
	GOTO	END_OF_INTERRUPT
_00133_DS_
;	.line	44; "main.c"	else if(INT0IF==1)
	CLRR	r0x1048
	JBCLR	_INTFbits,2
	INCR	r0x1048
;;100	MOVAR	r0x1048
;;99	MOVAR	r0x1049
	MOVAR	r0x1048
	MOVRA	r0x1049
	XORAI	0x01
	JBSET	STATUS,2
	GOTO	END_OF_INTERRUPT
;	.line	46; "main.c"	INT0IF=0;
	BCLR	_INTFbits,2
;	.line	47; "main.c"	if(need_answer==1)
	MOVAR	_need_answer
	XORAI	0x01
	JBSET	STATUS,2
	GOTO	_00128_DS_
;	.line	49; "main.c"	if(count_bit==0)
	MOVAI	0x00
	ORAR	_count_bit
	JBSET	STATUS,2
	GOTO	_00106_DS_
;	.line	51; "main.c"	sdo_byte=data[count_byte++];
	MOVAR	_count_byte
	MOVRA	r0x1048
	INCR	_count_byte
	MOVAR	r0x1048
	ADDAI	(_data + 0)
	MOVRA	r0x1048
	MOVAI	high (_data + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x1049
	MOVAR	r0x1048
	MOVRA	FSR
	BCLR	STATUS,7
	JBCLR	r0x1049,0
	BSET	STATUS,7
	MOVAR	INDF
	MOVRA	_sdo_byte
;;unsigned compare: left < lit(0x8=8), size=1
_00106_DS_
;	.line	53; "main.c"	if(count_bit<8)
	MOVAI	0x08
	RSUBAR	_count_bit
	JBCLR	STATUS,0
	GOTO	END_OF_INTERRUPT
;;genSkipc:3251: created from rifx:0028608C
;	.line	55; "main.c"	if(sdo_byte&0x01)
	JBSET	_sdo_byte,0
	GOTO	_00108_DS_
;	.line	57; "main.c"	SDO_1;
	BSET	_IOP2,1
	GOTO	_00109_DS_
_00108_DS_
;	.line	61; "main.c"	SDO_0;
	BCLR	_IOP2,1
;;shiftRight_Left2ResultLit:5283: shCount=1, size=1, sign=0, same=1, offr=0
_00109_DS_
;	.line	63; "main.c"	sdo_byte>>=1;
	BCLR	STATUS,0
	RRR	_sdo_byte
;	.line	64; "main.c"	count_bit++;
	INCR	_count_bit
;	.line	65; "main.c"	if(count_bit==8)
	MOVAR	_count_bit
	XORAI	0x08
	JBSET	STATUS,2
	GOTO	END_OF_INTERRUPT
;	.line	67; "main.c"	count_bit=0;
	CLRR	_count_bit
;	.line	68; "main.c"	if(count_byte==5)
	MOVAR	_count_byte
	XORAI	0x05
	JBSET	STATUS,2
	GOTO	END_OF_INTERRUPT
;	.line	70; "main.c"	count_byte=0;
	CLRR	_count_byte
;	.line	71; "main.c"	need_answer=0;
	CLRR	_need_answer
;	.line	72; "main.c"	OEP2&=0xfd;//回复STM32后 SDO设置为输入
	BCLR	_OEP2,1
	GOTO	END_OF_INTERRUPT
;;shiftRight_Left2ResultLit:5283: shCount=1, size=1, sign=0, same=1, offr=0
_00128_DS_
;	.line	79; "main.c"	sdi_byte>>=1;
	BCLR	STATUS,0
	RRR	_sdi_byte
;	.line	80; "main.c"	if(IOP2&0x01)
	JBSET	_IOP2,0
	GOTO	_00117_DS_
;	.line	82; "main.c"	sdi_byte|=0x80;
	BSET	_sdi_byte,7
	GOTO	_00118_DS_
_00117_DS_
;	.line	86; "main.c"	sdi_byte&=0x7f;    	   	   	
	BCLR	_sdi_byte,7
_00118_DS_
;	.line	88; "main.c"	count_bit++;
	INCR	_count_bit
;	.line	89; "main.c"	if(count_bit==8)
	MOVAR	_count_bit
	XORAI	0x08
	JBSET	STATUS,2
	GOTO	END_OF_INTERRUPT
;	.line	91; "main.c"	count_bit=0;
	CLRR	_count_bit
;	.line	92; "main.c"	data[count_byte++]=sdi_byte;//save a byte data
	MOVAR	_count_byte
	MOVRA	r0x1048
	INCR	_count_byte
	MOVAR	r0x1048
	ADDAI	(_data + 0)
	MOVRA	r0x1048
	MOVAI	high (_data + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x1049
	MOVAR	r0x1048
	MOVRA	FSR
	BCLR	STATUS,7
	JBCLR	r0x1049,0
	BSET	STATUS,7
	MOVAR	_sdi_byte
	MOVRA	INDF
;	.line	93; "main.c"	if(data[0]==0xc5)
	MOVAR	(_data + 0)
	MOVRA	r0x1048
	MOVAR	r0x1048
	XORAI	0xc5
	JBSET	STATUS,2
	GOTO	_00123_DS_
;	.line	95; "main.c"	if(sdi_byte==0x5c&&count_byte==data[1])//接收到一帧完整数据
	MOVAR	_sdi_byte
	XORAI	0x5c
	JBSET	STATUS,2
	GOTO	END_OF_INTERRUPT
	MOVAR	(_data + 1)
	MOVRA	r0x1048
	MOVAR	r0x1048
	XORAR	_count_byte
	JBSET	STATUS,2
	GOTO	END_OF_INTERRUPT
;	.line	97; "main.c"	need_DealData=1;
	MOVAI	0x01
	MOVRA	_need_DealData
	GOTO	END_OF_INTERRUPT
_00123_DS_
;	.line	102; "main.c"	count_byte=0;
	CLRR	_count_byte
END_OF_INTERRUPT
	RETIE	

;--------------------------------------------------------
; code
;--------------------------------------------------------
code_main	code
;***
;  pBlock Stats: dbName = M
;***
;entry:  _main	;Function start
; 2 exit points
;has an exit
;functions called:
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Delay_1ms
;   _Read32p64
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Send_byte_P23
;   _Delay_1ms
;   _Read32p64
;3 compiler assigned registers:
;   r0x1046
;   STK00
;   r0x1047
;; Starting pCode block
_main	;Function start
; 2 exit points
;	.line	119; "main.c"	OEP0=0x40;//p07 input,p06 output
	MOVAI	0x40
	MOVRA	_OEP0
;	.line	120; "main.c"	OEP1=0x00;//p17 input
	CLRR	_OEP1
;	.line	121; "main.c"	OEP2=0x1c;//p20 input,p22 output,p23 output,p24 output
	MOVAI	0x1c
	MOVRA	_OEP2
;	.line	122; "main.c"	SCK_1;
	BSET	_IOP2,2
;	.line	123; "main.c"	SDI_0;
	BCLR	_IOP2,3
;	.line	125; "main.c"	PUP0=0xff;//P07 pull-up
	MOVAI	0xff
	MOVRA	_PUP0
;	.line	126; "main.c"	PUP1=0xff;//P17 pull-up
	MOVAI	0xff
	MOVRA	_PUP1
;	.line	135; "main.c"	sdi_byte=0;
	CLRR	_sdi_byte
;	.line	136; "main.c"	count_bit=0;
	CLRR	_count_bit
;	.line	137; "main.c"	count_byte=0;
	CLRR	_count_byte
;	.line	138; "main.c"	need_DealData=0;
	CLRR	_need_DealData
;	.line	139; "main.c"	checksum=0;
	CLRR	_checksum
_00141_DS_
;	.line	144; "main.c"	Mark_0x02;//测试用代码//正式版本 由STM32分配芯片编号
	BCLR	_IOP2,4
;	.line	145; "main.c"	Send_byte_P23(0xc5);//帧头
	MOVAI	0xc5
	CALL	_Send_byte_P23
;	.line	146; "main.c"	Send_byte_P23(0x0a);//帧长
	MOVAI	0x0a
	CALL	_Send_byte_P23
;	.line	147; "main.c"	Send_byte_P23(0x02);//32P64 MCU ID
	MOVAI	0x02
	CALL	_Send_byte_P23
;	.line	148; "main.c"	Send_byte_P23(0x00);//P00
	MOVAI	0x00
	CALL	_Send_byte_P23
;	.line	149; "main.c"	Send_byte_P23(0x00);//输出0
	MOVAI	0x00
	CALL	_Send_byte_P23
;	.line	150; "main.c"	Send_byte_P23(0x02);//32P64 MCU ID
	MOVAI	0x02
	CALL	_Send_byte_P23
;	.line	151; "main.c"	Send_byte_P23(0x01);//P01
	MOVAI	0x01
	CALL	_Send_byte_P23
;	.line	152; "main.c"	Send_byte_P23(0x00);//0x00 正常值
	MOVAI	0x00
	CALL	_Send_byte_P23
;	.line	153; "main.c"	Send_byte_P23(0x05);//校验码
	MOVAI	0x05
	CALL	_Send_byte_P23
;	.line	154; "main.c"	Send_byte_P23(0x5c);//帧尾
	MOVAI	0x5c
	CALL	_Send_byte_P23
;	.line	156; "main.c"	Delay_1ms(2);
	MOVAI	0x02
	CALL	_Delay_1ms
;	.line	158; "main.c"	for(i=0;i<5;i++)
	CLRR	_i
;;unsigned compare: left < lit(0x5=5), size=1
_00143_DS_
	MOVAI	0x05
	RSUBAR	_i
	JBCLR	STATUS,0
	GOTO	_00141_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	161; "main.c"	temp=Read32p64();
	CALL	_Read32p64
	MOVRA	r0x1046
	MOVAR	STK00
	MOVRA	_temp
	MOVRA	r0x1047
;;101	MOVAR	r0x1047
;	.line	162; "main.c"	data[i]=temp;
	MOVAR	_i
	ADDAI	(_data + 0)
	MOVRA	r0x1047
	MOVAI	high (_data + 0)
	JBCLR	STATUS,0
	ADDAI	0x01
	MOVRA	r0x1046
	MOVAR	r0x1047
	MOVRA	FSR
	BCLR	STATUS,7
	JBCLR	r0x1046,0
	BSET	STATUS,7
	MOVAR	_temp
	MOVRA	INDF
;	.line	158; "main.c"	for(i=0;i<5;i++)
	INCR	_i
	GOTO	_00143_DS_
	RETURN	
; exit point of _main

;***
;  pBlock Stats: dbName = C
;***
;entry:  _Read32p64	;Function start
; 2 exit points
;has an exit
;3 compiler assigned registers:
;   r0x103B
;   r0x103C
;   r0x103D
;; Starting pCode block
_Read32p64	;Function start
; 2 exit points
;	.line	258; "main.c"	read_data=0;
	CLRR	r0x103B
;	.line	259; "main.c"	for(i=0;i<8;i++)
	CLRR	r0x103C
;;unsigned compare: left < lit(0x8=8), size=1
_00185_DS_
	MOVAI	0x08
	RSUBAR	r0x103C
	JBCLR	STATUS,0
	GOTO	_00188_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	261; "main.c"	SCK_1;  
	BSET	_IOP2,2
;	.line	262; "main.c"	Nop();
	nop
;	.line	263; "main.c"	SCK_0;
	BCLR	_IOP2,2
;	.line	264; "main.c"	Nop();
	nop
;	.line	265; "main.c"	a=IOP2;
	MOVAR	_IOP2
	MOVRA	r0x103D
;	.line	266; "main.c"	a&=0x01;
	MOVAI	0x01
	ANDRA	r0x103D
;	.line	267; "main.c"	a<<=i;
	MOVAR	r0x103C
	ISUBAI	0x00
	JBCLR	STATUS,2
	GOTO	_00198_DS_
_00197_DS_
	RLR	r0x103D
	ADDAI	0x01
	JBSET	STATUS,0
	GOTO	_00197_DS_
_00198_DS_
;	.line	268; "main.c"	read_data|=a;
	MOVAR	r0x103D
	ORRA	r0x103B
;	.line	259; "main.c"	for(i=0;i<8;i++)
	INCR	r0x103C
	GOTO	_00185_DS_
_00188_DS_
;	.line	270; "main.c"	return read_data;
	MOVAR	r0x103B
	RETURN	
; exit point of _Read32p64

;***
;  pBlock Stats: dbName = C
;***
;entry:  _Delay_1ms	;Function start
; 2 exit points
;has an exit
;2 compiler assigned registers:
;   r0x103E
;   r0x103F
;; Starting pCode block
_Delay_1ms	;Function start
; 2 exit points
;	.line	231; "main.c"	void Delay_1ms(uchar num)
	MOVRA	r0x103E
_00176_DS_
;	.line	236; "main.c"	for(i=num;i>0;i--)
	MOVAI	0x00
	ORAR	r0x103E
	JBCLR	STATUS,2
	GOTO	_00180_DS_
;	.line	238; "main.c"	for(j=123;j>0;j--)
	MOVAI	0x7b
	MOVRA	r0x103F
_00172_DS_
	MOVAI	0x00
	ORAR	r0x103F
	JBCLR	STATUS,2
	GOTO	_00178_DS_
;	.line	240; "main.c"	Nop();
	nop
;	.line	238; "main.c"	for(j=123;j>0;j--)
	DECR	r0x103F
	GOTO	_00172_DS_
_00178_DS_
;	.line	236; "main.c"	for(i=num;i>0;i--)
	DECR	r0x103E
	GOTO	_00176_DS_
_00180_DS_
	RETURN	
; exit point of _Delay_1ms

;***
;  pBlock Stats: dbName = C
;***
;entry:  _Send_byte	;Function start
; 2 exit points
;has an exit
;3 compiler assigned registers:
;   r0x1040
;   r0x1041
;   r0x1042
;; Starting pCode block
_Send_byte	;Function start
; 2 exit points
;	.line	203; "main.c"	void Send_byte(uchar byte_data)
	MOVRA	r0x1040
;	.line	206; "main.c"	SCK_0;
	BCLR	_IOP2,2
;	.line	207; "main.c"	for(i=0;i<8;i++)
	CLRR	r0x1041
;;unsigned compare: left < lit(0x8=8), size=1
_00163_DS_
	MOVAI	0x08
	RSUBAR	r0x1041
	JBCLR	STATUS,0
	GOTO	_00167_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	209; "main.c"	SCK_1;
	BSET	_IOP2,2
;	.line	210; "main.c"	if ((byte_data & 0x01)==1)
	MOVAI	0x01
	ANDAR	r0x1040
	MOVRA	r0x1042
	MOVAR	r0x1042
	XORAI	0x01
	JBSET	STATUS,2
	GOTO	_00161_DS_
;	.line	212; "main.c"	SDO_1;
	BSET	_IOP2,1
	GOTO	_00162_DS_
_00161_DS_
;	.line	216; "main.c"	SDO_0;
	BCLR	_IOP2,1
_00162_DS_
;	.line	218; "main.c"	Nop();
	nop
;	.line	219; "main.c"	SCK_0;
	BCLR	_IOP2,2
;;shiftRight_Left2ResultLit:5283: shCount=1, size=1, sign=0, same=1, offr=0
;	.line	221; "main.c"	byte_data>>=1;
	BCLR	STATUS,0
	RRR	r0x1040
;	.line	207; "main.c"	for(i=0;i<8;i++)
	INCR	r0x1041
	GOTO	_00163_DS_
_00167_DS_
	RETURN	
; exit point of _Send_byte

;***
;  pBlock Stats: dbName = C
;***
;entry:  _Send_byte_P23	;Function start
; 2 exit points
;has an exit
;3 compiler assigned registers:
;   r0x1043
;   r0x1044
;   r0x1045
;; Starting pCode block
_Send_byte_P23	;Function start
; 2 exit points
;	.line	174; "main.c"	void Send_byte_P23(uchar byte_data)
	MOVRA	r0x1043
;	.line	178; "main.c"	for(i=0;i<8;i++)
	CLRR	r0x1044
;;unsigned compare: left < lit(0x8=8), size=1
_00151_DS_
	MOVAI	0x08
	RSUBAR	r0x1044
	JBCLR	STATUS,0
	GOTO	_00155_DS_
;;genSkipc:3251: created from rifx:0028608C
;	.line	180; "main.c"	SCK_1;
	BSET	_IOP2,2
;	.line	181; "main.c"	if ((byte_data & 0x01)==1)
	MOVAI	0x01
	ANDAR	r0x1043
	MOVRA	r0x1045
	MOVAR	r0x1045
	XORAI	0x01
	JBSET	STATUS,2
	GOTO	_00149_DS_
;	.line	183; "main.c"	SDI_1;
	BSET	_IOP2,3
	GOTO	_00150_DS_
_00149_DS_
;	.line	187; "main.c"	SDI_0;
	BCLR	_IOP2,3
_00150_DS_
;	.line	189; "main.c"	SCK_0;
	BCLR	_IOP2,2
;	.line	190; "main.c"	Nop();
	nop
;	.line	191; "main.c"	Nop();
	nop
;;shiftRight_Left2ResultLit:5283: shCount=1, size=1, sign=0, same=1, offr=0
;	.line	192; "main.c"	byte_data>>=1;
	BCLR	STATUS,0
	RRR	r0x1043
;	.line	178; "main.c"	for(i=0;i<8;i++)
	INCR	r0x1044
	GOTO	_00151_DS_
_00155_DS_
	RETURN	
; exit point of _Send_byte_P23


;	code size estimation:
;	  277+    0 =   277 instructions (  554 byte)

	end
