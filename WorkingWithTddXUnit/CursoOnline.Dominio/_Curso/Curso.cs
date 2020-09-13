using System;

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

namespace CursoOnline.Dominio._Curso
{
    public class Curso
    {
        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public string Descricao { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

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
