using System;

namespace GolPro.Utils
{
    public class VoltarMenuException : Exception
    {
        public VoltarMenuException() : base("Operação cancelada pelo usuário.") { }
    }
}
