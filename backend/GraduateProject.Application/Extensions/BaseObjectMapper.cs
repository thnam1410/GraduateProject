using AutoMapper;

namespace GraduateProject.Application.Extensions;

public class BaseObjectMapper : IObjectMapper
{
    private readonly IMapper _mapper;

    public BaseObjectMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TSource, TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper.Map(source, destination);
    }
}