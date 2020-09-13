using Bogus;
using CursoOnline.Dominio._Curso;
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
        private readonly string _descricao;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public CursoTeste(ITestOutputHelper output)
        {
            var faker = new Faker();

            _output = output;
            _output.WriteLine("Construtor sendo executado!");

            _nome           = faker.Random.Word();
            _cargaHoraria   = faker.Random.Int(50, 1000);
            _descricao      = faker.Lorem.Paragraph();
            _publicoAlvo    = PublicoAlvo.Estudante;
            _valor          = (double) faker.Finance.Amount();
            
            _output.WriteLine($"Company fake data: {faker.Company.CompanyName()}");
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
                nome = _nome,
                cargaHoraria = _cargaHoraria,
                descricao = _descricao,
                publicoAlvo = _publicoAlvo,
                valor = _valor,
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
}
