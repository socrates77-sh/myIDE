#include <common.h>
#include <reg_mc20p22.h>
//#include  "int_mc20p22.h"
#include "abc.c"

uchar temp[5];
uchar aaa;

void initial()
{
   	uchar i=0;
    SEI(); //disable enterrupt 
    //RSP(); //调用子程序中不能清堆栈指针，否则含数将无法返回。
    //P0CONH=0b10101010; //p07--p04 use for output
    //P0CONL=0xaa;
    DDR0=0xff;
    P0=0xff;
    // WDTE=0x0a;
   	aaa = 0x55;
   	aaa = aaa *2;
   	for (i=0;i<5;i++)
   	{
   	   	temp[i] = aaa+i;
   	}
    for (i=0;i<5;i++)
   	{
   	   	aaa = temp[i];
   	}
}


@nosvf @interrupt void SWI_ISR(void)
   	{
   	  
   	}

@nosvf @interrupt void INT1_ISR(void)
   	{
   	  
   	}

@nosvf @interrupt void INT0_ISR(void)
   	{
   	  
   	}

@nosvf @interrupt void TMI2_ISR(void)
   	{  	
   	     
   	}

@nosvf @interrupt void TMI1_ISR(void)
   	{  	
   	     
   	}

@nosvf @interrupt void TMI0_ISR(void)
{  	
   	int i;
   	T0IF = 0;
   	aaa = 0x45;
    for (i=0;i<5;i++)
    {
       	temp[i] = aaa+i;
    }
   	for (i=0;i<5;i++)
   	{
   	   	aaa = temp[i];
   	}
}


@nosvf @interrupt void KBI_ISR(void)
   	{
   	  
   	}
   	
@nosvf @interrupt void WDTI_ISR(void)
   	{
   	  
   	}


void main(void)
{
    RSP(); //只能在主程序中清，调用子程序中不能清堆栈指针，否则含数将无法返回。
    initial();
   	CLI();
   	T0IM = 0;
    
    while(1)
    {
        P0=~P0;
   	   	aaa = 0;
    }  
    
}

