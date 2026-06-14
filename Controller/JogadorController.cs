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
        private JogadorModel _current; // registro encontrado/em edição

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
            this._fields = new List<string> { "Código", "Nome", "Posição", "Time" };
            this._data = new Data("Utils/Data/jogadores.txt");
            

            _jogadores.Add(new JogadorModel("J001", "Gabriel Barbosa",  "Atacante", "FLA"));
            _jogadores.Add(new JogadorModel("J002", "Endrick",          "Atacante", "PAL"));
            _jogadores.Add(new JogadorModel("J003", "Diego Souza",      "Meia",     "GRE"));


            this._jogadores = this._data.CarregarJogador();
            // Registro pré-carregado
            // Não tem função
            //  this._jogadores = this._data.CarregarJogadores();
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

        public void ShowForm()
        {
            _tela.PrepararTela("Cadastro de Jogadores", _column, _row, _column + _width, _row + _height);

            Console.SetCursorPosition(_column + 2, _row + 3);
            Console.Write("Matrícula  : ");

            Console.SetCursorPosition(_column + 2, _row + 5);
            Console.Write("Nome    : ");

            Console.SetCursorPosition(_column + 2, _row + 7);
            Console.Write("Posição  : ");

            Console.SetCursorPosition(_column + 2, _row + 7);
            Console.Write("Código do Time  : ");
        }

        public void EnterData(string which)
        {
            if (which == "PK")
            {
                // Lê apenas a chave primária (Código)
                Console.SetCursorPosition(_column + 12, _row + 3);
                string matricula = (Console.ReadLine() ?? "").ToUpper().Trim();
                _current = new JogadorModel();
                _current.Matricula = matricula;

            }
            else if (which == "DT")
            {
                // Lê os demais dados (Nome e Cidade)
                Console.SetCursorPosition(_column + 12, _row + 5);

                _current.Nome = Console.ReadLine() ?? "";

                Console.SetCursorPosition(_column + 12, _row + 7);
                _current.CodigoTime = Console.ReadLine() ?? "";

            }
        }

        public void ShowData()
        {
            if (_current == null) return;
            //limpar area dos campos

            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(_column + 12, _row + 3 + i);
                Console.Write(new string(' ', _width - 14));
            }

            Console.SetCursorPosition(_column + 12, _row + 3);
            Console.Write(_current.Matricula);

            Console.SetCursorPosition(_column + 12, _row + 5);
            Console.Write(_current.Nome);

            Console.SetCursorPosition(_column + 12, _row + 7);
            Console.Write(_current.CodigoTime);

        }




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
                    encontrado.Nome   = _current.Nome;
                    encontrado.CodigoTime = _current.CodigoTime;
                    _tela.MostrarMensagem("Opção Alterar ", _column + 2, _row + _height - 2);
                }
                else if (opcao == "2")
                {
                
                    _tela.MostrarMensagem("Opção  de Excluir", _column + 2, _row + _height - 2);
                }

            }else
            {
                string opcao = _tela.MostrarMenu(new List<string>
                {
                    "1 - Incluir",
                    "2 - Voltar"
                });

                if(opcao == "1")
                {
                    
                    _tela.MostrarMensagem("Opção  de Incluir", _column + 2, _row + _height - 2);
                }
            }


        }





    }
}
