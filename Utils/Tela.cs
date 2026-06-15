// utils\Tela.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace GolPro.utils
{
    public class Tela
    {
        // ── Paleta ────────────────────────────────────────────────────────────
        private readonly ConsoleColor _corFundo       = ConsoleColor.Black;
        private readonly ConsoleColor _corBorda       = ConsoleColor.DarkCyan;
        private readonly ConsoleColor _corTitulo      = ConsoleColor.Cyan;
        private readonly ConsoleColor _corTexto       = ConsoleColor.White;
        private readonly ConsoleColor _corDestaque    = ConsoleColor.Yellow;
        private readonly ConsoleColor _corSelBg       = ConsoleColor.DarkCyan;
        private readonly ConsoleColor _corSelFg       = ConsoleColor.Black;
        private readonly ConsoleColor _corMenuBorda   = ConsoleColor.DarkMagenta;
        private readonly ConsoleColor _corMensagem    = ConsoleColor.Green;
        private readonly ConsoleColor _corErro        = ConsoleColor.Red;

        private int _limiteColIni, _limiteLinIni, _limiteColFin, _limiteLinFin;

        public Tela(ConsoleColor corTela, ConsoleColor corTexto) { }

        // ── Primitivos ────────────────────────────────────────────────────────

        public void LimparTela(int colIni, int linIni, int colFin, int linFin)
        {
            Console.BackgroundColor = _corFundo;
            for (int y = linIni; y <= linFin; y++)
            {
                Console.SetCursorPosition(colIni, y);
                Console.Write(new string(' ', colFin - colIni + 1));
            }
        }

        private void DesenharCaixa(int colIni, int linIni, int colFin, int linFin, ConsoleColor corBorda)
        {
            LimparTela(colIni, linIni, colFin, linFin);
            Console.ForegroundColor = corBorda;

            for (int col = colIni + 1; col < colFin; col++)
            {
                Console.SetCursorPosition(col, linIni); Console.Write("─");
                Console.SetCursorPosition(col, linFin); Console.Write("─");
            }
            for (int lin = linIni + 1; lin < linFin; lin++)
            {
                Console.SetCursorPosition(colIni, lin); Console.Write("│");
                Console.SetCursorPosition(colFin, lin); Console.Write("│");
            }

            Console.SetCursorPosition(colIni, linIni); Console.Write("┌");
            Console.SetCursorPosition(colFin, linIni); Console.Write("┐");
            Console.SetCursorPosition(colIni, linFin); Console.Write("└");
            Console.SetCursorPosition(colFin, linFin); Console.Write("┘");

            Console.ForegroundColor = _corTexto;
        }

        private void DesenharCaixaDupla(int colIni, int linIni, int colFin, int linFin, ConsoleColor corBorda)
        {
            LimparTela(colIni, linIni, colFin, linFin);
            Console.ForegroundColor = corBorda;

            for (int col = colIni + 1; col < colFin; col++)
            {
                Console.SetCursorPosition(col, linIni); Console.Write("═");
                Console.SetCursorPosition(col, linFin); Console.Write("═");
            }
            for (int lin = linIni + 1; lin < linFin; lin++)
            {
                Console.SetCursorPosition(colIni, lin); Console.Write("║");
                Console.SetCursorPosition(colFin, lin); Console.Write("║");
            }

            Console.SetCursorPosition(colIni, linIni); Console.Write("╔");
            Console.SetCursorPosition(colFin, linIni); Console.Write("╗");
            Console.SetCursorPosition(colIni, linFin); Console.Write("╚");
            Console.SetCursorPosition(colFin, linFin); Console.Write("╝");

            Console.ForegroundColor = _corTexto;
        }

        private void DesenharSeparador(int colIni, int colFin, int linha, ConsoleColor corBorda)
        {
            Console.ForegroundColor = corBorda;
            Console.SetCursorPosition(colIni, linha); Console.Write("╠");
            for (int col = colIni + 1; col < colFin; col++)
            {
                Console.SetCursorPosition(col, linha); Console.Write("═");
            }
            Console.SetCursorPosition(colFin, linha); Console.Write("╣");
            Console.ForegroundColor = _corTexto;
        }

        private void Centralizar(int colIni, int colFin, int linha, string texto, ConsoleColor cor)
        {
            int col = colIni + ((colFin - colIni - texto.Length) / 2);
            Console.ForegroundColor = cor;
            Console.SetCursorPosition(Math.Max(colIni + 1, col), linha);
            Console.Write(texto);
            Console.ForegroundColor = _corTexto;
        }

        public void Centralizar(int colIni, int colFin, int linha, string texto)
            => Centralizar(colIni, colFin, linha, texto, _corTexto);

        public void DesenhaMoldura(int colIni, int linIni, int colFin, int linFin)
            => DesenharCaixaDupla(colIni, linIni, colFin, linFin, _corBorda);

        // ── Tela principal ────────────────────────────────────────────────────

        public void PrepararTela(string titulo, int colIni, int linIni, int colFin, int linFin)
        {
            Console.BackgroundColor = _corFundo;
            Console.ForegroundColor = _corTexto;
            Console.CursorVisible   = true;

            _limiteColIni = colIni;
            _limiteLinIni = linIni;
            _limiteColFin = colFin;
            _limiteLinFin = linFin;

            Console.Clear();

            // Moldura principal dupla
            DesenharCaixaDupla(colIni, linIni, colFin, linFin, _corBorda);

            // Título em destaque + caixa simples ao redor
            int titLen  = titulo.Length + 4;
            int titCol  = colIni + ((colFin - colIni - titLen) / 2);
            DesenharCaixa(titCol, linIni, titCol + titLen, linIni + 2, _corDestaque);

            Console.ForegroundColor = _corTitulo;
            Console.SetCursorPosition(titCol + 2, linIni + 1);
            Console.Write(titulo.ToUpper());

            // Linha divisória abaixo do título
            DesenharSeparador(colIni, colFin, linIni + 2, _corBorda);

            // Rodapé com versão
            Console.ForegroundColor = _corBorda;
            Console.SetCursorPosition(colFin - 12, linFin);
            Console.Write(" GolPro v1.0 ");

            Console.ForegroundColor = _corTexto;
        }

        // ── Menu interativo (↑↓ + Enter) ─────────────────────────────────────

        public string MostrarMenu(List<string> opcoes)
        {
            int maior     = opcoes.Max(o => o.Length);
            int largura   = maior + 6;
            int altura    = opcoes.Count + 5; // borda + hint top + items + separador + prompt + borda

            int colIni = _limiteColIni + ((_limiteColFin - _limiteColIni - largura) / 2);
            int linIni = _limiteLinIni + ((_limiteLinFin - _limiteLinIni - altura) / 2) - 1;
            int colFin = colIni + largura;
            int linFin = linIni + altura;

            DesenharCaixa(colIni, linIni, colFin, linFin, _corMenuBorda);

            // Dica de navegação no topo do menu
            Console.ForegroundColor = _corBorda;
            string hint = " ↑↓ Navegar  Enter Confirmar ";
            int hintCol = colIni + ((colFin - colIni - hint.Length) / 2);
            Console.SetCursorPosition(hintCol, linIni);
            Console.Write(hint);

            Console.CursorVisible = false;

            int selecionado = 0;
            ConsoleKey tecla;

            void DesenharOpcoes()
            {
                for (int i = 0; i < opcoes.Count; i++)
                {
                    Console.SetCursorPosition(colIni + 2, linIni + 1 + i);
                    if (i == selecionado)
                    {
                        Console.BackgroundColor = _corSelBg;
                        Console.ForegroundColor = _corSelFg;
                        Console.Write($" ► {opcoes[i].PadRight(maior + 1)}");
                    }
                    else
                    {
                        Console.BackgroundColor = _corFundo;
                        Console.ForegroundColor = _corTexto;
                        Console.Write($"   {opcoes[i].PadRight(maior + 1)}");
                    }
                    Console.BackgroundColor = _corFundo;
                    Console.ForegroundColor = _corTexto;
                }
            }

            do
            {
                DesenharOpcoes();
                tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.UpArrow)
                    selecionado = (selecionado - 1 + opcoes.Count) % opcoes.Count;
                else if (tecla == ConsoleKey.DownArrow)
                    selecionado = (selecionado + 1) % opcoes.Count;

            } while (tecla != ConsoleKey.Enter);

            Console.CursorVisible = true;

            // Extrai o número da opção, ex: "1 - Alterar" → "1"
            return opcoes[selecionado].Split(' ')[0];
        }

        // ── Mensagem de feedback ──────────────────────────────────────────────

        public void MostrarMensagem(string msg, int col, int row)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(new string(' ', 65));

            Console.ForegroundColor = _corMensagem;
            Console.SetCursorPosition(col, row);
            Console.Write($"  ✔  {msg}");

            Console.ForegroundColor = _corBorda;
            Console.Write("  [ Enter ]");

            Console.ForegroundColor = _corTexto;
            Console.ReadLine();
        }

        public void MostrarErro(string msg, int col, int row)
        {
            Console.SetCursorPosition(col, row);
            Console.Write(new string(' ', 65));

            Console.ForegroundColor = _corErro;
            Console.SetCursorPosition(col, row);
            Console.Write($"  ✖  {msg}");

            Console.ForegroundColor = _corBorda;
            Console.Write("  [ Enter ]");

            Console.ForegroundColor = _corTexto;
            Console.ReadLine();
        }
    }
}
