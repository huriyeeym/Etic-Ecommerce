using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etic.Business.Models
{
    public class Result<TResultType>
    {
        public List<string> ErrorMessages { get; set; }
        public bool IsList { get; set; }
        public bool IsOK { get; set; }
        public List<TResultType> List { get; set; }
        public TResultType SingleObject { get; set; }
    }
}
