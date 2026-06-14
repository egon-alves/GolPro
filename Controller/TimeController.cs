using System;
using System.Collections.Generic;
using GolPro.Model;
using GolPro.utils;

namespace GolPro.Controller
{
    public class TimeController
    {
        private int _column, _row, _width, _height;
        private List<string> _fields;
        private List<TimeModel> _times;
        private Tela _tela;

        public List<TimeModel> Times
        {
            get { return _times; }
            set { _times = value; }
        }

        public TimeController(int col, int row, int width, int height, Tela tela)
        {
            this._column = col;
            this._row    = row;
            this._width  = width;
            this._height = height;
            this._tela   = tela;
            this._times  = new List<TimeModel>();
            this._fields = new List<string> { "Código", "Nome", "Cidade" };
        }

        // ── Busca ────────────────────────────────────────────────────────────

        private TimeModel BuscarPorCodigo(string codigo)
        {
            foreach (TimeModel time in _times)
            {
                if (time.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                    return time;
            }
            return null;
        }

        // ── Tela ─────────────────────────────────────────────────────────────

        private void DesenharTela()
        {
            _tela.PrepararTela("Cadastro de Times", _column, _row, _column + _width, _row + _height);
        }

        private void MostrarCampos(string codigo, string nome, string cidade)
        {
            // Limpa a área dos campos antes de escrever
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(_column + 2, _row + 3 + i);
                Console.Write(new string(' ', _width - 3));
            }

            Console.SetCursorPosition(_column + 2, _row + 3);
            Console.Write($"Código  : {codigo}");

            Console.SetCursorPosition(_column + 2, _row + 5);
            Console.Write($"Nome    : {nome}");

            Console.SetCursorPosition(_column + 2, _row + 7);
            Console.Write($"Cidade  : {cidade}");
        }

        private void MostrarMensagem(string msg)
        {
            Console.SetCursorPosition(_column + 2, _row + _height - 2);
            Console.Write(new string(' ', _width - 3));
            Console.SetCursorPosition(_column + 2, _row + _height - 2);
            Console.Write(msg + " Pressione Enter...");
            Console.ReadLine();
        }

        // ── Fluxo principal ───────────────────────────────────────────────────

        public void GerenciarCadastro()
        {
            DesenharTela();
            MostrarCampos("", "", "");

            // Pede apenas o Código primeiro
            Console.SetCursorPosition(_column + 12, _row + 3);
            string codigo = (Console.ReadLine() ?? "").ToUpper().Trim();

            if (string.IsNullOrEmpty(codigo))
                return;

            TimeModel encontrado = BuscarPorCodigo(codigo);

            if (encontrado != null)
            {
                // Exibe os dados do time encontrado
                MostrarCampos(encontrado.Codigo, encontrado.Nome, encontrado.Cidade);

                // Oferece Editar ou Remover
                string opcao = _tela.MostrarMenu(new List<string>
                {
                    "1 - Editar",
                    "2 - Remover",
                    "0 - Voltar"
                });

                if (opcao == "1") Editar(encontrado);
                else if (opcao == "2") Remover(encontrado);
            }
            else
            {
                // Código não existe — preenche o código já digitado e pede o resto
                MostrarCampos(codigo, "", "");

                Console.SetCursorPosition(_column + 12, _row + 5);
                string nome = Console.ReadLine() ?? "";

                Console.SetCursorPosition(_column + 12, _row + 7);
                string cidade = Console.ReadLine() ?? "";

                Criar(codigo, nome, cidade);
            }
        }

        // ── Operações ─────────────────────────────────────────────────────────

        private void Criar(string codigo, string nome, string cidade)
        {
            _times.Add(new TimeModel(codigo, nome, cidade));
            MostrarMensagem("Time cadastrado com sucesso!");
        }

        private void Editar(TimeModel time)
        {
            DesenharTela();
            MostrarCampos(time.Codigo, time.Nome, time.Cidade);

            // Código não pode ser editado
            Console.SetCursorPosition(_column + 12, _row + 5);
            string novoNome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novoNome))
                time.Nome = novoNome;

            Console.SetCursorPosition(_column + 12, _row + 7);
            string novaCidade = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(novaCidade))
                time.Cidade = novaCidade;

            MostrarMensagem("Time atualizado com sucesso!");
        }

        private void Remover(TimeModel time)
        {
            Console.SetCursorPosition(_column + 2, _row + _height - 2);
            Console.Write($"Confirma remoção de '{time.Nome}'? (S/N): ");
            string resposta = (Console.ReadLine() ?? "").ToUpper();

            if (resposta == "S")
            {
                _times.Remove(time);
                MostrarMensagem("Time removido com sucesso!");
            }
        }
    }
}
