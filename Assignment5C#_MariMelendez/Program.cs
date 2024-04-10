using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment5C__MariMelendez
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }

        abstract class Mail
        {
            // Properties to set and get fields
            public double Weight { get; set; }
            private bool Express { get; set; }
            private string DestinationAddress { get; set; }

            // Mail constructor
            protected Mail(double weight, bool express, string destinationAddress)
            {
                Weight = weight;
                Express = express;
                DestinationAddress = destinationAddress;
            }

            // CalculatePostage method, that will define on each one of the class childs. 
            public abstract double CalculatePostage();


        }
    }
}
