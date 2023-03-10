
using Domain.Cloth;
using Infrastructure.IRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.Service.TypeClothService
{
    public class TypeClothesService : ITypeClothesService
    {
        readonly ITypeClothesRepository typeClothesRepository;
        public TypeClothesService(ITypeClothesRepository _typeClothesRepository)
        {
            this.typeClothesRepository = _typeClothesRepository;
        }
        public void Add(string name, int limit)
        {
            TypeClothes typeClothes = new TypeClothes(name, limit);
            typeClothesRepository.Add(typeClothes);
        }

        public void Delete(int id)
        {
            TypeClothes clothe = typeClothesRepository.GetById(id);
            if (clothe == null)
            {
                throw new Exception($"The entity with ID {id} was not found.");
            }
            typeClothesRepository.Delete(clothe);
        }
        public void Update(int id, string name, int limit)
        {
            TypeClothes clothe = typeClothesRepository.GetById(id);
            if (clothe == null)
            {
                throw new Exception($"The entity with ID {id} was not found.");
            }
            TypeClothes typeClothes = new TypeClothes(name, limit);
            typeClothesRepository.Update(id, typeClothes);
        }
        /*
        public IEnumerable<TypeClothes> GetList(string? key, int? pageSize, int? page)
        {
            return typeClothesRepository.GetList(key, pageSize ?? int.MaxValue, page ?? 1);
        }*/
        public IEnumerable<TypeClothes> GetList(string? key, int? pageSize, int? page)
        {
            Expression<Func<TypeClothes, bool>> filter = null;
            if (key!=null)
                filter = e => e.Name.ToUpper().Contains(key.ToUpper());
            return typeClothesRepository.GetFilter(filter, pageSize ?? int.MaxValue, page ?? 1);
        }

        public TypeClothes GetById(int id)
        {
            return typeClothesRepository.GetById(id);
        }

        public IEnumerable<TypeClothes> GetLimitGreater(int? limit,int? pageSize, int? page)
        {
            Expression<Func<TypeClothes, bool>> filter = p => p.Limit >= (limit??0);
            return typeClothesRepository.GetFilter(filter, pageSize ?? int.MaxValue, page ?? 1);
        }

        public IEnumerable<TypeClothes> GetLimitLess(int? limit, int? pageSize, int? page)
        {
            Expression<Func<TypeClothes, bool>> filter = p => p.Limit <= (limit ?? -1);
            return typeClothesRepository.GetFilter(filter, pageSize ?? int.MaxValue, page ?? 1);
        }
    }
}
