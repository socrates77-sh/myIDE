		LIST
;mc2722.inc    Standard Header File, Version 1.00 by Sinomcu
		NOLIST

;==========================================================================
;
;	Verify Processor
;
;==========================================================================


;==========================================================================
;
;	Register Definitions
;
;==========================================================================

;----- Register Files ---------------------------------------------------

HIBYTE    		EQU		H'0082'
FSR       		EQU		H'0083'
FSR0      		EQU		H'0083'
FSR1      		EQU		H'0084'
STATUS    		EQU		H'0086'
PFLAG     		EQU		H'0086'
OSCCAL    		EQU		H'008F'
WKCR0     		EQU		H'00A0'
WKCR5     		EQU		H'00A5'
ADIOS2    		EQU		H'00AF'
ADM     		EQU		H'00B1'
ADB     		EQU		H'00B2'
ADR      		EQU		H'00B3'
OEP0      		EQU		H'00B8'
EINTCR    		EQU		H'00BF'
OEP4      		EQU		H'00C4'
OEP5      		EQU		H'00C5'
INTF      		EQU		H'00C8'
INTE      		EQU		H'00C9'
OSCM      		EQU		H'00CA'
WDTCR     		EQU		H'00CC'
T1LDR     		EQU		H'00CD'
PCL       		EQU		H'00CE'
PCH       		EQU		H'00CF'
IOP0      		EQU		H'00D0'
IOP4      		EQU		H'00D4'
IOP5      		EQU		H'00D5'
T0CR      		EQU		H'00D8'
T0CNT     		EQU		H'00D9'
T1CR      		EQU		H'00DA'
T1CNT     		EQU		H'00DB'
BUZCR     		EQU		H'00DC'
MCR       		EQU		H'00DF'
PUP0      		EQU		H'00E0'
PUP4      		EQU		H'00E4'
PUP5      		EQU		H'00E5'
INDF      		EQU		H'00E7'
STKR0L    		EQU		H'00F0'
STKR0H    		EQU		H'00F1'
STKR1L    		EQU		H'00F2'
STKR1H    		EQU		H'00F3'
STKR2L    		EQU		H'00F4'
STKR2H    		EQU		H'00F5'
STKR3L    		EQU		H'00F6'
STKR3H    		EQU		H'00F7'
STKR4L    		EQU		H'00F8'
STKR4H    		EQU		H'00F9'
STKR5L    		EQU		H'00FA'
STKR5H    		EQU		H'00FB'
STKR6L    		EQU		H'00FC'
STKR6H    		EQU		H'00FD'
STKR7L    		EQU		H'00FE'
STKR7H    		EQU		H'00FF'

;----- OSCCAL Bits ---------------------------------------------------

#define		OSCCAL0   	OSCCAL,0
#define		OSCCAL1   	OSCCAL,1
#define		OSCCAL2   	OSCCAL,2
#define		OSCCAL3   	OSCCAL,3
#define		OSCCAL4   	OSCCAL,4
#define		OSCCAL5   	OSCCAL,5
#define		OSCCAL6   	OSCCAL,6
#define		OSCCAL7   	OSCCAL,7

;----- WKCR0 Bits ---------------------------------------------------

#define		P00WK     	WKCR0,0
#define		P01WK     	WKCR0,1
#define		P02WK     	WKCR0,2
#define		P03WK     	WKCR0,3
#define		P04WK     	WKCR0,4
#define		P05WK     	WKCR0,5
#define		P06WK     	WKCR0,6
#define		P07WK     	WKCR0,7

;----- WKCR5 Bits ---------------------------------------------------

#define		P50WK     	WKCR5,0
#define		P51WK     	WKCR5,1
#define		P52WK     	WKCR5,2
#define		P53WK     	WKCR5,3
#define		P54WK     	WKCR5,4

;----- ADIOS2 Bits ---------------------------------------------------

#define		AN0EN     	ADIOS2,0
#define		AN1EN     	ADIOS2,1
#define		AN2EN     	ADIOS2,2
#define		AN3EN     	ADIOS2,3
#define		AN4EN     	ADIOS2,4

;----- ADM Bits ---------------------------------------------------

#define		ADCHS0    	ADM,0
#define		ADCHS1    	ADM,1
#define		ADCHS2    	ADM,2
#define		ADCHS3    	ADM,3
#define		GCHS      	ADM,4
#define		ADEOC     	ADM,5
#define		ADSTR     	ADM,6
#define		ADEN      	ADM,7

