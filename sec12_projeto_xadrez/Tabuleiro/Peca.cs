/*
 ------------------------------------------------------------------------------
 Esta classe implementa as propriedades gerais das peças de um jogo qualquer
 ------------------------------------------------------------------------------
 */

namespace tabuleiro
{
    abstract class Peca
    {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; set; }


        public Peca() { }

        public Peca(Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            QuantidadeDeMovimentos = 0;
            Tabuleiro = tabuleiro;
        }
        //INCREMENTA A PROPRIEDADE CONTADORA DE MOVIMENTOS DA PEÇA
        public void IncrementarQuantidadeDeMovimentos()
        {
            QuantidadeDeMovimentos++;
        }
        protected bool PodeMover(Posicao pos)
        {
            Peca p = Tabuleiro.Peca(pos);
            return p == null || p.Cor != Cor;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
