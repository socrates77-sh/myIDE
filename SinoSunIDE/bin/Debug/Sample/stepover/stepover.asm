    
    ram80 equ $80
    ram81 equ $81
    ram82 equ $82

    org $40
    temp00 rmb 1
    temp01 rmb 1
    temp02 rmb 1

    org $1c00
reset:
  lda #$aa      
  sta ram80
  lda #$55
  sta ram80

loop:
    nop
    jsr abcd
    jsr abcdd
    nop
    bra loop

abcd:
    nop
    lda ram81
    inca
    sta ram81
    rts
abcdd:
    nop
    lda ram82
    inca
    sta ram82
    rts   
    
    org $1ffe
    fdb reset