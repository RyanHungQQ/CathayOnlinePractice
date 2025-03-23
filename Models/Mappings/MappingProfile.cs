using AutoMapper;
using Models.CathayOnlinePractice.Response;
using Models.Entities;

namespace Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 設置所有的映射規則
            CreateMap<Currency, CurrencyResponseDto>();
        }
    }
}
