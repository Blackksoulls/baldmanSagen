/* Generated by Nim Compiler v0.18.0 */
/*   (c) 2018 Andreas Rumpf */
/* The generated code is subject to the original license. */
/* Compiled for: Linux, amd64, gcc */
/* Command for C compiler:
   gcc -c -w -O3 -fno-strict-aliasing  -I/home/kuro/.choosenim/toolchains/nim-0.18.0/lib -o /home/kuro/Projects/PTUT/baldmanSagen/config/nimcache/stdlib_heapqueue.o /home/kuro/Projects/PTUT/baldmanSagen/config/nimcache/stdlib_heapqueue.c */
#define NIM_NEW_MANGLING_RULES
#define NIM_INTBITS 64

#include "nimbase.h"
#include <string.h>
#undef LANGUAGE_C
#undef MIPSEB
#undef MIPSEL
#undef PPC
#undef R3000
#undef R4000
#undef i386
#undef linux
#undef mips
#undef near
#undef powerpc
#undef unix
typedef struct TNimType TNimType;
typedef struct TNimNode TNimNode;
typedef struct tySequence_tCXsCfAd03XtyGJVg3kHhg tySequence_tCXsCfAd03XtyGJVg3kHhg;
typedef struct tyTuple_h8SeyS9aRwdD6lpWqt9anV3A tyTuple_h8SeyS9aRwdD6lpWqt9anV3A;
typedef struct tyObject_FuturecolonObjectType__SmxCgsot45ayPNDBegkWAg tyObject_FuturecolonObjectType__SmxCgsot45ayPNDBegkWAg;
typedef struct TGenericSeq TGenericSeq;
typedef struct tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g;
typedef struct tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w;
typedef struct tyObject_GcHeap_1TRH1TZMaVZTnLNcIHuNFQ tyObject_GcHeap_1TRH1TZMaVZTnLNcIHuNFQ;
typedef struct tyObject_GcStack_7fytPA5bBsob6See21YMRA tyObject_GcStack_7fytPA5bBsob6See21YMRA;
typedef struct tyObject_MemRegion_x81NhDv59b8ercDZ9bi85jyg tyObject_MemRegion_x81NhDv59b8ercDZ9bi85jyg;
typedef struct tyObject_SmallChunk_tXn60W2f8h3jgAYdEmy5NQ tyObject_SmallChunk_tXn60W2f8h3jgAYdEmy5NQ;
typedef struct tyObject_BigChunk_Rv9c70Uhp2TytkX7eH78qEg tyObject_BigChunk_Rv9c70Uhp2TytkX7eH78qEg;
typedef struct tyObject_LLChunk_XsENErzHIZV9bhvyJx56wGw tyObject_LLChunk_XsENErzHIZV9bhvyJx56wGw;
typedef struct tyObject_IntSet_EZObFrE3NC9bIb3YMkY9crZA tyObject_IntSet_EZObFrE3NC9bIb3YMkY9crZA;
typedef struct tyObject_Trunk_W0r8S0Y3UGke6T9bIUWnnuw tyObject_Trunk_W0r8S0Y3UGke6T9bIUWnnuw;
typedef struct tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw;
typedef struct tyObject_HeapLinks_PDV1HBZ8CQSQJC9aOBFNRSg tyObject_HeapLinks_PDV1HBZ8CQSQJC9aOBFNRSg;
typedef struct tyTuple_ujsjpB2O9cjj3uDHsXbnSzg tyTuple_ujsjpB2O9cjj3uDHsXbnSzg;
typedef struct tyObject_GcStat_0RwLoVBHZPfUAcLczmfQAg tyObject_GcStat_0RwLoVBHZPfUAcLczmfQAg;
typedef struct tyObject_CellSet_jG87P0AI9aZtss9ccTYBIISQ tyObject_CellSet_jG87P0AI9aZtss9ccTYBIISQ;
typedef struct tyObject_PageDesc_fublkgIY4LG3mT51LU2WHg tyObject_PageDesc_fublkgIY4LG3mT51LU2WHg;
typedef struct tyObject_FutureBasecolonObjectType__cnXnCCtV9cjKaEq9alHheOFg tyObject_FutureBasecolonObjectType__cnXnCCtV9cjKaEq9alHheOFg;
typedef struct RootObj RootObj;
typedef struct tyObject_CallbackList_tKSBWiaJMWD3JZxwqg7UFQ tyObject_CallbackList_tKSBWiaJMWD3JZxwqg7UFQ;
typedef struct Exception Exception;
typedef struct NimStringDesc NimStringDesc;
typedef struct tyObject_BaseChunk_Sdq7WpT6qAH858F5ZEdG3w tyObject_BaseChunk_Sdq7WpT6qAH858F5ZEdG3w;
typedef struct tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ;
typedef struct tySequence_uB9b75OUPRENsBAu4AnoePA tySequence_uB9b75OUPRENsBAu4AnoePA;
typedef struct tyObject_StackTraceEntry_oLyohQ7O2XOvGnflOss8EA tyObject_StackTraceEntry_oLyohQ7O2XOvGnflOss8EA;
typedef NU8 tyEnum_TNimKind_jIBKr1ejBgsfM33Kxw4j7A;
typedef NU8 tySet_tyEnum_TNimTypeFlag_v8QUszD1sWlSIWZz7mC4bQ;
typedef N_NIMCALL_PTR(void, tyProc_ojoeKfW4VYIm36I9cpDTQIg) (void* p, NI op);
typedef N_NIMCALL_PTR(void*, tyProc_WSm2xU5ARYv9aAR4l0z9c9auQ) (void* p);
struct TNimType {
NI size;
tyEnum_TNimKind_jIBKr1ejBgsfM33Kxw4j7A kind;
tySet_tyEnum_TNimTypeFlag_v8QUszD1sWlSIWZz7mC4bQ flags;
TNimType* base;
TNimNode* node;
void* finalizer;
tyProc_ojoeKfW4VYIm36I9cpDTQIg marker;
tyProc_WSm2xU5ARYv9aAR4l0z9c9auQ deepcopy;
};
typedef NU8 tyEnum_TNimNodeKind_unfNsxrcATrufDZmpBq4HQ;
struct TNimNode {
tyEnum_TNimNodeKind_unfNsxrcATrufDZmpBq4HQ kind;
NI offset;
TNimType* typ;
NCSTRING name;
NI len;
TNimNode** sons;
};
struct tyTuple_h8SeyS9aRwdD6lpWqt9anV3A {
NF Field0;
tyObject_FuturecolonObjectType__SmxCgsot45ayPNDBegkWAg* Field1;
};
struct TGenericSeq {
NI len;
NI reserved;
};
typedef NU8 tyEnum_TNimTypeFlag_v8QUszD1sWlSIWZz7mC4bQ;
typedef NU8 tyEnum_WalkOp_Wfy7gT5VQ8B3aJBxqU8D9cQ;
struct tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g {
NI refcount;
TNimType* typ;
};
struct tyObject_GcStack_7fytPA5bBsob6See21YMRA {
void* bottom;
};
struct tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w {
NI len;
NI cap;
tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g** d;
};
typedef tyObject_SmallChunk_tXn60W2f8h3jgAYdEmy5NQ* tyArray_SiRwrEKZdLgxqz9a9aoVBglg[512];
typedef NU32 tyArray_BHbOSqU1t9b3Gt7K2c6fQig[24];
typedef tyObject_BigChunk_Rv9c70Uhp2TytkX7eH78qEg* tyArray_N1u1nqOgmuJN9cSZrnMHgOQ[32];
typedef tyArray_N1u1nqOgmuJN9cSZrnMHgOQ tyArray_B6durA4ZCi1xjJvRtyYxMg[24];
typedef tyObject_Trunk_W0r8S0Y3UGke6T9bIUWnnuw* tyArray_lh2A89ahMmYg9bCmpVaplLbA[256];
struct tyObject_IntSet_EZObFrE3NC9bIb3YMkY9crZA {
tyArray_lh2A89ahMmYg9bCmpVaplLbA data;
};
typedef tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw* tyArray_0aOLqZchNi8nWtMTi8ND8w[2];
struct tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw {
tyArray_0aOLqZchNi8nWtMTi8ND8w link;
NI key;
NI upperBound;
NI level;
};
struct tyTuple_ujsjpB2O9cjj3uDHsXbnSzg {
tyObject_BigChunk_Rv9c70Uhp2TytkX7eH78qEg* Field0;
NI Field1;
};
typedef tyTuple_ujsjpB2O9cjj3uDHsXbnSzg tyArray_LzOv2eCDGiceMKQstCLmhw[30];
struct tyObject_HeapLinks_PDV1HBZ8CQSQJC9aOBFNRSg {
NI len;
tyArray_LzOv2eCDGiceMKQstCLmhw chunks;
tyObject_HeapLinks_PDV1HBZ8CQSQJC9aOBFNRSg* next;
};
struct tyObject_MemRegion_x81NhDv59b8ercDZ9bi85jyg {
NI minLargeObj;
NI maxLargeObj;
tyArray_SiRwrEKZdLgxqz9a9aoVBglg freeSmallChunks;
NU32 flBitmap;
tyArray_BHbOSqU1t9b3Gt7K2c6fQig slBitmap;
tyArray_B6durA4ZCi1xjJvRtyYxMg matrix;
tyObject_LLChunk_XsENErzHIZV9bhvyJx56wGw* llmem;
NI currMem;
NI maxMem;
NI freeMem;
NI occ;
NI lastSize;
tyObject_IntSet_EZObFrE3NC9bIb3YMkY9crZA chunkStarts;
tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw* root;
tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw* deleted;
tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw* last;
tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw* freeAvlNodes;
NIM_BOOL locked;
NIM_BOOL blockChunkSizeIncrease;
NI nextChunkSize;
tyObject_AvlNode_IaqjtwKhxLEpvDS9bct9blEw bottomData;
tyObject_HeapLinks_PDV1HBZ8CQSQJC9aOBFNRSg heapLinks;
};
struct tyObject_GcStat_0RwLoVBHZPfUAcLczmfQAg {
NI stackScans;
NI cycleCollections;
NI maxThreshold;
NI maxStackSize;
NI maxStackCells;
NI cycleTableSize;
NI64 maxPause;
};
struct tyObject_CellSet_jG87P0AI9aZtss9ccTYBIISQ {
NI counter;
NI max;
tyObject_PageDesc_fublkgIY4LG3mT51LU2WHg* head;
tyObject_PageDesc_fublkgIY4LG3mT51LU2WHg** data;
};
struct tyObject_GcHeap_1TRH1TZMaVZTnLNcIHuNFQ {
tyObject_GcStack_7fytPA5bBsob6See21YMRA stack;
NI cycleThreshold;
tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w zct;
tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w decStack;
tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w tempStack;
NI recGcLock;
tyObject_MemRegion_x81NhDv59b8ercDZ9bi85jyg region;
tyObject_GcStat_0RwLoVBHZPfUAcLczmfQAg stat;
tyObject_CellSet_jG87P0AI9aZtss9ccTYBIISQ marked;
tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w additionalRoots;
NI gcThreadId;
};
struct RootObj {
TNimType* m_type;
};
typedef struct {
N_NIMCALL_PTR(void, ClP_0) (void* ClE_0);
void* ClE_0;
} tyProc_IIomJ6ptE6vfJ5zRbATgkQ;
struct tyObject_CallbackList_tKSBWiaJMWD3JZxwqg7UFQ {
tyProc_IIomJ6ptE6vfJ5zRbATgkQ function;
tyObject_CallbackList_tKSBWiaJMWD3JZxwqg7UFQ* next;
};
struct NimStringDesc {
  TGenericSeq Sup;
NIM_CHAR data[SEQ_DECL_SIZE];
};
struct tyObject_FutureBasecolonObjectType__cnXnCCtV9cjKaEq9alHheOFg {
  RootObj Sup;
tyObject_CallbackList_tKSBWiaJMWD3JZxwqg7UFQ callbacks;
NIM_BOOL finished;
Exception* error;
NimStringDesc* errorStackTrace;
};
struct tyObject_FuturecolonObjectType__SmxCgsot45ayPNDBegkWAg {
  tyObject_FutureBasecolonObjectType__cnXnCCtV9cjKaEq9alHheOFg Sup;
};
struct tyObject_BaseChunk_Sdq7WpT6qAH858F5ZEdG3w {
NI prevSize;
NI size;
};
struct tyObject_SmallChunk_tXn60W2f8h3jgAYdEmy5NQ {
  tyObject_BaseChunk_Sdq7WpT6qAH858F5ZEdG3w Sup;
tyObject_SmallChunk_tXn60W2f8h3jgAYdEmy5NQ* next;
tyObject_SmallChunk_tXn60W2f8h3jgAYdEmy5NQ* prev;
tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ* freeList;
NI free;
NI acc;
NF data;
};
struct tyObject_BigChunk_Rv9c70Uhp2TytkX7eH78qEg {
  tyObject_BaseChunk_Sdq7WpT6qAH858F5ZEdG3w Sup;
tyObject_BigChunk_Rv9c70Uhp2TytkX7eH78qEg* next;
tyObject_BigChunk_Rv9c70Uhp2TytkX7eH78qEg* prev;
NF data;
};
struct tyObject_LLChunk_XsENErzHIZV9bhvyJx56wGw {
NI size;
NI acc;
tyObject_LLChunk_XsENErzHIZV9bhvyJx56wGw* next;
};
typedef NI tyArray_9a8QARi5WsUggNU9bom7kzTQ[8];
struct tyObject_Trunk_W0r8S0Y3UGke6T9bIUWnnuw {
tyObject_Trunk_W0r8S0Y3UGke6T9bIUWnnuw* next;
NI key;
tyArray_9a8QARi5WsUggNU9bom7kzTQ bits;
};
struct tyObject_PageDesc_fublkgIY4LG3mT51LU2WHg {
tyObject_PageDesc_fublkgIY4LG3mT51LU2WHg* next;
NI key;
tyArray_9a8QARi5WsUggNU9bom7kzTQ bits;
};
struct Exception {
  RootObj Sup;
Exception* parent;
NCSTRING name;
NimStringDesc* message;
tySequence_uB9b75OUPRENsBAu4AnoePA* trace;
Exception* up;
};
struct tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ {
tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ* next;
NI zeroField;
};
struct tyObject_StackTraceEntry_oLyohQ7O2XOvGnflOss8EA {
NCSTRING procname;
NI line;
NCSTRING filename;
};
struct tySequence_tCXsCfAd03XtyGJVg3kHhg {
  TGenericSeq Sup;
  tyTuple_h8SeyS9aRwdD6lpWqt9anV3A data[SEQ_DECL_SIZE];
};
struct tySequence_uB9b75OUPRENsBAu4AnoePA {
  TGenericSeq Sup;
  tyObject_StackTraceEntry_oLyohQ7O2XOvGnflOss8EA data[SEQ_DECL_SIZE];
};
N_NIMCALL(void, nimGCvisit)(void* d, NI op);
static N_NIMCALL(void, Marker_tySequence_tCXsCfAd03XtyGJVg3kHhg)(void* p, NI op);
N_NIMCALL(void, genericReset)(void* dest, TNimType* mt);
static N_INLINE(void, pop_QCQNB0ZBy9bm2KRBog9cFK7Aheapqueue)(tySequence_tCXsCfAd03XtyGJVg3kHhg** s, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A* Result);
N_NIMCALL(void, unsureAsgnRef)(void** dest, void* src);
static N_INLINE(TGenericSeq*, setLengthSeq)(TGenericSeq* seq, NI elemSize, NI newLen);
static N_INLINE(NI, resize_bzF9a0JivP3Z4njqaxuLc5wsystem)(NI old);
N_LIB_PRIVATE N_NIMCALL(void*, growObj_AVYny8c0GTk28gxjmat1MA)(void* old, NI newsize);
N_NIMCALL(TNimType*, extGetCellType)(void* c);
N_LIB_PRIVATE N_NIMCALL(void, forAllChildrenAux_YOI1Uo54H9aas13WthjhsfA)(void* dest, TNimType* mt, tyEnum_WalkOp_Wfy7gT5VQ8B3aJBxqU8D9cQ op);
static N_INLINE(void, zeroMem_t0o5XqKanO5QJfXMGEzp2Asystem)(void* p, NI size);
static N_INLINE(NI, len_MbUHcYx9cYb9bcgf9c9bDUNdKwasyncdispatch)(tySequence_tCXsCfAd03XtyGJVg3kHhg* h);
static N_INLINE(void, X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch)(tySequence_tCXsCfAd03XtyGJVg3kHhg* h, NI i, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A* Result);
static N_INLINE(void, X5BX5Deq__uxYqk5EjyY9bgre9cMDUNmmgheapqueue)(tySequence_tCXsCfAd03XtyGJVg3kHhg** h, NI i, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A v);
static N_INLINE(void, asgnRef)(void** dest, void* src);
static N_INLINE(void, incRef_9cAA5YuQAAC3MVbnGeV86swsystem)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c);
static N_INLINE(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g*, usrToCell_yB9aH5WIlwd0xkYrcdPeXrQsystem)(void* usr);
static N_INLINE(void, decRef_MV4BBk6J1qu70IbBxwEn4wsystem)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c);
static N_INLINE(void, rtlAddZCT_MV4BBk6J1qu70IbBxwEn4w_2system)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c);
N_LIB_PRIVATE N_NOINLINE(void, addZCT_fCDI7oO1NNVXXURtxSzsRw)(tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w* s, tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c);
N_LIB_PRIVATE N_NIMCALL(void, siftup_cV9czR9cIYHg0kFHmrHlD4nw)(tySequence_tCXsCfAd03XtyGJVg3kHhg** heap, NI p);
static N_INLINE(NIM_BOOL, heapCmp_iMuqO3tR8Yxs19c9c8MT2uUgheapqueue)(tyTuple_h8SeyS9aRwdD6lpWqt9anV3A x, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A y);
N_LIB_PRIVATE N_NIMCALL(NIM_BOOL, lt__RPwNdqiecAXtp3qcgn5S7g)(tyTuple_h8SeyS9aRwdD6lpWqt9anV3A x, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A y);
N_LIB_PRIVATE N_NIMCALL(void, siftdown_2LdT3wpZL6cmUR0Idj5aDQ)(tySequence_tCXsCfAd03XtyGJVg3kHhg** heap, NI startpos, NI p);
extern TNimType NTI_h8SeyS9aRwdD6lpWqt9anV3A_;
TNimType NTI_tCXsCfAd03XtyGJVg3kHhg_;
extern tyObject_GcHeap_1TRH1TZMaVZTnLNcIHuNFQ gch_IcYaEuuWivYAS86vFMTS3Q;
static N_NIMCALL(void, Marker_tySequence_tCXsCfAd03XtyGJVg3kHhg)(void* p, NI op) {
	tySequence_tCXsCfAd03XtyGJVg3kHhg* a;
	NI T1_;
	a = (tySequence_tCXsCfAd03XtyGJVg3kHhg*)p;
	T1_ = (NI)0;
	for (T1_ = 0; T1_ < a->Sup.len; T1_++) {
	nimGCvisit((void*)a->data[T1_].Field1, op);
	}
}

