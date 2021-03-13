using Application.Common.Behaviors;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enumeration;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ExceptionLogs.Commands.CreateExceptionLog
{
    public class CreateExceptionLogCommand : IRequest<string>
    {
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
        public string Description { get; set; }
    }

    public class CreateExceptionLogCommandHandler : IRequestHandler<CreateExceptionLogCommand, string>
    {
        private readonly IApplicationDbContext _context;
        public CreateExceptionLogCommandHandler(RequestContext reqCtx)
        {
            _context = reqCtx.Items["IApplicationDbContext"] as IApplicationDbContext;
        }

        public async Task<string> Handle(CreateExceptionLogCommand request, CancellationToken cancellationToken)
        {
            int retint;
            var newExceptionLog = new ApplicationLog()
            {
                Message = request.Message,
                LogTime = request.LogTime,
                Description = request.Description
            };
            _context.ApplicationLogs.Add(newExceptionLog);
            try
            {
                retint = await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType());
                _context.ApplicationLogs.Add(new ApplicationLog() { Message = e.ToString(), LogTime = DateTime.Now, Description = "CreateExceptionLogCommand" });
                return "Create ExceptionLog Error.";
            }
            return retint.ToString();
        }
    }

}
