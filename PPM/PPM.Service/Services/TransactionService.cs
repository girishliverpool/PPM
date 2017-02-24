using PPM.DataLayer.Interfaces;
using PPM.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM.Service.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IPPMTransactionService transactionDAL = null;
        public TransactionService(IPPMTransactionService transactionDAL)
        {
            this.transactionDAL = transactionDAL;
        }

        public bool BulkInsert(List<Dto.TransactionDetail> lstTransactions)
        {
            bool retval = false;

            try
            {
                retval = this.transactionDAL.BulkInsert(lstTransactions);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return retval;
        }

        public bool UpdateTransaction(Dto.TransactionDetail Transaction)
        {
            bool retval = false;

            try
            {
                retval = this.transactionDAL.UpdateTransaction(Transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return retval;
        }

        public bool DeleteTransaction(Guid TransactionID)
        {
            bool retval = false;

            try
            {
                retval = this.transactionDAL.DeleteTransaction(TransactionID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return retval;
        }


        public List<Dto.TransactionDetail> GetAllTransactions()
        {
            List<Dto.TransactionDetail> lst = new List<Dto.TransactionDetail>();

            try
            {
                lst = this.transactionDAL.GetAllTransaction();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return lst;
        }
    }
}
