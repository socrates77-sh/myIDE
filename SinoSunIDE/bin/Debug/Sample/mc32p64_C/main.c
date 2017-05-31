//FCPU>=4 系统时钟=16M/FCPU
#include "mc32p64.h"


uchar flag1,flag2,flag3,flag4;
uchar data[50];
uchar sdi_byte,count_bit,count_byte;
uchar need_DealData,need_test,checksum;
uchar i,j,temp1,temp2;
uchar temp;
uchar MCU_ID;
uchar OS_result,need_answer;
uchar sdo_byte;

#define SCK_0 IOP2&=0xfb
#define SCK_1 IOP2|=0x04

#define SDI_0 IOP2&=0xf7
#define SDI_1 IOP2|=0x08

#define Mark_0x02  	IOP2&=0xef //32p64芯片编号为0x02//测试用代码
#define Mark_0x01 IOP2|=0x10 //32p64芯片编号为0x01//测试用代码

#define MarkNext_0x02  	IOP0&=0xbf //将下一级32p64芯片编号为0x02
#define MarkNext_0x01 IOP0|=0x40 //将下一级32p64芯片编号为0x01

#define SDO_0 IOP2&=0xfd
#define SDO_1 IOP2|=0x02

void Send_byte_P23(uchar byte_data);
void Send_byte(uchar byte_data);
void Delay_1ms(uchar num);

void int_isr(void)__interrupt
{
 //	__asm 
 // fill 0x00,0x0
 //	__endasm;
   	if(T0IF==1)
   	{
   	   	T0IF=0;
   	   	IOP2^=0x02;
   	}
   	else if(INT0IF==1)
  {
   	   	INT0IF=0;
   	   	if(need_answer==1)
   	   	{//接收到OS测试命令后 需要回复结果
   	   	   	if(count_bit==0)
   	   	   	{
   	   	   	   	sdo_byte=data[count_byte++];
   	   	   	}
   	   	   	if(count_bit<8)
   	   	   	{
   	   	   	if(sdo_byte&0x01)
   	   	   	{
   	   	   	   	SDO_1;
   	   	   	}
   	   	   	else
   	   	   	{
   	   	   	   	SDO_0;
   	   	   	}
   	   	   	sdo_byte>>=1;
   	   	   	   	count_bit++;
   	   	   	   	if(count_bit==8)
   	   	   	   	{
   	   	   	   	   	count_bit=0;
   	   	   	   	   	if(count_byte==5)
   	   	   	   	{
   	   	   	   	   	count_byte=0;
   	   	   	   	   	   	need_answer=0;
   	   	   	   	   	   	OEP2&=0xfd;//回复STM32后 SDO设置为输入
   	   	   	   	}
   	   	   	   	}
   	   	   	}
   	   	}
   	   	else
   	   	{//正常状态 
   	   	sdi_byte>>=1;
   	   	if(IOP2&0x01)
   	   	{//SDI HIGH
   	   	   	sdi_byte|=0x80;
   	   	}
   	   	else
   	   	{//SDI LOW
   	   	   	sdi_byte&=0x7f;    	   	   	
   	   	}
   	   	count_bit++;
   	   	if(count_bit==8)
   	   	{
        count_bit=0;
   	   	   	data[count_byte++]=sdi_byte;//save a byte data
   	   	   	if(data[0]==0xc5)
   	   	   	{
       	   	   	if(sdi_byte==0x5c&&count_byte==data[1])//接收到一帧完整数据
       	   	   	{
       	   	   	   	need_DealData=1;
       	   	   	}
   	   	   	}
   	   	   	else
   	   	   	{//无效数据
   	   	   	   	count_byte=0;
   	   	   	}
   	   	}
   	   	}
   	}
}


