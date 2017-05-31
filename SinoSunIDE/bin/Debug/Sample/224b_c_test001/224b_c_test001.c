
#include <common.h>
#include <reg_mc20p24.h>
#include  "int_mc20p24.h"

#include    "abc.c"

void initial()
{
    SEI(); //disable enterrupt 
    //RSP(); //调用子程序中不能清堆栈指针，否则含数将无法返回。
    P0CONH=0b10101010; //p07--p04 use for output
    P0CONL=0xaa;
    P0=0xff;
    WDTE=0x0a;
    
}

void main(void)
{
    uchar a;
    RSP(); //只能在主程序中清，调用子程序中不能清堆栈指针，否则含数将无法返回。
    initial();
    
    while(1)
    {
        a++;
        funtion001(4,4);
        P0=~P0;
    }  
    
}