static N_INLINE(NI, resize_bzF9a0JivP3Z4njqaxuLc5wsystem)(NI old) {
	NI result;
	result = (NI)0;
	{
		if (!(old <= ((NI) 0))) goto LA3_;
		result = ((NI) 4);
	}
	goto LA1_;
	LA3_: ;
	{
		if (!(old < ((NI) 65536))) goto LA6_;
		result = (NI)(old * ((NI) 2));
	}
	goto LA1_;
	LA6_: ;
	{
		result = (NI)((NI)(old * ((NI) 3)) / ((NI) 2));
	}
	LA1_: ;
	return result;
}

static N_INLINE(void, zeroMem_t0o5XqKanO5QJfXMGEzp2Asystem)(void* p, NI size) {
	void* T1_;
	T1_ = (void*)0;
	T1_ = memset(p, ((int) 0), ((size_t) (size)));
}

static N_INLINE(TGenericSeq*, setLengthSeq)(TGenericSeq* seq, NI elemSize, NI newLen) {
	TGenericSeq* result;
	result = (TGenericSeq*)0;
	result = seq;
	{
		NI r;
		NI T5_;
		void* T6_;
		if (!((NI)((*result).reserved & ((NI) IL64(4611686018427387903))) < newLen)) goto LA3_;
		T5_ = (NI)0;
		T5_ = resize_bzF9a0JivP3Z4njqaxuLc5wsystem((NI)((*result).reserved & ((NI) IL64(4611686018427387903))));
		r = ((T5_ >= newLen) ? T5_ : newLen);
		T6_ = (void*)0;
		T6_ = growObj_AVYny8c0GTk28gxjmat1MA(((void*) (result)), (NI)((NI)(elemSize * r) + ((NI) 16)));
		result = ((TGenericSeq*) (T6_));
		(*result).reserved = r;
	}
	goto LA1_;
	LA3_: ;
	{
		if (!(newLen < (*result).len)) goto LA8_;
		{
			TNimType* T12_;
			T12_ = (TNimType*)0;
			T12_ = extGetCellType(((void*) (result)));
			if (!!((((*(*T12_).base).flags &(1U<<((NU)(((tyEnum_TNimTypeFlag_v8QUszD1sWlSIWZz7mC4bQ) 0))&7U)))!=0))) goto LA13_;
			{
				NI i;
				NI colontmp_;
				NI res;
				i = (NI)0;
				colontmp_ = (NI)0;
				colontmp_ = (NI)((*result).len - ((NI) 1));
				res = newLen;
				{
					while (1) {
						TNimType* T18_;
						if (!(res <= colontmp_)) goto LA17;
						i = res;
						T18_ = (TNimType*)0;
						T18_ = extGetCellType(((void*) (result)));
						forAllChildrenAux_YOI1Uo54H9aas13WthjhsfA(((void*) ((NI)((NU64)((NI)((NU64)(((NI) (ptrdiff_t) (result))) + (NU64)(((NI) 16)))) + (NU64)((NI)((NU64)(i) * (NU64)(elemSize)))))), (*T18_).base, ((tyEnum_WalkOp_Wfy7gT5VQ8B3aJBxqU8D9cQ) 2));
						res += ((NI) 1);
					} LA17: ;
				}
			}
		}
		LA13_: ;
		zeroMem_t0o5XqKanO5QJfXMGEzp2Asystem(((void*) ((NI)((NU64)((NI)((NU64)(((NI) (ptrdiff_t) (result))) + (NU64)(((NI) 16)))) + (NU64)((NI)((NU64)(newLen) * (NU64)(elemSize)))))), ((NI) ((NI)((NU64)((NI)((NU64)((*result).len) - (NU64)(newLen))) * (NU64)(elemSize)))));
	}
	goto LA1_;
	LA8_: ;
	LA1_: ;
	(*result).len = newLen;
	return result;
}

