#include  "ADC.h"
//extern uchar WriteDispDataPro();
/******************************************************************************
;��ʾ�����
*******************************************************************************/
const uchar LetterTbl[] = {
							DZero,DOne,DTwo,DThree,DFour,DFive,DSix,DSeven,
							DEigher,DNine,DLetterA,DLetterb,DLetterC,DLetterd,
							DLetterE,DLetterF};
//;-----------------------------------------------------------------------------	
/******************************************************************************
	;��ʾ����---------------------------------------------------------------
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
	;��ʾ����---------------------------------------------------------------
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
	command(0x03);	  //������ʾģʽ��7λ10��ģʽ 
	command(0x40);	  //������������,���õ�ַ�Զ���1ģʽ 
	command(0xc4);	  //������ʾ��ַ����04H��ʼ 
	for(i=0;i<5;i++)	   //������ʾ���� 
		{ 
		send_8bit(DispData[i]);	 //��00H��ż����ַ����ʾ���� 
		send_8bit(0);  //��ΪSEG9-14��δ�õ�������������ַ��ȫ��0�� 
		} 
	command(0x8F);	  //��ʾ�����������ʾ������Ϊ���� 

	pSTB=1; 

	
}



