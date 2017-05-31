#include  "ADC.h"

//��TM1628����8λ����,�ӵ�λ��ʼ------------------------ 
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
 
//��TM1628��������-------------------------------------- 
void command(uchar com) 
{ 
	P2CONL |= 0x80;
	pSTB=1; 
	pSTB=0; 
	send_8bit(com); 
} 
 
//��ȡ����ֵ������KEY[]���飬�ӵ��ֽڿ�ʼ���ӵ�λ��ʼ---- 
void read_key() 
{ 
	uchar i,j,key[5];
	command(0x42);  //���������� 
	pDIO = 1;      //��DIO�ø� 
	P2CONL &= 0x3F;
	for(j = 0;j < 5;j++)//������ȡ5���ֽ� 
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
		 