static N_INLINE(void, pop_QCQNB0ZBy9bm2KRBog9cFK7Aheapqueue)(tySequence_tCXsCfAd03XtyGJVg3kHhg** s, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A* Result) {
	NI L;
	NI T1_;
	genericReset((void*)Result, (&NTI_h8SeyS9aRwdD6lpWqt9anV3A_));
	T1_ = ((*s) ? (*s)->Sup.len : 0);
	L = (NI)(T1_ - ((NI) 1));
	(*Result).Field0 = (*s)->data[L].Field0;
	unsureAsgnRef((void**) (&(*Result).Field1), (*s)->data[L].Field1);
	(*s) = (tySequence_tCXsCfAd03XtyGJVg3kHhg*) setLengthSeq(&((*s))->Sup, sizeof(tyTuple_h8SeyS9aRwdD6lpWqt9anV3A), ((NI) (L)));
}

static N_INLINE(NI, len_MbUHcYx9cYb9bcgf9c9bDUNdKwasyncdispatch)(tySequence_tCXsCfAd03XtyGJVg3kHhg* h) {
	NI result;
	NI T1_;
	result = (NI)0;
	T1_ = (h ? h->Sup.len : 0);
	result = T1_;
	return result;
}

static N_INLINE(void, X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch)(tySequence_tCXsCfAd03XtyGJVg3kHhg* h, NI i, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A* Result) {
	genericReset((void*)Result, (&NTI_h8SeyS9aRwdD6lpWqt9anV3A_));
	(*Result).Field0 = h->data[i].Field0;
	unsureAsgnRef((void**) (&(*Result).Field1), h->data[i].Field1);
}

