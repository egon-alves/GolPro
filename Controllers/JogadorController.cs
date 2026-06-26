using System;
using System.Collections.Generic;
using System.Linq;
using GolPro.Models;
using GolPro.Utils;

namespace GolPro.Controller
{


    public class JogadorController
    {
        private int _column, _row, _width, _height;
        private List<JogadorModel> _jogadores;
        private List<TimeModel> _times;
        private Tela _tela;
        private Data _data;
        private JogadorModel _current;

        public List<JogadorModel> Jogadores
        {
            get { return _jogadores; }
            set { _jogadores = value; }
        }

        public JogadorController(int col, int row, int width, int height, Tela tela, List<TimeModel> times)
        {
            this._column = col;
            this._row = row;
            this._width = width;
            this._height = height;
            this._tela = tela;
            this._times = times;
            this._data = new Data("Utils/Data/jogadores.txt");

            this._jogadores = this._data.CarregarJogadores();

        }

        // ── Busca (privado) ───────────────────────────────────────────────────

        private bool TimeExiste(string codigoTime)
        {
            foreach (TimeModel time in _times)
            {
                if (time.Codigo.Equals(codigoTime, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }

        private JogadorModel FindByCode(string matricula)
        {
            foreach (JogadorModel jogador in _jogadores)
            {
                if (jogador.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase))
                    return jogador;
            }
            return null;
        }

        public JogadorModel BuscarPorMatricula(string matricula)
        {
            return FindByCode(matricula);
        }

        public void Salvar()
        {
            _data.SalvarJogador(_jogadores);
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

                if (string.IsNullOrEmpty(matricula))
                {
                    int maxMatricula = 0;
                    foreach(var j in _jogadores) {
                        if (int.TryParse(j.Matricula, out int mat)) {
                            if (mat > maxMatricula) maxMatricula = mat;
                        }
                    }
                    matricula = (maxMatricula + 1).ToString();
                    Console.SetCursorPosition(_column + 19, _row + 3);
                    Console.Write(matricula);
                }

                _current = new JogadorModel();
                _current.Matricula = matricula;
            }
            else if (which == "DT")

            {

                while (true)
                {
                    Console.SetCursorPosition(_column + 19, _row + 5);
                    Console.Write(new string(' ', _width - 21));
                    Console.SetCursorPosition(_column + 19, _row + 5);
                    string nome = (Console.ReadLine() ?? "").Trim();
                    if (string.IsNullOrEmpty(nome) || !nome.Replace(" ", "").All(char.IsLetter))
                    {
                        _tela.MostrarErroInLine("Nome inválido! Use apenas letras.", _column + 2, _row + _height - 2);
                        continue;
                    }
                    _current.Nome = nome;
                    _tela.MostrarErroInLine("", _column + 2, _row + _height - 2);
                    break;
                }

                string[] posicoesValidas = { "Goleiro", "Zagueiro", "Lateral", "Volante", "Meia", "Atacante" };
                while (true)
                {
                    Console.SetCursorPosition(_column + 19, _row + 8);
                    Console.Write(new string(' ', _width - 21));
                    Console.SetCursorPosition(_column + 19, _row + 8);
                    string posicao = (Console.ReadLine() ?? "").Trim();

                    var posEncontrada = posicoesValidas.FirstOrDefault(p => p.Equals(posicao, StringComparison.OrdinalIgnoreCase));
                    if (posEncontrada == null)
                    {
                        _tela.MostrarErroInLine("Posição inválida! Tente: " + string.Join(", ", posicoesValidas), _column + 2, _row + _height - 2);
                        continue;
                    }
                    _current.Posicao = posEncontrada;
                    _tela.MostrarErroInLine("", _column + 2, _row + _height - 2);
                    break;
                }

                string codigoTime;
                do
                {
                    Console.SetCursorPosition(_column + 19, _row + 10);
                    Console.Write(new string(' ', _width - 21));
                    Console.SetCursorPosition(_column + 19, _row + 10);
                    codigoTime = (Console.ReadLine() ?? "").ToUpper().Trim();

                    if (!TimeExiste(codigoTime))
                        _tela.MostrarErroInLine($"Time '{codigoTime}' não encontrado. Tente novamente.", _column + 2, _row + _height - 2);

                } while (!TimeExiste(codigoTime));

                _current.CodigoTime = codigoTime;
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
                        _current.CodigoTime
                    ));

                    _data.SalvarJogador(_jogadores);
                    _tela.MostrarMensagem("Jogador incluído com sucesso!", _column + 2, _row + _height - 2);
                }
            }
        }
    }
}
