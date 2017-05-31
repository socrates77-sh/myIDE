include mc20p24b_ist.asm
;$0000-$002F:Control registers
T0CNT    EQU    $00    ;timer0
T0DATA    EQU    $01
T0CON    EQU    $02
MCR    EQU    $03
BTCON    EQU    $0c    ;basic time
BTCLR    equ    1
BTCNT    EQU    $0d
P0    EQU    $10    ;port0-2
P1    EQU    $11
P2    EQU    $12
P0CONH    EQU    $16
P0CONL    EQU    $17
P0PND    EQU    $18
P1CON    EQU    $19
P2CONH    EQU    $1a
P2CONL    EQU    $1b
PWMDATA    EQU    $22    ;PWM
PWMCON    EQU    $23
ADCON    EQU    $27    ;A/D
ADDATAH    EQU    $28
ADDATAL    EQU    $29

;;;;;;;;;P0
P0_keys        define    0,P0
P0_ntc_senser    define    1,P0
P0_164data    define    2,P0
P0_164clk    define    3,P0
P0_com1        define    4,P0
P0_com2        define    5,P0
P0_ledcom    define    6,P0
P0_relay_motor    define    7,P0

;;;;;;;;;P1

XIN_4M        define    0,P1
XOUT_4M        define    1,P1
P12_inputequ    define    2,P1

;;;;;;;;;P2

P2_2k_buz        define    0,P2
P2_2k_buzv        equ    #%00000001
P2_triac_o3        define    1,P2
P2_relay_heater        define    2,P2
P2_triac_wpump        define    3,P2
P2_triac_gpump        define    4,P2
P2_triac_ir        define    5,P2
P2_triac_vibrate    define    6,P2


;------------------常量定义-------------
KEYS_AD        equ    #%00000110
TEMP_AD        equ    #%00010110



SWITCH_TEMP    EQU    #%00001000
C_K_LB        EQU    #10;5;8    ;键去抖次数
AVERAGE_CNT    EQU    #128    ;

COM1_VALID    EQU    #%00000111    ;low valid
COM2_VALID    EQU    #%00001101
COM3_VALID    EQU    #%00001011
COM4_VALID    EQU    #%00001110
LED_COM_VALID    EQU    #%00001111    ;high valid


Level_1    EQU    70;85;80        ;70-120
Level_2    EQU    100;123;130        ;100-160
Level_3    EQU    140;168;156;160        ;140-180
Level_4    EQU    160;177;168;180        ;160-200
Level_5    EQU    180;177;168;180        ;180-230

Level_1_H    EQU    120;85;80        ;70-120
Level_2_H    EQU    160;123;130        ;100-160
Level_3_H    EQU    180;168;156;160        ;140-180
Level_4_H    EQU    200;177;168;180        ;160-200
Level_5_H    EQU    230;177;168;180        ;180-230

SCREEN_TIME1    EQU    6;3;6
SCREEN_TIME2    EQU    6;3;6
SCREEN_TIME3    EQU    6;3;6
SCREEN_TIME4    EQU    5;2;3;6
SCREEN_TIME5    EQU    5;2;3;6


WINK_COUNTER1    EQU    20;10;200    ;设定主循环周期为5ms，闪烁频率1s
WINK_COUNTER2    EQU    20;10    ;设定主循环周期为5ms，闪烁频率1s

DEVICECODE_w    EQU    %00000000

BUZ_TIME_H    EQU    #$ff;60;30
BUZ_TIME_L    EQU    #255;200

;------------------变量定义-------------
;$0030-$00FF:RAM
Temp_P0        equ    $30    ;port backup
counting_time    equ    $31

;键滤波
R_KEY1        EQU    $32    ;滤波键值接口
key1    equ    0
key2    equ    1
key3    equ    2
key4    equ    3
key5    equ    4
key6    equ    5
key7    equ    6
key8    equ    7

key_add        equ    key2
key_sub        equ    key7
key_heat    equ    key8
key_time    equ    key6
key_onoff    equ    key3
key_o3        equ    key5
key_vibrate    equ    key4
key_ir        equ    key1





R_KEY1_BAK    EQU    $33    ;
R_KEY1_TRU    EQU    $34    ;
R_K_LB        EQU    $35    ;

