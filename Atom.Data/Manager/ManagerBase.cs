using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Atom.Data.Criteria;
using Atom.Domain.Attribute;
using System.Reflection;
using Atom.Domain.Configuration;
using System;
using Dapper.Contrib.Extensions;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Atom.Data.BaseClass;

namespace Atom.Data.Manager
{
    public abstract class ManagerBase
    {
        #region Private Variables
        private DatabaseConfiguration DBConfig { get; init; } = new DatabaseConfiguration();
        private IMapper Mapper { get; init; }
        #endregion

        #region Constructors
        public ManagerBase(IServiceProvider provider)
        {
            IConfiguration config = provider.GetService<IConfiguration>();
            config.GetSection(DatabaseConfiguration.DatabaseSection).Bind(DBConfig);
            Mapper = provider.GetService<IMapper>();
        }
        #endregion

        #region Read
        protected IEnumerable<TModel?> ReadMany<TModel, TEntity, TCriteria>(TCriteria criteria) where TModel : ModelBase where TCriteria : CriteriaBase where TEntity : EntityBase
        {
            var task = ReadManyAsync<TModel, TEntity, TCriteria>(criteria: criteria);

            return task.Result;
        }

        protected TModel? ReadSingle<TModel, TEntity, TCriteria>(TCriteria criteria) where TModel : ModelBase where TCriteria : CriteriaBase where TEntity : EntityBase
        {
            var task = ReadSingleAsync<TModel, TEntity, TCriteria>(criteria: criteria);

            return task?.Result;
        }

        protected async Task<IEnumerable<TModel?>> ReadManyAsync<TModel, TEntity, TCriteria>(TCriteria criteria) where TModel : ModelBase where TCriteria : CriteriaBase where TEntity : EntityBase
        {
            using IDbConnection connection = new SqlConnection(DBConfig.ConnectionString);

            DataAccess dataAccess = criteria.GetType().GetCustomAttribute<DataAccess>();

            CommandType accessMethod;

            string storedProc;

            AccessParams(criteria, out accessMethod, out storedProc);

            var result = await Task.Run(() => connection.QueryAsync<TEntity>(storedProc, criteria, commandType: accessMethod));

            IEnumerable<TModel> models = Mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(result);

            return models;
        }

        protected async Task<TModel?> ReadSingleAsync<TModel, TEntity, TCriteria>(TCriteria criteria) where TModel : ModelBase where TCriteria : CriteriaBase where TEntity : EntityBase
        {
            using IDbConnection connection = new SqlConnection(DBConfig.ConnectionString);

            CommandType accessMethod;

            string storedProc;

            AccessParams(criteria, out accessMethod, out storedProc);

            var result = await connection.QueryFirstOrDefaultAsync<TEntity>(storedProc, criteria, commandType: accessMethod);

            TModel model = Mapper.Map<TEntity, TModel>(result);

            return model;
        }
        #endregion

        #region Create
        protected async Task CreateSingleAsync<TModel, TEntity, TCriteria>(TCriteria criteria) where TModel : ModelBase where TCriteria : CriteriaBase where TEntity : EntityBase
        {
            using IDbConnection connection = new SqlConnection(DBConfig.ConnectionString);

            CommandType accessMethod;

            string storedProc;

            AccessParams(criteria, out accessMethod, out storedProc);

            await connection.ExecuteAsync(sql: storedProc, param: criteria, commandType: accessMethod);
        }

        protected void CreateSingle<TModel, TEntity, TCriteria>(TCriteria criteria) where TModel : ModelBase where TCriteria : CriteriaBase where TEntity : EntityBase
        {
            var task = CreateSingleAsync<TModel, TEntity, TCriteria>(criteria: criteria);
        }

        protected long CreateMany<TModel, TEntity>(IEnumerable<TModel> model) where TModel : ModelBase where TEntity : EntityBase
        {
            IEnumerable<TEntity> entity = Mapper.Map<IEnumerable<TEntity>>(model);
            using (SqlConnection conn = new SqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                return conn.Insert(entity);
            }
        }

        protected void CreateSingle<TModel, TEntity>(TModel model) where TModel : ModelBase where TEntity : EntityBase
        {
            var entity = Mapper.Map<TModel, TEntity>(model);
            
            using (SqlConnection conn = new SqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                conn.Insert(entity);
            }
        }
        #endregion

        #region Update
        protected bool UpdateMany<TModel, TEntity>(IEnumerable<TModel> models) where TModel : ModelBase where TEntity: EntityBase
        {
            IEnumerable<TEntity> entities = Mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(models);

            using (SqlConnection conn = new SqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                return conn.Update(entities);
            }
        }

        protected bool UpdateSingle<TModel, TEntity>(TModel model) where TModel : ModelBase where TEntity : EntityBase
        {

            //var obj = BuildUpdateObj<TModel, TEntity>(model);
            TEntity entity = Mapper.Map<TModel, TEntity>(model);
            using (SqlConnection conn = new SqlConnection(DBConfig.ConnectionString))
            {
                conn.Open();
                model.UpdatedDateTime = DateTime.Now;
                return conn.Update(entity);
            }
        }
        #endregion

        private static void AccessParams<TCriteria>(TCriteria criteria, out CommandType accessMethod, out string storedProc) where TCriteria : CriteriaBase
        {
            DataAccess dataAccess = criteria.GetType().GetCustomAttribute<DataAccess>();

            accessMethod = (CommandType)Enum.ToObject(typeof(CommandType), dataAccess.AccessMethod);
            storedProc = dataAccess.Name;
        }

        //private object BuildUpdateObj<TModel, TEntity>(TModel model) where TModel : ModelBase where TEntity : EntityBase
        //{
        //    var props = typeof(TEntity).GetProperties();
        //    List<PropertyInfo> updateProps = new();

        //    foreach (var p in props)
        //    {
        //        if (Attribute.IsDefined(p, typeof(KeyAttribute)))
        //        {
        //            updateProps.Add(p);
        //        }

        //        if (Attribute.IsDefined(p, typeof(DataOperation)))
        //        {
        //            var attr = p.GetCustomAttributes<DataOperation>();

        //            foreach (DataOperation d in attr)
        //            {
        //                if (d.Operation.Equals(Operation.Update))
        //                {
        //                    updateProps.Add(p);
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    if (updateProps.Any())
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        StringWriter sw = new StringWriter(sb);

        //        using (JsonWriter writer = new JsonTextWriter(sw))
        //        {
        //            writer.WriteStartObject();

        //            foreach (PropertyInfo p in updateProps)
        //            {
        //                var modelProp = model.GetType().GetProperty(p.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.CreateInstance);

        //                writer.WritePropertyName(modelProp.Name);
        //                writer.WriteValue(modelProp.GetValue(model, null));
        //            }

        //            writer.WriteEndObject();
        //        }

        //        object obj = JsonConvert.DeserializeObject<object>(sb.ToString());
        //        return obj;
        //    }

        //    return null;
        //}
    }
}
