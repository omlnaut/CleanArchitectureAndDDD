using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BuberDiner.Application.Common.Errors;

public interface IError
{
    public HttpStatusCode StatusCode { get; }
    public string Message { get; }
}
