

;---------------------------------------------
;       mc31p11.inc
;date:  2013.4.23
;v1.0
;----------------------------------------------

cblock  0x00
INDF
FSR
PCL
STATUS
MCR
INTECON
INTFLAG
IOP0
OEP0
PUP0
;-------0X0A
DKWP0
IOP1
OEP1
PUP1
DKWP1
DKW

endc

;--STATUS
#define C       STATUS,0
#define DC      STATUS,1
#define Z       STATUS,2


;MCR
#define         GIE     MCR,7
#define         TO      MCR,5
#define         PD      MCR,4
#define         MINT11  MCR,3
#define         MINT10  MCR,2
#define         MINT01  MCR,1
#define         MINT00  MCR,0

#define         DKWE    DKW,7
#define         RSEL    DKW,6
;#define         WSEL    DKW,5

;INTECON
#define         INT1IE  INTECON,3
#define         INT0IE  INTECON,2

;INTFLAG
#define         INT1IF  INTFLAG,3
#define         INT0IF  INTFLAG,2

