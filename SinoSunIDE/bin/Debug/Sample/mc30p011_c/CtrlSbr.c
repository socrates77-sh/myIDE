#include <mc30p011.h>
#include "externVar.H"
#include "CONST.H"
//;----------------------------------------------------------------	
//;变色模式
void MColorSbr()
{
   	if (++delay >= 4) 
   	{
   	   	delay = 0;
   	   	if (--ModeCnt == 0)
   	   	{
   	   	   	TGreen = TColor;
   	   	   	TRed = TColor;
   	   	   	TBlue = TColor;
   	   	   	/*
   	   	   	TGreen = TColor;
   	   	   	TRed = TColor;
   	   	   	TBlue = TColor;
   	   	   	*/
   	   	   	if (++Step > 5) Step = 0;
   	   	   	ModeCnt = TSteplengh;
   	   	}
   	   	if (Step == 0) {++TGreen;--TBlue;return;}
   	   	if (Step == 1) {--TGreen;++TBlue;return;}
   	   	if (Step == 2) {++TRed;--TBlue;return;}
   	   	if (Step == 3) {--TRed;++TBlue;return;}
   	   	if (Step == 4) {++TRed;--TGreen;return;}
   	   	if (Step == 5) {--TRed;++TGreen;return;}
/*
   	   	switch(Step)
   	   	   	{
   	   	   	case 0:
   	   	   	   	++TGreen;
   	   	   	   	--TBlue;
   	   	   	   	break;
   	   	   	case 1:
   	   	   	   	++TBlue;
   	   	   	   	--TGreen;
   	   	   	   	break;
   	   	   	case 2:
   	   	   	   	++TRed;
   	   	   	   	--TBlue;
   	   	   	   	break;
   	   	   	case 3:
   	   	   	   	--TRed;
   	   	   	   	++TBlue;
   	   	   	   	break;
   	   	   	case 4:
   	   	   	   	++TRed;
   	   	   	   	--TGreen;
   	   	   	   	break;
   	   	   	default:
   	   	   	   	--TRed;
   	   	   	   	++TGreen;
   	   	   	   	break;
   	   	   	}
*/
   	}
}

//;----------------------------------------------------------------	
//;烛光模式
void Mode2Sbr()
{
   	if (ModeCnt >= Step)
   	{
   	   	pBlueC = 1;
   	   	pRedC = 1;
   	   	pGreenC = 1;
   	   	if (ModeCnt > 50) ModeCnt = 0;
   	}
   	else
   	{
   	   	pBlueC = 0;
   	   	pRedC = 0;
   	   	pGreenC = 0;
   	}
}  	

