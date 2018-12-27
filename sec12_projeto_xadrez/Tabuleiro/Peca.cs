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
        public void DecrementarQuantidadeDeMovimentos()
        {
            QuantidadeDeMovimentos--;
        }
        //MÉTODO AUXILIAR RETORNA TRUE SE A POSIÇÃO SELECIONADA FOR VAZIA(NULL) OU FOR UMA PEÇA DO ADVERSÁRIO.
        protected bool PodeMover(Posicao pos)
        {
            Peca p = Tabuleiro.Peca(pos);
            return p == null || p.Cor != Cor;
        }

        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for(int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for(int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
