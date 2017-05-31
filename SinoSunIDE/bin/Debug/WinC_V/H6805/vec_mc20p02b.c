
extern @interrupt void _stext(void);		
extern @interrupt void SWI_ISR(void);	
extern @interrupt void TMI_ISR(void);	
extern @interrupt void KBI_ISR(void);
extern @interrupt void INT1_ISR(void);
extern @interrupt void INT0_ISR(void);
extern @interrupt void WDTI_ISR(void);

void (* const vectab[])() = {
	WDTI_ISR, 		
	KBI_ISR,		
	TMI_ISR,	
	INT1_ISR,
	INT0_ISR,		
	SWI_ISR,	  		   
	_stext			
	};
