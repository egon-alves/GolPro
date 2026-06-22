using System;
using System.Collections.Generic;
using GolPro.Models;
using GolPro.Utils;
namespace GolPro.Controller
{
    public class PartidaController
    {
        private int _column, _row, _width, _height;
        private Tela              _tela;
        private TimeController    _timeController;
        private JogadorController _jogadorController;

        private List<PartidaModel> _partidas;
        private PartidaModel _current;

        private int _proximoId;

      // Construtor seguindo o padrão dos outros controllers

      public PartidaController(int col, int row, int width, int height, Tela tela, TimeController timeCtrl, JogadorController jogCtrl)
        {
            _column = col;
            _row    = row;
            _width  = width;
            _height = height;
            _tela = tela;
            _timeController = timeCtrl;
            _jogadorController = jogCtrl;
            _partidas = new List<PartidaModel>();
            _proximoId = 1;

            // registro do pre carregamento

            PartidaModel pPre = new PartidaModel(
                _proximoId++, "PAL", "FLA", 
                new DateTime(2026, 3, 10), 1, 3
            );
            _partidas.Add(pPre);
            _timeController.BuscarPorCodigo("PAL")?.RegistrarPartida(1, 3);
            _timeController.BuscarPorCodigo("FLA")?.RegistrarPartida(3, 1);
        }

        // ── Acesso à lista ──────────────────────────────────────────────────

        public List<PartidaModel> ObterTodos()     { return _partidas; }
        public int                ObterProximoId() { return _proximoId; }

        public void DefinirLista(List<PartidaModel> lista, int proximoId)
        {
            _partidas  = lista;
            _proximoId = proximoId;
        }

        /// <summary>Localiza uma partida pelo ID.</summary>
        private PartidaModel FindById(int id)
        {
            foreach (PartidaModel p in _partidas)
                if (p.Id == id)
                    return p;
            return null;
        }

        private void ShowForm()
        {
            _tela.PrepararTela("Registro de Partidas", _column, _row, _column + _width, _row + _height);

            Console.SetCursorPosition(_column + 2, _row + 3);
            Console.Write("ID              : ");

            Console.SetCursorPosition(_column + 2, _row + 5);
            Console.Write("Mandante        : ");

            Console.SetCursorPosition(_column + 2, _row + 7);
            Console.Write("Visitante       : ");

            Console.SetCursorPosition(_column + 2, _row + 9);
            Console.Write("Data (dd/MM/yyyy): ");

            Console.SetCursorPosition(_column + 2, _row + 11);
            Console.Write("Gols Mandante   : ");

            Console.SetCursorPosition(_column + 2, _row + 13);
            Console.Write("Gols Visitante  : ");
        }

        public void EnterData(string which){

         if(which == "PK"){
            Console.SetCursorPosition(_column + 20, _row + 3);
            int.TryParse(Console.ReadLine(), out int id);
            _current = new PartidaModel();
            _current.Id = id;

            

         }else if(which == "DT") {
            
                Console.SetCursorPosition(_column + 20, _row + 5);
                _current.CodigoMandante = (Console.ReadLine() ?? "").ToUpper().Trim();

                Console.SetCursorPosition(_column + 20, _row + 7);
                _current.CodigoVisitante = (Console.ReadLine() ?? "").ToUpper().Trim();

                Console.SetCursorPosition(_column + 20, _row + 9);
                string dataStr = Console.ReadLine() ?? "";
                DateTime.TryParseExact(dataStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data);
                _current.Data = data;

                Console.SetCursorPosition(_column + 20, _row + 11);
                int.TryParse(Console.ReadLine(), out int golsMandante);
                _current.GolsMandante = golsMandante;

                Console.SetCursorPosition(_column + 20, _row + 13);
                int.TryParse(Console.ReadLine(), out int golsVisitante);
                _current.GolsVisitante = golsVisitante;
         }  




        }


        public void ShowData()
        {
            if (_current == null) return;

            // Limpa área dos campos (de _row+3 até _row+13)
            for (int i = 0; i <= 10; i++)
            {
                Console.SetCursorPosition(_column + 20, _row + 3 + i);
                Console.Write(new string(' ', _width - 22));
            }

            Console.SetCursorPosition(_column + 20, _row + 3);
            Console.Write(_current.Id);

            Console.SetCursorPosition(_column + 20, _row + 5);
            Console.Write(_current.CodigoMandante);

            Console.SetCursorPosition(_column + 20, _row + 7);
            Console.Write(_current.CodigoVisitante);

            Console.SetCursorPosition(_column + 20, _row + 9);
            Console.Write(_current.Data.ToString("dd/MM/yyyy"));

            Console.SetCursorPosition(_column + 20, _row + 11);
            Console.Write(_current.GolsMandante);

            Console.SetCursorPosition(_column + 20, _row + 13);
            Console.Write(_current.GolsVisitante);
        }

        // ── CRUD ──────────────────────────────────────────────────────────────
        // Fluxo principal do registro de partidas.
        // Segue o mesmo padrão do TimeController.CRUD():
        //   1. Mostra formulário e lê a chave primária (ID)
        //   2. Se encontrou → oferece Alterar ou Excluir
        //   3. Se não encontrou → oferece Incluir
        //
        // DIFERENÇA em relação ao Time/Jogador:
        //   Ao incluir/alterar/excluir uma partida, as estatísticas dos dois
        //   times envolvidos (pontos, vitórias, gols, etc.) precisam ser
        //   atualizadas via RegistrarPartida() / EstornarResultado().

