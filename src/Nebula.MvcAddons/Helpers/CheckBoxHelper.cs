using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Nebula.MvcAddons.Helpers {
    public static class CheckBoxHelper {

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper,
                                                    string name,
                                                    IEnumerable<SelectListItem> values) {
            return CheckBoxList(htmlHelper, name, values, null);
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper htmlHelper, 
                                                    string name,
                                                    IEnumerable<SelectListItem> values,
                                                    object htmlAttributes) {

            var sb = new StringBuilder();
            int counter = 1;

            foreach (var item in values) {
                item.Selected = item.Value == (string)((SelectList)values).SelectedValue;

                var label = new TagBuilder("label");

                Dictionary<string, object> htmlAttributesLocal = new Dictionary<string, object>();

                htmlAttributesLocal.Add("id", name + counter.ToString());
                if (item.Selected) htmlAttributesLocal.Add("checked", "checked");

                var input = new TagBuilder("input");

                if (htmlAttributes != null) input.MergeAttributes(new RouteValueDictionary(htmlAttributes));
                
                input.MergeAttribute("id", name + counter.ToString());
                
                input.MergeAttribute("type", "checkbox", true);
                input.MergeAttribute("name", name, true);
                input.MergeAttribute("value", item.Value, true);
                if (item.Selected) input.MergeAttribute("checked", "checked");

                label.InnerHtml = string.Format("{0} {1}", input.ToString(TagRenderMode.SelfClosing), item.Text);

                label.Attributes.Add("for", name + counter.ToString());
                sb.Append(label.ToString());
                counter++;
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                    Expression<Func<TModel, TProperty>> ex,
                                                    IEnumerable<SelectListItem> values) {

            return CheckBoxList(htmlHelper, ExpressionHelper.GetExpressionText(ex), values, null);
        }

        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                    Expression<Func<TModel, TProperty>> ex,
                                                    IEnumerable<SelectListItem> values,
                                                    object htmlAttributes) {

            return CheckBoxList(htmlHelper, ExpressionHelper.GetExpressionText(ex), values, htmlAttributes);
        }
    }
}
