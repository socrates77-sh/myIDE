;                                  
;                                    ��Ȩ����(c)����΢����
;*********** ��ë  ��ů�� ���� ���ư� ��Դ���������оƬ  
;��Ʒ�ͺ�:
;�ļ�  ��:
;ԭ��ͼ  :
;MCU     :MC20P24B
;ԭʼ�汾: SPW13031P
;����  ��:�ƾ�
;��������:2013/11/22
;�衡  ��: 

;      
;      
;    
;=================================================================================
;�޸�ʱ��:
;�汾    :
;�޸�  ��:

;�衡  ��: 
;=================================================================================
;�޸�ʱ��: 
;�汾    :
;�޸�  ��:
;�衡  ��: 
;          
;          
;���߷���ͼ:��
;������������                   _______��   ______
;        gnd            vss    ��1 ��  \___/   20��   vcc     
;        guoling        P1.0   ��2             19��   P0.0/ad0     
;        ralay          P1.1   ��3             18��   P0.1          
;                       P1.2   ��4             17��   P0.2          
;                       P2.0   ��5             16��   P0.3         
;        nj_p           P2.1   ��6             15��   P0.4          
;        tao_p          P2.2   ��7             14��   P0.5/ad5     yk_int  
;        you_p          P2.3   ��8             13��   P0.6         buzz 
;        zuo_p          P2.4   ��9             12��   P0.7/ad7    
;        hou_p          P2.5   ��10            11��   P2.6/ad8     qian_p    
;                              ��������������������
;*********************************************************************************
;*********************************************************************************
;#include mc20p24.inc



;=================================================================================
;����RAM:00H---02FH
    t0c      define    $00      ;t0cnt    t0������
    t0data   define    $01      ;t0data   t0�Ƚ�ֵ,��t0c������=t0data,�жϳ���
    t0m      define    $02      ;t0con    t0������
   ;��Ƶ bit7   bit6    ,��ϵͳʱ�ӷ�
   ;      0       0     ,fcpu/4096
   ;      0       1     ,fcpu/256
   ;      1       0     ,fcpu/8
   ;      1       1     ,fcpu/1
   ;bit5  bit4   ��
   t0clr    define     3,t0m    ;=1��t0c(t0cnt)
   ;bit2         �� 
   t0ie     define     1,t0m    ;=1,t0�жϿ�
   t0f      define     0,t0m    ;t0�жϱ�־λ

    mcr      define    $03      ;lvre�͵�ѹ��������

    wtdm     define    $0c      ;btcont ���Ź�����,#%10100000���ɲ���
    ;bit7   bit6   bit5  bit4
    ;  1      0     1    0      ;��
    ;  0      0     0    0      ;�ر�
    ;bit3   bit2                ;��Ƶ
    ;  0      0                 ;fsys/4096
    ;  0      1                 ;fsys/1024
    ;  1      0                 ;fsys/256
    ;  1      1                 ;fsys/128

    wtdc     define    $0d      ;btcnt ���Ź� 
    p0       define    $10
    p1       define    $11
    p2       define    $12

    p0mh     define    $16    ;p0conh,P0��4λ���ƼĴ���
    p0mL     define    $17    ;p0conh,P0��4λ���ƼĴ���
    p0int    define    $18    ;p0.0,P0.1���ж˿���7��  p0pnd
    p1m      define    $19    ;p1con,P01���ƼĴ���
    p2mh     define    $1a    ;p2conh,P2��4λ���ƼĴ���
    p2mL     define    $1b    ;p2conh,P2��4λ���ƼĴ���

    pwmc     define    $22    ;pwmdata
    pwmm     define    $23    ;pwmcon

    adm      define    $27    ;adcon,AD���ƼĴ���
   ; ͨ��ѡ�� bit7  bit6  bit5  bit4
   ;           0      0    0     0      ;AD0  ,��P0.0��
   ; ...........................
   ;           1      0    0     0      ;AD8
   feoc     define    3,adm    ;bit3 ת��������־λ
   ;��Ƶ��  bit2  bit1
   ;          0    0           ;fcpu/8
   ;          0    1           ;fcpu/4
   ;...............................
   fads     define    0,adm    ;bit0 ת��������־λ

    bufadh   define    $28    ;ADת�������8λ
    bufadL   define    $29    ;ADת�������2λ
