/*****************************
Name:	MC20P02 Head File     
Author:	lxf                   
Date:	2011.01.07            
*****************************/
#ifndef _REG_MC20P02_H
#define	_REG_MC20P02_H

#include "common.h"
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
} p0lcon @ 0x03;

#define P0LCON  p0lcon.byte		
#define P0LCON0 p0lcon.bitn.bit0
#define P0LCON1 p0lcon.bitn.bit1
#define P0LCON2 p0lcon.bitn.bit2
#define P0LCON3 p0lcon.bitn.bit3
#define P0LCON4 p0lcon.bitn.bit4
#define P0LCON5 p0lcon.bitn.bit5
#define P0LCON6 p0lcon.bitn.bit6
#define P0LCON7 p0lcon.bitn.bit7

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
} p1 @ 0x04;

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
} ddr1 @ 0x05;

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
} p1hcon @ 0x06;

#define P1HCON  p1hcon.byte		
#define P1HCON0 p1hcon.bitn.bit0
#define P1HCON1 p1hcon.bitn.bit1
#define P1HCON2 p1hcon.bitn.bit2
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
} kbim @ 0x07;

#define KBIM  kbim.byte		
#define KBIM0 kbim.bitn.bit0
#define KBIM1 kbim.bitn.bit1
#define KBIM2 kbim.bitn.bit2
#define KBIM3 kbim.bitn.bit3
#define KBIM4 kbim.bitn.bit4
#define KBIM5 kbim.bitn.bit5
#define KBIM6 kbim.bitn.bit6

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
} p2 @ 0x08;

#define P2	p2.byte		
#define P20 p2.bitn.bit0
#define P21 p2.bitn.bit1

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
} ddr2 @ 0x09;

#define DDR2  ddr2.byte		
#define DDR20 ddr2.bitn.bit0
#define DDR21 ddr2.bitn.bit1
  
volatile char TDR 	@0x0a;

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
} tcr @ 0x0b;

#define TCR	tcr.byte	
#define TIF	tcr.bitn.bit7
#define TIM	tcr.bitn.bit6
#define PR	tcr.bitn.bit3_5
#define PTA	tcr.bitn.bit2
#define PTS	tcr.bitn.bit1
#define PTE	tcr.bitn.bit0

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
} p0pnd @ 0x0c;

#define P0PND	p0pnd.byte	
#define INT0E	p0pnd.bitn.bit7
#define INT0M	p0pnd.bitn.bit5_6
#define INT0F	p0pnd.bitn.bit4
#define INT1E	p0pnd.bitn.bit3
#define INT1M	p0pnd.bitn.bit1_2
#define INT1F	p0pnd.bitn.bit0

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
} mcr @ 0x0d;

#define MCR		mcr.byte	
#define KBIE	mcr.bitn.bit7
#define KBIC	mcr.bitn.bit6
#define WDTE	mcr.bitn.bit5
#define WDTC	mcr.bitn.bit4     
#define WDTF	mcr.bitn.bit3 
#define WDTM	mcr.bitn.bit2 
#define USEL	mcr.bitn.bit1 

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
} rstfr @ 0x0e;
 
#define RSTFR 	rstfr.byte	   
#define RSTFR3	rstfr.bitn.bit3
#define RSTFR2	rstfr.bitn.bit2
#define RSTFR1	rstfr.bitn.bit1    
#define RSTFR0	rstfr.bitn.bit0    

#endif	
