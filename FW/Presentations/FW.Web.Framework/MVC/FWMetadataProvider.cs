namespace FW.Web.Framework.MVC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class FWMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        #region Methods

        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            var additionalValues = attributes.OfType<IModelAttribute>().ToList();
            foreach (var additionalValue in additionalValues)
            {
                if (!metadata.AdditionalValues.ContainsKey(additionalValue.Name))
                    metadata.AdditionalValues.Add(additionalValue.Name, additionalValue);
            }

            return metadata;
        }

        #endregion Methods
    }
}