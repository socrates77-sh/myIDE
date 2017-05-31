

#include mc32p821.inc


#define     RAM_first_size  0x80
  


cblock  0x0
    acc_temp            ;save the ACC value
    status_temp             ;save the ACC status
 work01
 work02
 work03 

endc

        org 0x00
        goto    main

        org 0x8
intport:
        movra   acc_temp            ;save the ACC status
        swapar  STATUS              ;save the CPU status
        movra   status_temp 
;int_t0:
;        jbset   T0IE
;        goto    int_t1
;        jbset   T0IF
;        goto    int_t1
;        bclr    T0IF
;        ;---
;
;
;int_t1:        
;        jbset   T1IE
;        goto    int_kbi
;        jbset   T1IF
;        goto    int_kbi
;        bclr    T1IF
;        ;---
;        jbset   IOP1,3
;        goto    set_p13_h
;        bclr    IOP1,3
;        goto    int_kbi
;set_p13_h:        
;        bset    IOP1,3         
;
;int_kbi:
;        jbset   KBIE
;        goto    int_int0
;        jbset   KBIF
;        goto    int_int0        
;        bclr    KBIF    
;        incr    work01
;         
;int_int0:
;        jbset   INT0IE
;        goto    int_int1
;        jbset   INT0IF
;        goto    int_int1
;        bclr    INT0IF
;        ;-----
;
; 
;int_int1:
;        jbset   INT1IE
;        goto    end_int
;        jbset   INT1IF
;        goto    end_int
;        bclr    INT1IF
;        ;----        
;        jbset   IOP1,2
;        goto    set_p12_h
;        bclr    IOP1,2
;        goto    end_int
;set_p12_h:        
;        bset    IOP1,2        
end_int:        
        swapar  status_temp
        movra   STATUS
        movar   acc_temp
        retie

main:
        bclr    GIE
        movai   0xaa
        movra   0x00
        call    fn_ram_test
        
        ;rom reading funtion test
        movai   0xf0
        movra   FSR0
        movai   0x03
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
        ;PC test
        clrr    work01
look_next_data:        
        incr    work01
        movar   work01
        call    pc_test_tab
        rsubar  work01
        jbset   Z
        goto    flag_error
        movar   work01
        isubai  0x04
        jbset   Z
        goto    look_next_data
        
        
        goto    funtion_initial
flag_error:
        goto    $        
funtion_initial:
        ;8 levers stack test
        call    fun001
        
        movai   0x00
        movra   IOP0
        movra   IOP1
        movra   OEP0
        movra   OEP1
        
        movai   0xff
        movra   PUP0
        movra   PUP1        
loop:
        ;incr    work01
        ;movar   work01
        ;movra   IOP1
        ;movra   IOP0
        movar   IOP1
        movar   IOP0
        
        goto    loop        

;----------------------------------------------
;       ram test
;  write & read all ram 
;
;-----------------------------------------------        
fn_ram_test:

ram_w:
        clrr     FSR0
        ;clrr    FSR1
#ifdef RAM_first_size        
ram_w_first:        
        incr    FSR0        
        movar   FSR0
        movra   INDF0        
        isubai  RAM_first_size-1
        jbset   Z
        goto    ram_w_first
#endif
#ifdef RAM_second_size        
ram_w_second: ;        0x100--0x1af
        incr    FSR1        
        movar   FSR1
        movra   INDF1        
        isubai  RAM_second_size-1
        jbset   Z
        goto    ram_w_second
#ENDIF
#ifdef  RAM_third_size        
        clrr    FSR0
        movai   0x02
        movra   FSR1
ram_w_thr:
        incr    FSR0    ;0x200--0x3ff
        movar   FSR0
        movra   INDF2        
        isubai  RAM_third_size-1
        jbset   Z
        goto    ram_w_thr
        incr    FSR1
        movar   FSR1
        isubai  0x04
        jbset   Z
        goto    ram_w_thr
#ENDIF        
;check data

        clrr    FSR0
#ifdef  RAM_first_size        
ram_first_check:
        
        incr    FSR0
        movar   INDF0                
        movar   FSR0
        rsubar  INDF0
        jbset   Z ;if D_w==D_read
        goto    flag_error
        movar   FSR0        
        isubai  RAM_first_size-1
        jbset   Z        
        goto    ram_first_check
#ENDIF
#ifdef  RAM_second_size        
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
#ENDIF
#ifdef  RAM_third_size        
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
#endif                
ram_clr:
        clrr     FSR0
        ;clrr    FSR1
#ifdef  RAM_first_size        
ram_clr_first:        
        incr    FSR0        
        movar   FSR0
        clrr    INDF0        
        isubai  RAM_first_size-1
        jbset   Z
        goto    ram_clr_first
#ENDIF
#ifdef  RAM_second_size        
        ;0X100--0X1af
        clrr    FSR1
ram_clr_second: ;        0x100--0x1af
        incr    FSR1        
        movar   FSR1
        clrr   INDF1        
        isubai  0xaf
        jbset   Z
        goto    ram_clr_second
#endif 
#ifdef  RAM_third_size                
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
#endif        

        return


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
        
        org 0x2fd
pc_test_tab:        
        addra   PCL
        retai   0x00
        retai   0x01
        retai   0x02
        retai   0x03
        retai   0x04
        
        org 0x3f0
        dw 0xaa55,0x3344,0x1256,0x7890        

        end        