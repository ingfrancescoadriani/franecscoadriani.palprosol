// lApproachDll.cpp : Defines the exported functions for the DLL application.
//


#include "stdafx.h"
#include "lApproachDll.h"
#include <cstring>
#include <string>
#include <time.h>
#include <ctime>
#using <System.dll>

using std::string;
using namespace System;
using namespace System::IO;
using namespace System::Security::Cryptography;
using namespace System::Text;
using namespace System::Runtime::InteropServices;


extern "C" {

	#include <stdio.h>
	#include <stdlib.h>
	#include <stdint.h>


	#ifndef UTIL_H
	#define UTIL_H

	#define FALSE 0
	#define TRUE  1

	// definições usadas para k*(L)
	#define SEM_SOLUCAO -1   // não resolvido ainda
	#define HOMOGENEO    0   // resolvido com packing homogêneo
	#define B1           1   // resolvido usando a subdivisão B1
	#define B2           2   // resolvido usando a subdivisão B2
	#define B3           3   // resolvido usando a subdivisão B3
	#define B4           4   // resolvido usando a subdivisão B4
	#define B5           5   // resolvido usando a subdivisão B5
	#define B6           6   // resolvido usando a subdivisão B6
	#define B7           7   // resolvido usando a subdivisão B7

	// tipos
	//typedef int index;
	//typedef int point;
	//typedef short qtde;
	//typedef short flag;

	// macros
	#define SWAP(A,B) {point C=A;A=B;B=C;}
	#define MAX(A,B)  ((A>B) ? (A):(B))

	#define DEITADO 0
	#define DE_PE   1

	#define CORTEVERT 0
	#define CORTEHOR  1
	
	point l, w;
	point nCombLin;
	point *combLin;
	point *iCombLin;

	int *ptoDiv;
	short *solNRet;

	const int ptoDiv1 = 1023;
	const int ptoDiv2 = 1047552;
	const int ptoDiv3 = 1072693248;

	const short nRet = 127;
	const short solucao = 896; 
	const short descSol = 7;
	const short descPtoDiv2 = 10;
	const short descPtoDiv3 = 20;

	qtde ret;
	point **ptoRet;

	qtde m;
	qtde resp;

	#endif

}


//FUNZIONI DI UTIL.C
LAPPROACHDLL_API point fechaEmZ(point p){
  return combLin[ iCombLin [ p ] ];
}

LAPPROACHDLL_API index q2i( point q0, point q1, point q2, point q3 ) {
  
  return ((( iCombLin[ q0 ]*nCombLin) +
                iCombLin[ q1 ])*nCombLin +
                    iCombLin[ q2 ])*nCombLin +
                        iCombLin[ q3 ];
}

LAPPROACHDLL_API qtde R_lim_sup( point x, point y ) {
  
  return (x*y) / (l*w); // A(R) / lw
}

LAPPROACHDLL_API qtde L_lim_sup( point *q ) {
  
  return (q[0]*q[1] - (q[0]-q[2])*(q[1]-q[3])) / (l*w); // A(L) / lw
}


LAPPROACHDLL_API qtde R_lim_inf( point x, point y ) {
  qtde a, b;
  
  a = (x/l)*(y/w);
  b = (x/w)*(y/l);
  
  return MAX( a, b );
}

LAPPROACHDLL_API qtde L_lim_inf( point *q ) {
  qtde a, b;
  
  // "quebra" o L em dois retângulos
  a = R_lim_inf( q[2], q[1] ) + R_lim_inf( q[0]-q[2], q[3] );
  b = R_lim_inf( q[2], q[1]-q[3] ) + R_lim_inf( q[0], q[3] );
  
  return MAX( a, b );
}

LAPPROACHDLL_API void normalizar( point *q ) {

  int i, j, i1, j1;

  i = q[0]; j = q[1]; i1 = q[2]; j1 = q[3];

  // Estes são para eliminar retângulos "degenerateds" pois
  // um retângulo sempre deve ser definido com i=i1 e j=j1.
  if( 0==i1 ) { i1=i; j=j1; }
  else if( 0==j1 ) { j1=j; i=i1; }
  else if ( i1 == i || j1 == j ) { i1=i; j1=j; }
  
  // Se a superfície for menor do que um retangulinho (em particular 
  // se for nula) também descarta.
  if ( i * j - ( i - i1 ) * ( j - j1 ) < l * w ) { q[0]=-1; return; }
  
  // E, se for necessário, isto deita o retângulo
  if ( i == i1 && j == j1 && i < j ) {SWAP(i,j); SWAP(i1,j1) }
  
  // Isto é para, se necessário, deitar Ls
  if( 0<i1  && i1<i  &&  0<j1  && j1<j  && i<j ) { SWAP(i,j); SWAP(i1,j1); }
  else if( 0<i1  && i1<i  &&  0<j1  && j1<j  && i==j  && i1<j1 ) { SWAP(i1,j1); }

  q[0]=i; q[1]=j; q[2]=i1; q[3]=j1;

}

LAPPROACHDLL_API void positionPatternB1( point *i, point *q, point *q1, point *q2 ) {
  
  q1[0] = q[2];
  q1[1] = fechaEmZ( q[1]-i[1] );
  q1[2] = i[0];
  q1[3] = fechaEmZ( q[1]-q[3] );

  q2[0] = q[0];
  q2[1] = q[3];
  q2[2] = fechaEmZ( q[0]-i[0] );
  q2[3] = i[1];
}


LAPPROACHDLL_API void positionPatternB2( point *i, point *q, point *q1, point *q2 ) {
  
  q1[0] = q[2];
  q1[1] = fechaEmZ( q[1]-q[3] );
  q1[2] = fechaEmZ( q[2]-i[0] );
  q1[3] = fechaEmZ( q[1]-i[1] );

  q2[0] = q[0];
  q2[1] = i[1];
  q2[2] = i[0];
  q2[3] = q[3];
}


LAPPROACHDLL_API void positionPatternB3( point *i, point *q, point *q1, point *q2 ) {
  
  q1[0] = q[0];
  q1[1] = q[1];
  q1[2] = i[0];
  q1[3] = i[1];

  q2[0] = fechaEmZ( q[0]-i[0] );
  q2[1] = fechaEmZ( q[1]-i[1] );
  q2[2] = fechaEmZ( q[2]-i[0] );
  q2[3] = fechaEmZ( q[3]-i[1] );
}



