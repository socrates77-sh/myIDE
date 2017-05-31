
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
		
		command(0x03);	  //设置显示模式，7位10段模式 
		command(0x40);	  //设置数据命令,采用地址自动加1模式 
		command(0xc4);	  //设置显示地址，从04H开始 
		for(Buf2=0;Buf2<2;Buf2++)	   //发送显示数据 
			{ 
			send_8bit(DispData[Buf2]); 	 //从00H起，偶数地址送显示数据 
			send_8bit(0);  //因为SEG9-14均未用到，所以奇数地址送全“0” 
			} 
		command(0x8F);	  //显示控制命令，打开显示并设置为最亮 
		//read_key(); 	  //读按键值 
		pSTB=1; 
		}
	
	EI();
	BUZInit(1);
	for(;;)
		{
		if(F2ms)	//;2ms程序
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




