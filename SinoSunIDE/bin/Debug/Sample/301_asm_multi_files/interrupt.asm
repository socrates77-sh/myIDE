
#include "mc30p011.inc"
#include "var.inc"


;----------------------------------

code_errupt	code	0x8
interrupt_code:
       movra    acc_temp
      swapar    STATUS
       movra    status_temp
       jbclr   T0IE      
       jbset   T0IF 
       goto    exit_interrupt
        bclr    T0IF
        bset     timer_10ms;
        bclr     timer_100ms
       
exit_interrupt:
    swapar    status_temp
    movra     STATUS
    swapr     acc_temp
    swapar    acc_temp
    retie   

;code_interrupt	code
        end    