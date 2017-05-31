/*	STRING TYPES HEADER
 *	Copyright (c) 1995 by COSMIC Software
 */

#ifndef __STRING__
#define __STRING__	1

/*	function declarations
 */
char *strcat(char *s1, char *s2);
char *strchr(char *s, char c);
char *strcpy(char *s1, char *s2);
char *strncat(char *s1, char *s2, unsigned char n);
char *strncpy(char *s1, char *s2, unsigned char n);
char *strpbrk(char *s1, char *s2);
char *strrchr(char *s, char c);
char *strstr(char *s1, char *s2);
int memcmp(void *s1, void *s2, unsigned char n);
int strcmp(char *s1, char *s2);
int strncmp(char *s1, char *s2, unsigned char n);
unsigned int strcspn(char *s1, char *s2);
unsigned int strlen(char *s);
unsigned int strspn(char *s1, char *s2);
void *memchr(void *s, char c, unsigned char n);
void *memcpy(void *s1, void *s2, unsigned char n);
void *memmove(void *s1, void *s2, unsigned char n);
void *memset(void *s, char c, unsigned char n);

@near char *xstrcat(@near char *s1, @near char *s2);
@near char *xstrchr(@near char *s, char c);
@near char *xstrcpy(@near char *s1, @near char *s2);
@near char *xstrncat(@near char *s1, @near char *s2, unsigned int n);
@near char *xstrncpy(@near char *s1, @near char *s2, unsigned int n);
@near char *xstrpbrk(@near char *s1, @near char *s2);
@near char *xstrrchr(@near char *s, char c);
@near char *xstrstr(@near char *s1, @near char *s2);
int xmemcmp(@near void *s1, @near void *s2, unsigned int n);
int xstrcmp(@near char *s1, @near char *s2);
int xstrncmp(@near char *s1, @near char *s2, unsigned int n);
unsigned int xstrcspn(@near char *s1, @near char *s2);
unsigned int xstrlen(@near char *s);
unsigned int xstrspn(@near char *s1, @near char *s2);
@near void *xmemchr(@near void *s, char c, unsigned int n);
@near void *xmemcpy(@near void *s1, @near void *s2, unsigned int n);
@near void *xmemmove(@near void *s1, @near void *s2, unsigned int n);
@near void *xmemset(@near void *s, char c, unsigned int n);

#endif
