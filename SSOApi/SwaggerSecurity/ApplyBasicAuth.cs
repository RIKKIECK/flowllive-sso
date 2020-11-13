using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace SSOApi.SwaggerSecurity
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplyBasicAuth : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public ApplyBasicAuth()
        {
            Name = "basic";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var basicAuthDict = new Dictionary<string, IEnumerable<string>>();
            basicAuthDict.Add(Name, new List<string>());
            operation.security = new IDictionary<string, IEnumerable<string>>[] { basicAuthDict };
        }
    }
}