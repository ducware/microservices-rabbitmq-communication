using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MSE.SendMail.Application.Services;
using MSE.SendMail.Domain.Models;

namespace MSE.SendMail.Api.Controllers
{
    [ApiController]
    [Route("v1/mails")]
    public class MailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public MailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("testmail")]
        public async Task<IActionResult> TestMailAsync(string email, string subject, string content)
        {
            var message = new Message(new string[]
            {
                email
            }, subject, content);

            _emailService.SendEmail(message);

            return StatusCode(StatusCodes.Status200OK, new
            {
                Status = "succees",
                Message = "Email sent"
            });
        }
    }
}
