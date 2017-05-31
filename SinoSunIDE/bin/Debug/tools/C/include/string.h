/*-------------------------------------------------------------------------
   string.h - ANSI functions forward declarations    
  
       Written By -  Sandeep Dutta . sandeep.dutta@usa.net (1998)

   This program is free software; you can redistribute it and/or modify it
   under the terms of the GNU General Public License as published by the
   Free Software Foundation; either version 2, or (at your option) any
   later version.
   
   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with this program; if not, write to the Free Software
   Foundation, 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
   
   In other words, you are welcome to use, share and improve this program.
   You are forbidden to forbid anyone else to use, share and improve
   what you give them.   Help stamp out software-hoarding!  
-------------------------------------------------------------------------*/


#ifndef __STRING_H
#define __STRING_H 

#include <stddef.h>

__reentrant __GENERIC char *strcpy(char *, __GENERIC char *);
__reentrant __GENERIC char *strncpy(char *, __GENERIC char *, size_t);
__reentrant __GENERIC char *strcat(char *, __GENERIC char *)  ;
__reentrant __GENERIC char *strncat(char *, __GENERIC char *, size_t);
__reentrant int strcmp(__GENERIC char *, __GENERIC char *)  ;
__reentrant int strncmp(__GENERIC char *, __GENERIC char *, size_t);
__reentrant __GENERIC char *strchr(__GENERIC char *, char);
__reentrant __GENERIC char *strrchr(__GENERIC char *, char);
__reentrant int strspn(__GENERIC char *, __GENERIC char *);
__reentrant int strcspn(__GENERIC char *, __GENERIC char *);
__reentrant __GENERIC char *strpbrk(__GENERIC char *, __GENERIC char *);
__reentrant __GENERIC char *strstr(__GENERIC char *, __GENERIC char *);
__reentrant int strlen(__GENERIC char *);
__reentrant __GENERIC char *strtok(__GENERIC char *,__GENERIC char *);
__reentrant void *memcpy(void *, const void *, size_t);
__reentrant int memcmp(const void *, const void *, size_t);
__reentrant void *memset(void *, unsigned char, size_t);
__reentrant void *memmove(void *, const void *, size_t);
__reentrant __GENERIC char *strerror(int);
__reentrant void *memchr(const void *, int, size_t);

#endif
