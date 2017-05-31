/*****************************
P12脚用作I,IRC 4MHz
步进电机驱动

P12  I	UpKey
P13  I	DownKey

P10  O	ACoil
P11  O	BCoil
P14  O	CCoil
P15  O	DCoil
******************************/
#include  <common.h>
#include  <reg_mc20p01.h>

#define UpKey P12
#define DownKey P13

uchar KeyPressFlag;
uchar KeyValidFlag;
uchar KeyCounter;
volatile uchar MsCounter;

//uchar Module;

#include  "int_mc20p01.h"

const unsigned char CoilTab[10]={0X03,0X12,0X30,0X21,0X00,0X03,0X21,0X30,0X12,0X00};


void  InitRst(void)
{

    DDR0 		=0XFF;
    P0HCON 	=0X00;

    DDR1 		=0XF3; //11110011
    P1HCON 	=0X0C;
    P1LCON  =0X00;
    P1DCON  =0X00;
    
    KBIM 		=0X00;
    
    
    T0LOAD = 250;//t0 1ms 中断 4M,8分频
    T0CR   =0x10;//00 010 000
//    
//    
//    DDR2 		=0XFF;
//
//    TDR 		=0;
//    TCR 		=0X40;
//    P0PND 	=0;
    MCR 		=0;
//    RSTFR 	=0;
    P0 			=0xff;
    P1 			=0;
}

void  main(void)
{
    uchar i,j,k;
    SEI();
    InitRst();
    
    
    CLI();

    j=0;
    
    _asm("FDB $FFFF");
    _asm("FDB $FFFF");
    _asm("FDB $FFFF");
    
    //_asm("mul");
    
    while(1)
    {
      WDTC=1;//喂狗
      MsCounter=0;
      
    	//keyscan begain
     if(UpKey!=DownKey)
     	{ 
     		KeyPressFlag=1;
     		if(KeyCounter<250)
     		KeyCounter++;
     		
     		if(UpKey) j=5;
     			else j=0;
     		}
     else
     	{
     		if(UpKey&&KeyPressFlag&&KeyCounter>50) KeyValidFlag=1;	
     			
     		else KeyValidFlag=0;
     		
     		KeyPressFlag=0;
     		KeyCounter=0;
     		
     		}
      
      //keydo begain
    	if(KeyValidFlag==1)
    		{
       KeyValidFlag=0;
    	for(k=0;k<10;k++)
    	{
    	for(i=0;i<5;i++)
    		{P1=CoilTab[i+j]; 	//j=0  upkey,j=5 downkey
         MsCounter=0;
         while(MsCounter<50) WDTC=1;//喂狗;//50ms
       }   	
    	}
    	}
    	
    	//在不处理电机时一圈1ms
    	while(MsCounter<1) ;
    	
    	}

}



