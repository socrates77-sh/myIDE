
  ;processor mc32t8132

INDF0      equ   0x1a0
INDF1      equ   0x1a1
INDF2      equ   0x1a2
HIBYTE     equ   0x1a3
FSR0       equ   0x1a4
FSR1       equ   0x1a5
PCL        equ   0x1a6
PFALG      equ   0x1a7
MCR        equ   0x1a8
INDF3      equ   0x1a9
INTE	   equ   0x1aa
INTF	   equ   0x1ab
OSCM       equ   0x1ac
CLKO       equ   0x1ad

IOP0       equ   0x1b0
OEP0       equ   0x1b1
PUP0       equ   0x1b2
PDP0       equ   0x1b3

IOP1       equ   0x1b4
OEP1       equ   0x1b5
PUP1       equ   0x1b6
PDP1       equ   0x1b7

IOP2       equ   0x1b8
OEP2       equ   0x1b9
PUP2       equ   0x1ba
PDP2       equ   0x1bb

IOP3       equ   0x1bc
OEP3       equ   0x1bd
PUP3       equ   0x1be
PDP3       equ   0x1bf

T0CR       equ   0x1c0
T0CNT      equ   0x1c1  
T0LOAD     equ   0x1c2
T0DATA     equ   0x1c3 

T1CR       equ   0x1c4
T1CNT      equ   0x1c5  
T1LOAD     equ   0x1c6
T1DATA     equ   0x1c7

T2CR       equ   0x1c8
T2CNTH     equ   0x1c9  
T2CNTL     equ   0x1ca  
T2LOADH    equ   0x1cb
T2LOADL    equ   0x1cc

TK0CR0     equ   0x1d0
TK0CR1     equ   0x1d1
TK0CR2     equ   0x1d2
TK0CNTH    equ   0x1d3
TK0CNTL    equ   0x1d4

TK1CR0     equ   0x1d8
TK1CR1     equ   0x1d9
TK1CR2     equ   0x1da
TK1CNTH    equ   0x1db
TK1CNTL    equ   0x1dc

ADCR0     equ   0x1e0
ADCR1     equ   0x1e1
ADCR2     equ   0x1e2
ADIO0     equ   0x1e3
ADIO1     equ   0x1e4
ADRH      equ   0x1e5  
ADRL      equ   0x1e6

LEDDS1    EQU   H'01E8'
LEDDS2    EQU   H'01E9'
LEDDS3    EQU   H'01EA'
LEDDS4    EQU   H'01EB'
LEDDS5    EQU   H'01EC'
LEDDS6    EQU   H'01ED'
LEDDS7    EQU   H'01EE'
LEDDS8    EQU   H'01EF'
LEDCR0    EQU   H'01F0'
LEDCR1    EQU   H'01F1'
LEDIOS0   EQU   H'01F2'
LEDIOS1   EQU   H'01F3'
LEDDRV    EQU   H'01F4' 


#define	Z	PFALG,2
#define	DC	PFALG,1
#define	C	PFALG,0


                     
;----- pflag bits ---a-----------------------------------------------------
;Z                       equ     0002h
;DC                      equ     0001h
;C                       equ     0000h
;----- mcr bits --------------------------------------------------------
gie                     equ     0007h
pd                      equ     0004h
to                      equ     0005h
mint11                  equ     0004h
mint10                  equ     0003h
mint01                  equ     0002h
mint00                  equ     0001h
;----- intecon bits --------------------------------------------------------  
adie                    equ     0006h
t2ie                    equ     0004h
int1ie                  equ     0003h
int0ie                  equ     0002h
t1ie                    equ     0001h
t0ie                    equ     0000h
;----- intflag bits --------------------------------------------------------
adif                    equ     0006h
t2if                    equ     0004h
int1if                  equ     0003h
int0if                  equ     0002h
t1if                    equ     0001h
t0if                    equ     0000h
;----- oscm bits --------------------------------------------------------
hfen                    equ     0000h
lfen                    equ     0001h
clks                    equ     0002h 
stbh                    equ     0004h
stbl                    equ     0005h   
;----tk0cr0 bits---------------------------------------------------------
tk0je                   equ     0005h
tk0rce                   equ     0004h
tk0clr                  equ     0003h
;----tk0cr1 bits---------------------------------------------------------
tk0if                   equ     0007h
tk0ie                   equ     0006h
;----tk1cr0 bits---------------------------------------------------------
tk1je                   equ     0005h
tk1rce                   equ     0004h
tk1clr                  equ     0003h
;----tk1cr1 bits---------------------------------------------------------
tk1if                   equ     0007h
tk1ie                   equ     0006h
;----ledcr0 bits---------------------------------------------------------
leden                   equ     0007h
duty                    equ     0006h
;-----------------------------------------------------------------------



     
passflg  equ     0x00   
passcnt  equ     0x01           
temp1    equ     0x02
temp2    equ     0x03
temp3    equ     0x04
mhbyte   equ     0x05
mlbyte   equ     0x06      
wsave    equ     0x07
ssave    equ     0x08
pcsave   equ     0x09