;-------------------------------------------------------------------------------
;���߶��壺
    guoling      define  0,p1             ;����

    buzz         define  6,p0
    yk_int       define  5,p0             ; 
    raly         define  1,p1             ; 
    nj_p         define  1,p2
    tao_p        define  2,p2
    you_p        define  3,p2
    zuo_p        define  4,p2
    hou_p        define  5,p2             ;ͻ�����ؼ�ⷢ��
    qian_p       define  6,p2
 
    
    ;buz_c        define  5,p0mh   ;P0.6
    ;zdata_m      define  7,p0mh   ;P0.7
    ;kdata_m      define  6,p2mh   ;P2.6
    ;data1629_m   define  3,p2mh   ;P2.5  ,bit3=1���

    ;bit1int_m    define  2,p1m    ;P1.1  ,bit2=1�����������
    ;bit1out_m    define  3,p1m    ;P1.1  ,bit3=1���
    ;bit2int_m    define  0,p1m    ;P1.0  ,bit0=1�����������
    ;bit2out_m    define  1,p1m    ;P1.0  ,bit1=1���

 
  ;=============================================================
  ;------------------��������-------------
   num_wtd         equ    #%10100000      ;�رտ��Ź�
   ;��¯��λ��Ӧ60�ݹ�������
   taowat_num1     equ    #10             ;1/7=10
   taowat_num2     equ    #17             ;2/7=17
   taowat_num3     equ    #26             ;3/7=26
   taowat_num4     equ    #34             ;4/7=34
   taowat_num5     equ    #43             ;5/7=43
   taowat_num6     equ    #51             ;6/7=51
   taowat_num7     equ    #60             ;60����



;8����:
   sm_num0    equ         #%00111111             ;=0
   sm_num1    equ         #%00000110 
   sm_num2    equ         #%01011011 
   sm_num3    equ         #%01001111 
   sm_num4    equ         #%01100110 
   sm_num5    equ         #%01101101 
   sm_num6    equ         #%01111101 
   sm_num7    equ         #%00000111 
   sm_num8    equ         #%01111111 
   sm_num9    equ         #%01101111 
   sm_numa    equ         #%01110111 
   sm_numb    equ         #%01111100 
   sm_numc    equ         #%00111001 
   sm_numd    equ         #%01011110 
   sm_nume    equ         #%01111001 
   sm_numf    equ         #%01110001 


  ;=============================================================
  ;------------------------------------------------------------- 
  ;�û�ram����;$0030-$00FF 
  org     $30

  acc_back          ds    1
  status_back       ds    1
  ;-------------------------------------------------------------
  ;ʱ��:
 
  yk_125us          ds    1
  t_125us           ds    1             ;125us 
  led_125us         ds    1             ;
  t_1ms             ds    1             ;1ms��ʱ��

  t_250ms           ds    1            ;250ms
  t_1s              ds    1            ;1����               
 

 ; mh_1ms            ds    1 
 ; mh_250ms          ds    1 
 ; mh_500ms          ds    1
  ;-------------------------------------------------------------

  
  f_a               ds    1
  buzz_on      define     0,f_a   
  buzlong      define     1,f_a          ;  
  key_lock     define     3,f_a 
  power_on     define     4,f_a 
  yk_ok        define     5,f_a 
   


  f_b              ds     1
  yk_wate      define     1,f_b          ;ѭ���ȱ�־
  gl_err       define     2,f_b          ;ѭ���ȱ�־  

                 

  ;----------------------------------------------------------------------
  num_buzz        ds      1              ;�������������
  num_watt        ds      1
  ;-----------------------------------------------------------
  pcs_ykup        ds      1
  pcs_ykup125us   ds      1

  pcs_buzz        ds      1               ;��������ʱ������=5ms*pcs_buzz
  pcs_com1        ds      1
 

  pcs_gl_low      ds      1
  pcs_gler        ds      1
  pcs_gl_hig      ds      1
  pcs_watt125us   ds      1

  ;-------------------------------------------------------------
  buf_qian        ds      1
  buf_hou         ds      1
  buf_zuo         ds      1
  buf_you         ds      1
  buf_nj          ds      1
  buf_tao         ds      1
 


  buf_outtime     ds      1
  buf_outqian     ds      1
  buf_outhou      ds      1
  buf_outzuo      ds      1
  buf_outyou      ds      1
  ;---------------------------------------------------------------         
  tm1629b_bit     ds      1     
  ;---------------------------------------------------------------
  yk_num1         ds      1               
  yk_num2         ds      1   
  yk_num3         ds      1     
 ;================================================================

    org       $BF
  buf_sp        ds     1    

  ;====================================================================
  ;====================================================================
  ;$1000-$1ff3 ���romָ��, $1ff4---$1ffd ���ж����;$1ffE--$1FFF ��λ���
    ORG      $1000
    fdb      star   



