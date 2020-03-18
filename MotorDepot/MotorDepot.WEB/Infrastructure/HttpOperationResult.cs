using MotorDepot.BLL.Infrastructure;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MotorDepot.WEB.Infrastructure
{
    /// <summary>
    /// Class that represents response with status code of unsuccessful operation
    /// </summary>
    public class HttpOperationStatusResult : ActionResult
    {
        private readonly IOperationStatus _unsuccessfulOperation;

        /// <summary>
        /// Instance new HttpOperationStatusResult object for checking code error of the first unsuccesful operation
        /// </summary>
        /// <param name="operations">List of operation that will be checked for http status codes errors</param>
        public HttpOperationStatusResult(params IOperationStatus[] operations)
        {
            if(operations == null)
                throw new ArgumentNullException(nameof(operations));

            _unsuccessfulOperation = operations.FirstOrDefault(x => x.Code != HttpStatusCode.OK);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if(context == null)
                throw new ArgumentNullException(nameof(context));

            if(_unsuccessfulOperation == null)
                throw new ArgumentException("Cannot execute status code of operation, all operations are success");

            context.HttpContext.Response.StatusCode = (int)_unsuccessfulOperation.Code;

        }
    }
}