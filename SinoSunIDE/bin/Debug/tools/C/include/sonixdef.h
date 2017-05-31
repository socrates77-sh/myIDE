#ifndef _SONIXDEF_H_
#define _SONIXDEF_H_

unsigned long GetRollingCode(unsigned int offset);

#define _DELAY(X)  __asm{_Delay X}
#define ROMWRT  __asm{ROMWRT}

#define DefineRomTable(name,addr,size,value) __asm{ __DefineRomTable name addr size value}

#ifdef _DEFINE_INTRINSIC_

struct _intrinsicbitfield
{
	unsigned bit0:1;
	unsigned bit1:1;
	unsigned bit2:1;
	unsigned bit3:1;
	unsigned bit4:1;
	unsigned bit5:1;
	unsigned bit6:1;
	unsigned bit7:1;
};

#define _getROM(addr)  		*((__ROM unsigned long*)(addr))
#define _getRAM(addr)  		*((__RAM unsigned *)(addr))
#define _setRAM(addr,value) 	*((__RAM unsigned *)(addr)) = value

#define _bSET(addr,nbit) (((*((__RAM struct _intrinsicbitfield *)addr)).bit##nbit) = 1)
#define _bCLR(addr,nbit) (((*((__RAM struct _intrinsicbitfield *)addr)).bit##nbit) = 0)

#define _bTest0(addr,nbit)	(((*((__RAM struct _intrinsicbitfield *)addr)).bit##nbit) == 0)
#define _bTest1(addr,nbit)	(((*((__RAM struct _intrinsicbitfield *)addr)).bit##nbit) != 0)

#endif
#endif