namespace backendAPIPorzione.Models.Dto
{
    public class MenuDto
    {
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int IdUsuario { get; set; }

        public List<UpdateProductoDto> Productos { get; set; }
    }
}
