echo #LINK COMMAND FILE EXAMPLE FOR x268             >  mc_com.lkf
echo +seg .text  -b 0x2003 -m0x7fd  -nCODE   -sROM      >> mc_com.lkf
echo +seg .const -a CODE   -it      -sROM               >> mc_com.lkf
echo +seg .bsct  -b 0x20   -nZPAGE  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .ubsct -b 0x20   -nUZPAGE -m0x140  -sRAM      >> mc_com.lkf
echo +seg .data  -b 0x20   -nIDATA  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .bss   -b 0x20   -nUDATA  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .share -a UZPAGE -is      -sRAM               >> mc_com.lkf
echo %COSMIC_HOME%Lib\crts.h05                          >> mc_com.lkf
echo %1.o                                               >> mc_com.lkf
echo %COSMIC_HOME%Lib\libi.h05                          >> mc_com.lkf
echo %COSMIC_HOME%Lib\libm.h05                          >> mc_com.lkf
echo %COSMIC_HOME%Lib\mylibc				>> mc_com.lkf
echo +seg .const -b 0x3FEC                              >> mc_com.lkf
echo %COSMIC_HOME%Lib\vec_x268.o                     >> mc_com.lkf