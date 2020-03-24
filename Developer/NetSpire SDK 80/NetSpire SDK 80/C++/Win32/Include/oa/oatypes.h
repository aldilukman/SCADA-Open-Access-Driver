/************************************************************************
 *									*
 *	oatype.h - generic type definitions.				*
 *									*
 ************************************************************************/

/* SCCSid: @(#)oatypes.h	1.1 */


#ifndef _OA_TYPES
#define	_OA_TYPES

#include <sys/types.h>

#if (!defined(WIN32) || _MSC_VER >= 1400)
#include <stdint.h>
#else
//visual studio 6
typedef signed char int8_t;
typedef short int16_t;
typedef int int32_t;

typedef unsigned char uint8_t;
typedef unsigned short uint16_t;
typedef unsigned int uint32_t;
#endif

#ifdef WIN32
//typedef int socklen_t;
//typedef unsigned int socket_type;
#else
//typedef int socket_type;
#endif

#ifndef BOOL_DECLARED
typedef int         BOOL ;
#define BOOL_DECLARED
#endif

#ifndef	NO
#define	NO	0
#endif

#ifndef YES
#define	YES	1
#endif

#ifndef TRUE
#define TRUE 1
#endif

#ifndef FALSE
#define FALSE 0
#endif

#define OA_ERR -1
#define GOOD 0

#define OA_STDOUT 1
#define OA_STDERR 2




#ifndef __STDC__
#ifndef _STDC_
#ifndef _NCR
typedef unsigned short  ushort ;
typedef unsigned int    uint ;
typedef unsigned long   ulong ;
#endif
#endif
#endif

#ifdef M_UNIX
typedef unsigned long   ulong ;
#endif

#ifdef QNX
typedef unsigned long	ulong;
typedef unsigned short	ushort;
#endif

#ifdef WIN32
typedef unsigned short ushort;
typedef unsigned long	ulong;
#endif

#ifdef NDK
typedef unsigned short ushort;
typedef unsigned long  ulong;
#endif

typedef unsigned char   uchar ;
typedef unsigned short  USHORT ;
typedef unsigned int    UINT ;
typedef unsigned long   ULONG ;
typedef unsigned char   UCHAR ;


/* May be needed for gcc version >2.95 or egcs on UW7
#if defined UNIXWARE7 && defined __GNUC__ && !defined _BOOLEAN_T
#define _BOOLEAN_T
typedef enum boolean { B_FALSE, B_TRUE } boolean_t;
#endif
*/

/*
typedef unsigned char   BYTE ;
typedef unsigned long   DWORD; 
*/

#endif