void main(void)
{ 
 //	__asm 
 //	org 0x400
 // fill 0x00,0x0
 //	__endasm;
  //p07 as 3264_EN,p17 as 3264_SCK,p20 as 3264_SDI,p21 as 3264_SDO
   	//p06 as 3264_EN_OUT
   	//p22 imitate 3264_SCK,P23 imitate 3264_SDI,P24 imitate 3264_EN//测试用 
  OEP0=0x40;//p07 input,p06 output
  OEP1=0x00;//p17 input
  OEP2=0x1c;//p20 input,p22 output,p23 output,p24 output
   	SCK_1;
   	SDI_0;
   	
   	PUP0=0xff;//P07 pull-up
   	PUP1=0xff;//P17 pull-up
 //	PUP2=0xff;//P20 pull-up
  
   	//外部中断0设置
 //	MINT0=1;//INT0下降沿触发
 // INT0IE=1;//使能外部0中断 
 //	INTF=0;//清除所有中断
 //	GIE=1;//使能总中断

   	sdi_byte=0;
   	count_bit=0;
  count_byte=0;
   	need_DealData=0;
  checksum=0;
// 	Delay_1ms(1);
   	while(1)
   	{
   	   	//发送OS测试命令
   	   	Mark_0x02;//测试用代码//正式版本 由STM32分配芯片编号
   	   	Send_byte_P23(0xc5);//帧头
   	   	Send_byte_P23(0x0a);//帧长
   	   	Send_byte_P23(0x02);//32P64 MCU ID
   	   	Send_byte_P23(0x00);//P00
   	   	Send_byte_P23(0x00);//输出0
   	   	Send_byte_P23(0x02);//32P64 MCU ID
   	   	Send_byte_P23(0x01);//P01
   	   	Send_byte_P23(0x00);//0x00 正常值
   	   	Send_byte_P23(0x05);//校验码
   	   	Send_byte_P23(0x5c);//帧尾

       	Delay_1ms(2);
       	   	//读32p64 OS测试结果
       	for(i=0;i<5;i++)
       	{
       	   	   	//data[i]=Read32p64();
       	   	   	temp=Read32p64();
       	   	   	data[i]=temp;
       	}
   	}
}

/*******************************************************************************
* Function Name  : Send_byte_P23
* Description    : p23 pin output data 
* Input          : 
* Output         : 
* Return         : 
*******************************************************************************/
void Send_byte_P23(uchar byte_data)
{
  uchar i;
  //SCK_0;
  for(i=0;i<8;i++)
  {
    SCK_1;
    if ((byte_data & 0x01)==1)
    {
   	   	   	SDI_1;
    } 
    else
    {
   	   	   	SDI_0;
    }
    SCK_0;
    Nop();
   	   	Nop();
   	   	byte_data>>=1;
  }
}

/*******************************************************************************
* Function Name  : Send_byte
* Description    : SDO PIN  output data 
* Input          : 
* Output         : 
* Return         : 
*******************************************************************************/
void Send_byte(uchar byte_data)
{
  uchar i;
  SCK_0;
  for(i=0;i<8;i++)
  {
    SCK_1;
    if ((byte_data & 0x01)==1)
    {
   	   	   	SDO_1;
    } 
    else
    {
   	   	   	SDO_0;
    }
    Nop();
    SCK_0;
    //Nop();
   	   	byte_data>>=1;
  }
}
/*******************************************************************************
* Function Name  : Delay_1ms
* Description    : 
* Input          : 
* Output         : 
* Return         : 
*******************************************************************************/
void Delay_1ms(uchar num)
{
  uchar i,j;
   	//SCK_0;//测试用
   	//SCK_1;//测试用
  for(i=num;i>0;i--)
  {
    for(j=123;j>0;j--)
    {
      Nop();
    }
  }
   	//SCK_0;//测试用
   	//SCK_1;//测试用
}

/*******************************************************************************
* Function Name  : Read32p64
* Description    : 
* Input          : 
* Output         : None
* Return         : 
*******************************************************************************/
uchar Read32p64()
{
  uchar i; //temp use for Nop instruce
  uchar a,read_data;
  read_data=0;
  for(i=0;i<8;i++)
  {
    SCK_1;  
    Nop();
    SCK_0;
    Nop();
    a=IOP2;
   	   	a&=0x01;
   	   	a<<=i;
    read_data|=a;
  }
  return read_data;
}