#ifndef _H_INTERRUPT_VECTOR
#define _H_INTERRUPT_VECTOR

#include "macros.h"

#ifndef IRQ_WDT
#define IRQ_WDT    IRQ_NonHandled
#endif

#ifndef IRQ_KBI
#define IRQ_KBI    IRQ_NonHandled
#endif

#ifndef IRQ_T2
#define IRQ_T2     IRQ_NonHandled
#endif

#ifndef IRQ_T1
#define IRQ_T1     IRQ_NonHandled
#endif
#ifndef IRQ_INT1
#define IRQ_INT1     IRQ_NonHandled
#endif

#ifndef IRQ_INT0
#define IRQ_INT0    IRQ_NonHandled
#endif

#ifndef IRQ_SWI
#define IRQ_SWI     IRQ_NonHandled
#endif

extern void main(void);
extern __interrupt void IRQ_T0(void);
extern 	__interrupt void IRQ_NonHandled(void);

#endif
