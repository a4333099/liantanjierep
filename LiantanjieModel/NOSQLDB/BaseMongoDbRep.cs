using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB;

namespace LiantanjieModel.NOSQLDB
{
    public class BaseMongoDbRep<T> where T:class
    {
        private static readonly string _connectionString = "Server=127.0.0.1";
        private static readonly string _dbName = "Liantanjie";

    
        public static  void Insert(T type)
        {

           // type.Id = Guid.NewGuid().ToString("N");

            // 首先创建一个连接
            using (Mongo mongo = new Mongo(_connectionString))
            {

                // 打开连接
                mongo.Connect();

                // 切换到指定的数据库
                var db = mongo.GetDatabase(_dbName);

                // 根据类型获取相应的集合
                var collection = db.GetCollection<T>();

                // 向集合中插入对象
                collection.Insert(type);
            }
        }


       

        public static void Delete(Expression<Func<T, bool>> wherelambada)
        {
            using (Mongo mongo = new Mongo(_connectionString))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<T>();

                // 从集合中删除指定的对象
                collection.Remove(wherelambada);
            }
        }

        public static void Update(T customer, Expression<Func<T, bool>> wherelambada)
        {
            using (Mongo mongo = new Mongo(_connectionString))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<T>();

                // 更新对象
                collection.Update(customer, wherelambada);
            }
        }

     

        public static T GetEntity(Expression<Func<T, bool>> wherelambada)
        {
            using (Mongo mongo = new Mongo(_connectionString))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<T>();

                // 查询单个对象
               // return collection.FindOne(wherelambada);
                return collection.FindOne<T>(wherelambada);
            }
        }

        public static IEnumerable<T> GetEntities(Expression<Func<T, bool>> wherelambada)
        {
            using (Mongo mongo = new Mongo(_connectionString))
            {
                mongo.Connect();
                var db = mongo.GetDatabase(_dbName);
                var collection = db.GetCollection<T>();

                // 查询单个对象
                return collection.Linq().Where(wherelambada).ToList();
            }
        }

        #region MyRegion

        //public List<T> GetList(string searchWord, PagingInfo pagingInfo)
        //{
        //    using (Mongo mongo = new Mongo(_connectionString))
        //    {
        //        mongo.Connect();
        //        var db = mongo.GetDatabase(_dbName);
        //        var collection = db.GetCollection<Customer>();

        //        // 先创建一个查询
        //        var query = from customer in collection.Linq()
        //                    select customer;

        //        // 增加查询过滤条件
        //        if (string.IsNullOrEmpty(searchWord) == false)
        //            query = query.Where(c => c.CustomerName.Contains(searchWord) || c.Address.Contains(searchWord));

        //        // 先按名称排序，再返回分页结果.
        //        return query.OrderBy(x => x.CustomerName).GetPagingList<Customer>(pagingInfo);
        //    }
        //}

        //public static T GetById(string customerId)
        //{
        //    using (Mongo mongo = new Mongo(_connectionString))
        //    {
        //        mongo.Connect();
        //        var db = mongo.GetDatabase(_dbName);
        //        var collection = db.GetCollection<T>();

        //        // 查询单个对象
        //        return collection.FindOne(x => x.Id == customerId);
        //    }
        //}
        //public static void Delete(string customerId)
        //{
        //    using (Mongo mongo = new Mongo(_connectionString))
        //    {
        //        mongo.Connect();
        //        var db = mongo.GetDatabase(_dbName);
        //        var collection = db.GetCollection<T>();

        //        // 从集合中删除指定的对象
        //        collection.Remove(x => x.Id == customerId);
        //    }
        //}

        #endregion
    }
}
