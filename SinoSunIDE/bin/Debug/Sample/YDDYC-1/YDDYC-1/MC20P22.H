#ifndef _H_MC20P22
#define _H_MC20P22

#include "mylib/macros.h"
#include "mylib/T0.h"

vuchar P0			@0x00;
vuchar DDR0			@0x01;
vuchar P0HCON		@0x02;
vuchar P1			@0x03;
vuchar DDR1			@0x04;
vuchar P1HCON		@0x05;
vuchar KBIM			@0x06;
vuchar const T0CNT	@0x07;
vuchar T0LOAD		@0x07;
vuchar T0CON		@0x08;

vuchar T1DATA		@0x09;	//;Write only
vuchar const T1CNT	@0x0A;
vuchar T1LOAD		@0x0A;
vuchar T1CON		@0x0B;
vuchar T2DATAH		@0x0C;
vuchar const T2CNTH	@0x0D;
vuchar T2LOADH		@0x0D;
vuchar T2DATAL		@0x0E;
vuchar const T2CNTL	@0x0F;
vuchar T2LOADL		@0x0F;
vuchar T2CON		@0x10;
vuchar INTC			@0x11;	//;INT Control Register
vuchar ADCM			@0x12;
vuchar ADCON		@0x13;
vuchar ADTH			@0x14;
vuchar ADTL			@0x15;
vuchar MCR			@0x16;
vuchar RSTFR		@0x17;

struct
{
	uchar	BUZOE		:1;
	uchar	T0PTS		:1;
	uchar	T0PTA		:1;
	uchar	Prescaler	:3;
	uchar	T0E 		:1;
	uchar	T0F 		:1;
	
}T0CONB @0x08;

//#define	T0_CLRF() 	T0CONB.T0F = 0;

struct
{
	uchar	PWM1E		:1;
	uchar	T1PTS		:1;
	uchar	T1EN		:1;
	uchar	Prescaler	:3;
	uchar	T1E 		:1;
	uchar	T1F 		:1;
	
}T1CONB @0x0B;

struct
{
	uchar	PWM2E		:1;
	uchar	T2PTS		:1;
	uchar	T2EN		:1;
	uchar	Prescaler	:3;
	uchar	T2E 		:1;
	uchar	T2F 		:1;
		
}T2CONB @0x10;

	

BITS_T	P0Bit@0x00;
BITS_T	P1Bit@0x03;
 
struct
{
	uchar	ADCE		:1;
	uchar	Prescaler	:2;
	uchar	EOC			:1;
	uchar	ADCEN		:1;
	uchar	Chanel		:3;
} ADCONBit	@0x13;

#define ADChanel(i) 	{ADCONBit.Chanel = i;}
#define ADPrescaler(i) 	{ADCONBit.Prescaler = i;}
#define ADOver			ADCONBit.EOC
#define ADStart()		{ADCONBit.ADCE = 1;}
#define CloseAD()		{ADCONBit.ADCEN = 0;}
struct
{
	uchar	LVD			:2;
	uchar	WDTM		:1;
	uchar	WDTF		:1;
	uchar	WDTC		:1;
	uchar	ADE5		:1;
	uchar	KBIC		:1;
	uchar	KBIE		:1;
} MCRBit	@0x16;
#define	ClrWDT()		{MCRBit.WDTC = 1;}
#define	DIKey()			{MCRBit.KBIE = 0;}
#define	EIKey()			{MCRBit.KBIE = 1;}
#define	ClrKeyF()		{MCRBit.KBIC = 1;}
//T0.Prescaler
enum
{
    T0PS2,
	T0PS4,
	T0PS8,
	T0PS16,
	T0PS32,
	T0PS64,
	T0PS128,
	T0PS256
};
//T1.Prescaler
enum
{
    T1PS2,
	T1PS4,
	T1PS8,
	T1PS16,
	T1PS32,
	T1PS64,
	T1PS128,
	T1PS256
};
//T2.Prescaler
enum
{
    T2PS1,
	T2PS2,
	T2PS4,
	T2PS8,
	T2PS16,
	T2PS32,
	T2PS64,
	T2PS128
};

#define NOP()		__asm("nop")
#define DI()    	__asm("sei")
#define EI()		__asm("cli")
#define SLEEP()		__asm("wait")
#define STOP()		__asm("stop")
#define InitSP()    __asm("RSP")
#define Wait()    	__asm("WAIT")
#endif	
/*
;=====================================================================
;	    ***************< MC20P22 >***************
;=====================================================================
;Special Function Register:00H----17H
;General Function Register:80H----FFH(º¬¶ÑÕ»C0H-FFH)

;OTP ROM:		   1800H--1FFFH

;------< Special Function Register >------
;00H----17H
P0			equ	$00
P0C			equ	$01	;0-input,1-output
P0PU		equ	$02	;0-disable,1-enable
P1			equ	$03
P1C			equ	$04	;0-input,1-output
P1PU		equ	$05	;0-disable,1-enable
KBIM		equ	$06	;0-disable,1-enable
T0CNT		equ	$07	;Read only
T0LOAD		equ	$07	;TLOAD0=0,T0 STOP ACCOUNT
T0CON		equ	$08
T1DATA		equ	$09	;Write only
T1CNT		equ	$0A
T1LOAD		equ	$0A
T1CON		equ	$0B
T2DATAH		equ	$0C
T2CNTH		equ	$0D
T2LOADH		equ	$0D
T2DATAL		equ	$0E
T2CNTL		equ	$0F
T2LOADL		equ	$0F
T2CON		equ	$10
INTCON		equ	$11	;INT Control Register
ADCM		equ	$12
ADCON		equ	$13
ADTH		equ	$14
ADTL		equ	$15
MCR		equ	$16
RSTFR		equ	$17


;T0CON--------------------()
BUZOE	define	0,T0CON		
T0PTS	define	1,T0CON		
T0PTA	define	2,T0CON		

T0E	define	6,T0CON		
T0F	define	7,T0CON		

;T1CON--------------------()
PWM1E	define	0,T1CON		
T1PTS	define	1,T1CON		
T1EN	define	2,T1CON		
				
T1E	define	6,T1CON		
T1F	define	7,T1CON		


;T2CON--------------------()
PWM2E	define	0,T2CON		
T2PTS	define	1,T2CON		
T2EN	define	2,T2CON		
	
T2E	define	6,T2CON		
T2F	define	7,T2CON		

;MCR-----------------------()
LVD24	define  0,MCR
LVD36	define  1,MCR
WDTM	define	2,MCR		
WDTF	define	3,MCR		
WDTC	define	4,MCR		
ADE5	define	5,MCR
KBIC	define	6,MCR
KBIE	define	7,MCR

;ADCM-----------------------()
ADE0	define	0,ADCM		
ADE1	define	1,ADCM		
ADE2	define	2,ADCM		
ADE3	define	3,ADCM		
ADE4	define	4,ADCM		
VHSL	define	5,ADCM		
VHSH	define	6,ADCM		
VREF	define	7,ADCM		

;ADCON-----------------------
ADCE	define	0,ADCON		
				
				
EOC	define	3,ADCON		
ADEN	define	4,ADCON		
				
				
				

;RSTFR------------------------
PORRF	define	0,RSTFR		
RSTRF	define	1,RSTFR		
LVRRF	define	2,RSTFR		
WDTRF	define	3,RSTFR		

*/
