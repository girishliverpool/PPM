using PPM.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM.DataLayer.Interfaces
{
    public interface IPPMTransactionService
    {
        bool BulkInsert(List<TransactionDetail> lstTransactions);
        List<TransactionDetail> GetAllTransaction();
        bool UpdateTransaction(TransactionDetail Transaction);
        bool DeleteTransaction(Guid TransactionID);
    }
}
