        LIST
;MC30P01A.INC  Standard Header File, Version 1.00 by Sinomcu
        NOLIST


;==========================================================================
;
;       Revision History
;
;==========================================================================

;Rev:   Date:    Reason:

;1.00   12/06/21 Initial Release

;==========================================================================
;
;       Verify Processor
;
;==========================================================================


;==========================================================================
;
;       Register Definitions
;
;==========================================================================

;W                            EQU     H'0000'
;F                            EQU     H'0001'

;----- Register Files------------------------------------------------------

INDF    	EQU     H'0000'
T0CNT   	EQU     H'0001'
PCL     	EQU     H'0002'
STATUS  	EQU     H'0003'
FSR     	EQU     H'0004'
P0      	EQU     H'0005'
P1      	EQU     H'0006'
GPR	  	EQU	H'0007'
MCR     	EQU     H'0008'
KBIM    	EQU     H'0009'
PCLATH  	EQU     H'000A'
PDCON   	EQU     H'000B'
ODCON   	EQU     H'000C'
PHCON   	EQU     H'000D'
INTECON 	EQU     H'000E'
INTEFLAG	EQU     H'000F'
OPT     	EQU     H'0041'
P5IOC   	EQU     H'0045'
P6IOC   	EQU     H'0046'

T1_FLAG		EQU	0x4B
T1CR		EQU	0x4C
T1CNT		EQU	0x4D
T1LOAD		EQU	0x4E
T1DATA		EQU	0x4F

#define		INDF0	INDF
#define		FSR0	FSR0
#define		P1HCON	PHCON
#define		T0CR	OPT
#define		DDR0	P5IOC
#define		DDR1	P6IOC
;----- STATUS Bits --------------------------
#define	RST	STATUS,7
#define	FLAG_TEMP0	STATUS,6
#define	FLAG_TEMP1	STATUS,5
#define	TO	STATUS,4
#define	PD	STATUS,3
#define	Z	STATUS,2
#define	DC	STATUS,1
#define	C	STATUS,0

;----- MCR Bits --------------------------
#define	WDTEN	MCR,7
#define	EIS	MCR,6
;----- KBIM Bits --------------------------
#define	KBIM0	KBIM,0
#define	KBIM1	KBIM,1
#define	KBIM2	KBIM,2
#define	KBIM3	KBIM,3
#define	KBIM4	KBIM,4
#define	KBIM5	KBIM,5
#define	KBIM6	KBIM,6
#define	KBIM7	KBIM,7
;------ PDCON Bits ------------------------
#define P00PD	PDCON,0
#define P01PD	PDCON,1
#define P02PD	PDCON,2
#define P03PD	PDCON,3

#define P10PD	PDCON,4
#define P11PD	PDCON,5
#define P12PD	PDCON,6
;----- ODCON Bits ------------------------
#define P10OD	ODCON,0
#define P11OD	ODCON,1
#define P12OD	ODCON,2

#define P14OD	ODCON,4
#define P15OD	ODCON,5
#define P16OD	ODCON,6
#define P17OD	ODCON,7
;-----  PHCON Bits -----------------------
#define	P10PH	PHCON,0
#define	P11PH	PHCON,1
#define	P12PH	PHCON,2

#define	P14PH	PHCON,4
#define	P15PH	PHCON,5
#define	P16PH	PHCON,6
#define	P17PH	PHCON,7
;----- INTECON Bits --------------------------
#define	INTE	INTECON
#define	GIE	INTECON,7
#define	INT0IE	INTECON,2
#define	KBIE	INTECON,1
#define	T0IE	INTECON,0

;----- INTEFLAG Bits --------------------------
#define	INTF	INTEFLAG
#define	INT0IF	INTF,2
#define	KBIF	INTF,1
#define	T0IF	INTF,0
;----- OPT Bits --------------------------
#define	INT0M	T0CR,6
;***********DDR0 DDR1********************************
#define	DDR00	DDR0,0
#define	DDR01	DDR0,1
#define	DDR02	DDR0,2
#define	DDR03	DDR0,3

