using System.Collections.Generic;

namespace WebMisc
{
    public class CancelResponse
    {
        public byte Status { set; get; }
        public decimal AmountDue { set; get; }
        
        public List<string> ErrorMessages { set; get; }
    }
}