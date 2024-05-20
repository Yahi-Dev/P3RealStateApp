using AutoMapper;
using RealStateApp.Core.Application.Dtos.Account;
using RealStateApp.Core.Application.Dtos.Domain_Dtos;
using RealStateApp.Core.Application.Features.ClientFavoriteProperty.Commands.CreateClientFavoriteProperty;
using RealStateApp.Core.Application.Features.ClientFavoriteProperty.Commands.UpdateClientFavoriteProperty;
using RealStateApp.Core.Application.Features.Improvement.Commands.CreateImprovement;
using RealStateApp.Core.Application.Features.Improvement.Commands.UpdateImprovement;
using RealStateApp.Core.Application.Features.Property.Commands.CreateProperty;
using RealStateApp.Core.Application.Features.Property.Commands.UpdateProperty;
using RealStateApp.Core.Application.Features.PropertyType.Commands.CreatePropertyType;
using RealStateApp.Core.Application.Features.PropertyType.Commands.UpdatePropertyType;
using RealStateApp.Core.Application.Features.SaleCategory.Commands.CreateSaleCategory;
using RealStateApp.Core.Application.Features.SaleCategory.Commands.UpdateSaleCategory;
using RealStateApp.Core.Application.ViewModels.Domain.ClientFavoriteProperty;
using RealStateApp.Core.Application.ViewModels.Domain.Improvement;
using RealStateApp.Core.Application.ViewModels.Domain.Property;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImage;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyImprovement;
using RealStateApp.Core.Application.ViewModels.Domain.PropertyType;
using RealStateApp.Core.Application.ViewModels.Domain.SaleCategory;
using RealStateApp.Core.Application.ViewModels.Identity.Admin;
using RealStateApp.Core.Application.ViewModels.Identity.Users;
using RealStateApp.Core.Application.Wrappers;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region ClientFavoriteProperty
            CreateMap<Domain.Entities.ClientFavoriteProperty, BaseClientFavoritePropertyViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedById, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.DeletedById, opt => opt.Ignore())
                .ForMember(x => x.DeletedDate, opt => opt.Ignore());

            CreateMap<Domain.Entities.ClientFavoriteProperty, SaveClientFavoritePropertyViewModel>()
                .ReverseMap();


            #region ClientFavoriteProperty CQRS
            CreateMap<ClientFavoriteProperty, CreateClientFavoritePropertyCommand>()
                .ReverseMap();


            CreateMap<ClientFavoriteProperty, UpdateClientFavoritePropertyCommand>()
                .ReverseMap();


            CreateMap<ClientFavoriteProperty, ClienteFavoritePropertyUpdateResponse>()
                .ReverseMap();
            #endregion

            #endregion


            #region Improvement
            CreateMap<Improvement, BaseImprovementViewModel>()
                    .ReverseMap()
                    .ForMember(x => x.CreatedById, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedById, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                    .ForMember(x => x.DeletedById, opt => opt.Ignore())
                    .ForMember(x => x.DeletedDate, opt => opt.Ignore());


            CreateMap<Improvement, SaveImprovementViewModel>()
                    .ReverseMap()
                    .ForMember(x => x.CreatedById, opt => opt.Ignore())
                    .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedById, opt => opt.Ignore())
                    .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                    .ForMember(x => x.DeletedById, opt => opt.Ignore())
                    .ForMember(x => x.DeletedDate, opt => opt.Ignore());


            #region ImprovementDTO
            CreateMap<Improvement, BaseImprovementDto>()
                .ReverseMap();


            CreateMap<Improvement, SaveImprovementDto>()
                .ReverseMap();
            #endregion


            #region Improvement CQRS
            CreateMap<Improvement, CreateImprovementCommand>()
                .ReverseMap();


            CreateMap<Improvement, UpdateImprovementCommand>()
                .ReverseMap();


            CreateMap<Improvement, UpdateImprovementResponse>()
                .ReverseMap();
            #endregion

            #endregion


            #region Property

            CreateMap<Property, SavePropertyViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedById, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.DeletedById, opt => opt.Ignore())
                .ForMember(x => x.DeletedDate, opt => opt.Ignore());

            CreateMap<Property, BasePropertyViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedById, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.DeletedById, opt => opt.Ignore())
                .ForMember(x => x.DeletedDate, opt => opt.Ignore());


            #region PropertyDTO
            CreateMap<Property, BasePropertyDto>()
                .ReverseMap();


            CreateMap<Property, SavePropertyDto>()
                .ReverseMap();
            #endregion


            #region Property CQRS
            CreateMap<Property, CreatePropertyCommand>()
                .ReverseMap();


            CreateMap<Property, UpdatePropertyCommand>()
                .ReverseMap();


            CreateMap<Property, UpdatePropertyResponse>()
                .ReverseMap();
            #endregion

            #endregion


            #region PropertyType
            CreateMap<PropertyType, SavePropertyTypeViewModel>()
                .ReverseMap();


            CreateMap<PropertyType, BasePropertyTypeViewModel>()
                .ReverseMap();

            #region PropertyTypeDTO
            CreateMap<PropertyType, BasePropertyTypeDto>()
                .ReverseMap();


            CreateMap<PropertyType, SavePropertyTypeDto>()
                .ReverseMap();
            #endregion


            #region PropertyType CQRS
            CreateMap<PropertyType, CreatePropertyTypeCommand>()
                .ReverseMap();


            CreateMap<PropertyType, UpdatePropertyTypeCommand>()
                .ReverseMap();


            CreateMap<PropertyType, UpdatePropertyTypeResponse>()
                .ReverseMap();
            #endregion

            #endregion


            #region SaleCategory
            CreateMap<SaleCategory, BaseSaleCategoryViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedById, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.DeletedById, opt => opt.Ignore())
                .ForMember(x => x.DeletedDate, opt => opt.Ignore());

            CreateMap<SaleCategory, SaveSaleCategoryViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedById, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedById, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.DeletedById, opt => opt.Ignore())
                .ForMember(x => x.DeletedDate, opt => opt.Ignore());

            #region SaleCategoryDTO
            CreateMap<SaleCategory, BaseSaleCategoryDto>()
                .ReverseMap();


            CreateMap<SaleCategory, SaveSaleCategoryDto>()
                .ReverseMap();
            #endregion


            #region SaleCategory CQRS
            CreateMap<SaleCategory, CreateSaleCategoryCommand>()
                .ReverseMap();


            CreateMap<SaleCategory, UpdateSaleCategoryCommand>()
                .ReverseMap();


            CreateMap<SaleCategory, UpdateSaleCategoryResponse>()
                .ReverseMap();
            #endregion

            #endregion


            #region Identity
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ReverseMap();

            CreateMap<DeveloperViewModel, BaseUserViewModel>()
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ReverseMap();

            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ReverseMap();

            CreateMap<DtoAccounts, BaseUserViewModel>()
                .ReverseMap();

            CreateMap<DtoAccounts, RegisterRequest>()
                .ReverseMap();

            CreateMap<BaseUserViewModel, SaveUserViewModel>()
                .ReverseMap();

            CreateMap<Response<BaseUserViewModel>, SaveUserViewModel>()
                .ReverseMap();


            CreateMap<DtoAccounts, SaveUserViewModel>()
                .ReverseMap();


            CreateMap<SaveAdminViewModel, SaveUserViewModel>()
               .ReverseMap();
            #endregion


            CreateMap<BasePropertyImageViewModel, PropertyImage>()
                .ReverseMap();


            CreateMap<SavePropertyImprovementViewModel, PropertyImprovement>()
               .ReverseMap();


            CreateMap<Property, PropertyInfoViewModel>()
               .ReverseMap();


            CreateMap<PropertyImprovement, BasePropertyImprovementViewModel>()
               .ReverseMap();


            CreateMap<Property, BasePropertyImprovementViewModel>()
              .ReverseMap();

            CreateMap<PropertyImprovement, BaseImprovementViewModel>()
              .ReverseMap();


            CreateMap<SavePropertyImageViewModel, PropertyImage>()
               .ReverseMap();


            CreateMap<BasePropertyImageViewModel, PropertyImage>()
              .ReverseMap();

        }
    }
}
