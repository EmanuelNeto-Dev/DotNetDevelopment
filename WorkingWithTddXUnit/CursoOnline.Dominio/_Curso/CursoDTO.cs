namespace CursoOnline.Dominio._Curso
{
    public class CursoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }

        public CursoDTO()
        {
        }
    }
}
