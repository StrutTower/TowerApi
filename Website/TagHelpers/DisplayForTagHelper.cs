using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Website.Utilities;

namespace Website.TagHelpers {
    [HtmlTargetElement("div", Attributes = "display-for")]
    [HtmlTargetElement("span", Attributes = "display-for")]
    [HtmlTargetElement("a", Attributes = "display-for")]
    [HtmlTargetElement("abbr", Attributes = "display-for")]
    [HtmlTargetElement("p", Attributes = "display-for")]
    [HtmlTargetElement("em", Attributes = "display-for")]
    [HtmlTargetElement("strong", Attributes = "display-for")]
    [HtmlTargetElement("th", Attributes = "display-for")]
    [HtmlTargetElement("td", Attributes = "display-for")]
    [HtmlTargetElement("h1", Attributes = "display-for")]
    [HtmlTargetElement("h2", Attributes = "display-for")]
    [HtmlTargetElement("h3", Attributes = "display-for")]
    [HtmlTargetElement("h4", Attributes = "display-for")]
    [HtmlTargetElement("h5", Attributes = "display-for")]
    [HtmlTargetElement("h6", Attributes = "display-for")]
    [HtmlTargetElement("li", Attributes = "display-for")]
    public class DisplayForTagHelper : TagHelper {

        public DisplayForTagHelper(IHtmlHelper htmlHelper) {
            _htmlHelper = htmlHelper;
        }

        private IHtmlHelper _htmlHelper { get; set; }

        [HtmlAttributeName("display-for")]
        public ModelExpression Model { get; set; }

        [HtmlAttributeName("display-template")]
        public string TemplateName { get; set; }

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            ((IViewContextAware)_htmlHelper).Contextualize(ViewContext);
            output.PostContent.AppendHtml(_htmlHelper.Display(Model, TemplateName));
        }
    }
}
