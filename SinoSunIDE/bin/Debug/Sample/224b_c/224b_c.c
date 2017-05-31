#include <common.h>
#include <reg_mc20p24.h>
//#include <reg_mc20p38.h>
#include  "int_mc20p24.h"
#include "abc.c"


void initial()
{
    SEI(); //disable enterrupt 
    RSP(); //调用子程序中不能清堆栈指针，否则含数将无法返回。
    //OPAOF=0x5A;
    
    temp[0] = 0x55;
}

void main(void)
{
    uchar a;
    RSP(); //只能在主程序中清，调用子程序中不能清堆栈指针，否则含数将无法返回。
    initial();
   	a = 5;
   	a = a*5;
    while(1)
    {
        a++;
        funtion001(4,4);
    }  
    
}

