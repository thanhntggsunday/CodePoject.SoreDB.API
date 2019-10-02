using CodeProject.StoreDB.Interfaces.DAL;
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

        IDapperRepositoryBase<Product> ProductRepository { get; }
        IDapperRepositoryBase<CartItem> CartItemRepository { get; }
        IDapperRepositoryBase<Order> OrderRepository { get; }
        IDapperRepositoryBase<OrderDetail> OrderDetailRepository { get; }
        IDapperRepositoryBase<PostCategory> PostCategoryRepository { get; }
        IDapperRepositoryBase<Post> PostRepository { get; }
        IDapperRepositoryBase<ProductCategory> ProductCategoryRepository { get; }
        IDapperRepositoryBase<Tag> TagRepository { get; }
    }
}