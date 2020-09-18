using System.Collections.Generic;

namespace LOGIC.Services.Interfaces
{
    public interface IGenericCrudService<TEntityResource, TEntityRequest>
    {
        TEntityResource GetById(int id);
        List<TEntityResource> GetAll();
        
        bool Create(TEntityRequest entity);
        bool Update(int id, TEntityRequest entity);
        bool Delete(int id);
    }
}