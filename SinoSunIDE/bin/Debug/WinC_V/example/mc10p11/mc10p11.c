/*****************************
Program:	TST(6122码型，38k，三角键盘，4M晶振)
Author :	Liang HuiFeng
Date   :	2010.12.27
Modify :  
******************************/
//S12-S0 GND
//PA0 蓝灯，DVD 模式
//PA1 红灯，TV 模式

#include  <common.h>
#include  <reg_mc10p11.h>

#include  "int_mc10p11.h"

//****************************
#define TVSysCodeH 0x80
#define TVSysCodeL 0x7e
#define DVDSysCodeH 0x08
#define DVDSysCodeL 0xf7

#define B_LED S0
#define R_LED S11

uchar KeyValue;
uchar OldKey;
uchar Debounce;
uchar Module;//TV module=0,dvd module =1;

BYTE flag;
#define KeyNo  		flag.bitn.bit7
#define KeyErr 		flag.bitn.bit6
#define DoubleKey flag.bitn.bit5

const unsigned char TVCode[]=
{		
	//S0		S1		S2		S3		S4		S5		S6		S7		S8		S9		S10		S11		S12			   
  0X00,																																						//S1   
  0X01, 0X02,	                                                                    //S2   
  0X03, 0X04,	0X05,	                                                              //S3   
  0X06, 0X07,	0X08,	0X09,	                                                        //S4   
  0X0A, 0X0B,	0X0C,	0X0D,	0X0E,	                                                  //S5   
  0X0F, 0X10,	0X11,	0X12,	0X13,	0X14,	                                            //S6   
  0X15, 0X16,	0X17,	0X18,	0X19,	0X1A,	0X1B,	                                      //S7   
  0X1C, 0X1D,	0X1E,	0X1F,	0X20,	0X21,	0X22,	0X23,	                                //S8   
  0X24, 0X25,	0X26,	0X27,	0X28,	0X29,	0X2A,	0X2B,	0X2C,	                          //S9   
  0X2D, 0X2E,	0X2F,	0X30,	0X31,	0X32,	0X33,	0X34,	0X35,	0X36,	                    //S10  
  0X37, 0X38,	0X39,	0X3A,	0X3B,	0X3C,	0X3D,	0X3E,	0X3F,	0X40,	0X41,	              //S11  
  0X42, 0X43,	0X44,	0X45,	0X46,	0X47,	0X48,	0X49,	0X4A,	0X4B,	0X4C,	0X4D,	        //S12  
  0X4E, 0X4F,	0X50,	0X51,	0X52,	0X53,	0X54,	0X55,	0X56,	0X57,	0X58,	0X59,	0X5A    //GND  
};
const unsigned char DVDCode[]=
{
	//S0		S1		S2		S3		S4		S5		S6		S7		S8		S9		S10		S11		S12			   
  0X00,																																						//S1   
  0X01, 0X02,	                                                                    //S2   
  0X03, 0X04,	0X05,	                                                              //S3   
  0X06, 0X07,	0X08,	0X09,	                                                        //S4   
  0X0A, 0X0B,	0X0C,	0X0D,	0X0E,	                                                  //S5   
  0X0F, 0X10,	0X11,	0X12,	0X13,	0X14,	                                            //S6   
  0X15, 0X16,	0X17,	0X18,	0X19,	0X1A,	0X1B,	                                      //S7   
  0X1C, 0X1D,	0X1E,	0X1F,	0X20,	0X21,	0X22,	0X23,	                                //S8   
  0X24, 0X25,	0X26,	0X27,	0X28,	0X29,	0X2A,	0X2B,	0X2C,	                          //S9   
  0X2D, 0X2E,	0X2F,	0X30,	0X31,	0X32,	0X33,	0X34,	0X35,	0X36,	                    //S10  
  0X37, 0X38,	0X39,	0X3A,	0X3B,	0X3C,	0X3D,	0X3E,	0X3F,	0X40,	0X41,	              //S11  
  0X42, 0X43,	0X44,	0X45,	0X46,	0X47,	0X48,	0X49,	0X4A,	0X4B,	0X4C,	0X4D,	        //S12  
  0X4E, 0X4F,	0X50,	0X51,	0X52,	0X53,	0X54,	0X55,	0X56,	0X57,	0X58,	0X59,	0X5A    //GND    
};

