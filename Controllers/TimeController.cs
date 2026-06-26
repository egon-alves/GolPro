using System;
using System.Collections.Generic;
using System.Linq;
using GolPro.Models;
using GolPro.Utils;
namespace GolPro.Controller
{
    public class TimeController
    {
        private int _column, _row, _width, _height;

        private List<TimeModel> _times;
        private Tela _tela;
        private Data _data;
        private TimeModel _current; // registro encontrado/em edição

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
            this._data = new Data("Utils/Data/times.txt");
            // Registro pré-carregado
            this._times = this._data.CarregarTimes();
            TimeModel tPre = new TimeModel("PAL", "Palmeiras", "São Paulo");
            _times.Add(tPre);
        }

        // ── Busca (privado) ───────────────────────────────────────────────────

        private TimeModel FindByCode(string codigo)
        {
            foreach (TimeModel time in _times)
            {
                if (time.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                    return time;
            }
            return null;
        }

        // ── Busca (público — usado pelo PartidaController) ───────────────────

        public TimeModel BuscarPorCodigo(string codigo)
        {
            return FindByCode(codigo);
        }

        public void Salvar()
        {
            _data.SalvarTimes(_times);
        }

        // ── Tela ─────────────────────────────────────────────────────────────

        public void ShowForm()
        {
            _tela.PrepararTela("Cadastro de Times", _column, _row, _column + _width, _row + _height);

            Console.SetCursorPosition(_column + 2, _row + 3);
            Console.Write("Código  : ");

            Console.SetCursorPosition(_column + 2, _row + 5);
            Console.Write("Nome    : ");

            Console.SetCursorPosition(_column + 2, _row + 7);
            Console.Write("Cidade  : ");
        }

        public void EnterData(string which)
        {
            if (which == "PK")
            {
                string codigo;
                while (true)
                {
                    Console.SetCursorPosition(_column + 12, _row + 3);
                    Console.Write(new string(' ', _width - 14));
                    Console.SetCursorPosition(_column + 12, _row + 3);
                    codigo = (Console.ReadLine() ?? "").ToUpper().Trim();

                    if (string.IsNullOrEmpty(codigo)) {
                        _tela.MostrarErroInLine("O código não pode ser vazio!", _column + 2, _row + _height - 2);
                        continue;
                    }
                    if (codigo.Length > 3) {
                        _tela.MostrarErroInLine("O código deve ter no máximo 3 letras!", _column + 2, _row + _height - 2);
                        continue;
                    }
                    if (!codigo.All(char.IsLetter)) {
                        _tela.MostrarErroInLine("O código deve conter apenas letras!", _column + 2, _row + _height - 2);
                        continue;
                    }
                    _tela.MostrarErroInLine("", _column + 2, _row + _height - 2); // limpa msg erro
                    break;
                }
                _current = new TimeModel();
                _current.Codigo = codigo;
            }
            else if (which == "DT")
            {
                // Lê os demais dados (Nome e Cidade)
                while (true)
                {
                    Console.SetCursorPosition(_column + 12, _row + 5);
                    Console.Write(new string(' ', _width - 14));
                    Console.SetCursorPosition(_column + 12, _row + 5);
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

                while (true)
                {
                    Console.SetCursorPosition(_column + 12, _row + 7);
                    Console.Write(new string(' ', _width - 14));
                    Console.SetCursorPosition(_column + 12, _row + 7);
                    string cidade = (Console.ReadLine() ?? "").Trim();
                    if (string.IsNullOrEmpty(cidade) || !cidade.Replace(" ", "").All(char.IsLetter))
                    {
                        _tela.MostrarErroInLine("Cidade inválida! Use apenas letras.", _column + 2, _row + _height - 2);
                        continue;
                    }
                    _current.Cidade = cidade;
                    _tela.MostrarErroInLine("", _column + 2, _row + _height - 2);
                    break;
                }
            }
        }

        public void ShowData()
        {
            if (_current == null) return;

            // Limpa área dos campos
            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(_column + 12, _row + 3 + i);
                Console.Write(new string(' ', _width - 14));
            }

            Console.SetCursorPosition(_column + 12, _row + 3);
            Console.Write(_current.Codigo);

            Console.SetCursorPosition(_column + 12, _row + 5);
            Console.Write(_current.Nome);

            Console.SetCursorPosition(_column + 12, _row + 7);
            Console.Write(_current.Cidade);
        }


        // ── CRUD ──────────────────────────────────────────────────────────────

        public void CRUD()
        {
            ShowForm();
            EnterData("PK");

            TimeModel encontrado = FindByCode(_current.Codigo);

            if (encontrado != null)
            {
                // Registro existe: exibe dados e oferece Alterar ou Excluir
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
                    // Alterar: redesenha form com dados atuais e pede novos DT
                    ShowForm();
                    ShowData();
                    EnterData("DT");
                    encontrado.Nome   = _current.Nome;
                    encontrado.Cidade = _current.Cidade;
                    _data.SalvarTimes(_times);
                    _tela.MostrarMensagem("Time alterado com sucesso!", _column + 2, _row + _height - 2);
                }
                else if (opcao == "2")
                {
                    // Excluir com confirmação
                    Console.SetCursorPosition(_column + 2, _row + _height - 2);
                    Console.Write($"Confirma exclusão de '{encontrado.Nome}'? (S/N): ");
                    string resp = (Console.ReadLine() ?? "").ToUpper();
                    if (resp == "S")
                    {
                        _times.Remove(encontrado);
                        _data.SalvarTimes(_times);
                        _tela.MostrarMensagem("Time excluído com sucesso!", _column + 2, _row + _height - 2);
                    }
                }
            }
            else
            {
                // Registro não existe: oferece Incluir
                string opcao = _tela.MostrarMenu(new List<string>
                {
                    "1 - Incluir",
                    "0 - Voltar"
                });

                if (opcao == "1")
                {
                    ShowForm();
                    // Reexibe o código já digitado
                    Console.SetCursorPosition(_column + 12, _row + 3);
                    Console.Write(_current.Codigo);

                    EnterData("DT");
                    _times.Add(new TimeModel(_current.Codigo, _current.Nome, _current.Cidade));
                    _data.SalvarTimes(_times); 
                    _tela.MostrarMensagem("Time incluído com sucesso!", _column + 2, _row + _height - 2);
                }
            }
        }
    }
}
