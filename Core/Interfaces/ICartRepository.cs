using Core.Entities;

namespace Core.Interfaces
{
    public interface ICartRepository
    {
        Task<CustomerCart> GetCartAsync(string id);
        Task<CustomerCart> UpdateCartAsync(CustomerCart cart);
        Task<bool> DeleteCartAsync(string id);
    }
}
