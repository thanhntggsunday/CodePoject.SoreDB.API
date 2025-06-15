namespace CodeProject.StoreDB.DataService.UnitOfWork
{
    using CodeProject.Interfaces.DAL;
    using CodeProject.StoreDB.DataService.DapperRepository;
    using CodeProject.StoreDB.Interfaces.DAL;
    using CodeProject.StoreDB.Model.DTO;
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
        private IDapperRepositoryBase<ProductDTO> _productRepository;

        /// <summary>
        /// Defines the _cartItemRepository
        /// </summary>
        private IDapperRepositoryBase<CartItemDTO> _cartItemRepository;

        /// <summary>
        /// Defines the _orderRepository
        /// </summary>
        private IDapperRepositoryBase<OrderDTO> _orderRepository;

        /// <summary>
        /// Defines the _orderDetailRepository
        /// </summary>
        private IDapperRepositoryBase<OrderDetailDTO> _orderDetailRepository;

        /// <summary>
        /// Defines the _postCategoryRepository
        /// </summary>
        private IDapperRepositoryBase<PostCategoryDTO> _postCategoryRepository;

        /// <summary>
        /// Defines the _postRepository
        /// </summary>
        private IDapperRepositoryBase<PostDTO> _postRepository;

        /// <summary>
        /// Defines the _productCategoryRepository
        /// </summary>
        private IDapperRepositoryBase<ProductCategoryDTO> _productCategoryRepository;

        /// <summary>
        /// Defines the _tagRepository
        /// </summary>
        private IDapperRepositoryBase<TagDTO> _tagRepository;

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
        public IDapperRepositoryBase<ProductDTO> ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new DapperRepositoryBase<ProductDTO>(_transaction);
                }

                return _productRepository;
            }
        }

        /// <summary>
        /// Gets the CartItemRepository
        /// </summary>
        public IDapperRepositoryBase<CartItemDTO> CartItemRepository
        {
            get
            {
                if (_cartItemRepository == null)
                {
                    _cartItemRepository = new DapperRepositoryBase<CartItemDTO>(_transaction);
                }

                return _cartItemRepository;
            }
        }

        /// <summary>
        /// Gets the OrderRepository
        /// </summary>
        public IDapperRepositoryBase<OrderDTO> OrderRepository
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new DapperRepositoryBase<OrderDTO>(_transaction);
                }

                return _orderRepository;
            }
        }

        /// <summary>
        /// Gets the OrderDetailRepository
        /// </summary>
        public IDapperRepositoryBase<OrderDetailDTO> OrderDetailRepository
        {
            get
            {
                if (_orderDetailRepository == null)
                {
                    _orderDetailRepository = new DapperRepositoryBase<OrderDetailDTO>(_transaction);
                }

                return _orderDetailRepository;
            }
        }

        /// <summary>
        /// Gets the PostCategoryRepository
        /// </summary>
        public IDapperRepositoryBase<PostCategoryDTO> PostCategoryRepository
        {
            get
            {
                if (_postCategoryRepository == null)
                {
                    _postCategoryRepository = new DapperRepositoryBase<PostCategoryDTO>(_transaction);
                }

                return _postCategoryRepository;
            }
        }

        /// <summary>
        /// Gets the PostRepository
        /// </summary>
        public IDapperRepositoryBase<PostDTO> PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new DapperRepositoryBase<PostDTO>(_transaction);
                }

                return _postRepository;
            }
        }

        /// <summary>
        /// Gets the ProductCategoryRepository
        /// </summary>
        public IDapperRepositoryBase<ProductCategoryDTO> ProductCategoryRepository
        {
            get
            {
                if (_productCategoryRepository == null)
                {
                    _productCategoryRepository = new DapperRepositoryBase<ProductCategoryDTO>(_transaction);
                }

                return _productCategoryRepository;
            }
        }

        /// <summary>
        /// Gets the TagRepository
        /// </summary>
        public IDapperRepositoryBase<TagDTO> TagRepository
        {
            get
            {
                if (_tagRepository == null)
                {
                    _tagRepository = new DapperRepositoryBase<TagDTO>(_transaction);
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