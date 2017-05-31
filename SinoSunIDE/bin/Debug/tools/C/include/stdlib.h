/*-------------------------------------------------------------------------
  stdlib.h - ANSI functions forward declarations

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

#ifndef __STDLIB_H
#define __STDLIB_H

#include <stddef.h>

typedef struct
{
    int quot;
    int rem;
} div_t;

typedef struct
{
    long quot;
    long rem;
} ldiv_t;


__reentrant float atof(__GENERIC char *);
__reentrant int atoi(__GENERIC char *);
__reentrant long atol(__GENERIC char *);
__reentrant int abs(int);
__reentrant long labs(long);
__reentrant div_t div(int, int);
__reentrant ldiv_t ldiv(long, long);
__reentrant int rand(void);
__reentrant void srand(unsigned int);
__reentrant long strtol(__GENERIC char *, __GENERIC char **, int); 
__reentrant unsigned long strtoul(__GENERIC char *, __GENERIC char **, int); 
#endif
