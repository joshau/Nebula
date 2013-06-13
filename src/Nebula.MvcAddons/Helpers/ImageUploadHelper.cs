using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace MvcAddons.Helpers {
    public static class ImageUploadHelper {
        
        public static MvcHtmlString ImageUploadFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
                                                                Expression<Func<TModel, TProperty>> expression, 
                                                                object htmlAttributes, 
                                                                string baseRelativeUrl) {

            TagBuilder uploadBuilder, previewLinkBuilder, hiddenFieldBuilder;
            string uploadControlFieldName, fieldValue, imagePreviewFieldId, imagePreviewFieldName;
            MvcHtmlString uploadControlHtml, previewLinkHtml, hiddenFieldHtml;

            object fieldValueModel = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            fieldValue = fieldValueModel == null ? "" : fieldValueModel.ToString();
            dynamic a;
            
            if (string.IsNullOrEmpty(fieldValue)) {
                uploadControlFieldName = expression.Body.ToString().Substring(expression.Body.ToString().LastIndexOf(".") + 1) + "_FileUpload";
                uploadBuilder = new TagBuilder("input");
                if (htmlAttributes != null)
                    uploadBuilder.MergeAttributes( new RouteValueDictionary(htmlAttributes));
                
                uploadBuilder.MergeAttribute("id", uploadControlFieldName, true);
                uploadBuilder.MergeAttribute("name", uploadControlFieldName, true);
                uploadBuilder.MergeAttribute("type", "file", true);
                uploadControlHtml = MvcHtmlString.Create(uploadBuilder.ToString(TagRenderMode.SelfClosing));

                previewLinkHtml = MvcHtmlString.Create("");
            }
            else {
                uploadControlHtml = MvcHtmlString.Create("");               

                previewLinkBuilder = new TagBuilder("a");
                previewLinkBuilder.MergeAttribute("href", baseRelativeUrl + (fieldValue.StartsWith("~") ? fieldValue.TrimStart('~') : fieldValue));
                previewLinkBuilder.MergeAttribute("target", "_blank");
                previewLinkBuilder.SetInnerText("Preview");
                previewLinkHtml = MvcHtmlString.Create(previewLinkBuilder.ToString());
            }

            imagePreviewFieldId = expression.Body.ToString().Replace(expression.Parameters.First().Name + ".", "").Replace(".", "_");
            imagePreviewFieldName = expression.Body.ToString().Replace(expression.Parameters.First().Name + ".", "");
            hiddenFieldBuilder = new TagBuilder("input");
            hiddenFieldBuilder.MergeAttribute("type", "hidden", true);
            hiddenFieldBuilder.MergeAttribute("id", imagePreviewFieldId, true);
            hiddenFieldBuilder.MergeAttribute("name", imagePreviewFieldName, true);
            hiddenFieldBuilder.MergeAttribute("value", fieldValue);
            hiddenFieldHtml = MvcHtmlString.Create(hiddenFieldBuilder.ToString(TagRenderMode.SelfClosing));

            return MvcHtmlString.Create(uploadControlHtml.ToString() +
                                            previewLinkHtml.ToString() +
                                            hiddenFieldHtml.ToString());
        }       
    }
}
