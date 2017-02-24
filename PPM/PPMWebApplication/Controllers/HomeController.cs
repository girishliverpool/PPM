using PPM.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PPM.Dto;
using System.Data.OleDb;
using System.Data;
using PPMWebAPP;

namespace PPMWebApplication.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITransactionService transactionService;
        public HomeController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase fileUpload)
        {
            try
            {
                List<TransactionDetail> lstTransactionDetails = new List<TransactionDetail>();

                var fileName = Path.GetFileName(fileUpload.FileName);
                var fileExt = Path.GetExtension(fileUpload.FileName);

                if (fileUpload != null)
                {
                    var newFileName = string.Format("{0}{1}", Guid.NewGuid(), fileExt);

                    var filePath = Server.MapPath("~\\Files\\" + newFileName);

                    fileUpload.SaveAs(filePath);

                    Helper helper = new Helper();

                    if (fileExt.ToLower().Equals(".xlsx"))
                        lstTransactionDetails = helper.GetExcelTransaction(filePath);
                    else if (fileExt.ToLower().Equals(".csv"))
                        lstTransactionDetails = helper.GetCSVTransaction(filePath);

                    if(lstTransactionDetails.Count >0)
                    {
                        List<TransactionErrors> lstErrors = this.ValidateTransactionForError(lstTransactionDetails);

                        if(lstErrors.Count > 0)
                        {
                            lstErrors.ForEach(x =>
                                {
                                    ViewBag.ValidationError += string.Format("Line {0} : Errors : {1}<br/>", x.LineNumber, x.ErrorMessage);
                                });

                            return View();
                        }

                        this.transactionService.BulkInsert(lstTransactionDetails);
                    }

                    ViewBag.Message = string.Format("File uploaded with {0} of lines", lstTransactionDetails.Count.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return View();
        }
    }
}