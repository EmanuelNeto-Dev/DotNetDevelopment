using CursoOnline.Dominio._Curso;

namespace CursoOnline.Dominio._Curso
{
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
    }
}
