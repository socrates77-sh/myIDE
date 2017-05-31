    
    ;#include "sub1.inc"

    
        extern funtion0
        extern funtion1
        extern abc
        extern aaa
         
kdkd equ 10h

PROG CODE
    ORG 03FFH
    GOTO lab_main

    org 000h
lab_main:
      nop
      nop
      movai 0xa5
      movra abc
      movra aaa
      call funtion0
      nop
lab_loop:
        nop
        call funtion1
        nop
  call funtion0 
        goto lab_loop
        
        end