;*******����ʼ*************
;*********start***************
star:
    sei                          ;�ر��ж�
    rsp                          ;��ջ��λ
    lda      num_wtd             ;=btcon  ,�رտ��Ź�
    sta      wtdm                ;���Ź����ƼĴ���
   ;clr      wtdc                ;���Ź�����������

;--------------------------------------------  
  ;ram����
clrram:
    ldx      #192;208
ram_clear1:
    clr      $2F,x
    decx
    beq      opp2   
    jmp      ram_clear1
    
opp2:
    rsp                          ;��ջ��λ
    jsr      ioset        
    jsr      time_stp 
;-------------------------------------------- 
    bset     0,num_buzz
    bset     7,t_1s              ;�ؼ̵���


    lda      #03
    sta      buf_qian
    sta      buf_hou
    sta      buf_you             ;???????????????
    sta      buf_zuo
    sta      buf_nj
    sta      buf_tao
    bset     power_on
  ;========================================================================================
  ;========================================================================================
  ;������ѭ����:
main:
  nop;jsr           yk_main           ;������·���ݽ���
     lda           led_125us
     cmp           #64
     bhs           main00          
     jmp           main             ;8MS 
main00:
     clr           led_125us
     inc           pcs_ykup

     jsr           buzz_main         ;������  
     jsr           time_main         ;ʱ�����, 
     jsr           outcfm_main       ;hort���� 
     jmp           main 
  ;========================================================================================
  ;======================================================================================== 




  ;===========================================================
  ;===========================================================
  ;�������
  outcfm_main:
       brset         power_on,outcfm_main00
       jmp           out_alloff    ;��    
  outcfm_main00:
       jsr           outk_tao      ;��¯
       jsr           outk_qian     ;ǰ
       jsr           outk_hou      ;��
       jsr           outk_zuo      ;��
       jsr           outk_you      ;��
       jsr           outk_nj       ;ů��
      ;jmp           outk_raly     ;�̵���
  ;------------------------------------------------------------
  ;�̵���:
  outk_raly:
       bclr          raly
       lda           t_1s
       cmp           #05
       bhs           outk_raly00  
       bset          raly
  outk_raly00:
       nop
       rts
  ;------------------------------------------------------------
  ;ů��
  outk_nj:
        tst          buf_nj
        bne          outk_nj00
        jmp          outnj_off      
  outk_nj00:
        ;jmp         outnj_on
  outnj_on:
        bclr         nj_p
        rts
  outnj_off:
        bset         nj_p
        rts
  ;------------------------------------------------------------
 

  ;-------------------------------------------------
  ;ǰ
  outk_qian:
        lda          buf_qian
        sta          pcs_com1
        jsr          qhzy_wattcunt            ;
        sta          buf_outqian      
        rts
  ;��
  outk_hou:
        lda          buf_hou
        sta          pcs_com1
        jsr          qhzy_wattcunt            ;
        sta          buf_outhou     
        rts
  ;��
  outk_zuo:
        lda          buf_zuo
        sta          pcs_com1
        jsr          qhzy_wattcunt            ;
        sta          buf_outzuo     
        rts
  ;��
  outk_you:
        lda          buf_you
        sta          pcs_com1
        jsr          qhzy_wattcunt            ;
        sta          buf_outyou     
        rts

  ;ǰ�����ҹ���:
  qhzy_wattcunt:
        tst          pcs_com1
        bne          watt_qhzyp1
        jmp          qhzy_d0                  ;0��
  watt_qhzyp1:
        lda          pcs_com1
        cmp          #01
        bne          watt_qhzyp2
        jmp          qhzy_d1                  ;1��
  watt_qhzyp2:
        lda          pcs_com1
        cmp          #02
        bne          watt_qhzyp3
        jmp          qhzy_d2                  ;2��
  watt_qhzyp3:
        cmp          #03
        bne          watt_qhzyp4
        jmp          qhzy_d3                  ;2��
  watt_qhzyp4:
        jmp          qhzy_d4                  ;3��

  qhzy_d0:
        lda          #00                      ;
        jmp          qhzy_dsetok
  qhzy_d1:
        lda          #04                      ;700W֮40W  1/20=3.5
        jmp          qhzy_dsetok
  qhzy_d2:
        lda          #20                      ;700W֮200W 1/3=20
        jmp          qhzy_dsetok
  qhzy_d3:
        lda          #40                      ;700W֮450W 2/3=40
        jmp          qhzy_dsetok
  qhzy_d4:
        lda          #60                      ;60����
       ;jmp          qhzy_dsetok
  qhzy_dsetok:
        sta          pcs_com1    
  ;���ÿ��:
        ldx          pcs_com1
        lda          table_watt,x
        nop  
        rts
  ;-------------------------------------------------
  ;��¯
  outk_tao:
        lda          buf_tao
        cmp          #02
        bhs          watt_taop1
        jmp          tao_d0                   ;0,1��
  watt_taop1:
        jsr          time_xt_clr              ;�̵�����ʱ5S
        lda          buf_tao
        cmp          #02
        bne          watt_taop2
        jmp          tao_d1                   ;2��
  watt_taop2:
        cmp          #03
        bne          watt_taop3
        jmp          tao_d2                   ;3��
  watt_taop3:
        cmp          #04
        bne          watt_taop4
        jmp          tao_d3                   ;4��
  watt_taop4:
        cmp          #05
        bne          watt_taop5
        jmp          tao_d4                   ;5��
  watt_taop5:
        cmp          #06
        bne          watt_taop6
        jmp          tao_d5                   ;6��
   watt_taop6:
        cmp          #07
        bne          watt_taop7
        jmp          tao_d6                   ;7��
  watt_taop7:
        jmp          tao_d7                   ;8��

  tao_d0:
        lda          #00                      ;
        jmp          tao_dsetok
  tao_d1:
        lda          taowat_num1              ;1/7=10
        jmp          tao_dsetok
  tao_d2:
        lda          taowat_num2              ;2/7=17
        jmp          tao_dsetok
  tao_d3:
        lda          taowat_num3              ;3/7=26
        jmp          tao_dsetok
  tao_d4:
        lda          taowat_num4              ;4/7=34
        jmp          tao_dsetok
  tao_d5:
        lda          taowat_num5              ;5/7=43
        jmp          tao_dsetok
  tao_d6:
        lda          taowat_num6              ;6/7=51
        jmp          tao_dsetok
  tao_d7:
        lda          taowat_num7              ;60����
       ;jmp          tao_dsetok
  tao_dsetok:
        sta          pcs_com1    
  ;���ÿ��:
        ldx          pcs_com1
        lda          table_watt,x
        sta          buf_outtime 
        nop     
        rts
  ;----------------------------------------------------------
  out_alloff:
        clr          buf_outtime     ;����¯
        clr          buf_outqian
        clr          buf_outhou
        clr          buf_outzuo
        clr          buf_outyou
        nop
        rts
  ;===========================================================
  ;===========================================================







  ;============================================================
  ;============================================================
  ;******************** 4  ������ 8ms��1��  *******************
