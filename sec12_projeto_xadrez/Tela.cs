using System;
using tabuleiro;

namespace sec12_projeto_xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro Tabuleiro)
        {
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (Tabuleiro.Peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(Tabuleiro.Peca(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}