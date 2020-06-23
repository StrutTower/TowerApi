using AngleSharp;
using AngleSharp.Dom;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SteamTower.Models;
using System;
using System.Linq;
using System.Reflection;

namespace Website.Utilities {
    public static class ViewUtilities {
        public static string GetSteamPersonaStateCss(SteamUser user) {
            if (user.GameID.HasValue) return "friend-ingame";
            else if (user.PersonaState == PersonaState.Online) return "friend-online";
            else if (user.PersonaState == PersonaState.Offline) return "friend-offline";
            else return "friend-away";
        }

        public static IHtmlContent FormatHtml(string html) {
            IBrowsingContext ctx = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            IDocument doc = ctx.OpenAsync(req => req.Content(html)).Result;

            foreach (var element in doc.DocumentElement.QuerySelectorAll("script")) {
                element.Remove();
            }

            foreach (var element in doc.DocumentElement.QuerySelectorAll("img")) {
                if (element.HasAttribute("style")) {
                    element.Attributes["style"].Value += "max-width:100%;";
                } else {
                    element.SetAttribute("style", "max-width:100%");
                }
            }
            return new HtmlString(doc.ToHtml());
        }

        /// <summary>
        /// Convert the boolean to an icon.
        /// </summary>
        /// <param name="boolean">Boolean to display as an icon</param>
        public static IHtmlContent BooleanIcon(bool boolean) {
            TagBuilder tag = new TagBuilder("span");
            if (boolean) {
                tag.AddCssClass("mdi mdi-check text-success");
            } else {
                tag.AddCssClass("mdi mdi-cancel text-danger");
            }
            return tag;
        }

        /// <summary>
        /// Convert the boolean to an icon.
        /// </summary>
        /// <param name="boolean">Boolean to display as an icon</param>
        public static IHtmlContent BooleanIconAndText(bool? boolean) {
            TagBuilder tag = new TagBuilder("span");
            if (boolean.HasValue && boolean.Value) {
                tag.AddCssClass("mdi mdi-checkbox-marked-outline text-success");
                tag.Attributes.Add("title", "True");
            } else if (boolean.HasValue) {
                tag.AddCssClass("mdi mdi-checkbox-blank-off-outline text-danger");
                tag.Attributes.Add("title", "False");
            } else {
                tag.AddCssClass("mdi mdi-checkbox-blank-outline text-secondary");
                tag.Attributes.Add("title", "Unknown");
            }
            TagBuilder container = new TagBuilder("span");
            container.InnerHtml.AppendHtml(tag.ToRawString() + " ");
            if (boolean.HasValue && boolean.Value) {
                container.InnerHtml.AppendHtml("True");
            } else if (boolean.HasValue) {
                container.InnerHtml.AppendHtml("False");
            } else {
                container.InnerHtml.AppendHtml("Unknown");
            }
            return container;
        }

        /// <summary>
        /// Returns the view of the website project.
        /// </summary>
        public static string GetWebsiteVersion() {
            return Assembly.GetCallingAssembly().GetName().Version.ToString(3);
        }
    }
}
