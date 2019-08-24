using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBRepository.DBModel;
using ProcessingContracts;

namespace DBRepository
{
    public class Repository : IRepository
    {
        public async Task<List<int>> GetBlockNumbers()
        {
            using (var context = new DBModel.DBModel())
            {
                var results = await context.Blocks.Where(s=>s.isAllTransactionLoadedToDb).Select(s => s.blockNumber).ToListAsync();
                return results;
            }
        }

        public async Task AddBlock<T>(T block)
        {
            using (var context = new DBModel.DBModel())
            {
                if (block is Block)
                {
                    Block block1 = block as Block;

                    var existingBlock = await context.Blocks.SingleOrDefaultAsync(s => s.hash == block1.hash);
                    if (existingBlock == null)
                    {
                        context.Blocks.Add(block1);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task SaveTransaction<T>(T transaction)
        {
            using (var context = new DBModel.DBModel())
            {
                if (transaction is TransactionDetail)
                {
                    TransactionDetail transaction1 = transaction as TransactionDetail;

                    var existingtransaction = await context.TransactionDetails.SingleOrDefaultAsync(s => s.txid == transaction1.txid);
                    if (existingtransaction == null)
                    {
                        try
                        {
                            context.TransactionDetails.Add(transaction1);
                            await context.SaveChangesAsync();
                        }
                        catch (DbEntityValidationException e)
                        {
                          
                        }
                    }
                }
            }
        }

        public async Task SetBlockToFinishProcessing(string blockhash)
        {
            using (var context = new DBModel.DBModel())
            {
                var existingBlock = await context.Blocks.SingleOrDefaultAsync(s => s.hash == blockhash);
                if (existingBlock != null)
                {
                    existingBlock.isAllTransactionLoadedToDb = true;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<string>> GetTransactionsIds(string blockhash)
        {
            using (var context = new DBModel.DBModel())
            {
                var results = await context.TransactionDetails.Where(s => s.blockhash == blockhash).Select(s => s.txid).ToListAsync();
                return results;
            }
        }
    }
}
