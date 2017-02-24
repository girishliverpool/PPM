using PPM.Dto;
using PPMWebAPP;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PPMWebApplication
{
    public class Helper
    {
        public List<TransactionDetail> GetExcelTransaction(string strFilePath)
        {
            List<TransactionDetail> lst = new List<TransactionDetail>();

            try
            {
                string strConnection = string.Empty;

                strConnection = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'", strFilePath);

                ImportExcel import = new ImportExcel(strConnection);
                DataTable dtRetVal = import.ExportExcelToDataTable(strFilePath);

                foreach (DataRow row in dtRetVal.Rows)
                {
                    lst.Add(new TransactionDetail
                    {
                        Account = Convert.ToString(row[0]),
                        Description = Convert.ToString(row[1]),
                        CurrencyCode = Convert.ToString(row[2]),
                        Amount = Convert.ToString(row[3])
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return lst;
        }

        public List<TransactionDetail> GetCSVTransaction(string strFilePath)
        {
            List<TransactionDetail> lst = new List<TransactionDetail>();

            try
            {
                lst = System.IO.File.ReadAllLines(strFilePath).Skip(1).Select(x => x.Split(',')).Select(x => new TransactionDetail
                {
                    Account = x[0],
                    Description = x[1],
                    CurrencyCode = x[2],
                    Amount = x[3]
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return lst;
        }
    }
}