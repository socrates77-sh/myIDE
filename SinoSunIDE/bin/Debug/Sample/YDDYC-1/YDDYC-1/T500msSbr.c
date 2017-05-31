#include  "EXT.h"
void T100msSbr()
{
	if (++T50ms < 125) return;
	T50ms = 0;
	FFlash50 = !FFlash50;
	if (!FFlash50) return;

	FFlash100 = !FFlash100;
	
	if (++CellDly >= 4)
		{
		-- CellDly;
		FVIn = 1;
		}
	if (!FADOver) return;
	
	if (FlashCnt != 0) --FlashCnt;

	if (++T500ms < 5) return;
	T500ms = 0;
	CellChkSbr();
	TemChkSbr();
	OutCtrlSbr();
}
/*;-------------------------------------------------------------------
;µç³ØµçÑ¹¼ì²â
;-------------------------------------------------------------------*/
void CellChkSbr()
{
	if (pVIn)
	//;³äµç×´Ì¬---------------------------------------------------
		{
		if (CellV > 1) return;
		if (VAD >= CV41) 
			{
			CellV = 2;
			return;
			}
		if (VAD >= CV38) CellV = 1;
		return;
		}
	//;·Åµç×´Ì¬---------------------------------------------------
	CellV = 0;
	if (!F38V)
		{
		if (VAD < CV38)
			{
			if (++V38CNT >= 10)
				{
				--V38CNT;
				F38V = 1;
				FlashCnt = 40;
				}
			return;
			}
		F32V = 0;
		return;
		}
	if (F32V) return;
	if (VAD < CV32)
		{
		if (++V32CNT > 10)
			{
			--V32CNT;
			F32V = 1;
			FlashCnt = 40;
			}
		return;
		}
	V32CNT = 0;
}
/*;-------------------------------------------------------------------
;ÎÂ¶È¼ì²â
;-------------------------------------------------------------------*/
void TemChkSbr()
{
	uchar bufL,bufH;
	
	if (Stauts == 0) return;
	bufL = CT38;
	bufH = CT41;
	if (Stauts != 1)
		{
		bufL = CT42;
		bufH = CT45;
		}
	if (TempAD > bufH) FTempOver = 1;
	if (TempAD < bufL) FTempOver = 0;
}
/*;-------------------------------------------------------------------
;Êä³ö¿ØÖÆ
;-------------------------------------------------------------------*/
void OutCtrlSbr()
{
	if (pVIn)
		{
		//	;-----------------------------------------------------------
		//	;³äµçÊäÈë×´Ì¬
		F32V = 0;
		F38V = 0;
		V32CNT = 0;
		V38CNT = 0;
		Stauts = 0;
		CellLow = 0;
		pHeat = 0;
		pCharge = 0;
		pPwr = 0;
		return;
		}
	switch (Stauts)
		{
		case 0:
			//;-----------------------------------------------------------
			//;´ý»ú×´Ì¬
			pCharge = 0;
			pHeat = 0;
			pPwr = 0;
			break;
		case 1:
		case 2:
			//;-----------------------------------------------------------
			//;¼ÓÈÈ×´Ì¬
			pCharge = 0;
			if ((FTempOver)||(FERR)) pHeat = 0;
			else pHeat = 1;
			break;
		default:
			//;-----------------------------------------------------------
			//;³äµçÊä³ö×´Ì¬
			pCharge = 1;
			pHeat = 0;
		}
			
}

