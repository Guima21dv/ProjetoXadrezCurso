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
                PosicaoXadrez pos = new PosicaoXadrez('a', 1);
                Console.WriteLine(pos);
                Console.WriteLine(pos.ToPosicao());
                PosicaoXadrez pos2 = new PosicaoXadrez('c', 7);
                Console.WriteLine(pos2);
                Console.WriteLine(pos2.ToPosicao());
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
