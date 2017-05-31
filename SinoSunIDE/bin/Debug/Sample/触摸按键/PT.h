#ifndef _H_PT
#define _H_PT

#define PT_LINE()   (uchar)((uchar)__LINE__==0?1:__LINE__)

#define PT_WAIT_WHILE(condition)    do{ _pt.line=PT_LINE(); case PT_LINE(): if(condition) return; }while(0)
#define PT_WAIT_UNTIL(condition)    PT_WAIT_WHILE(!(condition))
#define PT_YIELD()                  do{ _pt.line=PT_LINE(); return; case PT_LINE():; }while(0)
#define PT_YIELD_WHILE(condition)   do{ _pt.line=PT_LINE(); return; case PT_LINE(): if(condition) return; }while(0)
#define PT_YIELD_UNTIL(condition)   PT_YIELD_WHILE(!(condition))

#define PT_DELAY_TICK(n)    { _pt.time.tick=n; PT_YIELD(); }
#define PT_DELAY_HMS(hms)   { _pt.time.tick=MS(1000); _pt.time.sec=(ushort)(HMS_HOUR(hms)*3600+HMS_MIN(hms)*60+HMS_SEC(hms)); PT_YIELD(); }

#define HMS_HOUR(hms)       (1?1?hms)
#define HMS_MIN(hms)        (1?0?hms)
#define HMS_SEC(hms)        (0?0?hms)
#define PT_DELAY_MS(ms)     PT_DELAY_TICK(MS(ms))
#define PT_DELAY_HOUR(hh)   PT_DELAY_HMS(hh:00:00)
#define PT_DELAY_MIN(mm)    PT_DELAY_HMS(00:mm:00)
#define PT_DELAY_SEC(ss)    PT_DELAY_HMS(00:00:ss)

#define PT_RESET()          { goto _PT_RESET; }
#define PT_CLEAR()          { _pt.line=0; _pt.time.tick=0; _pt.time.sec=0; }

typedef union
{
    uchar tick;
    uchar sec;
}   PT_TIME1_T;

typedef struct
{
    uchar tick;
    uchar sec;
}   PT_TIME2_T;

typedef struct
{
    uchar tick;
    ushort sec;
}   PT_TIME3_T;

typedef struct
{
    uchar tick;
    ulong sec;
}   PT_TIME5_T;

typedef union
{
    uchar line;
    PT_TIME1_T time;
}   PT_LINE_T;  // use no time

typedef struct
{
    uchar line;
    PT_TIME1_T time;
}   PT_TICK_T;  // use only tick

typedef struct
{
    uchar line;
    PT_TIME2_T time;
}   PT_SEC1_T;  // use 1 byte sec

typedef struct
{
    uchar line;
    PT_TIME3_T time;
}   PT_SEC2_T;  // use 2 byte sec

typedef struct
{
    uchar line;
    PT_TIME5_T time;
}   PT_SEC4_T;  // use 4 byte sec

#define PT_USE_LINE PT_LINE_T
#define PT_USE_MS   PT_TICK_T
#define PT_USE_SEC  PT_SEC1_T
#define PT_USE_MIN  PT_SEC2_T
#define PT_USE_HOUR PT_SEC4_T

#define PT_INIT()   PT_INIT_EX(USE_LINE)
#define PT_INIT_EX(_pt_type)    \
    static PT_##_pt_type _pt = {0};
#define PT_BEGIN() \
    {\
        if(sizeof(_pt)>=sizeof(PT_TICK_T) && _pt.time.tick>0 && --_pt.time.tick>0) { return; }\
        if(sizeof(_pt)>=sizeof(PT_SEC1_T) && _pt.time.sec>0 && --_pt.time.sec>0) {_pt.time.tick=MS(1000); return;}\
        switch(_pt.line)\
        {\
            default:\
            case 0:;
#define PT_END()    \
        }\
    _PT_RESET:\
        { _pt.line=0; _pt.time.tick=0; _pt.time.sec=0; }\
    }\

#endif
