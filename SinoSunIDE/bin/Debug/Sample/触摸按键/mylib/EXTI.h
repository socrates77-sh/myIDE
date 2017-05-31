#ifndef _H_MC20P24B_EXTI
#define _H_MC20P24B_EXTI

#define EXTI_Enabled(obj)       { obj.Enabled=EXTI_EN; }
#define EXTI_Disabled(obj)      { obj.Enabled=EXTI_DIS; }
#define EXTI_IsEnabled(obj)     ( obj.Enabled==EXTI_EN )
#define EXTI_IsDisabled(obj)    ( obj.Enabled==EXTI_DIS )
#define EXTI_ClearFlag(obj)     { obj.UpdateIF=false; }
#define EXTI_EN(obj)            EXTI_Enabled(obj)
#define EXTI_DIS(obj)           EXTI_Disabled(obj)

struct
{
    uchar          	:4;
    uchar  UpdateIF	:1;
    uchar  Mode    	:2;
    uchar  Enabled 	:1;
}   INT0 @0x0C;

struct
{
    uchar  UpdateIF	:1;
    uchar  Mode    	:2;
    uchar  Enabled 	:1;
    uchar          	:4;
}   INT1 @0x0C;

enum
{
    EXTI_DIS,
    EXTI_EN
};

#endif