        public void CRUD()
        {
            // ── PASSO 1: Mostra formulário e lê o ID ─────────────────────
            ShowForm();
            EnterData("PK");

            // ── PASSO 2: Busca a partida pelo ID ─────────────────────────
            PartidaModel encontrado = FindById(_current.Id);

            if (encontrado != null)
            {
                // ══════════════════════════════════════════════════════════
                // PARTIDA ENCONTRADA — exibe dados e oferece Alterar/Excluir
                // ══════════════════════════════════════════════════════════
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
                    // ── ALTERAR ───────────────────────────────────────────
                    // 1) Estorna as estatísticas ANTIGAS dos dois times
                    //    (desfaz o efeito da partida anterior)
                    _timeController.BuscarPorCodigo(encontrado.CodigoMandante)
                        ?.EstornarResultado(encontrado.GolsMandante, encontrado.GolsVisitante);
                    _timeController.BuscarPorCodigo(encontrado.CodigoVisitante)
                        ?.EstornarResultado(encontrado.GolsVisitante, encontrado.GolsMandante);

                    // 2) Redesenha o form com os dados atuais e pede novos dados
                    ShowForm();
                    ShowData();
                    EnterData("DT");

                    // 3) Atualiza os campos do registro encontrado
                    encontrado.CodigoMandante  = _current.CodigoMandante;
                    encontrado.CodigoVisitante = _current.CodigoVisitante;
                    encontrado.Data            = _current.Data;
                    encontrado.GolsMandante    = _current.GolsMandante;
                    encontrado.GolsVisitante   = _current.GolsVisitante;

                    // 4) Registra as estatísticas NOVAS nos dois times
                    _timeController.BuscarPorCodigo(encontrado.CodigoMandante)
                        ?.RegistrarPartida(encontrado.GolsMandante, encontrado.GolsVisitante);
                    _timeController.BuscarPorCodigo(encontrado.CodigoVisitante)
                        ?.RegistrarPartida(encontrado.GolsVisitante, encontrado.GolsMandante);

                    _tela.MostrarMensagem("Partida alterada com sucesso!", _column + 2, _row + _height - 2);
                }
                else if (opcao == "2")
                {
                    // ── EXCLUIR ──────────────────────────────────────────
                    Console.SetCursorPosition(_column + 2, _row + _height - 2);
                    Console.Write($"Confirma exclusão da partida {encontrado.Id}? (S/N): ");
                    string resp = (Console.ReadLine() ?? "").ToUpper();

                    if (resp == "S")
                    {
                        // 1) Estorna as estatísticas dos dois times
                        _timeController.BuscarPorCodigo(encontrado.CodigoMandante)
                            ?.EstornarResultado(encontrado.GolsMandante, encontrado.GolsVisitante);
                        _timeController.BuscarPorCodigo(encontrado.CodigoVisitante)
                            ?.EstornarResultado(encontrado.GolsVisitante, encontrado.GolsMandante);

                        // 2) Remove da lista
                        _partidas.Remove(encontrado);

                        _tela.MostrarMensagem("Partida excluída com sucesso!", _column + 2, _row + _height - 2);
                    }
                }
            }
            else
            {
                // ══════════════════════════════════════════════════════════
                // PARTIDA NÃO ENCONTRADA — oferece Incluir
                // ══════════════════════════════════════════════════════════
                string opcao = _tela.MostrarMenu(new List<string>
                {
                    "1 - Incluir",
                    "0 - Voltar"
                });

                if (opcao == "1")
                {
                    // ── INCLUIR ──────────────────────────────────────────
                    ShowForm();

                    // 1) Gera o ID automaticamente e exibe na tela
                    _current.Id = _proximoId++;
                    Console.SetCursorPosition(_column + 20, _row + 3);
                    Console.Write(_current.Id);

                    // 2) Lê os demais dados (mandante, visitante, data, gols)
                    EnterData("DT");

                    // 3) Cria a nova partida e adiciona à lista
                    PartidaModel nova = new PartidaModel(
                        _current.Id,
                        _current.CodigoMandante,
                        _current.CodigoVisitante,
                        _current.Data,
                        _current.GolsMandante,
                        _current.GolsVisitante
                    );
                    _partidas.Add(nova);

                    // 4) Registra as estatísticas nos dois times
                    //    Mandante: fez GolsMandante, sofreu GolsVisitante
                    //    Visitante: fez GolsVisitante, sofreu GolsMandante
                    _timeController.BuscarPorCodigo(nova.CodigoMandante)
                        ?.RegistrarPartida(nova.GolsMandante, nova.GolsVisitante);
                    _timeController.BuscarPorCodigo(nova.CodigoVisitante)
                        ?.RegistrarPartida(nova.GolsVisitante, nova.GolsMandante);

                    _tela.MostrarMensagem("Partida registrada com sucesso!", _column + 2, _row + _height - 2);
                }
            }
        }
    }
}