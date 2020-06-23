using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;

namespace Website.Utilities {
    public static class HtmlHelperEditorExtensions {
        public static IHtmlContent LabelRequiredFor<TModel, TValue>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string labelDisplay = null) {

            ModelExpressionProvider modelExpressionProvider = (ModelExpressionProvider)html.ViewContext.HttpContext.RequestServices.GetService(typeof(IModelExpressionProvider));
            var modelExplorer = modelExpressionProvider.CreateModelExpression(html.ViewData, expression);

            string labelText = html.LabelFor(expression, HttpUtility.HtmlDecode(labelDisplay)).ToRawString();
            if (modelExplorer.Metadata.IsRequired) {
                TagBuilder span = new TagBuilder("span");
                span.InnerHtml.Append(" *");
                span.AddCssClass("text-danger");
                span.Attributes.Add("title", "This field is required.");
                labelText += span.ToRawString();
            }
            return new HtmlString(labelText);
        }

        /// <summary>
        /// Returns a label, editor, and validation message for the specified model property within a form-group div.
        /// This is the same as LabelEditorValFor except that it will be inside a form-group div.
        /// </summary>
        /// <param name="expression">Model</param>
        /// <param name="labelDisplay">Optional. Overrides the default label text.</param>
        /// <param name="editorTemplateName">Optional. Name of the editor template to use.</param>
        /// <param name="htmlAttributes">Optional. HTML attributes to add to the input.</param>
        public static IHtmlContent FormGroupEditorFor<TModel, TValue>(
            this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string labelDisplay = null,
            string editorTemplateName = null,
            object htmlAttributes = null) {

            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("form-group");
            div.InnerHtml.AppendHtml(LabelEditorValFor(html, expression, labelDisplay, editorTemplateName, htmlAttributes).ToRawString());

            return div;
        }

        /// <summary>
        /// Returns a label, editor, and validation message for the specified model property.
        /// </summary>
        /// <param name="expression">Model</param>
        /// <param name="labelDisplay">Optional. Overrides the default label text.</param>
        /// <param name="editorTemplateName">Optional. Name of the editor template to use.</param>
        /// <param name="htmlAttributes">Optional. HTML attributes to add to the input.</param>
        public static IHtmlContent LabelEditorValFor<TModel, TValue>(
            this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string labelDisplay = null,
            string templateName = null,
            object htmlAttributes = null) {

            var type = typeof(TValue);
            type = Nullable.GetUnderlyingType(type) ?? type;

            // Bootstrap 4 form-check for Booleans
            if (type == typeof(bool) && string.IsNullOrWhiteSpace(templateName)) {
                bool isNullable = false;
                if (typeof(TValue) == typeof(bool?))
                    isNullable = true;

                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("form-group form-check");
                div.InnerHtml.AppendHtml(html.EditorFor(expression, isNullable ? "NullableBoolean" : null, new { htmlAttributes }).ToRawString() +
                    html.LabelFor(expression, labelDisplay, new { @class = "form-check-label" }).ToRawString());
                return new HtmlString(
                    div.ToRawString() +
                    html.DescriptionFor(expression).ToRawString() +
                    html.ValidationMessageFor(expression).ToRawString()
                );
            }

            return new HtmlString(
                html.LabelRequiredFor(expression, labelDisplay).ToRawString() +
                html.EditorFor(expression, templateName, new { htmlAttributes }).ToRawString() +
                html.DescriptionFor(expression).ToRawString() +
                html.ValidationMessageFor(expression).ToRawString());
        }

        public static IHtmlContent FormGroupSelectListFor<TModel, TValue>(
            this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> selectlistItems,
            string optionLabel = null,
            string labelDisplay = null,
            object htmlAttributes = null) {
            return FormGroupSelectListFor(html, expression, new SelectList(selectlistItems, "Value", "Text"), optionLabel, labelDisplay, htmlAttributes);
        }

        public static IHtmlContent FormGroupSelectListFor<TModel, TValue>(
        this IHtmlHelper<TModel> html,
        Expression<Func<TModel, TValue>> expression,
        SelectList selectlist,
        string optionLabel = null,
        string labelDisplay = null,
        object htmlAttributes = null) {

            IDictionary<string, object> attributes = new RouteValueDictionary(htmlAttributes);
            if (attributes.ContainsKey("class")) {
                attributes["class"] += " form-control";
            } else {
                attributes.Add("class", "form-control");
            }

            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("form-group");
            div.InnerHtml.AppendHtml(
                html.LabelRequiredFor(expression, labelDisplay).ToRawString() +
                html.DropDownListFor(expression, selectlist, optionLabel, attributes).ToRawString() +
                html.DescriptionFor(expression).ToRawString() +
                html.ValidationMessageFor(expression).ToRawString());

            return div;
        }
    }
}
