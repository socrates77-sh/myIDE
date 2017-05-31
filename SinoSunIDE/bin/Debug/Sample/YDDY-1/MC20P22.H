
P0		EQU	$00
DDR0		EQU	$01
P0HCON		EQU	$02
P1		EQU	$03
DDR1		EQU	$04
P1HCON		EQU	$05
KBIM		EQU	$06
TCNT0		EQU	$07
TLOAD0		EQU	$07
TCR0		EQU	$08
TDATA1		EQU	$09
TCNT1		EQU	$0A
TLOAD1		EQU	$0A
TCR1		EQU	$0B
TDATA2H		EQU	$0C
TCNT2H		EQU	$0D
TLOAD2H		EQU	$0D
TDATA2L		EQU	$0E
TCNT2L		EQU	$0F
TLOAD2L		EQU	$0F
TCR2		EQU	$10
INTC		EQU	$11
ADCM		EQU	$12
ADCON		EQU	$13
ADTH		EQU	$14
ADTL		EQU	$15
MCR		EQU	$16
RSTFR		EQU	$17
;----------------------------------
;RSTFR-----------------------------
WDTRF	define	3,RSTFR
LVRRF	define	2,RSTFR
RSTRF	define	1,RSTFR
PORRF	define	0,RSTFR
;----------------------------------
;KBIM-------------------------------
KBIM6	define	6,KBIM
KBIM5	define	5,KBIM
KBIM4	define	4,KBIM
KBIM3	define	3,KBIM
KBIM2	define	2,KBIM
KBIM1	define	1,KBIM
KBIM0	define	0,KBIM
;----------------------------------
;MCR-------------------------------
KBIE	define	7,MCR
KBIC	define	6,MCR
ADE5	define	5,MCR
WDTC	define	4,MCR
WDTF	define	3,MCR
WDTM	define	2,MCR
LVD36	define  1,MCR
LVD24	define  0,MCR
;----------------------------------
;TCR0------------------------------
T0IF	define	7,TCR0
T0IM	define	6,TCR0

T0PTA	define	2,TCR0
T0PTS	define	1,TCR0
BUZOE	define	0,TCR0
;----------------------------------
;TCR1------------------------------
T1IF	define	7,TCR1
T1IM	define	6,TCR1
	
T1EN	define	2,TCR1
T1PTS	define	1,TCR1
PWM1E	define	0,TCR1
;----------------------------------
;TCR2------------------------------
T2IF	define	7,TCR2
T2IM	define	6,TCR2
		
T2EN	define	2,TCR2
T2PTS	define	1,TCR2
PWM2E	define	0,TCR2
;----------------------------------
;ADCM------------------------------
VREF	define	7,ADCM
VHSH	define	6,ADCM
VHSL	define	5,ADCM		
ADE4	define	4,ADCM
ADE3	define	3,ADCM
ADE2	define	2,ADCM
ADE1	define	1,ADCM
ADE0	define	0,ADCM
;----------------------------------
;ADCON-----------------------------
ADEN	define	4,ADCON
EOC	define	3,ADCON
ADCE	define	0,ADCON
;----------------------------------
;INTC------------------------------
INT0E	define	7,INTC
INT0M1	define	6,INTC
INT0M0	define	5,INTC		
INT0F	define	4,INTC
INT1E	define	3,INTC
INT1M1	define	2,INTC
INT1M0	define	1,INTC
INT1F	define	0,INTC
;----------------------------------

