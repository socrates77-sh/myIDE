
#define XORIA	XORAI	

	org 0x00
   GOTO		0x0001
   MOVAI	0x08
   IOSW		0x06
   MOVAI		0xFF
   MOVRA		0x06
   MOVAI		0x01
   MOVRA		0x3A
   MOVAI		0x01
   MOVRA		0x34
   MOVAI		0x64
   MOVRA		0x39
   CLRR		0x31
   CLRR		0x2F
   CLRR		0x2E
   CLRR		0x3C
   IOSR		0x05
   ANDAI		0x40
   ADDAI		0x08
   IOSW		0x01
   MOVAI		0x42
   MOVRA		0x01
    BSET		0x0006,0x0004
    BSET		0x0006,0x0005
   MOVAI		0x04
   MOVRA		0x38
   CALL		0x02C9
   MOVAI		0xA0
   CALL		0x02FD
   CALL		0x02EF
   MOVAI		0x00
   CALL		0x02FD
   CALL		0x02EF
   CALL		0x02C9
   MOVAI		0xA1
   CALL		0x02FD
   CALL		0x02EF
   CALL		0x030E
   CALL		0x02DC
   CALL		0x02D1
   MOVAR		0x3A
   XORIA		0xFF
    JBCLR		0x0003,0x0002
   GOTO		0x002C
   GOTO		0x0055
   MOVAI		0x01
   MOVRA		0x3A
   CALL		0x02C9
   MOVAI		0xA0
   CALL		0x02FD
   CALL		0x02EF
   MOVAI		0x00
   CALL		0x02FD
   CALL		0x02EF
   MOVAR		0x3A
   CALL		0x02FD
   CALL		0x02EF
   CALL		0x02D1
   MOVAI		0x64
   CALL		0x02AA
   GOTO		0x0055
    BCLR		0x0003,0x0000
   RLR		0x3A
   MOVAR		0x3A
    JBCLR		0x0003,0x0002
   INCR		0x3A
   MOVAI		0x01
   MOVRA		0x34
   MOVAI		0x64
   MOVRA		0x39
   CLRR		0x2F
   CLRR		0x2E
   CALL		0x02C9
   MOVAI		0xA0
   CALL		0x02FD
   CALL		0x02EF
   MOVAI		0x00
   CALL		0x02FD
   CALL		0x02EF
   MOVAR		0x3A
   CALL		0x02FD
   CALL		0x02EF
   CALL		0x02D1
   MOVAI		0x64
   CALL		0x02AA
   NOP 
   MOVAR		0x3A
   XORIA		0x01
    JBCLR		0x0003,0x0002
   GOTO		0x0075
   MOVAR		0x3A
   XORIA		0x02
    JBCLR		0x0003,0x0002
   GOTO		0x007C
   MOVAR		0x3A
   XORIA		0x04
    JBCLR		0x0003,0x0002
   GOTO		0x007E
   MOVAR		0x3A
   XORIA		0x08
    JBCLR		0x0003,0x0002
   GOTO		0x0080
   MOVAR		0x3A
   XORIA		0x10
    JBCLR		0x0003,0x0002
   GOTO		0x0082
   MOVAR		0x3A
   XORIA		0x20
    JBCLR		0x0003,0x0002
   GOTO		0x0084
   MOVAR		0x3A
   XORIA		0x40
    JBCLR		0x0003,0x0002
   GOTO		0x0086
   MOVAR		0x3A
   XORIA		0x80
    JBCLR		0x0003,0x0002
   GOTO		0x0088
   CALL		0x008A
   CALL		0x011A
   CALL		0x0135
   CALL		0x01C5
   CALL		0x01D4
   CALL		0x024F
   GOTO		0x0055
   CALL		0x008A
   GOTO		0x0055
   CALL		0x011A
   GOTO		0x0055
   CALL		0x0135
   GOTO		0x0055
   CALL		0x01C5
   GOTO		0x0055
   CALL		0x01D4
   GOTO		0x0055
   CALL		0x024F
   GOTO		0x0055
   CALL		0x026E
   GOTO		0x0055
   MOVAI		0x01
   MOVRA		0x34
   MOVAI		0x32
   MOVRA		0x39
   MOVAI		0x02
   MOVRA		0x37
   CLRR		0x2F
   MOVAI		0x10
   MOVRA		0x2D
   MOVAI		0x60
   MOVRA		0x30
   NOP 
   CALL		0x00C7
   DJZR		0x30
   GOTO		0x0095
   MOVAI		0x01
   MOVRA		0x34
   DJZR		0x2D
   GOTO		0x0093
   INCR		0x2F
   MOVAI		0x10
   MOVRA		0x2D
   MOVAI		0x60
   MOVRA		0x30
   NOP 
   CALL		0x00C7
   DJZR		0x30
   GOTO		0x00A2
   DJZR		0x2D
   GOTO		0x00A0
   CLRR		0x2F
   MOVAI		0x01
   MOVRA		0x37
   CLRR		0x2F
   MOVAI		0x08
   MOVRA		0x2D
   MOVAI		0x60
   MOVRA		0x30
   NOP 
   CALL		0x00C7
   DJZR		0x30
   GOTO		0x00B0
   MOVAI		0x01
   MOVRA		0x34
   DJZR		0x2D
   GOTO		0x00AE
   INCR		0x2F
   MOVAI		0x10
   MOVRA		0x2D
   MOVAI		0x60
   MOVRA		0x30
   NOP 
   CALL		0x00C7
   DJZR		0x30
   GOTO		0x00BD
   DJZR		0x2D
   GOTO		0x00BB
   CLRR		0x2F
   MOVAI		0x64
   MOVRA		0x39
   RETURN 
   MOVAR		0x37
   MOVRA		0x38
   MOVAR		0x34
   MOVRA		0x31
   RSUBAR		0x39
   MOVRA		0x35
    JBSET		0x002F,0x0000
   GOTO		0x00D0
   GOTO		0x00D3
    BSET		0x0006,0x0004
    BCLR		0x0006,0x0005
   GOTO		0x00D5
    BSET		0x0006,0x0005
    BCLR		0x0006,0x0004
    JBSET		0x0006,0x0003
   GOTO		0x00D9
    BCLR		0x003C,0x0000
   GOTO		0x00E0
    JBSET		0x003C,0x0000
   GOTO		0x00DC
   GOTO		0x00E0
    BSET		0x003C,0x0000
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x003C
    JBSET		0x000F,0x0000
   GOTO		0x00E0
    BCLR		0x000F,0x0000
   MOVAI		0x42
   MOVRA		0x01
   DJZR		0x31
   GOTO		0x00CD
    JBSET		0x002F,0x0000
   GOTO		0x00EA
   GOTO		0x00ED
    BCLR		0x0006,0x0004
    BSET		0x0006,0x0005
   GOTO		0x00EF
    BCLR		0x0006,0x0005
    BSET		0x0006,0x0004
    JBSET		0x0006,0x0003
   GOTO		0x00F3
    BCLR		0x003C,0x0000
   GOTO		0x00FA
    JBSET		0x003C,0x0000
   GOTO		0x00F6
   GOTO		0x00FA
    BSET		0x003C,0x0000
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x003C
   CALL		0x02B1
   DJZR		0x35
   GOTO		0x00E7
   DJZR		0x38
   GOTO		0x00C9
    JBCLR		0x002E,0x0000
   GOTO		0x010E
   GOTO		0x0102
   NOP 
   INCR		0x34
   MOVAR		0x34
   MOVRA		0x32
   MOVAI		0x31
   XORRA		0x32
   MOVAR		0x32
    JBCLR		0x0003,0x0002
   GOTO		0x010C
   GOTO		0x0119
   INCR		0x2E
   GOTO		0x0119
   NOP 
   DECR		0x34
   MOVAR		0x34
   MOVRA		0x32
   MOVAI		0x01
   XORRA		0x32
   MOVAR		0x32
    JBCLR		0x0003,0x0002
   GOTO		0x0118
   GOTO		0x0119
   CLRR		0x2E
   RETURN 
   MOVAI		0x04
   MOVRA		0x30
   MOVAI		0x7D
   MOVRA		0x31
   CALL		0x012A
   MOVAI		0x08
   MOVRA		0x30
   MOVAI		0x3E
   MOVRA		0x31
   CALL		0x012A
   MOVAI		0x10
   MOVRA		0x30
   MOVAI		0x1F
   MOVRA		0x31
   CALL		0x012A
   RETURN 
    BSET		0x0006,0x0004
    BCLR		0x0006,0x0005
   MOVAR		0x31
   CALL		0x0299
    BCLR		0x0006,0x0004
    BSET		0x0006,0x0005
   MOVAR		0x31
   CALL		0x0299
   DJZR		0x30
   GOTO		0x012A
   RETURN 
   MOVAI		0x01
   MOVRA		0x34
   MOVAI		0x06
   MOVRA		0x2D
   MOVRA		0x38
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x016E
   DJZR		0x30
   GOTO		0x013C
   INCR		0x2F
   MOVAI		0x06
   MOVRA		0x2D
   MOVRA		0x38
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x016E
   DJZR		0x30
   GOTO		0x0145
   CLRR		0x2F
   MOVAI		0x04
   MOVRA		0x2D
   MOVRA		0x38
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x016E
   DJZR		0x30
   GOTO		0x014E
   MOVAI		0x04
   MOVRA		0x2D
   MOVRA		0x38
   INCR		0x2F
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x016E
   DJZR		0x30
   GOTO		0x0157
   CLRR		0x2F
   MOVAI		0x02
   MOVRA		0x2D
   MOVRA		0x38
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x016E
   DJZR		0x30
   GOTO		0x0160
   MOVAI		0x04
   MOVRA		0x2D
   MOVRA		0x38
   INCR		0x2F
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x016E
   DJZR		0x30
   GOTO		0x0169
   CLRR		0x2F
   RETURN 
   MOVAR		0x34
   MOVRA		0x31
   RSUBAR		0x39
   MOVRA		0x35
    JBCLR		0x002F,0x0000
   GOTO		0x0177
    BCLR		0x0006,0x0005
    BSET		0x0006,0x0004
   GOTO		0x0179
    BCLR		0x0006,0x0004
    BSET		0x0006,0x0005
    JBSET		0x0006,0x0003
   GOTO		0x017D
    BCLR		0x003C,0x0000
   GOTO		0x0187
    JBSET		0x003C,0x0000
   GOTO		0x0180
   GOTO		0x0187
    BSET		0x003C,0x0000
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x003C
    BCLR		0x000F,0x0000
   MOVAI		0x42
   MOVRA		0x01
    JBSET		0x000F,0x0000
   GOTO		0x0187
    BCLR		0x000F,0x0000
   MOVAI		0x42
   MOVRA		0x01
   DJZR		0x31
   GOTO		0x0172
    JBCLR		0x002F,0x0000
   GOTO		0x0192
    BCLR		0x0006,0x0004
   GOTO		0x0193
    BCLR		0x0006,0x0005
    JBSET		0x0006,0x0003
   GOTO		0x0197
    BCLR		0x003C,0x0000
   GOTO		0x019E
    JBSET		0x003C,0x0000
   GOTO		0x019A
   GOTO		0x019E
    BSET		0x003C,0x0000
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x003C
   CALL		0x02B1
   NOP 
   DJZR		0x35
   GOTO		0x018E
   DJZR		0x38
   GOTO		0x016E
   MOVAR		0x2D
   MOVRA		0x38
    JBCLR		0x002E,0x0000
   GOTO		0x01B7
   GOTO		0x01A9
   NOP 
   INCR		0x34
   MOVAR		0x34
   MOVRA		0x37
   MOVAI		0x63
   XORRA		0x37
   MOVAR		0x37
    JBCLR		0x0003,0x0002
   GOTO		0x01B3
   GOTO		0x01C4
   INCR		0x2E
   MOVAI		0xFE
   MOVRA		0x38
   GOTO		0x01C4
   NOP 
   DECR		0x34
   MOVAR		0x34
   MOVRA		0x37
   MOVAI		0x01
   XORRA		0x37
   MOVAR		0x37
    JBCLR		0x0003,0x0002
   GOTO		0x01C1
   GOTO		0x01C4
   CLRR		0x2E
   MOVAI		0x50
   MOVRA		0x38
   RETURN 
   MOVAI		0x08
   MOVRA		0x30
   MOVAI		0x10
   MOVRA		0x31
   CALL		0x012A
   CALL		0x0255
   CALL		0x0255
   MOVAI		0x08
   MOVRA		0x30
   MOVAI		0x10
   MOVRA		0x31
   CALL		0x012A
   CALL		0x0255
   CALL		0x0255
   RETURN 
   MOVAI		0x01
   MOVRA		0x2B
   MOVAI		0x06
   MOVRA		0x2D
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x01EA
   MOVAI		0x01
   MOVRA		0x2B
   MOVAI		0x04
   MOVRA		0x2D
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x01EA
   MOVAI		0x01
   MOVRA		0x2B
   MOVAI		0x02
   MOVRA		0x2D
   MOVAI		0xC4
   MOVRA		0x30
   CALL		0x01EA
   RETURN 
   CALL		0x01EE
   DJZR		0x30
   GOTO		0x01EA
   RETURN 
    JBCLR		0x002F,0x0000
   GOTO		0x01F5
   MOVAR		0x34
   MOVRA		0x31
   RSUBAR		0x39
   MOVRA		0x35
   GOTO		0x01F9
   MOVAR		0x2B
   MOVRA		0x31
   RSUBAR		0x39
   MOVRA		0x35
    JBCLR		0x002F,0x0000
   GOTO		0x01FD
    BSET		0x0006,0x0004
   GOTO		0x01FE
    BSET		0x0006,0x0005
    JBSET		0x0006,0x0003
   GOTO		0x0202
    BCLR		0x003C,0x0000
   GOTO		0x0209
    JBSET		0x003C,0x0000
   GOTO		0x0205
   GOTO		0x0209
    BSET		0x003C,0x0000
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x003C
    JBSET		0x000F,0x0000
   GOTO		0x0209
    BCLR		0x000F,0x0000
   MOVAI		0x42
   MOVRA		0x01
   DJZR		0x31
   GOTO		0x01F9
    JBCLR		0x002F,0x0000
   GOTO		0x0214
    BCLR		0x0006,0x0004
   GOTO		0x0220
    BCLR		0x0006,0x0005
    JBSET		0x0006,0x0003
   GOTO		0x0219
    BCLR		0x003C,0x0000
   GOTO		0x0220
    JBSET		0x003C,0x0000
   GOTO		0x021C
   GOTO		0x0220
    BSET		0x003C,0x0000
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x003C
   CALL		0x02B1
   DJZR		0x35
   GOTO		0x0210
    BCLR		0x0006,0x0004
    BCLR		0x0006,0x0005
   MOVAR		0x2F
   XORIA		0x01
   MOVRA		0x2F
    BCLR		0x0006,0x0004
    BCLR		0x0006,0x0005
   DJZR		0x38
   GOTO		0x01EE
   MOVAR		0x2D
   MOVRA		0x38
    JBCLR		0x002E,0x0000
   GOTO		0x0240
   GOTO		0x0231
   NOP 
   INCR		0x34
   INCR		0x2B
   MOVAR		0x34
   MOVRA		0x32
   MOVAI		0x63
   XORRA		0x32
   MOVAR		0x32
    JBCLR		0x0003,0x0002
   GOTO		0x023C
   GOTO		0x024E
   INCR		0x2E
   MOVAI		0xFE
   MOVRA		0x38
   GOTO		0x024E
   NOP 
   DECR		0x34
   DECR		0x2B
   MOVAR		0x34
   MOVRA		0x32
   MOVAI		0x01
   XORRA		0x32
   MOVAR		0x32
    JBCLR		0x0003,0x0002
   GOTO		0x024B
   GOTO		0x024E
   CLRR		0x2E
   MOVAI		0x50
   MOVRA		0x38
   RETURN 
   MOVAI		0x08
   MOVRA		0x31
   CALL		0x0255
   DJZR		0x31
   GOTO		0x0251
   RETURN 
   MOVAI		0x04
   MOVRA		0x30
    BSET		0x0006,0x0004
    BCLR		0x0006,0x0005
   MOVAI		0x10
   CALL		0x0299
    BCLR		0x0006,0x0004
    BCLR		0x0006,0x0005
   MOVAI		0x10
   CALL		0x0299
   DJZR		0x30
   GOTO		0x0257
   MOVAI		0x04
   MOVRA		0x30
    BSET		0x0006,0x0005
    BCLR		0x0006,0x0004
   MOVAI		0x10
   CALL		0x0299
    BCLR		0x0006,0x0004
    BCLR		0x0006,0x0005
   MOVAI		0x10
   CALL		0x0299
   DJZR		0x30
   GOTO		0x0263
   RETURN 
   MOVAI		0x20
   MOVRA		0x2D
   MOVAI		0xFF
   MOVRA		0x31
    JBSET		0x0006,0x0003
   GOTO		0x0276
    BCLR		0x003C,0x0000
   GOTO		0x027D
    JBSET		0x003C,0x0000
   GOTO		0x0279
   GOTO		0x027D
    BSET		0x003C,0x0000
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x003C
   MOVAI		0x05
   MOVRA		0x30
    BSET		0x0006,0x0004
    BCLR		0x0006,0x0005
   CALL		0x02B1
    BCLR		0x0006,0x0004
    BSET		0x0006,0x0005
   CALL		0x02B1
   DJZR		0x30
   GOTO		0x027F
   DJZR		0x31
   GOTO		0x0272
   DJZR		0x2D
   GOTO		0x0270
   RETURN 
    JBSET		0x0006,0x0003
   GOTO		0x0291
    JBCLR		0x003C,0x0000
   GOTO		0x003C
   GOTO		0x0298
    JBCLR		0x003C,0x0000
   GOTO		0x0298
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x0297
   GOTO		0x0298
    BSET		0x003C,0x0000
   RETURN 
   MOVRA		0x37
    JBSET		0x0006,0x0003
   GOTO		0x029E
    BCLR		0x003C,0x0000
   GOTO		0x02A5
    JBSET		0x003C,0x0000
   GOTO		0x02A1
   GOTO		0x02A5
    BSET		0x003C,0x0000
   CALL		0x02BB
    JBSET		0x0006,0x0003
   GOTO		0x003C
   MOVAI		0x64
   CALL		0x02AA
   DJZR		0x37
   GOTO		0x029A
   RETURN 
   MOVRA		0x36
   CALL		0x02B1
   DJZR		0x36
   GOTO		0x02AB
   NOP 
   NOP 
   RETURN 
   MOVAI		0x01
   MOVRA		0x33
   MOVAI		0x40
   MOVRA		0x32
   DJZR		0x32
   GOTO		0x02B5
   DJZR		0x33
   GOTO		0x02B3
   NOP 
   RETURN 
   NOP 
   MOVAI		0x0A
   MOVRA		0x2C
   MOVAI		0x64
   CALL		0x02AA
   DJZR		0x2C
   GOTO		0x02BE
   RETURN 
   MOVAI		0x01
   MOVRA		0x37
   NOP 
   DJZR		0x37
   GOTO		0x02C5
   RETURN 
    BCLR		0x0006,0x0001
    BSET		0x0006,0x0002
    BSET		0x0006,0x0001
   CALL		0x02B1
    BCLR		0x0006,0x0002
   CALL		0x02B1
    BCLR		0x0006,0x0001
   RETURN 
    BCLR		0x0006,0x0002
    BSET		0x0006,0x0001
   CALL		0x02B1
    BSET		0x0006,0x0002
   RETURN 
    BCLR		0x0006,0x0002
    BSET		0x0006,0x0001
   CALL		0x02B1
    BCLR		0x0006,0x0001
    BSET		0x0006,0x0002
   RETURN 
    BSET		0x0006,0x0002
    BSET		0x0006,0x0001
   CALL		0x02B1
    BCLR		0x0006,0x0001
   RETURN 
    BCLR		0x0006,0x0002
    BSET		0x0006,0x0001
   NOP 
   NOP 
    BCLR		0x0006,0x0001
   NOP 
   NOP 
   RETURN 
    BSET		0x0006,0x0002
    BSET		0x0006,0x0001
   CALL		0x02C3
    BCLR		0x0006,0x0001
   CALL		0x02C3
   RETURN 
    BSET		0x0006,0x0002
    BSET		0x0006,0x0001
   CALL		0x02B1
   MOVAI		0x0A
   MOVRA		0x30
    JBCLR		0x0006,0x0002
   GOTO		0x02F7
   GOTO		0x02F9
   DJZR		0x30
   GOTO		0x02F4
    BCLR		0x0006,0x0001
   CLRR		0x30
   CALL		0x02B1
   RETURN 
   MOVRA		0x31
   MOVAI		0x08
   MOVRA		0x30
   RLR		0x31
    JBCLR		0x0003,0x0000
   GOTO		0x0304
   GOTO		0x0306
    BSET		0x0006,0x0002
   GOTO		0x0307
    BCLR		0x0006,0x0002
    BSET		0x0006,0x0001
   CALL		0x02B1
    BCLR		0x0006,0x0001
   CALL		0x02B1
   DJZR		0x30
   GOTO		0x0300
   RETURN 
    BSET		0x0006,0x0002
   MOVAI		0x08
   MOVRA		0x30
   CLRR		0x31
    BSET		0x0006,0x0001
   CALL		0x02B1
    JBSET		0x0006,0x0002
   GOTO		0x0317
   GOTO		0x031A
    BCLR		0x0003,0x0000
   RLR		0x31
   GOTO		0x031C
    BSET		0x0003,0x0000
   RLR		0x31
    BCLR		0x0006,0x0001
   CALL		0x02B1
   DJZR		0x30
   GOTO		0x0312
   MOVAR		0x31
   MOVRA		0x3A
   RETURN 
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   NOP		
   GOTO		0x0001
	end