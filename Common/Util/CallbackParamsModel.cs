using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Util
{

    public class CallbackParamsModel
    {
        public string Section { get; set; }
        public int Id { get; set; }

        public CallbackParamsModel(string section, int Id)
        {
            this.Section = section;
            this.Id = Id;
        }
    }
}