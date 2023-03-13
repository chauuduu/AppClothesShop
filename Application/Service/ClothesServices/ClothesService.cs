﻿
using Application.Service.OriginsService;
using Application.Service.TypeClothService;
using Domain.Cloth;
using Infrastructure.IRepository;
using Infrastructure.Repository;
using System.Drawing.Printing;
using System.Linq.Expressions;

namespace Application.Service.ClothService
{
    public class ClothesService : IClothesService
    {
        readonly IBaseRepository<Clothes> clothesRepository;
        readonly IOriginService origin;
        readonly ITypeClothesService typeClothes;

        public ClothesService(IBaseRepository<Clothes> clothesRepository, IOriginService origin, ITypeClothesService typeClothes)
        {
            this.clothesRepository = clothesRepository;
            this.origin = origin;
            this.typeClothes = typeClothes;
        }

        public void Add(string name, string description, Size size, decimal price, int rentalPrice, int typeClothesId, int originId, Status status)
        {
            if (origin.GetById(originId) == null)
            {
                throw new Exception($"The specified foreign key Origin with ID {originId} was not found.");
            }
            if (typeClothes.GetById(typeClothesId) == null)
            {
                throw new Exception($"The specified foreign key TypeClothes with ID {typeClothesId} was not found.");
            }
            Clothes cloth = new Clothes(name, description, size, price, rentalPrice, typeClothesId, originId, status);
            clothesRepository.Add(cloth);
        }

        public void Delete(int id)
        {
            var clothe = this.GetById(id);
            if (clothe == null)
            {
                throw new Exception($"The entity with ID {id} was not found.");
            }
            clothesRepository.Delete(clothe);
        }

        public Clothes GetById(int id)
        {
            Expression<Func<Clothes, bool>> filter = p => p.Id == id;
            return clothesRepository.Get(null,filter, 1, 1).data.FirstOrDefault();
        }

        public (IEnumerable<Clothes> data, int total) GetList(string? key, int? pageSize, int? page)
        {
            Expression<Func<Clothes, bool>> filter = null;
            if (key != null)
                filter = e => e.Name.ToUpper().Contains(key.ToUpper());
            return clothesRepository.Get(null, filter, pageSize ?? int.MaxValue, page ?? 1);
        }

       
        public void Update(int id, string name, string description, Size size, decimal price, int rentalPrice, int typeClothesId, int originId, Status status)
        {
            var ClothesBefore = this.GetById(id);
            if (ClothesBefore == null)
            {
                throw new Exception($"The entity with ID {id} was not found.");
            }
            if (origin.GetById(originId) == null)
            {
                throw new Exception($"The specified foreign key Origin with ID {originId} was not found.");
            }
            if (typeClothes.GetById(typeClothesId) == null)
            {
                throw new Exception($"The specified foreign key TypeClothes with ID {typeClothesId} was not found.");
            }
           Clothes cloth = new Clothes(name, description, size, price, rentalPrice, typeClothesId, originId, status);
           cloth.SetId(id);
           clothesRepository.Update(id, cloth);
        }
    }
}
