namespace CodeProject.StoreDB.DataService.UnitOfWork
{
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.DataService.DapperRepository;
    using CodeProject.StoreDB.Interfaces.DAL;
    using CodeProject.StoreDB.Model.Entities;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="UnitOfWork" />
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private bool _disposed;

        /// <summary>
        /// Defines the _productDataServiceRepository
        /// </summary>
        private IDapperRepositoryBase<Product> _productRepository;

        /// <summary>
        /// Defines the _cartItemRepository
        /// </summary>
        private IDapperRepositoryBase<CartItem> _cartItemRepository;

        /// <summary>
        /// Defines the _orderRepository
        /// </summary>
        private IDapperRepositoryBase<Order> _orderRepository;

        /// <summary>
        /// Defines the _orderDetailRepository
        /// </summary>
        private IDapperRepositoryBase<OrderDetail> _orderDetailRepository;

        /// <summary>
        /// Defines the _postCategoryRepository
        /// </summary>
        private IDapperRepositoryBase<PostCategory> _postCategoryRepository;

        /// <summary>
        /// Defines the _postRepository
        /// </summary>
        private IDapperRepositoryBase<Post> _postRepository;

        /// <summary>
        /// Defines the _productCategoryRepository
        /// </summary>
        private IDapperRepositoryBase<ProductCategory> _productCategoryRepository;

        /// <summary>
        /// Defines the _tagRepository
        /// </summary>
        private IDapperRepositoryBase<Tag> _tagRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        public UnitOfWork()
        {
            CreateSession();
        }

        /// <summary>
        /// Gets the ProductRepository
        /// </summary>
        public IDapperRepositoryBase<Product> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new DapperRepositoryBase<Product>(_transaction);
                }

                return _productRepository;
            }
        }

        /// <summary>
        /// Gets the CartItemRepository
        /// </summary>
        public IDapperRepositoryBase<CartItem> CartItemRepository
        {
            get
            {
                if (_cartItemRepository == null)
                {
                    _cartItemRepository = new DapperRepositoryBase<CartItem>(_transaction);
                }

                return _cartItemRepository;
            }
        }

        /// <summary>
        /// Gets the OrderRepository
        /// </summary>
        public IDapperRepositoryBase<Order> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new DapperRepositoryBase<Order>(_transaction);
                }

                return _orderRepository;
            }
        }

        /// <summary>
        /// Gets the OrderDetailRepository
        /// </summary>
        public IDapperRepositoryBase<OrderDetail> OrderDetailRepository
        {
            get
            {
                if (_orderDetailRepository == null)
                {
                    _orderDetailRepository = new DapperRepositoryBase<OrderDetail>(_transaction);
                }

                return _orderDetailRepository;
            }
        }

        /// <summary>
        /// Gets the PostCategoryRepository
        /// </summary>
        public IDapperRepositoryBase<PostCategory> PostCategoryRepository
        {
            get
            {
                if (_postCategoryRepository == null)
                {
                    _postCategoryRepository = new DapperRepositoryBase<PostCategory>(_transaction);
                }

                return _postCategoryRepository;
            }
        }

        /// <summary>
        /// Gets the PostRepository
        /// </summary>
        public IDapperRepositoryBase<Post> PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new DapperRepositoryBase<Post>(_transaction);
                }

                return _postRepository;
            }
        }

        /// <summary>
        /// Gets the ProductCategoryRepository
        /// </summary>
        public IDapperRepositoryBase<ProductCategory> ProductCategoryRepository
        {
            get
            {
                if (_productCategoryRepository == null)
                {
                    _productCategoryRepository = new DapperRepositoryBase<ProductCategory>(_transaction);
                }

                return _productCategoryRepository;
            }
        }

        /// <summary>
        /// Gets the TagRepository
        /// </summary>
        public IDapperRepositoryBase<Tag> TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new DapperRepositoryBase<Tag>(_transaction);
                }

                return _tagRepository;
            }
        }

        /// <summary>
        /// The CommitTransaction
        /// </summary>
        /// <param name="closeSession">The closeSession<see cref="Boolean"/></param>
        public void CommitTransaction(Boolean closeSession = true)
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                // _transaction = _connection.BeginTransaction();
                // resetRepositories();
            }
        }

        /// <summary>
        /// The CreateSession
        /// </summary>
        public void CreateSession()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApplicationDbContext"].ToString();

            ResetRepositories();

            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// The CloseSession
        /// </summary>
        public void CloseSession()
        {
            ResetRepositories();
            CloseConnection();

            this.Dispose();
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        CloseConnection();

                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection.Dispose();
                        SqlConnection.ClearAllPools();

                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        private void CloseConnection()
        {
            if (_transaction != null && _transaction.Connection != null)
            {
                _transaction.Connection.Close();
            }
        }

        private void ResetRepositories()
        {
            _cartItemRepository = null;
            _orderDetailRepository = null;
            _orderRepository = null;
            _postCategoryRepository = null;
            _postRepository = null;
            _productCategoryRepository = null;
            _productRepository = null;
            _tagRepository = null;
        }

        ~UnitOfWork()
        {
            dispose(false);
        }
    }
}