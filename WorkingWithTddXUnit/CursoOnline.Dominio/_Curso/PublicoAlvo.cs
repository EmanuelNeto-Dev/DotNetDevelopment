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
    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }
}
