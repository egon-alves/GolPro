using System;

namespace GolPro.Model
{
    public class PartidaModel
    {
        private int _id;
        private string _codigoMandante;
        private string _codigoVisitante;
        private DateTime _data;
        private int _golsMandante;
        private int _golsVisitante;

        // Deixar os metodos publicos

        public int Id
        {
            get { return _id; }
            set { _id = value; }

        }

        public string CodigoMandante
        {
            get { return _codigoMandante; }
            set { _codigoMandante = value; }
        }

        public string CodigoVisitante
        {
            get { return _codigoVisitante; }
            set { _codigoVisitante = value; }
        }

        public DateTime Data
        {
            get { return _data; }
            set { _data = value; }

        }

        public int GolsMandante
        {
            get { return _golsMandante; }
            set { _golsMandante = value; }

        }

                public int GolVisitante
        {
            get { return _golsVisitante; }
            set { _golsVisitante = value; }

        }

    // Criar os  construtores
        public PartidaModel()
        {
            _id = 0;
            _codigoMandante = "";
            _codigoVisitante = "";
            _data = DateTime.Now;
            _golsMandante = 0;
            _golsVisitante = 0;
        }

        public PartidaModel(int id, string codigoMandante, string codigoVisitante, DateTime data, int golsMandante, int golsVisitante)
        {
            _id = id;
            _codigoMandante = codigoMandante;
            _codigoVisitante = codigoVisitante;
            _data = data;
            _golsMandante = golsMandante;
            _golsVisitante = golsVisitante;
        }



    // Metodos
        
        public string Placar()
        {
            return $"{_golsMandante} x {_golsVisitante}";
            
        }

        public string Serializar()
        {
                    {
            return $"{_id}|{_codigoMandante}|{_codigoVisitante}|{_data:yyyyMMdd}|{_golsMandante}|{_golsVisitante}";
        }
            
        }

    }


}