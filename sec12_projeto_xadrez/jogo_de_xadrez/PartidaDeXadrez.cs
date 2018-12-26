using System.Collections.Generic;
using tabuleiro;
using tabuleiro.exceptions;
namespace jogo_de_xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tabuleiro { get; private set; }//Tabuleiro relacionado com a partida.
        public int Turno { get;private set; } // indica em que turno está a partida
        public Cor JogadorAtual { get;private set; }//Indica de quem é a vez da jogada (Peças pretas ou peças brancas
        public bool Terminada { get;private set; }//Indica se a partida está terminada ou não.
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            Terminada = false;
            ColocarPecas();

        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementarQuantidadeDeMovimentos();
            Peca capturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);
            if(capturada != null)
            {
                Capturadas.Add(capturada);
            }
        }
        //RETORNA O CONJUNTO DE PEÇAS CAPTURADAS DA COR ESPECIFICADA
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> temp = new HashSet<Peca>();
            foreach(Peca x in Capturadas)
            {
                if(x.Cor == cor)
                {
                    temp.Add(x);
                }
            }
            return temp;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> temp = new HashSet<Peca>();
            foreach (Peca x in Capturadas)
            {
                if (x.Cor == cor)
                {
                    temp.Add(x);
                }
            }
            temp.ExceptWith(PecasCapturadas(cor));
            return temp;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
            
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            if(Tabuleiro.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(JogadorAtual != Tabuleiro.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tabuleiro.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }
        public void ValidaPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tabuleiro.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }
        //COLOCA AS PEÇAS EM SUA POSIÇÃO INICIAL DA PARTIDA ( EM DESENVOLVIMENTO)
        private void ColocarPecas()
        {

            //COLOCANDO AS PEÇAS BRANCAS
            ColocarNovaPeca('c', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 2, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tabuleiro, Cor.Branca));
            //COLOCANDO AS PEÇAS PRETAS
            ColocarNovaPeca('c', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tabuleiro, Cor.Preta));

        }
    }
}