F1        EQU    $36    
F1_ir_mode    EQU    0    ;红外模式
F1_ir_modev    equ    #%00000001
F1_detect_ad    EQU    1    ;
F1_Button    EQU    2
F1_onoff    EQU    3    ;开关
F1_onoffv    equ    #%00001000    ;
F1_heat_mode    EQU    4    ;加热模式
F1_heat_modev    equ    #%00010000
F1_time_mode    EQU    5    ;时间模式
F1_o3_mode    EQU    6    ;臭氧模式
F1_vibrate_mode    EQU    7    ;振动模式
F1_vibrate_modev    equ    #%10000000

F2        EQU    $37
F2_4s_wink    EQU    0    ;持续4s闪烁，1为亮，0为灭
F2_4s_winkv    equ    #%00000001
F2_BUZ_EN    EQU    1    ;
F2_BUZ_BX    EQU    2    ;
F2_BUZ_Forever    EQU    3    ;
F2_steam_mode    EQU    4    ;热浪模式
F2_4s        EQU    5    ;持续4s闪烁，1为闪烁状态，0为不闪烁
F2_timer_mode    EQU    6    ;计时模式
F2_4s_relay    EQU    7    ;
;;;;;;;;;!!!!!!!!!!!!!!!!!!!!!

Timer_125us    EQU    $38    ;计时器
Timer_250us    EQU    $39    ;计时器
Timer_2500us    EQU    $3A    ;计时器

Timer_500ms    EQU    $3B    ;计时器
Timer_500ms_wink    EQU    $3C    ;计时器

Timer_1min    EQU    $3D    ;计时器
Timer_500ms_relay    EQU    $3E    ;档位,0(OFF)-1(ON)-2(ON)-3(ON)-4(ON)
;        EQU    $3F    ;P2缓存
Buffer_Temper_H    EQU    $40    ;温度缓存
Buffer_Temper_L    EQU    $41    

data164        EQU    $42
ad1datah_temp    EQU    $43
ad1datal_temp    EQU    $44
ad1_cnt        EQU    $45

AD_Cnt        EQU    $46
act_temperature    EQU    $47
set_temperature    EQU    $48

cnt_temp    EQU    $49
;    EQU    $4A

Temp        EQU    $4B
Temp1        EQU    $4C
Temp2        EQU    $4D
Temp_FLAGS    EQU    $4E
Timer_5ms_I2C    EQU    $4F
Old_Act_Temper    EQU    $50
SeveralTimer_125us    EQU    $51    ;屏蔽relay抖动计数器
SeveralTimer_5ms    EQU    $52    ;屏蔽relay抖动计数器
SeveralTimer_1s    EQU    $53    ;屏蔽relay抖动计数器
Timer_5ms_I2C_L    EQU    $54
Buzzer_H_Timer    EQU    $55    ;蜂鸣计时器
Buzzer_L_Timer    EQU    $56    ;蜂鸣计时器
Buzzer_Counter    EQU    $57    ;鸣响次数

F3    EQU    $58
F3_trouble_circuit    equ    0    ;探头故障标志，1故障，0正常
F3_4s_heat    equ    1
F3_set_bb_clr    equ    2

Temp_Act_Temperature    equ    $59

com_cnter    EQU    $60
com1_data    EQU    $61
com2_data    EQU    $62
;    EQU    $63
;    EQU    $64
led_com_data    EQU    $65
led6_r    equ    7
led6_y    equ    6
led5    equ    5
led7    equ    4
led3    equ    2
led1    equ    1
led2    equ    0

led_vibrate    define    led2,led_com_data
led_heattemp    define    led1,led_com_data
led_ir        define    led3,led_com_data
led_steam    define    led6_r,led_com_data
led_o3        define    led6_y,led_com_data
led_keeptemp    define    led5,led_com_data
led_heat    define    led7,led_com_data


reg_min        EQU    $66    ;分钟
reg_sec        EQU    $67    ;秒钟
Led_L        EQU    $68
Led_H        EQU    $69

R_KEY2        EQU    $6A    ;滤波键值接口
R_KEY2_BAK    EQU    $6B    ;键值备份单元
R_KEY2_TRU    EQU    $6C    ;滤波后的真值


;$1000-$1FFF:OTP ROM
    ORG $1000