//*****************************

void  InitRst(void)//端口初始化
{
    //IOR=0XFF;    
    IOR=0X77;
    flag.byte = 0;
}

void  InitRam(void)//寄存器初始化
{	
	uchar *P @0xe0;
  for(P=(uchar *)0xe1;P<=(uchar *)0xf0;P++)
  *P=0;
  OldKey=0X00;
  Module=0;
}

uchar Index(uchar i)
{
	uchar result=0;
	while(i--) result+=i;
	return result;	
}

void KeyScan(void)//按键扫描子函数
{
    uchar i,j;
    uchar row,col;
		uchar KeyNum;
    WORD KEY;
     
    KeyValue = 0xff;
    KeyNo = 0;   
    KeyErr = 0;        
    KeyNum = 0;			                 //扫描到的按键数	

    KBIF = 0;                        //复位扫描
    KEY.word = 0;
    
    KEY.byte.hi = ~KEYH & 0x1f;
		KEY.byte.lo = ~KEYL;
    
    if(KEY.word)
    {  	 	                   
    	for(j=0;j<13;j++)	           //产生列值
    	{
    		if (KEY.word & (1<<j)) 
    		{
    			KeyNum++;   
    			KeyValue=0x4D+j;
    	  }
    	}
      if(KeyNum=1)                //单键判断：对地按键
     	{  
     			DoubleKey = 0;  	
    			return;
    	}  
    	else
    	{
    		  DoubleKey = 0;
    			KeyErr = 1;
    			return;
    	}    		
    }  		
    	

		for(i=0;i<13;i++)                //产生行值
		{	
			KEYL = 0;	                     //写KEYL寄存器，端口进入扫描状态，从S0开始扫描
			                               //再次写KEYL寄存器此端口回到非扫描状态
			                               //每16次一个循环，清零KBIF将从S0重新开始
			KEY.byte.hi = ~KEYH & 0x1f;
			KEY.byte.lo = ~KEYL;
			
			KEYL = 0;                      //再次写KEYL寄存器，端口回到非扫描状态 
			    	
    	if(KEY.word)
    	{  	 	                   
    		for(j=0;j<13;j++)	           //产生列值
    		{
    			if (KEY.word & (1<<j)) 
    			{
    				KeyNum++;   
    				KeyValue=Index(i)+j;
    		  }
    		}
    	}  		
  	}
  	
  	KEYL = KeyValue; 
  	KEYL = KeyNum;
  	
  	if(KeyNum==0)                               //无按键
		{
			DoubleKey = 0;
			KeyNo = 1;
			return;
		}
  	if(KeyNum==2)                               //单键判断：2普通按键
    {  
    	DoubleKey = 0;  	
    	return;
    }                                     
    else if(KeyNum==4)                          //双键判断：4普通双键，四个管脚组成，无接地按键    	                                          
    {
    	if(DoubleKey==1)
    	{
    		return;    		
    	}
    	else if(OldKey != KeyValue)
    	//else if((OldKey==0x0A) && (KeyValue == 0x10)) 
    	{ 
    		DoubleKey = 1;  			
    		return;
    	}
    	else                                      //先按键键值覆盖后按键键值
    	{
    		KeyErr = 1;
    		return;
    	}    		
    } 
    else
    {
    	DoubleKey = 0;
    	KeyErr = 1;
    	return;
    }    
}

void Unit38K1(uchar Unit)
{
    uchar j;
    for(j=0; j<Unit; j++)
    {    	  
        OUTC=0;//5
        OUTC=0;//5
        OUTC=0;//5        
        NOP();//2        
        OUTC=1;//5
        OUTC=1;//5
        OUTC=1;//5
        OUTC=1;//5
        NOP();//2
    }
}