LAPPROACHDLL_API void positionPatternB4( point *i, point *q, point *q1, point *q2 ) {
  
  q1[0] = i[0];
  q1[1] = q[1];
  q1[2] = q[2];
  q1[3] = i[1];

  q2[0] = fechaEmZ( q[0]-q[2] );
  q2[1] = q[3];
  q2[2] = fechaEmZ( q[0]-i[0] );
  q2[3] = fechaEmZ( q[3]-i[1] );
}

LAPPROACHDLL_API void positionPatternB5( point *i, point *q, point *q1, point *q2 ) {
  
  q1[0] = q[2];
  q1[1] = q[1];
  q1[2] = i[0];
  q1[3] = fechaEmZ( q[1]-i[1] );
  
  q2[0] = fechaEmZ( q[0]-i[0] );
  q2[1] = q[3];
  q2[2] = fechaEmZ( q[0]-q[2] );
  q2[3] = i[1];
}


LAPPROACHDLL_API void positionPatternB6( point *i, point *q, point *q1, point *q2 ) {
  
  q1[0] = i[2];
  q1[1] = q[1];
  q1[2] = i[0];
  q1[3] = fechaEmZ( q[1]-i[1] );

  q2[0] = fechaEmZ( q[0]-i[0] );
  q2[1] = q[1];
  q2[2] = fechaEmZ( q[0]-i[2] );
  q2[3] = i[1];
}

LAPPROACHDLL_API void positionPatternB7( point *i, point *q, point *q1, point *q2 ) {
  
  q1[0] = q[0];
  q1[1] = fechaEmZ( q[1]-i[1] );
  q1[2] = i[0];
  q1[3] = fechaEmZ( q[1]-i[2] );
  
  q2[0] = q[0];
  q2[1] = i[2];
  q2[2] = fechaEmZ( q[0]-i[0] );
  q2[3] = i[1];
}

//FUNZIONI DI DRAW.C

LAPPROACHDLL_API flag corteR( point x, point y ) {
  qtde a, b;
  
  a = (x/l)*(y/w);
  b = (x/w)*(y/l);
  
  return (a>b) ? DEITADO : DE_PE ;
}

LAPPROACHDLL_API flag corteL( point *q ) {
  qtde a, b;
  
  // "quebra" o L em dois retângulos
  a = R_lim_inf( q[2], q[1] ) + R_lim_inf( q[0]-q[2], q[3] );
  b = R_lim_inf( q[2], q[1]-q[3] ) + R_lim_inf( q[0], q[3] );
  
  return (a>b) ? CORTEVERT : CORTEHOR;  
}

LAPPROACHDLL_API void arrange( qtde id ) {

  if( ptoRet[id][0] > ptoRet[id][2] ) { SWAP( ptoRet[id][0], ptoRet[id][2] ); }
  if( ptoRet[id][1] > ptoRet[id][3] ) { SWAP( ptoRet[id][1], ptoRet[id][3] ); }
}

LAPPROACHDLL_API void arrangeLdegenerated( point *q ) {

  // Estes cuatro son para eliminar retangulos "degenerateds" pois
  // um retangulo sempre deve ser definido com q[0]=q[2] e q[1]=q[3].
  if( 0==q[2] ) { q[2]=q[0]; q[1]=q[3]; }
  else if( 0==q[3] ) { q[3]=q[1]; q[0]=q[2]; }
  else if ( q[2] == q[0] || q[3] == q[1] ) { q[2]=q[0]; q[3]=q[1]; }
}

LAPPROACHDLL_API void translateX( qtde id, point deltaX ) {

  ptoRet[id][0] += deltaX;
  ptoRet[id][2] += deltaX;
}


LAPPROACHDLL_API void translateY( qtde id, point deltaY ) {

  ptoRet[id][1] += deltaY;
  ptoRet[id][3] += deltaY;
}


LAPPROACHDLL_API void P1( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY ) {

  qtde i;

  for( i=inicio; i<fim; i++ ) {
    ptoRet[i][1] = q[1]-ptoRet[i][1];
    ptoRet[i][3] = q[1]-ptoRet[i][3];

    arrange(i);

    translateX( i, deltaX );
    translateY( i, deltaY );
  }
}


LAPPROACHDLL_API void P2( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY ) {
  
  qtde i;
  
  for( i=inicio; i<fim; i++ ) {
    ptoRet[i][0] = q[0]-ptoRet[i][0];
    ptoRet[i][2] = q[0]-ptoRet[i][2];

    arrange(i);

    translateX( i, deltaX );
    translateY( i, deltaY );
  }
}

LAPPROACHDLL_API void P3( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY ) {
  
  qtde i;
  
  for( i=inicio; i<fim; i++ ) {
    ptoRet[i][0] = q[0]-ptoRet[i][0];
    ptoRet[i][2] = q[0]-ptoRet[i][2];

    ptoRet[i][1] = q[1]-ptoRet[i][1];
    ptoRet[i][3] = q[1]-ptoRet[i][3];

    arrange(i);

    translateX( i, deltaX );
    translateY( i, deltaY );
  }
}


LAPPROACHDLL_API void P4( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY ) {
  
  qtde i;
  
  for( i=inicio; i<fim; i++ ) {
    translateX( i, deltaX );
    translateY( i, deltaY );
  }
}

LAPPROACHDLL_API void P5( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY ) {
  
  qtde i;
  point tmp1, tmp2;

  for( i=inicio; i<fim; i++ ) {
    tmp1 = ptoRet[i][1];
    tmp2 = ptoRet[i][3];

    ptoRet[i][1] = q[0]-ptoRet[i][0];
    ptoRet[i][3] = q[0]-ptoRet[i][2];

    ptoRet[i][0] = tmp1;
    ptoRet[i][2] = tmp2;

    arrange(i);

    translateX( i, deltaX );
    translateY( i, deltaY );
  }
}

LAPPROACHDLL_API void P6( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY ) {
  
  qtde i;
  point tmp1, tmp2;
  
  for( i=inicio; i<fim; i++ ) {
    tmp1 = ptoRet[i][0];
    tmp2 = ptoRet[i][2];

    ptoRet[i][0] = q[1]-ptoRet[i][1];
    ptoRet[i][2] = q[1]-ptoRet[i][3];

    ptoRet[i][1] = tmp1;
    ptoRet[i][3] = tmp2;

    arrange(i);

    translateX( i, deltaX );
    translateY( i, deltaY );
  }
}


