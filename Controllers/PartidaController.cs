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
        private Data _data;

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
            _data = new Data("Utils/Data/partidas.txt");
            _partidas = _data.CarregarPartidas();
            

            if (_partidas.Count > 0)
                _proximoId = _partidas[_partidas.Count - 1].Id + 1;
            else
                _proximoId = 1;

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
            string entradaId = Console.ReadLine() ?? "";
            int id;
            if (string.IsNullOrWhiteSpace(entradaId)) {
                id = _proximoId;
                Console.SetCursorPosition(_column + 20, _row + 3);
                Console.Write(id);
            } else {
                int.TryParse(entradaId, out id);
            }
            _current = new PartidaModel();
            _current.Id = id;

         }else if(which == "DT") {
                string codMandante;
                while(true) {
                    Console.SetCursorPosition(_column + 20, _row + 5);
                    Console.Write(new string(' ', _width - 22));
                    Console.SetCursorPosition(_column + 20, _row + 5);
                    codMandante = (Console.ReadLine() ?? "").ToUpper().Trim();

                    if (string.IsNullOrEmpty(codMandante)) {
                        _tela.MostrarErroInLine("O time Mandante não pode ser vazio!", _column + 2, _row + _height - 2);
                        continue;
                    }
                    if (_timeController.BuscarPorCodigo(codMandante) == null) {
                        _tela.MostrarErroInLine($"Time '{codMandante}' não cadastrado!", _column + 2, _row + _height - 2);
                        continue;
                    }
                    _current.CodigoMandante = codMandante;
                    _tela.MostrarErroInLine("", _column + 2, _row + _height - 2);
                    break;
                }

                string codVisitante;
                while(true) {
                    Console.SetCursorPosition(_column + 20, _row + 7);
                    Console.Write(new string(' ', _width - 22));
                    Console.SetCursorPosition(_column + 20, _row + 7);
                    codVisitante = (Console.ReadLine() ?? "").ToUpper().Trim();

                    if (string.IsNullOrEmpty(codVisitante)) {
                        _tela.MostrarErroInLine("O time Visitante não pode ser vazio!", _column + 2, _row + _height - 2);
                        continue;
                    }
                    if (_timeController.BuscarPorCodigo(codVisitante) == null) {
                        _tela.MostrarErroInLine($"Time '{codVisitante}' não cadastrado!", _column + 2, _row + _height - 2);
                        continue;
                    }
                    if (codVisitante == codMandante) {
                        _tela.MostrarErroInLine("Visitante não pode ser igual ao Mandante!", _column + 2, _row + _height - 2);
                        continue;
                    }
                    _current.CodigoVisitante = codVisitante;
                    _tela.MostrarErroInLine("", _column + 2, _row + _height - 2);
                    break;
                }

                while(true) {
                    Console.SetCursorPosition(_column + 20, _row + 9);
                    Console.Write(new string(' ', _width - 22));
                    Console.SetCursorPosition(_column + 20, _row + 9);
                    string dataStr = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(dataStr)) {
                        _current.Data = DateTime.Now.Date;
                        Console.SetCursorPosition(_column + 20, _row + 9);
                        Console.Write(_current.Data.ToString("dd/MM/yyyy"));
                        break;
                    }
                    
                    if (DateTime.TryParseExact(dataStr, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data)) {
                        _current.Data = data;
                        _tela.MostrarErroInLine("", _column + 2, _row + _height - 2);
                        break;
                    } else {
                        _tela.MostrarErroInLine("Data inválida! Use o formato dd/MM/yyyy ou deixe vazio.", _column + 2, _row + _height - 2);
                    }
                }

                while(true) {
                    Console.SetCursorPosition(_column + 20, _row + 11);
                    Console.Write(new string(' ', _width - 22));
                    Console.SetCursorPosition(_column + 20, _row + 11);
                    string gmStr = Console.ReadLine() ?? "";
                    if (int.TryParse(gmStr, out int gm) && gm >= 0) {
                        _current.GolsMandante = gm;
                        _tela.MostrarErroInLine("", _column + 2, _row + _height - 2);
                        break;
                    } else {
                        _tela.MostrarErroInLine("Valor inválido! Digite um número >= 0.", _column + 2, _row + _height - 2);
                    }
                }

                while(true) {
                    Console.SetCursorPosition(_column + 20, _row + 13);
                    Console.Write(new string(' ', _width - 22));
                    Console.SetCursorPosition(_column + 20, _row + 13);
                    string gvStr = Console.ReadLine() ?? "";
                    if (int.TryParse(gvStr, out int gv) && gv >= 0) {
                        _current.GolsVisitante = gv;
                        _tela.MostrarErroInLine("", _column + 2, _row + _height - 2);
                        break;
                    } else {
                        _tela.MostrarErroInLine("Valor inválido! Digite um número >= 0.", _column + 2, _row + _height - 2);
                    }
                }
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

        // ── Auxiliares para Gols de Jogadores ────────────────────────────────

        private void EstornarGolsJogadores(List<string> matriculas)
        {
            foreach (string matricula in matriculas)
            {
                JogadorModel jogador = _jogadorController.BuscarPorMatricula(matricula);

                jogador?.EstornarGols(1);
            }
        }

        private void RegistrarGolsJogadores(int quantidadeGols, string codigoTime, List<string> listaMatriculas, string tipoTime)
        {
            listaMatriculas.Clear();
            for (int i = 0; i < quantidadeGols; i++)
            {
                while (true)
                {
                    // Limpa as duas últimas linhas
                    Console.SetCursorPosition(_column + 2, _row + _height - 3);
                    Console.Write(new string(' ', _width - 4));
                    Console.SetCursorPosition(_column + 2, _row + _height - 2);
                    Console.Write(new string(' ', _width - 4));

                    Console.SetCursorPosition(_column + 2, _row + _height - 3);
                    Console.Write($"Nome (ou matrícula) do autor do gol {i + 1} do {tipoTime} ({codigoTime}): ");
                    string busca = (Console.ReadLine() ?? "").ToUpper().Trim();

                    // Busca o jogador pelo nome (que contenha o texto) ou pela matrícula exata
                    var encontrados = _jogadorController.Jogadores.FindAll(j => 
                        j.CodigoTime == codigoTime && 
                        (j.Nome.ToUpper().Contains(busca) || j.Matricula.ToUpper() == busca));

                    if (encontrados.Count == 1)
                    {
                        JogadorModel jog = encontrados[0];
                        Console.SetCursorPosition(_column + 2, _row + _height - 2);
                        Console.Write($"Registrar gol para {jog.Nome} (Mat: {jog.Matricula})? (S/N): ");
                        string resp = (Console.ReadLine() ?? "").ToUpper();
                        
                        if (resp == "S")
                        {
                            listaMatriculas.Add(jog.Matricula);
                            jog.AdicionarGols(1);
                            break; // sai do while e vai para o próximo gol
                        }
                    }
                    else if (encontrados.Count > 1)
                    {
                        _tela.MostrarMensagem($"Encontrados {encontrados.Count} jogadores. Seja mais específico no nome!", _column + 2, _row + _height - 2);
                    }
                    else
                    {
                        _tela.MostrarMensagem("Nenhum jogador encontrado com esse nome neste time!", _column + 2, _row + _height - 2);
                    }
                }
            }
        }

        // ── CRUD ──────────────────────────────────────────────────────────────
        public void CRUD()
        {
            ShowForm();
            EnterData("PK");

            PartidaModel encontrado = FindById(_current.Id);

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
                    // 1) Estorna as estatísticas ANTIGAS
                    _timeController.BuscarPorCodigo(encontrado.CodigoMandante)
                        ?.EstornarResultado(encontrado.GolsMandante, encontrado.GolsVisitante);
                    _timeController.BuscarPorCodigo(encontrado.CodigoVisitante)
                        ?.EstornarResultado(encontrado.GolsVisitante, encontrado.GolsMandante);
                    EstornarGolsJogadores(encontrado.GolsMandanteMatriculas);
                    EstornarGolsJogadores(encontrado.GolsVisitanteMatriculas);

                    ShowForm();
                    ShowData();
                    EnterData("DT");

                    encontrado.CodigoMandante  = _current.CodigoMandante;
                    encontrado.CodigoVisitante = _current.CodigoVisitante;
                    encontrado.Data            = _current.Data;
                    encontrado.GolsMandante    = _current.GolsMandante;
                    encontrado.GolsVisitante   = _current.GolsVisitante;

                    // 2) Pergunta quem fez os gols
                    RegistrarGolsJogadores(encontrado.GolsMandante, encontrado.CodigoMandante, encontrado.GolsMandanteMatriculas, "Mandante");
                    RegistrarGolsJogadores(encontrado.GolsVisitante, encontrado.CodigoVisitante, encontrado.GolsVisitanteMatriculas, "Visitante");

                    // 3) Registra as estatísticas NOVAS e salva
                    _timeController.BuscarPorCodigo(encontrado.CodigoMandante)
                        ?.RegistrarPartida(encontrado.GolsMandante, encontrado.GolsVisitante);
                    _timeController.BuscarPorCodigo(encontrado.CodigoVisitante)
                        ?.RegistrarPartida(encontrado.GolsVisitante, encontrado.GolsMandante);
                    
                    _jogadorController.Salvar();
                    _timeController.Salvar();
                    _data.SalvarPartidas(_partidas);

                    _tela.MostrarMensagem("Partida alterada com sucesso!", _column + 2, _row + _height - 2);
                }
                else if (opcao == "2")
                {
                    Console.SetCursorPosition(_column + 2, _row + _height - 2);
                    Console.Write($"Confirma exclusão da partida {encontrado.Id}? (S/N): ");
                    string resp = (Console.ReadLine() ?? "").ToUpper();

                    if (resp == "S")
                    {
                        _timeController.BuscarPorCodigo(encontrado.CodigoMandante)
                            ?.EstornarResultado(encontrado.GolsMandante, encontrado.GolsVisitante);
                        _timeController.BuscarPorCodigo(encontrado.CodigoVisitante)
                            ?.EstornarResultado(encontrado.GolsVisitante, encontrado.GolsMandante);
                        EstornarGolsJogadores(encontrado.GolsMandanteMatriculas);
                        EstornarGolsJogadores(encontrado.GolsVisitanteMatriculas);

                        _partidas.Remove(encontrado);
                        _jogadorController.Salvar();
                        _timeController.Salvar();
                        _data.SalvarPartidas(_partidas);

                        _tela.MostrarMensagem("Partida excluída com sucesso!", _column + 2, _row + _height - 2);
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

                    _current.Id = _proximoId++;
                    Console.SetCursorPosition(_column + 20, _row + 3);
                    Console.Write(_current.Id);

                    EnterData("DT");

                    PartidaModel nova = new PartidaModel(
                        _current.Id,
                        _current.CodigoMandante,
                        _current.CodigoVisitante,
                        _current.Data,
                        _current.GolsMandante,
                        _current.GolsVisitante
                    );

                    // Pergunta quem fez os gols
                    RegistrarGolsJogadores(nova.GolsMandante, nova.CodigoMandante, nova.GolsMandanteMatriculas, "Mandante");
                    RegistrarGolsJogadores(nova.GolsVisitante, nova.CodigoVisitante, nova.GolsVisitanteMatriculas, "Visitante");

                    _partidas.Add(nova);

                    _timeController.BuscarPorCodigo(nova.CodigoMandante)
                        ?.RegistrarPartida(nova.GolsMandante, nova.GolsVisitante);
                    _timeController.BuscarPorCodigo(nova.CodigoVisitante)
                        ?.RegistrarPartida(nova.GolsVisitante, nova.GolsMandante);
                    
                    _jogadorController.Salvar();
                    _timeController.Salvar();
                    _data.SalvarPartidas(_partidas);

                    _tela.MostrarMensagem("Partida registrada com sucesso!", _column + 2, _row + _height - 2);
                }
            }
        }

        // ── Relatórios ────────────────────────────────────────────────────────

        public void Relatorio()
        {
            _tela.PrepararTela("Tabela de Classificação", _column, _row, _column + _width, _row + _height);
            
            // LINQ order by: Pontos > SaldoGols > GolsPro
            var times = _timeController.Times;
            times.Sort((t1, t2) => {
                if (t1.Pontos != t2.Pontos) return t2.Pontos.CompareTo(t1.Pontos);
                if (t1.SaldoGols != t2.SaldoGols) return t2.SaldoGols.CompareTo(t1.SaldoGols);
                return t2.GolsPro.CompareTo(t1.GolsPro);
            });

            Console.SetCursorPosition(_column + 2, _row + 3);
            Console.Write("Cód  Nome                 Pts  V   E   D   GP  GC  SG");

            int linha = 5;
            foreach(var t in times)
            {
                if (linha >= _row + _height - 2) break;
                Console.SetCursorPosition(_column + 2, _row + linha);
                Console.Write($"{t.Codigo,-4} {t.Nome,-20} {t.Pontos,3} {t.Vitorias,3} {t.Empates,3} {t.Derrotas,3} {t.GolsPro,3} {t.GolsContra,3} {t.SaldoGols,3}");
                linha++;
            }

            Console.SetCursorPosition(_column + 2, _row + _height - 2);
            Console.Write("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }

        public void ReportArtilheiros()
        {
            _tela.PrepararTela("Artilheiros do Campeonato", _column, _row, _column + _width, _row + _height);
            
            var jogadores = _jogadorController.Jogadores.FindAll(j => j.GolsMarcados > 0);
            jogadores.Sort((j1, j2) => j2.GolsMarcados.CompareTo(j1.GolsMarcados));

            Console.SetCursorPosition(_column + 2, _row + 3);
            Console.Write("Matrícula  Nome                 Time  Gols");

            int linha = 5;
            if (jogadores.Count == 0)
            {
                Console.SetCursorPosition(_column + 2, _row + 5);
                Console.Write("Nenhum gol registrado ainda.");
            }
            else
            {
                foreach(var j in jogadores)
                {
                    if (linha >= _row + _height - 2) break;
                    Console.SetCursorPosition(_column + 2, _row + linha);
                    Console.Write($"{j.Matricula,-10} {j.Nome,-20} {j.CodigoTime,-5} {j.GolsMarcados,4}");
                    linha++;
                }
            }

            Console.SetCursorPosition(_column + 2, _row + _height - 2);
            Console.Write("Pressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}