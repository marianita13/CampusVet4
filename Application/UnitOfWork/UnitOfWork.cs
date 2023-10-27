using System;
using Application.Repositories;
using Application.Repository;
using Domain.entities;
using Domain.Interfaces;
using Persistence;
using Persistence.Data;

namespace Application.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    public UnitOfWork(CampusVet4Context context)
    {
        _context = context;
    }
    private readonly CampusVet4Context _context;
    private IAppointment _Appointments;
    private ICity _Cities;
    private IClient _Clients;
    private IClientAddress _ClientAddresses;
    private IBreed _Breeds;
    private IClientPhone _ClientPhones;
    private ICountry _Countries;
    private IPet _Pets;
    private IService _Services;
    private IState _States;
    private IRole _Roles;

    public IAppointment Appointments{
            get{
                if (_Appointments == null){
                    _Appointments = new AppointmentRepository(_context);
                }
                return _Appointments;
            }
        }

    public IBreed Breeds{
            get{
                if(_Breeds == null){
                    _Breeds = new BreedRepository(_context);
                }
                return _Breeds;
            }
        }

    public ICity Cities{
            get{
                if (_Cities == null){
                    _Cities = new CityRepository(_context);
                }
                return _Cities;
            }
        }

    public IClientAddress ClientAddresses {
            get{
                if(_ClientAddresses == null){
                    _ClientAddresses = new ClientAddressRepository(_context);
                }
                return _ClientAddresses;
            }
        }

    public IClientPhone ClientPhones{
            get{
                if(_ClientPhones == null){
                    _ClientPhones = new ClientPhoneRepository(_context);
                }
                return _ClientPhones;
            }
        }

    public ICountry Countries{
            get{
                if(_Countries == null){
                    _Countries = new CountryRepository(_context);
                }
                return _Countries;
            }
        }

    public IPet Pets{
            get{
                if(_Pets == null){
                    _Pets = new PetRepository(_context);
                }
                return _Pets;
            }
        }

    public IService Services{
            get{
                if(_Services == null){
                    _Services = new ServiceRepository(_context);
                }
                return _Services;
            }
        }

    public IState States{
            get{
                if(_States == null){
                    _States = new StateRepository(_context);
                }
                return _States;
            }
        }

    public IRole Roles {
            get{
                if(_Roles == null){
                    _Roles = new RoleRepository(_context);
                }
                return _Roles;
            }
        }

    public IClient Clients{
            get{
                if(_Clients == null){
                    _Clients = new ClientRepository(_context);
                }
                return _Clients;
            }
        }


    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}