LAPPROACHDLL_API void P7( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY ) {
  
  qtde i;
  point tmp1, tmp2;

  for( i=inicio; i<fim; i++ ) {
    tmp1 = q[0]-ptoRet[i][0];
    tmp2 = q[0]-ptoRet[i][2];

    ptoRet[i][0] = q[1]-ptoRet[i][1];
    ptoRet[i][2] = q[1]-ptoRet[i][3];

    ptoRet[i][1] = tmp1;
    ptoRet[i][3] = tmp2;

    arrange(i);

    translateX( i, deltaX );
    translateY( i, deltaY );
  }
}


LAPPROACHDLL_API void P8( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY ) {
  
  qtde i;
  point tmp1, tmp2;

  for( i=inicio; i<fim; i++ ) {
    tmp1 = ptoRet[i][0];
    tmp2 = ptoRet[i][2];

    ptoRet[i][0] = ptoRet[i][1];
    ptoRet[i][2] = ptoRet[i][3];

    ptoRet[i][1] = tmp1;
    ptoRet[i][3] = tmp2;

    translateX( i, deltaX );
    translateY( i, deltaY );
  }
}

LAPPROACHDLL_API void DrawB1( index L, point *q ) {
  index L1, L2;
  point q1[4], q2[4], tmp[4];
  point largura, altura;
  point deltaX, deltaY;
  qtde inicio, fim;
  point div[2];

  div[0] = ptoDiv[L] & ptoDiv1;
  div[1] = (ptoDiv[L] & ptoDiv2) >> descPtoDiv2;
  positionPatternB1( div, q, q1, q2 );

  // desenha L1
  tmp[0] = q1[0];
  tmp[1] = q1[1];
  tmp[2] = q1[2];
  tmp[3] = q1[3];
  arrangeLdegenerated( tmp );

  normalizar( q1 );
  L1 = q2i( q1[0], q1[1], q1[2], q1[3] );

  inicio = ret;
  DrawR( L1, q1 );
  fim = ret;
  
  deltaX = 0; deltaY = div[1];
  if( div[0] == 0 ) deltaY = q[3];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }

  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P8( inicio, fim, L1, q1, deltaX, deltaY );
  }
  else {
    if( largura>=altura ) P1( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P5( inicio, fim, L1, q1, deltaX, deltaY );
  }
  
  // desenha L2
  tmp[0] = q2[0];
  tmp[1] = q2[1];
  tmp[2] = q2[2];
  tmp[3] = q2[3];
  arrangeLdegenerated( tmp );

  normalizar( q2 );
  L2 = q2i( q2[0], q2[1], q2[2], q2[3] );

  inicio = ret;
  DrawR( L2, q2 );
  fim = ret;
  
  deltaX = 0; deltaY = 0;
  if( div[1]==0 ) deltaX = div[0];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }

  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P8( inicio, fim, L2, q2, deltaX, deltaY );
  }
  else {
    if( largura>=altura ) P2( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P6( inicio, fim, L2, q2, deltaX, deltaY );
  }
}

LAPPROACHDLL_API void DrawB2( index L, point *q ) {
  index L1, L2;
  point q1[4], q2[4], tmp[4];
  point largura, altura;
  point deltaX, deltaY;
  qtde inicio, fim;
  point div[3];

  div[0] = ptoDiv[L] & ptoDiv1;
  div[1] = (ptoDiv[L] & ptoDiv2) >> descPtoDiv2;
  positionPatternB2( div, q, q1, q2 );

  // desenha L1
  tmp[0] = q1[0];
  tmp[1] = q1[1];
  tmp[2] = q1[2];
  tmp[3] = q1[3];
  arrangeLdegenerated( tmp );

  normalizar( q1 );
  L1 = q2i( q1[0], q1[1], q1[2], q1[3] );

  inicio = ret;
  DrawR( L1, q1 );
  fim = ret;

  deltaX = 0; deltaY = q[3];
  if( div[1]==q[1] ) deltaX = div[0];
  else if( tmp[0]==tmp[2] )  deltaY = div[1];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P8( inicio, fim, L1, q1, deltaX, deltaY );
  }
  else {
    if( largura>=altura ) P3( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P7( inicio, fim, L1, q1, deltaX, deltaY );
  }
  
  // desenha L2
  tmp[0] = q2[0];
  tmp[1] = q2[1];
  tmp[2] = q2[2];
  tmp[3] = q2[3];
  arrangeLdegenerated( tmp );

  normalizar( q2 );
  L2 = q2i( q2[0], q2[1], q2[2], q2[3] );

  inicio = ret;
  DrawR( L2, q2 );
  fim = ret;
  
  deltaX = 0; deltaY = 0;

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( largura>=altura ) P4( inicio, fim, L2, q2, deltaX, deltaY );
  else                  P8( inicio, fim, L2, q2, deltaX, deltaY );
}


LAPPROACHDLL_API void DrawB3( index L, point *q ) {
  index L1, L2;
  point q1[4], q2[4], tmp[4];
  point largura, altura;
  point deltaX, deltaY;
  qtde inicio, fim;
  point div[2];

  div[0] = ptoDiv[L] & ptoDiv1;
  div[1] = (ptoDiv[L] & ptoDiv2) >> descPtoDiv2;
  positionPatternB3( div, q, q1, q2 );

  // desenha L1
  tmp[0] = q1[0];
  tmp[1] = q1[1];
  tmp[2] = q1[2];
  tmp[3] = q1[3];
  arrangeLdegenerated( tmp );

  normalizar( q1 );
  L1 = q2i( q1[0], q1[1], q1[2], q1[3] );

  inicio = ret;
  DrawR( L1, q1 );
  fim = ret;
  
  deltaX = 0; deltaY = 0;

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( largura>=altura ) P4( inicio, fim, L1, q1, deltaX, deltaY );
  else                  P8( inicio, fim, L1, q1, deltaX, deltaY );
  
  // desenha L2
  tmp[0] = q2[0];
  tmp[1] = q2[1];
  tmp[2] = q2[2];
  tmp[3] = q2[3];
  arrangeLdegenerated( tmp );

  normalizar( q2 );
  L2 = q2i( q2[0], q2[1], q2[2], q2[3] );

  inicio = ret;
  DrawR( L2, q2 );
  fim = ret;
  
  deltaX = div[0]; deltaY = div[1];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( largura>=altura ) P4( inicio, fim, L2, q2, deltaX, deltaY );
  else                  P8( inicio, fim, L2, q2, deltaX, deltaY );
}

