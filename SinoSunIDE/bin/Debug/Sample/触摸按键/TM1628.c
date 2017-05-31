#include  "ADC.h"

//向TM1628发送8位数据,从低位开始------------------------ 
void send_8bit(uchar dat) 
{ 
	uchar i; 
	
	for(i=0;i<8;i++) 
		{ 
		if(dat&0x01) pDIO=1; 
		else pDIO=0; 
		pCLK=0; 
		pCLK=1; 
		dat=dat>>1; 
		}
} 
 
//向TM1628发送命令-------------------------------------- 
void command(uchar com) 
{ 
	P2CONL |= 0x80;
	pSTB=1; 
	pSTB=0; 
	send_8bit(com); 
} 
 
//读取按键值并存入KEY[]数组，从低字节开始，从低位开始---- 
void read_key() 
{ 
	uchar i,j,key[5];
	command(0x42);  //读键盘命令 
	pDIO = 1;      //将DIO置高 
	P2CONL &= 0x3F;
	for(j = 0;j < 5;j++)//连续读取5个字节 
		{
		for(i = 0;i < 8;i++)
			{
			key[j] = key[j]>>1; 
			pCLK = 0; 
			pCLK = 1; 
			if(pDIO) key[j] = key[j]|0X80; 
			} 
		pSTB=1; 
		} 
}
		 

