
#include "mc30p011.h"
#include "mc3x-common.h"
#include        "globle.h"

char temp2;

void initial()
{
   uchar i,j;

        i=20;
        j=T1CNT;
     if(j)  
        DDR0=0;
    else 
        DDR0=0XFF; 
        
}


void  abc(uchar a,uchar b)
{
    uchar i,j,k,l,m;
    
        i=a+2;
        j=a+8;
        k=i+j;
        l=b+a;
        m=k-l;
                
        if(m>3)
        {
          P1=0X80;       
        }
        else
        {
          P1=0X00;
        }
}