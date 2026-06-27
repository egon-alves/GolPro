using System;

namespace GolPro.Models
{
    public class PartidaModel
    {
        private int _id;
        private string _codigoMandante;
        private string _codigoVisitante;
        private DateTime _data;
        private int _golsMandante;
        private int _golsVisitante;
        private List<string> _golsMandanteMatriculas;
        private List<string> _golsVisitanteMatriculas;

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

        public int GolsVisitante
        {
            get { return _golsVisitante; }
            set { _golsVisitante = value; }

        }

        public List<string> GolsMandanteMatriculas
        {
            get { return _golsMandanteMatriculas; }
            set { _golsMandanteMatriculas = value; }
        }

        public List<string> GolsVisitanteMatriculas
        {
            get { return _golsVisitanteMatriculas; }
            set { _golsVisitanteMatriculas = value; }
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
            _golsMandanteMatriculas = new List<string>();
            _golsVisitanteMatriculas = new List<string>();
        }

        public PartidaModel(int id, string codigoMandante, string codigoVisitante, DateTime data, int golsMandante, int golsVisitante)
        {
            _id = id;
            _codigoMandante = codigoMandante;
            _codigoVisitante = codigoVisitante;
            _data = data;
            _golsMandante = golsMandante;
            _golsVisitante = golsVisitante;
            _golsMandanteMatriculas = new List<string>();
            _golsVisitanteMatriculas = new List<string>();
        }



    // Metodos
        
        public string Serializar()
        {
            string mandanteMats = string.Join(",", _golsMandanteMatriculas);
            string visitanteMats = string.Join(",", _golsVisitanteMatriculas);
            return $"{_id};{_codigoMandante};{_codigoVisitante};{_data:yyyyMMdd};{_golsMandante};{_golsVisitante};{mandanteMats};{visitanteMats}";
        }
            
        

    }


}