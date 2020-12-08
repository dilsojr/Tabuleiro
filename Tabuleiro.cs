using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tabuleiro
{
    public class Tabuleiro
    {
        
        private IList<char> listaLetrasSorteadas { get; set; }
        private char[,] arrayTabuleiro;
        
        private IList<char> letras = new List<char>
        {
            'A', 'B', 'C', 'D','E', 'F','G', 'H','I', 'J','K', 'L','M', 'N','O', 'P','Q', 'R','S', 'T', 'U', 'V', 'X', 'Y', 'Z'
        };
        private const char VAZIO = '\0';
        private  int jogadas = 0;
        public Tabuleiro()
        {
            listaLetrasSorteadas = new List<char>();
            arrayTabuleiro = new char[3, 3];

            Embaralhar();
        }

        private void Embaralhar()
        {
            SortearLetras();
            PreencherArrayTabuleiro();
            OrdenarListaLetrasSorteadas();
        }

        public void MovimentarLetra(char letra)
        {
            Tuple<int, int> coordenadasLetraMovimentada = arrayTabuleiro.ObterCoordenada(letra);

            if (coordenadasLetraMovimentada.Item1 == -1 && coordenadasLetraMovimentada.Item2 == -1)
                return;
            
            for (int indexLinha = 0; indexLinha < arrayTabuleiro.GetLength(0); indexLinha++)
            {
                int indexColunaAnalise = coordenadasLetraMovimentada.Item2 - 1;

                if (indexColunaAnalise > -1)
                {
                    if (indexLinha == coordenadasLetraMovimentada.Item1)
                    {
                        if (arrayTabuleiro[indexLinha, indexColunaAnalise] == VAZIO)
                        {
                            arrayTabuleiro[indexLinha, indexColunaAnalise] = arrayTabuleiro[coordenadasLetraMovimentada.Item1, coordenadasLetraMovimentada.Item2];
                            arrayTabuleiro[coordenadasLetraMovimentada.Item1, coordenadasLetraMovimentada.Item2] = VAZIO;
                            QuantidadeDeJogadas(letra);
                        }
                    }
                }

                indexColunaAnalise = coordenadasLetraMovimentada.Item2 + 1;

                if (indexColunaAnalise < arrayTabuleiro.GetLength(1))
                {
                    if (indexLinha == coordenadasLetraMovimentada.Item1)
                    {
                        if (arrayTabuleiro[indexLinha, indexColunaAnalise] == VAZIO)
                        {
                            arrayTabuleiro[indexLinha, indexColunaAnalise] = arrayTabuleiro[coordenadasLetraMovimentada.Item1, coordenadasLetraMovimentada.Item2];
                            arrayTabuleiro[coordenadasLetraMovimentada.Item1, coordenadasLetraMovimentada.Item2] = VAZIO;
                            QuantidadeDeJogadas(letra);
                        }
                    }
                }

                for (int indexColuna = 0; indexColuna < arrayTabuleiro.GetLength(1); indexColuna++)
                {
                    int indexLinhaAnalise = coordenadasLetraMovimentada.Item1 - 1;

                    if (indexLinhaAnalise > -1)
                    {
                        if (indexColuna == coordenadasLetraMovimentada.Item2)
                        {
                            if (arrayTabuleiro[indexLinhaAnalise, indexColuna] == VAZIO)
                            {
                                if (arrayTabuleiro[indexLinha, indexColuna] != VAZIO)
                                {
                                    arrayTabuleiro[indexLinhaAnalise, indexColuna] = arrayTabuleiro[coordenadasLetraMovimentada.Item1, coordenadasLetraMovimentada.Item2];
                                    arrayTabuleiro[coordenadasLetraMovimentada.Item1, coordenadasLetraMovimentada.Item2] = VAZIO;
                                    QuantidadeDeJogadas(letra);
                                }
                            }
                        }
                    }

                    indexLinhaAnalise = coordenadasLetraMovimentada.Item1 + 1;

                    if (indexLinhaAnalise < arrayTabuleiro.GetLength(0))
                    {
                        if (indexColuna == coordenadasLetraMovimentada.Item2)
                        {
                            if (arrayTabuleiro[indexLinhaAnalise, indexColuna] == VAZIO)
                            {

                                arrayTabuleiro[indexLinhaAnalise, indexColuna] = arrayTabuleiro[coordenadasLetraMovimentada.Item1, coordenadasLetraMovimentada.Item2];
                                arrayTabuleiro[coordenadasLetraMovimentada.Item1, coordenadasLetraMovimentada.Item2] = VAZIO;
                                QuantidadeDeJogadas(letra);

                            }
                        }
                    }
                }
                
                
            }
        }


        public void MostrarTabuleiro()
        {
           Console.Clear();
            for (int indexLinha = 0; indexLinha < arrayTabuleiro.GetLength(0); indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < arrayTabuleiro.GetLength(1); indexColuna++)
                {
                    Console.Write(arrayTabuleiro[indexLinha, indexColuna]);
                }
                Console.WriteLine();
            }
        }

        public bool GanhouJogo()
        {
            int index = 0;
            for (int indexLinha = 0; indexLinha < arrayTabuleiro.GetLength(0); indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < arrayTabuleiro.GetLength(1); indexColuna++)
                {
                    if (arrayTabuleiro[indexLinha, indexColuna] != listaLetrasSorteadas[index])
                        return false;
                }
            }

            return true;
        }

        public int MostrarQuantidadeJogadas()
        {
            return jogadas;

        }

        private void QuantidadeDeJogadas(char letra)
        {
            if (listaLetrasSorteadas.Contains(letra))
            {
                jogadas++;
            }
        }

        private void SortearLetras()
        {
            Random random = new Random();

            int linhas = arrayTabuleiro.GetLength(0);
            int colunas = arrayTabuleiro.GetLength(1);

            int quantidadeLetrasSorteio = linhas * colunas - 1;

            for (int index = 0; index < quantidadeLetrasSorteio; index++)
            {
                int indexLetraSorteada = random.Next(0, letras.Count - 1);

                while (listaLetrasSorteadas.Contains(letras[indexLetraSorteada]))
                {
                    indexLetraSorteada = random.Next(0, letras.Count - 1);
                }

                listaLetrasSorteadas.Add(letras[indexLetraSorteada]);
            }

            listaLetrasSorteadas.Add(VAZIO);
        }

        private void PreencherArrayTabuleiro()
        {
            int indexListaLetras = 0;
            for (int indexLinha = 0; indexLinha < arrayTabuleiro.GetLength(0); indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < arrayTabuleiro.GetLength(1); indexColuna++)
                {
                    arrayTabuleiro[indexLinha, indexColuna] = listaLetrasSorteadas[indexListaLetras];
                    indexListaLetras++;
                }
            }
        }

        private void OrdenarListaLetrasSorteadas()
        {
            listaLetrasSorteadas = listaLetrasSorteadas.OrderBy(letra => (int)letra).ToList();
            listaLetrasSorteadas.RemoveAt(0);
            listaLetrasSorteadas[listaLetrasSorteadas.Count - 1] = VAZIO;
        }

    }

    public static class ArrayExtensao
    {
        public static IList<T> ToList<T>(this T[,] array)
        {
            List<T> listaRetorno = new List<T>();

            for (int indexLinha = 0; indexLinha < array.GetLength(0); indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < array.GetLength(1); indexColuna++)
                {

                    listaRetorno.Add(array[indexLinha, indexColuna]);

                }
            }

            return listaRetorno;
        }

        public static Tuple<int, int> ObterCoordenada<T>(this T[,] array, T letra)
        {
            for (int indexLinha = 0; indexLinha < array.GetLength(0); indexLinha++)
            {
                for (int indexColuna = 0; indexColuna < array.GetLength(1); indexColuna++)
                {
                    if (array[indexLinha, indexColuna].Equals(letra))
                    {
                        return Tuple.Create(indexLinha, indexColuna);
                    }
                }
            }

            return Tuple.Create(-1, -1);
        }



    }
}
