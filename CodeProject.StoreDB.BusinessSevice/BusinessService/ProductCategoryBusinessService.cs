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
    /// Defines the <see cref="ProductCategoryBusinessService" />
    /// </summary>
    public class ProductCategoryBusinessService : IProductCategoryBusinessService
    {
        /// <summary>
        /// Defines the _dataService
        /// </summary>
        private readonly IUnitOfWork _dataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductCategoryBusinessService"/> class.
        /// </summary>
        /// <param name="dataService">The dataService<see cref="IUnitOfWork"/></param>
        public ProductCategoryBusinessService(IUnitOfWork dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// The CreateProductCategory
        /// </summary>
        /// <param name="productCategory">The obj<see cref="ProductCategory"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="ProductCategory"/></returns>
        public ProductCategory CreateProductCategory(ProductCategory productCategory, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ProductCategoryBusinessRules productCategoryBusinessRules = new ProductCategoryBusinessRules();
                ValidationResult results = productCategoryBusinessRules.Validate(productCategory);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return productCategory;
                }
                // Begin session
                _dataService.CreateSession();

                var param = productCategory.ToDynamicParametersForInsert();

                _dataService.ProductCategoryRepository.Insert(ProductCategoryScript.Insert, param, CommandType.Text);

                productCategory.ID = param.Get<int>("@ID");

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

            return productCategory;
        }

        /// <summary>
        /// The DeleteProductCategory
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void DeleteProductCategory(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                // Begin session
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);

                _dataService.ProductCategoryRepository.Delete(ProductScript.Delete, param, CommandType.Text);

                _dataService.CommitTransaction(true);

                transaction.ReturnMessage.Add("successfully!");
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
        /// The GetAll
        /// </summary>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{ProductCategoryDTO}"/></returns>
        public List<ProductCategoryDTO> GetAll(out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<ProductCategoryDTO> productCategoryDtoList = new List<ProductCategoryDTO>();

            try
            {
                _dataService.CreateSession();

                productCategoryDtoList = _dataService.ProductCategoryRepository.GetAll(
                    ProductCategoryScript.GetAll, CommandType.Text);

              
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

           

            return productCategoryDtoList;
        }

        /// <summary>
        /// The GetProductCategory
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="ProductCategoryDTO"/></returns>
        public ProductCategoryDTO GetProductCategory(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            ProductCategoryDTO productCategory = new ProductCategoryDTO();

            try
            {
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);
                productCategory = _dataService.ProductCategoryRepository.GetById(ProductCategoryScript.GetById, 
                    param, CommandType.Text);

             
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

            

            return productCategory;
        }

        /// <summary>
        /// The GetProductCategorys
        /// </summary>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="sortExpression">The sortExpression<see cref="string"/></param>
        /// <param name="sortDirection">The sortDirection<see cref="string"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{ProductCategoryDTO}"/></returns>
        public List<ProductCategoryDTO> GetProductCategorysByPaging(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            List<ProductCategoryDTO> productCategoryDtoList = new List<ProductCategoryDTO>();

            try
            {
                int totalRows = 0;

                _dataService.CreateSession();

                // Todo here:
                var param = new DynamicParameters();
                param.Add("@PageIndex", currentPageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                productCategoryDtoList = _dataService.ProductCategoryRepository.GetManyRows(
                                ProductCategoryScript.GetAllPaging, param, CommandType.Text);

                totalRows = param.Get<int>("@TotalRows");


                transaction = Utilities.CalculateForPagerOfTransaction(transaction, totalRows, 
                    pageSize, currentPageNumber);

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

            
            return productCategoryDtoList;
        }

        /// <summary>
        /// The UpdateProductCategory
        /// </summary>
        /// <param name="productCategory">The obj<see cref="ProductCategory"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void UpdateProductCategory(ProductCategory productCategory, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ProductCategoryBusinessRules productCategoryBusinessRules = new ProductCategoryBusinessRules();
                ValidationResult results = productCategoryBusinessRules.Validate(productCategory);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                // Begin session
                _dataService.CreateSession();

                var param = productCategory.ToDynamicParametersForUpdate();

                _dataService.ProductCategoryRepository.Update(ProductCategoryScript.Update, param, CommandType.Text);

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
    }
}