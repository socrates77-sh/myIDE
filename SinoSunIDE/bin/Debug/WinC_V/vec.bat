@echo off
set MoWinC=%~dp0

echo please input chip name
set /p chip=

%MoWinC%cx6805 -i %MoWinC%H6805 %MoWinC%H6805\vec_%chip%.c

move %MoWinC%H6805\vec_%chip%.o %MoWinC%LIB\vec_%chip%.o

pause

