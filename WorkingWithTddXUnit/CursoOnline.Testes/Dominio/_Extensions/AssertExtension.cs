using System;
using Xunit;

namespace CursoOnline.Teste.Dominio._Extensions
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            if (exception.Message == mensagem)
                Assert.True(true);
            else
                Assert.False(true, $"Esperava uma mensagem {mensagem}.");
        }
    }
}
