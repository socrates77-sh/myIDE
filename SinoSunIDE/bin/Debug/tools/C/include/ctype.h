/*-------------------------------------------------------------------------
  ctype.h - ANSI functions forward declarations
 
             Written By -  Sandeep Dutta . sandeep.dutta@usa.net (1998)

   Revisions:
   1.0 - June.1.2000 1.0 - Bela Torok / bela.torok@kssg.ch
   order: function definitions -> macros
   corretced macro: isalpha(c)  
   added macros: _tolower(c), _toupper(c), tolower(c), toupper(c) toascii(c)


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

#ifndef __CTYPE_H
#define __CTYPE_H

__reentrant char iscntrl( unsigned char );
__reentrant char isdigit( unsigned char );
__reentrant char isgraph( unsigned char );
__reentrant char islower( unsigned char );
__reentrant char isupper( unsigned char );
__reentrant char isprint( unsigned char );
__reentrant char ispunct( unsigned char );
__reentrant char isspace( unsigned char );
__reentrant char isxdigit( unsigned char );

#define isalnum(c)   (isalpha(c) || isdigit(c))
#define isalpha(c)   (isupper(c) || islower(c))

/* ANSI versions of _tolower & _toupper
#define _tolower(c)  ((c) - ('a' - 'A'))
#define _toupper(c)  ((c) + ('a' - 'A'))
*/

// The _tolower & _toupper functions below can applied to any 
// alpha characters regardless of the case (upper or lower)
#define _tolower(c)  ((c) | ('a' - 'A'))
#define _toupper(c)  ((c) & ~('a' - 'A'))

#define tolower(c)  ((isupper(c)) ? _tolower(c) : (c))
#define toupper(c)  ((islower(c)) ? _toupper(c) : (c))
#define toascii(c)  ((c) & 0x7F)

#endif

