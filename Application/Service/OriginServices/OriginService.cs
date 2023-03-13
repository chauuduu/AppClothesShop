using Domain.Cloth;
using Infrastructure.IRepository;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace Application.Service.OriginsService
{
    public class OriginService : IOriginService
    {
        readonly IBaseRepository<Origin> originRepository;

        public OriginService(IBaseRepository<Origin> _originRepository)
        {
            this.originRepository = _originRepository;
        }

        public void Add(string name,string address)
        {
            Origin Origin = new Origin(name,address);
            originRepository.Add(Origin);
        }

        public void Delete(int id)
        {
            Origin clothe = this.GetById(id);
            if (clothe == null)
            {
                throw new Exception($"The entity with ID {id} was not found.");
            }
            originRepository.Delete(clothe);
        }
        public void Update(int id, string name, string address)
        {
            Origin clothe = this.GetById(id);
            if (clothe == null)
            {
                throw new Exception($"The entity with ID {id} was not found.");
            }
            Origin Origin = new Origin(name,address);
            Origin.SetId(id);   
            originRepository.Update(id,Origin);
        }
        public (IEnumerable<Origin> data, int total) GetList(string? key, int? pageSize, int? page)
        {
            Expression<Func<Origin, bool>> filter = null;
            if (key != null)
                filter = e => e.Name.ToUpper().Contains(key.ToUpper());
            return originRepository.Get("Clothes", filter, pageSize ?? int.MaxValue, page ?? 1);
        }

        public Origin GetById(int id)
        {
            Expression<Func<Origin, bool>> filter = p => p.Id == id;
            return originRepository.Get("Clothes", filter, 1, 1).data.FirstOrDefault();
        }

    }
}

