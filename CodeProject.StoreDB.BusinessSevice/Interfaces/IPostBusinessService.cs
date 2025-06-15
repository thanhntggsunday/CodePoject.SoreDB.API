using CodeProject.StoreDB.Common.Classes;
using CodeProject.StoreDB.Model.DTO;
using CodeProject.StoreDB.Model.Entities;
using System.Collections.Generic;

namespace CodeProject.StoreDB.Interfaces.BLL
{
    // IPostBusinessService
    public interface IPostBusinessService
    {
        Post CreatePost(Post obj, out TransactionalInformation transaction);

        void UpdatePost(Post obj, out TransactionalInformation transaction);

        PostDTO GetPost(object id, out TransactionalInformation transaction);

        void DeletePost(object id, out TransactionalInformation transaction);

        List<PostDTO> GetPostsByPaging(int currentPageNumber, int pageSize, out TransactionalInformation transaction);
    }
}