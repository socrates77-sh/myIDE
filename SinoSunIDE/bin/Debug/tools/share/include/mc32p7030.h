#ifndef __MC32P7030__H
#define __MC32P7030__H

void _ClearWatchDogTimer(void);
void _ClrRAM(void);

#define NOP(X)  	__asm{	_NOP X }
#define LOG(X)  	__asm{	LOG CNameToAsmLabel(X) }
sfr	HIBYTE = 0x82;
sfr	FSR0 = 0x83;
sfr	FSR1 = 0x84;
sfr	PFLAG = 0x86;
sbit	Z = 0x86:0;
sbit	DC = 0x86:1;
sbit	C = 0x86:2;
sbit	LVD24 = 0x86:4;
sbit	LVD36 = 0x86:5;
sbit	NPD = 0x86:6;
sbit	NT0 = 0x86:7;
sfr	ANSEL = 0xae;
sfr	VREF = 0xaf;
sbit	VRS0 = 0xaf:0;
sbit	VRS1 = 0xaf:1;
sbit	VREFS = 0xaf:7;
sfr	ADCR = 0xB1;
sbit	ADON = 0xB1:7;
sbit	ADST = 0xB1:6;
sbit	ADEOC = 0xB1:5;
sbit	GCHS = 0xB1:4;
sbit	ADCHS2 = 0xB1:2;
sbit	ADCHS1 = 0xB1:1;
sbit	ADCHS0 = 0xB1:0;
sfr	ADRH = 0xb2;
sfr	ADRL = 0xb3;
sbit	ADCKS1 = 0xb3:6;
sbit	ADCKS0 = 0xb3:4;
sbit	ADR3 = 0xb3:3;
sbit	ADR1 = 0xb3:2;
sbit	ADR2 = 0xb3:1;
sbit	ADR0 = 0xb3:0;
sfr	OEP0 = 0xb8;
sbit	P00OE = 0xb8:0;
sbit	P01OE = 0xb8:1;
sbit	P02OE = 0xb8:2;
sbit	P03OE = 0xb8:3;
sbit	P04OE = 0xb8:4;	
sfr	PEDGE = 0xbf;
sbit	MINT01 = 0xbf:4;
sbit	MINT00 = 0xbf:3;
sfr	OEP4 = 0xc4;
sbit	P40OE = 0xc4:0;
sbit	P41OE = 0xc4:1;
sbit	P42OE = 0xc4:2;
sbit	P43OE = 0xc4:3;
sbit	P44OE = 0xc4:4;
sbit	P45OE = 0xc4:5;	
sfr	OEP5 = 0xc5;
sbit	P52OE = 0xc5:2;
sbit	P53OE = 0xc5:3;
sbit	P54OE = 0xc5:4;
sfr	INTF = 0xc8;
sbit	INT0IF = 0xc8:0;
sbit	INT1IF = 0xc8:1;
sbit	T2IF = 0xc8:4;	
sbit	T0IF = 0xc8:5;
sbit	T1IF = 0xc8:6;
sbit	ADIF = 0xc8:7;
sfr	INTE = 0xc9;
sbit	INT0IE = 0xc9:0;
sbit	INT1IE = 0xc9:1;
sbit	T2IE = 0xc9:4;	
sbit	T0IE = 0xc9:5;
sbit	T1IE = 0xc9:6;
sbit	ADIE = 0xc9:7;
sfr	OSCM = 0xca;
sbit	CPUM1 = 0xca:4;
sbit	CPUM0 = 0xca:3;
sbit	CLKS = 0xca:2;
sbit	HOFF = 0xca:1;
sfr	WDTR = 0xcc;
sfr	T0D = 0xcd;
sfr	PCL = 0xce;
sfr	PCH = 0xcf;
sfr	IOP0 = 0xd0;
sbit	P00D = 0xd0:0;
sbit	P01D = 0xd0:1;
sbit	P02D = 0xd0:2;
sbit	P03D = 0xd0:3;
sbit	P04D = 0xd0:4;
sfr	IOP4 = 0xd4;
sbit	P40D = 0xd4:0;
sbit	P41D = 0xd4:1;
sbit	P42D = 0xd4:2;
sbit	P43D = 0xd4:3;
sbit	P44D = 0xd4:4;
sbit	P45D = 0xd4:5;	
sfr	IOP5 = 0xd5;
sbit	P52D = 0xd5:2;
sbit	P53D = 0xd5:3;
sbit	P54D = 0xd5:4;
sfr	TXCR = 0xd8;
sbit	T0GE = 0xd8:1;
sbit	T0PTSX = 0xd8:2;
sbit	T1PTSX = 0xd8:3;
sbit	T2PTSX = 0xd8:4;	
sfr	T0CR = 0xda;
sbit	TC0EN = 0xda:7;
sbit	T0PR2 = 0xda:6;
sbit	T0PR1 = 0xda:5;
sbit	T0PR0 = 0xda:4;
sbit	T0PTS = 0xda:3;
sbit	T0ALOAD = 0xda:2;
sbit	BUZ0OE = 0xda:1;
sbit	PWM0OE = 0xda:0;
sfr	T0C = 0xdb;
sfr	T1CR = 0xdc;
sbit	TC1EN = 0xdc:7;
sbit	T1PR2 = 0xdc:6;
sbit	T1PR1 = 0xdc:5;
sbit	T1PR0 = 0xdc:4;
sbit	T1PTS = 0xdc:3;
sbit	T1ALOAD = 0xdc:2;
sbit	BUZ1OE = 0xdc:1;
sbit	PWM1OE = 0xdc:0;
sfr	T1C = 0xdd;
sfr	T1D = 0xde;
sfr	STKP = 0xdf;
sbit	GIE = 0xdf:7;
sbit	STKP2 = 0xdf:2;
sbit	STKP1 = 0xdf:1;
sbit	STKP0 = 0xdf:0;
sfr	PUP0 = 0xe0;
sfr	PUP4 = 0xe4;
sfr	PUP5 = 0xe5;
sfr	INDF = 0xe7;
sfr	STK3L = 0xf8;
sfr	STK3H = 0xf9;
sfr	STK2L = 0xfa;
sfr	STK2H = 0xfb;
sfr	STK1L = 0xfc;
sfr	STK1H = 0xfd;
sfr	STK0L = 0xfe;
sfr	STK0H = 0xff;
#endif
