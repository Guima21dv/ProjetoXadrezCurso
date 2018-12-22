/*
 -------------------------------------------------------------------------------
 Esta classe implementa a posição das peças conforme padrão utilizado para loca-
 lização das peças no tabuleiro( Forma visual de localização e não matricial da
 linguagem)
 -------------------------------------------------------------------------------
 */

using tabuleiro;
namespace jogo_de_xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
