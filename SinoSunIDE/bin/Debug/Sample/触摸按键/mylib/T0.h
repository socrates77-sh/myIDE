#ifndef _H_MC20P24B_T0
#define _H_MC20P24B_T0
struct
{
	uchar	T0F			:1;
	uchar	T0EN		:1;
	uchar				:1;
	uchar	Clear		:1;
	uchar				:2;
	uchar	Prescaler	:2;
}T0  @0x02;
#define T0_EN()		{ T0.T0EN=true;  }
#define T0_DIS()	{ T0.T0EN=false; }
#define T0_CLRF()   { T0.T0F=false; }
#define T0_PS(p)	{ T0.Prescaler=PS##p; }

//  T0.Prescaler
enum
{
    PSC4096,
    PSC256,
    PSC8,
    PSC1
};
#endif
