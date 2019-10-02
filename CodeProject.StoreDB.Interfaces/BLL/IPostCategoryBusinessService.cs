using CodeProject.StoreDB.Common.Classes;
using CodeProject.StoreDB.Model.DTO;
using CodeProject.StoreDB.Model.Entities;
using System.Collections.Generic;

namespace CodeProject.StoreDB.Interfaces.BLL
{
    // IPostCategoryBusinessService
    public interface IPostCategoryBusinessService
    {
        PostCategory CreatePostCategory(PostCategory obj, out TransactionalInformation transaction);

        void UpdatePostCategory(PostCategory obj, out TransactionalInformation transaction);

        PostCategoryDTO GetPostCategory(object id, out TransactionalInformation transaction);

        void DeletePostCategory(object id, out TransactionalInformation transaction);

        List<PostCategoryDTO> GetAllPostCategories(out TransactionalInformation transaction);

        List<PostCategoryDTO> GetPostCategorysByPaging(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction);
    }
}