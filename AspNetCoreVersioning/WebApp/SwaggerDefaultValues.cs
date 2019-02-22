using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace WebApp
{
    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated = apiDescription.IsDeprecated();

            if (null == operation.Parameters)
                return;

            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = apiDescription.ParameterDescriptions.First(x => x.Name == parameter.Name);

                if (null == parameter.Description)
                    parameter.Description = description.ModelMetadata?.Description;

                if (null == parameter.Default)
                    parameter.Default = description.DefaultValue;

                parameter.Required |= description.IsRequired;
            }
        }
    }
}