#define	DDR10	DDR1,0
#define	DDR11	DDR1,1
#define	DDR12	DDR1,2
#define	DDR14	DDR1,4
#define	DDR15	DDR1,5
#define	DDR16	DDR1,6
#define	DDR17	DDR1,7
;
;***********T1_FLAG Bits***************************
#define	TBS	T1_FLAG,7
#define	T1IE	T1_FLAG,1
#define	T1IF	T1_FLAG,0
;***********T1CR Bits*****************
#define	T1MEN	T1CR,7
#define	PWMOUT	T1CR,6
#define	BUZOUT	T1CR,5
#define	T1PTS1	T1CR,4
#define	T1PTS0	T1CR,3
#define	T1PR2	T1CR,2
#define	T1PR1	T1CR,1
#define	T1PR0	T1CR,0
;******************************
#define	inca	addai 1
;==========================================================================
;
;       RAM Definition
;
;==========================================================================

        __MAXRAM H'5F'
;        __BADRAM H'07'-H'09', H'1B'-H'1F'
;		__BADRAM H'87'-H'89', H'91', H'95'-H'97', H'9A', H'9E'-H'9F'
;        __BADRAM H'107'-H'109'
;        __BADRAM H'185', H'187'-H'189', H'18E'-H'18F'

;==========================================================================
;
;       Configuration Bits
;
;==========================================================================

_CONFIG1                    EQU     H'2000'
_CONFIG2                    EQU     H'2001'

;Configuration Byte 1 Options
_BOR_15                      EQU     H'3FF8'
_BOR_18                      EQU     H'3FF9'
_BOR_20                      EQU     H'3FFA'
_BOR_22                      EQU     H'3FFB'
_BOR_24                      EQU     H'3FFC'
_BOR_26                      EQU     H'3FFD'
_BOR_36                      EQU     H'3FFF'
_MCLR_ON                     EQU     H'3FFF'
_MCLR_OFF                    EQU     H'3FF7'
_FOSC_8M                     EQU     H'3FFF'
_FOSC_4M                     EQU     H'3FEF'
_FOSC_2M                     EQU     H'3FDF'
_FOSC_1M                     EQU     H'3FCF'
_FOSC_500K                   EQU     H'3FBF'
_FOSC_455K                   EQU     H'3FAF'
_FOSC_32K                    EQU     H'3F9F'
_FOSC_62P5K                  EQU     H'3F8F'
_FCPU_4T                     EQU     H'3FFF'
_FCPU_2T                     EQU     H'3F7F'
_TWDT_18_18                  EQU     H'3FFF'
_TWDT_4P5_4P5                EQU     H'3EFF'
_TWDT_288_288                EQU     H'3DFF'
_TWDT_72_72                  EQU     H'3CFF'
_TWDT_P140_18                EQU     H'3BFF'
_TWDT_P140_4P5               EQU     H'3AFF'
_TWDT_P140_288               EQU     H'39FF'
_TWDT_P140_72                EQU     H'38FF'
_WDT_ON                      EQU     H'3FFF'
_WDT_OFF                     EQU     H'37FF'
_ROTP_HALFCPU                EQU     H'3FFF'
_ROTP_200N                   EQU     H'2FFF'
_ROTP_100N                   EQU     H'1FFF'
_ROTP_300N                   EQU     H'0FFF'

;Configuration Byte 2 Options
_RDPORT_PIN                  EQU     H'3FFF'
_RDPORT_REG                  EQU     H'3EFF'
_SMT_ON                      EQU     H'3EFF'
_SMT_OFF                     EQU     H'3DFF'
_MCU_FEL                     EQU     H'3FFF'
_MCU_EMC                     EQU     H'3BFF'
_OSCM_HS                     EQU     H'3FFF'
_OSCM_LP                     EQU     H'3FFF'
_OSCM_IRC                    EQU     H'37FF'
_CP_ON                       EQU     H'1FFF'
_CP_OFF                      EQU     H'3FFF'


; To use the Configuration Bits, place the following lines in your source code
;  in the following format, and change the configuration value to the desired 
;  setting (such as CP_OFF to CP_ALL).  These are currently commented out here
;  and each __CONFIG line should have the preceding semicolon removed when
;  pasted into your source code.

;Program Configuration Register 1
;		__CONFIG    _CONFIG1, _CP_OFF & _CCP1_RB0 & _DEBUG_OFF & _WRT_PROTECT_OFF & _CPD_OFF & _LVP_OFF & _BODEN_OFF & _MCLR_OFF & _PWRTE_OFF & _WDT_OFF & _HS_OSC

;Program Configuration Register 2
;		__CONFIG    _CONFIG2, _IESO_OFF & _FCMEN_OFF






        LIST
