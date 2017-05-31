

#include  <common.h>
#include  <reg_mc20p24.h>

#include  "var.h"
#include  "int_mc20p24.h"

void  InitRst(void)
{
    BTCON   = 0xa2;
    MCR     = 0x01;
    PWMCON  = 0x08;
    PWMDATA = 0xff;

    P0CONH = 0x00;
    P0CONL = 0x5d;
    P0PND  = 0X00;
    
    P1CON  = 0x0a;

    P2CONH = 0x00;
    P2CONL = 0x00;

    P0  = 0x00;
    P1  = 0x00;
    P2  = 0x00;
    ADCON = 0x10;
}