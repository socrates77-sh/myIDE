#include <common.h>
#include <reg_mc20p24.h>
//#include <reg_mc20p38.h>
#include  "int_mc20p24.h"
#include "abc.c"


void initial()
{
    SEI(); //disable enterrupt 
    RSP(); //�����ӳ����в������ջָ�룬���������޷����ء�
    //OPAOF=0x5A;
    
    temp[0] = 0x55;
}

void main(void)
{
    uchar a;
    RSP(); //ֻ�������������壬�����ӳ����в������ջָ�룬���������޷����ء�
    initial();
   	a = 5;
   	a = a*5;
    while(1)
    {
        a++;
        funtion001(4,4);
    }  
    
}

