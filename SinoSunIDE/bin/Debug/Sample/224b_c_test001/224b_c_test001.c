
#include <common.h>
#include <reg_mc20p24.h>
#include  "int_mc20p24.h"

#include    "abc.c"

void initial()
{
    SEI(); //disable enterrupt 
    //RSP(); //�����ӳ����в������ջָ�룬���������޷����ء�
    P0CONH=0b10101010; //p07--p04 use for output
    P0CONL=0xaa;
    P0=0xff;
    WDTE=0x0a;
    
}

void main(void)
{
    uchar a;
    RSP(); //ֻ�������������壬�����ӳ����в������ջָ�룬���������޷����ء�
    initial();
    
    while(1)
    {
        a++;
        funtion001(4,4);
        P0=~P0;
    }  
    
}