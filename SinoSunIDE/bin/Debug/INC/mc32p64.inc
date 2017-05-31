

;mc32p64 header file
;
; 

;define must write at first row
       
INDF0   EQU     H'01B0'
INDF1   EQU     H'01B1'
INDF2   EQU     H'01B2'
HIBYTE  EQU     H'01B3'
FSR0    EQU     H'01B4'
FSR1    EQU     0X1B5
PCL     EQU     0X1B6
PFLAG   EQU    0X1B7
MCR     EQU     0X1B8
INDF3   EQU    0X1B9   
INTE    EQU     0X1BA
INTFLAG    EQU     0X1BB
OSCM    EQU     0X1BC
IOP0    EQU     0X1C0
OEP0    EQU     0X1C1
PUP0    EQU     0X1C2
IOP1    EQU     0X1C4
OEP1    EQU     0X1C5
PUP1    EQU     0X1C6
IOP2    EQU     0X1C8
OEP2    EQU     0X1C9
PUP2    EQU     0X1CA
IOP3    EQU     0X1CC
OEP3    EQU     0X1CD
PUP3    EQU     0X1CE
T0CR    EQU     0X1D0
T0CNT   EQU     0X1D1
T0LOAD  EQU     0X1D2
T0DATA  EQU     0X1D3
T1CR    EQU     0X1D4
T1CNT   EQU     0X1D5
T1LOAD  EQU     0X1D6
T1DATA  EQU     0X1D7
T2CR    EQU     0X1D8
T2CNTH   EQU     0X1D9
T2CNTL  EQU     0X1DA
T2LOADH EQU     0X1DB
T2LOADL EQU     0X1DC
TK0CRH  EQU     0X1E0
TK0CRL  EQU     0X1E1
TK0CNTH EQU     0X1E2
TK0CNTL EQU     0X1E3
TK1CRH  EQU     0X1E4
TK1CRL  EQU     0X1E5
TK1CNTH EQU     0X1E6
TK1CNTL EQU     0X1E7
TK2CRH  EQU     0X1E8
TK2CRL  EQU     0X1E9
TK2CNTH EQU     0X1EA
TK2CNTL EQU     0X1EB
ADCR0   EQU     0X1EC
ADCR1   EQU     0X1ED
ADIOS  EQU      0X1ED
ADRH    EQU     0X1EE
ADRL    EQU     0X1EF
I2CCR   EQU     0X1F0
I2CADDR EQU     0X1F1
I2CDATA EQU     0X1F2
OSCCAL0  EQU     0X1FB
OSCCAL1  EQU     0X1FC


;int 
#define         GIE             MCR,7
#define         T0        MCR,5
#define         PD              MCR,4
;#define         MINT1
  

;pflag bit 
#define         Z       PFLAG,2
#define         DC      PFLAG,1
#define         C       PFLAG,0 


;INTE
; 1=enable, 0=disable
#define         ADIE    INTE,6
#define         I2CIE   INTE,5
#define         T2IE    INTE,4
#define         INT1IE  INTE,3
#define         INT0IE  INTE,2
#define         T1IE    INTE,1
#define         T0IE    INTE,0

;TIME0
#define         TC0EN           T0CR,7
#define         PWM0OUT         T0CR,6
#define         BUZZ0OUT        T0CR,5
#define         T0PS1           T0CR,4
#define         T0PTS0          T0CR,3
;#define         T0PR2

;OSCM
#define         STBL    OSCM,5
#define         STBH    OSCM,4
#define         CLKS    OSCM,2
#define         LFEN    OSCM,1
#define         HFEN    OSCM,0          

;TIME0
#define         TC1EN           T1CR,7
#define         PWM1OUT         T1CR,6
#define         BUZZ1OUT        T1CR,5
#define         T1PS1           T1CR,4
#define         T1PTS0          T1CR,3

;INTFLAG
;1=active ,
#define         ADIF    INTFLAG,6 
#define         I2CIF   INTFLAG,5
#define         T2IF    INTFLAG,4
#define         INT1IF  INTFLAG,3
#define         INT0IF  INTFLAG,2
#define         T1IF    INTFLAG,1
#define         T0IF    INTFLAG,0


;-------------------------------------------------------
;timer2
;----------------------------
;T2CR
#define			TC2EN				T2CR,7
#define 		T2MODE			T2CR,6


;TK0CRH
#define					TK0JE		TK0CRH,5
#define					TK0RCE	TK0CRH,4
#define					TK0CLR	TK0CRH,3
;TK1CRH
#define					TK1JE		TK1CRH,5
#define					TK1RCE	TK1CRH,4
#define					TK1CLR	TK1CRH,3
;TK2CRH
#define					TK2JE		TK2CRH,5
#define					TK2RCE	TK2CRH,4
#define					TK2CLR	TK2CRH,3


;TKnCRL
#define					TK0IF		TK0CRL,7
#define					TK0IE		TK0CRL,6
					
;TKnCRL
#define					TK1IF		TK1CRL,7
#define					TK1IE		TK1CRL,6

;TKnCRL
#define					TK2IF		TK2CRL,7
#define					TK2IE		TK2CRL,6

;ADCR0
#define	        ADEOC   ADCR0,1
#define	        ADON    ADCR0,0      

;I2CCR
#define			HCF			I2CCR,7 ;data send end flag
#define			HAAS		I2CCR,6      ;addr matching flag 
#define			HBB			I2CCR,5				;
#define			HTX			I2CCR,4
#define			TXAK		I2CCR,3
#define			SRW			I2CCR,2
#define			HEN			I2CCR,1
#define			RXAK		I2CCR,0

;-------adc chanal----------------------------------------
#define         AD0     0X00
#define         AD1     0X10
#define         AD2     0X20
#define         AD3     0X30
#define         AD4     0X40
#define         AD5     0X50
#define         AD6     0X60
#define         AD7     0X70
#define         LDO     0X80
#define         AD_VDD          0XD0
#define         AD_VDD_4DIV     0XE0
#define         AD_VDD_2DIV     0XF0
