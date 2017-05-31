

;mc32p7011 header file
;
; 

;define must write at first row
       
HIBYTE	EQU			0x082
FSR0		EQU			0x083
FSR1    EQU     0x084
PFLAG   EQU     0x086
ANSEL   EQU     0x0AE
VREF    EQU     0x0AF
ADCR    EQU     0x0B1
ADRH    EQU     0x0B2
ADRL    EQU     0x0B3
OEP0    EQU     0x0B8   
PEDGE   EQU     0x0BF
OEP4    EQU     0x0C4
OEP5    EQU     0x0C5
INTF    EQU     0x0C8
INTE    EQU     0x0C9
OSCM    EQU     0x0CA
WDTR    EQU     0x0CC
T0D     EQU     0x0CD
PCL     EQU     0x0CE
PCH     EQU     0x0CF
IOP0    EQU     0x0D0
IOP4    EQU     0x0D4
IOP5    EQU     0x0D5
T0M     EQU     0x0D8
T0CR    EQU     0x0DA
T0C     EQU     0x0DB
T1CR    EQU     0x0DC
T1C     EQU     0x0DD
T1D     EQU     0x0DE
STKP    EQU     0x0DF
PUP0    EQU     0x0E0
PUP4    EQU     0x0E4
PUP5    EQU     0x0E5
INDF    EQU     0x0E7
T2CR		EQU			0x0EC
T2C			EQU			0x0ED
T2D			EQU			0x0EE
STK3L   EQU     0x0F8
STK3H   EQU     0x0F9
STK2L   EQU     0x0FA
STK2H   EQU     0x0FB
STK1L   EQU     0x0FC
STK1H   EQU     0x0FD
STK0L   EQU     0x0FE
STK0H   EQU     0x0FF


;HIBYTE 
#define         HIBYTE7           HIBYTE,7
#define         HIBYTE6           HIBYTE,6
#define         HIBYTE5           HIBYTE,5
#define         HIBYTE4           HIBYTE,4
#define         HIBYTE3           HIBYTE,3
#define         HIBYTE2           HIBYTE,2
#define         HIBYTE1           HIBYTE,1
#define         HIBYTE0           HIBYTE,0 

;FSR0 
#define         FSR07           FSR0,7
#define         FSR06           FSR0,6
#define         FSR05           FSR0,5
#define         FSR04           FSR0,4
#define         FSR03           FSR0,3
#define         FSR02           FSR0,2
#define         FSR01           FSR0,1
#define         FSR00           FSR0,0 

;FSR1
#define         FSR17           FSR1,7
#define         FSR16           FSR1,6
#define         FSR15           FSR1,5
#define         FSR14           FSR1,4
#define         FSR13           FSR1,3
#define         FSR12           FSR1,2
#define         FSR11           FSR1,1
#define         FSR10           FSR1,0

;PFLAG
#define         NT0             PFLAG,7
#define         NPD             PFLAG,6
#define         LVD36           PFLAG,5
#define         LVD24           PFLAG,4
#define         C               PFLAG,2
#define         DC              PFLAG,1
#define         Z               PFLAG,0

;ANSEL
#define         P45ANS          ANSEL,5
#define         P44ANS          ANSEL,4
#define         P43ANS          ANSEL,3
#define         P42ANS          ANSEL,2
#define         P41ANS          ANSEL,1
#define         P40ANS          ANSEL,0

;VREF
#define         VREFS           VREF,7
#define         VRS1            VREF,1
#define         VRS0            VREF,0

;ADCR
#define         ADON            ADCR,7
#define         ADST            ADCR,6
#define         ADEOC           ADCR,5
#define         GCHS            ADCR,4
#define         ADCHS2          ADCR,2
#define         ADCHS1          ADCR,1
#define         ADCHS0          ADCR,0

;ADRH
#define         ADR11           ADRH,7
#define         ADR10           ADRH,6
#define         ADR9            ADRH,5
#define         ADR8            ADRH,4
#define         ADR7            ADRH,3
#define         ADR6            ADRH,2
#define         ADR5            ADRH,1
#define         ADR4            ADRH,0          

