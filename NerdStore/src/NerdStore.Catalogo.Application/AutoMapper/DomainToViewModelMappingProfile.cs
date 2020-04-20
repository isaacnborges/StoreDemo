using AutoMapper;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain.Entities;

namespace NerdStore.Catalogo.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(pvw => pvw.Largura, x => x.MapFrom(p => p.Dimensoes.Largura))
                .ForMember(pvw => pvw.Altura, x => x.MapFrom(p => p.Dimensoes.Altura))
                .ForMember(pvw => pvw.Profundidade, x => x.MapFrom(p => p.Dimensoes.Profundidade));

            CreateMap<Categoria, CategoriaViewModel>();
        }
    }
}
