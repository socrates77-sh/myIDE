
@nosvf @interrupt void SWI_ISR(void)
   	{
   	  
   	}

@nosvf @interrupt void INT1_ISR(void)
   	{
   	  
   	}

@nosvf @interrupt void INT0_ISR(void)
   	{
   	  
   	}

@nosvf @interrupt void TMI2_ISR(void)
   	{  	
   	     
   	}

@nosvf @interrupt void TMI1_ISR(void)
   	{  	
   	     
   	}

@nosvf @interrupt void TMI0_ISR(void)
   	{  	
           	     P1=~P1;
   	   	   	   	for (i=0;i<5;i++)
               	{
               	   	aaa = temp[i];
               	}
   	}


@nosvf @interrupt void KBI_ISR(void)
   	{
   	  
   	}
   	
@nosvf @interrupt void WDTI_ISR(void)
   	{
   	  
   	}
