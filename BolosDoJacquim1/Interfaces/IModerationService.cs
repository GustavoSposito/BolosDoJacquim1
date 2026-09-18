using System.Threading.Tasks;

namespace BolosDoJacquin.Interfaces
{
    public interface IModerationService
    {
        Task<bool> ModerarTexto(string texto);
    }
}