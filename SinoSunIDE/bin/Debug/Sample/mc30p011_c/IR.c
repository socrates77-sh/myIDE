
#include <mc30p011.h>
#include "externVar.H"
#include "CONST.H"

void IRSbr4(void);

const  char  BlueTbl[ ] =  {255,255,14,252,247,38,255,174,214,35,214,117};
const  char  GreenTbl[ ] = {255,14,255,139,24,247,38,31,38,88,247,221};
const  char  RedTbl[ ] =   {14,255,255,26,160,227,0,174,49,227,39,174};


/*;红外接收子程序
;-----------------------------------------------------------------------
;20P02,IR为P01(NT1),定时器为125us中断
;红外线一开始发送一段13.5ms 的引导码，引导码由9ms的高电平和4.5ms的低电
;平组成，跟着引导码是客户码，客户反码，按键码，按键反码，如果按着键不放，
;则遥控器则发送一段重复码，重复码由9ms的高电平，2.25ms的低电平，跟着是
;一个短脉冲。
;引导码为13.5ms(108) 13.5/0.125 = 108(97~119)
;重复码为11.25ms(90) 11.25/0.125 = 90(81~99)
;0为1.125ms(9) 1.125/0.125 =9
;0为2.25ms(18) 2.25/0.125 =18
;误差按照±10%
;-----------------------------------------------------------------------*/
void IRSbr()
{
    /*
    ;incr   IRCnt
    ;movai  22
    ;rsubar IRCnt
    ;jbclr  C
    ;clrr   IRCnt
    ;movai  IRDate
    ;addar  IRCnt
    ;movra  FSR
    ;movar  IRTmr
    ;movra  INDF
    ;return
    */
    if (!FLead)                                 //;未收到头码
    {
        if (IRTmr < CHEAD-5) return;
        if (IRTmr > CHEAD+5) return;
        FLead = 1;                      //;当前接收到的是头码
        Custom = 0;
        CustomRev = 0;
        IRCode1 = 0;
        IRCodeRev1 = 0;
        FIRAck = 0;
        BitCnt = 32;
        return;
    }
    //;接收到头码-----------------------------------------------------
    if (IRTmr > CCodeR+5) {FLead = 0; return;}
    if (IRTmr >= CCodeR-5) 
        //;接收到重复码---------------------------------------------------
    {
        /*
        if (++LongIRCnt >= 6)
            {
            FLongRKey = 1;
            IRSbr4();
            }
        */
        return;
    }
            
    __asm
        movai   CCode0+(CCode1-CCode0)/2
        rsubar  _IRTmr
                
        rrr     _IRCode1        
        rrr     _IRCodeRev1 
        rrr     _Custom
        rrr     _CustomRev      
    __endasm;
    if (--BitCnt == 0) IRSbr4();
}

void IROffKeySbr()
{
    FOn = 0;
    FStop = 1;
    TStop = 3;
}

void IRSbr4()
{
    uchar   i;
    
    FLead = 0;
    //if (IRCodeRev1 != ~IRCode1) return;
    i = ~IRCode1;
    if (IRCodeRev1 != i) return;
    //if (CustomRev != ~Custom) return;
    i = ~Custom;
    if (CustomRev != i) return;
    if (Custom != CIDCode) return;
    
    if (FIRAck) return;
    FIRAck = 1;
    //power Key------------------------------------------------------
    if (IRCodeRev1 == CIRON)
    {
        if (FOn) return;
        FOn = 1;
        FTmr = 0;
    }
    if (!FOn) return;
    
    if (IRCodeRev1 == CIROFF)
    {
        IROffKeySbr();
        return;
    }
    if (IRCodeRev1 == CIRMODE)
    {
        //FCandle = !FCandle;
        __asm
            movai   CCandle
            xorra   _Flag2
        __endasm;
        Step = 10;
        FDirection = 0;
        ModeCnt = 0;
        T40ms = 0xff;
        return;
    }
    if (IRCodeRev1 == CIR4H)
    {
        Tmr = 0x3840;
IR4HKeySbr1:
        T1s = 250;
        FTmr = 1;
        return;
    }
    if (IRCodeRev1 == CIR8H)
    {
        Tmr = 0x7080;
        goto    IR4HKeySbr1;
    }
    if (IRCodeRev1 == CIRMCOLR)
    {
        //FMultiColor = !FMultiColor;               ???
        __asm
            movai   CMultiColor
            xorra   _Flag2
        __endasm;
        return;
    }
    if (IRCodeRev1 == CIRCOLR1) {i = 0;}
    if (IRCodeRev1 == CIRCOLR2) {i = 1;}
    if (IRCodeRev1 == CIRCOLR3) {i = 2;}
    if (IRCodeRev1 == CIRCOLR4) {i = 3;}
    if (IRCodeRev1 == CIRCOLR5) {i = 4;}
    if (IRCodeRev1 == CIRCOLR6) {i = 5;}
    if (IRCodeRev1 == CIRCOLR7) {i = 6;}
    if (IRCodeRev1 == CIRCOLR8) {i = 7;}
    if (IRCodeRev1 == CIRCOLR9) {i = 8;}
    if (IRCodeRev1 == CIRCOLRA) {i = 9;}
    if (IRCodeRev1 == CIRCOLRB) {i = 10;}
    if (IRCodeRev1 == CIRCOLRC) {i = 11;}
    
    FMultiColor = 0;
    //;---------------------------------------------------------------- 
    //;单色模式
    TRed = RedTbl[i];
    TGreen = GreenTbl[i];
    TBlue = BlueTbl[i];
        
    return;
}   

