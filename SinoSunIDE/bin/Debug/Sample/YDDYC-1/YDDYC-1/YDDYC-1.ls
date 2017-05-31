   1                     ; C Compiler for MC68HC05 (COSMIC Software)
   2                     ; Generator V4.1n - 19 Jul 2001
   3                     ; Optimizer V4.1n - 19 Jul 2001
1061                     	xref	_SysInitRamSbr
1062                     	xref	_SysInitSbr
1063                     	xref	_LEDAllOff
1064                     	xref	_LEDAllOff
1065                     	xref	_KeySbr
1066                     	xref	_ADCPro
1067                     	xref	_T100msSbr
1068                     	xref	_DisplaySbr
1109                     ; 13 void main()
1109                     ; 14 {	
1110                     	switch	.text
1111  0000               _main:
1114                     ; 17 	DI();
1117  0000 9b            	sei	
1119                     ; 18 	InitSP();
1122  0001 9c            	rsp	
1124                     ; 19 	SysInitSbr();
1126  0002 cd0000        	jsr	_SysInitSbr
1128                     ; 20 	SysInitRamSbr();
1130  0005 cd0000        	jsr	_SysInitRamSbr
1132                     ; 21 	EI();
1135  0008 9a            	cli	
1137  0009               L126:
1138                     ; 24 		if (pVIn) CellLow = 0;
1140  0009 3d00          	tst	_P0
1141  000b 2702          	beq	L526
1144  000d 3ff1          	clr	_CellLow
1145  000f               L526:
1146                     ; 25 		if (CellLow == 0x55) 
1148  000f b6f1          	lda	_CellLow
1149  0011 a155          	cmp	#85
1150  0013 2614          	bne	L726
1151                     ; 27 			CloseAD();
1153  0015 1913          	bclr	4,_ADCONBit
1154                     ; 28 			DIKey();
1157  0017 1f16          	bclr	7,_MCRBit
1158                     ; 29 			ClrKeyF();
1161  0019 1c16          	bset	6,_MCRBit
1162                     ; 30 			LEDAllOff();
1165  001b cd0000        	jsr	_LEDAllOff
1167                     ; 31 			pHeat = 0;
1169  001e 3f03          	clr	_P1
1170                     ; 32 			pCharge = 0;
1172  0020 3f03          	clr	_P1
1173                     ; 33 			NOP();
1176  0022 9d            	nop	
1178                     ; 34 			NOP();
1181  0023 9d            	nop	
1183                     ; 35 			NOP();
1186  0024 9d            	nop	
1188                     ; 36 			Wait();
1191  0025 8f            	wait	
1193                     ; 37 			NOP();
1196  0026 9d            	nop	
1198                     ; 38 			NOP();
1201  0027 9d            	nop	
1203                     ; 39 			NOP();
1206  0028 9d            	nop	
1208  0029               L726:
1209                     ; 42 		if(F2ms)	//;2ms³ÌÐò
1211  0029 0114dd        	brclr	0,_Flag2,L126
1212                     ; 44 			F2ms = false;
1214  002c 1114          	bclr	0,_Flag2
1215                     ; 45 			ClrWDT();
1217  002e 1816          	bset	4,_MCRBit
1218                     ; 46 			if (CellLow != 0x55)
1221  0030 b6f1          	lda	_CellLow
1222  0032 a155          	cmp	#85
1223  0034 27d3          	beq	L126
1224                     ; 48 				ADCPro();
1226  0036 cd0000        	jsr	_ADCPro
1228                     ; 49 				KeySbr();
1230  0039 cd0000        	jsr	_KeySbr
1232                     ; 50 				LEDAllOff();
1234  003c cd0000        	jsr	_LEDAllOff
1236                     ; 51 				if (FADOver)
1238  003f 0915c7        	brclr	4,_Flag1,L126
1239                     ; 53 					DisplaySbr();
1241  0042 cd0000        	jsr	_DisplaySbr
1243                     ; 54 					T100msSbr();
1245  0045 cd0000        	jsr	_T100msSbr
1247  0048 20bf          	bra	L126
1269                     ; 60 __interrupt void IRQ_T0(void)
1269                     ; 61 {
1271                     	xref.b	_IRQ_T0.L
1272                     	switch	.text
1273  004a               _IRQ_T0:
1276                     ; 62 	T0_CLRF();
1278  004a 1102          	bclr	0,_T0
1279                     ; 63 	F2ms = 0;
1282  004c 1114          	bclr	0,_Flag2
1283                     ; 64 }
1286  004e 80            	rti	
1306                     ; 65 __interrupt void IRQ_NonHandled(void)
1306                     ; 66 {
1308                     	xref.b	_IRQ_NonHandled.L
1309                     	switch	.text
1310  004f               _IRQ_NonHandled:
1313                     ; 67 	return;
1316  004f 80            	rti	
1328                     	xdef	_IRQ_NonHandled
1329                     	xdef	_IRQ_T0
1330                     	xdef	_main
1331                     	switch	.ubsct
1332  0000               _CellDly:
1333  0000 00            	ds.b	1
1334                     	xdef	_CellDly
1335  0001               _V32CNT:
1336  0001 00            	ds.b	1
1337                     	xdef	_V32CNT
1338  0002               _V38CNT:
1339  0002 00            	ds.b	1
1340                     	xdef	_V38CNT
1341  0003               _FlashCnt:
1342  0003 00            	ds.b	1
1343                     	xdef	_FlashCnt
1344  0004               _TempShDly:
1345  0004 00            	ds.b	1
1346                     	xdef	_TempShDly
1347  0005               _TempOpDly:
1348  0005 00            	ds.b	1
1349                     	xdef	_TempOpDly
1350  0006               _T500ms:
1351  0006 00            	ds.b	1
1352                     	xdef	_T500ms
1353  0007               _CellV:
1354  0007 00            	ds.b	1
1355                     	xdef	_CellV
1356  0008               _T100ms:
1357  0008 00            	ds.b	1
1358                     	xdef	_T100ms
1359  0009               _T50ms:
1360  0009 00            	ds.b	1
1361                     	xdef	_T50ms
1362  000a               _KeyLong:
1363  000a 00            	ds.b	1
1364                     	xdef	_KeyLong
1365  000b               _Stauts:
1366  000b 00            	ds.b	1
1367                     	xdef	_Stauts
1368  000c               _VADBuf:
1369  000c 0000          	ds.b	2
1370                     	xdef	_VADBuf
1371  000e               _VAD:
1372  000e 00            	ds.b	1
1373                     	xdef	_VAD
1374  000f               _TempADBuf:
1375  000f 0000          	ds.b	2
1376                     	xdef	_TempADBuf
1377  0011               _TempAD:
1378  0011 00            	ds.b	1
1379                     	xdef	_TempAD
1380  0012               _ADCCnt:
1381  0012 00            	ds.b	1
1382                     	xdef	_ADCCnt
1383  0013               _KeyCnt:
1384  0013 00            	ds.b	1
1385                     	xdef	_KeyCnt
1386  0014               _Flag2:
1387  0014 00            	ds.b	1
1388                     	xdef	_Flag2
1389  0015               _Flag1:
1390  0015 00            	ds.b	1
1391                     	xdef	_Flag1
1392                     	end