buzz_main:
     lda         num_buzz
     bne         buzz_main00        
     jmp         buzz_off            ;���뷢�� 
buzz_main00:     
     ;bset        buz_c
     inc         pcs_buzz            ;16ms*pcs_buzz
     lda         pcs_buzz
     cmp         #30
     blo         bu_mainon0a         
     jmp         buzz_low            ;���ڵ�����ת
 bu_mainon0a:
     brclr       buzlong,bu_mainon0b
     jmp         buzhig_h00          ;����(0.5)��־
  bu_mainon0b:
     lda         pcs_buzz
     cmp         #10
     blo         buzhig_h00        
     jmp         buzz_low
buzhig_h00:
     bset        buzz_on              ;0.25S����
     rts                              ;0.25S--0.5S
   
buzz_low:
     bclr        buzz_on
     lda         pcs_buzz
     cmp         #130;30+100
     blo         buzlow0a   
     jmp         buzz_low0
  buzlow0a:
     brclr       buzlong,buzlow0b
     rts                           ;����(0.5)��־
  buzlow0b:
     lda         pcs_buzz
     cmp         #20;10+10
     bhs         buzz_low0        
     rts

buzz_low0:
     clr         pcs_buzz        ;0.5S����1��
     dec         num_buzz
     beq         buzz_out        
     rts
        