static N_INLINE(void, incRef_9cAA5YuQAAC3MVbnGeV86swsystem)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c) {
	(*c).refcount = (NI)((NU64)((*c).refcount) + (NU64)(((NI) 8)));
}

static N_INLINE(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g*, usrToCell_yB9aH5WIlwd0xkYrcdPeXrQsystem)(void* usr) {
	tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* result;
	result = (tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g*)0;
	result = ((tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g*) ((NI)((NU64)(((NI) (ptrdiff_t) (usr))) - (NU64)(((NI)sizeof(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g))))));
	return result;
}

static N_INLINE(void, rtlAddZCT_MV4BBk6J1qu70IbBxwEn4w_2system)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c) {
	addZCT_fCDI7oO1NNVXXURtxSzsRw((&gch_IcYaEuuWivYAS86vFMTS3Q.zct), c);
}

static N_INLINE(void, decRef_MV4BBk6J1qu70IbBxwEn4wsystem)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c) {
	{
		(*c).refcount -= ((NI) 8);
		if (!((NU64)((*c).refcount) < (NU64)(((NI) 8)))) goto LA3_;
		rtlAddZCT_MV4BBk6J1qu70IbBxwEn4w_2system(c);
	}
	LA3_: ;
}

static N_INLINE(void, asgnRef)(void** dest, void* src) {
	{
		tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* T5_;
		if (!!((src == NIM_NIL))) goto LA3_;
		T5_ = (tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g*)0;
		T5_ = usrToCell_yB9aH5WIlwd0xkYrcdPeXrQsystem(src);
		incRef_9cAA5YuQAAC3MVbnGeV86swsystem(T5_);
	}
	LA3_: ;
	{
		tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* T10_;
		if (!!(((*dest) == NIM_NIL))) goto LA8_;
		T10_ = (tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g*)0;
		T10_ = usrToCell_yB9aH5WIlwd0xkYrcdPeXrQsystem((*dest));
		decRef_MV4BBk6J1qu70IbBxwEn4wsystem(T10_);
	}
	LA8_: ;
	(*dest) = src;
}

