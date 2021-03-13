using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ExceptionLogs.Commands.CreateExceptionLog;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebUI.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private IMediator _mediator;

        public PrivacyModel(ILogger<PrivacyModel> logger, IMediator mediatr)
        {
            _logger = logger;
            _mediator = mediatr;
        }

        public void OnGet()
        {
            _mediator.Send(new CreateExceptionLogCommand() { Message = "hello privacy", LogTime = DateTime.Now, Description = "Privacy" });
        }
    }
}
