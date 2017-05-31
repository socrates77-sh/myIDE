/*  math.h: Floating point math function declarations

    Copyright (C) 2001  Jesus Calvino-Fraga, jesusc@ieee.org 

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307 USA 
*/


#ifndef _INC_MATH
#define _INC_MATH

#define PI          3.1415926536
#define TWO_PI      6.2831853071
#define HALF_PI     1.5707963268
#define QUART_PI    0.7853981634
#define iPI         0.3183098862
#define iTWO_PI     0.1591549431
#define TWO_O_PI    0.6366197724

// EPS=B**(-t/2), where B is the radix of the floating-point representation
// and there are t base-B digits in the significand.  Therefore, for floats
// EPS=2**(-12).  Also define EPS2=EPS*EPS.
#define EPS 244.14062E-6
#define EPS2 59.6046E-9
#define XMAX 3.402823466E+38

// for sncc
union __float_char4
{
    float f;
    unsigned char c[4];
};

__reentrant float __asincosf(const float x, const int isacos);
__reentrant float __sincosf(const float x, const int iscos);
__reentrant float __sincoshf(const float x, const int iscosh);
__reentrant float __tancotf(const float x, const int iscot);

/**********************************************
 * Prototypes for float ANSI C math functions *
 **********************************************/

/* Trigonometric functions */
__reentrant float sinf(const float x);
__reentrant float cosf(const float x);
__reentrant float tanf(const float x);
__reentrant float asinf(const float x);
__reentrant float acosf(const float x);
__reentrant float atanf(const float x);
__reentrant float atan2f(const float x, const float y);

/* Hyperbolic functions */
__reentrant float sinhf(const float x);
__reentrant float coshf(const float x);
__reentrant float tanhf(const float x);

/* Exponential, logarithmic and power functions */
__reentrant float expf(const float x);
__reentrant float logf(const float x);
__reentrant float log10f(const float x);
__reentrant float powf(const float x, const float y);
__reentrant float sqrtf(const float a);

/* Nearest integer, absolute value, and remainder functions */
__reentrant float fabsf(const float x);
__reentrant float frexpf(const float x, int *pw2);
__reentrant float ldexpf(const float x, const int pw2);
__reentrant float ceilf(float x);
__reentrant float floorf(float x);
__reentrant float modff(float x, float * y);

__reentrant float fmodf(float x, float y);

__reentrant long mulint(int x, int y);
__reentrant long long mullong(long x, long y);

__reentrant unsigned long muluint(unsigned int x, unsigned int y);
__reentrant unsigned long long mululong(unsigned long x, unsigned long y);

__reentrant unsigned long divulong(unsigned long x,unsigned int y);
__reentrant unsigned long long divudlong(unsigned long long x,unsigned long y);

#endif  /* _INC_MATH */
