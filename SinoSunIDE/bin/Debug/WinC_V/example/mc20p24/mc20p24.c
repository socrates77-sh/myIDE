
#include  <common.h>
#include  <reg_mc20p24.h>

#include  "var.h"
#include  "int_mc20p24.h"


/*****************************************************************************/
/*->主程序
*****************************************************************************/

void  main(void)
{
    SEI();
   
    InitRst();                //初始化系统计时器
    InitRam();                //初始化工作寄存器

    T0DATA = T_T0DATA;
    T0CON = T_T0CON;
    CLI();
    Quotient = 170;
    startbibi(1,0);

    while (1)
    {
        BTCLR = 1;
        ad_check();  
        check_button();
        mn_cycle();
        buzzer_deal();
    }
}

void  InitRst(void)
{
    BTCON   = 0xa2;
    MCR     = 0x01;
    PWMCON  = 0x08;
    PWMDATA = 0xff;

    P0CONH = 0x00;
    P0CONL = 0x5d;
    P0PND  = 0X00;
    
    P1CON  = 0x0a;

    P2CONH = 0x00;
    P2CONL = 0x00;

    P0  = 0x00;
    P1  = 0x00;
    P2  = 0x00;
    ADCON = 0x10;
}

void  InitRam(void)
{	
    #asm
        ram_clear:			
	          ldx	#192
        ram_clear1:
	          clr	$2F,X
	          decx
	          bne	ram_clear1
    #endasm    
}

void error(uchar i)
{
	com2_data = i;
  com1_data = 0x0e;  
  startbibi(6,0);
}

void startbibi(uchar i, uchar j)
{    
    Buzzer_H_Timer = BUZ_TIME_H;
    Buzzer_L_Timer = BUZ_TIME_L;    
    FLAG_BUZ_BX = 1;
    FLAG_BUZ_EN = 1;
    FLAG_BUZ_Forever = 0;
    if(j!=0) FLAG_BUZ_Forever = 1;
    Buzzer_Counter = i;
}

void K_LB(void)
{	
	if(R_KEY1 == R_KEY1_BAK)
		{
		   if(--R_K_LB == 0)
		     {
		     	R_KEY1_TRU.byte =R_KEY1_BAK;
		     	R_K_LB = C_K_LB;
		     	return;
		     }
		   else return;
		}
  //K_LB1
	R_KEY1_BAK = R_KEY1;
	R_K_LB = C_K_LB;
}

void buzzer_deal(void)
{	
	if(FLAG_BUZ_EN == 1)
		{
			if(FLAG_BUZ_Forever == 0)
				{
					if(Buzzer_Counter == 0) 
					{
						FLAG_BUZ_EN = 0;
						return;
					}
      //loop_buz
			if(FLAG_BUZ_BX == 1)
				{
					if(--Buzzer_H_Timer == 0)
						{
							Buzzer_Counter--;
							FLAG_BUZ_BX = 0;
							Buzzer_H_Timer = BUZ_TIME_H;							
						}
				}
			else
				{
					if(--Buzzer_L_Timer == 0)
						{
							FLAG_BUZ_BX = 1;
							Buzzer_L_Timer = BUZ_TIME_L;
						}
				}			
		}	
	}
}  

void  ad_check(void)
{	  	  
	  WORD ad_result;

    ADCON = T_ADCON;
    ADCH = 8;
    ADPS = 3;
    NOP();
    NOP();
    NOP();
    
    ADCE = 1;
    while (EOC == 0) ;
    ad_result.word  = ADDATAH;
    
    Buffer_Temper.word  += ad_result.word;
    if (++AD_Cnt >= AVERAGE_CNT) 
    	{    		
    		Buffer_Temper.word = Buffer_Temper.word>>7;
        Quotient = Buffer_Temper_L;	 
        AD_Cnt = 0;
        Buffer_Temper.word = 0;
      }
        	
    if (Quotient <= 41) //error_1
    	{
    		error(1);
    	}
    else if (Quotient >= 220) //error_2
      {
      	error(2);
      }   
    else
      {         	     
      if(FLAG_ON_OFF == 0) //待机状态
      	{ 
      		led_com_data.byte = 0;
      		                    		
      		com1_data = 0x0c;
      		com2_data = 0x0c;
      	}    
      else
      	{   
          hextobcd(Temperature);
          com1_data = Led_H;
          com2_data = Led_L;  	
        }           
      }           
}

