using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace PPMWebAPP
{
    public class ImportExcel
    {
        private string strConnectionString = string.Empty;

        public ImportExcel(string strConnectionString)
        {
            this.strConnectionString = strConnectionString;
        }

        public DataTable ExportExcelToDataTable(string strFilePath)
        {
            DataTable dtRetVal = new DataTable();

            try
            {
                OleDbConnection ConnExcel = new OleDbConnection(this.strConnectionString);

                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter da = new OleDbDataAdapter();

                cmdExcel.Connection = ConnExcel;

                ConnExcel.Open();

                DataTable dtExcelSchema = new DataTable();
                dtExcelSchema = ConnExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string strSheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

                cmdExcel.CommandText = "SELECT * FROM [Sheet1$]";

                da.SelectCommand = cmdExcel;
                da.Fill(dtRetVal);

                ConnExcel.Close();

                this.RemoveEmptyRows(dtRetVal);

                dtRetVal.AcceptChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return dtRetVal;
        }

        private void RemoveEmptyRows(DataTable source)
        {
            for (int i = source.Rows.Count-1; i >= 0; i--)
            {
                DataRow currentRow = source.Rows[i];
                foreach (var colValue in currentRow.ItemArray)
                {
                    if (!string.IsNullOrEmpty(colValue.ToString()))
                        break;

                    // If we get here, all the columns are empty
                    source.Rows[i].Delete();
                }
            }
        }
    }
}
