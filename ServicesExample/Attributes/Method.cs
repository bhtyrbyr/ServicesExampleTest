using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesExample.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class Method : Attribute
    {
        private string _method;
        public string MethodName
        {
            get { return _method; }
        }
        public Method(string method)
        {
            _method = method;
        }
    }
}
