echo #LINK COMMAND FILE EXAMPLE FOR x111             >  mc_com.lkf
echo +seg .text  -b 0x1c03 -m0x3fd  -nCODE   -sROM      >> mc_com.lkf
echo +seg .const -a CODE   -it      -sROM               >> mc_com.lkf
echo +seg .bsct  -b 0xe0   -nZPAGE  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .ubsct -b 0xe0   -nUZPAGE -m0x20   -sRAM      >> mc_com.lkf
echo +seg .data  -b 0xe0   -nIDATA  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .bss   -b 0xe0   -nUDATA  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .share -a UZPAGE -is      -sRAM               >> mc_com.lkf
echo %COSMIC_HOME%Lib\crts.h05                          >> mc_com.lkf
echo %1.o                                               >> mc_com.lkf
echo %COSMIC_HOME%Lib\libi.h05                          >> mc_com.lkf
echo %COSMIC_HOME%Lib\libm.h05                          >> mc_com.lkf
echo +seg .const -b 0x1FF4                              >> mc_com.lkf
echo %COSMIC_HOME%Lib\vec_x111.o                     >> mc_com.lkf
