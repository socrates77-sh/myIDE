
C source file F:\APP\Software\仿真软件版本库\正式版版本库\WinScopeIDE_v1.01bt_20151212\WinC_V\H6805\reg_mc20p22.h:

union p0 at 0x0000
union ddr0 at 0x0001
union p0hcon at 0x0002
union p1 at 0x0003
union ddr1 at 0x0004
union p1hcon at 0x0005
union kbim at 0x0006
unsigned char T0CNT at 0x0007
union tcr0 at 0x0008
unsigned char TDATA1 at 0x0009
unsigned char T1CNT at 0x000a
union tcr1 at 0x000b
unsigned char TDATA2H at 0x000c
unsigned char T2CNTH at 0x000d
unsigned char TDATA2L at 0x000e
unsigned char T1CNTL at 0x000f
union tcr2 at 0x0010
union intc at 0x0011
union adcm at 0x0012
union adcon at 0x0013
unsigned char ADTH at 0x0014
unsigned char ADTL at 0x0015
union mcr at 0x0016
union rstfr at 0x0017

C source file abc.c:

(no globals)

void funtion001() lines 4 to 15 at 0x180d-0x1818
    static argument unsigned char aa at 0x008a
    static argument unsigned char bb at 0x008b

C source file 20p22_c.c:

unsigned char temp[5] at 0x0085
unsigned char aaa at 0x0084

void initial() lines 9 to 29 at 0x1819-0x1849
    static auto unsigned char i at 0x008a

void SWI_ISR() lines 32 to 35 at 0x184a-0x184a
    (no locals)

void INT1_ISR() lines 37 to 40 at 0x184b-0x184b
    (no locals)

void INT0_ISR() lines 42 to 45 at 0x184c-0x184c
    (no locals)

void TMI2_ISR() lines 47 to 50 at 0x184d-0x184d
    (no locals)

void TMI1_ISR() lines 52 to 55 at 0x184e-0x184e
    (no locals)

void TMI0_ISR() lines 57 to 70 at 0x184f-0x188b
    auto int i at -2 from frame pointer

void KBI_ISR() lines 73 to 76 at 0x188c-0x188c
    (no locals)

void WDTI_ISR() lines 78 to 81 at 0x188d-0x188d
    (no locals)

void main() lines 84 to 97 at 0x188e-0x1899
    (no locals)
