using Nogupe.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nogupe.Web.Services.Email
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(MailRequest request);
    }
}
