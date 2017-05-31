
C source file G:\Emulator\IDE\SinoSunIDE\SinoSunIDE\bin\Debug\WinC_V\H6805\reg_mc10p11.h:

unsigned char KEYL at 0x0000
union mcr at 0x0001
union ior at 0x0002

C source file mc10p11_c.c:

union bit_char work_flag at 0x00ea
union bit_int key_buffer at 0x00e8
unsigned char key_value at 0x00e7
unsigned char key_temp at 0x00e6
unsigned char key_bak at 0x00e5
unsigned char key_data at 0x00e4
unsigned char key_table[112] at 0x1f03

void delay_100us() lines 92 to 102 at 0x1c0d-0x1c1b
    static argument unsigned char time_100us at 0x00ed

void delay_1ms() lines 104 to 111 at 0x1c1c-0x1c2a
    static argument unsigned char time_1ms at 0x00ec

void sys_init() lines 115 to 124 at 0x1c2b-0x1c3b
    (no locals)

void read_key() lines 130 to 142 at 0x1c3c-0x1c53
    static argument unsigned char j at 0x00ec

void scan_key() lines 144 to 167 at 0x1c54-0x1c8c
    static auto unsigned char i at 0x00eb

void key_deal() lines 172 to 198 at 0x1c8d-0x1cd1
    (no locals)

void send_head() lines 201 to 213 at 0x1cd2-0x1d0c
    static auto unsigned char j at 0x00ec

void send_byte() lines 218 to 249 at 0x1d0d-0x1d8c
    static argument unsigned char send_data at 0x00ed
    static argument unsigned char count at 0x00ee
    static auto unsigned char j at 0x00ec

void send_tr_bit() lines 252 to 275 at 0x1d8d-0x1dfd
    static auto unsigned char j at 0x00ec

void send_byte_3010() lines 277 to 308 at 0x1dfe-0x1e71
    static argument unsigned char send_data at 0x00ec
    static argument unsigned char count at 0x00ed
    register unsigned char j at register x

void send_code() lines 312 to 349 at 0x1e72-0x1ee8
    static auto unsigned char send_data_temp at 0x00eb

void main() lines 354 to 375 at 0x1ee9-0x1efe
    (no locals)

void SWI_ISR() lines 377 to 380 at 0x1eff-0x1eff
    (no locals)

void KBI_ISR() lines 382 to 385 at 0x1f00-0x1f02
    (no locals)