LAPPROACHDLL_API void DrawB4( index L, point *q ) {
  index L1, L2;
  point q1[4], q2[4], tmp[4];
  point largura, altura;
  point deltaX, deltaY;
  qtde inicio, fim;
  point div[2];

  div[0] = ptoDiv[L] & ptoDiv1;
  div[1] = (ptoDiv[L] & ptoDiv2) >> descPtoDiv2;
  positionPatternB4( div, q, q1, q2 );

  // desenha L1
  tmp[0] = q1[0];
  tmp[1] = q1[1];
  tmp[2] = q1[2];
  tmp[3] = q1[3];
  arrangeLdegenerated( tmp );

  normalizar( q1 );
  L1 = q2i( q1[0], q1[1], q1[2], q1[3] );

  inicio = ret;
  DrawR( L1, q1 );
  fim = ret;
  
  deltaX = 0; deltaY = 0;

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( largura>=altura ) P4( inicio, fim, L1, q1, deltaX, deltaY );
  else                  P8( inicio, fim, L1, q1, deltaX, deltaY );
  
  // desenha L2
  tmp[0] = q2[0];
  tmp[1] = q2[1];
  tmp[2] = q2[2];
  tmp[3] = q2[3];
  arrangeLdegenerated( tmp );

  normalizar( q2 );
  L2 = q2i( q2[0], q2[1], q2[2], q2[3] );

  inicio = ret;
  DrawR( L2, q2 );
  fim = ret;
  
  deltaX = q[3]; deltaY = 0;
  if( div[0]==q[0] ) deltaY = div[1];
  else if( tmp[0]==tmp[2] )  deltaX = div[0];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P8( inicio, fim, L2, q2, deltaX, deltaY );
  }
  else {
    if( largura>altura ) P3( inicio, fim, L2, q2, deltaX, deltaY );
    else                 P7( inicio, fim, L2, q2, deltaX, deltaY );
  }
}


LAPPROACHDLL_API void DrawB5( index L, point *q ) {
  index L1, L2;
  point q1[4], q2[4], tmp[4];
  point largura, altura;
  point deltaX, deltaY;
  qtde inicio, fim;
  point div[2];

  div[0] = ptoDiv[L] & ptoDiv1;
  div[1] = (ptoDiv[L] & ptoDiv2) >> descPtoDiv2;
  positionPatternB5( div, q, q1, q2 );

  // desenha L1
  tmp[0] = q1[0];
  tmp[1] = q1[1];
  tmp[2] = q1[2];
  tmp[3] = q1[3];
  arrangeLdegenerated( tmp );

  normalizar( q1 );
  L1 = q2i( q1[0], q1[1], q1[2], q1[3] );

  inicio = ret;
  DrawR( L1, q1 );
  fim = ret;
  
  deltaX = 0; deltaY = 0;
  if( div[0]==0 ) deltaY = div[1];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P8( inicio, fim, L1, q1, deltaX, deltaY );
  }
  else {
    if( largura>=altura ) P1( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P5( inicio, fim, L1, q1, deltaX, deltaY );
  }
  
  // desenha L2
  tmp[0] = q2[0];
  tmp[1] = q2[1];
  tmp[2] = q2[2];
  tmp[3] = q2[3];
  arrangeLdegenerated( tmp );

  normalizar( q2 );
  L2 = q2i( q2[0], q2[1], q2[2], q2[3] );

  inicio = ret;
  DrawR( L2, q2 );
  fim = ret;
  
  deltaX = div[0]; deltaY = 0;
  if( div[1]==0) deltaX = q[2];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P8( inicio, fim, L2, q2, deltaX, deltaY );
  }
  else {
    if( largura>=altura ) P2( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P6( inicio, fim, L2, q2, deltaX, deltaY );
  }
}

LAPPROACHDLL_API void DrawB6( index L, point *q ) {
  index L1, L2;
  point q1[4], q2[4], tmp[4];
  point largura, altura;
  point deltaX, deltaY;
  qtde inicio, fim;
  point div[3];

  div[0] = ptoDiv[L] & ptoDiv1;
  div[1] = (ptoDiv[L] & ptoDiv2) >> descPtoDiv2;
  div[2] = (ptoDiv[L] & ptoDiv3) >> descPtoDiv3;
  positionPatternB6( div, q, q1, q2 );

  // desenha L1
  tmp[0] = q1[0];
  tmp[1] = q1[1];
  tmp[2] = q1[2];
  tmp[3] = q1[3];
  arrangeLdegenerated( tmp );

  normalizar( q1 );
  L1 = q2i( q1[0], q1[1], q1[2], q1[3] );

  inicio = ret;
  DrawR( L1, q1 );
  fim = ret;
  
  deltaX = 0; deltaY = 0;
  if( div[0]==0 ) deltaY = div[1];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }

  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P8( inicio, fim, L1, q1, deltaX, deltaY );
  }
  else {
    if( largura>=altura ) P1( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P5( inicio, fim, L1, q1, deltaX, deltaY );
  }
  
  // desenha L2
  tmp[0] = q2[0];
  tmp[1] = q2[1];
  tmp[2] = q2[2];
  tmp[3] = q2[3];
  arrangeLdegenerated( tmp );

  normalizar( q2 );
  L2 = q2i( q2[0], q2[1], q2[2], q2[3] );

  inicio = ret;
  DrawR( L2, q2 );
  fim = ret;
  
  deltaX = div[0]; deltaY = 0;
  if( div[1]==0 ) deltaX = div[2];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }

  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P8( inicio, fim, L2, q2, deltaX, deltaY );
  }
  else {
    if( largura>=altura ) P2( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P6( inicio, fim, L2, q2, deltaX, deltaY );
  }
}

