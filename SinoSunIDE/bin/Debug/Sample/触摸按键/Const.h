#ifndef __CONST.H_
#define __CONST.H_

/*****************************************************************************************
;端口定义
*****************************************************************************************/
#define	pSW			P1Bit.P12		//;??
#define pBUZ			P1Bit.P11
#define	ADin			P0Bit.P06
#define	pHeat			P0Bit.P05

#define	pDIO			P2Bit.P23
#define	pCLK			P2Bit.P24
#define	pSTB			P2Bit.P25
/*****************************************************************************************
;温度表(10K 3950)
*****************************************************************************************/
#define	CTEMP48 	52	//0x34	 
#define	CTEMP50 	55	//0x37
#define	CTEMP51 	56	//0x38 
#define	CTEMP52 	58	//0x3A 
#define	CTEMP54 	61	//0x3D 
#define	CTEMP55 	63	//0x3F 
#define	CTEMP56 	65	//0x41 
#define	CTEMP57 	67	//0x43 
#define	CTEMP58 	68	//0x44 
#define	CTEMP60 	72	//0x48 
#define	CTEMP61 	74	//0x4A 
#define	CTEMP62 	76	//0x4C 
#define	CTEMP63 	78	//0x4E 
#define	CTEMP63A	79	//0x4F 
#define	CTEMP64 	80	//0x50
#define	CTEMP65 	81	//0x51 
#define	CTEMP66 	83	//0x53 
#define	CTEMP67 	85	//0x55 
#define	CTEMP70 	91	//0x5B 
#define	CTEMP70A	92	//0x5C 
#define	CTEMP71 	93	//0x5D 
#define	CTEMP72 	95	//0x5F 
#define	CTEMP73 	97	//0x61 
#define	CTEMP74 	99	//0x63
#define	CTEMP75 	101	//0x65
#define	CTEMP77 	105	//0x69
#define	CTEMP79 	109	//0x6D
#define	CTEMP82 	115	//0x73
#define	CTEMP84 	118	//0x76
#define	CTEMP85 	120	//0x78
#define	CTEMP86 	122	//0x7A
#define	CTEMP88 	126	//0x7E
#define	CTEMP90 	130	//0x82
#define	CTEMP97 	143	//0x8F
#define	CTEMP100	148	//0x94
#define	CTEMP104	154	//0x9A 
#define	CTEMP107	159	//0x9F 
#define	CTEMP108	161	//0xA1
#define	CTEMP115	171	//0xAB
#define	CTEMP116	173	//0xAD
#define	CTEMP118	175	//0xAF
#define	CTEMP119	177	//0xB1
#define	CTEMP120	178	//0xB2
#define	CTEMP121	179	//0xB3
#define	CTEMP122	180	//0xB4
#define	CTEMP123	182	//0xB6
#define	CTEMP124	183	//0xB7
#define	CTEMP125	184	//0xB8
#define	CTEMP126	185	//0xB9
#define	CTEMP127	187	//0xBB
#define	CTEMP128	188	//0xBC
#define	CTEMP130	190	//0xBE
#define	CTEMP131	191	//0xBF
#define	CTEMP133	193	//0xC1
#define	CTEMP134	194	//0xC2
#define	CTEMP137	197	//0xC5
#define	CTEMP140	200	//0xC8
#define	CTEMP145	204	//0xCC
#define	CTEMP148	207	//0xCF
#define	CTEMP150	208	//0xD0
#define	CTEMP162	216	//0xD8
#define	CTEMP168	220	//0xDC
#define	CTEMP180	225	//0xE1	
/*****************************************************************************************
;菜单标准工作时间
*****************************************************************************************/
#define	CTFASH		5
#define	CTCONGEE	25
#define	CTPULSE 	50
#define	CTSTEW		25	
#define	CTCHICHEN	20
#define	CTRICE 		12
#define	CTQRICE 	15
#define	CTSOUP 		30
#define	CTQSOUP 	50
	
/*****************************************************************************************
;按键值定义
*****************************************************************************************/
#define	CKPreSet		0x40
#define	CKWarm			0x08
#define	CKMenu			0x20	//
#define	CKRice			0x80
#define	CKBaoTang		0x10
/*****************************************************************************************
;菜单值定义
*****************************************************************************************/
#define	CFASH		1
#define	CCONGEE		2
#define	CPULSE 		3
#define	CSTEW		4	
#define	CCHICHEN	5
#define	CQRICE 		6
#define	CRICE 		7
#define	CQSOUP 		8
#define	CSOUP 		9

#define	CWARM		10

#define	DFASH		0x20
#define	DCONGEE		0x01
#define	DPULSE 		0x02
#define	DSTEW		0x04	
#define	DCHICHEN	0x08
#define	DQRICE 		0x80
#define	DRICE 		0x80	//DISP5
#define	DQSOUP 		0x10
#define	DSOUP 		0x10	//DISP5

#define	DWARM		0x40
#define	DPRESET		0x40	//DISP5
/*-------------------间隙加热比设定------------------------*/
#define	CWarmedHeantONTime			20		/*20/2=10s*/
#define	CWarmedHeantOffTime			110		// 55s

#define	CJingZhu0HeantONTime		10
#define	CJingZhu0HeantOffTime		20
#define	CJingZhu1HeantONTime		40
#define	CJingZhu1HeantOffTime		60
#define	CJingZhuStep2Timer			8		/*精煮第二步时间*/

#define	CCakeONTime					60
#define	CCakeOffTime				60

#define  SegA				0x02 	// 
#define  SegB				0x80	//  
#define  SegC				0x08 	// 
#define  SegD				0x20 	// 
#define  SegE				0x40 	// 
#define  SegF				0x01 	// 
#define  SegG				0x04 	// 
#define  SegDp				0x10 	// 


#define  DZero		SegA|SegB|SegC|SegD|SegE|SegF
#define  DOne		SegB|SegC
#define  DTwo		SegA|SegB|SegD|SegE|SegG
#define  DThree		SegA|SegB|SegC|SegD|SegG
#define  DFour		SegB|SegC|SegF|SegG
#define  DFive		SegA|SegC|SegD|SegF|SegG
#define  DSix		SegA|SegC|SegD|SegE|SegF|SegG
#define  DSeven		SegA|SegB|SegC
#define  DEigher	SegA|SegB|SegC|SegD|SegE|SegF|SegG
#define  DNine		SegA|SegB|SegC|SegD|SegF|SegG
#define  DLetterA	SegA|SegB|SegC|SegE|SegF|SegG
#define  DLetterb	SegC|SegD|SegE|SegF|SegG
#define  DLetterC	SegA|SegD|SegE|SegF
#define  DLetterd	SegB|SegC|SegD|SegE|SegG
#define  DLetterE	SegA|SegD|SegE|SegF|SegG
#define  DLetterF	SegA|SegE|SegF|SegG
#define  DLetterP	SegA|SegB|SegE|SegF|SegG
#define  DLettert	SegD|SegE|SegF|SegG


#define	ADCOpenBit				1
#define	ADCShortBit				2
#define	NoPanBit				4
#define	HighTempBit				8

#define	E5Flag				0x40
#define	E6Flag				0x80

//******************************************************************************

#define	DWarmOn()			(DispData[4] |= 0x40)

#define	DPreSetOn()			(DispData[5] |= 0x40)

#define	DColOn()			(DispData[1] |= 0x02)
#define	DColOff()			(DispData[1] &= ~0x02)

//******************************************************************************

#endif
