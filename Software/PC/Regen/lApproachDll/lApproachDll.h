extern "C" {
	//#ifdef LAPPROACHDLL_EXPORTS
	//#define LAPPROACHDLL_API __declspec(dllexport)
	//#else
	//#define LAPPROACHDLL_API __declspec(dllimport)
	//#endif

	#define LAPPROACHDLL_API __declspec(dllexport)
	//qua
	typedef int index;
	typedef int point;
	typedef short qtde;
	typedef short flag;

	LAPPROACHDLL_API point fechaEmZ( point p );

	LAPPROACHDLL_API index q2i( point q0, point q1, point q2, point q3 );

	LAPPROACHDLL_API qtde R_lim_sup( point x, point y );
	LAPPROACHDLL_API qtde L_lim_sup( point *q );
	LAPPROACHDLL_API qtde R_lim_inf( point x, point y );
	LAPPROACHDLL_API qtde L_lim_inf( point *q );

	LAPPROACHDLL_API void normalizar( point *q );

	LAPPROACHDLL_API void positionPatternB1( point *i, point *q, point *q1, point *q2 );
	LAPPROACHDLL_API void positionPatternB2( point *i, point *q, point *q1, point *q2 );
	LAPPROACHDLL_API void positionPatternB3( point *i, point *q, point *q1, point *q2 );
	LAPPROACHDLL_API void positionPatternB4( point *i, point *q, point *q1, point *q2 );
	LAPPROACHDLL_API void positionPatternB5( point *i, point *q, point *q1, point *q2 );
	LAPPROACHDLL_API void positionPatternB6( point *i, point *q, point *q1, point *q2 );
	LAPPROACHDLL_API void positionPatternB7( point *i, point *q, point *q1, point *q2 );

	LAPPROACHDLL_API void Draw( index L, point *q, point ***solInd );

	LAPPROACHDLL_API void divide( point *i, point *q, point *q1, point *q2, void (*func)( point*, point*, point*, point* ) );
	LAPPROACHDLL_API void divideL( index L, point *q, point *lim, flag B, void (*func)( point*, point*, point*, point* ) );
	LAPPROACHDLL_API void divideR( index L, point *q, flag indicador, void (*func)( point*, point*, point*, point* ) );

	LAPPROACHDLL_API void Solve( index L, point *q );
	LAPPROACHDLL_API void L_Algorithm( point *q, point ***solInd );

	LAPPROACHDLL_API void arrange( qtde id );

	LAPPROACHDLL_API void normaliza( point *q );

	LAPPROACHDLL_API void translateX( qtde id, point deltaX );
	LAPPROACHDLL_API void translateY( qtde id, point deltaY );

	LAPPROACHDLL_API void MakeFile( index L, point *q, point ***solInd );

	LAPPROACHDLL_API void P1( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY );
	LAPPROACHDLL_API void P2( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY );
	LAPPROACHDLL_API void P3( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY );
	LAPPROACHDLL_API void P4( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY );
	LAPPROACHDLL_API void P5( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY );
	LAPPROACHDLL_API void P6( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY );
	LAPPROACHDLL_API void P7( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY );
	LAPPROACHDLL_API void P8( qtde inicio, qtde fim, index L, point *q, point deltaX, point deltaY );

	LAPPROACHDLL_API void DrawB1( index L, point *q );
	LAPPROACHDLL_API void DrawB2( index L, point *q );
	LAPPROACHDLL_API void DrawB3( index L, point *q );
	LAPPROACHDLL_API void DrawB4( index L, point *q );
	LAPPROACHDLL_API void DrawB5( index L, point *q );
	LAPPROACHDLL_API void DrawB6( index L, point *q );
	LAPPROACHDLL_API void DrawB7( index L, point *q );

	LAPPROACHDLL_API void DrawRetangulos( point x, point y );
	LAPPROACHDLL_API void DrawR( index L, point *q );
	



}