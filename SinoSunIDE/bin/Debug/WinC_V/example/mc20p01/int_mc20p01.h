
@nosvf @interrupt void SWI_ISR(void)
	{
	  
	}

@nosvf @interrupt void INT1_ISR(void)
	{
	  
	}

@nosvf @interrupt void INT0_ISR(void)
	{
	  
	}

@nosvf @interrupt void TMI1_ISR(void)
	{	
	     
	}

@nosvf @interrupt void TMI0_ISR(void)
	{	
		T0IF=0;
	  MsCounter++;   
	}


@nosvf @interrupt void KBI_ISR(void)
	{
	  
	}
	
@nosvf @interrupt void WDTI_ISR(void)
	{
	  
	}
