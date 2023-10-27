
namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IAppointment Appointments { get; }
    IBreed Breeds { get; }
    ICity Cities { get; }
    IClient Clients { get; }
    IClientAddress ClientAddresses { get; }
    IClientPhone ClientPhones { get; }
    ICountry Countries { get; }
    IPet Pets { get; }
    IService Services { get; }
    IState States { get; }
    IRole Roles { get; }
    Task<int> SaveAsync();
}
