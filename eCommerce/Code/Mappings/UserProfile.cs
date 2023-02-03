using AutoMapper;
using eCommerce.DataAccess;
using eCommerce.Models.UserAccountVM;

namespace eCommerce.Code.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterVM, UserT>()
                .ForMember(u => u.PasswordHash, s => s.MapFrom(r => r.Password));
        }
    }
}
