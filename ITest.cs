using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MohammadpourAspNetCoreSaturdayMondayEvening
{
    public interface ITest
    {
        string F1();
    }
    public class Test1 : ITest
    {
        public string F1() => "Dependency Injection is Okkkkkk!!!!";
    }
    public class Test2 : ITest
    {
        public string F1() => "Dependency Injection is Okkkkkk!!!!";

        public Test2 Copy() => this.MemberwiseClone() as Test2;

    }
}