LAPPROACHDLL_API void DrawB7( index L, point *q ) {
  index L1, L2;
  point q1[4], q2[4], tmp[4];
  point largura, altura;
  point deltaX, deltaY;
  qtde inicio, fim;
  point div[3];

  div[0] = ptoDiv[L] & ptoDiv1;
  div[1] = (ptoDiv[L] & ptoDiv2) >> descPtoDiv2;
  div[2] = (ptoDiv[L] & ptoDiv3) >> descPtoDiv3;
  positionPatternB7( div, q, q1, q2 );

  // desenha L1
  tmp[0] = q1[0];
  tmp[1] = q1[1];
  tmp[2] = q1[2];
  tmp[3] = q1[3];
  arrangeLdegenerated( tmp );

  normalizar( q1 );
  L1 = q2i( q1[0], q1[1], q1[2], q1[3] );

  inicio = ret;
  DrawR( L1, q1 );
  fim = ret;
  
  deltaX = 0; deltaY = div[1];
  if( div[0]==0 ) deltaY = div[2];
  
  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }
  
  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P8( inicio, fim, L1, q1, deltaX, deltaY );
  }
  else {
    if( largura>=altura ) P1( inicio, fim, L1, q1, deltaX, deltaY );
    else                  P5( inicio, fim, L1, q1, deltaX, deltaY );
  }
  
  // desenha L2
  tmp[0] = q2[0];
  tmp[1] = q2[1];
  tmp[2] = q2[2];
  tmp[3] = q2[3];
  arrangeLdegenerated( tmp );

  normalizar( q2 );
  L2 = q2i( q2[0], q2[1], q2[2], q2[3] );

  inicio = ret;
  DrawR( L2, q2 );
  fim = ret;
  
  deltaX = 0; deltaY = 0;
  if( div[1]==0 ) deltaX = div[0];

  if( tmp[0]!=tmp[1] ) { largura = tmp[0]; altura = tmp[1]; }
  else               { largura = tmp[2]; altura = tmp[3]; }

  if( tmp[0]==tmp[2] ) {
    if( largura>=altura ) P4( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P8( inicio, fim, L2, q2, deltaX, deltaY );
  }
  else {  
    if( largura>=altura ) P2( inicio, fim, L2, q2, deltaX, deltaY );
    else                  P6( inicio, fim, L2, q2, deltaX, deltaY );
  }
}

LAPPROACHDLL_API void DrawRetangulos( point x, point y ) {
  int i, j;

  flag corte = corteR( x, y );

  if( corte == DEITADO )
    for( i=0; i+l<=x; i+=l )
      for( j=0; j+w<=y; j+=w ) {
        ptoRet[ret][0] = i;
        ptoRet[ret][1] = j;
        ptoRet[ret][2] = i+l;
        ptoRet[ret][3] = j+w;
        ret++;
      }

  else
    for( i=0; i+w<=x; i+=w )
      for( j=0; j+l<=y; j+=l ) {
        ptoRet[ret][0] = i;
        ptoRet[ret][1] = j;
        ptoRet[ret][2] = i+w;
        ptoRet[ret][3] = j+l;
        ret++;
      }
}

LAPPROACHDLL_API void DrawR( index L, point *q ) {
  int i;
  qtde inicio, fim;

  switch( (solNRet[L] & solucao) >> descSol ) {
  case HOMOGENEO:

    // L não degenerated
    if( q[0] != q[2] ) {

      flag corte = corteL( q );
      if( corte == CORTEVERT ) {
        DrawRetangulos( q[2], q[1] );
        inicio = ret;
        DrawRetangulos( fechaEmZ( q[0]-q[2] ), q[3] );
        fim = ret;
        for( i=inicio; i<fim; i++ )
          translateX( i, q[2] );
      }
      else {
        inicio = ret;
        DrawRetangulos( q[2], fechaEmZ( q[1]-q[3] ) );
        fim = ret;
        for( i=inicio; i<fim; i++ )
          translateY( i, q[3] );
        DrawRetangulos( q[0], q[3] );
      }
    }
    // L degenerated (R)
    else
      DrawRetangulos( q[0], q[1] );

    break;

	  case B1:  DrawB1( L, q ); break;
	  case B2:  DrawB2( L, q ); break;
	  case B3:  DrawB3( L, q ); break;
	  case B4:  DrawB4( L, q ); break;
	  case B5:  DrawB5( L, q ); break;
	  case B6:  DrawB6( L, q ); break;
	  case B7:  DrawB7( L, q ); break;
	  default:
		//printf("Soluzione inesistente: %d\n", (solNRet[L] & solucao) >> descSol );
		exit(0);
		break;
  }

}


LAPPROACHDLL_API void MakeFile( index L, point *q, point ***solInd){
	//printf("\nEntrato in makeFile\n");
	/*system("Pause");*/
  int i;
  qtde n;
  //FILE *file;

  //file = fopen( "solution.txt", "w" );
  //if( file==NULL ) {
  //  printf("Erro ao abrir o arquivo solution.txt\n");
  //  exit(0);
  //}
  /*printf("file aperto\n");
  system("pause");*/
  n = solNRet[L] & nRet;
  m = n;
  //point **sol = *solInd;
  //printf("\nAllocando solProv... ");
  point **solProv = (point **) malloc(n*sizeof(point*));
	if(solProv==NULL){
		//printf("\nErrore in allocazione solProv");
		exit(0);
	}

	for(i=0;i<n;i++){
		solProv[i] = (point*) malloc(4* sizeof(point));
		if(solProv[i] == NULL){
			//printf("\nErrore in allocazioen solProv[%d]", i);
			exit(0);
		}
	}

	//printf("Ok.\n");

  //printf("Inizio scrittura...");
  /*system("pause");*/
  for( i=0; i<m; i++){

    //fprintf( file, "%d %d %d %d\n", (int)ptoRet[i][0], (int)ptoRet[i][1], (int)ptoRet[i][2], (int)ptoRet[i][3] );
	solProv[i][0] = ptoRet[i][0];
	solProv[i][1] = ptoRet[i][1];
	solProv[i][2] = ptoRet[i][2];
	solProv[i][3] = ptoRet[i][3];
	
  }
  //printf(" Ok.\n");
  //printf("Valori inseriti in solProv\n");
  //printf("solProv: %d %d %d %d\n", (int)solProv[0][0], (int)solProv[0][1], (int)solProv[0][2], (int)solProv[0][3]); 

  *solInd = solProv;
  //printf("*solInd = solProv: DONE\n");
  //printf("*solInd: %d %d %d %d\n", (int)*solInd[0][0], (int)*solInd[0][1], (int)solProv[0][2], (int)solProv[0][3]);

  //fclose( file );
}

LAPPROACHDLL_API void Draw( index L, point *q, point ***solInd ) {
	//printf("\nEntrato in draw\n");
	
  int i;
  qtde n;

  n = solNRet[L] & nRet;

  ptoRet = (point**) malloc( n*sizeof( point* ) );
  if( ptoRet == NULL ) {
    //printf("\nErrore di allocazione di memoria\n");
    exit(0);
  }
  for( i=0; i<n; i++ ) {
    ptoRet[i] = (point*) malloc( 4*sizeof( point ) );
    if( ptoRet[i] == NULL ) {
      //printf("\nErrore di allocazione di memoria\n");
      exit(0);
    }
  }

  ret = 0;

  DrawR( L, q );

  
  MakeFile( L, q, solInd );

  for( i=0; i<n; i++ )
    free( ptoRet[i] );
  free( ptoRet );
}