reset:
    sei
    rsp

 lda #$aa
 sta $01
 lda #$55
 sta $02    
 sta $03
 sta $10
 sta $13

    lda    #%10100010    ;dis WDT    ;cnt WDT    Fsys/4096    overflow time=4096*256*2/Fosc
    sta    BTCON
    lda    #%00000000    ;Timer disable
    sta    T0CON
    lda    #$FF
    sta    T0DATA
    lda    #$01        ;EN LVR
    sta    MCR
    lda    #%00001000    ;PWM output low
    sta    PWMCON
    lda    #$FF
    sta    PWMDATA
    
    lda    #%10101010
    sta    P0CONH
    lda    #%10101111
    sta    P0CONL
    clr    P0
    
    lda    #%01001010
    sta    P2CONH
    lda    #%10101010
    sta    P2CONL
    lda    #%01111010
    sta    P2
    
    lda    #$00        ;INT disable
    sta    P0PND
    lda    #$00        ;port1 use outside OSC
    sta    P1CON
    clr    P1
    lda    #%00010000    ;ADC1 enable
    sta    ADCON
    
    

ram_clear:            ;clear all ram
    ldx    #208
ram_clear1:
    clr    $2F,X
    decx
    bne    ram_clear1
    
    
    
    lda    #250        ;Timer enable
    sta    T0DATA
    lda    #%11001010    ;使能计数器中断，1分频，250*2/4=125us    ;10001010，8分频,250*8*2/4=1000us
    sta    T0CON
    
    cli
    jsr    b1
    lda    #40
    sta    set_temperature
    
;;;;;;;;;***************************************************
;;;;;;;;;***************************************************
;;;;;;;;;***************************************************



main:
    bset    BTCLR,BTCON    ;CLR WDT
    
    jsr    check_button
    jsr    timing_deal
    jsr    ad_detect
    jsr    K_LB
    jsr    buzzer_deal
    jsr    display_led
    
    jmp    main
    
    
    
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;计时;;;;;;;;;;;;;;;;;;;;;;;;;;
timing_deal:
    lda    Timer_2500us
    cmp    #200
    blo    end_timing_deal
    
    brclr    F2_4s_relay,F2,end_4s_relay
    inc    Timer_500ms_relay
    lda    Timer_500ms_relay
    cmp    #8
    blo    end_4s_relay
    bclr    F2_4s_relay,F2
    bset    F3_4s_heat,F3
end_4s_relay:
    
    
    brclr    F2_4s,F2,end_4s_wink
    lda    F2_4s_winkv    ;闪烁
    eor    F2
    sta    F2
    inc    Timer_500ms_wink
    lda    Timer_500ms_wink
    cmp    #8
    blo    end_4s_wink
    bclr    F2_4s,F2
    lda    counting_time
    beq    end_4s_wink
    brclr    F1_time_mode,F1,end_4s_wink
    bclr    F1_time_mode,F1
    bset    F2_timer_mode,F2
    clr    Timer_250us    ;计时开始
    clr    Timer_2500us
    clr    Timer_500ms
    clr    Timer_1min
    rts
end_4s_wink:    
    
loop_timing_deal:
    clr    Timer_2500us    ;500ms
    
    inc    Timer_500ms
    lda    Timer_500ms
    cmp    #120
    blo    end_timing_deal
    
    clr    Timer_500ms    ;1min
    brclr    F1_onoff,F1,end_timing_deal
    brclr    F2_timer_mode,F2,end_timing_deal
    inc    Timer_1min
    lda    Timer_1min
    cmp    counting_time
    blo    end_timing_deal
    bclr    F2_timer_mode,F2
    bclr    F1_onoff,F1    ;定时关机
    jsr    b1
end_timing_deal:
    rts
    
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;LED显示;;;;;;;;;;;;;;;;;;;;;;;;;;
display_led:
    lda    #%01001010
    sta    P2CONH
    lda    #%10101010
    sta    P2CONL
    
    brclr    F1_onoff,F1,off_disp
    brset    F3_trouble_circuit,F3,error_disp
    brset    F1_time_mode,F1,time_disp
    brclr    F1_heat_mode,F1,temp_disp
    brset    F2_4s,F2,settemp_disp
    
temp_disp:
    lda    act_temperature
    jmp    deal_bcd
    
error_disp:
    lda    #11        ;E0错误代码
    sta    com1_data
    clr    com2_data
    jmp    end_deal_bcd
    
settemp_disp:    
    brset    F2_4s_wink,F2,loop_settemp_disp
    lda    #10    ;不显示
    sta    com1_data
    sta    com2_data
    jmp    end_deal_bcd
loop_settemp_disp:
    lda    set_temperature
    jmp    deal_bcd
    
off_disp:            ;关机
    lda    #10    ;不显示
    sta    com1_data
    sta    com2_data
    jmp    end_deal_bcd
    
