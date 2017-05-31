#include  "ADC.h"

void InitialSys(void)
{ 
	//;端口初始化--------------------------------------------------------------------
	P0 = 0;
	P1 = 0;
	P2 = 0;
	//;P0 --------------------------------------------------------------------
	P0CONH = 0xFF;	
	P0CONL = 0xFF;	
	P0PND = 0;
	//;P1 --------------------------------------------------------------------
	P1CON = 0b00001010;	
	//;P2 --------------------------------------------------------------------
	P2CONH = 0b11111010;	//;
	P2CONL = 0b10100010;	
	//;定时器初始化***********************************************************
	//;BT---------------------------------------------------------------------
	//;fT = Fsys/2/4096/256 时钟频率=4.19时 fT0=2Hz,T=0.5s 
	//;-----------------------------------------------------------------------
	BTCON = 0b10101001;
	//;T0---------------------------------------------------------------------
	//;f = Fsys/1 时钟频率=4MHz时 fT0=2MHz,T=0.5us*250=125us 
	//;-----------------------------------------------------------------------
	T0DATA = 250;
	T0CON = 0b11111110;
	//;PWM--------------------------------------------------------------------
	//;f = Fsys/2/8/64 时钟频率=4.19时 fT0=4096Hz,T=244us 
	//;-----------------------------------------------------------------------
	PWMCONBit.IE = 0;
	//PWMCON = 0b01011100;
	//PWMDATA = 0x80;
	//;ADC--------------------------------------------------------------------
	//;f = Fsys/64/2/8 时钟频率=4.19时 fT0=4096Hz,T=244us 
	//;-----------------------------------------------------------------------
	//ADCON = 0b01010110;
}
//;===============================================================================
//	;初始化RAM
//	
#define	RAMSIZE		0xF0	//;变量用到的最大地址
//;===============================================================================
void InitalRAM()
{
	#asm
		CLRA
		LDX		#0xE0
	Inital_RAM1:	
		STA		,X
		DECX
		CPX		#$2F
		BHI		Inital_RAM1
	#endasm
}

