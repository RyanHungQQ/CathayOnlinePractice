using AutoMapper;

namespace Models.Mappings
{
    public class Mapper<TSource, TDestination>
    {
        protected readonly IMapper _mapper;

        public Mapper()
        {
            // 創建 AutoMapper 配置，定義映射規則
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();  // 自動映射 TSource 到 TDestination
            });

            _mapper = config.CreateMapper();
        }

        // 直接使用 this（當前物件）進行映射
        public TDestination ToEntity()
        {
            return _mapper.Map<TDestination>(this);  // 使用 this 進行映射
        }
    }
}
