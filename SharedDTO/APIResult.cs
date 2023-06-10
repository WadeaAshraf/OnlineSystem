using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedDTO
{
    public class APIResult
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public APIResult SaveSucess(object obj) {
            return new APIResult { IsSuccess = true, Data = obj };
        }
        public APIResult SaveFailer(Exception e)
        {
            return new APIResult { IsSuccess = false, Message=e.Message };
        }
    }
}
