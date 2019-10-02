using CodeProject.StoreDB.Common.Classes;
using CodeProject.StoreDB.Model.DTO;
using CodeProject.StoreDB.Model.Entities;
using System.Collections.Generic;

namespace CodeProject.StoreDB.Interfaces.BLL
{
    // ITagBusinessService
    public interface ITagBusinessService
    {
        Tag CreateTag(Tag obj, out TransactionalInformation transaction);

        void UpdateTag(Tag obj, out TransactionalInformation transaction);

        TagDTO GetTag(object id, out TransactionalInformation transaction);

        void DeleteTag(object id, out TransactionalInformation transaction);

        List<TagDTO> GetTagsByPaging(int currentPageNumber, int pageSize, string sortExpression, string sortDirection, out TransactionalInformation transaction);
    }
}