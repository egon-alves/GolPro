// utils\Tela.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace GolPro.utils
{
    public class Tela
    {
        private ConsoleColor _corTela;
        private ConsoleColor _corTexto;

        // Declaração dos limites da tela principal
        private int _limiteColIni;
        private int _limiteLinIni;
        private int _limiteColFin;
        private int _limiteLinFin;

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

            for (int coluna = colunaInicial; coluna <= colunaFinal; coluna++)
            {
                Console.SetCursorPosition(coluna, linhaInicial); Console.Write("═");
                Console.SetCursorPosition(coluna, linhaFinal); Console.Write("═");
            }

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
            
            // Salva os limites para cálculo do menu
            this._limiteColIni = colunaInicial;
            this._limiteLinIni = linhaInicial;
            this._limiteColFin = colunaFinal;
            this._limiteLinFin = linhaFinal;
            
            Console.Clear();

            this.DesenhaMoldura(colunaInicial, linhaInicial, colunaFinal, linhaFinal);
            this.Centralizar(colunaInicial, colunaFinal, linhaInicial + 1, titulo);
        }

        public string MostrarMenu(List<string> opcoes)
        {
            int maiorComprimento = opcoes.Max(o => o.Length);
            int larguraMenu = maiorComprimento + 2;
            int alturaMenu = opcoes.Count + 2;

            // Calcula o centro baseado na moldura principal
            int colini = this._limiteColIni + ((this._limiteColFin - this._limiteColIni - larguraMenu) / 2);
            int linini = this._limiteLinIni + ((this._limiteLinFin - this._limiteLinIni - alturaMenu) / 2) -4;

            int colfin = colini + larguraMenu;
            int linfin = linini + alturaMenu;

            this.DesenhaMoldura(colini, linini, colfin, linfin);

            for (int x = 0; x < opcoes.Count; x++)
            {
                Console.SetCursorPosition(colini + 1, linini + 1 + x);
                Console.Write(opcoes[x]);
            }
            
            Console.SetCursorPosition(colini + 1, linini + 1 + opcoes.Count);
            Console.Write("Opção : ");
            return Console.ReadLine();
        }
    }
        
}