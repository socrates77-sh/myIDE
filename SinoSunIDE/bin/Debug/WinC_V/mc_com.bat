@echo off
set COSMIC_HOME=%~dp0

%COSMIC_HOME%cx6805 -l -i %COSMIC_HOME%H6805 +debug %1.c
call %COSMIC_HOME%mc_cfg\%2.bat %1

if exist System\%1.h05 (del System\%1.h05)
if exist %1.lst (del %1.lst)
if exist System\%1.sym (del System\%1.sym)
if exist System\%1.m (del System\%1.m)
if exist Debug\%1.cod (del Debug\%1.cod)
if exist %1.s19 (del %1.s19)

if exist %1.o (
		%COSMIC_HOME%clnk -m System\%1.sym -o System\%1.h05 mc_com.lkf
		)

if exist System\%1.h05 (echo ========= 链接 %1.o 程序中 ============= 
%COSMIC_HOME%chex -o %1.s19 -fm System\%1.h05
%COSMIC_HOME%clabs -slst System\%1.h05 
%COSMIC_HOME%sed -e "s/^[ ]*//" < %1.lst | %COSMIC_HOME%sed -e "s/^[0-9]\{1,\}//1" | %COSMIC_HOME%sed -e "s/ \{1,\}/\t\t\t\t/" | %COSMIC_HOME%sed -e "/^[\t]*[0-9][0-9a-f]\{3\}[ ]*[0-9a-f]\{2\}/ s/^[\t]*\([0-9a-f]\{4\}\)[ ]*/[1]\t\1\t/" > %1.log
%COSMIC_HOME%sed "/./=" %1.log | %COSMIC_HOME%sed -e "/./N;s/\n/\t/" | %COSMIC_HOME%sed -e "s/^/L /"> %1.lst
%COSMIC_HOME%cprd System\%1.h05>System\%1.m
del %1.log
del mc_com.lkf
del %1.ls
del %1.o

if not "%2"=="x222" (
 find /n "mul" < %1.lst>nul&&set mul=yes 
if defined mul (echo ========= 编译器不支持MUL指令 =============
echo ========= 编译 %1.c 程序失败 =============
del System\%1.h05
del System\%1.sym
del %1.s19) else (echo ========= 编译 %1.c 程序成功 =============)
)else (echo 编译成功)
) else (echo ========= 编译 %1.c 程序失败 =============)

 if exist %1.s19 (
 if not exist output md output
 copy %1.s19	output\ 
 if exist %1.lst	(copy %1.lst	output\)
 )
