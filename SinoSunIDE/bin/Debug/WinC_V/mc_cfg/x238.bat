echo #LINK COMMAND FILE EXAMPLE FOR MC20P38             >  mc_com.lkf
echo +seg .text  -b 0x2003 -m0x1ffd -nCODE   -sROM      >> mc_com.lkf
echo +seg .const -a CODE   -it      -sROM               >> mc_com.lkf
echo +seg .bsct  -b 0x40   -nZPAGE  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .ubsct -b 0x40   -nUZPAGE -m0xa0   -sRAM      >> mc_com.lkf
echo +seg .data  -b 0x40   -nIDATA  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .bss   -b 0x40   -nUDATA  -m0x00   -sRAM      >> mc_com.lkf
echo +seg .share -a UZPAGE -is      -sRAM               >> mc_com.lkf
echo %COSMIC_HOME%Lib\crts.o                            >> mc_com.lkf
echo %1.o                                               >> mc_com.lkf
echo %COSMIC_HOME%Lib\libi.h05                          >> mc_com.lkf
echo %COSMIC_HOME%Lib\libm.h05                          >> mc_com.lkf
echo +seg .const -b 0x3FEC                              >> mc_com.lkf
echo %COSMIC_HOME%Lib\vec_x238.o                        >> mc_com.lkf
