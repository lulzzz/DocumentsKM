using DocumentsKM.Dtos;
using DocumentsKM.Models;

namespace DocumentsKM.Profiles
{
    public class AttachedDocsProfile : AutoMapper.Profile
    {
        public AttachedDocsProfile()
        {
            CreateMap<AttachedDoc, AttachedDocResponse>();
            CreateMap<AttachedDocCreateRequest, AttachedDoc>();
        }
    }
}
