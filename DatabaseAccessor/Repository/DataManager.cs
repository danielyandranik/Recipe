using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseAccess.Repository
{
    public class DataManager
    {
        private ISpExecuter _spExecuter;

        private MapInfo _mapInfo;

        public DataManager(ISpExecuter spExecuter, MapInfo mapInfo)
        {
            this._spExecuter = spExecuter;
            this._mapInfo = mapInfo;
        }

        public object Operate<TResult>(string operationName, IEnumerable<KeyValuePair<string, object>> parameters = null)
            where TResult : class
        {
            // getting operation info
            var operationInfo = this.GetOperationInfo(operationName);

            // getting parameters
            var spParams = null as IEnumerable<KeyValuePair<string, object>>;

            if (parameters != null)
                spParams = this.ConstructParameters(operationInfo.ParametersMappInfo, parameters).ToList();
            else spParams = parameters.ToList();

            // executing specific operation
            if (operationInfo.ReturnDataType == ReturnDataType.Entity)
                return this._spExecuter.ExecuteEntitySp<TResult>(operationInfo.SpName, spParams);
            else if (operationInfo.ReturnDataType == ReturnDataType.Enumerable)
                return this._spExecuter.ExecuteSp<TResult>(operationInfo.SpName, spParams);
            else if (operationInfo.ReturnDataType == ReturnDataType.Scalar)
                return this._spExecuter.ExecuteScalarSp<object>(operationInfo.SpName, spParams);
            else
                return this._spExecuter.ExecuteSpNonQuery(operationInfo.SpName, spParams);
        }

        public object Operate<TResult>(string operationName, TResult entity)
            where TResult : class
        {
            var parameters = this.GetParameters<TResult>(entity);

            return this.Operate<TResult>(operationName, parameters);
        }

        public Task<object> OperateAsync<TResult>(string operationName, IEnumerable<KeyValuePair<string, object>> parameters = null)
            where TResult : class
        {
            var task = new Task<object>(() =>
            {
                return this.Operate<TResult>(operationName, parameters);
            });

            task.Start();

            return task;
        }

        public Task<object> OperateAsync<TResult>(string operationName, TResult entity)
            where TResult : class
        {
            var parametes = this.GetParameters<TResult>(entity);

            return this.OperateAsync<TResult>(operationName, parametes);
        }

        private OperationInfo GetOperationInfo(string operationName)
        {
            return new OperationInfo
            {
                Name = operationName,
                SpName = this._mapInfo.OpNames[operationName],
                ReturnDataType = this._mapInfo.ReturnValues[operationName],
                ParametersMappInfo = this._mapInfo.Parameters[operationName]
            };
        }

        private IEnumerable<KeyValuePair<string, object>> ConstructParameters(
            Dictionary<string, string> mapInfo, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            return parameters.Select(kv =>
                    new KeyValuePair<string, object>(mapInfo[kv.Key], kv.Value));
        }

        private IEnumerable<KeyValuePair<string, object>> GetParameters<TResult>(TResult entity)
        {
            var type = entity.GetType();

            if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
            {
                return new[]
                {
                    new KeyValuePair<string,object>("primitive",entity)
                };
            }

            var properties = entity.GetType().GetProperties();

            return properties.Select(property =>
                    KeyValuePair.Create(property.Name, property.GetValue(entity)));
        }
    }
}
