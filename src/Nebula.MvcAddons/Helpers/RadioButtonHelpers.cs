using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcAddons.Helpers {

    public static class RadioButtonHelpers {

        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper,
                                                                    string name,
                                                                    IEnumerable<SelectListItem> values) {
            //string name = ExpressionHelper.GetExpressionText(ex);
            var sb = new StringBuilder();
            int counter = 1;

            foreach (var item in values) {
                item.Selected = item.Value == (string)((SelectList)values).SelectedValue;

                var label = new TagBuilder("label");

                Dictionary<string, object> htmlAttributes = new Dictionary<string, object>();

                htmlAttributes.Add("id", name + counter.ToString());
                if (item.Selected) htmlAttributes.Add("checked", "checked");

                label.InnerHtml = string.Format("{0} {1}", htmlHelper.RadioButton(name, item.Value, htmlAttributes).ToString(), item.Text);

                label.Attributes.Add("for", name + counter.ToString());
                sb.Append(label.ToString());
                counter++;
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        //Author: Darin Dimitrov
        //http://stackoverflow.com/questions/5435748/radiobuttons-instead-of-dropdownlist-in-mvc-3-application
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                    Expression<Func<TModel, TProperty>> ex,
                                                                    IEnumerable<SelectListItem> values) {

            return RadioButtonList(htmlHelper, ExpressionHelper.GetExpressionText(ex), values);
        }
    }
}
