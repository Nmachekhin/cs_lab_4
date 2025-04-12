using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDisplay
{
    internal class ZodiakSign
    {
        private DateTime _beginDate;
        private DateTime _endDate;
        private string _sign;

        public ZodiakSign(DateTime beginDate, DateTime endDate, string sign)
        {
            _beginDate = beginDate;
            _endDate = endDate;
            _sign = new String(sign);
        }

        public string SignName
        {
            get { string signStringCopy = new String(_sign); return signStringCopy; }
        }

        public async Task<bool> CheckBirthDate(DateTime birthday)
        {
            return birthday <= _endDate && birthday >= _beginDate;
        }
            
    }
}
