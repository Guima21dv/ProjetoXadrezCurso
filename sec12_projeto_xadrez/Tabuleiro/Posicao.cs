/*
 ----------------------------------------------------------------------------------------
 Esta classe implementa a posicao para a matriz, ou seja sua posição Linha e Coluna estão
 No padrão matricial da linguagem e não do jogo geral.

 ----------------------------------------------------------------------------------------
 */
namespace tabuleiro
{
    class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao()
        {

        }
        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }
        public override string ToString()
        {
            return Linha + ", " + Coluna;
        }

        public void DefinirValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }
    }
}
