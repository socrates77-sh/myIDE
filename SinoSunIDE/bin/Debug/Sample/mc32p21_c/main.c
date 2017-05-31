
#include  <mc32p21.h>
#include  "VAR.H"
//���ǽ�λ���޸ģ������˿����ü���
#define      COM0      P00      
#define      COM1      P01
#define      COM2      P02
 
#define      SEG_A     P10
#define      SEG_B     P11
#define      SEG_C     P12
#define      SEG_D     P13
#define      SEG_E     P14
#define      SEG_F     P15
#define      SEG_G     P03
#define      SEG_H     P16

//============================
  
//====Ϊ���޸���ʾ�˿ڷ��㣬�����˴���ROM�ռ�======
const  uchar  TAB_sega[10]={1,0,1,1,0,1,1,1,1,1};
const  uchar  TAB_segb[10]={1,1,1,1,1,0,0,1,1,1};
const  uchar  TAB_segc[10]={1,1,0,1,1,1,1,1,1,1};
const  uchar  TAB_segd[10]={1,0,1,1,0,1,1,0,1,1};
const  uchar  TAB_sege[10]={1,0,1,0,0,0,1,0,1,0};
const  uchar  TAB_segf[10]={1,0,0,0,1,1,1,0,1,1};
const  uchar  TAB_segg[10]={0,0,1,1,1,1,1,0,1,1};
                                



void  Init_sys(void)
{
       OEP0   = 0xef;
       IOP0   = 0x00;
       OEP1   = 0xff;
       IOP1   = 0x00;   
       ANSEL  = 0x10;//p04��ΪAD������� 
       T0CR   = 0x84;//FCPU   16T
       T0LOAD = 200;
       ADCR0  = 0x43;
       ADCR1  = 0x00;//ref=2V
       INTE   = 0x01; 
       INTF   = 0x00;
       PUP0   = 0;
}

void  Display(uchar i)
{
          COM0 = 1;
          COM1 = 1;
          COM2 = 1; 
          SEG_A = TAB_sega[i];
          SEG_B = TAB_segb[i];
          SEG_C = TAB_segc[i];
          SEG_D = TAB_segd[i];
          SEG_E = TAB_sege[i];
          SEG_F = TAB_segf[i];
          SEG_G = TAB_segg[i];                                  
}


//===============8bits*8bits==================================
//===����3ϵ�б�����ԭ���޷�����ֲ�������R_rx����Ϊ��������ľֲ�������ֻҪ��Ƕ���в�ʹ����ͬ���ֵı�������==
//==׷���Ч�ʣ����鲻ʹ��16 bits����======================================
//==R_r1,R_r0
void  Multiplication(uchar R_Multiplier,uchar R_Multiplicand)
{
      R_r1 = 0;      //�����8λ
      R_r0 = 0;      //�����8λ
      R_r3 = R_Multiplier;
      R_r4 = R_Multiplicand;     
      for(R_r2=0;R_r2<8;R_r2++)   
      {
     __asm;              
        rrr     _R_r3      
        jbset   _STATUS,0  
        goto    Mul_loop
        movar   _R_r4
        addra   _R_r1      
      Mul_loop:                                                                
        rrr     _R_r1      
        rrr     _R_r0            
        __endasm;      
      }  
      //������Ҫ�����԰�R_r1��R_r0����Ϊint�������ɷ���ֵ     
}
//=========================================================
//R_dividend_H,R_dividend_L������=========
//R_divisor����==========================
//R_r0�̵�8λ 
//R_r1�̸�8λ
//R_r2����
//=========================================================
void  Division(unsigned int R_dend,uchar R_sor)
{
        R_r0 = 0;
        R_r1 = 0;
        R_r2 = 0; 
        R_r3 = R_dend>>8;
        R_r4 = R_dend&0xff;
        R_r5 = R_sor;
        R_r7 = 0;                       
        if(R_r5==0)
        {
            R_r0 = 0;
            R_r1 = 0;
            R_r2 = 0;        
        }
        else
        {
            for(R_r6=0;R_r6<16;R_r6++)
            {
       __asm;                    
        bclr            _STATUS,0
        rlr             _R_r4      
        rlr             _R_r3      
        rlr             _R_r2      
        rlr             _R_r7
        movar           _R_r5
        rsubar          _R_r2
        jbclr           _STATUS,0
        goto            updata_div
        jbclr           _R_r7,0
        goto            updata_div
        bclr            _STATUS,0
        goto            r0_shift
     updata_div:                                                                       
        movra           _R_r2
        bset            _STATUS,0
     r0_shift:
        rlr             _R_r0
        rlr             _R_r1 
     __endasm;   
         }
        }                                                                                            
}

//===============================================
void main (void)
{   
        GIE = 0;
        ClrWdt();
        __asm
          movai       0x7f
          movra       0x00
          movar       0x00
          movra       FSR          
          clrr        INDF
          djzr        0x00
          goto        $-4
          clrr        INDF
          clrr        0x00
        __endasm;    
        
        Init_sys();
        GIE = 1;
        while(1)
        {          
                R_16_ADC = 0;
                for(R_ADC_Filt=0;R_ADC_Filt<255;R_ADC_Filt++)
                {
                    ADEOC = 0;//start ADC
                    do{Nop();}while(ADEOC==0);       
                    R_16_ADC = R_16_ADC+ADRH;                    
                }                  
                R_ADC_temp  = R_16_ADC>>8;                
                R_ADC_const = 200;//��Ҫ�˲�Ч���ã�������Ҫһ���㷨ȥȷ����ֵ��
                if(R_ADC_bak>R_ADC_temp)
                {
                      R_ADC_temp = R_ADC_bak-R_ADC_temp;
                      Multiplication(R_ADC_temp,R_ADC_const); 
                      R_ADC_temp = R_ADC_bak-R_r1; 
                } 
                else{
                      R_ADC_temp = R_ADC_temp-R_ADC_bak;
                      Multiplication(R_ADC_temp,R_ADC_const);
                      R_ADC_temp = R_ADC_bak+R_r1;   
                }   
                R_ADC_bak = R_ADC_temp;
                Multiplication(R_ADC_temp,200);
                R_ADC_temp = R_r1;//ȡ��8λ
                Division(R_ADC_temp,100);//ȡ��λ
                R_disp_H   = R_r0;
                R_ADC_temp = R_r2;
                Division(R_ADC_temp,10);//ȡʮλ
                R_disp_M = R_r0;  
                R_disp_L = R_r2; 
            
       }
}
//=============================================================        
void int_isr(void) __interrupt
{
        __asm
                movra   _ABuf
                swapar  _STATUS
                movra   _StatusBuf
        __endasm;
        T0IF  = 0;
        R_disp_mode++;
        if(R_disp_mode>2)R_disp_mode = 0;                
        if(R_disp_mode==0){Display(R_disp_H);COM0=0;}
        if(R_disp_mode==1){Display(R_disp_M);COM1=0;}
        if(R_disp_mode==2){Display(R_disp_L);COM2=0;}       
        __asm
                swapar  _StatusBuf
                movra   _STATUS
                swapr   _ABuf
                swapar  _ABuf
        __endasm;
}