
#include "mc30p011.h"
#include "mc3x-common.h"
#include        "globle.h"

//ad		rrr		1112

void main()
{
        char t1;       
        //GPR=2;
     GIE=0;
     ClrWdt();
        DDR0=0Xaa;
        //DDR1=0X00;
        P0=0;
        P1=1;
        T0CNT=0;
        initial();
     while(1)
     {
        P0 =~P0;
        t1++;
        temp2++;
        abc(t1,temp2);    
     }   
        
}