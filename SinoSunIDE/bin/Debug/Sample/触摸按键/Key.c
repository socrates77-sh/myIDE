#include  "ADC.h"
/*******************************************************************************
;����Ӧ����
*******************************************************************************/
void KeyAckedPro()
{
	if (!FCtinuKey) BUZInit(1);
	/*
		{
		BUZCnt = 1;
		BUZTime = 0;
		FBUZ = true;
		BUZOnTime = 60;
		}
	*/
	FKeyAck = true;
}
/*******************************************************************************
;�޼����´���
*******************************************************************************/
void NoKeyPressPro()
{
	LongKey = 0;
	KeyCode = 0;
	KeyCtnuCNT = 0;
	FKeyAck = false;
	FCtinuKey = false;
	Menu = 0;
}
/*******************************************************************************
;��
*******************************************************************************/
void KeyPro()
{
	uchar x;
	if (KeyBuf == 0) {NoKeyPressPro(); return;}
	if (KeyBuf != KeyCode)		//;��һ�ΰ���
		{
		KeyCode = KeyBuf;
		KeyCnt = 0;
		//FKeyAck = false;
		//FCtinuKey = false;
		return;
		}
	if ((!FKeyAck)&&(++KeyCnt>5)) KeyDealPro();
	else 
		{
		if (!FCtinuKey) return;
		if (++KeyCtnuCNT > 200) KeyDealPro();
		}
}
/*******************************************************************************
;����֧
*******************************************************************************/
void KeyDealPro()
{
	KeyCtnuCNT = 0;
	Menu =KeyCode;
}
//-------------------------------------------------------------------------------
void BUZInit(uchar times)
{
	
	BUZCnt = times;
	BUZOnTime = 60;
	BUZOffTime = 250;
	FBUZ = true;
 }

