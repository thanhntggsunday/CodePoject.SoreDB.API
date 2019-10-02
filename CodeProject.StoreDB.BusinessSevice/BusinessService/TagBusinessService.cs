namespace CodeProject.StoreDB.BusinessSevice.BusinessService
{
    using AutoMapper;
    using CodeProject.Business.Common.Errors;
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.BusinessSevice.BusinessRules;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation.Results;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="TagBusinessService" />
    /// </summary>
    public class TagBusinessService : ITagBusinessService
    {
        /// <summary>
        /// Defines the _dataService
        /// </summary>
        private readonly IUnitOfWork _dataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TagBusinessService"/> class.
        /// </summary>
        /// <param name="dataService">The dataService<see cref="IUnitOfWork"/></param>
        public TagBusinessService(IUnitOfWork dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// The CreateTag
        /// </summary>
        /// <param name="obj">The obj<see cref="Tag"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="Tag"/></returns>
        public Tag CreateTag(Tag obj, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                TagBusinessRules tagBusinessRules = new TagBusinessRules();
                ValidationResult results = tagBusinessRules.Validate(obj);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return obj;
                }

                _dataService.CreateSession();
                // Todo here:

                _dataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Product successfully created.");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }

            return obj;
        }

        /// <summary>
        /// The DeleteTag
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void DeleteTag(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                // Begin session
                _dataService.CreateSession();
                // Todo here:

                _dataService.CommitTransaction(true);

                // End session.
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }
        }

        /// <summary>
        /// The GetTag
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="TagDTO"/></returns>
        public TagDTO GetTag(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            Tag tag = new Tag();

            try
            {
                _dataService.CreateSession();
                // Todo here:

                transaction.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }

            var tagDto = Mapper.Map<Tag, TagDTO>(tag);

            return tagDto;
        }

        /// <summary>
        /// The GetTags
        /// </summary>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="sortExpression">The sortExpression<see cref="string"/></param>
        /// <param name="sortDirection">The sortDirection<see cref="string"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{TagDTO}"/></returns>
        public List<TagDTO> GetTagsByPaging(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<Tag> tags = new List<Tag>();
            List<TagDTO> tagDtoList = new List<TagDTO>();

            try
            {
                int totalRows;

                _dataService.CreateSession();
                // Todo here:

                //transaction.TotalPages = Utilities.CalculateTotalPages(totalRows, pageSize);
                //transaction.TotalRows = totalRows;

                transaction.ReturnStatus = true;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }

            foreach (var item in tags)
            {
                tagDtoList.Add(Mapper.Map<Tag, TagDTO>(item));
            }

            return tagDtoList;
        }

        /// <summary>
        /// The UpdateTag
        /// </summary>
        /// <param name="obj">The obj<see cref="Tag"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void UpdateTag(Tag obj, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                TagBusinessRules productBusinessRules = new TagBusinessRules();
                ValidationResult results = productBusinessRules.Validate(obj);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _dataService.CreateSession();

                // Todo here:

                _dataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("Product was successfully updated.");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;
            }
            finally
            {
                _dataService.CloseSession();
            }
        }
    }
}