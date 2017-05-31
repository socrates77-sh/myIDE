
#include "reg_mc20p02.h"
#include "int_mc20p02.h"

void main()
{
    unsigned char read_buff[8];
    char i;
    i=0;
    while(1)
    {
        read_buff[i] = i;
     i=~i;
    }   
    
}