//FUNZIONI DI LAPPROACH.C
LAPPROACHDLL_API void divide( point *i, point *q, point *q1, point *q2, void (*func)( point*, point*, point*, point* ) ) {

  // deixa na posição padrão
  (*func)( i, q, q1, q2 );
  
  // normaliza os L's
  normalizar( q1 );
  normalizar( q2 );
}

LAPPROACHDLL_API void divideL( index L, point *q, point *lim, flag B, void (*func)( point*, point*, point*, point* ) ) {
  int i, j;
  point i_k[2]; // point de divisão
  index L1, L2; // novos L's formados com a divisão no point i_k
  point q1[4], q2[4];
  
  // limites para cada divisão
  for( i_k[0]=lim[0], i=iCombLin[ i_k[0] ]; i_k[0]<=lim[1]; i_k[0]=combLin[++i] )
    for( i_k[1]=lim[2], j=iCombLin[ i_k[1] ]; i_k[1]<=lim[3]; i_k[1]=combLin[++j] ) {
      
      // função usada para divisão
      divide( i_k, q, q1, q2, (*func) );
      
      if( q1[0]<0 || q2[0]<0 ) continue;
      
      L1 = q2i( q1[0], q1[1], q1[2], q1[3] ); L2 = q2i( q2[0], q2[1], q2[2], q2[3] );
      
      // verifica se a soma do limite superior dos 2 novos L's
      //   é maior que o L original (L pai)
      if( L_lim_sup(q1)+L_lim_sup(q2) > (solNRet[L] & nRet) ) {
        Solve(L1,q1); Solve(L2,q2);
        
        // verifica se a soma do número de retângulos dos 2 novos L's
        //   é maior q o L original (L pai)
        if( ((solNRet[L1]+solNRet[L2]) & nRet) > (solNRet[L] & nRet) ) {
          solNRet[L] = ((solNRet[L1]+solNRet[L2]) & nRet) | (B << descSol);               // divisão usada
          ptoDiv[L] = i_k[0] | (i_k[1] << descPtoDiv2);
        }
      }
    }
}

LAPPROACHDLL_API void divideR( index L, point *q, flag indicador, void (*func)( point*, point*, point*, point* ) ) {
  int i, j, k; // indexs do vetor de combinacoes lineares
  point i_k[3]; // point de divisão
  index L1, L2; // novos L's formados com a divisão no point i
  point q1[4], q2[4];
  
  // limites para cada divisão
  for( i_k[0]=0, i=iCombLin[ i_k[0] ]; i_k[0]<=q[0]; i_k[0]=combLin[++i] )
    for( i_k[1]=0, j=iCombLin[ i_k[1] ]; i_k[1]<=q[1]; i_k[1]=combLin[++j] )
      for( i_k[2]=i_k[indicador], k=iCombLin[ i_k[2] ]; i_k[2]<=q[indicador]; i_k[2]=combLin[++k] ) {
        
        if( i_k[0]==0 && i_k[1]==0 ) continue;
        
        // função usada para divisão
        divide( i_k, q, q1, q2, (*func) );
        
        if( q1[0]<0 || q2[0]<0 )  continue;
        
        L1 = q2i( q1[0], q1[1], q1[2], q1[3] ); L2 = q2i( q2[0], q2[1], q2[2], q2[3] );
        
        // verifica se a soma do limite superior dos 2 novos L's
        //   é maior que o R original (R pai)
        if( L_lim_sup( q1 )+L_lim_sup( q2 ) > (solNRet[L] & nRet) ) {
          Solve(L1,q1); Solve(L2,q2);
          
          // verifica se a soma do número de retângulos dos 2 novos L's
          //   é maior q o R original (R pai)
          if( ((solNRet[L1]+solNRet[L2]) & nRet) > (solNRet[L] & nRet) ) {
            solNRet[L] = ((solNRet[L1]+solNRet[L2]) & nRet) | (((indicador==0) ? B6 : B7) << descSol); // divisão usada
            ptoDiv[L] = i_k[0] | (i_k[1] << descPtoDiv2) | (i_k[2] << descPtoDiv3);
          }
        }
      }
}

LAPPROACHDLL_API void Solve( index L, point *q ) {
  
  if( (solNRet[ L ] & nRet) == 0 ) {
    
    // L não degenerated
    if( q[0]!=q[2] ) {
      
      solNRet[ L ] = L_lim_inf( q ) | (HOMOGENEO << descSol);

      // Verifica se pode ser resolvido com packing homogeneo
      if( (solNRet[ L ] & nRet) != L_lim_sup( q ) )
      // tenta para cada subdivisão B1,..,B5
      {
        point lim[4];
        
        // 0 <= x' <= x  e  0 <= y' <= y
        lim[0]=0; lim[1]=q[2]; lim[2]=0; lim[3]=q[3];
        divideL( L, q, lim, B1, positionPatternB1 );
        divideL( L, q, lim, B3, positionPatternB3 );
        divideL( L, q, lim, B5, positionPatternB5 );
        
        // 0 <= x' <= x  e  y <= y' <= Y
        lim[0]=0; lim[1]=q[2]; lim[2]=q[3]; lim[3]=q[1];
        divideL( L, q, lim, B2, positionPatternB2 );
        
        // x <= x' <= X  e  0 <= y' <= y
        lim[0]=q[2]; lim[1]=q[0]; lim[2]=0; lim[3]=q[3];
        divideL( L, q, lim, B4, positionPatternB4 );
      }
    }
    
    // L degenerated (R)
    else {  // q[0]==q[2]  <=>  q[1]==q[3]
      solNRet[ L ] = R_lim_inf( q[0], q[1] ) | (HOMOGENEO << descSol);

      // Verifica se pode ser resolvido com packing homogeneo
      if( (solNRet[ L ] & nRet) != R_lim_sup( q[0], q[1] ) )
      // tenta para cada subdivisão B6, B7
      {
        divideR( L, q, 0, positionPatternB6 );
        divideR( L, q, 1, positionPatternB7 );
      }
    }
  }
}

