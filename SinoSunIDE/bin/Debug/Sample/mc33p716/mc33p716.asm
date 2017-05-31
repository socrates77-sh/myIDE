#include mc33p716.inc

;*************ram****************
;#define	r0	0x1af
;#define	r1	0x1ae	
;#define	r2	0x1ad
cblock 0x00
r0
r1
r2
r3
acc_temp
status_temp
flag



endc

#define  stop_enable         flag,0
#define  clk_enable          flag,1
#define  key                 P2,1

;***********************
#define R0 r0
#define OUT_IR	P1,7

#define    RAM_COUNT    0xff    ;48个RAM
#define    FIR_RAM_ADDR    0x00    ;RAM首地址
;#undefine  INDF
;#define    INDF		INDF3
#define    MAX_DIS_DATA    18




            org 0x0
        goto    main

        org 0x8        
start_just_int:
        jbclr    T0IE
        jbset    T0IF
        goto     lab_if_timer1_int
;----timer0-----------------------
        bclr    T0IF
        

;---------------------------------
lab_if_timer1_int:        
        jbclr    T1IE
        jbset    T1IF
        goto    just_int0        
;*********timer1***********
        bclr    T1IF
        ;movai   0x3c
        ;movra   TBCR
        jbclr       P1,4
        goto        lab_p01_4
        bset        P1,4
        goto        end_time1_interrupt
lab_p01_4:    
        bclr        P1,4


end_time1_interrupt:
;**********end_timer1******
just_int0:
    jbclr    INT0IE
    jbset    INT0IF
    goto     just_int1
;**********int0************
    bclr    INT0IF
    

    
    ;goto	end_time1_interrupt
end_int0_interrupt:
just_kbim:
        jbclr KBIE
        jbset   KBIF
        goto    just_int1
        bclr    KBIF
;*********end int0*********
just_int1:
    jbclr    INT1IE
    jbset    INT1IF
    goto     end_just_int
;**********int0************
    bclr    INT1IF

;**********end_kbim********
end_just_int:
exit_interrupt:

    retie     
 
    
;***************************************
main:
    clrwdt
 ;   bset    WDTEN
    bclr    GIE    ;开全局中断
;************main loop***************
main_loop:
    clrwdt
    movai	0x87
    movra	DKW	

;*******************************
;*****按键值保存在key_value*****
;**T扫 P00-P16-P10-P27-P20 16个IO
key_deal:
    movai	0xff
    movra	DKWP0
    movra	DKWP1
    movra	DKWP2

    bset	LFEN
    
    ;timer1
    movai       100
    movra       TBLOAD
    movai       0x24
    movra       TBCR
    
    bset KBIE
    bset        stop_enable
    
    stop	
    movai   0xff
    movra   DDR1
    bclr    KBIE
    clrr    DKWP1
    bset GIE
    bset    TBCR,5
    bset    TBCR,2

loop:
        
    ;call    keyscan
        jbclr   key
        goto    main


        goto loop
        
;keyscan:
;        jbclr   key; P2,1 ;
;        goto    end_keyscan
;        call    delay10ms
;        call    delay10ms
;        call    delay10ms
;        jbclr   key; P2,1
;        goto    end_keyscan
;
;        jbclr       stop_enable
;        goto        lab_p00_0
;        bset        stop_enable
;        bclr        clk_enable
;        goto        cancle_end_keyscan
;lab_p00_0:    
;        bclr        stop_enable
;        bset        clk_enable        
;                                
;cancle_end_keyscan:
;        jbset   key;P1,6 
;        goto    cancle_end_keyscan
;        call    delay10ms
;        ;call    delay10ms
;        call    delay10ms
;        jbset   key;P1,6
;        goto    cancle_end_keyscan
;end_keyscan:        
;        return
;
;delay10ms:
;        movai   0xff
;        movra   r1
;re_load:
;        movai   0xff        
;        movra   r0
;del_dec:        
;        djzr    r0
;        goto    del_dec
;        djzr    r1
;        goto    re_load
;        nop
;        return         



    end
