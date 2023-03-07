using FluentEmail.Core;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ConsoleFluentEmailDemo
{
    internal class Program
    {
        static async Task  Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var Sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl=false,
                //UseDefaultCredentials=false,
                //Host="ip of the client ",

                // this will make folder and make emails as files and put in this folder 
                // DeliveryMethod=SmtpDeliveryMethod.SpecifiedPickupDirectory,
                // PickupDirectoryLocation=@"C:\TestEmailsDemos"

                // cand send and recive emails in network and lisen to it by Papercut-SMTP application 
                // download from here https://github.com/ChangemakerStudios/Papercut-SMTP/releases
                Port = 25,
                DeliveryMethod= SmtpDeliveryMethod.Network
            });
            Email.DefaultSender = Sender;
            var email = await Email
                .From("from@gamil.com")
                .To("toemail@gmail.com", "toName")
                .Subject("thanks")
                .Body("thanks to the user in body")
                .SendAsync();

            Console.WriteLine("Email Status"+email.Successful);
            Console.WriteLine("Email ErrorMessages" + email.ErrorMessages.ToString());
        }
    }
}
