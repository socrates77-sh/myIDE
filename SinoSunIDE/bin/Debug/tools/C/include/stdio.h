#ifndef __STDIO_H__
#define __STDIO_H__

#include <stdarg.h>

__reentrant int putchar(int );
__reentrant int printf(__GENERIC char *,...);
__reentrant int vsprintf(__GENERIC char *, __GENERIC char *, va_list);
__reentrant int sprintf(char *buf, __GENERIC char *fmt, ...);
#endif