LAPPROACHDLL_API void L_Algorithm( point *q , point ***solInd) {
	//printf("Entrato con parametro\n");
	
  index L;
  int i;
  int nL = nCombLin*nCombLin*nCombLin*nCombLin;
  //struct tms starttime, currenttime;

  L = q2i( q[0], q[1], q[2], q[3] );

  //printf("Allocando il vettore ptoDiv... ");
  ptoDiv = (point*) malloc( nL*sizeof( point ) );
  if( ptoDiv == NULL ) {
    //printf("\nErrore di allocazione di memoria\n");
    exit(0);
  }
  //printf("OK.\n");

  //printf("Allocando il vettore nRet... ");
  solNRet = (qtde*) malloc( nL*sizeof( qtde ) );
  if( solNRet == NULL ) {
    //printf("\nErrore di allocazione di memoria\n");
    exit(0);
  }
  //printf("OK.\n");
  
  for( i=0; i<nL; i++ ) solNRet[i] = 0;

  //printf("Entrando no Solve...\n\n");
  //times(&starttime);
  Solve( L, q );
  //times(&currenttime);

  //printf("Tempo do Solve : %gs.\n", (double)( currenttime.tms_utime - starttime.tms_utime )/60 );

  resp = solNRet[L] & nRet;
  //printf("\nUscito da solve\n");

  //times(&starttime);
  Draw( L, q, solInd);
  //times(&currenttime);

  //printf("Tempo do Draw  : %gs.\n", (double)( currenttime.tms_utime - starttime.tms_utime )/60 );

  free( ptoDiv );
  free( solNRet );
}


LAPPROACHDLL_API void princ(int L_pallet, int W_pallet, int l_box, int w_box, int* vecSol, int* dimension){
	int i, j;
	  point q[4];
  point l_gde, w_gde; // dimensões do retângulo maior (palete)
  //struct tms starttime, currenttime;
  
  //printf ("Digite L e W: "); scanf("%hi %hi", &l_gde, &w_gde);
  //printf ("Digite l e w: "); scanf("%hi %hi", &l, &w);


  //printf ("Digite L e W: "); scanf("%d %d", &l_gde, &w_gde);
  //printf ("Digite l e w: "); scanf("%d %d", &l, &w);

  l_gde = (point) L_pallet;
  w_gde = (point) W_pallet;
  l = (point) l_box;
  w = (point) w_box;

  //printf("\nAllocando il vettore iCombLin... ");
  iCombLin = (point*) malloc( (l_gde+2) * sizeof( point ) );
  if( iCombLin == NULL ) {
    //printf("\nErrore nell'allocazione di memoria\n");
    exit(0);
  }
  //printf("OK.\n");

  //times(&starttime);

  // ===================================
  //  Combinações lineares de l's e w's 
  // ===================================
  
  // inicializacao do vetor de indexs de combinacoes lineares
  for( i=0; i<=l_gde; i++ )  iCombLin[i]=FALSE;
  
  // se j eh combinacao linear de l e w, entao marca como TRUE.
  // ordem da combinacao:
  //   0, w, 2w,.., l, l+w, l+2w,.., 2l, 2l+w, 2l+2w,..., nl+mw

  //printf("Inizializzando le combinazioni lineari... ");
  i=0;
  while( 1 ) {
    for( j=i*l; j<=l_gde; j+=w )
      iCombLin[j] = TRUE;     // é combinação
    if( i++*l>l_gde ) break;  // acabou as combinacoes lineares
  }
  //printf("OK.\n");

  //printf("Allocando il vettore combLin... ");
  combLin = (point*) malloc( (l_gde+2) * sizeof( point ) );
  if( combLin == NULL ) {
    //printf("\nErro de alocacao de memoria\n");
    exit(0);
  }
  //printf("OK.\n");

  // =========================================
  //  arrangendo os vetores combLin e iCombLin
  // =========================================
  //printf("Arrangiamento del vettore degli indici e delle combinazioni lineari... ");
  nCombLin=0;
  for( i=0; i<=l_gde; i++ ) {
    
    if( iCombLin[i] ) {
      combLin[nCombLin] = i;
      iCombLin[i] = nCombLin;
      nCombLin++;
    }
    else iCombLin[i] = iCombLin[i-1];
  }
  
  // marcando o final do vetor
  iCombLin[l_gde+1] = iCombLin[l_gde]+1;
  combLin[nCombLin] = l_gde+1;
  //printf("OK.\n");

  l_gde = fechaEmZ( l_gde );
  w_gde = fechaEmZ( w_gde );

  q[0] = l_gde;
  q[1] = w_gde;
  q[2] = l_gde;
  q[3] = w_gde;


  point **sol;

  sol=(point **) malloc(sizeof(point *));
  if(sol == NULL)
  {
	  //printf("\nErrore allocazione di sol\n");
	  
	  exit(0);
  }


  //printf("Entrando nell'algoritmo...\n");
  //TODO passare bene il parametro coi risultati
  L_Algorithm( q , &sol);

  //times(&currenttime);
 /* vecSol = (int *) realloc(vecSol, 4*m*sizeof(point*));*/
  
  //printf("m: %d", m);


    for(i=0; i<m; i++){
	  vecSol[i] = (int)sol[i][0];
	  vecSol[(m+i)] = (int)sol[i][1];
	  vecSol[(2*m)+i] = (int)sol[i][2];
	  vecSol[(3*m)+i] = (int)sol[i][3];

  }
  

	*dimension = (int) (m*4);
  free( combLin );
  free( iCombLin );
  
  for(i=0; i<m; i++){
	  free(sol[i]);
  }
  free(sol);
   
}
LAPPROACHDLL_API void funcMD5(const char* input, char** output){
	Encoding^ utf8 = Encoding::UTF8;
	std::string s1(input);
	std::string s(("prefisso" + s1) + "suffisso");
	String^ inString = gcnew String(s.c_str());
	array<Byte>^ inBytes = MD5::Create()->ComputeHash(utf8->GetBytes(inString));
	StringBuilder^ sBuilder = gcnew StringBuilder();

	int i;

	for(i=0; i< inBytes.Length; i++) {
		sBuilder->Append(inBytes[i].ToString("x2"));
	}

	String^ outString = sBuilder->ToString();
	*output = (char*)(Marshal::StringToHGlobalAnsi(outString)).ToPointer();
}

