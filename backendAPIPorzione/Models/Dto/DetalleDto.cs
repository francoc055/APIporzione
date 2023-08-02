namespace backendAPIPorzione.Models.Dto
{
    public class DetalleDto
    {
        public int IdMenu  { get; set; }
        public List<int> IdProductos { get; set; } = new List<int>();
    }
}
