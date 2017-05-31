
extern @interrupt void _stext(void);		
extern @interrupt void SWI_ISR(void);	
extern @interrupt void INT1_ISR(void);	
extern @interrupt void INT0_ISR(void);	
extern @interrupt void T0INT_ISR(void);	
extern @interrupt void PWMINT_ISR(void);	
extern @interrupt void main();

void (* const vectab[])() = {
	INT1_ISR,		
	T0INT_ISR,		
	PWMINT_ISR,		
	INT0_ISR,		
	SWI_ISR,	  		   
	main	//_stext				
	};
