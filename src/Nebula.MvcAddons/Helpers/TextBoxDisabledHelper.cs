using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcAddons.Helpers {
    public static partial class TextBoxDisabledHelper {

        public static MvcHtmlString TextBoxDisabledFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                Expression<Func<TModel, TProperty>> expression) {

            return TextBoxDisabledFor<TModel, TProperty>(htmlHelper, expression, null, null);
        }

        public static MvcHtmlString TextBoxDisabledFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                Expression<Func<TModel, TProperty>> expression,
                                                                string regexPattern) {

            return TextBoxDisabledFor<TModel, TProperty>(htmlHelper, expression, regexPattern, null);
        }

        public static MvcHtmlString TextBoxDisabledFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                Expression<Func<TModel, TProperty>> expression,
                                                                object htmlAttributes) {

            return TextBoxDisabledFor<TModel, TProperty>(htmlHelper, expression, null, htmlAttributes);
        }

        public static MvcHtmlString TextBoxDisabledFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
                                                                Expression<Func<TModel, TProperty>> expression, 
                                                                string regexPattern,
                                                                object htmlAttributes) {

            string fieldName = expression.Body.ToString().Substring(expression.Body.ToString().IndexOf('.', 0) + 1);
            object value = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            Regex regex = new Regex(string.IsNullOrEmpty(regexPattern) ? "^.*$" : regexPattern);
            string valueString = value != null ? value.ToString().Trim() : string.Empty;
            bool hasValidValue = (!string.IsNullOrEmpty(valueString) && regex.IsMatch(valueString));

            TagBuilder tagBuiler = new TagBuilder("input");            
            
            if (htmlAttributes != null) tagBuiler.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            tagBuiler.MergeAttribute("id", fieldName.Replace('.', '_'), true);
            tagBuiler.MergeAttribute("name", fieldName, true);
            tagBuiler.MergeAttribute("value", valueString, true);

            if (!tagBuiler.Attributes.ContainsKey("type")) tagBuiler.MergeAttribute("type", hasValidValue ? "hidden" : "text", true);
            if (!string.IsNullOrEmpty(regexPattern)) tagBuiler.MergeAttribute("pattern", regexPattern, true);

            return MvcHtmlString.Create(string.Format("{0}{1}", tagBuiler.ToString(TagRenderMode.SelfClosing), hasValidValue ? valueString : string.Empty));
        }
    }
}