void Unit38K0(uchar Unit)
{
    uchar j;
    for(j=0; j<Unit; j++)
    {
        OUTC=1;//5
        OUTC=1;//5
        OUTC=1;//5
        NOP();//2        
        OUTC=1;//5
        OUTC=1;//5
        OUTC=1;//5
        OUTC=1;//5
        NOP();//2
    }
}

void IrOutHeadH(void)//9ms
{
    //发射9ms
    Unit38K1(171);
    Unit38K1(171);
}

void IrOutHeadL(void)//4.5ms
{
    Unit38K0(171);
}

void SendBit(uchar Data)
{
    Unit38K1(21);
    Unit38K0(21);
    if(Data==1) Unit38K0(42);
}

void IrOut(uchar CodeData)
{
    uchar i;
    for(i=0; i<8; i++)
    {
        if(CodeData&0x01) SendBit(1);
        else SendBit(0);
        CodeData=CodeData>>1;
    }
}

void LED(uchar ledcont)//0,R_LED,1,B_led,其他，关闭LED
{
		switch(ledcont)
	{
		case 0: B_LED=0;
			      R_LED=1;
			      break;
		case 1: R_LED=0;
			      B_LED=1;
			      break;			      
		default:
			     R_LED=0;
			     B_LED=0;
			     break;
		}	
}

void  main(void)
{
    SEI();
    InitRst();
    InitRam();
    CLI();
    while(1)
    {
        KeyScan();
				Debounce++;
        if(KeyNo)  //stop
        {             
            KBIF = 0;
            KBIE=1;
            NOP();
            NOP();
            STOP();
            NOP();
            NOP();
            KBIE=0;
            Debounce = 0;
            OldKey=0xff; 
            KeyValue=0xff; 
        }
        else if(KeyErr) //err
        {   
        	  Debounce = 0;         
            OldKey=0xff;
        }
        else if((Debounce>30)&&(DoubleKey==1)&&(KeyValue!=OldKey))
        {        	 
        	OldKey=KeyValue;         	
        	SEI();                        
          LED(Module);
          IrOutHeadH();//9ms
          IrOutHeadL();//4.5ms
            IrOut(0);
            IrOut(0);
            IrOut(DVDCode[KeyValue]);
            IrOut(~DVDCode[KeyValue]);
          NOP();
          NOP();
          NOP();
          NOP();
          NOP();
          NOP();
          NOP();
          NOP();
          NOP();
          NOP();
          NOP();
          SendBit(0);
          LED(2);
          Debounce=65;
          CLI();
        }
        else if((Debounce>20)&&(DoubleKey==0)&&(KeyValue!=OldKey))
        {            
        	  OldKey=KeyValue;           	                    
            if(Module==0)
            {
            SEI();
            if(KeyValue==0X8E) Module=1;            
            LED(Module);
            IrOutHeadH();//9ms
            IrOutHeadL();//4.5ms
            IrOut(TVSysCodeH);
            IrOut(TVSysCodeL);
            IrOut(TVCode[KeyValue]);
            IrOut(~TVCode[KeyValue]);
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            SendBit(0);
            LED(2);
            Debounce=65;
            CLI();
            }
            else if(Module==1)
            {
            SEI();
            if(KeyValue==0X8E) Module=0;
            LED(Module);
            IrOutHeadH();//9ms
            IrOutHeadL();//4.5ms
            IrOut(DVDSysCodeH);
            IrOut(DVDSysCodeL);
            IrOut(DVDCode[KeyValue]);
            IrOut(~DVDCode[KeyValue]);
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            NOP();
            SendBit(0);
            LED(2);
            Debounce=65;
            CLI();
            }            
        }
        else if((Debounce>108)&&(KeyValue==OldKey))
        {           	   	  

            SEI();
            
            LED(Module);
            
            IrOutHeadH();//9ms
            Unit38K0(85);//2.25MS
            SendBit(0);
            
            LED(2);

            Debounce=10;
            CLI();
            
        }

    }
   
}
