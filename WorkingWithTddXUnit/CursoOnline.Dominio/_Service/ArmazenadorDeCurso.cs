using CursoOnline.Dominio._Curso;
using System;

namespace CursoOnline.Dominio._Curso
{
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRespositorio)
        {
            _cursoRepositorio = cursoRespositorio;
        }

        public void Armazenar(CursoDTO cursoDTO)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDTO.Nome);

            if (cursoJaSalvo != null)
                throw new ArgumentException("Nome do curso já consta no banco de dados!");

            if (!Enum.TryParse<PublicoAlvo>(cursoDTO.PublicoAlvo, out var publicoAlvo))
                throw new ArgumentException("Público alvo inválido!");

            var curso 
                = new Curso(cursoDTO.Nome, cursoDTO.CargaHoraria, cursoDTO.Descricao, publicoAlvo, cursoDTO.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }
}
