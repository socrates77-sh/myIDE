/*******************************************************************
;移动电源
;MC20P22-SOP14
*******************************************************************/
#include  "main.H"
#include  "int_MC20P22.H"
#include  "CONST.H"
#include  "ADCSbr.C"
#include  "DisplaySbr.C"
#include  "Key.C"
#include  "SysInit.C"

void main()
{	
	uchar i;
	
	//DI();
	InitSP();
	SysInitSbr();
	SysInitRamSbr();
	EI();
	for(;;)
		{
		if (pVIn) CellLow = 0;
		if (CellLow == 0x55) 
			{
			CloseAD();
			DIKey();
			ClrKeyF();
			LEDAllOff();
			pHeat = 0;
			pCharge = 0;
			NOP();
			NOP();
			NOP();
			Wait();
			NOP();
			NOP();
			NOP();
			}
		
		if(F2ms)	//;2ms程序
			{
			F2ms = false;
			ClrWDT();
			if (CellLow != 0x55)
				{
				ADCPro();
				KeySbr();
				LEDAllOff();
				if (FADOver)
					{
					DisplaySbr();
					T100msSbr();
					}
				}
			}
		}
}
__interrupt void IRQ_T0(void)
{
	T0_CLRF();
	F2ms = 0;
}
__interrupt void IRQ_NonHandled(void)
{
	return;
}


