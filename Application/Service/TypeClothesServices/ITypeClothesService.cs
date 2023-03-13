using Domain.Cloth;

namespace Application.Service.TypeClothService
{
    public interface ITypeClothesService
    {
        (IEnumerable<TypeClothes> data, int total) GetList(string? key, int? pageSize,int? page);
        (IEnumerable<TypeClothes> data, int total) GetLimitGreater(int? limit,int? pageSize, int? page);
        (IEnumerable<TypeClothes> data, int total) GetLimitLess(int? limit, int? pageSize, int? page);
        TypeClothes GetById(int id);
        void Add(string name,int limit);
        void Update(int id, string name, int limit);
        void Delete(int id);
    }
}
