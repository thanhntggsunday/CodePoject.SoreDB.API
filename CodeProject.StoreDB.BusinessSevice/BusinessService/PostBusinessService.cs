namespace CodeProject.StoreDB.BusinessSevice.BusinessService
{
    using AutoMapper;
    using CodeProject.Business.Common.Errors;
    using CodeProject.Business.Common.Utility;
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.BusinessSevice.BusinessRules;
    using CodeProject.StoreDB.Common.Classes;
    using CodeProject.StoreDB.Interfaces.BLL;
    using CodeProject.StoreDB.Model.DTO;
    using CodeProject.StoreDB.Model.Entities;
    using FluentValidation.Results;
    using System;
    using System.Collections.Generic;
    using CodeProject.StoreDB.DataService.Extensions;
    using CodeProject.StoreDB.DataService.Logger;
    using CodeProject.StoreDB.DataService.SQL;
    using System.Data;
    using Dapper;

    /// <summary>
    /// Defines the <see cref="PostBusinessService" />
    /// </summary>
    public class PostBusinessService : IPostBusinessService
    {
        /// <summary>
        /// Defines the _dataService
        /// </summary>
        private readonly IUnitOfWork _dataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostBusinessService"/> class.
        /// </summary>
        /// <param name="dataService">The dataService<see cref="IUnitOfWork"/></param>
        public PostBusinessService(IUnitOfWork dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// The CreatePost
        /// </summary>
        /// <param name="post">The obj<see cref="Post"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="Post"/></returns>
        public Post CreatePost(Post post, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                PostBusinessRules postBusinessRules = new PostBusinessRules();
                ValidationResult results = postBusinessRules.Validate(post);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return post;
                }

                _dataService.CreateSession();

                // Todo here:

                var param = post.ToDynamicParametersForInsert();

                _dataService.PostRepository.Insert(PostScript.Insert, param, CommandType.Text);

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

            return post;
        }

        /// <summary>
        /// The DeletePost
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void DeletePost(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            try
            {
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);
                _dataService.PostRepository.Delete(PostScript.Delete, param, CommandType.Text);

                _dataService.CommitTransaction(true);
                transaction.ReturnStatus = true;
                transaction.ReturnMessage.Add("successfully!");

                // End session

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
        /// The GetPost
        /// </summary>
        /// <param name="id">The id<see cref="object"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="PostDTO"/></returns>
        public PostDTO GetPost(object id, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            PostDTO postDto = new PostDTO();

            try
            {
                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@ID", id);
                var item = _dataService.PostRepository.GetById(PostScript.GetById, param, CommandType.Text);

                postDto.ID = item.ID;
                postDto.Name = item.Name;
                postDto.Alias = item.Alias;
                postDto.CategoryID = item.CategoryID;
                postDto.Image = item.Image;               
                postDto.Description = item.Description;
                postDto.Content = item.Content;
                postDto.HomeFlag = item.HomeFlag;
                postDto.HotFlag = item.HotFlag;
                postDto.ViewCount = item.ViewCount;
                postDto.CreatedDate = item.CreatedDate;
                postDto.CreatedBy = item.CreatedBy;
                postDto.UpdatedDate = item.UpdatedDate;
                postDto.UpdatedBy = item.UpdatedBy;
                postDto.MetaKeyword = item.MetaKeyword;
                postDto.MetaDescription = item.Description;
                postDto.Status = item.Status;              
                postDto.CategoryName = item.CategoryName;

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
        /// The GetPostsByPaging
        /// </summary>
        /// <param name="currentPageNumber">The currentPageNumber<see cref="int"/></param>
        /// <param name="pageSize">The pageSize<see cref="int"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        /// <returns>The <see cref="List{PostDTO}"/></returns>
        public List<PostDTO> GetPostsByPaging(int currentPageNumber, int pageSize, 
                                              out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();
            List<PostDTO> postDtoList = new List<PostDTO>();

            try
            {
                int totalRows = 0;

                _dataService.CreateSession();

                var param = new DynamicParameters();
                param.Add("@PageIndex", currentPageNumber);
                param.Add("@PageSize", pageSize);
                param.Add("@TotalRows", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var result = _dataService.PostRepository.GetManyRows(PostScript.GetAllPaging, param,
                                                                     CommandType.Text);

                totalRows = param.Get<int>("@TotalRows");

                foreach (var item in result)
                {
                    postDtoList.Add(new PostDTO()
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Alias = item.Alias,
                        CategoryID = item.CategoryID,
                        CategoryName = item.CategoryName,
                        Image = item.Image,                       
                        Description = item.Description,
                        Content = item.Content,
                        HomeFlag = item.HomeFlag,
                        HotFlag = item.HotFlag,
                        ViewCount = item.ViewCount,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        UpdatedDate = item.UpdatedDate,
                        UpdatedBy = item.UpdatedBy,
                        MetaKeyword = item.MetaKeyword,
                        MetaDescription = item.Description,
                        Status = item.Status                       
                    });
                }


                transaction = Utilities.CalculateForPagerOfTransaction(transaction, totalRows,
                                                                        pageSize, currentPageNumber);
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

            return postDtoList;
        }

        /// <summary>
        /// The UpdatePost
        /// </summary>
        /// <param name="post">The obj<see cref="Post"/></param>
        /// <param name="transaction">The transaction<see cref="TransactionalInformation"/></param>
        public void UpdatePost(Post post, out TransactionalInformation transaction)
        {
            transaction = new TransactionalInformation();

            try
            {
                PostBusinessRules postBusinessRules = new PostBusinessRules();
                ValidationResult results = postBusinessRules.Validate(post);

                bool validationSucceeded = results.IsValid;
                IList<ValidationFailure> failures = results.Errors;

                if (validationSucceeded == false)
                {
                    transaction = ValidationErrors.PopulateValidationErrors(failures);
                    return;
                }

                _dataService.CreateSession();

                var param = post.ToDynamicParametersForUpdate();

                _dataService.PostRepository.Update(PostScript.Update, param, CommandType.Text);

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