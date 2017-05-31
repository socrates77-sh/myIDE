#include "mc30p011.inc"
#include "var.inc"

        extern  fn_initial
        extern  fn_keyscan
        extern  fn_display
        extern  fn_otherthing

code_0x3ff      code    0x3ff ;定义一个代码段，起始地址为0x3ff
        goto    reset
code_0x00       code    0x00    ;定义一个代码段，起始地址为0x00
        goto    reset

        
code_reset      code    ; 定义一个代码段，不固定起始地址
reset:
     bclr       GIE 
     call       fn_initial
     clrr       T0CR
     bset       T0IE ;enable timer0 interrupt  
     bset       GIE

loop:
        call    fn_keyscan
        call    fn_display
        call    fn_otherthing
        goto    loop
   
        end     

    end
                