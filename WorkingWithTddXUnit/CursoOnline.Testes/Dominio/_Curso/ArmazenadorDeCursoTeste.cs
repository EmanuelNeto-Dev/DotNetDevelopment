using Bogus;
using CursoOnline.Dominio._Curso;
using CursoOnline.Teste.Dominio._Builders;
using CursoOnline.Teste.Dominio._Extensions;
using Moq;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Teste.Dominio._Curso
{
    public class ArmazenadorDeCursoTeste
    {
        private readonly ITestOutputHelper _output;
        private readonly CursoDTO _cursoDTO;

        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        
        public ArmazenadorDeCursoTeste(ITestOutputHelper output)
        {
            var faker = new Faker();
            
            _output = output;
            _output.WriteLine("Construtor 'ArmazenadorDeCursoTeste' sendo executado!");

            _cursoDTO = new CursoDTO
            {
                Nome = faker.Random.Word(),
                CargaHoraria = faker.Random.Int(50, 1000),
                Descricao = faker.Lorem.Paragraph(),
                PublicoAlvo = "Estudante",
                Valor = (double)faker.Finance.Amount()
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarUmCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDTO);
            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome.Equals(_cursoDTO.Nome) &&
                    c.Descricao.Equals(_cursoDTO.Descricao)
                )
            ), Times.Once); // Verifica se o método analisado pelo Verify() foi chamado de acordo com a propriedade Times. Neste caso, ao menos 1 vez.
        }

        [Theory]
        [InlineData("")]
        [InlineData("Médico")]
        public void NaoDeveAdicionarUmCursoComPublicoAlvoInvalidoOuVazio(string publicoAlvo)
        {
            _cursoDTO.PublicoAlvo = publicoAlvo;
            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
                .ComMensagem("Público alvo inválido!");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutro()
        {
            var cursoJaSalvo = CursoBuilder
                .NovoCurso()
                .ComNome(_cursoDTO.Nome)
                .ComCargaHoraria(_cursoDTO.CargaHoraria)
                .ComValor(_cursoDTO.Valor)
                .Build();

            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDTO.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDTO))
                .ComMensagem("Nome do curso já consta no banco de dados!");
        }
    }
}
