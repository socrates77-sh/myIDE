#ifndef _H_INTERRUPT_VECTOR
#define _H_INTERRUPT_VECTOR

#include "adc.h"

#ifndef INT1_ISR
#define INT1_ISR    IRQ_NonHandled
#endif
/*
#ifndef IRQ_T0INT
#define IRQ_T0INT    IRQ_NonHandled
#endif
*/
#ifndef PWMINT_ISR
#define PWMINT_ISR     IRQ_NonHandled
#endif


#ifndef IRQ_INT0
#define IRQ_INT0    IRQ_NonHandled
#endif

#ifndef SWI_ISR
#define SWI_ISR     IRQ_NonHandled
#endif

extern void main(void);
extern __interrupt void IRQ_T0INT(void);
#endif
