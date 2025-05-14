using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Exeptions
{/// <summary>
 /// Excepción base para todos los errores relacionados con la capa de negocio.
 /// </summary>
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class EntityNotFoundException : BusinessException
    {
        public string EntityType { get; }
        public object EntityId { get; }

        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string entityType, object entityId)
            : base($"La entidad '{entityType}' con ID '{entityId}' no fue encontrada.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }

        public EntityNotFoundException(string entityType, object entityId, Exception innerException)
            : base($"La entidad '{entityType}' con ID '{entityId}' no fue encontrada.", innerException)
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }

    public class ValidationException : BusinessException
    {
        public string PropertyName { get; }

        public ValidationException(string message) : base(message) { }

        public ValidationException(string propertyName, string message)
            : base($"Error de validación en '{propertyName}': {message}")
        {
            PropertyName = propertyName;
        }

        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class BusinessRuleViolationException : BusinessException
    {
        public string RuleCode { get; }

        public BusinessRuleViolationException(string message) : base(message) { }

        public BusinessRuleViolationException(string ruleCode, string message)
            : base($"Violación de regla de negocio [{ruleCode}]: {message}")
        {
            RuleCode = ruleCode;
        }

        public BusinessRuleViolationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class UnauthorizedAccessBusinessException : BusinessException
    {
        public string Resource { get; }
        public string Operation { get; }

        public UnauthorizedAccessBusinessException(string message) : base(message) { }

        public UnauthorizedAccessBusinessException(string resource, string operation)
            : base($"Acceso no autorizado al recurso '{resource}' para la operación '{operation}'.")
        {
            Resource = resource;
            Operation = operation;
        }

        public UnauthorizedAccessBusinessException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class ExternalServiceException : BusinessException
    {
        public string ServiceName { get; }

        public ExternalServiceException(string message) : base(message) { }

        public ExternalServiceException(string serviceName, string message)
            : base($"Error en el servicio externo '{serviceName}': {message}")
        {
            ServiceName = serviceName;
        }

        public ExternalServiceException(string serviceName, string message, Exception innerException)
            : base($"Error en el servicio externo '{serviceName}': {message}", innerException)
        {
            ServiceName = serviceName;
        }
    }
}