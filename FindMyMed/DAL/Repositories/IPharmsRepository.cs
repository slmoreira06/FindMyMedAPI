using FindMyMed.DTO;
using FindMyMed.Models;

namespace FindMyMed.DAL
{
    public interface IPharmsRepository
    {
        Pharmacy GetPharmById(int id);
        IEnumerable<Pharmacy> GetPharms();
        UpdatePharmDTO UpdatePharmProfile(int id, UpdatePharmDTO pharmDTO);

    }
}
