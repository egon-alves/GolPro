using System;

namespace GolPro.Models
{
    public class JogadorModel
    {
        
        // Atributos do Jogador

        private string _matricula;
        private string _nome;
        private string _posicao;
        private string _codigoTime;
        private int _golsMarcados;


         // Propriedades responsáveis por permitir a leitura e alteração
        // dos atributos privados da classe JogadorModel.

        public string Matricula
        {
            get { return _matricula; }
            set { _matricula = value; }
        }

        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public string Posicao
        {
            get { return _posicao; }
            set { _posicao = value; }
        }

        public string CodigoTime
        {
            get { return _codigoTime; }
            set { _codigoTime = value; }
        }


        public int GolsMarcados
        {
            get { return _golsMarcados; }
            set { _golsMarcados = value; }
        }

  // Construtores


        public JogadorModel(string matricula, string nome, string posicao, string codigoTime)
        {
            _matricula = matricula;
            _nome = nome;
            _posicao = posicao;
            _codigoTime = codigoTime;
            _golsMarcados = 0;
        }

                public JogadorModel()
        {
            _matricula  = "";
            _nome       = "";
            _posicao    = "";
            _codigoTime = "";
            _golsMarcados       = 0;
        }

         // Metodos

        public void AdicionarGols(int gols)
        {
            _golsMarcados += gols;
        }


        public void EstournarGols(int gols)
        {
            _golsMarcados -= gols;
        }

        public string Serializar()
        {
            return $"{_matricula};{_nome};{_posicao};{_codigoTime};{_golsMarcados}";
        }




    }
}