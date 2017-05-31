


;---------------------------------------------
;       mc34p01.inc
;date:  2013.11.1
;v1.0
;----------------------------------------------

cblock  0x00
INDF            ;00h
T0CNT           ;01h
PCL             ;02h
STATUS          ;03
FSR             ;04
GPR0            ;05
IOP1            ;06
GPR1            ;07
MCR
KBIM
;-----0AH------------------
PCLATH  ;0AH
PDCON   ;0BH
ODCON   ;0CH
PUCON   ;0DH
INTECON ;0EH
INTFLAG ;0FH
endc

#define T0CR    0X01
#define DDR1    0X06

;--STATUS
#define C       STATUS,0
#define DC      STATUS,1
#define Z       STATUS,2
#define PD      STATUS,3
#define T0      STATUS,4
#define RST     STATUS,7


;MCR
#define         WDTEN   MCR,7
#define         EIS     MCR,6
#define         LVRF    MCR,5
;#define         LVDEN   MCR,4
;#define         MINT11  MCR,3
#define         MINT10  MCR,2
#define         MINT01  MCR,1
#define         MINT00  MCR,0

#define         KBIM5   KBIME,5
#define         KBIM4   KBIME,4
#define         KBIM3   KBIME,3
#define         KBIM2   KBIME,2
#define         KBIM1   KBIME,1
#define         KBIM0   KBIME,0

;INTECON
#define         GIE     INTECON,7
#define         INTIE   INTECON,2
#define         KBIE  INTECON,1
#define         T0IE    INTECON,0

;INTFLAG
#define         INTIF   INTFLAG,2
#define         KBIF  INTFLAG,1
#define         T0IF    INTFLAG,0

