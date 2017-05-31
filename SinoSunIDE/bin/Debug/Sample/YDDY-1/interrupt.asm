WDTISbr:
SWISbr:    
INT1Sbr:
INT0Sbr:
TMI2Sbr:
TMI1Sbr:
KBISbr:
    BSET    KBIC
    BCLR    T1IF
    BCLR    WDTF
    BCLR    INT1F
    BCLR    INT0F
    BCLR    T2IF
    RTI
TMI0Sbr:
    BCLR    T0IF
    BSET	F2ms
    RTI
	
    
