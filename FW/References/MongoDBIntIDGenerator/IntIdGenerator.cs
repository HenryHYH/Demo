namespace MongoDBIntIdGenerator
{
    using System;

    /// <summary>
    /// Obsolete Int32 identifier generator. 
    /// </summary>
    public class IntIdGenerator : Int32IdGenerator
    {
        #region Fields

        private static readonly Lazy<IntIdGenerator> _instance = new Lazy<IntIdGenerator>(() => new IntIdGenerator());

        #endregion Fields

        #region Constructors

        private IntIdGenerator()
        {
        }

        #endregion Constructors

        #region Properties

        public static IntIdGenerator Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        #endregion Properties
    }
}