;ADRL
#define         ADCKS1          ADRL,6 
#define         ADLEN           ADRL,5
#define         ADCKS0          ADRL,4
#define         ADR3            ADRL,3
#define         ADR2            ADRL,2
#define         ADR1            ADRL,1
#define         ADR0            ADRL,0

;OEP0
#define         P04OE            OEP0,4
#define         P03OE            OEP0,3
#define         P02OE            OEP0,2
#define         P01OE            OEP0,1
#define         P00OE            OEP0,0

;PEDGE
#define         MINT01           PEDGE,4
#define         MINT00           PEDGE,3

;OEP4
#define         P45OE            OEP4,5
#define         P44OE            OEP4,4
#define         P43OE            OEP4,3
#define         P42OE            OEP4,2
#define         P41OE            OEP4,1
#define         P40OE            OEP4,0

;OEP5
#define         P54OE            OEP5,4
#define         P53OE            OEP5,3
#define         P52OE            OEP5,2

;INTF
#define         ADIF            INTF,7
#define         T1IF            INTF,6
#define         T0IF            INTF,5
#define         T2IF            INTF,4
#define         INT1IF          INTF,1
#define         INT0IF          INTF,0

;INTE
#define         ADIE            INTE,7
#define         T1IE            INTE,6
#define         T0IE            INTE,5
#define         T2IE            INTE,4
#define         INT1IE          INTE,1
#define         INT0IE          INTE,0

;OSCM
#define         CPUM1           OSCM,4
#define         CPUM0           OSCM,3
#define         CLKS            OSCM,2
#define         HOFF            OSCM,1

;WDTR
#define         WDTR7           WDTR,7
#define         WDTR6           WDTR,6
#define         WDTR5           WDTR,5
#define         WDTR4           WDTR,4
#define         WDTR3           WDTR,3
#define         WDTR2           WDTR,2
#define         WDTR1           WDTR,1
#define         WDTR0           WDTR,0

;T0D
#define         T0D7           T0D,7
#define         T0D6           T0D,6
#define         T0D5           T0D,5
#define         T0D4           T0D,4
#define         T0D3           T0D,3
#define         T0D2           T0D,2
#define         T0D1           T0D,1
#define         T0D0           T0D,0

;PCL
#define         PC7             PCL,7
#define         PC6             PCL,6
#define         PC5             PCL,5
#define         PC4             PCL,4
#define         PC3             PCL,3
#define         PC2             PCL,2
#define         PC1             PCL,1
#define         PC0             PCL,0

;PCH
;#define         PC10            PCH,2
;#define         PC9             PCH,1
;#define         PC8             PCH,0

;IOP0
#define         P04D             IOP0,4
#define         P03D             IOP0,3
#define         P02D             IOP0,2
#define         P01D             IOP0,1
#define         P00D             IOP0,0

;IOP4
#define         P45D             IOP4,5
#define         P44D             IOP4,4
#define         P43D             IOP4,3
#define         P42D             IOP4,2
#define         P41D             IOP4,1
#define         P40D             IOP4,0

;IOP5
#define         P54D             IOP5,4
#define         P53D             IOP5,3
#define         P52D             IOP5,2

;TXCR
#define         T2PTSX           TXCR,4
#define         T1PTSX           TXCR,3
#define         T0PTSX           TXCR,2
#define         T0GE             TXCR,1

;T0CR
#define         TC0EN            T0CR,7
#define         T0PR2            T0CR,6
#define         T0PR1            T0CR,5
#define         T0PR0            T0CR,4
#define         T0PTS            T0CR,3
#define         T0ALOAD          T0CR,2
#define         BUZ0OE           T0CR,1
#define         PWM0OE           T0CR,0

;T0C
#define         T0C7           T0C,7
#define         T0C6           T0C,6
#define         T0C5           T0C,5
#define         T0C4           T0C,4
#define         T0C3           T0C,3
#define         T0C2           T0C,2
#define         T0C1           T0C,1
#define         T0C0           T0C,0

;T1CR
#define         TC1EN          T1CR,7
#define         T1PR2          T1CR,6
#define         T1PR1          T1CR,5
#define         T1PR0          T1CR,4
#define         T1PTS          T1CR,3
#define         T1ALOAD        T1CR,2
#define         BUZ1OE         T1CR,1
#define         PWM1OE         T1CR,0

