#ifndef _H_MY_MACROS
#define _H_MY_MACROS

#ifndef __STM8S_TYPE_H
typedef unsigned char   uchar;
typedef unsigned short  ushort;
typedef unsigned long   ulong;

typedef signed char     char;
typedef signed short    short;
typedef signed long     long;
//typedef _Bool           bool;

typedef volatile uchar     vuchar;
typedef volatile ushort    vshort;
typedef volatile ulong     vulong;
#endif

typedef @tiny uchar        x8;
#define __inline        @inline
#define __interrupt     @interrupt
#define __asm           _asm

#define ERROR   (uchar)(-1)
#define FALSE       0
#define TRUE        1

#define false       0
#define true        1

#ifndef NULL
#define NULL        0
#endif

typedef void    (*func_void)(void);
typedef void    (*addr);

//----- ³£ÓÃ²Ù×÷ -----//
#define STR(str)    #str
#undef  BIT
#define BIT(n)      (1<<(n))
#define BIN(bin)    (0b##bin)
#define BYTE_H      (0)
#define BYTE_L      (1)
#define MINI(a,b)   ( (a)<(b) ? (a) : (b) )
#define MAXI(a,b)   ( (a)>(b) ? (a) : (b) )
#define SWAP(a,b)   { register uchar tmp=(a); a=(b); b=tmp; }
//#define SWAP(a,b) {  a^=b^=a^=b; }

#define CODE_BEGIN()    do{
#define CODE_END()      } while(0);
#define CODE_BREAK      break;
#define ATOM_BEGIN()    { Interrupt_ALL_Disabled(); CODE_BEGIN()
#define ATOM_END()      CODE_END() Interrupt_ALL_Enabled(); }
#define ATOM_BREAK      break;
#define ARRAY_SIZE(a)   ( sizeof(a)/sizeof(a[0]) )
#define H4BIT(byte)     ( (uchar)((byte)>>4) )
#define L4BIT(byte)     ( (uchar)((byte)&15) )
#define H8BIT(word)     ( (uchar)((word)>>8) )
#define L8BIT(word)     ( (uchar)((word)) )
#define PTR_BYTE(var)   ( (uchar*)(&var) )
#define GET_BYTE(var)   ( *PTR_BYTE(var) )
#define REG_READ(reg)   ( *(volatile uchar*)(&reg) )
#define TO_BOOL(v)      ( (bool)(v) )
#define GET_VALUE(n)    n
#define SUB_ABS(a,b)    ( (a)>(b)?(a-b):(b-a) )
#define IS_POWER_2N(n)  ( ((n)&(n-1))==0 )

#define POS_ITEM(type,item) ( (uchar)(ushort)(&((type*)0)->item) )
#define BOOL_EQU(dst,src)   { (dst)=false; if(src) (dst)=true; }
#define REG16_EQU(reg,val)  { PTR_BYTE(reg)[BYTE_H]=H8BIT(val); PTR_BYTE(reg)[BYTE_L]=L8BIT(val); }

typedef struct
{
    uchar  bit0    :1;
    uchar  bit1    :1;
    uchar  bit2    :1;
    uchar  bit3    :1;
    uchar  bit4    :1;
    uchar  bit5    :1;
    uchar  bit6    :1;
    uchar  bit7    :1;
}   BITS_T;
#define TO_BITS(var)    (*(BITS_T*)&var)

#endif
