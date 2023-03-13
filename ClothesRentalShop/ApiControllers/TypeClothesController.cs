

using Application.Service.ClothService;
using Application.Service.TypeClothService;
using ClothesRentalShop.Mapper;
using ClothesRentalShop.ViewModel;
using Domain.Cloth;
using Microsoft.AspNetCore.Mvc;

namespace ClothesRentalShop.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeClothesController : ControllerBase
    {
        readonly ITypeClothesService typeClothesService;
        readonly MappingService<TypeClothes, TypeClothesViewModel> mapper;

        public TypeClothesController(ITypeClothesService typeClothesService)
        {
            this.typeClothesService = typeClothesService;
            this.mapper = new MappingService<TypeClothes, TypeClothesViewModel> ();
        }

        [HttpGet]
        public IActionResult Get(string? key,int? pageSize,int? page)
        {
            try
            {
                var rs = typeClothesService.GetList(key, pageSize, page);
                return Ok(new PagingResponse()
                {
                    Count = rs.data.Count(),
                    TotalCount = rs.total,
                    Data = mapper.Map(rs.data)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("limitgreater")]
        public IActionResult GetThan(int? limit,int? pageSize, int? page)
        {
            try
            {
                var rs = typeClothesService.GetLimitGreater(limit,pageSize, page);
                return Ok(new PagingResponse()
                {
                    Count = rs.data.Count(),
                    TotalCount = rs.total,
                    Data = mapper.Map(rs.data)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("limitless")]
        public IActionResult GetLess(int? limit, int? pageSize, int? page)
        {
            try
            {
                var rs = typeClothesService.GetLimitLess(limit, pageSize, page);
                return Ok(new PagingResponse()
                {
                    Count = rs.data.Count(),
                    TotalCount = rs.total,
                    Data = mapper.Map(rs.data)
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            try
            {
                var rs = typeClothesService.GetById(id);
                if (rs == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map(rs));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public IActionResult Insert(TypeClothesViewModel type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    typeClothesService.Add(type.Name, type.Limit);
                    return Ok();
                }
                return UnprocessableEntity(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, TypeClothesViewModel type)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    typeClothesService.Update(id,type.Name, type.Limit);
                    return Ok();
                }
                return UnprocessableEntity(ModelState);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                typeClothesService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}


