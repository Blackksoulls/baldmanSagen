/* Generated by Nim Compiler v0.18.0 */
/*   (c) 2018 Andreas Rumpf */
/* The generated code is subject to the original license. */
/* Compiled for: Linux, amd64, gcc */
/* Command for C compiler:
   gcc -c -w -O3 -fno-strict-aliasing  -I/home/kuro/.choosenim/toolchains/nim-0.18.0/lib -o /home/kuro/Projects/PTUT/baldmanSagen/config/nimcache/stdlib_strutils.o /home/kuro/Projects/PTUT/baldmanSagen/config/nimcache/stdlib_strutils.c */
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
typedef struct NimStringDesc NimStringDesc;
typedef struct TGenericSeq TGenericSeq;
typedef struct tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA;
typedef struct Exception Exception;
typedef struct RootObj RootObj;
typedef struct TNimType TNimType;
typedef struct TNimNode TNimNode;
typedef struct tySequence_uB9b75OUPRENsBAu4AnoePA tySequence_uB9b75OUPRENsBAu4AnoePA;
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
typedef struct tyObject_StackTraceEntry_oLyohQ7O2XOvGnflOss8EA tyObject_StackTraceEntry_oLyohQ7O2XOvGnflOss8EA;
typedef struct tyObject_BaseChunk_Sdq7WpT6qAH858F5ZEdG3w tyObject_BaseChunk_Sdq7WpT6qAH858F5ZEdG3w;
typedef struct tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ;
struct TGenericSeq {
NI len;
NI reserved;
};
struct NimStringDesc {
  TGenericSeq Sup;
NIM_CHAR data[SEQ_DECL_SIZE];
};
typedef NU8 tySet_tyChar_nmiMWKVIe46vacnhAFrQvw[32];
typedef NI tyArray_9cc9aPiDa8VaWjVcFLabEDZQ[256];
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
struct RootObj {
TNimType* m_type;
};
struct Exception {
  RootObj Sup;
Exception* parent;
NCSTRING name;
NimStringDesc* message;
tySequence_uB9b75OUPRENsBAu4AnoePA* trace;
Exception* up;
};
struct tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA {
  Exception Sup;
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
typedef NimStringDesc* tyArray_nHXaesL0DJZHyVS07ARPRA[1];
typedef NU8 tyEnum_HttpMethod_wfZHspwVKQPl9aWhkIcMrAA;
struct tyObject_StackTraceEntry_oLyohQ7O2XOvGnflOss8EA {
NCSTRING procname;
NI line;
NCSTRING filename;
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
struct tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ {
tyObject_FreeCell_u6M5LHprqzkn9axr04yg9bGQ* next;
NI zeroField;
};
struct tySequence_uB9b75OUPRENsBAu4AnoePA {
  TGenericSeq Sup;
  tyObject_StackTraceEntry_oLyohQ7O2XOvGnflOss8EA data[SEQ_DECL_SIZE];
};
N_NIMCALL(NimStringDesc*, copyString)(NimStringDesc* src);
N_LIB_PRIVATE N_NIMCALL(void, nsuInitSkipTable)(NI* a, NimStringDesc* sub);
N_LIB_PRIVATE N_NIMCALL(NI, nsuFindStrA)(tyArray_9cc9aPiDa8VaWjVcFLabEDZQ a, NimStringDesc* s, NimStringDesc* sub, NI start, NI last);
N_NIMCALL(NimStringDesc*, copyStrLast)(NimStringDesc* s, NI start, NI last);
N_NIMCALL(NimStringDesc*, copyStrLast)(NimStringDesc* s, NI first, NI last);
static N_INLINE(void, appendString)(NimStringDesc* dest, NimStringDesc* src);
static N_INLINE(void, copyMem_E1xtACub5WcDa3vbrIXbwgsystem)(void* dest, void* source, NI size);
N_NIMCALL(NimStringDesc*, resizeString)(NimStringDesc* dest, NI addlen);
N_NIMCALL(NimStringDesc*, copyStr)(NimStringDesc* s, NI start);
N_NIMCALL(NimStringDesc*, copyStr)(NimStringDesc* s, NI first);
N_LIB_PRIVATE N_NIMCALL(NI, nsuFindChar)(NimStringDesc* s, NIM_CHAR sub, NI start, NI last);
N_NIMCALL(NimStringDesc*, rawNewString)(NI space);
N_NIMCALL(NimStringDesc*, rawNewString)(NI cap);
N_LIB_PRIVATE N_NIMCALL(void, nsuAddf)(NimStringDesc** s, NimStringDesc* formatstr, NimStringDesc** a, NI aLen_0);
N_LIB_PRIVATE N_NOINLINE(void, invalidFormatString_61EJWW6vRISEo9a8gt0tusw)(void);
N_NIMCALL(void*, newObj)(TNimType* typ, NI size);
N_NIMCALL(NimStringDesc*, copyStringRC1)(NimStringDesc* src);
static N_INLINE(void, nimGCunrefNoCycle)(void* p);
static N_INLINE(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g*, usrToCell_yB9aH5WIlwd0xkYrcdPeXrQsystem)(void* usr);
static N_INLINE(void, rtlAddZCT_MV4BBk6J1qu70IbBxwEn4w_2system)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c);
N_LIB_PRIVATE N_NOINLINE(void, addZCT_fCDI7oO1NNVXXURtxSzsRw)(tyObject_CellSeq_Axo1XVm9aaQueTOldv8le5w* s, tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c);
static N_INLINE(void, asgnRef)(void** dest, void* src);
static N_INLINE(void, incRef_9cAA5YuQAAC3MVbnGeV86swsystem)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c);
static N_INLINE(void, decRef_MV4BBk6J1qu70IbBxwEn4wsystem)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c);
N_NIMCALL(void, raiseException)(Exception* e, NCSTRING ename);
N_NIMCALL(NimStringDesc*, addChar)(NimStringDesc* s, NIM_CHAR c);
N_LIB_PRIVATE N_NIMCALL(NI, findNormalized_SW1VCMDsxPTtzxnYrf3N6w)(NimStringDesc* x, NimStringDesc** inArray, NI inArrayLen_0);
N_LIB_PRIVATE N_NIMCALL(NI, nsuCmpIgnoreStyle)(NimStringDesc* a, NimStringDesc* b);
N_LIB_PRIVATE N_NIMCALL(NIM_CHAR, nsuToLowerAsciiChar)(NIM_CHAR c);
N_NIMCALL(NimStringDesc*, reprEnum)(NI e, TNimType* typ);
static N_INLINE(void, asgnRefNoCycle)(void** dest, void* src);
N_NIMCALL(NimStringDesc*, mnewString)(NI len);
N_NIMCALL(NimStringDesc*, mnewString)(NI len);
extern TNimType NTI_yCEN9anxCD6mzBxGjuaRBdg_;
extern TNimType NTI_Gi06FkNeykJn7mrqRZYrkA_;
extern tyObject_GcHeap_1TRH1TZMaVZTnLNcIHuNFQ gch_IcYaEuuWivYAS86vFMTS3Q;
extern TNimType NTI_wfZHspwVKQPl9aWhkIcMrAA_;
STRING_LITERAL(TM_JGc9b9bh2D3nTdUR7TGyq8aA_2, "", 0);
STRING_LITERAL(TM_JGc9b9bh2D3nTdUR7TGyq8aA_3, "invalid format string", 21);
STRING_LITERAL(TM_JGc9b9bh2D3nTdUR7TGyq8aA_4, "invalid enum value: ", 20);

