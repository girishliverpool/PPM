using PPM.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPM.Dto;

namespace PPMWebApplication.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View(this.transactionService.GetAllTransactions().OrderBy(x => Convert.ToInt64(x.Account)));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            try
            {
                return View(this.transactionService.GetAllTransactions().Where(x => x.TransactionID.Equals(id)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpPost]
        public ActionResult Edit(TransactionDetail transactionDetails)
        {
            try
            {

                ViewBag.ValidationError = string.Empty;

                if (!this.IsValidAmount(transactionDetails.Amount))
                {
                    ViewBag.ValidationError = "Please provide valid amount.";
                }

                if (!this.IsValidCurrencyCode(transactionDetails.CurrencyCode))
                {
                    if (string.IsNullOrEmpty(ViewBag.ValidationError))
                        ViewBag.ValidationError = "Please provide valid currency code.";
                    else
                        ViewBag.ValidationError = string.Format("{0}<br/>{1}", ViewBag.ValidationError, "Please provide valid currency code.");
                }

                if (!string.IsNullOrEmpty(ViewBag.ValidationError))
                {
                    return View(transactionDetails);
                }

                this.transactionService.UpdateTransaction(transactionDetails);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            try
            {
                return View(this.transactionService.GetAllTransactions().Where(x => x.TransactionID.Equals(id)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        [HttpPost]
        public ActionResult Delete(Guid id, TransactionDetail transactionDetails)
        {
            try
            {
                this.transactionService.DeleteTransaction(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
