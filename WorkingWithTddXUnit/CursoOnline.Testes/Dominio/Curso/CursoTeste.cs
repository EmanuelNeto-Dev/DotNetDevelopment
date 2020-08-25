using CursoOnline.Teste.Dominio._Extensions;
using ExpectedObjects;
using System;
using Xunit;

#region Caso de Uso

/* Eu, enquanto administrador, quero criar e editar cursos para que sejam abertas matrículas para o mesmo.
 * 
 * DoD (Definition of Done) = Critério de aceite:
 *  => Criar um curso com nome, carga horária, público alvo e valor de curso;
 *  => As opções para público alvo são: Estudante, Universitário, Empregado e Empreendedor
 *  => Todos os campos do curso são obrigatórios.
 */

#endregion

namespace CursoOnline.Teste.Dominio.Curso
{
    public class CursoTeste
    {
        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                nome = "Informática Básica",
                cargaHoraria = (double)80,
                publicoAlvo = PublicoAlvo.Estudante,
                valor = (double)950,
            };
            
            //Act
            var curso = new Curso(cursoEsperado.nome, cursoEsperado.cargaHoraria, cursoEsperado.publicoAlvo, cursoEsperado.valor);
            
            //Assert
            cursoEsperado.ToExpectedObject().Matches(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerNomeVazioOuNulo(string param)
        {
            //Arrange
            var cursoEsperado = new
            {
                nome = param,
                cargaHoraria = (double)80,
                publicoAlvo = PublicoAlvo.Estudante,
                valor = (double)950,
            };

            //Assert
            Assert.Throws<ArgumentException>(() => 
                new Curso(cursoEsperado.nome, cursoEsperado.cargaHoraria, cursoEsperado.publicoAlvo, cursoEsperado.valor))
                .ComMensagem("Curso não pode ter com nome vazio ou nulo!");
            
        }

        [Fact]
        public void CursoNaoDeveTerCargaHorariaMenorQue1()
        {
            //Arrange
            var cursoEsperado = new
            {
                nome = "Informática Básica",
                cargaHoraria = (double)80,
                publicoAlvo = PublicoAlvo.Estudante,
                valor = (double)950,
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.nome, 0, cursoEsperado.publicoAlvo, cursoEsperado.valor))
                .ComMensagem("Curso precisa ter horária!");
        }

        [Fact]
        public void CursoNaoDeveTerValorMenorQue1()
        {
            //Arrange
            var cursoEsperado = new
            {
                nome = "Informática Básica",
                cargaHoraria = (double)80,
                publicoAlvo = PublicoAlvo.Estudante,
                valor = (double)950,
            };

            //Assert
            Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.nome, cursoEsperado.cargaHoraria, cursoEsperado.publicoAlvo, 0))
                .ComMensagem("Curso precisa não pode ter valor zerado!");
        }
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }

    public class Curso
    {
        public string Nome { get; }
        public double CargaHoraria { get; }
        public PublicoAlvo PublicoAlvo { get; }
        public double Valor { get; }

        public Curso() { }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if(string.IsNullOrEmpty(nome))
                throw new ArgumentException("Curso não pode ter com nome vazio ou nulo!");
            
            if (cargaHoraria < 1)
                throw new ArgumentException("Curso precisa ter horária!");

            if (valor < 1)
                throw new ArgumentException("Curso precisa não pode ter valor zerado!");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

    }
}
