#ifndef _H_MC20P24B_GPIO
#define _H_MC20P24B_GPIO

#define GPIO_CFG_IN(obj)        { DDR.obj=DDR_IN; }
#define GPIO_CFG_OUT(obj)       { DDR.obj=DDR_OUT; }
#define GPIO_CFG_PULL(obj)      { PL1.obj=PULL_EN; }
#define GPIO_CFG_FLOAT(obj)     { PL1.obj=PULL_DIS; }
#define GPIO_CFG_IN_PULL(obj)   { DDR.obj=DDR_IN; PL1.obj=PULL_EN; }
#define GPIO_CFG_IN_FLOAT(obj)  { DDR.obj=DDR_IN; PL1.obj=PULL_DIS; }
#define GPIO_CFG_OUT_PP(obj)    { PL1.obj=PULL_DIS; DDR.obj=DDR_OUT; }
#define GPIO_CFG(obj,v)         GPIO_CFG_##v(obj)
#define GPIO_SET(obj,v)         GPIO_CFG(obj,v)

#define GPIO_REV(obj)       { OUT.obj=!OUT.obj; }
#define GPIO_OUT(obj,v)     { OUT.obj=(v); }
#define GPIO_OUT1(obj)      { OUT.obj=1; }
#define GPIO_OUT0(obj)      { OUT.obj=0; }
#define GPIO_IN(obj)        ( PIN.obj )
#define GPIO_IS_OUT1(obj)   ( OUT.obj==1 )
#define GPIO_IS_OUT0(obj)   ( OUT.obj==0 )

#define GPIO_CFG_IN(obj)   	( PORTCFG.obj==IN_NOPULL)
#define GPIO_CFG_PULLIN(obj)( PORTCFG.obj==IN_PULL)
#define GPIO_CFG_PWM(obj)	( PORTCFG.obj==PWMOUT)
#define GPIO_CFG_OUT(obj)	( PORTCFG.obj==OUT)
#define GPIO_CFG_ADIN(obj)	( PORTCFG.obj==ADIN)
#define GPIO_CFG_OCOUT(obj)	( PORTCFG.obj==OCOUT)
#define GPIO_CFG(obj,v)         GPIO_CFG_##v(obj)


//  DDR
enum
{
    IN_PULL  = 0b00,
    IN_NOPULL = 0b01,
    PWMOUT = 0b01,
    
    OUT = 0b10,
    ADIN = 0b11,
    OCOUT = 0b11
};

//  PL1
enum
{
    PULL_DIS= 0,
    PULL_EN = 1,
    //
    PULL_DIS_ALL= 0x00,
    PULL_EN_ALL = 0xff
};
//  PL0
enum
{
    DOWN_DIS= 0,
    DOWN_EN = 1,
    //
    DOWN_DIS_ALL= 0x00,
    DOWN_EN_ALL = 0xff
};
//  KBI
enum
{
    KBI_DIS = 0,
    KBI_EN  = 1
};

enum
{
    PA0, PA1, PA2, PA3, PA4, PA5, PA6, PA7
};

enum
{
    PB0, PB1, PB2, PB3, PB4, PB5, PB6, PB7
};

enum
{
    PC0, PC1, PC2, PC3, PC4, PC5, PC6, PC7
};


#define IO(io)      uchar  io:1;
#define NC

#ifndef IO_PORTA
#define IO_PORTA    \
/*PA0*/ IO(PA0)     \
/*PA1*/ IO(PA1)     \
/*PA2*/ IO(PA2)     \
/*PA3*/ IO(PA3)     \
/*PA4*/ IO(PA4)     \
/*PA5*/ IO(PA5)     \
/*PA6*/ IO(PA6)     \
/*PA7*/ IO(PA7)
#endif

#ifndef IO_PORTB
#define IO_PORTB    \
/*PB0*/ IO(PB0)     \
/*PB1*/ IO(PB1)     \
/*PB2*/ IO(PB2)     \
/*PB3*/ IO(PB3)     \
/*PB4*/ IO(PB4)     \
/*PB5*/ IO(PB5)     \
/*PB6*/ IO(PB6)     \
/*PB7*/ IO(PB7)
#endif

#ifndef IO_PORTC
#define IO_PORTC    \
/*PC0*/ IO(PC0)     \
/*PC1*/ IO(PC1)     \
/*PC2*/ IO(PC2)     \
/*PC3*/ IO(PC3)     \
/*PC4*/ IO(PC4)     \
/*PC5*/ IO(PC5)     \
/*PC6*/ IO(PC6)     \
/*PC7*/ IO(PC7)
#endif

typedef struct
{
    IO_PORTA
    IO_PORTB
    IO_PORTC
}   PORT;
volatile PORT PIN @0x10;
volatile PORT OUT @0x10;

#define IOC(ioc)      uchar  ioc:2;
#ifndef PORTACH
#define PORTACH    \
/*PA7*/ IOC(PA7C)		\
/*PA6*/ IOC(PA6C)		\
/*PA5*/ IOC(PA5C)		\
/*PA4*/ IOC(PA4C)		
#endif
#ifndef PORTACL
#define PORTACL    \
/*PA3*/ IOC(PA3C)     \
/*PA2*/ IOC(PA2C)     \
/*PA1*/ IOC(PA1C)     \
/*PA0*/ IOC(PA0C)     
#endif

#ifndef PORTB
#define PORTB    \
/*PC3*/ IOC(PB3C)     \
/*PC2*/ IOC(PB2C)     \
/*PC1*/ IOC(PB1C)     \
/*PC0*/ IOC(PB0C)     
#endif
#ifndef PORTCH
#define PORTCH    \
/*PC7*/ uchar		:1;	\
/*PC6*/ uchar		:3; \
/*PC5*/ IOC(PC5C)     \
/*PC4*/ IOC(PC4C)     
#endif
#ifndef PORTCL
#define PORTCL    \
/*PC3*/ IOC(PC3C)     \
/*PC2*/ IOC(PC2C)     \
/*PC1*/ IOC(PC1C)     \
/*PC0*/ IOC(PC0C)     
#endif

struct
{
    PORTAH
    PORTAL
    uchar  :8;
	PORTB
    PORTCH
	PORTCL
}   PORTCFG @0x16; 

struct
{
    IO_PORTA
}   PL0     @0x03;  //下拉

struct
{
    IO_PORTB
}   KBI     @0x07;  //键盘使能

vuchar PA_ODR  @0x10;
vuchar PB_ODR  @0x11;
vuchar PC_ODR  @0x12;
vuchar PA_IDR  @0x10;
vuchar PB_IDR  @0x11;
vuchar PC_IDR  @0x12;
uchar  PA_DDR  @0x01;
uchar  PB_DDR  @0x05;
uchar  PC_DDR  @0x09;
uchar  PA_PL1  @0x02;
uchar  PA_PL0  @0x03;
uchar  PB_PL1  @0x06;

#endif
