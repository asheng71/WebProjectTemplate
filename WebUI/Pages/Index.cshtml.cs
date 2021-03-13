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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IMediator _mediator;

        public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        

        public void OnGet()
        {
            _mediator.Send(new CreateExceptionLogCommand() { Message = "hello world", LogTime = DateTime.Now, Description = "Index" });
        }
    }
}
