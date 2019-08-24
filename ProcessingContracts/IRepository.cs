using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessingContracts
{
    public interface IRepository
    {
        Task<List<int>> GetBlockNumbers();
        Task AddBlock<T>(T block);
        Task SaveTransaction<T>(T transaction);
        Task SetBlockToFinishProcessing(string blockhash);

        Task<List<string>> GetTransactionsIds(string blockhash);
    }
}