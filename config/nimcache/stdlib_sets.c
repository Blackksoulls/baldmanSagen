/* Generated by Nim Compiler v0.18.0 */
/*   (c) 2018 Andreas Rumpf */
/* The generated code is subject to the original license. */
/* Compiled for: Linux, amd64, gcc */
/* Command for C compiler:
   gcc -c -w -O3 -fno-strict-aliasing  -I/home/kuro/.choosenim/toolchains/nim-0.18.0/lib -o /home/kuro/Projects/PTUT/baldmanSagen/config/nimcache/stdlib_sets.o /home/kuro/Projects/PTUT/baldmanSagen/config/nimcache/stdlib_sets.c */
#define NIM_NEW_MANGLING_RULES
#define NIM_INTBITS 64

#include "nimbase.h"
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
typedef struct tyObject_HashSet_J1RbOJyRcRl1E5zDK6YKcQ tyObject_HashSet_J1RbOJyRcRl1E5zDK6YKcQ;
typedef struct tySequence_8Np6tlClE5az1CyqZdN19bQ tySequence_8Np6tlClE5az1CyqZdN19bQ;
typedef struct TNimType TNimType;
typedef struct TNimNode TNimNode;
typedef struct tyTuple_1v9bKyksXWMsm0vNwmZ4EuQ tyTuple_1v9bKyksXWMsm0vNwmZ4EuQ;
typedef struct TGenericSeq TGenericSeq;
struct tyObject_HashSet_J1RbOJyRcRl1E5zDK6YKcQ {
tySequence_8Np6tlClE5az1CyqZdN19bQ* data;
NI counter;
};
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
struct tyTuple_1v9bKyksXWMsm0vNwmZ4EuQ {
NI Field0;
NI Field1;
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
struct TGenericSeq {
NI len;
NI reserved;
};
struct tySequence_8Np6tlClE5az1CyqZdN19bQ {
  TGenericSeq Sup;
  tyTuple_1v9bKyksXWMsm0vNwmZ4EuQ data[SEQ_DECL_SIZE];
};
static N_NIMCALL(void, Marker_tySequence_8Np6tlClE5az1CyqZdN19bQ)(void* p, NI op);
TNimType NTI_J1RbOJyRcRl1E5zDK6YKcQ_;
TNimType NTI_1v9bKyksXWMsm0vNwmZ4EuQ_;
extern TNimType NTI_rR5Bzr1D5krxoo1NcNyeMA_;
TNimType NTI_8Np6tlClE5az1CyqZdN19bQ_;
static N_NIMCALL(void, Marker_tySequence_8Np6tlClE5az1CyqZdN19bQ)(void* p, NI op) {
	tySequence_8Np6tlClE5az1CyqZdN19bQ* a;
	NI T1_;
	a = (tySequence_8Np6tlClE5az1CyqZdN19bQ*)p;
	T1_ = (NI)0;
}
NIM_EXTERNC N_NOINLINE(void, stdlib_setsInit000)(void) {
}

NIM_EXTERNC N_NOINLINE(void, stdlib_setsDatInit000)(void) {
static TNimNode* TM_0JXiWyhP5OCO8jWMA6sb1w_2[2];
static TNimNode* TM_0JXiWyhP5OCO8jWMA6sb1w_3[2];
static TNimNode TM_0JXiWyhP5OCO8jWMA6sb1w_0[6];
NTI_J1RbOJyRcRl1E5zDK6YKcQ_.size = sizeof(tyObject_HashSet_J1RbOJyRcRl1E5zDK6YKcQ);
NTI_J1RbOJyRcRl1E5zDK6YKcQ_.kind = 18;
NTI_J1RbOJyRcRl1E5zDK6YKcQ_.base = 0;
NTI_J1RbOJyRcRl1E5zDK6YKcQ_.flags = 2;
TM_0JXiWyhP5OCO8jWMA6sb1w_2[0] = &TM_0JXiWyhP5OCO8jWMA6sb1w_0[1];
NTI_1v9bKyksXWMsm0vNwmZ4EuQ_.size = sizeof(tyTuple_1v9bKyksXWMsm0vNwmZ4EuQ);
NTI_1v9bKyksXWMsm0vNwmZ4EuQ_.kind = 18;
NTI_1v9bKyksXWMsm0vNwmZ4EuQ_.base = 0;
NTI_1v9bKyksXWMsm0vNwmZ4EuQ_.flags = 3;
TM_0JXiWyhP5OCO8jWMA6sb1w_3[0] = &TM_0JXiWyhP5OCO8jWMA6sb1w_0[3];
TM_0JXiWyhP5OCO8jWMA6sb1w_0[3].kind = 1;
TM_0JXiWyhP5OCO8jWMA6sb1w_0[3].offset = offsetof(tyTuple_1v9bKyksXWMsm0vNwmZ4EuQ, Field0);
TM_0JXiWyhP5OCO8jWMA6sb1w_0[3].typ = (&NTI_rR5Bzr1D5krxoo1NcNyeMA_);
TM_0JXiWyhP5OCO8jWMA6sb1w_0[3].name = "Field0";
TM_0JXiWyhP5OCO8jWMA6sb1w_3[1] = &TM_0JXiWyhP5OCO8jWMA6sb1w_0[4];
TM_0JXiWyhP5OCO8jWMA6sb1w_0[4].kind = 1;
TM_0JXiWyhP5OCO8jWMA6sb1w_0[4].offset = offsetof(tyTuple_1v9bKyksXWMsm0vNwmZ4EuQ, Field1);
TM_0JXiWyhP5OCO8jWMA6sb1w_0[4].typ = (&NTI_rR5Bzr1D5krxoo1NcNyeMA_);
TM_0JXiWyhP5OCO8jWMA6sb1w_0[4].name = "Field1";
TM_0JXiWyhP5OCO8jWMA6sb1w_0[2].len = 2; TM_0JXiWyhP5OCO8jWMA6sb1w_0[2].kind = 2; TM_0JXiWyhP5OCO8jWMA6sb1w_0[2].sons = &TM_0JXiWyhP5OCO8jWMA6sb1w_3[0];
NTI_1v9bKyksXWMsm0vNwmZ4EuQ_.node = &TM_0JXiWyhP5OCO8jWMA6sb1w_0[2];
NTI_8Np6tlClE5az1CyqZdN19bQ_.size = sizeof(tySequence_8Np6tlClE5az1CyqZdN19bQ*);
NTI_8Np6tlClE5az1CyqZdN19bQ_.kind = 24;
NTI_8Np6tlClE5az1CyqZdN19bQ_.base = (&NTI_1v9bKyksXWMsm0vNwmZ4EuQ_);
NTI_8Np6tlClE5az1CyqZdN19bQ_.flags = 2;
NTI_8Np6tlClE5az1CyqZdN19bQ_.marker = Marker_tySequence_8Np6tlClE5az1CyqZdN19bQ;
TM_0JXiWyhP5OCO8jWMA6sb1w_0[1].kind = 1;
TM_0JXiWyhP5OCO8jWMA6sb1w_0[1].offset = offsetof(tyObject_HashSet_J1RbOJyRcRl1E5zDK6YKcQ, data);
TM_0JXiWyhP5OCO8jWMA6sb1w_0[1].typ = (&NTI_8Np6tlClE5az1CyqZdN19bQ_);
TM_0JXiWyhP5OCO8jWMA6sb1w_0[1].name = "data";
TM_0JXiWyhP5OCO8jWMA6sb1w_2[1] = &TM_0JXiWyhP5OCO8jWMA6sb1w_0[5];
TM_0JXiWyhP5OCO8jWMA6sb1w_0[5].kind = 1;
TM_0JXiWyhP5OCO8jWMA6sb1w_0[5].offset = offsetof(tyObject_HashSet_J1RbOJyRcRl1E5zDK6YKcQ, counter);
TM_0JXiWyhP5OCO8jWMA6sb1w_0[5].typ = (&NTI_rR5Bzr1D5krxoo1NcNyeMA_);
TM_0JXiWyhP5OCO8jWMA6sb1w_0[5].name = "counter";
TM_0JXiWyhP5OCO8jWMA6sb1w_0[0].len = 2; TM_0JXiWyhP5OCO8jWMA6sb1w_0[0].kind = 2; TM_0JXiWyhP5OCO8jWMA6sb1w_0[0].sons = &TM_0JXiWyhP5OCO8jWMA6sb1w_2[0];
NTI_J1RbOJyRcRl1E5zDK6YKcQ_.node = &TM_0JXiWyhP5OCO8jWMA6sb1w_0[0];
}

