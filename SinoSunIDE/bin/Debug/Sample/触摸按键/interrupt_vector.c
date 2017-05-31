#include "interrupt_vector.h"

extern 	__interrupt void IRQ_NonHandled(void);
void (* const vectab[])() =
{
	INT1_ISR,		
	T0INT_ISR,		
	PWMINT_ISR,		
	INT0_ISR,		
	SWI_ISR,	  		   
	_stext	
};
