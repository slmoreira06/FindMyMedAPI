using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface ICartsRepository
    {
        Cart GetCart();
        UpdateCartDTO SaveCart(UpdateCartDTO cartDTO);
        bool ClearCart();
    }
}
