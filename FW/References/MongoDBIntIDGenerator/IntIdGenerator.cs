using System;

namespace MongoDBIntIdGenerator
{
    /// <summary>
    /// Obsolete Int32 identifier generator. 
    /// </summary>
    public class IntIdGenerator : Int32IdGenerator
    {
        private static readonly Lazy<IntIdGenerator> _instance = new Lazy<IntIdGenerator>(() => new IntIdGenerator());

        private IntIdGenerator()
        { }

        public static IntIdGenerator Instance
        {
            get
            {
                return _instance.Value;
            }
        }
    }
}