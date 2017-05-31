/*****************************
Name:	MC20P68 Head File     
Author:	                  
Date:	2012.06.19            
*****************************/

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} p0 @ 0x00;

#define P0	p0.byte		
#define P00 p0.bitn.bit0
#define P01 p0.bitn.bit1
#define P02 p0.bitn.bit2
#define P03 p0.bitn.bit3
#define P04 p0.bitn.bit4
#define P05 p0.bitn.bit5
#define P06 p0.bitn.bit6
#define P07 p0.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} ddr0 @ 0x01;

#define DDR0  ddr0.byte		
#define DDR00 ddr0.bitn.bit0
#define DDR01 ddr0.bitn.bit1
#define DDR02 ddr0.bitn.bit2
#define DDR03 ddr0.bitn.bit3
#define DDR04 ddr0.bitn.bit4
#define DDR05 ddr0.bitn.bit5
#define DDR06 ddr0.bitn.bit6
#define DDR07 ddr0.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} p0hcon @ 0x02;

#define P0HCON  p0hcon.byte		
#define P0HCON0 p0hcon.bitn.bit0
#define P0HCON1 p0hcon.bitn.bit1
#define P0HCON2 p0hcon.bitn.bit2
#define P0HCON3 p0hcon.bitn.bit3
#define P0HCON4 p0hcon.bitn.bit4
#define P0HCON5 p0hcon.bitn.bit5
#define P0HCON6 p0hcon.bitn.bit6
#define P0HCON7 p0hcon.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} p1 @ 0x03;

#define P1	p1.byte		
#define P10 p1.bitn.bit0
#define P11 p1.bitn.bit1
#define P12 p1.bitn.bit2
#define P13 p1.bitn.bit3
#define P14 p1.bitn.bit4
#define P15 p1.bitn.bit5
#define P16 p1.bitn.bit6
#define P17 p1.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} ddr1 @ 0x04;

#define DDR1  ddr1.byte		
#define DDR10 ddr1.bitn.bit0
#define DDR11 ddr1.bitn.bit1
#define DDR12 ddr1.bitn.bit2
#define DDR13 ddr1.bitn.bit3
#define DDR14 ddr1.bitn.bit4
#define DDR15 ddr1.bitn.bit5
#define DDR16 ddr1.bitn.bit6
#define DDR17 ddr1.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} p1hcon @ 0x05;

#define P1HCON  p1hcon.byte		
#define P1HCON0 p1hcon.bitn.bit0
#define P1HCON1 p1hcon.bitn.bit1
#define P1HCON2 p1hcon.bitn.bit2
#define P1HCON3 p1hcon.bitn.bit3
#define P1HCON4 p1hcon.bitn.bit4
#define P1HCON5 p1hcon.bitn.bit5
#define P1HCON6 p1hcon.bitn.bit6
#define P1HCON7 p1hcon.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} p2 @ 0x06;

#define P2	p2.byte		
#define P20 p2.bitn.bit0
#define P21 p2.bitn.bit1
#define P22 p2.bitn.bit2
#define P23 p2.bitn.bit3
#define P24 p2.bitn.bit4
#define P25 p2.bitn.bit5
#define P26 p2.bitn.bit6

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} ddr2 @ 0x07;

#define DDR2  ddr2.byte		
#define DDR20 ddr2.bitn.bit0
#define DDR21 ddr2.bitn.bit1
#define DDR22 ddr2.bitn.bit2
#define DDR23 ddr2.bitn.bit3
#define DDR24 ddr2.bitn.bit4
#define DDR25 ddr2.bitn.bit5
#define DDR26 ddr2.bitn.bit6

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} p2hcon @ 0x08;

#define P2HCON  p2hcon.byte		
#define P2HCON0 p2hcon.bitn.bit0
#define P2HCON1 p2hcon.bitn.bit1
#define P2HCON2 p2hcon.bitn.bit2
#define P2HCON3 p2hcon.bitn.bit3
#define P2HCON4 p2hcon.bitn.bit4
#define P2HCON5 p2hcon.bitn.bit5
#define P2HCON6 p2hcon.bitn.bit6

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} kbim @ 0x09;

#define KBIM  kbim.byte		
#define KBIM0 kbim.bitn.bit0
#define KBIM1 kbim.bitn.bit1
#define KBIM2 kbim.bitn.bit2
#define KBIM3 kbim.bitn.bit3
#define KBIM4 kbim.bitn.bit4
#define KBIM5 kbim.bitn.bit5

volatile char T0CNT 	@0x0a;
volatile char T0LOAD 	@0x0a;

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3_5       : 3;
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} tcr0 @ 0x0b;

