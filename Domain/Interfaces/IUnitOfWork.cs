using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Core.Interfaces;

public interface IUnityOfWork
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
    Task<int> SaveAsync();
}
