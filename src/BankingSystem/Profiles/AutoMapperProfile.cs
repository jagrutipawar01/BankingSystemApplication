using AutoMapper;
using BankingSystem.Models;

namespace BankingSystem.Profiles
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile() {

      CreateMap<CreateAccountModel, Account>();
    }
  }
}
