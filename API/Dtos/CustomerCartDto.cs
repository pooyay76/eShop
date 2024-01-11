namespace API.Dtos
{
    public class CustomerCartDto
    {
        public string Id { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
