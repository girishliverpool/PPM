using PPM.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM.ServiceInterfaces
{
    public interface ITransactionService
    {
        bool BulkInsert(List<TransactionDetail> lstTransactions);
        bool UpdateTransaction(TransactionDetail Transaction);
        bool DeleteTransaction(Guid TransactionID);
        List<TransactionDetail> GetAllTransactions();
    }
}
