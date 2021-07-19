using LojaVirtual.Libraries.Security;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace LojaVirtual.Libraries.Email
{
    public class EmailManage
    {
        private SmtpClient _smtp;
        private IConfiguration _conf;
        private IHttpContextAccessor _httpContextAccessor;
        private string passPhrase = "xdoskx21321Oo@@@sko443askzmkas12313";

        public EmailManage(SmtpClient smtp, IConfiguration conf, IHttpContextAccessor httpContextAccessor)
        {
            _smtp = smtp;
            _conf = conf;
            _httpContextAccessor = httpContextAccessor;
        }


        public void NewEmail(Contact contact)
        {
            //Mensagem exemplo enviada para e-mail
            string corpoMsg = string.Format("<h3>Olá <b>" + contact.Name + ",</b></h3> como vai? " +
                "Esses foram os dados enviados via formulário de contato: <br/><br/>" +
                "<b>Nome:</b> " + contact.Name + "<br/>" +
                "<b>E-mail:</b> " + contact.Email + "<br/>" +
                "<b>Texto:</b> " + contact.Name + "<br/>" +
                "<br/>Essa mensagem foi enviada automaticamente do site LojaVirtual!");

            MailMessage message = new MailMessage();
            message.From = new MailAddress(_conf.GetValue<string>("Email:Username"));
            message.To.Add("nearofshadown@gmail.com");
            message.Subject = "Contato - LojaVirtual";
            message.Body = corpoMsg;
            message.IsBodyHtml = true;

            //Enviar mensagem via Smtp
            _smtp.Send(message);
        }


        public void NewPasswordEmail(Collaborator collaborator)
        {
            //Mensagem exemplo enviada para e-mail após adm solicitar nova senha para funcionário
            string corpoMsg = string.Format("<h2>Colaborador - LojaVirtual</h2> " +
                "Sua nova senha é: " +
                "<h3>{0}</h3>", collaborator.Password);

            MailMessage message = new MailMessage();
            message.From = new MailAddress(_conf.GetValue<string>("Email:Username"));
            message.To.Add(collaborator.Email);
            message.Subject = "Colaborador - LojaVirtual - Nova senha para o colaborador " + collaborator.Name;
            message.Body = corpoMsg;
            message.IsBodyHtml = true;

            //Enviar mensagem via Smtp
            _smtp.Send(message);
        }


        public void NewOrderEmail(Client client, Order order)
        {
            //Mensagem exemplo enviada para e-mail após cliente efetuar um pedido
            string corpoMsg = string.Format(
                "Olá <b>" + client.Name + "</b>, " +
                "como vai? <br/> Só estou passando para lembrar que o " +
                "teu pedido foi realizado com sucesso! <br/>" +
                "Este é o n° do teu pedido: <b>{0}<b><br/>" +
                "<b>Não esqueça de entrar em nosso site " +
                "para acompanhar mais informações!",
                order.Id + "-" + order.TransactionId
                );

            MailMessage message = new MailMessage();
            message.From = new MailAddress(_conf.GetValue<string>("Email:Username"));
            message.To.Add(client.Email);
            message.Subject = "Pedido n° " + order.Id + "-" + order.TransactionId + " - LojaVirtual";
            message.Body = corpoMsg;
            message.IsBodyHtml = true;

            //Enviar mensagem via Smtp
            _smtp.Send(message);
        }


        public void RecoverPasswordEmail(Client client)
        {
            //Mensagem exemplo enviada para e-mail para recuperação de senha
            string idCrip = StringCipher.Encrypt(client.Id.ToString(), passPhrase);

            var request = _httpContextAccessor.HttpContext.Request;

            string url = $"{request.Scheme}://{request.Host}/Client/Home/CreateNewPassword/{idCrip}";

            string corpoMsg = string.Format(
                "Olá <b>" + client.Name + "</b>, como vai?" +
                "<br/>Aqui está o link para criar uma nova " +
                "senha: <a href='{0} target='_blank'>{0}</a>'",
                url
                );

            MailMessage message = new MailMessage();
            message.From = new MailAddress(_conf.GetValue<string>("Email:Username"));
            message.To.Add(client.Email);
            message.Subject = "Recuperação de senha - LojaVirtual";
            message.Body = corpoMsg;
            message.IsBodyHtml = true;

            //Enviar mensagem via Smtp
            //_smtp.Send(message);
        }
    }
}
