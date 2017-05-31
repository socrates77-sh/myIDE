/*	GENERAL LIBRARY FUNCTIONS HEADER
 *	Copyright (c) 1995 by COSMIC Software
 */
#ifndef __STDLIB__
#define __STDLIB__	1

#define EXIT_FAILURE 0
#define EXIT_SUCCESS 1
#define MB_CUR_MAX 1
#define NULL	(void *)0
#define RAND_MAX 32767

#ifndef __SIZE_T__
#define __SIZE_T__	1
typedef unsigned int size_t;
#endif

#ifndef __WCHAR_T__
#define __WCHAR_T__	1
typedef char wchar_t;
#endif

typedef struct {
	int quot;
	int rem;
	} div_t;

typedef struct {
	long quot;
	long rem;
	} ldiv_t;

int abs(int i);
double atof(char *nptr);
int atoi(char *nptr);
long atol(char *nptr);
div_t div(int numer, int denom);
void exit(int status);
long labs(long l);
ldiv_t ldiv(long numer, long denom);
int rand(void);
void srand(unsigned int seed);
double strtod(char *s, char **endptr);
long strtol(char *s, char **endptr, int base);
long xstrtol(@near char *s, @near char **endptr, int base);

#define abort exit
#define strtoul strtol
#define xstrtoul xstrtol

#endif
