#include "interrupt_vector.h"

void (* const vectab[])() =
{
	IRQ_WDT,
	IRQ_KBI,
	IRQ_T2,
	IRQ_T1,
	IRQ_T0,
	IRQ_INT1,
	IRQ_INT0,
	IRQ_SWI,
	main,
};


