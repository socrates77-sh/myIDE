
#ifndef _INC_STDARG
#define _INC_STDARG

typedef char * va_list; 

// sizeof(T) returns the number of byte
#define va_start(ap,v)	( ap = (va_list)&v + sizeof(v))
#define va_arg(ap,t)    ( *(t *)((ap += sizeof(t)) - sizeof(t))  )
#define va_end(ap)	( ap = (va_list)0 )

#endif	/* _INC_STDARG */
