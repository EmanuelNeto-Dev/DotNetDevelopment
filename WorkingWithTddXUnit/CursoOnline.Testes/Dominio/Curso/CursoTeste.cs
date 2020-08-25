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
                cargaHoraria = (double) 80,
                publicoAlvo = PublicoAlvo.Estudante,
                valor = (double) 950,
            };
            
            //Act
            Curso curso = new Curso(cursoEsperado.nome, cursoEsperado.cargaHoraria, cursoEsperado.publicoAlvo, cursoEsperado.valor);

            //Assert
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
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
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

    }
}
