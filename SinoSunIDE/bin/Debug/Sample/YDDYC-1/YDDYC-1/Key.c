#include  "EXT.h"
//;-------------------------------------------------------------------
void KeySbr()
{
	if (pVIn) return;	//;	;充电状态不响应按键
	if (!pKey)
		{
		FKeyAck = 0;
		KeyCnt = 0;
		KeyLong = 0;
		return;
		}
	if (FKeyAck) return;
	if (++KeyCnt < 5) return;
	KeyCnt = 0;
	++KeyLong;
	if ((Stauts == 0)&&(KeyLong < 150)) return;
	FKeyAck = 1;
	if (++Stauts >= 4) Stauts = 0;
	pPwr = 1;
	T50ms = 0;
	FFlash50 = 1;
}

