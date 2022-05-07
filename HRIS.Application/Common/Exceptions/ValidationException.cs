using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace HRIS.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public ValidationException(IDictionary<string, string[]> errors)
            : this()
        {
            Errors = errors;
        }

        public IDictionary<string, string[]> Errors { get; private set; }

        public void RenameErrorKeys(Dictionary<string, string> newKeyMapping)
        {
            var _newErrors = new Dictionary<string, string[]>();
            foreach (var _error in Errors)
            {
                var _newKey = newKeyMapping.TryGetValue(_error.Key);

                if (_newKey != null)
                    _newErrors.Add(_newKey, _error.Value);
                else
                    _newErrors.Add(_error.Key, _error.Value);
            }

            Errors = _newErrors;
        }
    }
}
