using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using DatabaseAccess.SpExecuters;

namespace DatabaseAccess.Repository
{
    /// <summary>
    /// Class for managing data
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// Stored procedure executer
        /// </summary>
        private ISpExecuter _spExecuter;

        /// <summary>
        /// Mapping information
        /// </summary>
        private MapInfo _mapInfo;

        /// <summary>
        /// Creates new instance of <see cref="DataManager"/>
        /// </summary>
        /// <param name="spExecuter">Stored procedure exucuter</param>
        /// <param name="mapInfo">Mapping information</param>
        public DataManager(SpExecuter spExecuter, MapInfo mapInfo)
        {
            // setting fields
            this._spExecuter = spExecuter;
            this._mapInfo = mapInfo;
        }

        /// <summary>
        /// Operates the action.
        /// </summary>
        /// <typeparam name="TResult">Type of resuly</typeparam>
        /// <param name="operationName">Operation mapped name.</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Result</returns>
        public object Operate<TResult>(string operationName, IEnumerable<KeyValuePair<string, object>> parameters = null)
            where TResult : class
        {
            // getting operation info
            var operationInfo = this.GetOperationInfo(operationName);

            // getting parameters
            var spParams = null as IEnumerable<KeyValuePair<string, object>>;

            // constructing parameters
            if (parameters != null)
                spParams = this.ConstructParameters(operationInfo.ParametersMappInfo, parameters).ToList();
            else spParams = parameters;

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

        /// <summary>
        /// Operates the action asynchronosuly
        /// </summary>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="operationName">Operation name</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>Result</returns>
        public Task<object> OperateAsync<TResult>(string operationName, IEnumerable<KeyValuePair<string, object>> parameters = null)
            where TResult : class
        {
            // creating task
            var task = new Task<object>(() => this.Operate<TResult>(operationName, parameters));

            // starting task
            task.Start();

            // returning task
            return task;
        }

        /// <summary>
        /// Operates the action
        /// </summary>
        /// <typeparam name="TParamater">Type of complex parameter.</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="operationName">Operation name</param>
        /// <param name="paramater">Parameters</param>
        /// <returns>Result</returns>
        public object Operate<TParamater,TResult>(string operationName,TParamater paramater)
            where TResult:class
        {
            // getting parameters using reflection
            var parameters = this.GetParameters(paramater);

            // returning result
            return this.Operate<TResult>(operationName, parameters);
        }

        /// <summary>
        /// Operates the action asynchronously
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter</typeparam>
        /// <typeparam name="TResult">Type of result</typeparam>
        /// <param name="operationName">Operation name</param>
        /// <param name="parameter">Parameter</param>
        /// <returns>result</returns>
        public Task<object> OperateAsync<TParameter,TResult>(string operationName,TParameter parameter)
            where TResult:class
        {
            // creating task
            var task = new Task<object>(() => this.Operate<TParameter,TResult>(operationName, parameter));

            // starting task
            task.Start();

            // returning task
            return task;
        }

        /// <summary>
        /// Gets operation information
        /// </summary>
        /// <param name="operationName">Operation name</param>
        /// <returns>operation information</returns>
        private OperationInfo GetOperationInfo(string operationName)
        {
            // constructing and returning operation information
            return new OperationInfo
            {
                Name = operationName,
                SpName = this._mapInfo.OpNames[operationName],
                ReturnDataType = this._mapInfo.ReturnValues[operationName],
                ParametersMappInfo = this._mapInfo.Parameters[operationName]
            };
        }

        /// <summary>
        /// Gets parameters using mapping information
        /// </summary>
        /// <param name="mapInfo">Mapping information</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>result</returns>
        private IEnumerable<KeyValuePair<string, object>> ConstructParameters(
            Dictionary<string, string> mapInfo, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            // returning parameters
            return parameters.Select(kv =>
                    new KeyValuePair<string, object>(mapInfo[kv.Key], kv.Value));
        }

        /// <summary>
        /// Gets properties as operation parameters from complex parameter using reflection
        /// </summary>
        /// <typeparam name="TParameter">Type of parameter</typeparam>
        /// <param name="entity">entity</param>
        /// <returns>parameters</returns>
        private IEnumerable<KeyValuePair<string, object>> GetParameters<TParameter>(TParameter parameter)
        {
            // getting type of complex parameter
            var type = parameter.GetType();

            // checking if parameter has primitive type
            // note that for DataManager primitive types are not only .NET primitive types but also string and decimal
            if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
            {
                return new[]
                {
                    new KeyValuePair<string,object>("primitive",parameter)
                };
            }

            // getting properties
            var properties = parameter.GetType().GetProperties();

            // list of parameters
            var parameters = new List<KeyValuePair<string, object>>();

            // loop over properties
            foreach (var property in properties)
            {
                // getting property type
                var propertyType = property.PropertyType;

                // if property doesn't have primitive type
                if (!propertyType.IsPrimitive && propertyType != typeof(string) && propertyType != typeof(decimal))
                {
                    // getting properties of property
                    var propProperties = propertyType.GetProperties();

                    // adding paramaters
                    parameters.AddRange(
                        propProperties.Select(propProperty =>
                        KeyValuePair.Create(propProperty.Name, propProperty.GetValue(property.GetValue(parameter)))));
                }
                else parameters.Add(KeyValuePair.Create(property.Name, property.GetValue(parameter)));
            }

            // returning parameters
            return parameters;
        }
    }
}