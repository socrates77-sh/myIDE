
extern @interrupt void _stext(void);		
extern @interrupt void SWI_ISR(void);	
extern @interrupt void TMI_ISR(void);	
extern @interrupt void KBI_ISR(void);


void (* const vectab[])() = {
	KBI_ISR,		
	TMI_ISR,	
	_stext,
	_stext,
	SWI_ISR,	  		   
	_stext				
	};
