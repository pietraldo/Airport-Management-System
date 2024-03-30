using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace projectAirport
{
    public abstract class Media(string name)
    {
        string name = name;
        public string Name { get { return name; } }
    }
}
