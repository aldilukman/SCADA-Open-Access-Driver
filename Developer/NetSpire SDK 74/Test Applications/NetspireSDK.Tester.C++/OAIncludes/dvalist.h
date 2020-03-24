/* //////1/////////2/////////3/////////4/////////5/////////6/////////7/////// */
/* * * * * * * * * * * D V A  L I S T S T R U C T U R E  * * * * * * * * * * **/
/* //////1/////////2/////////3/////////4/////////5/////////6/////////7/////// */

/*
 * DVAlist.h
 *
 *	structure definitions for dva application doubly linked lists
 */

/* SCCSid: @(#)list.h	1.2 */

#ifdef __cplusplus
extern "C" {
#endif

#ifndef _DVALIST
#define _DVALIST	1

#ifndef _DVADLL
#include <oa/oatypes.h>
#endif

#ifndef DVA_ASSERT
#define DVA_ASSERT( a ) assert( a )
#endif

#ifndef DVA_LIST_ASSERT
#define DVA_LIST_ASSERT( a ) DVA_ASSERT( a->magic == DVA_LIST_MAGIC )
#endif

#ifndef SL_LIST_ASSERT
# ifdef DEBUG_DVALIST
#  define SL_LIST_ASSERT( a ) DVA_ASSERT( a->magic == (char) SL_LIST_MAGIC )
# else
#  define SL_LIST_ASSERT( a )
# endif
#endif

#ifndef SL_NODE_ASSERT
# ifdef DEBUG_DVALIST
#  define SL_NODE_ASSERT( a ) DVA_ASSERT( a->magic == (char) SL_NODE_MAGIC )
# else
#  define SL_NODE_ASSERT( a )
# endif
#endif

#ifndef DVA_NODE_ASSERT
#define DVA_NODE_ASSERT( a ) DVA_ASSERT( ( a == ( DVAnode *) 0 ) || ( a->ourList->magic == DVA_LIST_MAGIC ) )
#endif

#define SL_LIST_MAGIC  0x81
#define SL_NODE_MAGIC  0x66
#define DVA_LIST_MAGIC 0x54
#define DVA_LIST_HEAP_MAGIC 0x18

#define DVA_LIST_DEF_TYPE struct DVAlist
#define DVA_NODE_DEF_TYPE struct DVAnode

/*
 * List internal errno support. 
 */

#define	DVA_LE_DUPLICATE	 30	/* duplicate insertion */
#define	DVA_LE_LIMIT		 31	/* list limit exceeded */
#define	DVA_LE_LIST		 32	/* node missing list ptr. */
#define	DVA_LE_ARG		 33	/* invalid argument */
#define	DVA_LE_SORT		 34	/* sort function returned invalid value */

#define	DVA_ADD_POS_TOP           1    
#define	DVA_ADD_POS_SORT          2    
#define	DVA_ADD_POS_BOT           3    

#define	DVA_TOP_LIST	0
#define	DVA_BOT_LIST	1
#define	DVA_DOWN_LIST	0
#define	DVA_UP_LIST	1

DVA_LIST_DEF_TYPE
{
	DVA_NODE_DEF_TYPE *listTop;
	DVA_NODE_DEF_TYPE *listBot;
	unsigned long elements;
	int (*sortFunction)( void *, void *, int);
	DVA_NODE_DEF_TYPE *(*defaultDeleteNodeFn)( DVA_NODE_DEF_TYPE *, int );
	long dataLen;
	char bufId;  
	char duplicates;
	char defaultAddNodePos;
#ifndef NDEBUG_REMOVEMAGIC
	char magic;
#endif
};
 
typedef DVA_LIST_DEF_TYPE DVAlist;

DVA_NODE_DEF_TYPE
{
	DVAlist *ourList;
	DVA_NODE_DEF_TYPE *nextNode;
	DVA_NODE_DEF_TYPE *prevNode;
	void *dataPtr;
};

typedef DVA_NODE_DEF_TYPE DVAnode;



struct  SLlist
{
	struct SLnode *listTop;
	struct SLnode *listBot;
	unsigned long elements;
	unsigned long dataLen;
	struct SLlist *freeList;
#ifdef DEBUG_DVALIST
	char magic;
#endif
};

typedef struct SLlist  SLlist;

struct SLnode
{
	struct SLnode *nextNode;
#ifdef DEBUG_DVALIST
	char magic;
#endif
};

typedef struct SLnode  SLnode;



/*
 * Function prototypes.
 */

/* Singly linked minimal list with freenode pool */

SLlist *SLinitList( int dataLen, char useNodePool );
void    SLfreePoolAlloc( void );
SLlist *SLdelList( SLlist *theList, SLnode *(*)(SLlist *, SLnode *) );
SLnode *SLaddToListBot( SLlist *theList, void *data );
SLnode *SLaddNodeToListBot( SLlist *theList, SLnode *theNode );
SLlist *SLinitFreeList( int dataLen );
SLlist *SLincFreeList( SLlist *theList );
SLnode *SLdelNode( SLlist *theList, SLnode *theNode );
SLnode *SLunlinkNode( SLlist *theList, SLnode *theNode );
SLnode *SLdoToAllList( SLlist *theList, SLnode *(*theFunction)( SLlist *, SLnode * ) );

/* Double linked list  */

DVAlist *InitList( DVAnode *(*)(DVAlist *, void *, unsigned long),int (*)( void *,void *, int), BOOL, int );
DVAnode *AddToListTop( DVAlist *, void *, unsigned long );
DVAnode *AddToListBot( DVAlist *, void *, unsigned long );
DVAnode *AddToListSort( DVAlist *, void *, unsigned long );
DVAnode *AddToListSortBot( DVAlist *, void *, unsigned long );
DVAnode *MakeNode( void *, unsigned long, int );
DVAnode *AddToList( DVAlist *, void *, unsigned long );
DVAnode *AddToListCurr( DVAnode *, void *, unsigned long, BOOL );
DVAnode *LinkNode( DVAnode *, DVAnode * );
DVAnode *UnlinkNode( DVAnode * );
void     DelList( DVAlist * );
DVAnode *DelNode( DVAnode *, int );
DVAnode *SearchList( DVAlist *, void *, unsigned long );
DVAnode *SearchListBySortFunction( DVAlist *, void *, unsigned long );
DVAnode *GoToNode( DVAlist *, int, int );
DVAnode *DoToAllList( DVAlist *, DVAnode *(*)(DVAnode *, int) );
DVAnode *AddNodeToListTop( DVAlist *, DVAnode * );
DVAnode *AddNodeToListBot( DVAlist *, DVAnode * );
DVAnode *AddNodeToListCurr( DVAnode *, DVAnode *, int );
DVAnode *AddNodeToListSort( DVAlist *, DVAnode * );
DVAnode *ReturnFreeNode( DVAnode *, int );
int CopyList( DVAlist *, DVAlist * );
int MoveList( DVAlist *, DVAlist * );
DVAnode *MoveNode( DVAlist *, DVAnode * );

DVAlist *virtualiseList( DVAlist *, long );
int unvirtualiseList( DVAlist *, char *, int );

/* standard list macro definitions  */

#define	DVA_SET_DEL_NODE( a, b )		((a)->defaultDeleteNodeFn = (b))
#define	DVA_SET_LIST_TYPE( a, b )		((a)->type = (b))
#define	DVA_DEL_NODE( a )			((a)->ourList->defaultDeleteNodeFn(a, (a)->ourList->bufId ) )
#define	DVA_LIST_SIZE( a )			((a)->elements)
#define	DVA_LIST_TOP( a )			((a)->listTop)
#define	DVA_LIST_BOT( a )			((a)->listBot)
#define	DVA_DEL_TOP( a )	if( DVA_LIST_TOP( (a) ) ) DVA_DEL_NODE( DVA_LIST_TOP((a)) ) 
#define	DVA_DEL_BOT( a )	if( DVA_LIST_BOT( (a) ) ) DVA_DEL_NODE( DVA_LIST_BOT((a)) ) 
#define	DVA_CLEAR_LIST( a )		DoToAllList( (a), (a)->defaultDeleteNodeFn )
#define	DVA_NEXT_NODE( a )		(a)->nextNode
#define	DVA_PREV_NODE( a )		(a)->prevNode

#define DVA_NODE_DATA( a )              (a->dataPtr)
#define SL_NODE_DATA( a )               ((int) (a) + sizeof( SLnode ) )
#define SL_NODE_FROM_DATA_PTR( a )		((SLnode*)((int) (a) - sizeof( SLnode ) ))

#endif 

#ifdef __cplusplus
}
#endif
