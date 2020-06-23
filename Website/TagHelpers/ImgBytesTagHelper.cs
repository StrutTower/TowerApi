using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Website.TagHelpers {
    [HtmlTargetElement("img", Attributes = "asp-bytes")]
    public class ImgBytesTagHelper : TagHelper {
        [HtmlAttributeName("asp-bytes")]
        public ModelExpression Data { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output) {
            if (Data.Model != null && Data.Model is byte[] bytes) {
                output.TagName = "img";
                output.Attributes.Add("src", "data:image/jpg;base64," + Convert.ToBase64String(bytes));
            }
        }
    }
}