void  check_button(void)
{		
    if  ( FLAG_Button == 1 )         
    {
        if ( R_KEY1_TRU.byte == 0)    //释放
        {
            FLAG_Button = 0;            
            Timer_press_125us = 0;
            Timer_press_25ms  = 0;            
        }   
        else
        {
           if(key_1 == 1)
        	 {
        	 	  if (Timer_press_25ms >= 60)
                {
                    Timer_press_25ms = 0;
                    FLAG_ON_OFF = 0;                                        
                }
        	 }          
        }
    }  
    else                           
    {
        if ( R_KEY1_TRU.byte != 0)     //按键
        {
            FLAG_Button = 1;            
            Timer_press_125us = 0;
            Timer_press_25ms = 0; 
            if(FLAG_ON_OFF == 0)
            	{
                 if(key_1 == 1)
            	   {
            	 			FLAG_ON_OFF = 1;            	 			
            	 			LED_1 = 1;             	 			
            	   } 
            	   else return;            		
            	}
            else
            	{            		
                if(key_2 == 1)
                	{
                		LED_2 = ~LED_2; 
                	}
                else if(key_3 == 1)
                	{
                		LED_3 = ~LED_3; 
                	}
                else if(key_4 == 1)
                	{
                		LED_4 = ~LED_4; 
                	}
                else if(key_5 == 1)
                	{
                		LED_5 = ~LED_5; 
                	}
                else if(key_6 == 1)
                	{
                		LED_6 = ~LED_6; 
                	}
                else return;
              }
            startbibi(1,0);  
        }
    }          
}



void hextobcd(uchar data)
{
	Led_L = data;
	Led_H = 0;
	
	while(Led_L >= 10) 
		{
			Led_L = Led_L - 10;
			Led_H++;			
		}
	if(Led_H >= 10) Led_H = Led_H - 10;		
}



void mn_cycle(void)
{
    if (Timer_refresh_25ms >= 10)
    {
        Timer_refresh_25ms = 0;
        Temperature = temperature_table[Quotient];
    }

    if (FLAG_ON_OFF == 1) //;倒计时处理
    {
        if (Timer_50ms >= 200)
        {
            Timer_50ms = 0;
            if (++Timer_10s >= 6)
            {
                Timer_10s = 0;
                if (++Timer_1min >= 60)
                {
                    Timer_1min = 0;
                    if(++Timer_1hour >= 60) //;倒计时时间到，转待机
                    	{
                    	  Timer_1hour = 0;
                        FLAG_ON_OFF      = 0;  
                        //startbibi(2,0);
                    }
                }
            }
        }
    }
}

void display(void)
{
		   	//display_led  
    	if(++com_cnter == 1)
    		{    			
    			Temp = table_0_9[com1_data];
    			
    			Temp_P0.byte = Temp & 0x8f;
    			Temp_P2.byte = Temp & 0x60;
    			
    			LED_com = 1;    			
					COM2 = 1;	 
					COM1 = 0;
					
					P0CONL = 0xaa;
    	    P0CONH = 0xae;        
          P2CONL = 0xaa;
    	    P2CONH = 0x4a;        
    	 
    	    P2 = Temp_P2.byte; 
          P0 = Temp_P0.byte;
    		}
    	else if(com_cnter == 2) 
    		{
    			Temp = table_0_9[com2_data];

    			Temp_P0.byte = Temp & 0x8f;
    			Temp_P2.byte = Temp & 0x60;
    			LED_com = 1;
    			COM1 = 1;
    			COM2 = 1;      			
    			
    			P0 = Temp_P0.byte;
    			P2 = Temp_P2.byte;   
    			 
    			P0CONL = 0x55;
    			P0CONH = 0x2e;        	    
    			P2CONH = 0x0a; 
    			
    			return;     
    		}    	
    		else if(com_cnter == 3) 
    		{
 			    Temp = ~P0 & 0x8f;   	    
    			R_KEY1 = ~P2 & 0x40 | Temp;
    			K_LB();      					
    			LED_com = 1;
    			COM1 = 1;
    			COM2 = 0; 
    			
    			P0CONL = 0xaa;
    	    P0CONH = 0xae;        
          P2CONL = 0xaa;
    	    P2CONH = 0x4a;        
    	 
    	    P2 = Temp_P2.byte; 
          P0 = Temp_P0.byte;
    		}
    	else 
    		{     			
          Temp_P0.byte = led_com_data.byte & 0x87;	
    			Temp_P2.byte = led_com_data.byte & 0x60;    	
    			LED_com = 0;
    			COM1 = 1;
    			COM2 = 1; 	  			
    			com_cnter = 0;  
    			
    			P0CONL = 0xaa;
    	    P0CONH = 0xae;        
          P2CONL = 0xaa;
    	    P2CONH = 0x4a;        
    	 
    	    P0 = Temp_P0.byte;
    	    P2 = Temp_P2.byte; 
            			
    		} 
    		

}

