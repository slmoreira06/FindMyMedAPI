using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface ISupportsRepository
    {
        bool CreateSupport(Support support);
        Support GetSupportById(int id);
        IEnumerable<Support> GetSupports();
    }
}
