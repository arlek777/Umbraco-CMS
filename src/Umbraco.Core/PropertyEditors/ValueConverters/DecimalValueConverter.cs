﻿using System;
using System.Globalization;
using Umbraco.Core.Models.PublishedContent;

namespace Umbraco.Core.PropertyEditors.ValueConverters
{
    [DefaultPropertyValueConverter]
    public class DecimalValueConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
            => Constants.PropertyEditors.DecimalAlias.Equals(propertyType.PropertyEditorAlias);

        public override Type GetPropertyValueType(PublishedPropertyType propertyType)
            => typeof (decimal);

        public override PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType)
            => PropertyCacheLevel.Content;

        public override object ConvertSourceToInter(IPropertySet owner, PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null) return 0M;

            // in XML a decimal is a string
            if (source is string sourceString)
            {
                return decimal.TryParse(sourceString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal d) ? d : 0M;
            }

            // in the database an a decimal is an a decimal
            // default value is zero
            return source is decimal ? source : 0M;
        }
    }
}