buzz_out:
     bclr        buzlong
    ;clr         pcs_adint
buzz_off:
     bclr        buzz_on
    ;bclr        buz_c           ; 
buzmain_out:     
     rts
     
  ;==========================================================
  ;==========================================================







    
  

     
  ;==========================================================
  ;========================================================== 
despaly_main:
     sta        pcs_com1
despaly_main00:                  ;3us*pcs_com1
     dec        pcs_com1 
     beq        despaly_mainout         
     jmp        despaly_main00
despaly_mainout:
     rts
  ;==========================================================
  ;==========================================================







  ;========================================================
  ;======================================================== 
  ;------ ʱ��У0 -------------
time_clr:    
     ;jsr        time_mh_clr
time_xt_clr:
     CLR        t_1ms
     clr        t_250ms
     clr        t_1s
     rts
;time_mh_clr:
     ;clr        mh_1ms
     ;clr        mh_250ms
     ;clr        mh_500ms
     ;rts
  ;========================================================
  ;========================================================








  
     
     

  

  ;==========================================================
  ;==========================================================
  ;*************         12  ʱ�����        ****************
time_main:
    ;jsr         time_mh_1s
    ;jmp         time_xt_1s  
   ;---------------------------------

time_xt_1s:
     brset       2,t_250ms,xt_1sinc   ;250ms*4=1S 
     rts
  xt_1sinc: 
     clr         t_250ms
     lda         t_1s
     cmp         #255
     bhs         xt_1sinc00  
     inc         t_1s
  xt_1sinc00:
     rts  
 
  

;time_mh_1s:
      ;brset      1,mh_250ms,mh_500msinc
      ;rts
 ;mh_500msinc:
      ;clr        mh_250ms
      ;inc        mh_500ms
      ;lda        mh_500ms
      ;cmp        #15                       ;14+1;SET=50W*14=700W
      ;blo        mh_500msinc01
      ;clr        mh_500ms
  ;mh_500msinc01:   
      ;rts       
  ;=========================================================
  ;=========================================================
 
 


  ;=========================================
  ;=========================================
  ;��������
ioset:
  ; ���ø�I/O��״̬����:
    lda      #%00100100         ;
    sta      p0mh;P0CONH
   ;lda      #%11101010         ; 
   ;sta      p0mL;P0CONL 
    clr      p0mL;P0CONL        ;��������������
    clr      p0int              ;=p0pnd
    lda      #%00001001         ;P1.0����,������������
    sta      p1m
    lda      #%01001010         ; 
    sta      p2mh;P2CONH
    lda      #%10101001
    sta      p2mL;P2CONL
 ;���ø�I/O�ڳ�ʼ��ѹ
  out_off:
    bclr     raly
    bset     tao_p
    bset     qian_p
    bset     hou_p
    bset     zuo_p
    bset     you_p
    bset     nj_p
    rts
  ;-----------------------------------------------------------
  ;��ʱ������:
