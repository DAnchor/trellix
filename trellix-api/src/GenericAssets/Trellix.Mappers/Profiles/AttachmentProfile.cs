namespace Trellix.Mappers.Profiles.User;

using AutoMapper;
using Trellix.Core.Models;
using Trellix.Dtos.Payload.Attachment;

public class AttachmentProfile : Profile
{
    public AttachmentProfile()
    {
        CreateMap<AttachmentDto, AttachmentModel>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
        
        CreateMap<AttachmentModel, AttachmentBasicDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
            .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn));
    }
}