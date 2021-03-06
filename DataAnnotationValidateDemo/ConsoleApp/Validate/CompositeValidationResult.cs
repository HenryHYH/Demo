﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Validate
{
    public class CompositeValidationResult : ValidationResult
    {
        private readonly IList<ValidationResult> results = new List<ValidationResult>();

        public IEnumerable<ValidationResult> Results { get { return results; } }

        public CompositeValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames)
        {
        }

        protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult)
        {
        }

        public void AddResult(ValidationResult validationResult)
        {
            results.Add(validationResult);
        }
    }
}
