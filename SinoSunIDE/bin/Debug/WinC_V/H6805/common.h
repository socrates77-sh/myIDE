
#ifndef COMMON_H
#define	COMMON_H

#define uchar unsigned char
#define uint  unsigned int
typedef union bit_char BYTE;
typedef union bit_int  WORD;

#define SEI()  _asm("sei")
#define CLI()  _asm("cli")
#define RSP()  _asm("rsp")
#define NOP()  _asm("nop")
#define STOP() _asm("stop")

#define BSET(x, y)  ((x).bitn.bit ## y = 1) //eg p1.bitn.bit1 = 1
#define BCLR(x, y)  ((x).bitn.bit ## y = 0) //eg p1.bitn.bit1 = 0
#define CLRBYTE(x)  ((x).byte = 0)	   //eg p1.byte = 0
#define BGET(x, y)  ((x).bitn.bit ## y)	   //eg p1.bitn.bit1

#define SET_BIT(x,y) ((x) |= (1<<(y)))
#define CLR_BIT(x,y) ((x) &=~(1<<(y)))
#define GET_BIT(x,y) ((x) & (1<<(y)))

#define and     &&
#define and_eq  &=
#define bitand  &
#define bitor   |
#define compl   ~
#define not     !
#define not_eq  !=
#define or      ||
#define or_eq   |=
#define xor     ^
#define xor_eq  ^=

union	bit_char
{
	uchar byte;
	struct bits
	{
	  uchar bit0:1; 
	  uchar bit1:1; 
	  uchar bit2:1; 
	  uchar bit3:1; 
	  uchar bit4:1; 
	  uchar bit5:1; 
	  uchar bit6:1; 
	  uchar bit7:1; 
	}  bitn;
}; 	

union	bit_int
{
	uint word;
	struct 
	{
	   uchar hi; // 1 
	   uchar lo; // 2 
	}byte;		
};
#endif
