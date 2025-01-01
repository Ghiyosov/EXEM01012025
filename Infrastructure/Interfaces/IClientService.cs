using DoMAin.DTOs;
using DoMAin.Filters;
using Infrastructure.ApiResponce;

namespace Infrastructure.Interfaces;

public interface IClientService
{
    public Task<Responce<List<ReadClientDTO>>> RedaClients(ClienFilter filter);
    
    public Task<Responce<ReadClientDTO>> ReadClient(int Id);
    
    public Task<Responce<string>> AddClient(CreateClientDTO dto);
    
    public Task<Responce<string>> UpdateClient(UpdateClientDTO dto);
    
    public Task<Responce<string>> DeleteClient(int Id);
}