
#include "mc33p78.inc"


cblock 0x00
    acc_temp            ;save the ACC value
    status_temp             ;save the ACC status
        work01
        work02
        work03 
endc


        org 0x0
        goto    main
        
        org 0x8
intport:
        movra   acc_temp            ;save the ACC status
        swapar  STATUS              ;save the CPU status
        movra   status_temp 
        
  
end_int:        
        swapar  status_temp
        movra   STATUS
        movar   acc_temp
        retie
        
main:
        bclr    GIE
        ;movai     	0xff
        ;movra     	OEP2
        ;movra     	OEP1
        ;comr      	IOP1
        ;comr      	IOP2
        ;GOTO      	main
       	
        
ram_w:
        clrr     FSR0
        ;clrr    FSR1
ram_w_first:        
        incr    FSR0        
        movar   FSR0
        movra   INDF0        
        isubai  0xff
        jbset   Z
        goto    ram_w_first
ram_w_second: ;        0x100--0x1af
        incr    FSR1        
        movar   FSR1
        movra   INDF1        
        isubai  0xaf
        jbset   Z
        goto    ram_w_second
        clrr    FSR0
        movai   0x02
        movra   FSR1
ram_w_thr:
        incr    FSR0    ;0x200--0x3ff
        movar   FSR0
        movra   INDF2        
        isubai  0xff
        jbset   Z
        goto    ram_w_thr
        incr    FSR1
        movar   FSR1
        isubai  0x04
        jbset   Z
        goto    ram_w_thr
;check data
        clrr    FSR0
ram_first_check:
        
        incr    FSR0        
        movar   FSR0
        rsubar  INDF0
        jbset   Z ;if D_w==D_read
        goto    flag_error
        movar   FSR0        
        isubai  0xff
        jbset   Z        
        goto    ram_first_check
        ;0X100--0X1af
        clrr    FSR1
ram_check_second: ;        0x100--0x1af
        incr    FSR1        
        movar   FSR1
        rsubar   INDF1
        jbset   Z
        goto    flag_error
        movar   FSR1        
        isubai  0xaf
        jbset   Z
        goto    ram_check_second
        ;0x200--0x3ff
        clrr    FSR0
        movai   0x02
        movra   FSR1
ram_check_thr:
        incr    FSR0    ;0x200--0x3ff
        movar   FSR0
        rsubar   INDF2 
        jbset   Z
        goto    flag_error
        movar   FSR0
        isubai  0xff
        jbset   Z
        goto    ram_check_thr
        incr    FSR1
        movar   FSR1
        isubai  0x04
        jbset   Z
        goto    ram_check_thr        
        ;end ram_w
        
ram_clr:
        clrr     FSR0
        ;clrr    FSR1
ram_clr_first:        
        incr    FSR0        
        movar   FSR0
        clrr   INDF0        
        isubai  0xff
        jbset   Z
        goto    ram_clr_first
        ;0X100--0X1af
        clrr    FSR1
ram_clr_second: ;        0x100--0x1af
        incr    FSR1        
        movar   FSR1
        clrr   INDF1        
        isubai  0xaf
        jbset   Z
        goto    ram_clr_second
        
        clrr    FSR0
        movai   0x02
        movra   FSR1
ram_clr_thr:
        incr    FSR0    ;0x200--0x3ff
        movar   FSR0
        clrr   INDF2        
        isubai  0xff
        jbset   Z
        goto    ram_clr_thr
        incr    FSR1
        movar   FSR1
        isubai  0x04
        jbset   Z
        goto    ram_clr_thr
        
        ;rom reading funtion test
        movai   0xf0
        movra   FSR0
        movai   0x1f
        movra   FSR1
        movar   INDF3
        isubai  0x55
        jbset   Z
        goto    flag_error
        incr    FSR0
        movar   INDF3
        isubai  0x44
        jbset   Z
        goto    flag_error        
        incr    FSR0
        movar   INDF3
        isubai  0x56
        jbset   Z
        goto    flag_error
        goto    funtion_initial
flag_error:
        goto    $

funtion_initial:
        ;8 levers stack test
        call    fun001        
        
LOOP:
        NOP
        call   	fun001
        GOTO LOOP        
fun001:
        incr    work01
        call    fun002
        return
fun002:
        incr    work01
        call    fun003
        return
fun003:
        incr    work01
        call    fun004
        return
fun004:
        incr    work01        
        call    fun005
        return
fun005:
        incr    work01
        call    fun006
        return
fun006:
        incr    work01
        call    fun007
        return
fun007:
        incr    work01
        call    fun008
        return        
fun008:
        incr    work01
        ;call    fun009 ; 
        return
fun009:
        incr    work01
        return
        
        end        