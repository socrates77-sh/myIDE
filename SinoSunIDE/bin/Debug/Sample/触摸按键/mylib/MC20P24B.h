#ifndef _H_MC20P24B
#define _H_MC20P24B

#include "mylib/macros.h"
//#include "mylib/EXTI.h"
#include "mylib/T0.h"

vuchar const T0CNT	@0x00;
vuchar T0DATA		@0x01;
vuchar T0CON		@0x02;
vuchar MCR			@0x03;

vuchar BTCON		@0x0C;
vuchar BTCNT		@0x0D;

vuchar P0			@0x10;
vuchar P1			@0x11;
vuchar P2			@0x12;
vuchar P0CONH		@0x16;
vuchar P0CONL		@0x17;
vuchar P0PND		@0x18;
vuchar P1CON		@0x19;
vuchar P2CONH		@0x1A;
vuchar P2CONL		@0x1B;

vuchar PWMDATA		@0x22;
vuchar PWMCON		@0x23;

vuchar ADCON		@0x28;
vuchar ADDATAH		@0x28;
vuchar ADDATAL		@0x29;

struct
{
	uchar			:7;
	uchar	LVR		:1;
}OPTION @0x03;
	
struct
{
	uchar	PSClear		:1;
	uchar	Clear		:1;
	uchar	Prescaler	:2;
	uchar	Enabled		:4;
}WDT @0x0C;

#define WDT_RESET() 	{ for(;;){} }
#define WDT_EN()    	{ WDT.Enabled=0xA; }
#define WDT_DIS()   	{ WDT.Enabled=0; }
#define WDT_CLR()		{ WDT.Clear=true; }
#define WDT_PSClear()  	{ WDT.PSClear=true }
#define WDT_PS(p)		{ WDT.Prescaler=WDTPS##p; }

struct
{
	uchar	P00		:1;
	uchar	P01		:1;
	uchar	P02		:1;
	uchar	P03		:1;
	uchar	P04		:1;
	uchar	P05		:1;
	uchar	P06		:1;
	uchar	P07		:1;
}P0Bit @0x10;

struct
{
	uchar	P10		:1;
	uchar	P11		:1;
	uchar	P12		:1;
	uchar	P13		:1;
	uchar	P14		:1;
	uchar	P15		:1;
	uchar	P16	:1;
	uchar	P17		:1;
}P1Bit @0x11;

struct
{
	uchar	P20		:1;
	uchar	P21		:1;
	uchar	P22		:1;
	uchar	P23		:1;
	uchar	P24		:1;
	uchar	P25		:1;
	uchar	P26		:1;
	uchar	P27		:1;
}P2Bit @0x12;


struct
{
	uchar			:7;
	uchar	EN		:1;
}P11OC	@0x19;
struct
{
	uchar			:6;
	uchar	EN		:1;
	uchar			:1;
}P10OC	@0x19;
struct
{
	uchar	P10C	:2;
	uchar	P11C	:2;
	uchar			:4;
 }P1C	@0x19;

struct
{
	uchar	P04C	:2;
	uchar	P05C	:2;
	uchar	P06C	:2;
	uchar	P07C	:2;
}P0CONHBit	@0x16;

struct
{
	uchar	P00C	:2;
	uchar	P01C	:2;
	uchar	P02C	:2;
	uchar	P03C	:2;
}P0CONLBit	@0x17;
struct
{
	uchar	P24C	:2;
	uchar	P25C	:2;
	uchar	P26C	:2;
	uchar	P27C	:2;
}P2CONHBit	@0x1A;

struct
{
	uchar	P20C	:2;
	uchar	P21C	:2;
	uchar	P22C	:2;
	uchar	P23C	:2;
}P2CONLBit	@0x1B;


struct
{
	uchar	IF			:1;
	uchar	IE			:1;
	uchar	Start		:1;
	uchar	Clear		:1;
	uchar	PWMDRS		:1;
	uchar				:1;
	uchar	Prescaler	:2;
}PWMCONBit	@0x23;

struct
{
	uchar	ADCE		:1;
	uchar	Prescaler	:2;
	uchar	EOC			:1;
	uchar	Chanel		:4;
} ADCONBit	@0x27;

#define ADChanel(i) {ADCONBit.Chanel = i;}
#define ADPrescaler(i) {ADCONBit.Prescaler = i;}
#define ADOver 		ADCONBit.EOC
#define ADStart()	{ADCONBit.ADCE = 1;}
//TIM.Prescaler
enum
{
    PS4096,
    PS256,
    PS8,
    PS1
};
//WDT.Prescaler
enum
{
    WDTPS4096,
    WDTPS1024,
    WDTPS256,
    WDTPS128
};


#define NOP()		__asm("nop")
#define DI()    	__asm("sei")
#define EI()		__asm("cli")
#define SLEEP()		__asm("wait")
#define STOP()		__asm("stop")
#define InitSP()    __asm("RSP")
#endif	
