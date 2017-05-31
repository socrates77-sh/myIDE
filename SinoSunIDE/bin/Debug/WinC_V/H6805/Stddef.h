/*	STANDARD DEFINITIONS HEADER
 *	Copyright (c) 1995 by COSMIC Software
 */
#ifndef __STDDEF__
#define __STDDEF__	1

#ifndef __SIZE_T__
#define __SIZE_T__	1
typedef unsigned int size_t;
#endif

#ifndef __WCHAR_T__
#define __WCHAR_T__	1
typedef char wchar_t;
#endif

typedef int ptrdiff_t;

#define NULL	(void *)0
#define offsetof(styp, mbr) ((size_t)&(((styp *)0)->mbr))

#endif
