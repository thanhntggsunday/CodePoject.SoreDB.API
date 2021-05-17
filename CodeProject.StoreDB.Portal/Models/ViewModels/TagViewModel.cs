namespace CodeProject.StoreDB.Portal.Classes.ViewModels
{
    using CodeProject.StoreDB.Common.Classes;

    /// <summary>
    /// Defines the <see cref="TagViewModel" />
    /// </summary>
    public class TagViewModel : TransactionalInformation
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public string Type { get; set; }
    }
}
