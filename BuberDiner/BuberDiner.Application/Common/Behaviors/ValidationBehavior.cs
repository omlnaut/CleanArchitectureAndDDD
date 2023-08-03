using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDiner.Application.Authentication.Commands.Register;
using BuberDiner.Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace BuberDiner.Application.Common.Behaviors;

public class ValidationBehavior : IPipelineBehavior<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    public async Task<ErrorOr<AuthenticationResult>> Handle(
        RegisterCommand request,
        RequestHandlerDelegate<ErrorOr<AuthenticationResult>> next,
        CancellationToken cancellationToken)
    {
        var result = await next();

        return result;
    }
}