;T1C
#define         T1C7           T1C,7
#define         T1C6           T1C,6
#define         T1C5           T1C,5
#define         T1C4           T1C,4
#define         T1C3           T1C,3
#define         T1C2           T1C,2
#define         T1C1           T1C,1
#define         T1C0           T1C,0

;T1D
#define         T1D7           T1D,7
#define         T1D6           T1D,6
#define         T1D5           T1D,5
#define         T1D4           T1D,4
#define         T1D3           T1D,3
#define         T1D2           T1D,2
#define         T1D1           T1D,1
#define         T1D0           T1D,0

;STKP
#define         GIE            STKP,7
#define         STKP2          STKP,2
#define         STKP1          STKP,1
#define         STKP0          STKP,0

;PUP0
#define         P04PU            PUP0,4
#define         P03PU            PUP0,3
#define         P02PU            PUP0,2
#define         P01PU            PUP0,1
#define         P00PU            PUP0,0

;PUP4
#define         P45PU            PUP4,5
#define         P44PU            PUP4,4
#define         P43PU            PUP4,3
#define         P42PU            PUP4,2
#define         P41PU            PUP4,1
#define         P40PU            PUP4,0

;PUP5
#define         P54PU            PUP5,4
#define         P53PU            PUP5,3
#define         P52PU            PUP5,2

;INDF
#define         INDF7            INDF,7
#define         INDF6            INDF,6
#define         INDF5            INDF,5
#define         INDF4            INDF,4
#define         INDF3            INDF,3
#define         INDF2            INDF,2
#define         INDF1            INDF,1
#define         INDF0            INDF,0

;T2CR
#define         TC2EN          T2CR,7
#define         T2PR2          T2CR,6
#define         T2PR1          T2CR,5
#define         T2PR0          T2CR,4
#define         T2PTS          T2CR,3
#define         T2ALOAD        T2CR,2
#define         BUZ2OE         T2CR,1
#define         PWM2OE         T2CR,0

;T2C
#define         T2C7           T2C,7
#define         T2C6           T2C,6
#define         T2C5           T2C,5
#define         T2C4           T2C,4
#define         T2C3           T2C,3
#define         T2C2           T2C,2
#define         T2C1           T2C,1
#define         T2C0           T2C,0

;T2D
#define         T2D7           T2D,7
#define         T2D6           T2D,6
#define         T2D5           T2D,5
#define         T2D4           T2D,4
#define         T2D3           T2D,3
#define         T2D2           T2D,2
#define         T2D1           T2D,1
#define         T2D0           T2D,0

;STK3L
#define         STK37           STK3L,7
#define         STK36           STK3L,6
#define         STK35           STK3L,5
#define         STK34           STK3L,4
#define         STK33           STK3L,3
#define         STK32           STK3L,2
#define         STK31           STK3L,1
#define         STK30           STK3L,0

;STK3H
#define         STK310          STK3H,2
#define         STK39           STK3H,1
#define         STK38           STK3H,0

;STK2L
#define         STK27           STK2L,7
#define         STK26           STK2L,6
#define         STK25           STK2L,5
#define         STK24           STK2L,4
#define         STK23           STK2L,3
#define         STK22           STK2L,2
#define         STK21           STK2L,1
#define         STK20           STK2L,0

;STK2H
#define         STK210          STK2H,2
#define         STK29           STK2H,1
#define         STK28           STK2H,0

;STK1L
#define         STK17           STK1L,7
#define         STK16           STK1L,6
#define         STK15           STK1L,5
#define         STK14           STK1L,4
#define         STK13           STK1L,3
#define         STK12           STK1L,2
#define         STK11           STK1L,1
#define         STK10           STK1L,0

;STK1H
#define         STK110          STK1H,2
#define         STK19           STK1H,1
#define         STK18           STK1H,0

;STK0L
#define         STK07           STK0L,7
#define         STK06           STK0L,6
#define         STK05           STK0L,5
#define         STK04           STK0L,4
#define         STK03           STK0L,3
#define         STK02           STK0L,2
#define         STK01           STK0L,1
#define         STK00           STK0L,0

;STK0H
#define         STK010          STK0H,2
#define         STK09           STK0H,1
#define         STK08           STK0H,0

