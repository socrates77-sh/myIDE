#include  "ADC.h"
/*******************************************************************************
;键响应后处理
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
;无键按下处理
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
;键
*******************************************************************************/
void KeyPro()
{
	uchar x;
	if (KeyBuf == 0) {NoKeyPressPro(); return;}
	if (KeyBuf != KeyCode)		//;第一次按下
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
;键分支
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

