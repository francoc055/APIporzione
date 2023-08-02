using backendAPIPorzione.Models.cliente_servidor;

namespace backendAPIPorzione.Services.IService
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
    }
}
