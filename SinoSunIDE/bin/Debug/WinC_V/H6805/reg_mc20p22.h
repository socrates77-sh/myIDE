/*****************************
Name:	MC20P22 Head File     
Author:	lxf                   
Date:	2011.01.07            
*****************************/

#ifndef mc20p22_h
#define mc20p22_h

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
#define P1HCON6 p1hcon.bitn.bit6

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
} kbim @ 0x06;

#define KBIM  kbim.byte		
#define KBIM0 kbim.bitn.bit0
#define KBIM1 kbim.bitn.bit1
#define KBIM2 kbim.bitn.bit2
#define KBIM3 kbim.bitn.bit3
#define KBIM4 kbim.bitn.bit4
#define KBIM5 kbim.bitn.bit5
#define KBIM6 kbim.bitn.bit6

volatile char T0CNT 	@0x07;
#define TLOAD0 T0CNT

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
} tcr0 @ 0x08;

#define TCR0		tcr0.byte	
#define T0IF		tcr0.bitn.bit7
#define T0IM		tcr0.bitn.bit6
#define PR0 		tcr0.bitn.bit3_5
#define PTA			tcr0.bitn.bit2
#define PTS0		tcr0.bitn.bit1
#define BUZOUT	tcr0.bitn.bit0

volatile char TDATA1 	@0x09;

volatile char T1CNT 	@0x0a;
#define TLOAD1 T1CNT

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
} tcr1 @ 0x0b;

#define TCR1		tcr1.byte	
#define T1IF		tcr1.bitn.bit7
#define T1IM		tcr1.bitn.bit6
#define PR1 		tcr1.bitn.bit3_5
#define TMR1EN	tcr1.bitn.bit2
#define PTS1		tcr1.bitn.bit1
#define PWM0OUT	tcr1.bitn.bit0

volatile char TDATA2H 	@0x0c;
volatile char T2CNTH 	@0x0d;
#define TLOAD2H T2CNTH

volatile char TDATA2L 	@0x0e;

volatile char T1CNTL 	@0x0f;
#define TLOAD2L T1CNTL

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
} tcr2 @ 0x10;

#define TCR2		tcr2.byte	
#define T2IF		tcr2.bitn.bit7
#define T2IM		tcr2.bitn.bit6
#define PR2 		tcr2.bitn.bit3_5
#define TMR2EN	tcr2.bitn.bit2
#define PTS2		tcr2.bitn.bit1
#define PWM1OUT	tcr2.bitn.bit0


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
} intc @ 0x11;

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
    unsigned char bit4_0       : 5;	
    unsigned char bit6_5       : 2; 
    unsigned char bit7         : 1; 	
  } bitn;
} adcm @ 0x12;

#define ADCM		adcm.byte	
#define VREF		adcm.bitn.bit7
#define VHS			adcm.bitn.bit6_5
#define ADE		adcm.bitn.bit4_0

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit2_1       : 2;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit7_5       : 3; 	
  } bitn;
} adcon @ 0x13;

#define ADCON	adcon.byte	
#define ADCH	adcon.bitn.bit7_5
#define ADEN	adcon.bitn.bit4
#define EOC		adcon.bitn.bit3 
#define ADPS	adcon.bitn.bit2_1     
#define ADCE	adcon.bitn.bit0 

volatile uchar const ADTH 	@0x14;
volatile uchar const ADTL 	@0x15;	

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
} mcr @ 0x16;

#define MCR		mcr.byte	
#define KBIE	mcr.bitn.bit7
#define KBIC	mcr.bitn.bit6
#define ADCM_P12	mcr.bitn.bit5
#define WDTC	mcr.bitn.bit4     
#define WDTF	mcr.bitn.bit3 
#define WDTM	mcr.bitn.bit2      
#define LVR_36	mcr.bitn.bit1 
#define LVR_42	mcr.bitn.bit0 

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
} rstfr @ 0x17;
 
#define RSTFR 	rstfr.byte	   
#define RSTFR3	rstfr.bitn.bit3
#define RSTFR2	rstfr.bitn.bit2
#define RSTFR1	rstfr.bitn.bit1    
#define RSTFR0	rstfr.bitn.bit0    

#endif	
