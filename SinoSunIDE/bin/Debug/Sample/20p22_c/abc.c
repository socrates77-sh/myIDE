#include <common.h>
#include <reg_mc20p22.h>

  void funtion001(unsigned char aa,unsigned char bb)
  {
    //unsigned char  a=0;
    //unsigned char  b=1;
    if(aa==bb)
    {
        P0=0xff;
        //NOP();
    }   
    else 
        P0=0x00;
  }