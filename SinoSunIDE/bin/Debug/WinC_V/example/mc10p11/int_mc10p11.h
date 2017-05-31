
@interrupt void SWI_ISR(void)
	{
	  
	}

@interrupt void KBI_ISR(void)
	{
	  KBIE = 0;
	  KBIF = 0;
	}
