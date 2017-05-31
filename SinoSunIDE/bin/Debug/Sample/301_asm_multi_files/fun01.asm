

#include "mc30p011.inc"
#include "var.inc"


;---------ȫ�ֱ�ţ��ⲿ�������--------------------------------
        extern  reset
        extern  loop
        
        global  fn_initial
        global  fn_keyscan

code_fun01      code  ;����һ�������ַ�Σ�����ָ����ʼ��ַ        
       
fn_initial:
        movai   0xff
        movra   P0
        movra   P1
        clrr    DDR0
        clrr    DDR1
        call    fn_clear_ram
        bset    led0
        bset    led1
        return      

        
fn_clear_ram:
    movai       48 ;0x10--0x3f
    movra       GPR
    movai       0x10 ;first ram addr
    movra       FSR
lab_clear_loop:    
    clrr        INDF
    INCR        FSR
    DJZR        GPR
    goto        lab_clear_loop          
    return 

fn_keyscan:
    movai       0xaa
    nop
    nop ;....
    
    return
    
    end
    
        