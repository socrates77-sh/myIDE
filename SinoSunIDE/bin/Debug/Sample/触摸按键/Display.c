#include  "ADC.h"
//extern uchar WriteDispDataPro();
/******************************************************************************
;显示译码表
*******************************************************************************/
const uchar LetterTbl[] = {
							DZero,DOne,DTwo,DThree,DFour,DFive,DSix,DSeven,
							DEigher,DNine,DLetterA,DLetterb,DLetterC,DLetterd,
							DLetterE,DLetterF};
//;-----------------------------------------------------------------------------	
/******************************************************************************
	;显示程序---------------------------------------------------------------
*******************************************************************************/
void Hex2DecPro(uchar hex)
{
	uchar i;
	Buf2 = 0;
	while (hex >=10)
		{
		++Buf2;
		hex -= 10;
		}
	Buf1 = hex;
}
/******************************************************************************
	;显示程序---------------------------------------------------------------
*******************************************************************************/
void DisConvpPro()
{
	uchar i;
	
	for(i = 0; i < 5; ++i) DispData[i] = 0;
	if (Menu)
		{
		DispData[1] = LetterTbl[Menu];
		DispData[2] = LetterTbl[T2sCnt];
		DispData[3] = LetterTbl[(ADDef[T2sCnt-1]&0xF0)>>4];
		DispData[4] = LetterTbl[ADDef[T2sCnt-1]&0x0F];
		}
	else
		{
		DispData[1] = LetterTbl[T2sCnt];
		DispData[3] = LetterTbl[(ADDef[T2sCnt-1]&0xF0)>>4];
		DispData[4] = LetterTbl[ADDef[T2sCnt-1]&0x0F];
		/*
		Hex2DecPro(TMin);
		DispData[1] = LetterTbl[Buf2];
		DispData[2] = LetterTbl[Buf1];
		Hex2DecPro(TSec);
		DispData[3] = LetterTbl[Buf2];
		DispData[4] = LetterTbl[Buf1];
		*/
		if (FFlash) DispData[2] |= 0x10;
		}
		
}		
/******************************************************************************
*******************************************************************************/

void DispPro()
{
	uchar i; 

	DisConvpPro();
	command(0x03);	  //设置显示模式，7位10段模式 
	command(0x40);	  //设置数据命令,采用地址自动加1模式 
	command(0xc4);	  //设置显示地址，从04H开始 
	for(i=0;i<5;i++)	   //发送显示数据 
		{ 
		send_8bit(DispData[i]);	 //从00H起，偶数地址送显示数据 
		send_8bit(0);  //因为SEG9-14均未用到，所以奇数地址送全“0” 
		} 
	command(0x8F);	  //显示控制命令，打开显示并设置为最亮 

	pSTB=1; 

	
}



