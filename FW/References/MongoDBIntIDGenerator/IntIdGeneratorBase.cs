namespace MongoDBIntIdGenerator
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    /// <summary>
    /// Base class for id generator based on integer values.
    /// </summary>
    public abstract class IntIdGeneratorBase : IIdGenerator
    {
        #region Fields

        private string m_idCollectionName;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBIntIdGenerator.IntIdGeneratorBase"/> class.
        /// </summary>
        /// <param name="idCollectionName">Identifier collection name.</param>
        protected IntIdGeneratorBase(string idCollectionName)
        {
            m_idCollectionName = idCollectionName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoDBIntIdGenerator.IntIdGeneratorBase"/> class.
        /// </summary>
        protected IntIdGeneratorBase()
            : this("IDs")
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Generates an Id for a document.
        /// </summary>
        /// <param name="container">The container of the document (will be a MongoCollection when called from the C# driver).</param>
        /// <param name="document">The document.</param>
        /// <returns>An Id.</returns>
        public object GenerateId(object container, object document)
        {
            var idSequenceCollection = ((MongoCollection)container).Database
                .GetCollection(m_idCollectionName);

            var query = Query.EQ("_id", ((MongoCollection)container).Name);

            return ConvertToInt(idSequenceCollection.FindAndModify(new FindAndModifyArgs()
                    {
                        Query = query,
                        Update = CreateUpdateBuilder(),
                        VersionReturned = FindAndModifyDocumentVersion.Modified,
                        Upsert = true
                    }).ModifiedDocument["seq"]);
        }

        /// <summary>
        /// Tests whether an Id is empty.
        /// </summary>
        /// <param name="id">The Id.</param>
        /// <returns>True if the Id is empty.</returns>
        public abstract bool IsEmpty(object id);

        /// <summary>
        /// Converts to int.
        /// </summary>
        /// <returns>The to int.</returns>
        /// <param name="value">Value.</param>
        protected abstract object ConvertToInt(BsonValue value);

        /// <summary>
        /// Creates the update builder.
        /// </summary>
        /// <returns>The update builder.</returns>
        protected abstract UpdateBuilder CreateUpdateBuilder();

        #endregion Methods
    }
}