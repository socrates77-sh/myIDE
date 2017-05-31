/*****************************
Name:	MC10P02r Head File     
Author:	lxf                   
Date:	2011.01.07            
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
} pa @ 0x00;

#define PA	pa.byte		
#define PA0 pa.bitn.bit0
#define PA1 pa.bitn.bit1
#define PA2 pa.bitn.bit2
#define PA3 pa.bitn.bit3
#define PA4 pa.bitn.bit4
#define PA5 pa.bitn.bit5
#define PA6 pa.bitn.bit6
#define PA7 pa.bitn.bit7

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
} pb @ 0x01;

#define PB	pb.byte		
#define PB0 pb.bitn.bit0
#define PB2 pb.bitn.bit2
#define PB3 pb.bitn.bit3
#define PB4 pb.bitn.bit4
#define PB5 pb.bitn.bit5
#define PB6 pb.bitn.bit6
#define PB7 pb.bitn.bit7

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
} ddra @ 0x04;

#define DDRA  ddra.byte		
#define DDRA0 ddra.bitn.bit0
#define DDRA1 ddra.bitn.bit1
#define DDRA2 ddra.bitn.bit2
#define DDRA3 ddra.bitn.bit3
#define DDRA4 ddra.bitn.bit4
#define DDRA5 ddra.bitn.bit5
#define DDRA6 ddra.bitn.bit6
#define DDRA7 ddra.bitn.bit7

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
} ddrb @ 0x05;

#define DDRB  ddrb.byte		
#define DDRB0 ddrb.bitn.bit0
#define DDRB2 ddrb.bitn.bit2
#define DDRB3 ddrb.bitn.bit3
#define DDRB4 ddrb.bitn.bit4
#define DDRB5 ddrb.bitn.bit5
#define DDRB6 ddrb.bitn.bit6
#define DDRB7 ddrb.bitn.bit7

volatile char TDR 	@0x08;

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0_2       : 3;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;
    unsigned char bit5         : 1;
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} tcr @ 0x09;

#define TCR		tcr.byte	
#define TIF		tcr.bitn.bit7
#define TIM		tcr.bitn.bit6
#define PRER	tcr.bitn.bit3
#define PR		tcr.bitn.bit0_2


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
} kbim @ 0x0b;

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
} mcr @ 0x0c;

#define MCR		mcr.byte	
#define KBIE	mcr.bitn.bit7
#define KBIC	mcr.bitn.bit6
#define PBP		mcr.bitn.bit4     
#define PBP3	mcr.bitn.bit3 
#define PBP2	mcr.bitn.bit2 
#define OUTC	mcr.bitn.bit1 
#define FCAE	mcr.bitn.bit0 

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
} pc @ 0x0d;

#define PC	pc.byte		
#define PC0 pc.bitn.bit0
#define PC1 pc.bitn.bit1

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
} ddrc @ 0x0e;

#define DDRC  ddrc.byte		
#define DDRC0 ddrc.bitn.bit0
#define DDRC1 ddrc.bitn.bit1
  





