#include  "ADC.h"
//;500ms------------------------------------------------------------------
void T500msPro()
{
	uchar i; 
	
	FFlash = !FFlash;

	if (++T2s >= 2)
		{
		T2s = 0;
		if (++T2sCnt > 4) T2sCnt = 1;
		}
	if (FFlash) 
		{
		if (++TSec >= 60) 
			{
			TSec = 0;
			++TMin;
			}
		}	
}

