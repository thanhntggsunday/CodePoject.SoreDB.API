namespace CodeProject.Business.Common.Errors
{
    using CodeProject.StoreDB.Common.Classes;
    using FluentValidation.Results;
    using System.Collections.Generic;

    /// <summary>
    /// Validation Errors
    /// </summary>
    public static class ValidationErrors
    {
        /// <summary>
        /// Populate Validation Errors
        /// </summary>
        /// <param name="failures"></param>
        /// <returns></returns>
        public static TransactionalInformation PopulateValidationErrors(IList<ValidationFailure> failures)
        {
            TransactionalInformation transaction = new TransactionalInformation();

            transaction.ReturnStatus = false;
            foreach (ValidationFailure error in failures)
            {
                if (transaction.ValidationErrors.ContainsKey(error.PropertyName) == false)
                    transaction.ValidationErrors.Add(error.PropertyName, error.ErrorMessage);

                transaction.ReturnMessage.Add(error.ErrorMessage);
            }

            return transaction;
        }
    }
}