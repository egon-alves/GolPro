using System;

namespace GolPro.Model
{
    public class PartidaModel
    {
        private int _id;
        private string _codigoMandate;
        private string _codigoVisitante;
        private DateTime _data;
        private int _golMandante;
        private int _golVisitante;

        // Deixar os metodos publicos

        public int Id
        {
            get { return _id; }
            set { _id = value; }

        }

        public string CodigoMandante
        {
            get { return _codigoMandate; }
            set { _codigoMandate = value; }
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

        public int GolMandante
        {
            get { return _golMandante; }
            set { _golMandante = value; }

        }

                public int GolVisitante
        {
            get { return _golVisitante; }
            set { _golVisitante = value; }

        }

    // Criar os  construtores


    // Metodos
        

    }


}