time_disp:
    lda    counting_time
    beq    temp_disp
    
    brset    F2_4s_wink,F2,loop_time_disp
    lda    #10    ;不显示
    sta    com1_data
    sta    com2_data
    jmp    end_deal_bcd
loop_time_disp:

deal_bcd:
    jsr    hextobcd
    lda    Led_L
    sta    com2_data
    lda    Led_H
    sta    com1_data
end_deal_bcd:
    
    
status_set:
    brset    F3_trouble_circuit,F3,loop_trouble
    
    brclr    F1_onoff,F1,loop_offdisp
    
    brset    F1_heat_mode,F1,loop_heatdisp
    bclr    led_heat
    bclr    led_heattemp
    bclr    led_keeptemp
    bclr    P2_relay_heater
    bset    P2_triac_wpump
    jmp    end_heatdisp
    
loop_trouble:    
    clr    led_com_data
    
    bclr    P0_relay_motor
    bclr    P2_relay_heater
    bset    P2_triac_o3
    bset    P2_triac_wpump
    bset    P2_triac_gpump
    bset    P2_triac_ir
    bset    P2_triac_vibrate
    rts
    
loop_offdisp:    
    clr    led_com_data        ;关机
    
    bclr    P0_relay_motor
    bclr    P2_relay_heater
    bset    P2_triac_o3
    bset    P2_triac_wpump
    bset    P2_triac_gpump
    bset    P2_triac_ir
    bset    P2_triac_vibrate
    
    bclr    F1_ir_mode,F1
    bclr    F1_heat_mode,F1
    bclr    F1_time_mode,F1
    bclr    F1_o3_mode,F1
    bclr    F1_vibrate_mode,F1
    bclr    F2_4s_wink,F2
    bclr    F2_steam_mode,F2
    bclr    F2_4s,F2
    rts
    
    
loop_heatdisp:
    bset    led_heat
    brclr    F3_4s_heat,F3,end_heatdisp
    bclr    P2_triac_wpump    ;开水泵
    lda    act_temperature
    cmp    set_temperature
    blo    loop1_heatdisp
    beq    loop2_heatdisp
    bclr    led_heattemp    ;保温
    bset    led_keeptemp
    bclr    P2_relay_heater
    jmp    loop2_heatdisp
loop1_heatdisp:
    bclr    led_keeptemp    ;加热
    bset    led_heattemp
    bset    P2_relay_heater
loop2_heatdisp:
end_heatdisp:

    brset    F1_ir_mode,F1,loop_irdisp
    bset    P2_triac_ir
    bset    P2_triac_vibrate
    bclr    led_ir
    jmp    end_irdisp
loop_irdisp:
    bclr    P2_triac_ir
    bclr    P2_triac_vibrate
    bset    led_ir
end_irdisp:

    brset    F1_vibrate_mode,F1,loop_vibratedisp
    bclr    P0_relay_motor
    bclr    led_vibrate
    jmp    end_vibratedisp
loop_vibratedisp:
    bset    P0_relay_motor
    bset    led_vibrate
end_vibratedisp:
    
    brset    F1_o3_mode,F1,loop_o3disp
    brset    F2_steam_mode,F2,loop_steamdisp
    bset    P2_triac_o3
    bset    P2_triac_gpump
    bclr    led_steam
    bclr    led_o3
    jmp    end_o3disp
loop_steamdisp:
    bset    P2_triac_o3        ;气浪模式
    bclr    P2_triac_gpump
    bset    led_steam
    bclr    led_o3
    jmp    end_o3disp
loop_o3disp:
    bclr    P2_triac_o3        ;臭氧模式
    bset    P2_triac_gpump
    bset    led_o3
    bclr    led_steam
end_o3disp:
    jmp    end_disp
    
    
    
end_disp:    
    rts
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;164传送;;;;;;;;;;;;;;;;;;;;;;;;;;;;
transfer_164:
    sta    data164
    bclr    P0_164data
    bclr    P0_164clk
    ldx    #8
loop3_164:
    brset    0,data164,loop1_164
    bclr    P0_164data
    jmp    loop2_164
loop1_164:
    bset    P0_164data
loop2_164:
    bset    P0_164clk
    bclr    P0_164clk
    lsr    data164
    decx
    bne    loop3_164
    rts
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;键处理;;;;;;;;;;;;;;;;;;;;;;;;;;
check_button:                
    brclr    F1_Button,F1,nonpress

press:
    lda    R_KEY1_TRU
    beq    loose
    jmp    end_check_button
