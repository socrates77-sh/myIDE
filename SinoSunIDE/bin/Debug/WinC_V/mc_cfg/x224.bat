echo #LINK COMMAND FILE EXAMPLE FOR MC20P24             >  mc_com.lkf
echo +seg .text  -b 0x1003 -m0xffd -nCODE   -sROM      >> mc_com.lkf
echo +seg .const -a CODE   -it      -sROM               >> mc_com.lkf
echo +seg .bsct  -b 0x30   -nZPAGE  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .ubsct -b 0x30   -nUZPAGE -m0x90   -sRAM      >> mc_com.lkf
echo +seg .data  -b 0x30   -nIDATA  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .bss   -b 0x30   -nUDATA  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .share -a UZPAGE -is      -sRAM               >> mc_com.lkf
rem echo %COSMIC_HOME%Lib\crts.h05                          >> mc_com.lkf
echo %COSMIC_HOME%Lib\crts.o                            >> mc_com.lkf
echo %1.o                                               >> mc_com.lkf
rem echo %COSMIC_HOME%Lib\libi.h05                          >> mc_com.lkf
echo %COSMIC_HOME%Lib\libm.h05                          >> mc_com.lkf
echo +seg .const -b 0x1FF4                              >> mc_com.lkf
echo %COSMIC_HOME%Lib\vec_x224.o                     >> mc_com.lkf
