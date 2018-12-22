using tabuleiro;

namespace sec12_projeto_xadrez.jogo_de_xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(cor, tabuleiro)
        {

        }

        public override string ToString()
        {
            return "P";
        }

        public override bool[,] MovimentosPossiveis()
        {
            throw new System.NotImplementedException();
        }
    }
}