loose:
    bclr    F1_Button,F1
    jmp    end_check_button
    
nonpress:
    lda    R_KEY1_TRU
    bne    anjian
    jmp    end_check_button
    
    
    ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;anjian
anjian:    
    bset    F1_Button,F1
    
    brset    key_onoff,R_KEY1_TRU,loop_anjian_onoff
    brset    F3_trouble_circuit,F3,loop0_trouble
    brset    F1_onoff,F1,loop1_end_button
loop0_trouble:
    jmp    end_check_button
loop1_end_button:    
    brset    key_heat,R_KEY1_TRU,loop_anjian_heat
    brclr    key_time,R_KEY1_TRU,loop0_anjian_time
    jmp    loop_anjian_time
loop0_anjian_time:
    brclr    key_o3,R_KEY1_TRU,loop0_anjian_o3
    jmp    loop_anjian_o3
loop0_anjian_o3:
    brclr    key_vibrate,R_KEY1_TRU,loop0_anjian_vibrate
    jmp    loop_anjian_vibrate
loop0_anjian_vibrate:
    brclr    key_ir,R_KEY1_TRU,loop1_anjian_ir
    jmp    loop_anjian_ir
loop1_anjian_ir:
    brset    F1_heat_mode,F1,loop2_end_button
    jmp    end_check_button
loop2_end_button:
    jsr    b1
    brset    key_add,R_KEY1_TRU,loop_anjian_add
    brset    key_sub,R_KEY1_TRU,loop_anjian_sub
    jmp    end_check_button
    
loop_anjian_onoff:            ;开关按键
    lda    F1_onoffv
    eor    F1
    sta    F1
    jsr    b1
    lda    #10
    sta    counting_time
    jmp    end_check_button
    
loop_anjian_add:            ;加温度按键
    brclr    F2_4s,F2,loop1_anjian_add
    brset    F1_time_mode,F1,loop1_anjian_add
    
    inc    set_temperature
    lda    set_temperature
    cmp    #50
    blo    loop1_anjian_add
    lda    #50
    sta    set_temperature
loop1_anjian_add:
    bclr    F1_time_mode,F1
    bset    F2_4s,F2
    clr    Timer_2500us
    clr    Timer_500ms_wink
    bset    F2_4s_wink,F2
    jmp    end_check_button
    
loop_anjian_sub:            ;减温度按键
    brclr    F2_4s,F2,loop1_anjian_sub
    brset    F1_time_mode,F1,loop1_anjian_sub
    
    dec    set_temperature
    lda    set_temperature
    cmp    #35
    bhs    loop1_anjian_sub
    lda    #35
    sta    set_temperature
loop1_anjian_sub:
    bclr    F1_time_mode,F1
    bset    F2_4s,F2
    clr    Timer_2500us
    clr    Timer_500ms_wink
    bset    F2_4s_wink,F2
    jmp    end_check_button
    
loop_anjian_heat:            ;加热冲浪按键
    lda    F1_heat_modev
    eor    F1
    sta    F1
    brclr    F1_heat_mode,F1,loop1_heat
    clr    Timer_500ms_relay
    bset    F2_4s_relay,F2
    bclr    F3_4s_heat,F3
    jmp    loop2_heat
loop1_heat:
    bset    F3_4s_heat,F3
    bclr    F2_4s_relay,F2
loop2_heat:
    
    jsr    b1
    brset    F1_time_mode,F1,end_check_button
    bclr    F2_4s,F2
    jmp    end_check_button
    
loop_anjian_time:            ;定时按键
    brclr    F1_time_mode,F1,loop1_anjian_time
    
    lda    #10
    add    counting_time
    cmp    #60
    bls    end_anjian_time
    lda    #0
end_anjian_time:
    sta    counting_time
loop1_anjian_time:
    bset    F1_time_mode,F1
    jsr    b1
    bset    F2_4s,F2
    clr    Timer_2500us
    clr    Timer_500ms_wink
    bset    F2_4s_wink,F2
;    lda    counting_time
;    bne    end_check_button
;    bclr    F1_time_mode,F1
;    bclr    F2_4s,F2
;    
    jmp    end_check_button
    
end_check_button:    
    rts
    
loop_anjian_o3:                ;气浪臭氧按键
    brset    F2_steam_mode,F2,loop1_anjian_o3
    brset    F1_o3_mode,F1,loop2_anjian_o3
    bset    F2_steam_mode,F2
    bclr    F1_o3_mode,F1
    jmp    end_anjian_o3
