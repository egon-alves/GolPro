// utils\Tela.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace GolPro.Utils
{
    public class Tela
    {
        //
        // atributos
        //
        private ConsoleColor _corFundo;
        private ConsoleColor _corTexto;

        // paleta fixa do tema
        private static readonly ConsoleColor Fundo      = ConsoleColor.Black;
        private static readonly ConsoleColor Texto      = ConsoleColor.White;
        private static readonly ConsoleColor Borda      = ConsoleColor.White;
        private static readonly ConsoleColor Titulo     = ConsoleColor.White;
        private static readonly ConsoleColor Destaque   = ConsoleColor.White;
        private static readonly ConsoleColor MenuBorda  = ConsoleColor.White;
        private static readonly ConsoleColor SelFundo   = ConsoleColor.White;
        private static readonly ConsoleColor SelTexto   = ConsoleColor.Black;
        private static readonly ConsoleColor CorSucesso = ConsoleColor.White;
        private static readonly ConsoleColor CorErro    = ConsoleColor.White;

        // limites salvos para centralizar o menu
        private int _ci, _li, _cf, _lf;

        //
        // construtores
        //

        public Tela(ConsoleColor cf, ConsoleColor ct)
        {
            this._corFundo = cf;
            this._corTexto = ct;
        }

        public Tela() { }

        //
        // ── Métodos da interface Biblioteca (obrigatórios) ────────────────────
        //

        // PrepararTela: limpa, monta moldura, centraliza o título
        public void PrepararTela(string titulo, int ci, int li, int cf, int lf)
        {
            Console.BackgroundColor = Fundo;
            Console.ForegroundColor = Texto;
            Console.CursorVisible   = true;

            _ci = ci; _li = li; _cf = cf; _lf = lf;

            Console.Clear();

            MontarMoldura(ci, li, cf, lf);

            // separador abaixo do título
            Console.ForegroundColor = Borda;
            Console.SetCursorPosition(ci, li + 2); Console.Write("╠");
            for (int c = ci + 1; c < cf; c++) { Console.SetCursorPosition(c, li + 2); Console.Write("═"); }
            Console.SetCursorPosition(cf, li + 2); Console.Write("╣");

            // título sobre o separador
            Console.ForegroundColor = Titulo;
            Centralizar(ci, cf, li + 1, titulo.ToUpper());

            // versão no rodapé
            Console.ForegroundColor = Borda;
            Console.SetCursorPosition(cf - 12, lf);
            Console.Write(" GolPro v1.0 ");

            Console.ForegroundColor = Texto;
        }

        // Centralizar: escreve texto centralizado numa linha
        public void Centralizar(int ci, int cf, int linha, string texto)
        {
            int col = ci + ((cf - ci - texto.Length) / 2);
            Console.SetCursorPosition(Math.Max(ci + 1, col), linha);
            Console.Write(texto);
        }

        // Perguntar: limpa a linha, escreve a pergunta e lê a resposta
        public string Perguntar(string pergunta, int linha, int ci, int cf)
        {
            LimparArea(ci, linha, cf, linha);
            Console.SetCursorPosition(ci, linha);
            Console.Write(pergunta);
            return Console.ReadLine() ?? "";
        }

        // LimparArea: apaga uma área retangular da tela
        public void LimparArea(int ci, int li, int cf, int lf)
        {
            Console.BackgroundColor = Fundo;
            for (int y = li; y <= lf; y++)
            {
                Console.SetCursorPosition(ci, y);
                Console.Write(new string(' ', cf - ci + 1));
            }
        }

        // MontarMoldura: desenha moldura dupla
        public void MontarMoldura(int ci, int li, int cf, int lf)
        {
            LimparArea(ci, li, cf, lf);
            Console.ForegroundColor = Borda;

            for (int c = ci + 1; c < cf; c++)
            {
                Console.SetCursorPosition(c, li); Console.Write('═');
                Console.SetCursorPosition(c, lf); Console.Write('═');
            }
            for (int l = li + 1; l < lf; l++)
            {
                Console.SetCursorPosition(ci, l); Console.Write('║');
                Console.SetCursorPosition(cf, l); Console.Write('║');
            }

            Console.SetCursorPosition(ci, li); Console.Write('╔');
            Console.SetCursorPosition(cf, li); Console.Write('╗');
            Console.SetCursorPosition(ci, lf); Console.Write('╚');
            Console.SetCursorPosition(cf, lf); Console.Write('╝');

            Console.ForegroundColor = Texto;
        }

        // MostrarMenu (assinatura Biblioteca — com posição explícita)
        public string MostrarMenu(int colIni, int linIni, List<string> opcoes)
        {
            int maior  = opcoes.Max(o => o.Length);
            int colFin = colIni + maior + 5;
            int linFin = linIni + opcoes.Count + 2;
            return MenuInterativo(colIni, linIni, colFin, linFin, opcoes);
        }

        // MostrarMenu (sobrecarga sem posição — calcula o centro)
        public string MostrarMenu(List<string> opcoes)
        {
            int maior   = opcoes.Max(o => o.Length);
            int largura = maior + 5;
            int altura  = opcoes.Count + 2;

            int colIni = _ci + ((_cf - _ci - largura) / 2);
            int linIni = _li + ((_lf - _li - altura) / 2);
            int colFin = colIni + largura;
            int linFin = linIni + altura;

            return MenuInterativo(colIni, linIni, colFin, linFin, opcoes);
        }

        //
        // ── Extensões do GolPro ───────────────────────────────────────────────
        //

        // MostrarMensagem: sucesso em verde
        public void MostrarMensagem(string msg, int col, int row)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(new string(' ', 65));
            Console.ForegroundColor = CorSucesso;
            Console.SetCursorPosition(col, row);
            Console.Write($"  ✔  {msg}  [ Enter ]");
            Console.ForegroundColor = Texto;
            Console.ReadLine();
        }

        // MostrarErro: erro em vermelho
        public void MostrarErro(string msg, int col, int row)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(new string(' ', 65));
            Console.ForegroundColor = CorErro;
            Console.SetCursorPosition(col, row);
            Console.Write($"  ✖  {msg}  [ Enter ]");
            Console.ForegroundColor = Texto;
            Console.ReadLine();
        }

        // MostrarErroInLine: exibe o erro sem pausar a tela (ideal para loops de validação)
        public void MostrarErroInLine(string msg, int col, int row)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(new string(' ', 65));
            if (!string.IsNullOrEmpty(msg)) {
                Console.ForegroundColor = CorErro;
                Console.SetCursorPosition(col, row);
                Console.Write($"  ✖  {msg}");
                Console.ForegroundColor = Texto;
            }
        }

        //
        // ── Métodos privados ──────────────────────────────────────────────────
        //

        private string MenuInterativo(int colIni, int linIni, int colFin, int linFin, List<string> opcoes)
        {
            int maior = opcoes.Max(o => o.Length);

            // caixa do menu
            LimparArea(colIni, linIni, colFin, linFin);
            Console.ForegroundColor = MenuBorda;

            for (int c = colIni + 1; c < colFin; c++)
            {
                Console.SetCursorPosition(c, linIni); Console.Write('─');
                Console.SetCursorPosition(c, linFin); Console.Write('─');
            }
            for (int l = linIni + 1; l < linFin; l++)
            {
                Console.SetCursorPosition(colIni, l); Console.Write('│');
                Console.SetCursorPosition(colFin, l); Console.Write('│');
            }
            Console.SetCursorPosition(colIni, linIni); Console.Write('┌');
            Console.SetCursorPosition(colFin, linIni); Console.Write('┐');
            Console.SetCursorPosition(colIni, linFin); Console.Write('└');
            Console.SetCursorPosition(colFin, linFin); Console.Write('┘');

            // hint na borda superior
            string hint2 = "↑↓ 0-9 Enter";
            Console.ForegroundColor = MenuBorda;
            Console.SetCursorPosition(colFin - hint2.Length - 1, linIni);
            Console.Write(hint2);

            Console.CursorVisible = false;
            int sel = 0;
            bool confirmar = false;

            void Desenhar()
            {
                for (int i = 0; i < opcoes.Count; i++)
                {
                    Console.SetCursorPosition(colIni + 1, linIni + 1 + i);
                    if (i == sel)
                    {
                        Console.BackgroundColor = SelFundo;
                        Console.ForegroundColor = SelTexto;
                        Console.Write($" ► {opcoes[i].PadRight(maior + 1)}");
                    }
                    else
                    {
                        Console.BackgroundColor = Fundo;
                        Console.ForegroundColor = Texto;
                        Console.Write($"   {opcoes[i].PadRight(maior + 1)}");
                    }
                    Console.BackgroundColor = Fundo;
                    Console.ForegroundColor = Texto;
                }
            }

            do
            {
                Desenhar();
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow)
                    sel = (sel - 1 + opcoes.Count) % opcoes.Count;
                else if (key.Key == ConsoleKey.DownArrow)
                    sel = (sel + 1) % opcoes.Count;
                else if (key.Key == ConsoleKey.Enter)
                    confirmar = true;
                else if (char.IsDigit(key.KeyChar))
                {
                    // procura a opção cujo prefixo bate com o dígito digitado
                    string digito = key.KeyChar.ToString();
                    for (int i = 0; i < opcoes.Count; i++)
                    {
                        if (opcoes[i].Split(' ')[0] == digito)
                        {
                            sel = i;
                            confirmar = true;
                            break;
                        }
                    }
                }
            } while (!confirmar);

            Console.CursorVisible = true;
            return opcoes[sel].Split(' ')[0];
        }
    }
}
