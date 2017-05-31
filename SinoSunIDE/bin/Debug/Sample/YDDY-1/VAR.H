	ORG	$80
Flag1		DS	1
Flag2		DS	1
KeyCnt		DS	1
ADCCnt		DS	1	;计算平均值用
TempAD		DS	1
TempADBuf	DS	2
VAD			DS	1
VADBuf		DS	2
ADCnt		DS	1
Stauts		DS	1
KeyLong		DS	1
T50ms		DS	1
T100ms		DS	1
CellV		DS	1
T500ms		DS	1
Buf			DS	1
TempOpDly	DS	1
TempShDly	DS	1
FlashCnt	DS	1
V38CNT		DS	1
V32CNT		DS	1
CellDly		DS	1

Test		DS	1
Test1		DS	1
CellLow		EQU	$F1
;标志定义-------------------------------------

FTempOver	define	0,Flag1
FKeyAck		define	1,Flag1
FFlash50	define	2,Flag1	
FFlash100	define	3,Flag1 
FADOver		define	4,Flag1 
FERR 		define	5,Flag1 
F32V		define	6,Flag1 
F38V		define	7,Flag1 

F2ms		define	0,Flag2
FVin		define	1,Flag2
;C8ms		EQU	$80
;端口定义-------------------------------------
pKey	define	3,P0
pLED1	define	5,P1
pLED2	define	3,P1
pLED3	define	0,P1
pLED4	define	2,P0
pHeat	define	2,P1
pCharge	define	1,P1
pVIn	define	4,P0
pPwr	define	4,P1
;常数定义-------------------------------------
CV32	EQU $6F 	;3.2V
CV38	EQU	$84		;3.8V
CV41	EQU	$91		;4.15V
CT38	EQU	$6D
CT41	EQU $74
CT42	EQU	$76
CT45	EQU $7D
	

