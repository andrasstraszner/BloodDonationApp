using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString RequiredLabelFor<T, U>(this HtmlHelper<T> helper, Expression<Func<T, U>> expression, object htmlAttributes)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metaData.DisplayName
                ?? metaData.PropertyName
                ?? htmlFieldName.Split('.').Last();

            if (metaData.IsRequired)
            {
                labelText += "*";
            }

            return LabelExtensions.LabelFor(helper, expression, labelText, htmlAttributes);
        }
    }
}