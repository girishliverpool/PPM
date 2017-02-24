using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPM.Dto;
using System.Reflection;

namespace PPMWebApplication.Controllers
{
    public class BaseController : Controller
    {
        [NonAction]
        public bool IsValidCurrencyCode(string strCurrencyCode)
        {
            if (strCurrencyCode.Trim().IsNullOrEmpty()) return false;

            return CurrencyCodeMapper.IsValidCurrencyCode(strCurrencyCode);
        }

        [NonAction]
        public bool IsValidAmount(string strAmount)
        {

            if (strAmount.Trim().IsNullOrEmpty()) return false;

            decimal result;
            return decimal.TryParse(strAmount, out result);
        }

        [NonAction]
        public List<TransactionErrors> ValidateTransactionForError(List<TransactionDetail> lstTransactionDetails)
        {
            List<TransactionErrors> lst = new List<TransactionErrors>();

            int i = 2;
            try
            {
                lstTransactionDetails.ForEach(x =>
                    {
                        string strError = string.Empty;

                        if(x.Account.IsNullOrEmpty())
                        {
                            strError = "Account";
                        }

                        if(x.Description.IsNullOrEmpty())
                        {
                            strError = strError.IsNullOrEmpty() ? "Description is empty" : string.Format("{0}, Description", strError);
                        }

                        if(x.CurrencyCode.IsNullOrEmpty())
                        {
                            strError = strError.IsNullOrEmpty() ? "Currency Code is empty" : string.Format("{0}, Currency Code", strError);
                        }

                        if(x.Amount.IsNullOrEmpty())
                        {
                            strError = strError.IsNullOrEmpty() ? "Amount is empty" : string.Format("{0}, Amount", strError);
                        }

                        if(!this.IsValidCurrencyCode(x.CurrencyCode))
                        {
                            strError = strError.IsNullOrEmpty() ? "Invalid Currency Code" : string.Format("{0}, Invalid Currency Code", strError);
                        }

                        if(!this.IsValidAmount(x.Amount))
                        {
                            strError = strError.IsNullOrEmpty() ? "Invalid Amount" : string.Format("{0}, Invalid Amount", strError);
                        }

                        if(strError.IsNotNullOrEmpty())
                        {
                            lst.Add(new TransactionErrors{
                                LineNumber = i.ToString(),
                                ErrorMessage = strError
                            });
                        }

                        i++;
                              
                    });
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return lst;
        }

    }
}