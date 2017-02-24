using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PPM.Dto
{
    public class TransactionDetail
    {
        [Required(ErrorMessage="Please provide transaction id")]
        public Guid TransactionID { get; set; }

        [Required(ErrorMessage="Please provide account")]
        public string Account { get; set; }

        [Required(ErrorMessage="Please provide description")]
        public string Description { get; set; }

        [Required(ErrorMessage="Please provide valid currency code")]
        [StringLength(3, ErrorMessage="Only three charachers allowed")]
        public string CurrencyCode { get; set; }

        [Required(ErrorMessage="Please provide amount")]
        public string Amount { get; set; }
    }
}
