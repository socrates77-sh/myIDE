
#include "mc31p11.h"
//#include "mc3x-common.h"



void main()
{
    //unsigned char i,k;
   
     GIE=0;
     IOP0=0XFF;
     IOP1=0XFF;
     OEP0=0XFF;
     OEP1=0XFF;
     OEP01 = 1;
     
     
     while(1)
     {        
       IOP1=~IOP1;       
       OEP0=0;
       //kskks=0;
       //bkde=0;     
     }
        
        
}