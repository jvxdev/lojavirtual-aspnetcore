using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class EmailContact
    {
        public static void Contact(Contact contact)
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("nearofshadown@gmail.com", "");
            smtp.EnableSsl = true;

            //Mensagem exemplo enviada para e-mail
            string corpoMsg = string.Format("<h3>Olá <b>" + contact.Name + ",</b></h3>Como vai? " +
                "Esses foram os dados enviados via formulário de contato: <br/><br/>" + 
                "<b>Nome:</b> " + contact.Name + "<br/>" +
                "<b>E-mail:</b> " + contact.Email + "<br/>" +
                "<b>Texto:</b> " + contact.Name + "<br/>" +
                "<br/>Essa mensagem foi enviada automaticamente do site LojaVirtual!");

            MailMessage mensagem = new MailMessage();
            mensagem.From = new MailAddress(contact.Email);
            mensagem.To.Add("nearofshadown@gmail.com");
            mensagem.Subject = "Contato - LojaVirtual";
            mensagem.Body = corpoMsg;
            mensagem.IsBodyHtml = true;

            //Enviar mensagem via Smtp
            smtp.Send(mensagem);
        }
    }
}
