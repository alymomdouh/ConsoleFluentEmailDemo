using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using System;
using System.Net.Mail;
using System.Text;
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
            
            // there many ways to make template one way as string builder 
            StringBuilder template = new();
            template.AppendLine("<h1>Dear user </h1>");
            template.AppendLine("<div>thank you for read our email  from @Model.firstName  and my age is @Model.age and my position is @Model.role </div> ");
            template.AppendLine("i hope see you againe ");

            Email.DefaultSender = Sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("from@gamil.com")
               // .To("toemail@gmail.com", "toName")
                .To("aly.mamdouh@interactts.com", "Aly Mamdouh")
                .Subject("thanks")
                //.Body("thanks to the user in body")
                .UsingTemplate(template.ToString(), new
                {
                    firstName="Aly Momdouh",
                    age=20,
                    role="Full Stack .Net Deveoper"
                })
                .SendAsync();

            Console.WriteLine("Email Status"+email.Successful);
            Console.WriteLine("Email ErrorMessages" + email.ErrorMessages.ToString());
        }
    }
}