LAPPROACHDLL_API void decrypt(char* cipherTextIn, char** out_stringChar) {
    std::string s(cipherTextIn);
	String^ cipherText = gcnew String(s.c_str());
	Encoding^ utf8 = Encoding::UTF8;
	Encoding^ ascii = Encoding::ASCII;
	const int keySize = 256;
	String^ passPhrase = "M**reyup+ate#Ruye+a7EpEjaq@r_vexT3swah56wu?ezefuwabucre8udafura3";
	array<Byte>^ initVectorBytes = ascii->GetBytes("tu89ge6i340t89u2");

	array<Byte>^ cipherTextBytes = Convert::FromBase64String(cipherText);
	array<Byte>^ salt;

	PasswordDeriveBytes^ password = gcnew PasswordDeriveBytes(passPhrase, salt);
	array<Byte>^ keyBytes = password->GetBytes(keySize/8);

	RijndaelManaged^ symmetricKey = gcnew RijndaelManaged();
	symmetricKey->Mode = CipherMode::CBC;

	ICryptoTransform^ decryptor = symmetricKey->CreateDecryptor(keyBytes, initVectorBytes);
	MemoryStream^ memoryStream = gcnew MemoryStream(cipherTextBytes);
	CryptoStream^ cryptoStream = gcnew CryptoStream(memoryStream, decryptor, CryptoStreamMode::Read);

	array<Byte>^ plainTextBytes = gcnew array<Byte>(cipherTextBytes->Length);
	int decryptedByteCount = cryptoStream->Read(plainTextBytes, 0, plainTextBytes->Length);
	String^ out_string = utf8->GetString(plainTextBytes, 0, decryptedByteCount);
	*out_stringChar = (char*)(Marshal::StringToHGlobalAnsi(out_string)).ToPointer();
}


extern "C" LAPPROACHDLL_API bool checkIdPcLicence(char* cbcDate, char* id, char* hashId) {
	/**
	 * Add a fixed salt to the provided id, hash it with MD5 and return false if the
	 * result doesn't match the hashId argument.
	 */
	char* idSalt = "Al2405KvMz=%*39gfMQWocNT03578)-D_ad92rihgbnN%gf893G";
	int idSaltLength = strlen(idSalt);
	int idLenght = strlen(id);
	char* saltedId = (char*)malloc((idSaltLength + idLenght + 1) * sizeof *saltedId);
	strcpy(saltedId, idSalt);
	strcat(saltedId, id);
	char* hashedSaltedId;
	funcMD5(saltedId, &hashedSaltedId);
	free(saltedId);

	if(strcmp(hashedSaltedId, hashId) != 0)
		return false;

	// **Controllo delle date.**
	// Decrypt cbcDate, ottenendo una stringa di 16 caratteri.
	// Separa le due date come startDate e endDate.
	// Verifica che la data odierna sia contenuta nelle due.
	// Altrimenti return false.
	char** dateFromToChar = new char *[100];
	decrypt(cbcDate, dateFromToChar);
	std::string dateFromTo(*dateFromToChar);

	try
	{
		tm tm1,tm2;
		sscanf_s(*dateFromToChar,"%2d%2d%4d%2d%2d%4d",&tm1.tm_mday,&tm1.tm_mon,&tm1.tm_year,&tm2.tm_mday,&tm2.tm_mon,&tm2.tm_year);
		tm1.tm_year = tm1.tm_year-1900;
		tm2.tm_year = tm2.tm_year-1900;
		time_t now;
		time(&now);  /* get current time; same as: now = time(NULL)  */

		tm1 = *localtime(&now);
		tm2 = *localtime(&now);

		//newyear.tm_hour = 0; newyear.tm_min = 0; newyear.tm_sec = 0;
		//newyear.tm_mon = 0;  
		sscanf_s(*dateFromToChar,"%2d%2d%4d%2d%2d%4d",&tm1.tm_mday,&tm1.tm_mon,&tm1.tm_year,&tm2.tm_mday,&tm2.tm_mon,&tm2.tm_year);
		tm1.tm_year = tm1.tm_year - 1900;
		tm1.tm_mon = tm1.tm_mon - 1;
		tm2.tm_year = tm2.tm_year - 1900;
		tm2.tm_mon = tm2.tm_mon - 1;
		//seconds = difftime(now, mktime(&tm1));

		double diffFromStart = difftime (now, mktime(&tm1));
		double diffToEnd = difftime (now, mktime(&tm2));
		if (diffFromStart<0 || diffToEnd>0)
			return false;
	}
	catch (int e)
	{
		return false;
	}
	
	// Return true since the initial verification has been passed.
	return true;
}

extern "C" LAPPROACHDLL_API bool decodeWorkData(char* cipherTextIn, char** out_stringChar, char* cbcDate, char* id, char* hashId, char** out_checkSum) {
	
	if (!checkIdPcLicence(cbcDate, id, hashId))
		return false;

	/**
	 * Decrypt the ciphertext and pass back the result via the
	 * out_stringChar argument.
	 */
	decrypt(cipherTextIn, out_stringChar);

	/**
	 * Merge cipherTextIn with hashId, hash the result with MD5 and
	 * pass back the checksum via the out_checkSum variable.
	 */
	char* cipherTextPlusHashId = (char*)malloc((strlen(cipherTextIn) + strlen(hashId) + 1) * sizeof *cipherTextPlusHashId);
	strcpy(cipherTextPlusHashId, cipherTextIn);
	strcat(cipherTextPlusHashId, hashId);
	funcMD5(cipherTextPlusHashId, out_checkSum);

	// Return true since the initial verification has been passed.
	return true;
}


extern "C" LAPPROACHDLL_API bool generateLayout(int L_pallet, int W_pallet, int l_box, int w_box, char* cbcDate, char* id, char* hashId, int* vecSol, int* dimension, char** out_checkSum) {
	
	if (!checkIdPcLicence(cbcDate, id, hashId))
		return false;

	// chiama princ
	princ(L_pallet, W_pallet, l_box, w_box, vecSol, dimension);

	char* dimensionStr= (char*)malloc(100 * sizeof *dimensionStr);
	sprintf(dimensionStr,"%d",*dimension);

	char* saltedId = (char*)malloc(( + strlen(cbcDate) + strlen(hashId)  + strlen(dimensionStr) + 1) * sizeof *saltedId);
	strcpy(saltedId, cbcDate);
	strcat(saltedId, hashId);
	strcat(saltedId, dimensionStr);
	funcMD5(saltedId, out_checkSum);
	free(saltedId);
	free(dimensionStr);

	return true;	
}


