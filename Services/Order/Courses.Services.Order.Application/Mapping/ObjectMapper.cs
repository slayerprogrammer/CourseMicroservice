using AutoMapper;

namespace Courses.Services.Order.Application.Mapping
{
    public static class ObjectMapper
    {
        //çağrıldığında
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
              {
                  var config = new MapperConfiguration(cfg =>
                  {
                      cfg.AddProfile<CustomMapping>();
                  });
                  return config.CreateMapper();
              });

        public static IMapper Mapper => lazy.Value;
    }
}