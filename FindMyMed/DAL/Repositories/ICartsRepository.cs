using FindMyMed.DTO;

namespace FindMyMed.DAL
{
    public interface ICartsRepository
    {
        UpdateCartDTO SaveCart(UpdateCartDTO cartDTO);
    }
}
