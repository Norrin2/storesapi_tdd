using StoresApi.Models;

namespace StoresApi.Repositories
{
    public interface IStoreRepository
    {
        Store Add(Store store);
        bool Delete(int id, int companyId);
        Store? Update(Store store);
        Store? FindById(int id, int companyId);
    }
}