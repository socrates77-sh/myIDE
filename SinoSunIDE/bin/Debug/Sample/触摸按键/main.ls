   1                     ; C Compiler for MC68HC05 (COSMIC Software)
   2                     ; Generator V4.1n - 19 Jul 2001
   3                     ; Optimizer V4.1n - 19 Jul 2001
1382                     	xref	_BUZInit
1383                     	xref	_InitalRAM
1384                     	xref	_InitialSys
1385                     	xref	_command
1386                     	xref	_send_8bit
1387                     	xref	_DispPro
1388                     	xref	_KeyPro
1389                     	xref	_ADCPro
1390                     	xref	_BUZPro
1391                     	xref	_T500msPro
1434                     ; 7 void main()
1434                     ; 8 {	
1435                     	switch	.text
1436  0000               _main:
1439                     ; 11 	DI();
1442  0000 9b            	sei	
1444                     ; 12 	InitSP();
1447  0001 9c            	rsp	
1449                     ; 13 	WDT_DIS();
1451  0002 b60c          	lda	_WDT
1452  0004 a40f          	and	#15
1453  0006 b70c          	sta	_WDT
1454                     ; 14 	InitialSys();
1457  0008 cd0000        	jsr	_InitialSys
1459                     ; 15 	InitalRAM();
1461  000b cd0000        	jsr	_InitalRAM
1463                     ; 29 	for (Buf1=0;Buf1<5;++Buf1) DispData[Buf1] = 0;
1465  000e 3f2e          	clr	_Buf1
1466  0010               L7501:
1469  0010 be2e          	ldx	_Buf1
1470  0012 6f31          	clr	_DispData,x
1473  0014 3c2e          	inc	_Buf1
1476  0016 b62e          	lda	_Buf1
1477  0018 a105          	cmp	#5
1478  001a 25f4          	blo	L7501
1479                     ; 30 	DispData[2] = 0x10;
1481  001c a610          	lda	#16
1482  001e b733          	sta	_DispData+2
1483                     ; 31 	for (Buf1=0;Buf1<5;++Buf1) 
1485  0020 3f2e          	clr	_Buf1
1486  0022               L5601:
1487                     ; 34 		command(0x03);	  //设置显示模式，7位10段模式 
1489  0022 5f            	clrx	
1490  0023 a603          	lda	#3
1491  0025 cd0000        	jsr	_command
1493                     ; 35 		command(0x40);	  //设置数据命令,采用地址自动加1模式 
1495  0028 5f            	clrx	
1496  0029 a640          	lda	#64
1497  002b cd0000        	jsr	_command
1499                     ; 36 		command(0xc4);	  //设置显示地址，从04H开始 
1501  002e 5f            	clrx	
1502  002f a6c4          	lda	#196
1503  0031 cd0000        	jsr	_command
1505                     ; 37 		for(Buf2=0;Buf2<2;Buf2++)	   //发送显示数据 
1507  0034 3f2d          	clr	_Buf2
1508  0036               L3701:
1509                     ; 39 			send_8bit(DispData[Buf2]); 	 //从00H起，偶数地址送显示数据 
1511  0036 be2d          	ldx	_Buf2
1512  0038 e631          	lda	_DispData,x
1513  003a cd0000        	jsr	_send_8bit
1515                     ; 40 			send_8bit(0);  //因为SEG9-14均未用到，所以奇数地址送全“0” 
1517  003d 5f            	clrx	
1518  003e 4f            	clra	
1519  003f cd0000        	jsr	_send_8bit
1521                     ; 37 		for(Buf2=0;Buf2<2;Buf2++)	   //发送显示数据 
1523  0042 3c2d          	inc	_Buf2
1526  0044 b62d          	lda	_Buf2
1527  0046 a102          	cmp	#2
1528  0048 25ec          	blo	L3701
1529                     ; 42 		command(0x8F);	  //显示控制命令，打开显示并设置为最亮 
1531  004a 5f            	clrx	
1532  004b a68f          	lda	#143
1533  004d cd0000        	jsr	_command
1535                     ; 44 		pSTB=1; 
1537  0050 1a12          	bset	5,_P2Bit
1538                     ; 31 	for (Buf1=0;Buf1<5;++Buf1) 
1540  0052 3c2e          	inc	_Buf1
1543  0054 b62e          	lda	_Buf1
1544  0056 a105          	cmp	#5
1545  0058 25c8          	blo	L5601
1546                     ; 47 	EI();
1549  005a 9a            	cli	
1551                     ; 48 	BUZInit(1);
1553  005b 5f            	clrx	
1554  005c a601          	lda	#1
1555  005e cd0000        	jsr	_BUZInit
1557  0061               L1011:
1558                     ; 51 		if(F2ms)	//;2ms程序
1560  0061 0141fd        	brclr	0,_Flag1,L1011
1561                     ; 53 			F2ms = false;
1563  0064 1141          	bclr	0,_Flag1
1564                     ; 54 			WDT_CLR();
1566  0066 120c          	bset	1,_WDT
1567                     ; 55 			BUZPro();
1570  0068 cd0000        	jsr	_BUZPro
1572                     ; 56 			if (Deley2s >= 1) ADCPro();
1574  006b 3d2c          	tst	_Deley2s
1575  006d 2703          	beq	L7011
1578  006f cd0000        	jsr	_ADCPro
1580  0072               L7011:
1581                     ; 57 			KeyPro();
1583  0072 cd0000        	jsr	_KeyPro
1585                     ; 58 			DispPro();
1587  0075 cd0000        	jsr	_DispPro
1589                     ; 59 			if (F500ms) 
1591  0078 0541e6        	brclr	2,_Flag1,L1011
1592                     ; 61 				F500ms = false;
1594  007b 1541          	bclr	2,_Flag1
1595                     ; 62 				T500ms = 0;
1597  007d 3f3a          	clr	_T500ms
1598                     ; 63 				if (++Deley2s >= 1) Deley2s = 1;
1600  007f 3c2c          	inc	_Deley2s
1601  0081 2704          	beq	L3111
1604  0083 a601          	lda	#1
1605  0085 b72c          	sta	_Deley2s
1606  0087               L3111:
1607                     ; 64 				T500msPro();
1609  0087 cd0000        	jsr	_T500msPro
1611  008a 20d5          	bra	L1011
1636                     ; 69 __interrupt void IRQ_T0INT(void)
1636                     ; 70 {
1638                     	xref.b	_IRQ_T0INT.L
1639                     	switch	.text
1640  008c               _IRQ_T0INT:
1643                     ; 71 	T0_CLRF();
1645  008c 1102          	bclr	0,_T0
1646                     ; 73 	pBUZ = !pBUZ;
1649  008e b611          	lda	_P1Bit
1650  0090 a802          	eor	#2
1651  0092 b711          	sta	_P1Bit
1652                     ; 74 	if (++T2ms >= 16)
1654  0094 3c39          	inc	_T2ms
1655  0096 b639          	lda	_T2ms
1656  0098 a110          	cmp	#16
1657  009a 2510          	blo	L3211
1658                     ; 76 		T2ms = 0;
1660  009c 3f39          	clr	_T2ms
1661                     ; 77 		F2ms = true;
1663  009e 1041          	bset	0,_Flag1
1664                     ; 79 		if (++T500ms >= 250)
1666  00a0 3c3a          	inc	_T500ms
1667  00a2 b63a          	lda	_T500ms
1668  00a4 a1fa          	cmp	#250
1669  00a6 2504          	blo	L3211
1670                     ; 81 			T500ms = 0;
1672  00a8 3f3a          	clr	_T500ms
1673                     ; 82 			F500ms = true;
1675  00aa 1441          	bset	2,_Flag1
1676  00ac               L3211:
1677                     ; 85 }
1680  00ac 80            	rti	
1700                     ; 87 __interrupt void IRQ_NonHandled(void)
1700                     ; 88 {
1702                     	xref.b	_IRQ_NonHandled.L
1703                     	switch	.text
1704  00ad               _IRQ_NonHandled:
1707                     ; 89     return;
1710  00ad 80            	rti	
1722                     	xdef	_IRQ_NonHandled
1723                     	xdef	_IRQ_T0INT
1724                     	xdef	_main
1725                     	switch	.ubsct
1726  0000               _TMin:
1727  0000 00            	ds.b	1
1728                     	xdef	_TMin
1729  0001               _TSec:
1730  0001 00            	ds.b	1
1731                     	xdef	_TSec
1732  0002               _T2sCnt:
1733  0002 00            	ds.b	1
1734                     	xdef	_T2sCnt
1735  0003               _T2s:
1736  0003 00            	ds.b	1
1737                     	xdef	_T2s
1738  0004               _ADDef:
1739  0004 00000000      	ds.b	4
1740                     	xdef	_ADDef
1741  0008               _Menu:
1742  0008 00            	ds.b	1
1743                     	xdef	_Menu
1744  0009               _BUZTime:
1745  0009 00            	ds.b	1
1746                     	xdef	_BUZTime
1747  000a               _ADCValue:
1748  000a 000000000000  	ds.b	8
1749                     	xdef	_ADCValue
1750  0012               _ADCCNT:
1751  0012 00            	ds.b	1
1752                     	xdef	_ADCCNT
1753  0013               _BomAvr:
1754  0013 00            	ds.b	1
1755                     	xdef	_BomAvr
1756  0014               _ADCBuf1:
1757  0014 000000000000  	ds.b	16
1758                     	xdef	_ADCBuf1
1759  0024               _ADCBuf:
1760  0024 000000000000  	ds.b	8
1761                     	xdef	_ADCBuf
1762  002c               _Deley2s:
1763  002c 00            	ds.b	1
1764                     	xdef	_Deley2s
1765  002d               _Buf2:
1766  002d 00            	ds.b	1
1767                     	xdef	_Buf2
1768  002e               _Buf1:
1769  002e 00            	ds.b	1
1770                     	xdef	_Buf1
1771  002f               _BUZOffTime:
1772  002f 00            	ds.b	1
1773                     	xdef	_BUZOffTime
1774  0030               _BUZOnTime:
1775  0030 00            	ds.b	1
1776                     	xdef	_BUZOnTime
1777  0031               _DispData:
1778  0031 0000000000    	ds.b	5
1779                     	xdef	_DispData
1780  0036               _KeyCnt:
1781  0036 00            	ds.b	1
1782                     	xdef	_KeyCnt
1783  0037               _KeyBuf:
1784  0037 00            	ds.b	1
1785                     	xdef	_KeyBuf
1786  0038               _KeyCode:
1787  0038 00            	ds.b	1
1788                     	xdef	_KeyCode
1789  0039               _T2ms:
1790  0039 00            	ds.b	1
1791                     	xdef	_T2ms
1792  003a               _T500ms:
1793  003a 00            	ds.b	1
1794                     	xdef	_T500ms
1795  003b               _Heat1min:
1796  003b 00            	ds.b	1
1797                     	xdef	_Heat1min
1798  003c               _KeyCtnuCNT:
1799  003c 00            	ds.b	1
1800                     	xdef	_KeyCtnuCNT
1801  003d               _LongKey:
1802  003d 00            	ds.b	1
1803                     	xdef	_LongKey
1804  003e               _T1msCnt:
1805  003e 00            	ds.b	1
1806                     	xdef	_T1msCnt
1807  003f               _DisCnt:
1808  003f 00            	ds.b	1
1809                     	xdef	_DisCnt
1810  0040               _BUZCnt:
1811  0040 00            	ds.b	1
1812                     	xdef	_BUZCnt
1813  0041               _Flag1:
1814  0041 00            	ds.b	1
1815                     	xdef	_Flag1
1816                     	end