;----- ADB Bits ---------------------------------------------------

#define		ADSPS0    	ADB,0
#define		ADSPS1    	ADB,1
#define		ADSPS2    	ADB,2
#define		ADSPS3    	ADB,3
#define		ADCKS0    	ADB,4
#define		ADCKS1    	ADB,5
#define		ADCKS2    	ADB,6
#define		ADRSEL    	ADB,7

;----- OEP0 Bits ---------------------------------------------------

#define		P00OE     	OEP0,0
#define		P01OE     	OEP0,1
#define		P02OE     	OEP0,2
#define		P03OE     	OEP0,3
#define		P04OE     	OEP0,4
#define		P05OE     	OEP0,5
#define		P06OE     	OEP0,6
#define		P07OE     	OEP0,7

;----- EINTCR Bits ---------------------------------------------------

#define		MINT00    	EINTCR,3
#define		MINT01    	EINTCR,4

;----- OEP4 Bits ---------------------------------------------------

#define		P40OE     	OEP4,0
#define		P41OE     	OEP4,1
#define		P42OE     	OEP4,2
#define		P43OE     	OEP4,3
#define		P44OE     	OEP4,4

;----- OEP5 Bits ---------------------------------------------------

#define		P50OE     	OEP5,0
#define		P51OE     	OEP5,1
#define		P52OE     	OEP5,2
#define		P53OE     	OEP5,3
#define		P54OE     	OEP5,4

;----- INTF Bits ---------------------------------------------------

#define		INT0IF    	INTF,0
#define		PWMIF     	INTF,3
#define		T0IF      	INTF,4
#define		T1IF      	INTF,5
#define		ADIF      	INTF,7

;----- INTE Bits ---------------------------------------------------

#define		INT0IE    	INTE,0
#define		PWMIE     	INTE,3
#define		T0IE      	INTE,4
#define		T1IE      	INTE,5
#define		ADIE      	INTE,7

;----- OSCM Bits ---------------------------------------------------

#define		HFDE      	OSCM,1
#define		CLKS      	OSCM,2
#define		CPUM0     	OSCM,3
#define		CPUM1     	OSCM,4

;----- WDTCR Bits ---------------------------------------------------

#define		WDTCR0    	WDTCR,0
#define		WDTCR1    	WDTCR,1
#define		WDTCR2    	WDTCR,2
#define		WDTCR3    	WDTCR,3
#define		WDTCR5    	WDTCR,5
#define		WDTCR6    	WDTCR,6
#define		WDTCR7    	WDTCR,7

;----- T1LDR Bits ---------------------------------------------------

#define		T1LDR0    	T1LDR,0
#define		T1LDR1    	T1LDR,1
#define		T1LDR2    	T1LDR,2
#define		T1LDR3    	T1LDR,3
#define		T1LDR4    	T1LDR,4
#define		T1LDR5    	T1LDR,5
#define		T1LDR6    	T1LDR,6
#define		T1LDR7    	T1LDR,7

;----- PCL Bits ---------------------------------------------------

#define		PC0       	PCL,0
#define		PC1       	PCL,1
#define		PC2       	PCL,2
#define		PC3       	PCL,3
#define		PC4       	PCL,4
#define		PC5       	PCL,5
#define		PC6       	PCL,6
#define		PC7       	PCL,7

;----- PCH Bits ---------------------------------------------------

#define		PC8       	PCH,0
#define		PC9       	PCH,1
#define		PC10      	PCH,2

;----- IOP0 Bits ---------------------------------------------------

#define		P00D      	IOP0,0
#define		P01D      	IOP0,1
#define		P02D      	IOP0,2
#define		P03D      	IOP0,3
#define		P04D      	IOP0,4
#define		P05D      	IOP0,5
#define		P06D      	IOP0,6
#define		P07D      	IOP0,7

;----- IOP4 Bits ---------------------------------------------------

#define		P40D      	IOP4,0
#define		P41D      	IOP4,1
#define		P42D      	IOP4,2
#define		P43D      	IOP4,3

;----- IOP5 Bits ---------------------------------------------------

#define		P50D      	IOP5,0
#define		P51D      	IOP5,1
#define		P52D      	IOP5,2
#define		P53D      	IOP5,3

;----- T0CR Bits ---------------------------------------------------

