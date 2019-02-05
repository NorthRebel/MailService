using System.Threading.Tasks;
using MailService.Domain.Repositories;
using MailService.Services.Mail;
using Microsoft.AspNetCore.Mvc;

namespace MailService.API.Controllers
{
    [ApiController]
    [Route("api/mails")]
    public class MailController : Controller
    {
        /// <inheritdoc cref="ICorrespondenceRepository"/>
        private readonly ICorrespondenceRepository _correspondenceRepository;

        /// <inheritdoc cref="IRecipientRepository"/>
        private readonly IRecipientRepository _recipientRepository;

        /// <inheritdoc cref="IMessageRepository"/>
        private readonly IMessageRepository _messageRepository;

        /// <inheritdoc cref="IEmailService"/>
        private readonly IEmailService _emailService;

        public MailController(ICorrespondenceRepository correspondenceRepository, 
            IRecipientRepository recipientRepository, 
            IMessageRepository messageRepository, 
            IEmailService emailService)
        {
            _correspondenceRepository = correspondenceRepository;
            _recipientRepository = recipientRepository;
            _messageRepository = messageRepository;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] string name)
        {
            
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Show()
        {
            return Ok();
        }
    }
}
