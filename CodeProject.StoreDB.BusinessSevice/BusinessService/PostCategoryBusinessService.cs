namespace CodeProject.StoreDB.BusinessSevice.BusinessService
{
    using AutoMapper;
    using CodeProject.Business.Common.Errors;
    using CodeProject.Business.Common.Utility;
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.BusinessSevice.BusinessRules;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.DataService.Extensions;
    using CodeProject.StoreDB.DataService.SQL;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using Dapper;
    using FluentValidation.Results;
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Defines the <see cref="PostCategoryBusinessService" />
    /// </summary>
    public class PostCategoryBusinessService : IPostCategoryBusinessService
    {
        /// <summary>
        /// Defines the _dataService
        /// </summary>
        private readonly IUnitOfWork _dataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostCategoryBusinessService"/> class.
        /// </summary>
        /// <param name="dataService">The dataService<see cref="IUnitOfWork"/></param>
        public PostCategoryBusinessService(IUnitOfWork dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// The CreatePostCategory
        /// </summary>
        /// <param name="pc">The obj<see cref="PostCategory"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="PostCategory"/></returns>
        public PostCategory CreatePostCategory(PostCategory pc, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                PostCategoryBusinessRules postCategoryBusinessRules = new PostCategoryBusinessRules();
                ValidationResult results = postCategoryBusinessRules.Validate(pc);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return pc;
                }

                // Begin session
                _dataService.CreateSession();

                var param = pc.ToDynamicParametersForInsert();

                _dataService.PostCategoryRepository.Insert(PostCategoryScript.Insert, param, CommandType.Text);

                pc.ID = param.Get<int>("@ID");

                _dataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully");
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

            return pc;
        }

        /// <summary>
        /// The DeletePostCategory
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void DeletePostCategory(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                // Begin session
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);

                _dataService.PostCategoryRepository.Delete(PostCategoryScript.Delete, param, CommandType.Text);

                _dataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully.");
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
        /// The GetPostCategory
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="PostCategoryDTO"/></returns>
        public PostCategoryDTO GetPostCategory(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            PostCategoryDTO postDto = new PostCategoryDTO();

            try
            {
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);
                postDto = _dataService.PostCategoryRepository.GetById(PostCategoryScript.GetById,
                    param, CommandType.Text);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully.");
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

           

            return postDto;
        }

        /// <summary>
        /// The GetPostCategorys
        /// </summary>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="sortExpression">The sortExpression<see cref="string"/></param>
        /// <param name="sortDirection">The sortDirection<see cref="string"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{PostCategoryDTO}"/></returns>
        public List<PostCategoryDTO> GetAllPostCategories(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
                       
            List<PostCategoryDTO> PostCategorieDtoList = new List<PostCategoryDTO>();

            try
            {
                _dataService.CreateSession();

                // Todo here:
                PostCategorieDtoList = _dataService.PostCategoryRepository.GetAll(
                    PostCategoryScript.GetAll, CommandType.Text);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully.");
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

            
            return PostCategorieDtoList;
        }

        /// <summary>
        /// The UpdatePostCategory
        /// </summary>
        /// <param name="pc">The obj<see cref="PostCategory"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void UpdatePostCategory(PostCategory pc, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                PostCategoryBusinessRules postCategoryBusinessRules = new PostCategoryBusinessRules();
                ValidationResult results = postCategoryBusinessRules.Validate(pc);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                // Todo here:
                var param = pc.ToDynamicParametersForUpdate();

                _dataService.PostCategoryRepository.Update(PostCategoryScript.Update, param, CommandType.Text);

                _dataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully");
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
        /// The GetPostCategorys
        /// </summary>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="sortExpression">The sortExpression<see cref="string"/></param>
        /// <param name="sortDirection">The sortDirection<see cref="string"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{PostCategoryDTO}"/></returns>
        public List<PostCategoryDTO> GetPostCategorysByPaging(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
                       
            List<PostCategoryDTO> PostCategorieDtoList = new List<PostCategoryDTO>();

            try
            {
                int totalRows = 0;

                _dataService.CreateSession();

                // Todo here:

                var param = new DynamicParameters();
                param.Add("@PageIndex", currentPageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                PostCategorieDtoList = _dataService.PostCategoryRepository.GetManyRows(
                                ProductCategoryScript.GetAllPaging, param, CommandType.Text);

                totalRows = param.Get<int>("@TotalRows");

                transaction = Utilities.CalculateForPagerOfTransaction(transaction, totalRows, pageSize, currentPageNumber);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully.");
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

           

            return PostCategorieDtoList;
        }
    }
}