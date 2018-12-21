using tabuleiro;

namespace tabuleiro
{
    class Peca
    {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }


        public Peca()
        {

        }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = posicao;
            Cor = cor;
            QuantidadeDeMovimentos = 0;
            Tabuleiro = tabuleiro;
        }
    }
}
