using CodeProject.StoreDB.Interfaces.DAL;
using CodeProject.StoreDB.Model.DTO;
using CodeProject.StoreDB.Model.Entities;
using System;

namespace CodeProject.Interfaces.DAL
{
    public partial interface IUnitOfWork : IDisposable
    {
        void CreateSession();

        // void BeginTransaction();
        void CommitTransaction(Boolean closeSession);

        // void RollbackTransaction(Boolean closeSession);
        void CloseSession();

        IDapperRepositoryBase<ProductDTO> ProductRepository { get; }
        IDapperRepositoryBase<CartItemDTO> CartItemRepository { get; }
        IDapperRepositoryBase<OrderDTO> OrderRepository { get; }
        IDapperRepositoryBase<OrderDetailDTO> OrderDetailRepository { get; }
        IDapperRepositoryBase<PostCategoryDTO> PostCategoryRepository { get; }
        IDapperRepositoryBase<PostDTO> PostRepository { get; }
        IDapperRepositoryBase<ProductCategoryDTO> ProductCategoryRepository { get; }
        IDapperRepositoryBase<TagDTO> TagRepository { get; }
    }
}