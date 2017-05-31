/******************************************************************
;6122码遥控器解码

;******************************************************************/
#include <mc30p011.h>
#include  "VAR.H"
#include  "CONST.H"
extern void InitialSys();
extern void InitalRAM();
extern void Mode2Sbr();
extern void MColorSbr();
extern void T1sSbr();
uchar time_125us;
uchar time_10ms;
uchar time_1s;
uchar display_1;
uchar display_1_data;
int i;
const unsigned char CoilTab[19]={0x3F,0x06,0x5B,
0x4F,0x66,0x6D,0x7D,0x07,0x7F,0x6F,0x77,0x7C,0x39,
0x5E,0x79,0x71,0x73
};

void main ()
{
    uchar aaa;
    GIE = 0;    
    ClrWdt();
    InitialSys();
    InitalRAM();
    FMultiColor = 1;
    FOn = 1;
    pIR = 1;
    KBIE = 1;
    KBIM3 = 1;
    GIE = 1;
    for(i=0;i<19;i++)
    {
        display_1_data = i;
        display_1      = CoilTab[display_1_data];
    }
    while(1)
    {
        if (FStop)
        {
            T1IF = 0;
            T0IF = 0;
        }
        if ((delay05ms != 0)||(!FOn))
        {
            pRedC = 1;
            pGreenC = 1;
            pBlueC = 1;
        }
        else 
        {
            switch(FMultiColor)
            {
              case 1:
                pRedC = 0;
                pGreenC = 0;
                pBlueC = 0;
                break;
              case 0:
                {
                    if (FCandle) Mode2Sbr();
                }
                break;
            }
        }
        if (F4ms)
        {
            F4ms = 0;
            ClrWdt();
            if (delay05ms == 0)
            {
                if (FOn)
                {
                    if (FMultiColor) MColorSbr();
                    else
                    {
                        if (FCandle)
                        {
                            //;烛光模式---------------------------------------
                            if (++T40ms > 10)
                            {
                                T40ms = 0;
                                if (FDirection)
                                {
                                    if (++Step > 50) FDirection = 0;
                                }
                                else
                                {
                                    if (--Step < 10) FDirection = 1;
                                }                                                               
                            }
                        }
                    }
                }
            }
            else --delay05ms;
            T1sSbr();
        }
    }
}

