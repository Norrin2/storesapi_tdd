using StoresApi.Models;

namespace StoresApi.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private List<Store> _stores = new List<Store>();
        private int _currentIdAutoIncrement = 0;

        public Store Add(Store store)
        {
            _currentIdAutoIncrement++;
            store.Id = _currentIdAutoIncrement;
            _stores.Add(store);
            return store;
        }

        public bool Delete(int id, int companyId)
        {
            if (FindById(id, companyId) == null) return false;

            _stores = _stores.Where(s => !(s.Id == id && s.CompanyId == companyId)).ToList();
            return true;
        }

        public Store? Update(Store store)
        {
            var existingStore = _stores.FirstOrDefault(s => s.Id == store.Id && s.CompanyId == store.CompanyId);

            if (existingStore != null)
            {
                int index = _stores.FindIndex(s => s.Id == store.Id);
                if (index != -1)
                {
                    _stores[index] = store;
                    return _stores[index];
                }
            }

            return null;
        }

        public Store? FindById(int id, int companyId)
        {
            return _stores.FirstOrDefault(s => s.Id == id && s.CompanyId == companyId);
        }
    }
}
