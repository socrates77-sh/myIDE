/*****************************
Name:	MC20P38 Head File     
Author:	                  
Date:	2012.12.10            
*****************************/

#ifndef MC20P38_H
#define MC20P38_H


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

#define PB  pb.byte		
#define PB0 pb.bitn.bit0
#define PB1 pb.bitn.bit1
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
} pc @ 0x02;

#define PC  pc.byte		
#define PC0 pc.bitn.bit0
#define PC1 pc.bitn.bit1
#define PC2 pc.bitn.bit2
#define PC3 pc.bitn.bit3
#define PC4 pc.bitn.bit4
#define PC5 pc.bitn.bit5
#define PC6 pc.bitn.bit6
#define PC7 pc.bitn.bit7

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
} ddra @ 0x03;

#define DDRA	ddra.byte		
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
} ddrb @ 0x04;

#define DDRB  ddrb.byte		
#define DDRB0 ddrb.bitn.bit0
#define DDRB1 ddrb.bitn.bit1
#define DDRB2 ddrb.bitn.bit2
#define DDRB3 ddrb.bitn.bit3
#define DDRB4 ddrb.bitn.bit4
#define DDRB5 ddrb.bitn.bit5
#define DDRB6 ddrb.bitn.bit6
#define DDRB7 ddrb.bitn.bit7

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
} pahcon @ 0x05;

#define PAHCON  pahcon.byte		
#define PAHCON0 pahcon.bitn.bit0
#define PAHCON1 pahcon.bitn.bit1
#define PAHCON2 pahcon.bitn.bit2
#define PAHCON3 pahcon.bitn.bit3
#define PAHCON4 pahcon.bitn.bit4
#define PAHCON5 pahcon.bitn.bit5
#define PAHCON6 pahcon.bitn.bit6
#define PAHCON7 pahcon.bitn.bit7

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
} pbhcon @ 0x06;

#define PBHCON	pbhcon.byte		
#define PBHCON0 pbhcon.bitn.bit0
#define PBHCON1 pbhcon.bitn.bit1
#define PBHCON2 pbhcon.bitn.bit2
#define PBHCON3 pbhcon.bitn.bit3
#define PBHCON4 pbhcon.bitn.bit4
#define PBHCON5 pbhcon.bitn.bit5
#define PBHCON6 pbhcon.bitn.bit6

volatile char TDR0 	  @0x07;
volatile char TLOAD0 	@0x07;


volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 3;
    //unsigned char bit1         : 1;
    //unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 2; 	
    //unsigned char bit7         : 1; 	
  } bitn;
} tcr0 @ 0x08;

#define TCR0   tcr0.byte		
#define T0PS	 tcr0.bitn.bit0
//#define T0E		 tcr0.bitn.bit3
#define T0ON	 tcr0.bitn.bit4
#define T0S 	 tcr0.bitn.bit5
#define TOM 	 tcr0.bitn.bit6
//#define TCR07 tcr0.bitn.bit7


volatile char TDR1 	  @0x09;
volatile char TLOAD1 	@0x09;


volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 3;
    //unsigned char bit1         : 1;
    //unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} tcr1 @ 0x0a;

#define TCR1  tcr1.byte		
#define T1PS  tcr1.bitn.bit0
//#define TCR11 tcr1.bitn.bit1
//#define TCR12 tcr1.bitn.bit2
//#define TCR13 tcr1.bitn.bit3
#define T1ON  tcr1.bitn.bit4
//#define TCR15 tcr1.bitn.bit5
//#define TCR16 tcr1.bitn.bit6
#define T1M  tcr1.bitn.bit7



 volatile char TDR2 	@0x0B;  
 volatile char TLOADL2 	@0x0B;  
// volatile char T1CNTH 	@0x0D;  
// volatile char T1LOADH 	@0x0D;  



volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 3;
    //unsigned char bit1         : 1;
    //unsigned char bit2         : 1;
    unsigned char bit3       	 : 1;
    unsigned char bit4       	 : 1;
    unsigned char bit5       	 : 3;
    //unsigned char bit6         : 1; 	
    //unsigned char bit7         : 1; 	
  } bitn;
} tcr2 @ 0x0c;

#define TCR2	tcr2.byte	
#define T2PS	tcr2.bitn.bit1
#define T2ON	tcr2.bitn.bit4


//volatile char TK0CNTL 	@0x0f;
//volatile char TK0CNTH 	@0x10;

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
    unsigned char bit6	       : 1;
    unsigned char bit7         : 1; 	
  } bitn;
} ppgc @ 0x0d;

#define PPGC	ppgc.byte	   
#define PPGEN	ppgc.bitn.bit2  
#define PLEV	ppgc.bitn.bit3
#define PTSYN	ppgc.bitn.bit4
#define PSPEN	ppgc.bitn.bit5
#define PRSEN	ppgc.bitn.bit6
#define PST	ppgc.bitn.bit7

volatile char PPGT @0X0e;
volatile char NTCON @0X0f;

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 6;
    //unsigned char bit2         : 1;
    //unsigned char bit3         : 1;
    //unsigned char bit4         : 1;	
    //unsigned char bit5         : 1;
    unsigned char bit6	       : 1;
    unsigned char bit7         : 1; 	
  } bitn;
} intdb @ 0x10;

#define INTDB	intdb.byte
#define DBC		intdb.bitn.bit0
#define INTS	intdb.bitn.bit6
#define INTINV intdb.bitn.bit7	


volatile char CVREF @0X11;
volatile char PPGDL @0x12;


volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 6;
    //unsigned char bit2         : 1;
    //unsigned char bit3         : 1;
    //unsigned char bit4         : 3;	
    //unsigned char bit5         : 1;
    unsigned char bit6	       : 1;
    unsigned char bit7         : 1; 	
  } bitn;
} cmp0c @ 0x13;

#define CMP0C	 cmp0c.byte
#define C0FOF	 cmp0c.bitn.bit0
#define C0CRS	 cmp0c.bitn.bit6
#define C0COFM cmp0c.bitn.bit7


volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 6;
    //unsigned char bit2         : 1;
    //unsigned char bit3         : 1;
    //unsigned char bit4         : 3;	
    //unsigned char bit5         : 1;
    unsigned char bit6	       : 1;
    unsigned char bit7         : 1; 	
  } bitn;
} cmp1c @ 0x14;

#define CMP1C	 cmp1c.byte
#define C1FOF	 cmp1c.bitn.bit0
#define C1CRS	 cmp1c.bitn.bit6
#define C1COFM cmp1c.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 6;
    //unsigned char bit2         : 1;
    //unsigned char bit3         : 1;
    //unsigned char bit4         : 3;	
    //unsigned char bit5         : 1;
    unsigned char bit6	       : 1;
    unsigned char bit7         : 1; 	
  } bitn;
} cmp2c @ 0x15;

#define CMP2C	 cmp2c.byte
#define C2FOF	 cmp2c.bitn.bit0
#define C2CRS	 cmp2c.bitn.bit6
#define C2COFM cmp2c.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 6;
    //unsigned char bit2         : 1;
    //unsigned char bit3         : 1;
    //unsigned char bit4         : 3;	
    //unsigned char bit5         : 1;
    unsigned char bit6	       : 1;
    unsigned char bit7         : 1; 	
  } bitn;
} opac @ 0x16;

#define OPAC	 opac.byte
#define OPAOF	 opac.bitn.bit0
#define OPARS	 opac.bitn.bit6
#define OPAOFM opac.bitn.bit7


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
    unsigned char bit6	       : 1;
    unsigned char bit7         : 1; 	
  } bitn;
} intc0 @ 0x17;