loop1_anjian_o3:
    bclr    F2_steam_mode,F2
    bset    F1_o3_mode,F1
    jmp    end_anjian_o3
loop2_anjian_o3:
    bclr    F2_steam_mode,F2
    bclr    F1_o3_mode,F1
    jmp    end_anjian_o3
end_anjian_o3:    
    jsr    b1
    jmp    end_check_button
    
loop_anjian_vibrate:            ;振动按键
    lda    F1_vibrate_modev
    eor    F1
    sta    F1
    jsr    b1
    jmp    end_check_button
    
loop_anjian_ir:                ;红外按键
    lda    F1_ir_modev
    eor    F1
    sta    F1
    jsr    b1
    jmp    end_check_button
    
    
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;ad检测;;;;;;;;;;;;;;;;;;;;;;;;
ad_detect:
    brset    F1_detect_ad,F1,ad_detect_temp
    
ad_detect_keys:
    bset    0,ADCON        ;**************start A/D convertion***********
AD_waiting0:
    brclr    3,ADCON,AD_waiting0    ;判转换完毕否,为1转换完毕
    
average_ad0_divide:
    lda    ADDATAH
    jsr    keys_detect
    bset    F1_detect_ad,F1
    lda    TEMP_AD
    sta    ADCON
    rts
    
keys_detect:
    cmp    #23
    bhs    loop1_keydetect
    clr    R_KEY1
    bset    key2,R_KEY1
    rts
loop1_keydetect:
    cmp    #58
    bhs    loop2_keydetect
    clr    R_KEY1
    bset    key7,R_KEY1
    rts
loop2_keydetect:
    cmp    #85
    bhs    loop3_keydetect
    clr    R_KEY1
    bset    key8,R_KEY1
    rts
loop3_keydetect:
    cmp    #105
    bhs    loop4_keydetect
    clr    R_KEY1
    bset    key6,R_KEY1
    rts
loop4_keydetect:
    cmp    #120
    bhs    loop5_keydetect
    clr    R_KEY1
    bset    key3,R_KEY1
    rts
loop5_keydetect:
    cmp    #133
    bhs    loop6_keydetect
    clr    R_KEY1
    bset    key5,R_KEY1
    rts
loop6_keydetect:
    cmp    #144
    bhs    loop7_keydetect
    clr    R_KEY1
    bset    key4,R_KEY1
    rts
loop7_keydetect:
    cmp    #153
    bhs    loop8_keydetect
    clr    R_KEY1
    bset    key1,R_KEY1
    rts
loop8_keydetect:
    clr    R_KEY1
    rts
    
    
ad_detect_temp:    
    
    inc    cnt_temp
    lda    cnt_temp
    cmp    #5
    bhs    loop1_detect_temp
    rts
loop1_detect_temp:
    clr    cnt_temp
    
    bset    0,ADCON        ;**************start A/D convertion***********
AD_waiting1:
    brclr    3,ADCON,AD_waiting1    ;判转换完毕否,为1转换完毕
    
average_ad1_divide:
    lda    ADDATAH
    add    ad1datal_temp
    sta    ad1datal_temp
    bhs    loop1_average1
    inc    ad1datah_temp
loop1_average1:
    inc    ad1_cnt
    lda    ad1_cnt
    cmp    AVERAGE_CNT
    blo    loop1_average2
    lda    #7
loop1_average3:
    lsr    ad1datah_temp
    ror    ad1datal_temp
    deca
    bne    loop1_average3
    
    lda    ad1datal_temp
    cmp    #20;10
    blo    temp_short_circuit
    cmp    #245;250
    bhs    temp_open_circuit
    
    bclr    F3_trouble_circuit,F3
    lda    ad1datal_temp
    cmp    #65
    blo    loop1_tab_temp
    cmp    #219
    bhs    loop3_tab_temp
    lda    ad1datal_temp
    sub    #65
    tax
loop2_tab_temp:    
    lda    tab_temp,x
    sta    act_temperature
    clr    ad1_cnt
    clr    ad1datal_temp
    clr    ad1datah_temp
loop1_average2:
    bclr    F1_detect_ad,F1
    lda    KEYS_AD
    sta    ADCON
    rts

loop3_tab_temp:
    ldx    #219
    jmp    loop2_tab_temp
loop1_tab_temp:
    ldx    #65
    jmp    loop2_tab_temp
    
