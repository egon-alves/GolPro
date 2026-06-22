using System;
using System.Collections.Generic;
using GolPro.Models;
using GolPro.Utils;
namespace GolPro.Controller
{
    public class PartidaController
    {
        private Tela              _tela;
        private TimeController    _timeController;
        private JogadorController _jogadorController;

        private List<PartidaModel> _partidas;

        private int _proximoId;

      // VOu montar um construtor 

      public PartidaController(Tela tela, TimeController timeCtrl, JogadorController jogCtrl)
        {
            _tela = tela;
            _timeController = timeCtrl;
            _jogadorController = jogCtrl;
            _partidas= new List<PartidaModel>();
            _proximoId = 1;

            // registro do pre carregamento

            PartidaModel pPre = new PartidaModel(
                _proximoId++, "PAL", "FLA", 
                new DateTime(2026,3,10), 1,3
            );
            _partidas.Add(pPre);
            _timeController.BuscarPorCodigo("PAL")?.RegistrarResultado(2, 1);
            _timeController.BuscarPorCodigo("FLA")?.RegistrarResultado(1, 2);

                    // ── Acesso à lista ──────────────────────────────────────────────────

            public List<PartidaModel> ObterTodos()   { return _partidas; }
            public int                ObterProximoId() { return _proximoId; }

            public void DefinirLista(List<PartidaModel> lista, int proximoId)
            {
                _partidas   = lista;
                _proximoId  = proximoId;
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
            
        }



        }
    }
}