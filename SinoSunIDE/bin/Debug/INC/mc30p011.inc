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

;----- Register Files------------------------------------------------------

INDF                         EQU     H'0000'
T0CNT                        EQU     H'0001'
PCL                          EQU     H'0002'
STATUS                       EQU     H'0003'
FSR                          EQU     H'0004'
P0                           EQU     H'0005'
P1                           EQU     H'0006'
GPR			      	             EQU	   H'0007'
MCR                          EQU     H'0008'
KBIM                         EQU     H'0009'
PCLATH                       EQU     H'000A'
PDCON                        EQU     H'000B'
ODCON                        EQU     H'000C'
PUCON                        EQU     H'000D'
INTECON                      EQU     H'000E'
INTEFLAG                     EQU     H'000F'
T0CR                         EQU     H'0041'
P5IOC                        EQU     H'0045'
P6IOC                        EQU     H'0046'
DDR0                         EQU     H'0045'
DDR1                         EQU     H'0046'
TMCR                         EQU     H'004B'
T1CR                         EQU     H'004C'
T1CNT                        EQU     H'004D'
T1LOAD                       EQU     H'004E'
T1DATA                       EQU     H'004F'

;#define		sleep			stop
;----- STATUS Bits --------------------------------------------------------
#define	RST	STATUS,7
#define	GPR1	STATUS,6
#define	GPR0	STATUS,5
#define	TO	STATUS,4
#define	PD	STATUS,3
#define	Z	STATUS,2
#define	DC	STATUS,1
#define	C	STATUS,0

;----- MCR Bits --------------------------------------------------------
#define	WDTEN	MCR,7
#define	EIS	MCR,6

;----- INTECON Bits --------------------------------------------------------
#define	INTE	INTECON
#define	GIE	INTECON,7
#define	INT0IE	INTECON,2
#define	KBIE	INTECON,1
#define	T0IE	INTECON,0

;----- INTEFLAG Bits --------------------------------------------------------
#define	INTF	INTEFLAG
#define	INT0IF	INTF,2
#define	KBIF	INTF,1
#define	T0IF	INTF,0
;----- T0CR Bits --------------------------------------------------------
#define INT0M T0CR,6
;INT0M	                       EQU     H'0006'
T0CS                         EQU     H'0005'
T0SE                         EQU     H'0004'
PTA                          EQU     H'0003'
PR2                          EQU     H'0002'
PR1                          EQU     H'0001'
PR0                          EQU     H'0000'

;------TMCR -----------------
#define TBS     TMCR,7
#define T1IE    TMCR,1
#define T1IF    TMCR,0

;-------T1CR----------
#define TMR1EN  T1CR,7
#define PWMOUT  T1CR,6
#define BUZOUT  T1CR,5

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
