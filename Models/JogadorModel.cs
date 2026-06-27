using System;

namespace GolPro.Models
{
    public class JogadorModel
    {
                private string _matricula;
        private string _nome;
        private string _posicao;
        private string _codigoTime;
        private int _golsMarcados;


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


        public JogadorModel(string matricula, string nome, string codigoTime)
        {
            _matricula = matricula;
            _nome = nome;
    
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

        /// <summary>
        /// Incrementa a contagem de gols marcados pelo jogador.
        /// </summary>
        public void AdicionarGols(int gols)
        {
            _golsMarcados += gols;
        }


        /// <summary>
        /// Decrementa a contagem de gols marcados pelo jogador (usado ao estornar partidas).
        /// </summary>
        public void EstornarGols(int gols)
        {
            _golsMarcados -= gols;
        }

        /// <summary>
        /// Converte os dados do jogador em uma string delimitada por ponto-e-vírgula para persistência em arquivo.
        /// </summary>
        public string Serializar()
        {
            return $"{_matricula};{_nome};{_posicao};{_codigoTime};{_golsMarcados}";
        }




    }
}