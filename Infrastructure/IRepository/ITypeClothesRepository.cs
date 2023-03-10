using Domain.Cloth;
using System.Linq.Expressions;

namespace Infrastructure.IRepository
{
    public interface ITypeClothesRepository
    {
        IEnumerable<TypeClothes> GetList(string? key,int? pageSize, int? page);
        IEnumerable<TypeClothes> GetFilter(Expression<Func<TypeClothes, bool>>? filter, int? pageSize, int? page);
        TypeClothes GetById(int id);
        void Add(TypeClothes typeclothes);
        void Update(int id, TypeClothes typeClothes);
        void Delete(TypeClothes typeclothes);
    }
}
