/*
 ----------------------------------------------------------------------
 Classe que implementa o tabuleiro do jogo e controla a disposição das
 peças do jogo e sua validade
 */

using tabuleiro.exceptions;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        //ACESSORES PARA A MATRIZ PECAS
        public Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca Peca(Posicao posicao)
        {
            return Pecas[posicao.Linha, posicao.Coluna];
        }
        //MÉTODO VERIFICADOR SE EXISTE ALGUMA PEÇA NA POSIÇÃO DADA
        public bool ExistePeca(Posicao posicao)
        {
            ValidaPosicao(posicao);
            return Peca(posicao) != null;
        }

        //MÉTODOS QUE INSEREM E RETIRAM PEÇAS NO TABULEIRO

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
            {
                throw new TabuleiroException("Já existe uma peça nessa posição!");
            }
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.Posicao = posicao;
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if (Peca(posicao) == null)
            {
                return null;
            }
            Peca aux = Peca(posicao);
            aux.Posicao = null;
            Pecas[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }

        //VALIDAÇÃO DAS POSIÇÕES ONDE AS PEÇAS PODEM FICAR(CONTERMEANSURE TO INDEXOUTOFRANGE IN THE PRINCIPE)

        public bool PosicaoValida(Posicao posicao)
        {
            if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }

        public void ValidaPosicao(Posicao posicao)
        {
            if (!PosicaoValida(posicao))
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}
