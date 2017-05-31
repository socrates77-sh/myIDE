/*****************************
Name:	MC20P01 Head File     
Author:	lxf                   
Date:	2011.01.07            
*****************************/
/*
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
*/
BYTE	p0@0x00;
#define P0	p0.byte		
#define P00 p0.bitn.bit0
#define P01 p0.bitn.bit1
#define P02 p0.bitn.bit2
#define P03 p0.bitn.bit3
#define P04 p0.bitn.bit4
#define P05 p0.bitn.bit5

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
#define DDR13 ddr1.bitn.bit3
#define DDR14 ddr1.bitn.bit4
#define DDR15 ddr1.bitn.bit5

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
#define P1HCON3 p1hcon.bitn.bit3
#define P1HCON4 p1hcon.bitn.bit4
#define P1HCON5 p1hcon.bitn.bit5

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
} p1lcon @ 0x06;

#define P1LCON  p1lcon.byte		
#define P1LCON0 p1lcon.bitn.bit0
#define P1LCON1 p1lcon.bitn.bit1
#define P1LCON3 p1lcon.bitn.bit3
#define P1LCON4 p1lcon.bitn.bit4
#define P1LCON5 p1lcon.bitn.bit5

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
} p1dcon @ 0x07;

#define P1DCON  p1dcon.byte		
#define P1DCON0 p1dcon.bitn.bit0
#define P1DCON1 p1dcon.bitn.bit1
#define P1DCON3 p1dcon.bitn.bit3
#define P1DCON4 p1dcon.bitn.bit4
#define P1DCON5 p1dcon.bitn.bit5

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
} kbim @ 0x08;

#define KBIM  kbim.byte		
#define KBIM0 kbim.bitn.bit0
#define KBIM1 kbim.bitn.bit1
#define KBIM2 kbim.bitn.bit2
#define KBIM3 kbim.bitn.bit3
#define KBIM4 kbim.bitn.bit4
#define KBIM5 kbim.bitn.bit5

volatile char T0CNT 	@0x09;
#define T0LOAD T0CNT

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
} t0cr @ 0x0a;

#define T0CR		t0cr.byte	
#define T0IF		t0cr.bitn.bit7
#define T0IM		t0cr.bitn.bit6
#define PR0 		t0cr.bitn.bit3_5
#define PTA			t0cr.bitn.bit2
#define PTS0		t0cr.bitn.bit1
#define BUZOUT	t0cr.bitn.bit0

volatile char T1DATA 	@0x0b;

volatile char T1CNT 	@0x0c;
#define T1LOAD T1CNT

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
} t1cr @ 0x0d;

#define T1CR		t1cr.byte	
#define T1IF		t1cr.bitn.bit7
#define T1IM		t1cr.bitn.bit6
#define PR1 		t1cr.bitn.bit3_5
#define T1EN		t1cr.bitn.bit2
#define PTS1		t1cr.bitn.bit1
#define PWMOUT	t1cr.bitn.bit0

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1_2       : 2;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5_6       : 2;
    unsigned char bit7         : 1; 	
  } bitn;
} intc @ 0x0e;

#define INTC	intc.byte	
#define INT0E	intc.bitn.bit7
#define INT0M	intc.bitn.bit5_6
#define INT0F	intc.bitn.bit4
#define INT1E	intc.bitn.bit3
#define INT1M	intc.bitn.bit1_2
#define INT1F	intc.bitn.bit0

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
} mcr @ 0x0f;

#define MCR		mcr.byte	
#define KBIE	mcr.bitn.bit7
#define KBIC	mcr.bitn.bit6
#define WDTC	mcr.bitn.bit4     
#define WDTF	mcr.bitn.bit3 
#define WDTM	mcr.bitn.bit2 

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
} rstfr @ 0x10;
 
#define RSTFR 	rstfr.byte	   
#define RSTFR3	rstfr.bitn.bit3
#define RSTFR2	rstfr.bitn.bit2
#define RSTFR1	rstfr.bitn.bit1    
#define RSTFR0	rstfr.bitn.bit0    

	
