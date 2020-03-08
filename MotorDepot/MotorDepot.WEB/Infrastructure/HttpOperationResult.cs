using MotorDepot.BLL.Infrastructure;
using System;
using System.Web.Mvc;

namespace MotorDepot.WEB.Infrastructure
{
    public class HttpOperationStatusResult : ActionResult
    {
        private readonly IOperationStatus _unsuccessfulOperation;

        /// <summary>
        /// Instance new HttpOperationStatusResult object for checking code error of the first of unsuccess operations
        /// </summary>
        /// <param name="operations">List of operation that will be checked for http status codes errors</param>
        public HttpOperationStatusResult(params IOperationStatus[] operations)
        {
            foreach (var operation in operations)
            {
                if (operation.Success) continue;

                _unsuccessfulOperation = operation;
                break;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            context.HttpContext.Response.StatusCode = (int)_unsuccessfulOperation.Code;
            context.HttpContext.Response.StatusDescription = _unsuccessfulOperation.Message;
        }
    }
}