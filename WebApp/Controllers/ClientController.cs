using DoMAin.DTOs;
using DoMAin.Filters;
using Infrastructure.ApiResponce;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class ClientController(IClientService _service)
{
    [HttpGet("GetClients")]
    public async Task<Responce<List<ReadClientDTO>>> RedaClients([FromQuery]ClienFilter filter)
        => await _service.RedaClients(filter);
    [HttpGet("GetClient/{id}")]
    public async Task<Responce<ReadClientDTO>> ReadClient(int Id)
        => await _service.ReadClient(Id);
    [HttpPost("CreateClient")]
    public async Task<Responce<string>> AddClient(CreateClientDTO dto)
        => await _service.AddClient(dto);
    [HttpPut("UpdateClient")] 
    public async Task<Responce<string>> UpdateClient(UpdateClientDTO dto)
        => await _service.UpdateClient(dto);
    [HttpDelete("DeleteClient/{id}")]
    public async Task<Responce<string>> DeleteClient(int Id)
        => await _service.DeleteClient(Id);
}