using AutoMapper;
namespace BeerWebAPI.Service.Mapper
{
    /// <summary>
    /// This is generic mapper class used for mapping the objects
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public static class MapperObject<TSource, TDestination>
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.ShouldMapProperty = property => property.GetMethod.IsPublic || property.GetMethod.IsAssembly || property.GetMethod.IsGenericMethodDefinition;
                config.AddProfile<MappingProfile<TSource, TDestination>>();
            });
            var mapper = configuration.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
    public class MappingProfile<TSource, TDestination> : Profile
    {
        public MappingProfile()
        {
            CreateMap<TSource, TDestination>().ReverseMap();
        }
    }
}