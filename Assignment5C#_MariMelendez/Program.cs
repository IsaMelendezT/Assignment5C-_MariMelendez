using System;
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

            Box box = new Box(30);

            Lettre lettre1 = new Lettre(200, true, "Chemin des Acacias 28, 1009 Pully", Lettre.Format.A3);
            Lettre lettre2 = new Lettre(800, false, "", Lettre.Format.A4); // invalid

            Advertisement adv1 = new Advertisement(1500, true, "Les Moilles  13A, 1913 Saillon");
            Advertisement adv2 = new Advertisement(3000, false, ""); // invalid

            Parcel parcel1 = new Parcel(5000, true, "Grand rue 18, 1950 Sion", 30);
            Parcel parcel2 = new Parcel(3000, true, "Chemin des fleurs 48, 2800 Delemont", 70); // invalid parcel

            box.addMail(lettre1);
            box.addMail(lettre2);
            box.addMail(adv1);
            box.addMail(adv2);
            box.addMail(parcel1);
            box.addMail(parcel2);


            Console.WriteLine("The total amount of postage is " + box.stamp());
            box.display();

            Console.WriteLine("The box contains " + box.mailIsInvalid() + " invalid mails");
        }

        abstract class Mail
        {
            // Properties to set and get fields
            public double Weight { get; set; }
            public bool Express { get; set; }
            public string DestinationAddress { get; set; }

            // Mail constructor
            public Mail(double weight, bool express, string destinationAddress)
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
                double amount = Express ? 2 * (baseFare + Weight * 0.001) : baseFare + 0.001 * Weight;
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

        class Box
        {
            private List<Mail> mails;
            private int maxSize;

            public Box(int maxSize)
            {
                this.maxSize = maxSize;
                mails = new List<Mail>();
            }

            public void addMail(Mail mail)
            {
                if (mails.Count < maxSize)
                {
                    mails.Add(mail);
                }
                else
                {
                    Console.WriteLine("The Mailbox is full, please process and empty the mailbox");
                }
            }

            public int mailIsInvalid()
            {
                int count = 0;
                foreach (Mail mail in mails)
                {
                    if (string.IsNullOrEmpty(mail.DestinationAddress))
                    {
                        count++;
                    }
                    else if (mail is Parcel && ((Parcel)mail).Volume > 50)
                    {
                        count++;
                    }
                }
                return count;
            }

            public double stamp()
            {
                double mailTotal = 0.0;
                foreach (Mail mail in mails)
                {
                    if (mail is Parcel && ((Parcel)mail).Volume > 50)
                    {
                        mailTotal = mailTotal;
                    }
                    else if (!string.IsNullOrEmpty(mail.DestinationAddress))
                    {
                        mailTotal += mail.CalculatePostage();
                    }
                }
                return mailTotal;
            }

            public void display()
            {
                Console.WriteLine("The contents of the mailbox:");
                foreach (Mail mail in mails)
                {
                    if (string.IsNullOrEmpty(mail.DestinationAddress) || (mail is Parcel && ((Parcel)mail).Volume > 50))
                    {
                        Console.WriteLine(mail.GetType().Name + " (Invalid courier)");
                        Console.WriteLine("      Weight: " + mail.Weight + " grams");
                        Console.WriteLine("      Express: " + (mail.Express ? "yes" : "no"));
                        Console.WriteLine("      Destination:");
                        Console.WriteLine("      Price: 0.0");
                        if (mail is Lettre)
                        {
                            Lettre lettre = (Lettre)mail;
                            Console.WriteLine("      Format: " + lettre.LetterFormat);
                        }
                        else if (mail is Parcel)
                        {
                            Parcel parcel = (Parcel)mail;
                            Console.WriteLine("      Volume: " + parcel.Volume + " liters");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine(mail.GetType().Name);
                        Console.WriteLine("      Weight: " + mail.Weight + " grams");
                        Console.WriteLine("      Express: " + (mail.Express ? "yes" : "no"));
                        Console.WriteLine("      Destination: " + mail.DestinationAddress);
                        double price = mail.CalculatePostage();
                        Console.WriteLine("      Price: $" + price);
                        if (mail is Lettre)
                        {
                            Lettre lettre = (Lettre)mail;
                            Console.WriteLine("      Format: " + lettre.LetterFormat);
                        }
                        else if (mail is Parcel)
                        {
                            Parcel parcel = (Parcel)mail;
                            Console.WriteLine("      Volume: " + parcel.Volume + " liters");
                        }
                        Console.WriteLine();
                    }
                }

            }
        }
    }
}