#include  "EXT.H"

/*******************************************************************************
*******************************************************************************/
void ADCovSbr(uchar x)
{
	ADCON = x;
	ADStart();
	while (!ADOver){}
}
/*******************************************************************************
ADC 转换
*******************************************************************************/
void ADCPro()
{  	
	if ((pPwr)||(!pVIn))
		{
		ADCCnt = 0;
		TempADBuf = 0;
		VADBuf = 0;
		FADOver = 0;
		return;
		}
	
	ADCovSbr(0x16);
	TempADBuf += (ushort)ADTH;

	ADCovSbr(0x36);
	VADBuf += (ushort)ADTH;
	CloseAD();		//;关闭ADC功能，降低功耗
	
	if (++ADCCnt < 8) return;
	ADCCnt = 0;
	TempAD = (uchar) (TempADBuf << 3);
	VAD = (uchar) (VADBuf << 3);
				
	TempADBuf = 0;
	VADBuf = 0;
	if (!FADOver) CellChkSbr();
	FADOver = 1;
	if (TempAD < 5)
		{
		if (++TempOpDly >= 5)
			{
			FERR = 1;
			--TempOpDly;
			}
		return;
		}
	TempOpDly = 0;
	if (TempAD > 0xFA)
		{
		if (++TempShDly >= 5)
			{
			FERR = 1;
			--TempShDly;
			}
		return;
		}
	TempShDly = 0;
	FERR = 0;
	
}

