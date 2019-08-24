using System.Threading.Tasks;

namespace ProcessingContracts
{
    public interface IProcessing
    {
        Task Process(int totalLastProcessingCount);
    }
}