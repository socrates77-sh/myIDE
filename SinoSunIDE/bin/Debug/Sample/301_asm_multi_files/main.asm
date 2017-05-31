#include "mc30p011.inc"
#include "var.inc"

        extern  fn_initial
        extern  fn_keyscan
        extern  fn_display
        extern  fn_otherthing

code_0x3ff      code    0x3ff ;����һ������Σ���ʼ��ַΪ0x3ff
        goto    reset
code_0x00       code    0x00    ;����һ������Σ���ʼ��ַΪ0x00
        goto    reset

        
code_reset      code    ; ����һ������Σ����̶���ʼ��ַ
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
                