time_stp:
    clr      pwmm  
    lda      #62         ;2us*62=125us
    sta      t0data       ;�Ƚ�ֵ 
    lda      #%10001000
    sta      t0m

   ;t0m=t0con    t0������
   ;��Ƶ bit7   bit6    ,��ϵͳʱ�ӷ�
   ;      0       0     ,fcpu/4096
   ;      0       1     ,fcpu/256
   ;      1       0     ,fcpu/8
   ;      1       1     ,fcpu/1
   ;bit5  bit4   ��
   ;t0m,3: t0clr          ;=1��t0c(t0cnt)
   ;bit2         �� 
   ;t0m,1: t0ie    ;=1,t0�жϿ�
   ;t0m,0: t0f     ;t0�жϱ�־λ

    cli
    bset     t0ie
    rts
  ;--------------------------------------------
  ;��ʱ�ж�
int_time:
    brset    t0f,int_ok 
    jmp      int_timeout
int_ok:    
    bclr     t0f;0,T0CON
    inc      led_125us 
    inc      t_125us
    inc      pcs_ykup125us

    lda      yk_125us
    cmp      #255
    bhs      int_buzzmain
    inc      yk_125us
int_buzzmain:
    brset    buzz_on,int_buz00
    jmp      buz_low 
int_buz00:    
    brset    1,t_125us,int_buzz01
    jmp      buz_low    ; .0
int_buzz01:
    bset     buzz;temp_buzz            ;256--500us��,0--255us�� 
    jmp      int_time1
buz_low:
    bclr     buzz;temp_buzz

int_time1:
    brset    3,t_125us,int_time10         ;125us*8=1mS  
    jmp      int_cut;int_timeout
int_time10:    
    clr      t_125us 
    inc      t_1ms
  
    lda      t_1ms
    cmp      #250
    bhs      int_xt250ms      
    jmp      int_cut                     ;����250ms
