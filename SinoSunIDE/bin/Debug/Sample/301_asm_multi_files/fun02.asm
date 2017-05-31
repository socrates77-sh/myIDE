

#include        "mc30p011.inc"
#include        "var.inc"

        global  fn_display
        global  fn_otherthing
        
        extern  fn_keyscan
        
        
code_fun02      code  ; 定义一个代码段，不指定程序起始地址
        
fn_display:
        bclr    led0
        call    fn_keyscan
        NOP
        nop 
        nop ;....
        return
        
fn_otherthing:
        nop
        nop
        return     
     
     end   