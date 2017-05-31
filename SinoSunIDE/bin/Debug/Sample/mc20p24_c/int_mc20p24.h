

@interrupt void SWI_ISR(void)
{
}

@interrupt void INT1_ISR(void)
{

}

@interrupt void PWMINT_ISR(void)
{

}
@nosvf @interrupt void T0INT_ISR(void)
{		
    //T0DATA = T_T0DATA;
    T0CON  = T_T0CON;

    if(FLAG_BUZ_EN == 1) 
    	{
    		if(FLAG_BUZ_BX == 1)
    			{    				 
    				P1 = P1 ^ 0x01;
    			}
    	}
    else
    	{
    		P1 = P1 & 0xfe;
    	}
 
     if(++Timer_125us >= 2)
    	{
        		Timer_125us = 0 ;   		
    		if(++Timer_250us >= 200)
    			{
    				Timer_250us = 0;
    				Timer_50ms++;
    			}
    	}
    	//end_50ms
    if(++Timer_press_125us >= 200)
    	{
    		Timer_press_125us = 0;
    		Timer_press_25ms++;
    	}
    	//end_press_25ms
    	
    if(++Timer_refresh_125us >= 200)
     	{
     		Timer_refresh_125us= 0;
     		Timer_refresh_25ms++;
     	}
     	//end_refresh_25ms    	
     	display(); 	
}

@interrupt void INT0_ISR(void)
{

}