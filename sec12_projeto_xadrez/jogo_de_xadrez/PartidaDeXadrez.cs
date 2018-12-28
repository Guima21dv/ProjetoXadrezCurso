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
        public bool Xeque { get;private set; }
        public Peca VulneravelEnPassant { get; private set; }

        public PartidaDeXadrez()
        {
            Tabuleiro = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            VulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            Terminada = false;
            Xeque = false;
            ColocarPecas();


        }
        //MÉTODO PARA EXECUTAR MOVIMENTOS DE PEÇAS NO TABULEIRO
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tabuleiro.RetirarPeca(origem);
            p.IncrementarQuantidadeDeMovimentos();
            Peca capturada = Tabuleiro.RetirarPeca(destino);
            Tabuleiro.ColocarPeca(p, destino);
            if(capturada != null)
            {
                Capturadas.Add(capturada);
            }
            //#jogadaespecial ROQUE PEQUENO
            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(origemDaTorre);
                T.IncrementarQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(T, destinoDaTorre);
            }

            //#jogadaespecial ROQUE GRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(origemDaTorre);
                T.IncrementarQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(T, destinoDaTorre);
            }
            //#jogadaespecial EN PASSANT
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && capturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    capturada = Tabuleiro.RetirarPeca(posP);
                    Capturadas.Add(capturada);
                }
            }

            return capturada;
        }
        //MÉTODO QUE AO SER CHAMADO DESFAZ A JOGADA RETORNANDO AS PEÇAS PARA SEUS LOCAIS ANTES DA JOGADA.
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tabuleiro.RetirarPeca(destino);
            p.DecrementarQuantidadeDeMovimentos();
            if(pecaCapturada != null)
            {
                Tabuleiro.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tabuleiro.ColocarPeca(p, origem);

            //#jogadaespecial ROQUE PEQUENO
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tabuleiro.RetirarPeca(destinoDaTorre);
                T.DecrementarQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(T, origemDaTorre);
            }

            //#jogadaespecial ROQUE GRANDE
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tabuleiro.RetirarPeca(destinoDaTorre);
                T.DecrementarQuantidadeDeMovimentos();
                Tabuleiro.ColocarPeca(T, origemDaTorre);
            }
            //#jogadaespecial EN PASSANT
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tabuleiro.RetirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tabuleiro.ColocarPeca(peao, posP);
                }
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
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    temp.Add(x);
                }
            }
            temp.ExceptWith(PecasCapturadas(cor));
            return temp;
        }

        private Cor Adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            return Cor.Branca;
        }

        private Peca ERei(Cor cor)
        {
            foreach(Peca x in PecasEmJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = ERei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não existe rei da cor " + cor + " no tabuleiro.");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna]){
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for(int i = 0; i < Tabuleiro.Linhas; i++)
                {
                    for(int j = 0; j < Tabuleiro.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool xeq = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!xeq)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            
            Peca cap = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, cap);
                throw new TabuleiroException("Não é permitido se colocar em xeque.");
            }
            Peca p = Tabuleiro.Peca(destino);
            //#jogadaespecial PROMOCAO
            if(p is Peao)
            {
                if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tabuleiro.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(Tabuleiro, p.Cor);
                    Tabuleiro.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
            }
            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;

            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
            //#jogadaespecial EN PASSANT
            if(p is Peao && p.QuantidadeDeMovimentos == 1 && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }
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
            if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
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

            ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));


            ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
            ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
        }
    }
}
