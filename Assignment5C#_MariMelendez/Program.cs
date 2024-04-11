﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            public bool Express { get; set; }
            public string DestinationAddress { get; set; }

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

        class Lettre : Mail
        {
            public Format LetterFormat { get; set; }

            public Lettre(double weight, bool express, string destinationAddress, Format letterformat) : base(weight, express, destinationAddress)
            {
                this.LetterFormat = letterformat;
            }

            public enum Format
            {
                A3, A4
            }

            public override double CalculatePostage()
            {
                double baseFare = this.LetterFormat == Format.A3 ? 3.5 : 2.5;
                double amount = Express ? 2 * (baseFare + Weight * 0.001): baseFare + 0.001 * Weight; 
                return amount;
            }

        }

        class Parcel : Mail
        {
            public double Volume { get; set; }
            public Parcel(double weight, bool express, string destinationAddress, double volume) : base(weight, express, destinationAddress)
            {
                Volume = volume;
            }

            public override double CalculatePostage() 
            {
                double amount = Express ? 2 * (0.25 * Volume + Weight * 0.001) : 0.25 * Volume + Weight * 0.001;
                return amount;
            } 
        }

        class Advertisement : Mail
        {
            public Advertisement(double weight, bool express, string destinationAddress) : base(weight, express, destinationAddress)
            {
            }

            public override double CalculatePostage()
            {
                double amount = Express ? 2 * 5 * 0.001 * Weight : 5 * 0.001 * Weight;
                return amount;
            }
        }
    }
}