static N_INLINE(void, X5BX5Deq__uxYqk5EjyY9bgre9cMDUNmmgheapqueue)(tySequence_tCXsCfAd03XtyGJVg3kHhg** h, NI i, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A v) {
	(*h)->data[i].Field0 = v.Field0;
	asgnRef((void**) (&(*h)->data[i].Field1), v.Field1);
}

static N_INLINE(NIM_BOOL, heapCmp_iMuqO3tR8Yxs19c9c8MT2uUgheapqueue)(tyTuple_h8SeyS9aRwdD6lpWqt9anV3A x, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A y) {
	NIM_BOOL result;
{	result = (NIM_BOOL)0;
	result = lt__RPwNdqiecAXtp3qcgn5S7g(x, y);
	goto BeforeRet_;
	}BeforeRet_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(void, siftdown_2LdT3wpZL6cmUR0Idj5aDQ)(tySequence_tCXsCfAd03XtyGJVg3kHhg** heap, NI startpos, NI p) {
	NI pos;
	tyTuple_h8SeyS9aRwdD6lpWqt9anV3A newitem;
	pos = p;
	memset((void*)(&newitem), 0, sizeof(newitem));
	X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch((*heap), pos, (&newitem));
	{
		while (1) {
			NI parentpos;
			tyTuple_h8SeyS9aRwdD6lpWqt9anV3A parent;
			if (!(startpos < pos)) goto LA2;
			parentpos = (NI)((NU64)((NI)(pos - ((NI) 1))) >> (NU64)(((NI) 1)));
			memset((void*)(&parent), 0, sizeof(parent));
			X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch((*heap), parentpos, (&parent));
			{
				NIM_BOOL T5_;
				T5_ = (NIM_BOOL)0;
				T5_ = heapCmp_iMuqO3tR8Yxs19c9c8MT2uUgheapqueue(newitem, parent);
				if (!T5_) goto LA6_;
				X5BX5Deq__uxYqk5EjyY9bgre9cMDUNmmgheapqueue(heap, pos, parent);
				pos = parentpos;
			}
			goto LA3_;
			LA6_: ;
			{
				goto LA1;
			}
			LA3_: ;
		} LA2: ;
	} LA1: ;
	X5BX5Deq__uxYqk5EjyY9bgre9cMDUNmmgheapqueue(heap, pos, newitem);
}

N_LIB_PRIVATE N_NIMCALL(void, siftup_cV9czR9cIYHg0kFHmrHlD4nw)(tySequence_tCXsCfAd03XtyGJVg3kHhg** heap, NI p) {
	NI endpos;
	NI pos;
	NI startpos;
	tyTuple_h8SeyS9aRwdD6lpWqt9anV3A newitem;
	NI childpos;
	endpos = len_MbUHcYx9cYb9bcgf9c9bDUNdKwasyncdispatch((*heap));
	pos = p;
	startpos = pos;
	memset((void*)(&newitem), 0, sizeof(newitem));
	X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch((*heap), pos, (&newitem));
	childpos = (NI)((NI)(((NI) 2) * pos) + ((NI) 1));
	{
		while (1) {
			NI rightpos;
			tyTuple_h8SeyS9aRwdD6lpWqt9anV3A T12_;
			if (!(childpos < endpos)) goto LA2;
			rightpos = (NI)(childpos + ((NI) 1));
			{
				NIM_BOOL T5_;
				tyTuple_h8SeyS9aRwdD6lpWqt9anV3A T7_;
				tyTuple_h8SeyS9aRwdD6lpWqt9anV3A T8_;
				NIM_BOOL T9_;
				T5_ = (NIM_BOOL)0;
				T5_ = (rightpos < endpos);
				if (!(T5_)) goto LA6_;
				memset((void*)(&T7_), 0, sizeof(T7_));
				X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch((*heap), childpos, (&T7_));
				memset((void*)(&T8_), 0, sizeof(T8_));
				X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch((*heap), rightpos, (&T8_));
				T9_ = (NIM_BOOL)0;
				T9_ = heapCmp_iMuqO3tR8Yxs19c9c8MT2uUgheapqueue(T7_, T8_);
				T5_ = !(T9_);
				LA6_: ;
				if (!T5_) goto LA10_;
				childpos = rightpos;
			}
			LA10_: ;
			memset((void*)(&T12_), 0, sizeof(T12_));
			X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch((*heap), childpos, (&T12_));
			X5BX5Deq__uxYqk5EjyY9bgre9cMDUNmmgheapqueue(heap, pos, T12_);
			pos = childpos;
			childpos = (NI)((NI)(((NI) 2) * pos) + ((NI) 1));
		} LA2: ;
	}
	X5BX5Deq__uxYqk5EjyY9bgre9cMDUNmmgheapqueue(heap, pos, newitem);
	siftdown_2LdT3wpZL6cmUR0Idj5aDQ(heap, startpos, pos);
}

N_LIB_PRIVATE N_NIMCALL(void, pop_Mc9al9cevONzXYNcXlhSyX0w)(tySequence_tCXsCfAd03XtyGJVg3kHhg** heap, tyTuple_h8SeyS9aRwdD6lpWqt9anV3A* Result) {
	tyTuple_h8SeyS9aRwdD6lpWqt9anV3A lastelt;
	genericReset((void*)Result, (&NTI_h8SeyS9aRwdD6lpWqt9anV3A_));
	memset((void*)(&lastelt), 0, sizeof(lastelt));
	pop_QCQNB0ZBy9bm2KRBog9cFK7Aheapqueue(((tySequence_tCXsCfAd03XtyGJVg3kHhg**) (heap)), (&lastelt));
	{
		NI T3_;
		T3_ = (NI)0;
		T3_ = len_MbUHcYx9cYb9bcgf9c9bDUNdKwasyncdispatch((*heap));
		if (!(((NI) 0) < T3_)) goto LA4_;
		X5BX5D__f9aTbbbrR4Jun2c9bhbx9c8UQasyncdispatch((*heap), ((NI) 0), Result);
		X5BX5Deq__uxYqk5EjyY9bgre9cMDUNmmgheapqueue(heap, ((NI) 0), lastelt);
		siftup_cV9czR9cIYHg0kFHmrHlD4nw(heap, ((NI) 0));
	}
	goto LA1_;
	LA4_: ;
	{
		(*Result).Field0 = lastelt.Field0;
		unsureAsgnRef((void**) (&(*Result).Field1), lastelt.Field1);
	}
	LA1_: ;
}
NIM_EXTERNC N_NOINLINE(void, stdlib_heapqueueInit000)(void) {
}

NIM_EXTERNC N_NOINLINE(void, stdlib_heapqueueDatInit000)(void) {
NTI_tCXsCfAd03XtyGJVg3kHhg_.size = sizeof(tySequence_tCXsCfAd03XtyGJVg3kHhg*);
NTI_tCXsCfAd03XtyGJVg3kHhg_.kind = 24;
NTI_tCXsCfAd03XtyGJVg3kHhg_.base = (&NTI_h8SeyS9aRwdD6lpWqt9anV3A_);
NTI_tCXsCfAd03XtyGJVg3kHhg_.marker = Marker_tySequence_tCXsCfAd03XtyGJVg3kHhg;
}

