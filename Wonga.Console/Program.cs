using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wonga.Console
{
    class Program
    {
        private readonly Regex _plateauRegex = new Regex(@"^\s*(?<x>[\d]+)\s+(?<y>[\d]+)\s*$", RegexOptions.Compiled);
        
        static void Main(string[] args)
        {
            //Console.ReadLine();
        }
    }
}
