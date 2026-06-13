using System;
using System.Collections.Generic;
using GolPro.Model;
using GolPro.utils;

namespace GolPro.Controllers
{

    public class TimeController
    {
        private int _column, _row, _width, _heigth, _position;
        private string _title;
        private List<string> _fields;
        private TimeModel _time;
    
        private List<TimeModel> _times;
        private Tela _tela;

        public List<TimeModel> Times
        {
            get { return _times; }
            set { _times = value; }
        }

        public TimeController(int col, int row, Tela tela)
        {
            this._column = col;
            this._row = row;
            this._tela = tela;
            this._fields = new List<string> { "Código", "Nome", "Cidade" };
            this._times = new List<TimeModel>();
        }
        

    }

}