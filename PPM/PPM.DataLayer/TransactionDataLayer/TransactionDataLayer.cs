
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PPM.DataLayer.Interfaces;
using PPM.DataLayer.Model;
using PPM.Dto;

namespace PPM.DataLayer
{
    public class TransactionDataLayer : IPPMTransactionService
    {
        /// <summary>
        /// Bulk Insert Transactions
        /// </summary>
        /// <param name="lstTransactions">List of Dto.Transaction</param>
        /// <returns></returns>
        public bool BulkInsert(List<Dto.TransactionDetail> lstTransactions)
        {
            bool retval = false;

            try
            {

                List<Transaction> lst = lstTransactions.Select(x => new Transaction
                    {
                        TransactionID = Guid.NewGuid(),
                        Account = x.Account,
                        Description = x.Description,
                        CurrencyCode = x.CurrencyCode,
                        Amount = Convert.ToDecimal(x.Amount)
                    }).ToList();


                using (KPMGEntities context = new KPMGEntities())
                {
                    try
                    {
                        context.Configuration.AutoDetectChangesEnabled = false;

                        context.Transactions.AddRange(lst);

                        context.SaveChanges();

                    }
                    finally
                    {
                        context.Configuration.AutoDetectChangesEnabled = true;

                        retval = true;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return retval;
        }

        /// <summary>
        /// Update Transaction
        /// </summary>
        /// <param name="Transaction">Object of type Dto.Transaction</param>
        /// <returns></returns>
        public bool UpdateTransaction(Dto.TransactionDetail Transaction)
        {
            bool retval = false;

            try
            {
                using (KPMGEntities context = new KPMGEntities())
                {
                    Model.Transaction transObj = context.Transactions.Where(x => x.TransactionID.Equals(Transaction.TransactionID)).FirstOrDefault();

                    if (transObj.IsNull()) return retval;

                    transObj.Account = Transaction.Account;
                    transObj.Description = Transaction.Description;
                    transObj.CurrencyCode = Transaction.CurrencyCode;
                    transObj.Amount = Convert.ToDecimal(Transaction.Amount);

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return retval;
        }

        /// <summary>
        /// Delete Transaction
        /// </summary>
        /// <param name="TransactionID">Transaction Identifier</param>
        /// <returns></returns>
        public bool DeleteTransaction(Guid TransactionID)
        {
            bool retval = false;

            try
            {
                using (KPMGEntities context = new KPMGEntities())
                {
                    Model.Transaction transObj = context.Transactions.Where(x => x.TransactionID.Equals(TransactionID)).FirstOrDefault();

                    if (transObj.IsNull()) return retval;

                    context.Transactions.Remove(transObj);
                   
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return retval;
        }

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns></returns>
        public List<TransactionDetail> GetAllTransaction()
        {
            List<TransactionDetail> lst = new List<TransactionDetail>();

            try
            {
                using (KPMGEntities context = new KPMGEntities())
                {
                    lst = context.Transactions.Select(x => new TransactionDetail
                    {
                        TransactionID = x.TransactionID,
                        Account = x.Account,
                        Description = x.Description,
                        CurrencyCode = x.CurrencyCode,
                        Amount = x.Amount.ToString()
                    }).AsParallel().ToList(); 
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return lst;
        }
    }
}