#define INTC0	intc0.byte	   
#define T2F		intc0.bitn.bit2  
#define T2E		intc0.bitn.bit3
#define T1F		intc0.bitn.bit4
#define T1E		intc0.bitn.bit5
#define T0F		intc0.bitn.bit6
//#define T0E	  intc0.bitn.bit7

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
    unsigned char bit6	       : 1;
    unsigned char bit7         : 1; 	
  } bitn;
} intc1 @ 0x18;

#define INTC1	intc1.byte
#define LVDIF		intc1.bitn.bit0	   
#define LVDIE		intc1.bitn.bit1  
#define CMP2F		intc1.bitn.bit2
#define CMP2E		intc1.bitn.bit3
#define CMP1F		intc1.bitn.bit4
#define CMP1E		intc1.bitn.bit5
#define INTF		intc1.bitn.bit6
#define INTE	  intc1.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 1;
    unsigned char bit1         : 2;
    //unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 4;	
    //unsigned char bit5         : 1;
    //unsigned char bit6	       : 1;
    //unsigned char bit7         : 1; 	
  } bitn;
} adcon @ 0x19;

#define ADCON		adcon.byte
#define ADCE		adcon.bitn.bit0	   
#define ADPS		adcon.bitn.bit1  
//#define CMP2F		adcon.bitn.bit2
#define ADCH		adcon.bitn.bit3
//#define CMP1F		adcon.bitn.bit4
//#define CMP1E		adcon.bitn.bit5
//#define INTF		adcon.bitn.bit6
//#define INTE	  adcon.bitn.bit7


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
} adcm @ 0x1A;

#define ADCM  adcm.byte		
#define ADCM0 adcm.bitn.bit0
#define ADCM1 adcm.bitn.bit1
#define ADCM2 adcm.bitn.bit2
#define ADCM3 adcm.bitn.bit3
#define ADCM4 adcm.bitn.bit4
#define ADCM5 adcm.bitn.bit5
#define ADCM6 adcm.bitn.bit6
#define ADCM7 adcm.bitn.bit7

volatile char ADTH @0X1b;
volatile char ADTL @0x1c;

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
} mcr @ 0x1d;

#define mcr     mcr.byte		
#define WDTC	  mcr.bitn.bit0
#define LVD0 	  mcr.bitn.bit1
#define LVDE	  mcr.bitn.bit2
#define OSTPC   mcr.bitn.bit3
#define OPAEN   mcr.bitn.bit4
#define CMP2EN  mcr.bitn.bit5
#define CMP1EN  mcr.bitn.bit6
#define CMP0EN  mcr.bitn.bit7

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
} rstfr @ 0x1e;

#define RSTFR      rstfr.byte		
#define RSTF0	   rstfr.bitn.bit0
#define RSTF1 	 rstfr.bitn.bit1
#define RSTF2	   rstfr.bitn.bit2
#define RSTF3    rstfr.bitn.bit3
//#define OPAEN    rstfr.bitn.bit4
//#define CMP2EN   rstfr.bitn.bit5
//#define CMP1EN   rstfr.bitn.bit6
//#define CMP0EN   rstfr.bitn.bit7

volatile union
{
  unsigned char byte;
  struct
  {
    unsigned char bit0         : 2;
    //unsigned char bit1         : 1;
    unsigned char bit2         : 1;
    unsigned char bit3         : 1;
    unsigned char bit4         : 1;	
    unsigned char bit5         : 1; 
    unsigned char bit6         : 1; 	
    unsigned char bit7         : 1; 	
  } bitn;
} mcrx @ 0x1f;

#define MCRX    mcrx.byte		
#define PPGTEX	   mcrx.bitn.bit0
//#define RSTF1 	 mcrx.bitn.bit1
//#define RSTF2	   mcrx.bitn.bit2
//#define RSTF3    mcrx.bitn.bit3
#define OPAMPOP   rstfr.bitn.bit4
#define C2CMP0P   rstfr.bitn.bit5
#define C1CMP0P   rstfr.bitn.bit6
#define C0CMP0P   rstfr.bitn.bit7

	               
#endif