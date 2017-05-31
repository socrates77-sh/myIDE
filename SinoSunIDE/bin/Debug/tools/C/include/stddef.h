#ifndef __STDDEF_H
#define __STDDEF_H

/* Offset of member MEMBER in a struct of type TYPE.  */
#define offsetof(TYPE, MEMBER) ((size_t) (unsigned)&((TYPE *)0)->MEMBER)

#ifndef NULL
    #define NULL (void *)0
#endif

typedef unsigned int ptrdiff_t;
typedef unsigned int size_t;

#endif