int_xt250ms:
    clr      t_1ms
    inc      t_250ms  
 ;-----------------------------------------------------------
 ;������:
  int_cut:
      brclr        guoling,gl_lowmain
      jmp          gl_higmain         ;�����
  ;�����
  gl_lowmain:
      lda          pcs_gl_low 
      cmp          #255
      blo          gl_l00
      jmp          intgl_err
  gl_l00:
      inc          pcs_gl_low 
      lda          pcs_gl_low
      cmp          #02
      beq          gl_l01
      jmp          gl_l02
  gl_l01:
      bclr         gl_err             ;�������
      clr          pcs_gler
  gl_l02:
      bhs          gl_l03
      jmp          int_gui
  gl_l03:
      clr          pcs_gl_hig 
      tst          pcs_watt125us
      bne          gl_l04
      jmp          int_gui
  gl_l04:
      dec          pcs_watt125us
      jmp          int_gui


  intgl_err:
      inc          pcs_gler
      brset        3,pcs_gler,gl_err00
      jmp          int_gui
  gl_err00:
      bset         gl_err             ;�������
      clr          pcs_gler
      jmp          int_gui

  gl_higmain:     ;�����: 
      lda          pcs_gl_hig
      cmp          #255
      blo          gl_h00
      jmp          intgl_err
  gl_h00:
      inc          pcs_gl_hig
      lda          pcs_gl_hig
      cmp          #02
      beq          gl_h01
      jmp          gl_h02
  gl_h01:
      bclr         gl_err             ;�������
      clr          pcs_gler         
  gl_h02:
      bhs          gl_h03
      jmp          int_gui
  gl_h03:
      lda          pcs_gl_low
      cmp          #55
      bhs          gl_h04
      jmp          int_gui            ;SET
  gl_h04:
      clr          pcs_gl_low
      lda          #80
      sta          pcs_watt125us         
  ;���---------------------------------------------------------------
  int_gui:
      brclr        gl_err,int_guiqian    ;�������
      jmp          int_alloff          ;����ͨ 
 ;ǰ:
  int_guiqian:
      lda          pcs_watt125us
      cmp          buf_outqian
      bhs          int_guiqian00
      jmp          int_guiqian01
  int_guiqian00:
      bset         qian_p
      jmp          int_guihou          ;
  int_guiqian01:                       ;�趨������                 
      bclr         qian_p              ;ʵ�ʿ�ȵ����趨���,��ͨ
 ;��:
  int_guihou:
      lda          pcs_watt125us
      cmp          buf_outhou
      bhs          int_guihou00
      jmp          int_guihou01
  int_guihou00:
      bset         hou_p
      jmp          int_guizuo          ;
  int_guihou01:                       ;�趨������                 
      bclr         hou_p              ;ʵ�ʿ�ȵ����趨���,��ͨ 
 ;��:
  int_guizuo:
      lda          pcs_watt125us
      cmp          buf_outzuo
      bhs          int_guizuo00
      jmp          int_guizuo01
  int_guizuo00:
      bset         zuo_p
      jmp          int_guiyou          ;
  int_guizuo01:                       ;�趨������                 
      bclr         zuo_p              ;ʵ�ʿ�ȵ����趨���,��ͨ 
 ;��:
  int_guiyou:
      lda          pcs_watt125us
      cmp          buf_outyou
      bhs          int_guiyou00
      jmp          int_guiyou01
  int_guiyou00:
      bset         you_p
      jmp          int_guitao          ;
  int_guiyou01:                       ;�趨������                 
      bclr         you_p              ;ʵ�ʿ�ȵ����趨���,��ͨ 

 ;��¯:
  int_guitao:
      lda          pcs_watt125us
      cmp          buf_outtime
      blo          int_guitao01
      jmp          int_mortstop       ;�����ǰ����(�ݼ���ʽ)
  int_guitao01:                                                           ;�趨������                 
      bclr         tao_p              ;ʵ�ʿ�ȵ����趨���,��ͨ
      jmp          int_timeout
  ;--------------------------------------------
  int_alloff:
      bset         qian_p
      bset         hou_p
      bset         zuo_p
      bset         you_p
  int_mortstop:
      bset         tao_p              ;�ر�
  int_timeout:
    rti  
  ;===========================================  
  ;===========================================  



  
 

  ;==========================================================
  ;==========================================================  
  ;      -----------------                    ----------
  ;     ��               ��       ��---------��         ����1bit
  ;-----                  -------- 
  ;       25msͷ��        312us    ��1��0
  yk_main:
      brset         yk_int,yk_higmain
      jmp           yk_upmain
  yk_higmain:
      clr           pcs_ykup125us
      clr           pcs_ykup
      clr           yk_125us
      brset         power_on,yk_higmain00
      rts                                   ;��·����
  yk_higmain00:
      clr           led_125us
      brclr         yk_ok,yk_higmain01
      rts                                   ;�Ѿ�����
  yk_higmain01:
      brset         yk_int,yk_higmain02
      jmp           yk_higp                 ;ͷ���ж�
  yk_higmain02:
      lda           yk_125us
      cmp           #255                    ;30ms
      bhs           yk_errmain
      jmp           yk_higmain01
  yk_errmain:
      bclr          power_on
  ykint_ret:
      clr           pcs_ykup125us
      rts


  ;ͷ���ж�
  yk_higp:
      lda           yk_125us
      cmp           #24                    ;3msyishang
      blo           yk_higp00
      jmp           yk_bufintmain          ;���ݽ���
  yk_higp00:
      rts                                  ;������ͷ��

  ;���ݽ��� :
  yk_bufintmain:
      clr           yk_125us
      clr           yk_num3
      clr           tm1629b_bit

  yk_bufintmain00:
  ;��1bit:   ;210
      lda           #210            ; 
      jsr           despaly_main         

  ;λ��ȡ:
      bclr          0,yk_num3
      brclr         yk_int,rade_1bitok
      bset          0,yk_num3
  rade_1bitok:  ;170
      lda           #150
      jsr           despaly_main                 
     
      inc           tm1629b_bit
      lda           tm1629b_bit
      cmp           #24             ;8bit*3=24
      blo           bitok_8p
      jmp           ykbuf_okmain    ;3������,24bit
  bitok_8p:
      cmp           #08
      bne           bitok_16p         
      jmp           ykbuf_8bit      ;1������,8bit
  bitok_16p:
      cmp           #16
      bne           bitint_inc         
      jmp           ykbuf_16bit     ;2������,16bit
  bitint_inc:
      lsl           yk_num3         ;�߼�����
      clr           yk_125us
  ;�ȵ͵�ƽ:
  bitwat_low:
      brset         yk_int,bitwat_low00
      jmp           yk_bufintmain00 ;��������
  bitwat_low00:
      lda           yk_125us
      cmp           #16             ;2ms
      bhs           bitwat_low01
      jmp           bitwat_low
  bitwat_low01:
      clr           led_125us
      jmp           ykint_ret       ;���ճ���,����һ��ͷ��

  ;�ɹ�1������,8bit
  ykbuf_8bit:
      lda           yk_num3
      sta           yk_num1
      jmp           ykbuf_okcl
  ;�ɹ�2������,16bit
  ykbuf_16bit:
      lda           yk_num3
      sta           yk_num2
  ykbuf_okcl:
      clr           yk_125us
      jmp           bitwat_low

  ;����ȫ���ɹ�,24bit
  ykbuf_okmain:
      bset          yk_ok                ;���պϸ�  
      lda           yk_num1
      and           #%00001111
      sta           buf_hou

      lda           yk_num2
      and           #%00001111
      sta           buf_you

      lda           yk_num3
      and           #%00001111
      sta           buf_nj

      lda           #04
  ykbuf_lsr:                   ;����
      lsr           yk_num1
      lsr           yk_num2
      lsr           yk_num3
      deca
      beq           ykbuf_lsr00 
      jmp           ykbuf_lsr
    ykbuf_lsr00:
      lda           yk_num1
      sta           buf_qian
      lda           yk_num2
      sta           buf_zuo
      lda           yk_num3
      sta           buf_tao

      clr           pcs_ykup125us
      bset          7,led_125us

      bset          0,num_buzz

      rts
 


