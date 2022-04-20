using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface ICartsRepository
    {
        UpdateCartDTO SaveCart(UpdateCartDTO cartDTO);
    }
}