#define TCR0	tcr0.byte	
#define T0IF	tcr0.bitn.bit7
#define T0IM	tcr0.bitn.bit6
#define P0R	tcr0.bitn.bit3_5
#define PTA0	tcr0.bitn.bit2

volatile char T1CNTL 	@0x0c;
volatile char T1LOADL 	@0x0c;
volatile char T1CNTH 	@0x0D;
volatile char T1LOADH 	@0x0D;

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3_5       : 3;
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} tcr1 @ 0x0e;

#define TCR1	tcr.byte	
#define T1IF	tcr.bitn.bit7
#define T1IM	tcr.bitn.bit6
#define P1R	tcr.bitn.bit3_5
#define TMR1EM	tcr.bitn.bit2
#define PTS1	tcr.bitn.bit1
#define TMODE	tcr.bitn.bit0


volatile char TK0CNTL 	@0x0f;
volatile char TK0CNTH 	@0x10;

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0_2       : 3;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1;
    unsigned char bit6_7       : 2; 	
  } bitn;
} tk0con0 @ 0x11;

#define TK0CON0	tk0con0.byte	
#define TK0S	tk0con0.bitn.bit6_7
#define TK0ON	tk0con0.bitn.bit5
#define TK0JE	tk0con0.bitn.bit3
#define TK0FS	tk0con0.bitn.bit0_2

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5_7       : 3; 	
  } bitn;
} tk0con1 @ 0x12;

#define TK0CON1	tk0con1.byte	
#define TK0EN	tk0con1.bitn.bit4
#define TK0IO3	tk0con1.bitn.bit3
#define TK0IO2	tk0con1.bitn.bit2
#define TK0IO1	tk0con1.bitn.bit1
#define TK0IO0	tk0con1.bitn.bit0

volatile char TK1CNTL 	@0x13;
volatile char TK1CNTH 	@0x14;

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0_2       : 3;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1;
    unsigned char bit6_7       : 2; 	
  } bitn;
} tk1con0 @ 0x15;

#define TK1CON0	tk0con1.byte	
#define TK1S	tk0con1.bitn.bit6_7
#define TK1ON	tk0con1.bitn.bit5
#define TK1JE	tk0con1.bitn.bit3
#define TK1FS	tk0con1.bitn.bit0_2

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5_7       : 3; 	
  } bitn;
} tk1con1 @ 0x16;

#define TK1CON1	tk1con1.byte	
#define TK1EN	tk1con1.bitn.bit4
#define TK1IO3	tk1con1.bitn.bit3
#define TK1IO2	tk1con1.bitn.bit2
#define TK1IO1	tk1con1.bitn.bit1
#define TK1IO0	tk1con1.bitn.bit0

volatile char TK2CNTL 	@0x17;
volatile char TK2CNTH 	@0x18;

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0_2       : 3;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1;
    unsigned char bit6_7       : 2; 	
  } bitn;
} tk2con0 @ 0x19;

#define TK2CON0	tk2con0.byte	
#define TK2S	tk2con0.bitn.bit6_7
#define TK2ON	tk2con0.bitn.bit5
#define TK2JE	tk2con0.bitn.bit3
#define TK2FS	tk2con0.bitn.bit0_2

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5_7       : 3; 	
  } bitn;
} tk2con1 @ 0x1a;

#define TK2CON1	tk2con1.byte	
#define TK2EN	tk2con1.bitn.bit4
#define TK2IO3	tk2con1.bitn.bit3
#define TK2IO2	tk2con1.bitn.bit2
#define TK2IO1	tk2con1.bitn.bit1
#define TK2IO0	tk2con1.bitn.bit0

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} intc @ 0x1b;

#define INTC	intc.byte
#define INTE	intc.bitn.bit7
#define INTF	intc.bitn.bit6
#define TK0E	intc.bitn.bit5
#define TK0F	intc.bitn.bit4
#define TK1E	intc.bitn.bit3
#define TK1F	intc.bitn.bit2
#define TK2E	intc.bitn.bit1
#define TK2F	intc.bitn.bit0

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0_1       : 2;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} mcr @ 0x1c;

#define MCR		mcr.byte	
#define KBIE	mcr.bitn.bit7
#define KBIC	mcr.bitn.bit6
#define WDTE	mcr.bitn.bit5
#define WDTC	mcr.bitn.bit4     
#define WDTF	mcr.bitn.bit3 
#define WDTM	mcr.bitn.bit2 
#define INT_MOD	mcr.bitn.bit0_1 

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} rstfr @ 0x1d;
 
#define RSTFR 	rstfr.byte	   
#define RSTFR3	rstfr.bitn.bit3
#define RSTFR2	rstfr.bitn.bit2
#define RSTFR1	rstfr.bitn.bit1    
#define RSTFR0	rstfr.bitn.bit0    

	