yk_upmain:
      lda           pcs_ykup125us
      cmp           #40           ;5ms         
      blo           yk_upmain00
      bset          power_on      ;����
yk_upmain00:
      lda           pcs_ykup
      cmp           #5            ;8ms*=600MS,�����ٽ���
      blo           yk_upmain01
      bclr          yk_ok
yk_upmain01:
      rts   
  ;==========================================================
  ;========================================================== 
   












  ;===========================================      
  ;=========================================== 
  table_watt:
     fcb    $00     ;0��
     fcb    $15,$18,$19,$1B,$1D,$1E,$1F,$20,$21,$23      ;01,02,03,04,05,06,07,08,09,10
     fcb    36,36,37,38,39,39,40,41,42,42      ;11,12,13,14,15,16,17,18,19,20
     fcb    42,43,44,44,45,46,46,47,48,48      ;21,22,23,24,25,26,27,28,29,30
     fcb    49,50,51,51,52,52,53,54,55,56      ;31,32,33,34,35,36,37,38,39,40
     fcb    56,57,58,59,59,60,61,62,63,64      ;41,42,43,44,45,46,47,48,49,50
     fcb    65,66,67,68,70,73,74,76,79,85,85   ;51,52,53,54,55,56,57,58,59,60 
     
     
     
     
     ;ʮ����
     ;fcb    #21,#24,25,27,29,30,31,32,33,35      ;01,02,03,04,05,06,07,08,09,10
     ;fcb    36,36,37,38,39,39,40,41,42,42      ;11,12,13,14,15,16,17,18,19,20
     ;fcb    42,43,44,44,45,46,46,47,48,48      ;21,22,23,24,25,26,27,28,29,30
     ;fcb    49,50,51,51,52,52,53,54,55,56      ;31,32,33,34,35,36,37,38,39,40
     ;fcb    56,57,58,59,59,60,61,62,63,64      ;41,42,43,44,45,46,47,48,49,50
     ;fcb    65,66,67,68,70,73,74,76,79,85,85   ;51,52,53,54,55,56,57,58,59,60


  ;==========================================================
  ;==========================================================
  



  

  ;============================================
    ORG    $1ff1
    jmp    main
    ;interrupt vector
;    ORG    $1ff4
;    FDB    int1
    ORG    $1ff6
    FDB    int_time;t0int
;   ORG    $1ff8
;   FDB    pwmint
;   ORG    $1ffa
;   FDB    int0
;   ORG    $1ffc
;   FDB    swi
    ORG    $1ffe
    FDB    star


