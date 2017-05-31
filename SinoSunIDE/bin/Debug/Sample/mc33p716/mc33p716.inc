;==========================================================================

;W                            EQU     H'0000'
;F                            EQU     H'0001'

;----- Register Files------------------------------------------------------
cblock	0x1B0
INDF0			
INDF1			
INDF2			
HIBYTE			
FSR0			
FSR1			
PCL			
PFALG			
;----0x1B8
MCR			
INDF3			
INTE0			
INTF0			
OSCM	;INVALID_ADDR0		
INVALID_ADDR1		
INVALID_ADDR2		
INVALID_ADDR3		
;----0x1C0
P0			
DDR0                    
PUP0           
PUS0
PDP0
PDS0
DKWP0
INVALID_ADDR_P0
;ANSEL
;P1                      
;DDR1                    
;PUP1                    
;KBIM           
;-----0x1C8
P1			
DDR1                        
PUP1           
PUS1
PDP1
PDS1
DKWP1
INVALID_ADDR_P1
;----0x1D0
P2			
DDR2                    
PUP2           
PUS2
PDP2
PDS2
DKWP2
INVALID_ADDR_P2
;----0x1D8
TACR
TALOADH
TALOADL
TALATRH
TALATRL
TALATFH
TALATFL
INVALID_ADDR_TA
;-----0x1E0
TBCR
TBDATA
TBLOAD
INVALID_ADDR_TB0
OPCR0
OPCR1
DKW
INVALID_ADDR_TB1
;----0x1E8
ADCR0
ADCR1
ADRH
ADRL
TMPCR
INVALID_ADDR_AD0      
INVALID_ADDR_AD1        
INVALID_ADDR_AD2
endc
;------------------------------------------------
#define		STATUS		PFALG
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
#define	T0	MCR,5
#define	PD	MCR,4
#define	MINT11	MCR,3
#define	MINT10	MCR,2
#define	MINT01	MCR,1
#define	MINT00	MCR,0
;-----  P2HCON Bits -----------------------
#define	P20PH	P2HCON,0
#define	P21PH	P2HCON,1
#define	P22PH	P2HCON,2
#define	P23PH	P2HCON,3
#define	P24PH	P2HCON,4
#define	P25PH	P2HCON,5
#define	P26PH	P2HCON,6
#define	P27PH	P2HCON,7
;----- INTECON Bits --------------------------
#define	INTE	INTECON
#define	KBIE	INTECON,7
#define	ADIE	INTECON,6
#define	LVDIE	INTECON,5

#define	INT1IE	INTECON,3
#define	INT0IE	INTECON,2

;----- INTEFLAG Bits --------------------------
#define	INTF	INTEFLAG
#define	KBIF	INTF,7
#define	ADIF	INTF,6
#define	LVDIF	INTF,5

#define	INT1IF	INTF,3
#define	INT0IF	INTF,2
;---- ADCR0 Bits -----------------
#define	   ADCKS_8	0<<1	;Fsys/8
#define	   ADCKS_4 	1<<1	;Fsys/4
#define	   ADCKS_2 	2<<1	;Fsys/2
#define	   ADCKS_1 	3<<1	;Fsys/1
#define	   ADCHS_0	0<<4	;AN0
#define	   ADCHS_1	1<<4	;AN1
#define	   ADCHS_2	2<<4	;AN2
#define	   ADCHS_3	3<<4	;AN3
#define	   ADCHS_4	4<<4	;AN4
#define	   ADCHS_5	5<<4	;AN5
#define	   ADCHS_6	6<<4	;AN6
#define	   ADCHS_7	7<<4	;AN7
#define	   ADCHS_8	8<<4	;AN8
#define	   ADCHS_9	9<<4	;AN9
#define	   ADCHS_VDD	0x0d<<4	;VDD
#define	   ADCHS_VDD4	0x0e<<4	;VDD/4
#define	   ADCHS_VDD2	0x0f<<4	;VDD/2
#define ADEN	ADCR0,0
#define	ADEOC	ADCR0,1
;----- T0CR  Bits ----------------
#define	T0CR	TACR
#define	T0IF	T0CR,7
#define	T0XF	T0CR,6
#define	T0IE	T0CR,5
#define	PR0_1	0<<3
#define	PR0_2	1<<3
#define	PR0_4	2<<3
#define	PR0_8	3<<3
#define	TC0EN	T0CR,2
;----- T1CR  Bits ----------------
#define	T1CR	TBCR
#define	T1IF	T1CR,7
#define	T1XF	T1CR,6
#define	T1IE	T1CR,5
#define	PR1_1	0<<3
#define	PR1_2	1<<3
#define	PR1_4	2<<3
#define	PR1_8	3<<3
#define	TC1EN	T1CR,2
;----- OSCM  Bits ----------------
#define	CLKS	OSCM,2
#define	LFEN	OSCM,1
#define	HFEN	OSCM,0
;
;;-----  POWCR Bits -------
;#define LDOEN	POWCR,7
;#define	ENB	POWCR,6
;;-----  LVDCR Bits -------
;#define LVD	PLVDCR,7
;#define	LVDS	PLVDCR,6
;***********DDR1 DDR2 **********
#define	DDR10	DDR1,0
#define	DDR11	DDR1,1
#define	DDR12	DDR1,2
#define	DDR13	DDR1,3

#define	DDR20	DDR2,0
#define	DDR21	DDR2,1
#define	DDR22	DDR2,2
#define	DDR23	DDR2,3
#define	DDR24	DDR2,4
#define	DDR25	DDR2,5
#define	DDR26	DDR2,6
#define	DDR27	DDR2,7
;******************************
;----DKW----
#define	DKWE	DKW,7
#define	IROS	DKW,6
#define	IROT	DKW,5
#define	WSEL_32		00
#define	WSEL_64		1<<3
#define	WSEL_96		2<<3
#define	WSEL_128 	3<<3
#define	ISEL_0_7	0
#define	ISEL_1_7	1
#define	ISEL_2_7	2
#define	ISEL_3_7	3
#define	ISEL_4_7	4
#define	ISEL_5_7	5
#define	ISEL_6_7	6
#define	ISEL_7_7	7


