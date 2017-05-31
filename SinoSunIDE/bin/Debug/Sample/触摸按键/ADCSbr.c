#include  "ADC.h"

#define ADCH0	0
#define ADCH1	1
#define ADCH2	2
#define ADCH3	3
#define ADCH4	4
#define ADCH5	5
#define ADCH6	6
#define ADCH7	7
#define ADCH8	8

void GetKeyPro(uchar x,uchar y);
/*******************************************************************************
蜂鸣器  in: 鸣叫声数；BUZCNT 鸣叫次数，BUZF 启动标志,
*******************************************************************************/
void BUZPro()
{
	if (BUZCnt == 0) 
		{
		//pBUZ = 0;
		FBUZ = 0;
		return;
		}

	if (FBUZ)
		{
		if (++BUZTime >= BUZOnTime)
			{
			//pBUZ = 0;
			BUZTime = 0;
			FBUZ = false;
			--BUZCnt;
			}
		}
	else
		{
		if (++BUZTime >= BUZOffTime)
			{
			BUZTime = 0;
			FBUZ = true;
			}
		}
}
/*******************************************************************************
ADC 转换
*******************************************************************************/
void ADCPro()
{  	
	uchar addifference[4],sn[4];
	uchar i,j,k,l;
/*
	for (Buf1 = 0; Buf1 < 4;++Buf1)
		{
		switch (Buf1)
			{
			case 0:
				ADChanel(4);
				break;
			case 1:
				ADChanel(3);
				break;
			case 2:
				ADChanel(6);
				break;
			default:
				ADChanel(7);
				break;
			}
		//ADPrescaler()
		ADStart();
		while(!ADOver){}
		if (!FADOver) 
			{
			ADCBuf[Buf1] = ADDATAH;
			}
		else
			{
			#asm
				LDX		_Buf1
				LDA		_ADDATAH
				ADD		_ADCBuf,X
				RORA
				STA		_ADCBuf,X
			#endasm
			}
		//ADCBuf[i] += ADDATAH;
		}
	
	FADOver = 1;
	if (++ADCCNT >= 8)
		{
		ADCCNT--;
		for (i = 0; i < 4;++i)
			{
			if (ADCBuf[i] > ADCValue[i])
				{
				ADCValue[i] = ADCBuf[i];	//ADCValue 存放最大值
				addifference[i] = 0;	//addifference 存放差值
				}
			else 
				{
				addifference[i] = ADCValue[i]-ADCBuf[i];
				//ADCValue[i] = ADCBuf[i];	//ADCValue 存放最大值
				}
			//ADCBuf[i] = 0;
			}
	
		*/

	for (i = 0; i < 4;++i)
		{
		switch (i)
			{
			case 0:
				ADChanel(4);
				break;
			case 1:
				ADChanel(3);
				break;
			case 2:
				ADChanel(6);
				break;
			default:
				ADChanel(7);
				break;
			}
		//ADPrescaler(0);
		ADStart();
		
		while(!ADOver){}
		ADCBuf1[i] += ADDATAH;
		}
		
	if (++ADCCNT >= 8)
		{
		ADCCNT = 0;
		for (i = 0; i < 4;++i)
			{
			ADCBuf[i] = ADCBuf1[i] >> 3;
			ADCBuf1[i] = 0;
			if (ADCBuf[i] > ADCValue[i])
				{
				ADCValue[i] = ADCBuf[i];	//ADCValue 存放最大值
				addifference[i] = 0;	//addifference 存放差值
				}
			else 
				{
				addifference[i] = ADCValue[i]-ADCBuf[i];
				ADDef[i] = addifference[i];
				}
			}
		FADOver = 1;
		//差值修正
		if (addifference[0] > 1) --addifference[0];
		ADDef[0] = addifference[0];
		if (addifference[1] > 3) addifference[1] -= 3;
		ADDef[1] = addifference[1];
		if (addifference[2] > 4) addifference[2] -= 4;
		ADDef[2] = addifference[2];
		ADDef[3] = addifference[3];
		
		/*查找差值最大的按键AD值*/
		for (i = 0; i < 4; ++i) sn[i] = i;
		for(i=0;i<3;i++)
			{
			l=0;
			for(j=0;j<3;j++)
				{
				if(addifference[j+1]<addifference[j])
					{
					k=addifference[j];
					addifference[j]=addifference[j+1];
					addifference[j+1]=k;
					k=sn[j];
					sn[j]=sn[j+1];
					sn[j+1]=k;
					
					l=1;
					}
				}
			if(l==0) break;
			}
		
		KeyBuf = 0;

		if ((addifference[3] > 8)&&(addifference[2] < 8))KeyBuf =sn[3]+1; 
		}

}
	
