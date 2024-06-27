using Microsoft.AspNetCore.Http.HttpResults;
using MinimalAPIERP.Dtos;

namespace MinimalAPIERP.Servicios
{
    public interface IRaincheckService
    {
        Task<Results<Ok, NotFound, BadRequest>> UpdateRaincheckAsync(Guid guid, RaincheckDto raincheckDto);
        Task<RaincheckDto> CreateRaincheckAsync(RaincheckCreateDto raincheckDto);
    }
}