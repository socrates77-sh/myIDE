
#include "mc32p64.inc"


    org 0x00
;CHIP ID: 0x3264
       goto start
       
start:
     nop
     call       fun01
     nop
        nop
        goto start
   
   
fun01:
        nop
        call    fun02
        return

fun02:
        call    fun03
        nop
        return
fun03:
        call    fun04
        nop
        return        
fun04:
        call    fun05
        nop
        return  
fun05:
        call    fun06
        nop
        return 
fun06:
        call    fun07
        nop
        return
fun07:
        nop
        call    fun08
        nop
        return        
        
fun08:
        nop
        nop
        return        

  end