#define		T1CKS1    	T0CR,2
#define		T1CKS2    	T0CR,3
#define		T0PRS0    	T0CR,4
#define		T0PRS1    	T0CR,5
#define		T0EN      	T0CR,7

;----- T0CNT Bits ---------------------------------------------------

#define		T0CNT0    	T0CNT,0
#define		T0CNT1    	T0CNT,1
#define		T0CNT2    	T0CNT,2
#define		T0CNT3    	T0CNT,3
#define		T0CNT4    	T0CNT,4
#define		T0CNT5    	T0CNT,5
#define		T0CNT6    	T0CNT,6
#define		T0CNT7    	T0CNT,7

;----- T1CR Bits ---------------------------------------------------

#define		PWM1OE    	T1CR,0
#define		BUZ1OE    	T1CR,1
#define		T1LDEN    	T1CR,2
#define		T1CKS0    	T1CR,3
#define		T1PRS0    	T1CR,4
#define		T1PRS1    	T1CR,5
#define		T1PRS2    	T1CR,6
#define		T1EN      	T1CR,7

;----- T1CNT Bits ---------------------------------------------------

#define		T1CNT0    	T1CNT,0
#define		T1CNT1    	T1CNT,1
#define		T1CNT2    	T1CNT,2
#define		T1CNT3    	T1CNT,3
#define		T1CNT4    	T1CNT,4
#define		T1CNT5    	T1CNT,5
#define		T1CNT6    	T1CNT,6
#define		T1CNT7    	T1CNT,7

;----- BUZCR Bits ---------------------------------------------------

#define		BUZPRS0   	BUZCR,5
#define		BUZPRS1   	BUZCR,6
#define		BUZEN     	BUZCR,7

;----- MCR Bits ---------------------------------------------------

#define		STKP0     	MCR,0
#define		STKP1     	MCR,1
#define		STKP2     	MCR,2
#define		GIE       	MCR,7

;----- PUP0 Bits ---------------------------------------------------

#define		P00PU     	PUP0,0
#define		P01PU     	PUP0,1
#define		P02PU     	PUP0,2
#define		P03PU     	PUP0,3
#define		P04PU     	PUP0,4
#define		P05PU     	PUP0,5
#define		P06PU     	PUP0,6
#define		P07PU     	PUP0,7

;----- PUP4 Bits ---------------------------------------------------

#define		P40PU     	PUP4,0
#define		P41PU     	PUP4,1
#define		P42PU     	PUP4,2
#define		P43PU     	PUP4,3
#define		P44PU     	PUP4,4

;----- PUP5 Bits ---------------------------------------------------

#define		P50PU     	PUP5,0
#define		P51PU     	PUP5,1
#define		P52PU     	PUP5,2
#define		P53PU     	PUP5,3
#define		P54PU     	PUP5,4

;----- INDF Bits ---------------------------------------------------

#define		INDF0     	INDF,0
#define		INDF1     	INDF,1
#define		INDF2     	INDF,2
#define		INDF3     	INDF,3
#define		INDF4     	INDF,4
#define		INDF5     	INDF,5
#define		INDF6     	INDF,6
#define		INDF7     	INDF,7

;==========================================================================
;
;	RAM Definition
;
;==========================================================================

	__MAXRAM H'FF'
	__BADRAM H'80'-H'81'
	__BADRAM H'85'
	__BADRAM H'87'
	__BADRAM H'88'-H'8D'
	__BADRAM H'90'-H'9F'
	__BADRAM H'A1'-H'A4'
	__BADRAM H'A6'-H'A7'
	__BADRAM H'A8'-H'AC'
	__BADRAM H'B0'
	__BADRAM H'B6'-H'B7'
	__BADRAM H'B9'-H'BE'
	__BADRAM H'C0'-H'C3'
	__BADRAM H'C6'-H'C7'
	__BADRAM H'CB'
	__BADRAM H'D1'-H'D3'
	__BADRAM H'D6'-H'D7'
	__BADRAM H'DD'-H'DE'
	__BADRAM H'E1'-H'E3'
	__BADRAM H'E6'
	__BADRAM H'EF'


;==========================================================================
;
;	Configuration Bits
;
;==========================================================================

_CONFIG1  		EQU		H'8001'
_CONFIG0  		EQU		H'8000'

;OPBIT1

;OPBIT0

		LIST
