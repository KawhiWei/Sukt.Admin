using System;

namespace Sukt.Core.Shared.Audit
{
    /// <summary>
    /// MongoDb表名称特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MongoDBTableAttribute : Attribute
    {
        public MongoDBTableAttribute(string tablename)
        {
            TableName = tablename;
        }

        /// <summary>
        /// MongoDB表名称
        /// </summary>
        public string TableName { get; }
    }
}