temp_short_circuit:
    bset    F3_trouble_circuit,F3
    jmp    loop1_average2
temp_open_circuit:
    bset    F3_trouble_circuit,F3
    jmp    loop1_average2
    rts

    
    
    
;;---------------------------------------------------
;    
;DELAY_4500us:
;    lda    #10
;    sta    Temp1
;    
;LLLOOP_2:
;    lda    #140
;    sta    Temp2
;LLLOOP_1:
;    dec    Temp2
;    bne    LLLOOP_1
;    
;    dec    Temp1
;    bne    LLLOOP_2
;    
;    rts
;    
;;～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～延时
;
;DELAY_20us:
;    lda    #10
;LLLOOP_3:
;    deca
;    bne    LLLOOP_3
;    
;    rts
;    

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～单声鸣叫
    
b1:
    lda    BUZ_TIME_H
    sta    Buzzer_H_Timer
    sta    Buzzer_L_Timer
    bset    F2_BUZ_BX,F2
    bset    F2_BUZ_EN,F2
    bclr    F2_BUZ_Forever,F2        ;响1声
    lda    #1
    sta    Buzzer_Counter
    rts
    
;～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～多声鸣叫
    
b1_b1:
    lda    BUZ_TIME_H
    sta    Buzzer_H_Timer
    lda    BUZ_TIME_L
    sta    Buzzer_L_Timer
    bset    F2_BUZ_BX,F2
    bset    F2_BUZ_EN,F2
    bclr    F2_BUZ_Forever,F2        ;响6声
    lda    #4
    sta    Buzzer_Counter
    rts
    
;～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～多声鸣叫一直响
    
b1_b1_:
    lda    BUZ_TIME_H
    sta    Buzzer_H_Timer
    lda    BUZ_TIME_L
    sta    Buzzer_L_Timer
    bset    F2_BUZ_BX,F2
    bset    F2_BUZ_EN,F2
    bset    F2_BUZ_Forever,F2        ;一直响
    rts
    
;～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～蜂鸣处理

buzzer_deal:
    brclr    F2_BUZ_EN,F2,end_buz_deal
    brset    F2_BUZ_Forever,F2,loop_buz1
    lda    Buzzer_Counter
    cmp    #0
    beq    end_buz_deal1
    
loop_buz1:
    brclr    F2_BUZ_BX,F2,loop_buz
    dec    Buzzer_H_Timer            ;鸣响
    lda    Buzzer_H_Timer
    cmp    #0
    bne    end_buz_deal
    dec    Buzzer_Counter
    bclr    F2_BUZ_BX,F2
    lda    BUZ_TIME_H
    sta    Buzzer_H_Timer
    jmp    end_buz_deal
    
loop_buz:
    dec    Buzzer_L_Timer            ;不响
    lda    Buzzer_L_Timer
    cmp    #0
    bne    end_buz_deal
    bset    F2_BUZ_BX,F2
    lda    BUZ_TIME_L
    sta    Buzzer_L_Timer
    jmp    end_buz_deal
    
end_buz_deal:
    rts
    
end_buz_deal1:    
    bclr    F2_BUZ_EN,F2
    rts
    
    
    
        
;～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～～16进制转10进制

hextobcd:        
        sta    Led_L
        clr    Led_H
loop2_hexbcd:
        lda    Led_L
        sub    #10
        blo    loop1_hexbcd
        sta    Led_L
        inc    Led_H
        jmp    loop2_hexbcd
loop1_hexbcd:
;        lda    Led_H
;        sub    #10
;        blo    loop3_hexbcd
;        sta    Led_H
;loop3_hexbcd:
        rts
        
        
        
        
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;------------------键滤波------------------------
;;;;;;;;;
;该次键值与上次比较
;滤波完成否
;滤波OK的键值
;若不同上次键值,则保存该次值;初始化滤波次数
;;;;;;;;;
;BACKUP-->BAK-->备份
;TRUER--->真

;R_K_BAK------上次备份值
;R_KEY--------该次键值
;R_K_TRUER----校验后得到的值

;C_K_VER-----校验次数    
;R_K_VER-----次数寄存单元

K_LB:

    lda    R_KEY1            ;该次的值和上次备份的值进行比较
    cmp    R_KEY1_BAK
    bne    K_LB1
;    lda    R_KEY2
;    cmp    R_KEY2_BAK
;    bne    K_LB1
    
    
    dec    R_K_LB            ;-1不为0跳,滤波次数没完
    bne    RET_K_LB
    
    lda    R_KEY1_BAK        ;滤波后真正的键值
    sta    R_KEY1_TRU
