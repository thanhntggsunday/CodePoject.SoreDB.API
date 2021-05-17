namespace CodeProject.StoreDB.BusinessSevice.BusinessService
{
    using CodeProject.Business.Common.Errors;
    using CodeProject.Business.Common.Utility;
    using CodeProject.Interfaces.BLL;
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.BusinessSevice.BusinessRules;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.DataService.Extensions;
    using CodeProject.StoreDB.DataService.Logger;
    using CodeProject.StoreDB.DataService.SQL;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using Dapper;
    using FluentValidation.Results;
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// Defines the <see cref="ProductBusinessService" />
    /// </summary>
    public class ProductBusinessService : IProductBusinessService
    {
        /// <summary>
        /// Defines the _dataService
        /// </summary>
        private readonly IUnitOfWork _dataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBusinessService"/> class.
        /// </summary>
        /// <param name="dataService">The dataService<see cref="IUnitOfWork"/></param>
        public ProductBusinessService(IUnitOfWork dataService)
        {
            _dataService = dataService;

            // Logging:
            Log4NetLogger.log.Info("Initial Product Service.");
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Product CreateProduct(Product product, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ProductBusinessRules productBusinessRules = new ProductBusinessRules();
                ValidationResult results = productBusinessRules.Validate(product);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return product;
                }
                // Begin session
                _dataService.CreateSession();

                var param = product.ToDynamicParametersForInsert();

                _dataService.ProductRepository.Insert(ProductScript.Insert, param, CommandType.Text);

                _dataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }

            return product;
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="transaction"></param>
        public void UpdateProduct(Product product, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                ProductBusinessRules productBusinessRules = new ProductBusinessRules();
                ValidationResult results = productBusinessRules.Validate(product);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                // Begin session
                _dataService.CreateSession();

                var param = product.ToDynamicParametersForUpdate();

                _dataService.ProductRepository.Update(ProductScript.Update, param, CommandType.Text);

                _dataService.CommitTransaction(true);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }
        }

        /// <summary>
        /// Get Products
        /// </summary>
        /// <param name="currentPageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public List<ProductDTO> GetAllProductsByPaging(int currentPageNumber, int pageSize,
            out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<ProductDTO> productsDtoList = new List<ProductDTO>();

            try
            {
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@PageIndex", currentPageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                productsDtoList = _dataService.ProductRepository.GetManyRows(ProductScript.GetAllPaging, param,
                                                                              CommandType.Text);

                int totalRow = param.Get<int>("@TotalRows");

                transaction = Utilities.CalculateForPagerOfTransaction(transaction, totalRow, pageSize, currentPageNumber);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }

            return productsDtoList;
        }

        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public ProductDTO GetProduct(object productID, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            var productDTO = new ProductDTO();
            int id = Convert.ToInt32(productID);

            try
            {
                // Begin session
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);
                productDTO = _dataService.ProductRepository.GetById(ProductScript.GetById, param, CommandType.Text);

                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }

            return productDTO;
        }

        /// <summary>
        /// The DeleteProduct
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void DeleteProduct(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                // Begin session
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);
                _dataService.ProductRepository.Delete(ProductScript.Delete, param, CommandType.Text);

                _dataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully!");
                // End session
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }
        }

        /// <summary>
        /// The GetProductsByCategoryId
        /// </summary>
        /// <param name="categoryId">The categoryId<see cref="int"/></param>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{ProductDTO}"/></returns>
        public List<ProductDTO> GetProductsByCategoryId(int categoryId, int currentPageNumber,
            int pageSize, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<ProductDTO> productsDtoList = new List<ProductDTO>();

            try
            {
                int totalRows;

                // Begin session
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@PageIndex", currentPageNumber);
                param.Add("@PageSize", pageSize); // CategoryID
                param.Add("@CategoryID", categoryId);
                param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                productsDtoList = _dataService.ProductRepository.GetManyRows(
                    ProductScript.GetAllPagingByCategoryId, param, CommandType.Text);

                totalRows = param.Get<int>("@TotalRows");

                transaction = Utilities.CalculateForPagerOfTransaction(transaction, totalRows,
                    pageSize, currentPageNumber);
                transaction.ReturnStatus = true;

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

            return productsDtoList;
        }

        /// <summary>
        /// The SearchProductByProductNameAndCategoryName
        /// </summary>
        /// <param name="categoryName">The categoryName<see cref="string"/></param>
        /// <param name="productName">The productName<see cref="string"/></param>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{ProductDTO}"/></returns>
        public List<ProductDTO> GetProductByProductNameAndCategoryName(string categoryName, string productName,
            int currentPageNumber, int pageSize, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<ProductDTO> productsDtoList = new List<ProductDTO>();

            try
            {
                int totalRows = 0;

                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@PageIndex", currentPageNumber);
                param.Add("@PageSize", pageSize); // CategoryID
                param.Add("@ProductName", productName);
                param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                productsDtoList = _dataService.ProductRepository.GetManyRows(
                    ProductScript.GetAllPagingByProductName, param, CommandType.Text);

                totalRows = param.Get<int>("@TotalRows");

                transaction = Utilities.CalculateForPagerOfTransaction(transaction, totalRows,
                    pageSize, currentPageNumber);
                transaction.ReturnStatus = true;

                transaction.ReturnMessage.Add("successfully!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }

            return productsDtoList;
        }

        /// <summary>
        /// The GetNewProducts
        /// </summary>
        /// <param name="productNumber">The productNumber<see cref="int"/></param>
        /// <param name="newLaptops">The newLaptops<see cref="List{ProductDTO}"/></param>
        /// <param name="newSmartPhones">The newSmartPhones<see cref="List{ProductDTO}"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void GetNewProducts(int productNumber, out List<ProductDTO> newLaptops,
            out List<ProductDTO> newSmartPhones, out TransactionalInformation transaction)
        {
            newLaptops = new List<ProductDTO>();
            newSmartPhones = new List<ProductDTO>();
            // int totalRows;

            transaction = new TransactionalInformation();

            try
            {
                _dataService.CreateSession();

                // Get laptop news:
                var param = new DynamicParameters();
                param.Add("@CategoryID", 1);

                newLaptops = _dataService.ProductRepository.GetManyRows(
                    ProductScript.GetNewProductsByCategoryId, param, CommandType.Text);

                param = new DynamicParameters();
                param.Add("@CategoryID", 2);

                newSmartPhones = _dataService.ProductRepository.GetManyRows(
                    ProductScript.GetNewProductsByCategoryId, param, CommandType.Text);

                transaction.Pager.TotalPages = 1;
                transaction.ReturnStatus = true;

                transaction.ReturnMessage.Add("successfully!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }
        }

        public List<ProductDTO> GetProductsByName(out TransactionalInformation transaction,
            int currentPageNumber = 1, int pageSize = 10, string productName = "")
        {
            transaction = new TransactionalInformation();
            List<ProductDTO> productsDtoList = new List<ProductDTO>();

            try
            {
                int totalRows = 0;

                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@PageIndex", currentPageNumber);
                param.Add("@PageSize", pageSize); // CategoryID
                param.Add("@ProductName", productName);
                param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                productsDtoList = _dataService.ProductRepository.GetManyRows(
                    ProductScript.GetAllPagingByProductName, param, CommandType.Text);

                totalRows = param.Get<int>("@TotalRows");

                transaction = Utilities.CalculateForPagerOfTransaction(transaction, totalRows,
                    pageSize, currentPageNumber);
                transaction.ReturnStatus = true;

                transaction.ReturnMessage.Add("successfully!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }

            return productsDtoList;
        }

        public void GetProductDetailsAndProductsRelate(object productID, out ProductDTO productDTO,
            out List<ProductDTO> productDTOs, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            productDTO = new ProductDTO();
            productDTOs = new List<ProductDTO>();

            int id = Convert.ToInt32(productID);

            try
            {
                // Begin session
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);
                productDTO = _dataService.ProductRepository.GetById(ProductScript.GetById, param, CommandType.Text);

                // Get product Relate:
                param = new DynamicParameters();
                param.Add("@CategoryID", productDTO.CategoryID);
                param.Add("@ID", id);

                productDTOs = _dataService.ProductRepository.GetManyRows(
                    ProductScript.GetProductsRelate, param, CommandType.Text);

                transaction.ReturnStatus = true;

                transaction.ReturnMessage.Add("successfully!");
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                transaction.ReturnMessage.Add(errorMessage);
                transaction.ReturnStatus = false;

                // Logging:
                Log4NetLogger.log.Error(ex.Message);
            }
            finally
            {
                _dataService.CloseSession();
            }
        }

        public void CloseSession()
        {
            _dataService.CloseSession();
        }
    }
}