N_LIB_PRIVATE N_NIMCALL(NI, nsuFindChar)(NimStringDesc* s, NIM_CHAR sub, NI start, NI last) {
	NI result;
	NI last_2;
	void* found;
{	result = (NI)0;
	{
		if (!(((NI) (last)) == ((NI) 0))) goto LA3_;
		last_2 = (s ? (s->Sup.len-1) : -1);
	}
	goto LA1_;
	LA3_: ;
	{
		last_2 = ((NI) (last));
	}
	LA1_: ;
	found = memchr(((void*) ((&s->data[start]))), sub, (NI)((NI)(last_2 - ((NI) (start))) + ((NI) 1)));
	{
		if (!!((found == 0))) goto LA8_;
		result = (NI)((NU64)(((NI) (ptrdiff_t) (found))) - (NU64)(((NI) (s->data))));
		goto BeforeRet_;
	}
	LA8_: ;
	result = ((NI) -1);
	goto BeforeRet_;
	}BeforeRet_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NIM_BOOL, allCharsInSet_wVfr4F6j4mVzI8ggLoMVdw)(NimStringDesc* s, tySet_tyChar_nmiMWKVIe46vacnhAFrQvw theSet) {
	NIM_BOOL result;
{	result = (NIM_BOOL)0;
	{
		NIM_CHAR c;
		NI i;
		NI L;
		c = (NIM_CHAR)0;
		i = ((NI) 0);
		L = (s ? s->Sup.len : 0);
		{
			while (1) {
				if (!(i < L)) goto LA3;
				c = s->data[i];
				{
					if (!!(((theSet[(NU)(((NU8)(c)))>>3] &(1U<<((NU)(((NU8)(c)))&7U)))!=0))) goto LA6_;
					result = NIM_FALSE;
					goto BeforeRet_;
				}
				LA6_: ;
				i += ((NI) 1);
			} LA3: ;
		}
	}
	result = NIM_TRUE;
	goto BeforeRet_;
	}BeforeRet_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(void, nsuInitSkipTable)(NI* a, NimStringDesc* sub) {
	NI m;
	NI m1;
	NI i;
	m = (sub ? sub->Sup.len : 0);
	m1 = (NI)(m + ((NI) 1));
	i = ((NI) 0);
	{
		while (1) {
			if (!(i <= ((NI) 248))) goto LA2;
			a[(((NU8)(((NIM_CHAR) (((NI) ((NI)(i + ((NI) 0)))))))))- 0] = m1;
			a[(((NU8)(((NIM_CHAR) (((NI) ((NI)(i + ((NI) 1)))))))))- 0] = m1;
			a[(((NU8)(((NIM_CHAR) (((NI) ((NI)(i + ((NI) 2)))))))))- 0] = m1;
			a[(((NU8)(((NIM_CHAR) (((NI) ((NI)(i + ((NI) 3)))))))))- 0] = m1;
			a[(((NU8)(((NIM_CHAR) (((NI) ((NI)(i + ((NI) 4)))))))))- 0] = m1;
			a[(((NU8)(((NIM_CHAR) (((NI) ((NI)(i + ((NI) 5)))))))))- 0] = m1;
			a[(((NU8)(((NIM_CHAR) (((NI) ((NI)(i + ((NI) 6)))))))))- 0] = m1;
			a[(((NU8)(((NIM_CHAR) (((NI) ((NI)(i + ((NI) 7)))))))))- 0] = m1;
			i += ((NI) 8);
		} LA2: ;
	}
	{
		NI i_2;
		NI colontmp_;
		NI res;
		i_2 = (NI)0;
		colontmp_ = (NI)0;
		colontmp_ = (NI)(m - ((NI) 1));
		res = ((NI) 0);
		{
			while (1) {
				if (!(res <= colontmp_)) goto LA5;
				i_2 = res;
				a[(((NU8)(sub->data[i_2])))- 0] = (NI)(m - i_2);
				res += ((NI) 1);
			} LA5: ;
		}
	}
}

N_LIB_PRIVATE N_NIMCALL(NI, nsuFindStrA)(tyArray_9cc9aPiDa8VaWjVcFLabEDZQ a, NimStringDesc* s, NimStringDesc* sub, NI start, NI last) {
	NI result;
	NI last_2;
	NI m;
	NI n;
	NI j;
{	result = (NI)0;
	{
		if (!(((NI) (last)) == ((NI) 0))) goto LA3_;
		last_2 = (s ? (s->Sup.len-1) : -1);
	}
	goto LA1_;
	LA3_: ;
	{
		last_2 = ((NI) (last));
	}
	LA1_: ;
	m = (sub ? sub->Sup.len : 0);
	n = (NI)(last_2 + ((NI) 1));
	j = start;
	{
		while (1) {
			if (!(((NI) (j)) <= (NI)(n - m))) goto LA7;
			{
				{
					NI k;
					NI colontmp_;
					NI res;
					k = (NI)0;
					colontmp_ = (NI)0;
					colontmp_ = (NI)(m - ((NI) 1));
					res = ((NI) 0);
					{
						while (1) {
							if (!(res <= colontmp_)) goto LA11;
							k = res;
							{
								if (!!(((NU8)(sub->data[k]) == (NU8)(s->data[(NI)(k + ((NI) (j)))])))) goto LA14_;
								goto LA8;
							}
							LA14_: ;
							res += ((NI) 1);
						} LA11: ;
					}
				}
				result = ((NI) (j));
				goto BeforeRet_;
			} LA8: ;
			j += a[(((NU8)(s->data[(NI)(((NI) (j)) + m)])))- 0];
		} LA7: ;
	}
	result = ((NI) -1);
	goto BeforeRet_;
	}BeforeRet_: ;
	return result;
}

static N_INLINE(void, copyMem_E1xtACub5WcDa3vbrIXbwgsystem)(void* dest, void* source, NI size) {
	void* T1_;
	T1_ = (void*)0;
	T1_ = memcpy(dest, source, ((size_t) (size)));
}

static N_INLINE(void, appendString)(NimStringDesc* dest, NimStringDesc* src) {
	copyMem_E1xtACub5WcDa3vbrIXbwgsystem(((void*) ((&(*dest).data[((*dest).Sup.len)- 0]))), ((void*) ((*src).data)), ((NI) ((NI)((*src).Sup.len + ((NI) 1)))));
	(*dest).Sup.len += (*src).Sup.len;
}

N_LIB_PRIVATE N_NIMCALL(NimStringDesc*, nsuReplaceStr)(NimStringDesc* s, NimStringDesc* sub, NimStringDesc* by) {
	NimStringDesc* result;
	tyArray_9cc9aPiDa8VaWjVcFLabEDZQ a;
	NI last;
	NI i;
	NimStringDesc* T8_;
	result = (NimStringDesc*)0;
	result = copyString(((NimStringDesc*) &TM_JGc9b9bh2D3nTdUR7TGyq8aA_2));
	nsuInitSkipTable(a, sub);
	last = (s ? (s->Sup.len-1) : -1);
	i = ((NI) 0);
	{
		while (1) {
			NI j;
			NimStringDesc* T7_;
			j = nsuFindStrA(a, s, sub, ((NI) (i)), ((NI) (last)));
			{
				if (!(j < ((NI) 0))) goto LA5_;
				goto LA1;
			}
			LA5_: ;
			T7_ = (NimStringDesc*)0;
			T7_ = copyStrLast(s, i, (NI)(j - ((NI) 1)));
			result = resizeString(result, T7_->Sup.len + 0);
appendString(result, T7_);
			result = resizeString(result, by->Sup.len + 0);
appendString(result, by);
			i = (NI)(j + (sub ? sub->Sup.len : 0));
		}
	} LA1: ;
	T8_ = (NimStringDesc*)0;
	T8_ = copyStr(s, i);
	result = resizeString(result, T8_->Sup.len + 0);
appendString(result, T8_);
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NIM_BOOL, nsuStartsWith)(NimStringDesc* s, NimStringDesc* prefix) {
	NIM_BOOL result;
	NI i;
{	result = (NIM_BOOL)0;
	i = ((NI) 0);
	{
		while (1) {
			{
				if (!((NU8)(prefix->data[i]) == (NU8)(0))) goto LA5_;
				result = NIM_TRUE;
				goto BeforeRet_;
			}
			LA5_: ;
			{
				if (!!(((NU8)(s->data[i]) == (NU8)(prefix->data[i])))) goto LA9_;
				result = NIM_FALSE;
				goto BeforeRet_;
			}
			LA9_: ;
			i += ((NI) 1);
		}
	}
	}BeforeRet_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NIM_BOOL, contains_6e5MGL10HDAJ205lBYpWxw)(NimStringDesc* s, NIM_CHAR c) {
	NIM_BOOL result;
	NI T1_;
{	result = (NIM_BOOL)0;
	T1_ = (NI)0;
	T1_ = nsuFindChar(s, c, ((NI) 0), ((NI) 0));
	result = (((NI) 0) <= T1_);
	goto BeforeRet_;
	}BeforeRet_: ;
	return result;
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

static N_INLINE(void, nimGCunrefNoCycle)(void* p) {
	tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c;
	c = usrToCell_yB9aH5WIlwd0xkYrcdPeXrQsystem(p);
	{
		(*c).refcount -= ((NI) 8);
		if (!((NU64)((*c).refcount) < (NU64)(((NI) 8)))) goto LA3_;
		rtlAddZCT_MV4BBk6J1qu70IbBxwEn4w_2system(c);
	}
	LA3_: ;
}

static N_INLINE(void, incRef_9cAA5YuQAAC3MVbnGeV86swsystem)(tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c) {
	(*c).refcount = (NI)((NU64)((*c).refcount) + (NU64)(((NI) 8)));
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

N_LIB_PRIVATE N_NOINLINE(void, invalidFormatString_61EJWW6vRISEo9a8gt0tusw)(void) {
	tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA* e;
	NimStringDesc* T1_;
	e = (tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA*)0;
	e = (tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA*) newObj((&NTI_yCEN9anxCD6mzBxGjuaRBdg_), sizeof(tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA));
	(*e).Sup.Sup.m_type = (&NTI_Gi06FkNeykJn7mrqRZYrkA_);
	T1_ = (NimStringDesc*)0;
	T1_ = (*e).Sup.message; (*e).Sup.message = copyStringRC1(((NimStringDesc*) &TM_JGc9b9bh2D3nTdUR7TGyq8aA_3));
	if (T1_) nimGCunrefNoCycle(T1_);
	asgnRef((void**) (&(*e).Sup.parent), NIM_NIL);
	raiseException((Exception*)e, "ValueError");
}

N_LIB_PRIVATE N_NIMCALL(NIM_CHAR, nsuToLowerAsciiChar)(NIM_CHAR c) {
	NIM_CHAR result;
	result = (NIM_CHAR)0;
	{
		if (!(((NU8)(c)) >= ((NU8)(65)) && ((NU8)(c)) <= ((NU8)(90)))) goto LA3_;
		result = ((NIM_CHAR) (((NI) ((NI)(((NU8)(c)) + ((NI) 32))))));
	}
	goto LA1_;
	LA3_: ;
	{
		result = c;
	}
	LA1_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NI, nsuCmpIgnoreStyle)(NimStringDesc* a, NimStringDesc* b) {
	NI result;
	NI i;
	NI j;
	result = (NI)0;
	i = ((NI) 0);
	j = ((NI) 0);
	{
		while (1) {
			NIM_CHAR aa;
			NIM_CHAR bb;
			{
				while (1) {
					if (!((NU8)(a->data[i]) == (NU8)(95))) goto LA4;
					i += ((NI) 1);
				} LA4: ;
			}
			{
				while (1) {
					if (!((NU8)(b->data[j]) == (NU8)(95))) goto LA6;
					j += ((NI) 1);
				} LA6: ;
			}
			aa = nsuToLowerAsciiChar(a->data[i]);
			bb = nsuToLowerAsciiChar(b->data[j]);
			result = (NI)(((NU8)(aa)) - ((NU8)(bb)));
			{
				NIM_BOOL T9_;
				T9_ = (NIM_BOOL)0;
				T9_ = !((result == ((NI) 0)));
				if (T9_) goto LA10_;
				T9_ = ((NU8)(aa) == (NU8)(0));
				LA10_: ;
				if (!T9_) goto LA11_;
				goto LA1;
			}
			LA11_: ;
			i += ((NI) 1);
			j += ((NI) 1);
		}
	} LA1: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NI, findNormalized_SW1VCMDsxPTtzxnYrf3N6w)(NimStringDesc* x, NimStringDesc** inArray, NI inArrayLen_0) {
	NI result;
	NI i;
{	result = (NI)0;
	i = ((NI) 0);
	{
		while (1) {
			if (!(i < (inArrayLen_0-1))) goto LA2;
			{
				NI T5_;
				T5_ = (NI)0;
				T5_ = nsuCmpIgnoreStyle(x, inArray[i]);
				if (!(T5_ == ((NI) 0))) goto LA6_;
				result = i;
				goto BeforeRet_;
			}
			LA6_: ;
			i += ((NI) 2);
		} LA2: ;
	}
	result = ((NI) -1);
	goto BeforeRet_;
	}BeforeRet_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(void, nsuAddf)(NimStringDesc** s, NimStringDesc* formatstr, NimStringDesc** a, NI aLen_0) {
	NI i;
	NI num;
	i = ((NI) 0);
	num = ((NI) 0);
	{
		while (1) {
			if (!(i < (formatstr ? formatstr->Sup.len : 0))) goto LA2;
			{
				if (!((NU8)(formatstr->data[i]) == (NU8)(36))) goto LA5_;
				switch (((NU8)(formatstr->data[(NI)(i + ((NI) 1))]))) {
				case 35:
				{
					{
						if (!((NU64)((aLen_0-1)) < (NU64)(num))) goto LA10_;
						invalidFormatString_61EJWW6vRISEo9a8gt0tusw();
					}
					LA10_: ;
					(*s) = resizeString((*s), a[num]->Sup.len + 0);
appendString((*s), a[num]);
					i += ((NI) 2);
					num += ((NI) 1);
				}
				break;
				case 36:
				{
					(*s) = addChar((*s), 36);
					i += ((NI) 2);
				}
				break;
				case 49 ... 57:
				case 45:
				{
					NI j;
					NIM_BOOL negative;
					NI idx;
					j = ((NI) 0);
					i += ((NI) 1);
					negative = ((NU8)(formatstr->data[i]) == (NU8)(45));
					{
						if (!negative) goto LA16_;
						i += ((NI) 1);
					}
					LA16_: ;
					{
						while (1) {
							if (!(((NU8)(formatstr->data[i])) >= ((NU8)(48)) && ((NU8)(formatstr->data[i])) <= ((NU8)(57)))) goto LA19;
							j = (NI)((NI)((NI)(j * ((NI) 10)) + ((NU8)(formatstr->data[i]))) - ((NI) 48));
							i += ((NI) 1);
						} LA19: ;
					}
					{
						if (!!(negative)) goto LA22_;
						idx = (NI)(j - ((NI) 1));
					}
					goto LA20_;
					LA22_: ;
					{
						idx = (NI)(aLen_0 - j);
					}
					LA20_: ;
					{
						if (!((NU64)((aLen_0-1)) < (NU64)(idx))) goto LA27_;
						invalidFormatString_61EJWW6vRISEo9a8gt0tusw();
					}
					LA27_: ;
					(*s) = resizeString((*s), a[idx]->Sup.len + 0);
appendString((*s), a[idx]);
				}
				break;
				case 123:
				{
					NI j_2;
					NI k;
					NIM_BOOL negative_2;
					NI isNumber;
					j_2 = (NI)(i + ((NI) 2));
					k = ((NI) 0);
					negative_2 = ((NU8)(formatstr->data[j_2]) == (NU8)(45));
					{
						if (!negative_2) goto LA32_;
						j_2 += ((NI) 1);
					}
					LA32_: ;
					isNumber = ((NI) 0);
					{
						while (1) {
							if (!!((((NU8)(formatstr->data[j_2])) == ((NU8)(0)) || ((NU8)(formatstr->data[j_2])) == ((NU8)(125))))) goto LA35;
							{
								if (!(((NU8)(formatstr->data[j_2])) >= ((NU8)(48)) && ((NU8)(formatstr->data[j_2])) <= ((NU8)(57)))) goto LA38_;
								k = (NI)((NI)((NI)(k * ((NI) 10)) + ((NU8)(formatstr->data[j_2]))) - ((NI) 48));
								{
									if (!(isNumber == ((NI) 0))) goto LA42_;
									isNumber = ((NI) 1);
								}
								LA42_: ;
							}
							goto LA36_;
							LA38_: ;
							{
								isNumber = ((NI) -1);
							}
							LA36_: ;
							j_2 += ((NI) 1);
						} LA35: ;
					}
					{
						NI idx_2;
						if (!(isNumber == ((NI) 1))) goto LA47_;
						{
							if (!!(negative_2)) goto LA51_;
							idx_2 = (NI)(k - ((NI) 1));
						}
						goto LA49_;
						LA51_: ;
						{
							idx_2 = (NI)(aLen_0 - k);
						}
						LA49_: ;
						{
							if (!((NU64)((aLen_0-1)) < (NU64)(idx_2))) goto LA56_;
							invalidFormatString_61EJWW6vRISEo9a8gt0tusw();
						}
						LA56_: ;
						(*s) = resizeString((*s), a[idx_2]->Sup.len + 0);
appendString((*s), a[idx_2]);
					}
					goto LA45_;
					LA47_: ;
					{
						NI x;
						NimStringDesc* T59_;
						T59_ = (NimStringDesc*)0;
						T59_ = copyStrLast(formatstr, (NI)(i + ((NI) 2)), (NI)(j_2 - ((NI) 1)));
						x = findNormalized_SW1VCMDsxPTtzxnYrf3N6w(T59_, a, aLen_0);
						{
							NIM_BOOL T62_;
							T62_ = (NIM_BOOL)0;
							T62_ = (((NI) 0) <= x);
							if (!(T62_)) goto LA63_;
							T62_ = (x < (aLen_0-1));
							LA63_: ;
							if (!T62_) goto LA64_;
							(*s) = resizeString((*s), a[(NI)(x + ((NI) 1))]->Sup.len + 0);
appendString((*s), a[(NI)(x + ((NI) 1))]);
						}
						goto LA60_;
						LA64_: ;
						{
							invalidFormatString_61EJWW6vRISEo9a8gt0tusw();
						}
						LA60_: ;
					}
					LA45_: ;
					i = (NI)(j_2 + ((NI) 1));
				}
				break;
				case 97 ... 122:
				case 65 ... 90:
				case 128 ... 255:
				case 95:
				{
					NI j_3;
					NI x_2;
					NimStringDesc* T70_;
					j_3 = (NI)(i + ((NI) 1));
					{
						while (1) {
							if (!(((NU8)(formatstr->data[j_3])) >= ((NU8)(97)) && ((NU8)(formatstr->data[j_3])) <= ((NU8)(122)) || ((NU8)(formatstr->data[j_3])) >= ((NU8)(65)) && ((NU8)(formatstr->data[j_3])) <= ((NU8)(90)) || ((NU8)(formatstr->data[j_3])) >= ((NU8)(48)) && ((NU8)(formatstr->data[j_3])) <= ((NU8)(57)) || ((NU8)(formatstr->data[j_3])) >= ((NU8)(128)) && ((NU8)(formatstr->data[j_3])) <= ((NU8)(255)) || ((NU8)(formatstr->data[j_3])) == ((NU8)(95)))) goto LA69;
							j_3 += ((NI) 1);
						} LA69: ;
					}
					T70_ = (NimStringDesc*)0;
					T70_ = copyStrLast(formatstr, (NI)(i + ((NI) 1)), (NI)(j_3 - ((NI) 1)));
					x_2 = findNormalized_SW1VCMDsxPTtzxnYrf3N6w(T70_, a, aLen_0);
					{
						NIM_BOOL T73_;
						T73_ = (NIM_BOOL)0;
						T73_ = (((NI) 0) <= x_2);
						if (!(T73_)) goto LA74_;
						T73_ = (x_2 < (aLen_0-1));
						LA74_: ;
						if (!T73_) goto LA75_;
						(*s) = resizeString((*s), a[(NI)(x_2 + ((NI) 1))]->Sup.len + 0);
appendString((*s), a[(NI)(x_2 + ((NI) 1))]);
					}
					goto LA71_;
					LA75_: ;
					{
						invalidFormatString_61EJWW6vRISEo9a8gt0tusw();
					}
					LA71_: ;
					i = j_3;
				}
				break;
				default:
				{
					invalidFormatString_61EJWW6vRISEo9a8gt0tusw();
				}
				break;
				}
			}
			goto LA3_;
			LA5_: ;
			{
				(*s) = addChar((*s), formatstr->data[i]);
				i += ((NI) 1);
			}
			LA3_: ;
		} LA2: ;
	}
}

N_LIB_PRIVATE N_NIMCALL(NimStringDesc*, nsuFormatOpenArray)(NimStringDesc* formatstr, NimStringDesc** a, NI aLen_0) {
	NimStringDesc* result;
	result = (NimStringDesc*)0;
	result = rawNewString(((NI) ((NI)((formatstr ? formatstr->Sup.len : 0) + (NI)((NU64)(aLen_0) << (NU64)(((NI) 4)))))));
	nsuAddf((&result), formatstr, a, aLen_0);
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NimStringDesc*, nsuJoinSep)(NimStringDesc** a, NI aLen_0, NimStringDesc* sep) {
	NimStringDesc* result;
	result = (NimStringDesc*)0;
	{
		NI L;
		if (!(((NI) 0) < aLen_0)) goto LA3_;
		L = (NI)((sep ? sep->Sup.len : 0) * (NI)(aLen_0 - ((NI) 1)));
		{
			NI i;
			NI colontmp_;
			NI res;
			i = (NI)0;
			colontmp_ = (NI)0;
			colontmp_ = (aLen_0-1);
			res = ((NI) 0);
			{
				while (1) {
					if (!(res <= colontmp_)) goto LA7;
					i = res;
					L += (a[i] ? a[i]->Sup.len : 0);
					res += ((NI) 1);
				} LA7: ;
			}
		}
		result = rawNewString(((NI) (L)));
		result = resizeString(result, a[((NI) 0)]->Sup.len + 0);
appendString(result, a[((NI) 0)]);
		{
			NI i_2;
			NI colontmp__2;
			NI res_2;
			i_2 = (NI)0;
			colontmp__2 = (NI)0;
			colontmp__2 = (aLen_0-1);
			res_2 = ((NI) 1);
			{
				while (1) {
					if (!(res_2 <= colontmp__2)) goto LA10;
					i_2 = res_2;
					result = resizeString(result, sep->Sup.len + 0);
appendString(result, sep);
					result = resizeString(result, a[i_2]->Sup.len + 0);
appendString(result, a[i_2]);
					res_2 += ((NI) 1);
				} LA10: ;
			}
		}
	}
	goto LA1_;
	LA3_: ;
	{
		result = copyString(((NimStringDesc*) &TM_JGc9b9bh2D3nTdUR7TGyq8aA_2));
	}
	LA1_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NimStringDesc*, nsuFormatSingleElem)(NimStringDesc* formatstr, NimStringDesc* a) {
	NimStringDesc* result;
	tyArray_nHXaesL0DJZHyVS07ARPRA T1_;
	result = (NimStringDesc*)0;
	result = rawNewString(((NI) ((NI)((formatstr ? formatstr->Sup.len : 0) + (a ? a->Sup.len : 0)))));
	memset((void*)T1_, 0, sizeof(T1_));
	T1_[0] = copyString(a);
	nsuAddf((&result), formatstr, T1_, 1);
	return result;
}

static N_INLINE(void, asgnRefNoCycle)(void** dest, void* src) {
	{
		tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c;
		if (!!((src == NIM_NIL))) goto LA3_;
		c = usrToCell_yB9aH5WIlwd0xkYrcdPeXrQsystem(src);
		(*c).refcount += ((NI) 8);
	}
	LA3_: ;
	{
		tyObject_Cell_1zcF9cV8XIAtbN8h5HRUB8g* c_2;
		if (!!(((*dest) == NIM_NIL))) goto LA7_;
		c_2 = usrToCell_yB9aH5WIlwd0xkYrcdPeXrQsystem((*dest));
		{
			(*c_2).refcount -= ((NI) 8);
			if (!((NU64)((*c_2).refcount) < (NU64)(((NI) 8)))) goto LA11_;
			rtlAddZCT_MV4BBk6J1qu70IbBxwEn4w_2system(c_2);
		}
		LA11_: ;
	}
	LA7_: ;
	(*dest) = src;
}

N_LIB_PRIVATE N_NIMCALL(tyEnum_HttpMethod_wfZHspwVKQPl9aWhkIcMrAA, parseEnum_Fate71RPNtHBz7Ge4w8uyQ)(NimStringDesc* s) {
	tyEnum_HttpMethod_wfZHspwVKQPl9aWhkIcMrAA result;
	tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA* e_2;
	NimStringDesc* T9_;
{	result = (tyEnum_HttpMethod_wfZHspwVKQPl9aWhkIcMrAA)0;
	{
		tyEnum_HttpMethod_wfZHspwVKQPl9aWhkIcMrAA e;
		NI res;
		e = (tyEnum_HttpMethod_wfZHspwVKQPl9aWhkIcMrAA)0;
		res = ((NI) 0);
		{
			while (1) {
				if (!(res <= ((NI) 8))) goto LA3;
				e = ((tyEnum_HttpMethod_wfZHspwVKQPl9aWhkIcMrAA) (res));
				{
					NI T6_;
					T6_ = (NI)0;
					T6_ = nsuCmpIgnoreStyle(s, reprEnum((NI)e, (&NTI_wfZHspwVKQPl9aWhkIcMrAA_)));
					if (!(T6_ == ((NI) 0))) goto LA7_;
					result = e;
					goto BeforeRet_;
				}
				LA7_: ;
				res += ((NI) 1);
			} LA3: ;
		}
	}
	e_2 = (tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA*)0;
	e_2 = (tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA*) newObj((&NTI_yCEN9anxCD6mzBxGjuaRBdg_), sizeof(tyObject_ValueError_Gi06FkNeykJn7mrqRZYrkA));
	(*e_2).Sup.Sup.m_type = (&NTI_Gi06FkNeykJn7mrqRZYrkA_);
	T9_ = (NimStringDesc*)0;
	T9_ = rawNewString(s->Sup.len + 20);
appendString(T9_, ((NimStringDesc*) &TM_JGc9b9bh2D3nTdUR7TGyq8aA_4));
appendString(T9_, s);
	asgnRefNoCycle((void**) (&(*e_2).Sup.message), T9_);
	asgnRef((void**) (&(*e_2).Sup.parent), NIM_NIL);
	raiseException((Exception*)e_2, "ValueError");
	}BeforeRet_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NI, nsuCmpIgnoreCase)(NimStringDesc* a, NimStringDesc* b) {
	NI result;
	NI i;
	NI m;
{	result = (NI)0;
	i = ((NI) 0);
	m = (((a ? a->Sup.len : 0) <= (b ? b->Sup.len : 0)) ? (a ? a->Sup.len : 0) : (b ? b->Sup.len : 0));
	{
		while (1) {
			NIM_CHAR T3_;
			NIM_CHAR T4_;
			if (!(i < m)) goto LA2;
			T3_ = (NIM_CHAR)0;
			T3_ = nsuToLowerAsciiChar(a->data[i]);
			T4_ = (NIM_CHAR)0;
			T4_ = nsuToLowerAsciiChar(b->data[i]);
			result = (NI)(((NU8)(T3_)) - ((NU8)(T4_)));
			{
				if (!!((result == ((NI) 0)))) goto LA7_;
				goto BeforeRet_;
			}
			LA7_: ;
			i += ((NI) 1);
		} LA2: ;
	}
	result = (NI)((a ? a->Sup.len : 0) - (b ? b->Sup.len : 0));
	}BeforeRet_: ;
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NimStringDesc*, nsuToLowerAsciiStr)(NimStringDesc* s) {
	NimStringDesc* result;
	result = (NimStringDesc*)0;
	result = mnewString(((NI) ((s ? s->Sup.len : 0))));
	{
		NI i;
		NI colontmp_;
		NI res;
		i = (NI)0;
		colontmp_ = (NI)0;
		colontmp_ = (NI)((s ? s->Sup.len : 0) - ((NI) 1));
		res = ((NI) 0);
		{
			while (1) {
				if (!(res <= colontmp_)) goto LA3;
				i = res;
				result->data[i] = nsuToLowerAsciiChar(s->data[i]);
				res += ((NI) 1);
			} LA3: ;
		}
	}
	return result;
}

N_LIB_PRIVATE N_NIMCALL(NimStringDesc*, nsuRepeatChar)(NIM_CHAR c, NI count) {
	NimStringDesc* result;
	result = (NimStringDesc*)0;
	result = mnewString(count);
	{
		NI i;
		NI colontmp_;
		NI res;
		i = (NI)0;
		colontmp_ = (NI)0;
		colontmp_ = (NI)(((NI) (count)) - ((NI) 1));
		res = ((NI) 0);
		{
			while (1) {
				if (!(res <= colontmp_)) goto LA3;
				i = res;
				result->data[i] = c;
				res += ((NI) 1);
			} LA3: ;
		}
	}
	return result;
}
NIM_EXTERNC N_NOINLINE(void, stdlib_strutilsInit000)(void) {
}

NIM_EXTERNC N_NOINLINE(void, stdlib_strutilsDatInit000)(void) {
}

