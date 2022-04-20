using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IProductsRepository
    {
        bool CreateProduct(Product product);
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts();
        UpdateProductDTO UpdateProduct(int id, UpdateProductDTO productDTO);
    }
}
