/*****************************
Name:	MC10P01 Head File     
Author:	lxf                   
Date:	2011.01.07            
*****************************/

volatile char KEYL	@0x00;	
#define	KEY	KEYL

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0_4       : 5;
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} mcr @ 0x01;

#define MCR	 mcr.byte		
#define KEYH mcr.bitn.bit0_4
#define OUTC mcr.bitn.bit5
#define KBIF mcr.bitn.bit6
#define KBIE mcr.bitn.bit7       

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
} ior @ 0x02;

#define IOR	 ior.byte		
#define S0   ior.bitn.bit0
#define S0D  ior.bitn.bit1
#define S0U  ior.bitn.bit2
#define S0E  ior.bitn.bit3
#define S11  ior.bitn.bit4
#define S11D ior.bitn.bit5
#define S11U ior.bitn.bit6
#define S11E ior.bitn.bit7

#define S11_AS_IO	S11E=0
#define S11_AS_KEY	S11E=1
#define S0_AS_IO	S0E=0
#define S0_AS_KEY	S0E=1
#define S0_INPUT	S0D=0
#define S0_OUTPUT	S0D=1
#define S11_INPUT	S11D=0
#define S11_OUTPUT	S11D=1
