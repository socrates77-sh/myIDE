@echo off
set COSMIC_HOME=%~dp0
set CHIP_ID=MC20P22

rem	by  TQB 20121126

if exist MC20P22.lkf (del MC20P22.lkf)

rem %COSMIC_HOME%cx6805 -l -i %COSMIC_HOME%H6805 +debug %1.c	
for /f "tokens=1,* delims=." %%i in ('dir /b *.c') do (
	if 	exist "%%i.o"	del "%%i.o"
echo 编译 %%i.c 程序中... ...
	%COSMIC_HOME%cx6805 -l -i %COSMIC_HOME%H6805 +debug %%i.c	
)
echo #	LINK COMMAND FILE EXAMPLE FOR 68HC05 GP20 		>> MC20P22.lkf
echo #	Copyright (c) 1998 by COSMIC Software			>> MC20P22.lkf
echo #  	MC20P22										>> MC20P22.lkf
echo +seg .text  -b 0x1800 -m0x800 -nCODE -sROM			>> MC20P22.lkf
echo +seg .const -a CODE   -it  -sROM					>> MC20P22.lkf
echo +seg .bsct  -b 0x80   -nZPAGE	-m0xD0 -sRAM 		>> MC20P22.lkf
echo +seg .ubsct -a ZPAGE  -nUZPAGE  -sRAM				>> MC20P22.lkf
echo +seg .share -a UZPAGE -is  -sRAM					>> MC20P22.lkf
echo +seg .data  -a UZPAGE -nIDATA -sRAM 				>> MC20P22.lkf
echo +seg .bss   -a IDATA  -nUDATA -sRAM				>> MC20P22.lkf
for /f "tokens=1,* delims=." %%i in ('dir /b *.o') do (
 	echo %%i.o											>> MC20P22.lkf
)
findstr /v /c:"interrupt_vector.o" MC20P22.lkf > MC20P22.temp
move /y MC20P22.temp MC20P22.lkf

echo %COSMIC_HOME%Lib\crts.h05                          >> MC20P22.lkf
echo %COSMIC_HOME%Lib\libi.h05                          >> MC20P22.lkf
echo %COSMIC_HOME%Lib\libm.h05                          >> MC20P22.lkf
echo +seg .const -b 0x1FEE	# vectors start address		>> MC20P22.lkf
echo interrupt_vector.o		# interrupt vectors			>> MC20P22.lkf

echo +def __memory=@.bss	# symbol used by library	>> MC20P22.lkf



for /f "tokens=1,* delims=." %%i in ('dir /b *.c') do (
if exist "%%i.h05" del "%%i.h05"
if exist "%%i.lst"  del "%%i.lst")

if exist System\%1.sym (del System\%1.sym)
if exist System\%1.m (del System\%1.m)
if exist Debug\%1.cod (del Debug\%1.cod)
if exist %1.s19 (del %1.s19)

if exist %1.o (%COSMIC_HOME%clnk -m System\%1.sym -o System\%1.h05 MC20P22.lkf)

if exist System\%1.h05 (echo 链接 %1.o 程序中... ...
%COSMIC_HOME%chex -o %1.s19 -fm System\%1.h05
%COSMIC_HOME%clabs -slst System\%1.h05 
for /f "tokens=1,* delims=." %%i in ('dir /b *.c') do (
	%COSMIC_HOME%sed -e "s/^[ ]*//" < %%i.lst | %COSMIC_HOME%sed -e "s/^[0-9]\{1,\}//1" | %COSMIC_HOME%sed -e "s/ \{1,\}/\t\t\t\t/" | %COSMIC_HOME%sed -e "/^[\t]*[0-9][0-9a-f]\{3\}[ ]*[0-9a-f]\{2\}/ s/^[\t]*\([0-9a-f]\{4\}\)[ ]*/[1]\t\1\t/" > %%i.log
)
@echo off

if exist log.log del log.log
type *.log >>System\log.log

move System\log.log %1.log

for /f "tokens=1,* delims=." %%i in ('dir /b *.c') do (
	del %%i.lst
)

%COSMIC_HOME%sed "/./=" %1.log | %COSMIC_HOME%sed -e "/./N;s/\n/\t/" | %COSMIC_HOME%sed -e "s/^/L /"> %1.lst
%COSMIC_HOME%cprd System\%1.h05>System\%1.m

for /f "tokens=1,* delims=." %%i in ('dir /b *.c') do (
	del %%i.o
	del %%i.ls
	del %%i.log
)

find /n "mul" < %1.lst>nul&&set mul=yes
 if defined mul (echo ========= 编译器不支持MUL指令,请修改相关乘法程序 =============
 echo ========= 编译 %1.c 程序失败 =============
 del System\%1.h05
 del System\%1.sym
 del Output\%1.s19) else (echo ========= 编译 %1.c 程序成功 =============)
 ) else (echo ========= 编译 %1.c 程序失败 =============)

