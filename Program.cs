using System;

namespace Tabuleiro
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro();
            do
            {
                
                tab.MostrarTabuleiro();
                Console.WriteLine("Jogadas :"+ tab.MostrarQuantidadeJogadas());
                Console.WriteLine("Escolha a letra: ");
                char letra = Convert.ToChar(Console.ReadLine().ToUpper());
                tab.MovimentarLetra(letra);
               
                
                
            } while (!tab.GanhouJogo());

            Console.WriteLine("Ganhou o jogo");

            Console.ReadKey();
        }
    }
}
