using System;
using Microsoft.AspNetCore.Mvc;

namespace SagaMicroserviceTransaction.Domain
{
    public class SagaException : Exception
    {
        public ValidationProblemDetails ProblemDetails { get; }
        public SagaException(string message) : base(message)
        {
        }

        public SagaException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SagaException(string typePrefix, string actionName, string title, string detail, string instance = "")
        {
            instance = string.IsNullOrWhiteSpace(instance) ? "None" : instance;

            ProblemDetails = new ValidationProblemDetails
            {
                Title = title,
                Detail = detail,
                Type = typePrefix + "." + actionName,
                Instance = instance
            };
        }

        public SagaException(ValidationProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }

        public SagaException(ProblemDetails problemDetails)
        {
            ProblemDetails = new ValidationProblemDetails()
            {
                Type = problemDetails.Type,
                Title = problemDetails.Title,
                Status = problemDetails.Status,
                Detail = problemDetails.Detail,
                Instance = problemDetails.Instance
            };
        }
    }
}
