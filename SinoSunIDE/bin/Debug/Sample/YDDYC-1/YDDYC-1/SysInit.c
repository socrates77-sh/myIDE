#include  "EXT.h"
//;-----------------------------------------------------------------------
void SysInitSbr()
{
	P0 = 0;
	DDR0 = 4;     
	P0HCON = 0;
	P1 = 0;
	DDR1 = 0x3F;
	P1HCON = 0;
	KBIM = 0;
	ADCM = 0b01100011;		//;�ο���ѹΪVdd
	/*;-------------------------------------------------------------
	;T0 2ms
	;2M/2/16=16us 16us*125=2ms
	;-------------------------------------------------------------*/
	T0LOAD = 125;
	T0CON = 0b00011000; 
	//;---------------------------------------------------------------
	//;T1 stop
	//;---------------------------------------------------------------
	T1CON = 0b01000100;
	//;T2 noused
	T2CON = 0b01010100;

	INTC = 0;
	MCR = 0b01000000;
	//;RSTFR        EQU    $17
}
//;-----------------------------------------------------------------------
void SysInitRamSbr()
{
	#asm
	ram_clear:            
		LDX	#$71
	ram_clear1:
		CLR	$7F,X
		DECX
		BNE	ram_clear1
	#endasm
}
//;-------------------------------------------------------------------
void LEDAllOff()
{
	pLED1 = 0;
	pLED2 = 0;
	pLED3 = 0;
	pLED4 = 0;
}