;    lda    R_KEY2_BAK
;    sta    R_KEY2_TRU
    
    jmp    K_LB2
K_LB1:
    lda    R_KEY1            ;不同上次值时,把此次值保存起来
    sta    R_KEY1_BAK
;    lda    R_KEY2
;    sta    R_KEY2_BAK
    
K_LB2:
    lda    C_K_LB            ;初始化滤波次数
    sta    R_K_LB
RET_K_LB:
    rts
    
;------------------中断---------------- 125us一次

t0int:
    bclr    0,T0CON
    
    inc    Timer_125us
    lda    Timer_125us
    cmp    #2
    blo    end_250us
    clr    Timer_125us
    inc    Timer_250us
    
    brclr    F2_BUZ_EN,F2,end_250us
    
    lda    P2_2k_buzv
    eor    P2
    sta    P2
end_250us:
    
    lda    Timer_250us
    cmp    #10
    blo    end_2500ustime
    jmp    time_2500us
end_2500ustime:
    rti
    
time_2500us:
    clr    Timer_250us
    inc    Timer_2500us
    
    
    
    lda    #%10101010
    sta    P0CONH
    lda    #%10101111
    sta    P0CONL
    
    inc    com_cnter
    lda    com_cnter
    cmp    #1
    beq    disp_com1
    cmp    #2
    beq    disp_com2
    jmp    disp_led_com

disp_led_com:
    clr    com_cnter
    bclr    P0_com1
    bclr    P0_com2
    bclr    P0_ledcom
    lda    led_com_data
    jsr    transfer_164
    bset    P0_ledcom
    jmp    backup_ldadata
    
disp_com1:
    bclr    P0_com1
    bclr    P0_com2
    bclr    P0_ledcom
    ldx    com1_data
    lda    table_0_9,x
    jsr    transfer_164
    bset    P0_com1
    jmp    backup_ldadata
    
disp_com2:
    bclr    P0_com1
    bclr    P0_com2
    bclr    P0_ledcom
    ldx    com2_data
    lda    table_0_9,x
    jsr    transfer_164
    bset    P0_com2
    jmp    backup_ldadata
    
backup_ldadata:
    rti
    
    
    
    
tab_temp:
    fcb     0, 0, 0, 0, 1                ;65-69
    fcb     1, 2, 2, 3, 3, 3, 4, 4, 5, 5        ;70-79
    fcb     5, 6, 6, 7, 7, 8, 8, 8, 9, 9        ;80-89
    fcb    10,10,10,11,11,12,12,12,13,13        ;90-99
    fcb    14,14,14,15,15,16,16,16,17,17        ;100-109
    fcb    18,18,18,19,19,20,20,20,21,21        ;110-119
    fcb    21,22,22,23,23,23,24,24,25,25        ;120-129
    fcb    26,26,26,27,27,28,28,28,29,29        ;130-139
    fcb    30,30,30,31,31,32,32,33,33,33        ;140-149
    fcb    34,34,35,35,36,36,37,37,38,38        ;150-159
    fcb    38,39,39,40,40,41,41,42,42,43        ;160-169
    fcb    43,44,44,45,45,46,46,47,48,48        ;170-179
    fcb    49,49,50,50,51,52,52,53,53,54        ;180-189
    fcb    55,55,56,56,57,58,58,59,60,61        ;190-199
    fcb    61,62,63,64,64,65,66,67,68,68        ;200-209
    fcb    69,70,71,72,73,74,75,76,77,78        ;210-219
    
table_0_9:                ;b - a - g - f - . - d - e - c
        fcb    $D7    ;0
        fcb    $81    ;1
        fcb    $E6    ;2
        fcb    $E5    ;3
        fcb    $B1    ;4
        fcb    $75    ;5
        fcb    $77    ;6
        fcb    $C1    ;7
        fcb    $F7    ;8
        fcb    $F5    ;9
        fcb    $00    ;不显示
        fcb    $76    ;E
        
        
;---------------------------------------------------
    
    ORG    $1ff1
    jmp    main
    
    ;interrupt vector
;    ORG    $1ff4
;    FDB    int1
    ORG    $1ff6
    FDB    t0int
;    ORG    $1ff8
;    FDB    pwmint
;    ORG    $1ffa
;    FDB    int0
;    ORG    $1ffc
;    FDB    swi
    ORG    $1ffe
    FDB    reset

