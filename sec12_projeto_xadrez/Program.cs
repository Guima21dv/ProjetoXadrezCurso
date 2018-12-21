using System;
using tabuleiro;
using jogo_de_xadrez;
using tabuleiro.exceptions;


namespace sec12_projeto_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            try {
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(3, 5));
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(0, 2));

                Tela.ImprimirTabuleiro(tab);
        }catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
