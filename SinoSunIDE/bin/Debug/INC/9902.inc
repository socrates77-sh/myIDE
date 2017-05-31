
;==========================================================================
;				mc32p7212
;       Revision History
;
;==========================================================================

;----- Register Files------------------------------------------------------

INDF0   EQU     0X180
INDF1   EQU     0X181
INDF2   EQU     0X182
HIBYTE  EQU     0X183
FSR0    EQU     0X184
FSR1    EQU     0X185
PCL     EQU     0X186
PFLAG   EQU     0X187
MCR     EQU     0X188
INDF3   EQU     0X189   
INTE0   EQU     0X18A
INTF0   EQU     0X18B
OSCM		EQU			0X18C
LVDCR		EQU			0X18D
IOP0    EQU     0X190
OEP0    EQU     0X191
PUP0    EQU     0X192
PSEL		EQU			0X193
IOP1    EQU     0X194
OEP1    EQU     0X195
PUP1    EQU     0X196
T0CR    EQU     0X1A0
T0CNT   EQU     0X1A1
T0LOAD  EQU     0X1A2
T0DATA  EQU     0X1A3
T1CR    EQU     0X1A4
T1CNT   EQU     0X1A5
T1LOAD  EQU     0X1A6
T1DATA  EQU     0X1A7
ADCR0   EQU     0X1B0
ADCR1   EQU     0X1B1
ADCR2   EQU     0X1B2
ADRH    EQU     0X1B4
ADRM    EQU     0X1B5
ADRL    EQU     0X1B6
IICSCR  EQU     0X1B8
IICADDR	EQU			0X1B9
IICDATA EQU     0X1BA
TPSC		EQU			0X1EE

#define		STATUS		PFLAG
#define		INDF		INDF0
#define		FSR		FSR0
#define		P0HCON		PUP0
#define		P1HCON		PUP1
#define		INTECON		INTE0
#define		INTEFLAG	INTF0	


;----- STATUS Bits --------------------------
#define	Z	STATUS,2
#define	DC	STATUS,1
#define	C	STATUS,0

;----- MCR Bits --------------------------
#define	GIE	MCR,7
#define	MINT11	MCR,3
#define	MINT10	MCR,2
#define	MINT01	MCR,1
#define	MINT00	MCR,0

;----- OSCM  Bits ----------------
#define	STBL	OSCM,5
#define	STBH	OSCM,4
#define	CLKS	OSCM,2
#define	LFEN	OSCM,1
#define	HFEN	OSCM,0

;-----  LVDCR Bits -------
#define LVD	PLVDCR,7
#define	LVDF	PLVDCR,0

;----- INTECON Bits --------------------------
#define	INTE	INTECON
#define	ADIE	INTECON,6
#define	KBIE	INTECON,4
#define	INT1IE	INTECON,3
#define	INT0IE	INTECON,2
#define	T1IE	INTECON,1
#define	T0IE	INTECON,0

;----- INTEFLAG Bits --------------------------
#define	INTF	INTEFLAG
#define	ADIF	INTF,6
#define	KBIF	INTF,4
#define	INT1IF	INTF,3
#define	INT0IF	INTF,2
#define	T1IF	INTF,1
#define	T0IF	INTF,0

;----- T0CR  Bits ----------------
#define	TC0EN	T0CR,7
#define	PWM0OE	T0CR,6
#define	BUZ0OE	T0CR,5

;----- T1CR  Bits ----------------
#define	TC1EN	T1CR,7
#define	PWM1OE	T1CR,6
#define	BUZ1OE	T1CR,5

;---- ADCR0 Bits -----------------
#define AFE_EN	ADCR0,7
#define	CHOP_EN	ADCR0,3

;---- ADCR2 Bits -----------------
#define	   VLDOSL1	ADCR2,3
#define	   VLDOSL0	ADCR2,2	
#define	   VLDOEN	ADCR2,1
#define	   VR_INVDDS	ADCR2,0

;---- IICSCR Bits -----------------
#define	   HCF	IICSCR,7
#define	   HAAS	IICSCR,6
#define	   HBB	IICSCR,5
#define	   HTX	IICSCR,4
#define	   TXAK	IICSCR,3
#define	   SRW	IICSCR,2
#define	   HEN	IICSCR,1
#define	   RXAK	IICSCR,0
