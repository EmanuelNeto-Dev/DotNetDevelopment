using CursoOnline.Dominio._Curso;

namespace CursoOnline.Teste.Dominio._Builders
{
    class CursoBuilder
    {
        private string nome;
        private int cargaHoraria;
        private string descricao;
        private PublicoAlvo publicoAlvo;
        private double valor;

        public static CursoBuilder NovoCurso()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome) 
        {
            this.nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao) 
        {
            this.descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(int cargaHoraria)
        {
            this.cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            this.publicoAlvo = publicoAlvo;
            return this;
        }
        
        public CursoBuilder ComValor(double valor)
        {
            this.valor = valor;
            return this;
        }

        public Curso Build() => new Curso(nome, cargaHoraria, descricao, publicoAlvo, valor);
    }
}
