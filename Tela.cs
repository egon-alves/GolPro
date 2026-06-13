using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace GolPro
{
    internal class Tela
    {

        // Classe tela vai ter 2 cores, vou declarar os atributos para isso

        private ConsoleColor _corTela;
        private ConsoleColor _corTexto;

        // Estou usando um metodo Construtor
        public Tela(ConsoleColor corTela, ConsoleColor corTexto)
        {
            this._corTela = corTela;
            this._corTexto = corTexto;

        }


        public void LimparTela(int colunaInicial, int linhaInicial, int colunaFinal, int linhaFinal)
        {
            for (int x = colunaInicial; x <= colunaFinal; x++)
            {
                for (int y = linhaInicial; y <= linhaFinal; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(" ");
                }
            }
        }

        public void DesenhaMoldura(int colunaInicial, int linhaInicial, int colunaFinal, int linhaFinal)
        
        {
            this.LimparTela(colunaInicial, linhaInicial, colunaFinal, linhaFinal);
            

            // Desenhar a borda da base e do topo 

            for (int coluna = colunaInicial; coluna <= colunaFinal; coluna++)
            {
                Console.SetCursorPosition(coluna, linhaInicial); Console.Write("═");
                Console.SetCursorPosition(coluna, linhaFinal); Console.Write("═");

            }

            // Bordas Verticais (lados)

            for (int linha = linhaInicial; linha <= linhaFinal; linha++)
            {
                Console.SetCursorPosition(colunaInicial, linha); Console.Write("║");
                Console.SetCursorPosition(colunaFinal, linha); Console.Write("║");
            }

            Console.SetCursorPosition(colunaInicial, linhaInicial); Console.Write("╔");
            Console.SetCursorPosition(colunaFinal, linhaInicial); Console.Write("╗");
            Console.SetCursorPosition(colunaInicial, linhaFinal); Console.Write("╚");
            Console.SetCursorPosition(colunaFinal, linhaFinal); Console.Write("╝");


        }

        public void Centralizar(int colunaInicial, int colunaFinal, int linha, string texto)
        {

            int coluna = colunaInicial + ((colunaFinal - colunaInicial - texto.Length) / 2);
            Console.SetCursorPosition(coluna, linha);
            Console.Write(texto);
        }

        public void PrepararTela(string titulo, int colunaInicial, int linhaInicial, int colunaFinal, int linhaFinal)
        {
            Console.BackgroundColor = this._corTela;
            Console.ForegroundColor = this._corTexto;
            Console.Clear();

            this.DesenhaMoldura(colunaInicial, linhaInicial, colunaFinal, linhaFinal);
            this.Centralizar(colunaInicial, colunaFinal, linhaInicial + 1, titulo);
        }
        public string MostrarMenu(int colini, int linini, List<string> opcoes)
        {
            int colfin = colini + opcoes[0].Length + 1;
            int linfin = linini + opcoes.Count() + 2;

            this.DesenhaMoldura(colini, linini, colfin, linfin);

            for (int x = 0; x < opcoes.Count(); x++)
            {
                Console.SetCursorPosition(colini + 1, linini + 1 + x);
                Console.Write(opcoes[x]);
            }
            Console.SetCursorPosition(colini + 1, linini + 1 + opcoes.Count());
            Console.Write("Opção : ");
            return Console.ReadLine();
        }

    }
}
