/*	PROGRAM ASSERTION HEADER
 *	Copyright (c) 1995 by COSMIC Software
 */
#undef assert
#ifndef NDEBUG
#define assert(expr) \
	{ \
	if (!(expr)) \
		{ \
		printf("\nassert error at %s:%d\n", \
			__FILE__, __LINE__), \
		exit(); \
		} \
	}
#else
#define assert(expr)
#endif
