
#include  "main.h"
#include  "const.h"


extern uchar RdKey(void);
void main()
{	
	uchar i;
	
	DI();
	InitSP();
	WDT_DIS();
	InitialSys();
	InitalRAM();
	
/*
	for (Buf1 = 0; Buf1 <100; ++Buf1)
		{
		if (RdKey() == CKWarm) ++Buf2;
		else break;
		}
	if (Buf2 >80)
		{
		SelfStep = 0;
  		FKeyAck = 1;
		}
*/
	for (Buf1=0;Buf1<5;++Buf1) DispData[Buf1] = 0;
	DispData[2] = 0x10;
	for (Buf1=0;Buf1<5;++Buf1) 
		{
		
		command(0x03);	  //������ʾģʽ��7λ10��ģʽ 
		command(0x40);	  //������������,���õ�ַ�Զ���1ģʽ 
		command(0xc4);	  //������ʾ��ַ����04H��ʼ 
		for(Buf2=0;Buf2<2;Buf2++)	   //������ʾ���� 
			{ 
			send_8bit(DispData[Buf2]); 	 //��00H��ż����ַ����ʾ���� 
			send_8bit(0);  //��ΪSEG9-14��δ�õ�������������ַ��ȫ��0�� 
			} 
		command(0x8F);	  //��ʾ�����������ʾ������Ϊ���� 
		//read_key(); 	  //������ֵ 
		pSTB=1; 
		}
	
	EI();
	BUZInit(1);
	for(;;)
		{
		if(F2ms)	//;2ms����
			{
			F2ms = false;
			WDT_CLR();
			BUZPro();
			if (Deley2s >= 1) ADCPro();
			KeyPro();
			DispPro();
			if (F500ms) 
				{
				F500ms = false;
				T500ms = 0;
				if (++Deley2s >= 1) Deley2s = 1;
				T500msPro();
				}
			}
		}
}
__interrupt void IRQ_T0INT(void)
{
	T0_CLRF();
	//if (FBUZ) pBUZ = !pBUZ;
	pBUZ = !pBUZ;
	if (++T2ms >= 16)
		{
		T2ms = 0;
		F2ms = true;
	
		if (++T500ms >= 250)
			{
			T500ms = 0;
			F500ms = true;
			}
		}
}

__interrupt void IRQ_NonHandled(void)
{
    return;
}




