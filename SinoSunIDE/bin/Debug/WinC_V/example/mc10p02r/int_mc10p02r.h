
@nosvf @interrupt void SWI_ISR(void)
	{
	  
	}

@nosvf @interrupt void KBI_ISR(void)
	{
	   KBIC=1;	
	}

@nosvf @interrupt void TMI_ISR(void)
	{
  	 TIF=0;
     TDR=TDR+226;
     Debounce++;  
	}
