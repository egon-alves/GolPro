using System;
using System.Collections.Generic;
using GolPro.Model;
using GolPro.utils;

namespace GolPro.Controller
{
    public class JogadorController
    {
        private int _column, _row, _width, _height;
        private List<string> _fields;
        private List<JogadorModel> _jogadores;
        private Tela _tela;
        private Data _data;
        private JogadorModel _current;

        public List<JogadorModel> Jogadores
        {
            get { return _jogadores; }
            set { _jogadores = value; }
        }

        public JogadorController(int col, int row, int width, int height, Tela tela)
        {
            this._column = col;
            this._row = row;
            this._width = width;
            this._height = height;
            this._tela = tela;
            this._fields = new List<string> { "Matrícula", "Nome", "Posição", "Cód. Time" };
            this._data = new Data("Utils/Data/jogadores.txt");

            // Carrega do arquivo; se não existir, retorna lista vazia
            this._jogadores = this._data.CarregarJogadores();
        }

        // ── Busca (privado) ───────────────────────────────────────────────────

        private JogadorModel FindByCode(string matricula)
        {
            foreach (JogadorModel jogador in _jogadores)
            {
                if (jogador.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase))
                    return jogador;
            }
            return null;
        }

        // ── Tela ─────────────────────────────────────────────────────────────

        public void ShowForm()
        {
            _tela.PrepararTela("Cadastro de Jogadores", _column, _row, _column + _width, _row + _height);

            Console.SetCursorPosition(_column + 2, _row + 3);
            Console.Write("Matrícula      : ");

            Console.SetCursorPosition(_column + 2, _row + 5);
            Console.Write("Nome           : ");

            Console.SetCursorPosition(_column + 2, _row + 7);
            Console.Write("  Posicoes: Goleiro / Zagueiro / Lateral / Meia / Atacante");

            Console.SetCursorPosition(_column + 2, _row + 8);
            Console.Write("Posicao        : ");

            Console.SetCursorPosition(_column + 2, _row + 10);
            Console.Write("Codigo do Time : ");
        }

        public void EnterData(string which)
        {
            if (which == "PK")
            {
                Console.SetCursorPosition(_column + 19, _row + 3);
                string matricula = (Console.ReadLine() ?? "").ToUpper().Trim();
                _current = new JogadorModel();
                _current.Matricula = matricula;
            }
            else if (which == "DT")

            {

                string[] posicoesValidas = { "Goleiro", "Zagueiro", "Lateral", "Meia", "Atacante" };
                Console.SetCursorPosition(_column + 19, _row + 5);
                _current.Nome = Console.ReadLine() ?? "";

                Console.SetCursorPosition(_column + 19, _row + 8);
                _current.Posicao = Console.ReadLine() ?? "";

                Console.SetCursorPosition(_column + 19, _row + 10);
                _current.CodigoTime = (Console.ReadLine() ?? "").ToUpper().Trim();
            }
        }

        public void ShowData()
        {
            if (_current == null) return;

            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(_column + 19, _row + 3 + i);
                Console.Write(new string(' ', _width - 21));
            }

            Console.SetCursorPosition(_column + 19, _row + 3);
            Console.Write(_current.Matricula);

            Console.SetCursorPosition(_column + 19, _row + 5);
            Console.Write(_current.Nome);

            Console.SetCursorPosition(_column + 19, _row + 8);
            Console.Write(_current.Posicao);

            Console.SetCursorPosition(_column + 19, _row + 10);
            Console.Write(_current.CodigoTime);
        }

        // ── CRUD ──────────────────────────────────────────────────────────────

        public void CRUD()
        {
            ShowForm();
            EnterData("PK");

            JogadorModel encontrado = FindByCode(_current.Matricula);

            if (encontrado != null)
            {
                _current = encontrado;
                ShowData();

                string opcao = _tela.MostrarMenu(new List<string>
                {
                    "1 - Alterar",
                    "2 - Excluir",
                    "0 - Voltar"
                });

                if (opcao == "1")
                {
                    ShowForm();
                    ShowData();
                    EnterData("DT");
                    encontrado.Nome = _current.Nome;
                    encontrado.Posicao = _current.Posicao;
                    encontrado.CodigoTime = _current.CodigoTime;
                    _data.SalvarJogador(_jogadores);
                    _tela.MostrarMensagem("Jogador alterado com sucesso!", _column + 2, _row + _height - 2);
                }
                else if (opcao == "2")
                {
                    Console.SetCursorPosition(_column + 2, _row + _height - 2);
                    Console.Write($"Confirma exclusão de '{encontrado.Nome}'? (S/N): ");
                    string resp = (Console.ReadLine() ?? "").ToUpper();
                    if (resp == "S")
                    {
                        _jogadores.Remove(encontrado);
                        _data.SalvarJogador(_jogadores);
                        _tela.MostrarMensagem("Jogador excluído com sucesso!", _column + 2, _row + _height - 2);
                    }
                }
            }
            else
            {
                string opcao = _tela.MostrarMenu(new List<string>
                {
                    "1 - Incluir",
                    "0 - Voltar"
                });

                if (opcao == "1")
                {
                    ShowForm();
                    Console.SetCursorPosition(_column + 19, _row + 3);
                    Console.Write(_current.Matricula);

                    EnterData("DT");
                    _jogadores.Add(new JogadorModel(
                        _current.Matricula,
                        _current.Nome,
                        _current.Posicao,
                        _current.CodigoTime,
                        0  // numeroCamisa padrão — pode ser lido depois
                    ));

                    _data.SalvarJogador(_jogadores);
                    _tela.MostrarMensagem("Jogador incluído com sucesso!", _column + 2, _row + _height - 2);
                }
            }
        }
    }
}
