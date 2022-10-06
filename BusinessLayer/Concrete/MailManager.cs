using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MailManager : IMailService
    {
        IMailRepository _mailRepository;

        public MailManager(IMailRepository mailRepository)
        {
            _mailRepository = mailRepository;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            await _mailRepository.SendEmailAsync(toEmail, subject, content);
        }
    }
}
