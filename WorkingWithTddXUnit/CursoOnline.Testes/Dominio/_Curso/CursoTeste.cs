using CursoOnline.Teste.Dominio._Builders;
using CursoOnline.Teste.Dominio._Extensions;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

#region Caso de Uso

/* Eu, enquanto administrador, quero criar e editar cursos para que sejam abertas matrículas para o mesmo.
 * 
 * DoD (Definition of Done) = Critério de aceite:
 *  => Criar um curso com nome, carga horária, público alvo e valor de curso;
 *  => As opções para público alvo são: Estudante, Universitário, Empregado e Empreendedor
 *  => Todos os campos do curso são obrigatórios.
 *  
 *  => Curso pode ou não ter uma descrição
 */

#endregion

namespace CursoOnline.Teste.Dominio._Curso
{
    public class CursoTeste : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly int _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public CursoTeste(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo executado!");

            _nome = "Informática Básica";
            _cargaHoraria = 80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 950;
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo executado!");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            //Arrange
            var cursoEsperado = new
            {
                nome = "Informática Básica",
                cargaHoraria = (double)80,
                descricao = "descricao genérica",
                publicoAlvo = PublicoAlvo.Estudante,
                valor = (double)950,
            };
            
            //Act
            var curso = new Curso(cursoEsperado.nome, cursoEsperado.cargaHoraria, cursoEsperado.descricao, cursoEsperado.publicoAlvo, cursoEsperado.valor);
            
            //Assert
            cursoEsperado.ToExpectedObject().Matches(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CursoNaoDeveTerNomeVazioOuNulo(string nomeInvalido)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => 
                CursoBuilder.NovoCurso()
                    .ComNome(nomeInvalido)
                    .ComCargaHoraria(_cargaHoraria)
                    .ComPublicoAlvo(_publicoAlvo)
                    .ComValor(_valor)
                    .Build())
                .ComMensagem("Curso não pode ter com nome vazio ou nulo!");   
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void CursoNaoDeveTerCargaHorariaMenorQue1(int cargaHorariaInvalida)
        {
            //Assert
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.NovoCurso()
                    .ComNome(_nome)
                    .ComCargaHoraria(cargaHorariaInvalida)
                    .ComPublicoAlvo(_publicoAlvo)
                    .ComValor(_valor)
                    .Build())
                .ComMensagem("Curso precisa ter horária!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CursoNaoDeveTerValorMenorQue1(double valorInvalido)
        {
            //Assert
            Assert.Throws<ArgumentException>(() =>
                 CursoBuilder.NovoCurso()
                    .ComNome(_nome)
                    .ComCargaHoraria(_cargaHoraria)
                    .ComPublicoAlvo(_publicoAlvo)
                    .ComValor(valorInvalido)
                    .Build())
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
        public string Descricao { get; }
        public PublicoAlvo PublicoAlvo { get; }
        public double Valor { get; }

        public Curso() { }

        public Curso(string nome, double cargaHoraria, string descricao, PublicoAlvo publicoAlvo, double valor)
        {
            if(string.IsNullOrEmpty(nome))
                throw new ArgumentException("Curso não pode ter com nome vazio ou nulo!");
            
            if (cargaHoraria < 1)
                throw new ArgumentException("Curso precisa ter horária!");

            if (valor < 1)
                throw new ArgumentException("Curso precisa não pode ter valor zerado!");

            Nome = nome;
            CargaHoraria = cargaHoraria;
            Descricao = descricao;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

    }
}
