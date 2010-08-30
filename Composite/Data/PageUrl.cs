﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Composite.Data;
using Composite.Data.Types;
using Composite.Core.WebClient.Renderings.Page;
using Composite.Core.WebClient;
using Composite.Core.Extensions;


namespace Composite.Data
{
    /// <summary>
    /// Page url type
    /// </summary>
    public enum PageUrlType
    {
        Undefined = 0,

        /// <summary>
        /// A main url by with a C1 page is accessed. F.e. "/Home/About.aspx"
        /// </summary>
        Published = 1,

        /// <summary>
        /// Unpublihed reference to a page. F.e. "/Renderers/Page.aspx?id=7446ceda-df90-49f0-a183-4e02ed6f6eec"
        /// </summary>
        Unpublihed = 2,

        /// <summary>
        /// Friendly url. A short url, by accessing which C1 will make a redirect to related "public" url
        /// </summary>
        Friendly = 3
    }

    /// <summary>
    /// Represents a page url
    /// </summary>
    public sealed class PageUrl
    {
        public PageUrl(PublicationScope publicationScope, CultureInfo locale, Guid pageId) :
            this(publicationScope, locale, pageId, PageUrlType.Undefined)
        {
        }

        public PageUrl(PublicationScope publicationScope, CultureInfo locale, Guid pageId, PageUrlType urlType)
        {
            Verify.ArgumentNotNull(locale, "locale");
            Verify.ArgumentCondition(pageId != Guid.Empty, "pageId", "PageId should not be an empty guid.");

            this.PublicationScope = publicationScope;
            Locale = locale;
            PageId = pageId;
            UrlType = urlType;
        }

        /// <summary>
        /// Gets the type of the URL.
        /// </summary>
        /// <value>The type of the URL.</value>
        public PageUrlType UrlType { get; private set; }

        /// <summary>
        /// Gets the publication scope.
        /// </summary>
        /// <value>The publication scope.</value>
        public PublicationScope PublicationScope { get; private set; }

        /// <summary>
        /// Gets the locale.
        /// </summary>
        /// <value>The locale.</value>
        public CultureInfo Locale { get; private set; }

        /// <summary>
        /// Gets the page id.
        /// </summary>
        /// <value>The page id.</value>
        public Guid PageId { get; private set; }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <returns></returns>
        public IPage GetPage()
        {
            using(Storage.Open(PublicationScope, Locale))
            {
                return Data.Types.PageManager.GetPageById(PageId);
            }
        }

        /// <summary>
        /// Builds a url.
        /// </summary>
        /// <returns></returns>
        public UrlBuilder Build()
        {
            Verify.That(UrlType != PageUrlType.Undefined, "Url type is undefined");

            return Build(UrlType);
        }

        /// <summary>
        /// Builds a url of the the specified URL type.
        /// </summary>
        /// <param name="urlType">Type of the URL.</param>
        /// <returns></returns>
        public UrlBuilder Build(PageUrlType urlType)
        {
            Verify.ArgumentCondition(urlType != PageUrlType.Undefined, "urlType", "Url type is undefined");

            string legasyScopeName = GetLegasyPublicationScopeIdentifier(PublicationScope);

            if (urlType == PageUrlType.Published)
            {
                var lookupTable = PageStructureInfo.GetIdToUrlLookup(legasyScopeName, Locale);

                if (!lookupTable.ContainsKey(PageId))
                {
                    return null;
                }

                var publicUrl = new UrlBuilder(lookupTable[PageId]);
                if (PublicationScope != PublicationScope.Published)
                {
                    publicUrl["dataScope"] = GetLegasyPublicationScopeIdentifier(PublicationScope);
                }

                return publicUrl;
            }

            if (urlType == PageUrlType.Unpublihed)
            {
                string basePath = UrlUtils.ResolvePublicUrl("Renderers/Page.aspx");
                UrlBuilder result = new UrlBuilder(basePath);

                result["pageId"] = PageId.ToString();
                result["cultureInfo"] = Locale.ToString();
                result["dataScope"] = GetLegasyPublicationScopeIdentifier(PublicationScope);

                return result;
            }

            throw new NotImplementedException("Only 'Public' and 'Internal' url types are supported.");
        }

        /// <summary>
        /// Parses the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        public static PageUrl Parse(string url)
        {
            NameValueCollection queryParameters;

            return Parse(url, out queryParameters);
        }

        /// <summary>
        /// Parses the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="queryParameters">The query parameters that weren't used to define which page was accessed.</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.Compare(System.String,System.String,System.Boolean)")]
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "1#")]
        public static PageUrl Parse(string url, out NameValueCollection queryParameters)
        {
            Verify.ArgumentNotNull(url, "url");

            var urlBuilder = new UrlBuilder(url);
            return IsInternalUrl(urlBuilder)
                       ? ParseInternalUrl(urlBuilder, out queryParameters)
                       : ParsePublicUrl(urlBuilder, out queryParameters);
        }


        internal static PageUrl ParsePublicUrl(UrlBuilder urlBuilder, out NameValueCollection notUsedQueryParameters)
        {
            notUsedQueryParameters = null;

            string requestPath;
            Uri uri;

            if (Uri.TryCreate(urlBuilder.FilePath, UriKind.Absolute, out uri))
            {
                requestPath = HttpUtility.UrlDecode(uri.AbsolutePath).ToLower();
            }
            else
            {
                requestPath = urlBuilder.FilePath.ToLower();
            }

            string requestPathWithoutUrlMappingName;
            CultureInfo locale = GetCultureInfo(requestPath, out requestPathWithoutUrlMappingName);

            if (locale == null)
            {
                return null;
            }

            PublicationScope publicationScope = PublicationScope.Published;

            string dataScopeName = urlBuilder["dataScope"];
            if (!dataScopeName.IsNullOrEmpty() && string.Compare(dataScopeName, DataScopeIdentifier.AdministratedName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                publicationScope = PublicationScope.Unpublihed;
            }

            Guid pageId = Guid.Empty;
            using (new DataScope(DataScopeIdentifier.FromPublicationScope(publicationScope), locale))
            {
                if (PageStructureInfo.GetLowerCaseUrlToIdLookup().TryGetValue(requestPath.ToLower(), out pageId) == false)
                {
                    return null;
                }
            }

            notUsedQueryParameters = new NameValueCollection(urlBuilder.GetQueryParameters());
            notUsedQueryParameters.Remove("dataScope");

            return new PageUrl(publicationScope, locale, pageId, PageUrlType.Published);
        }

        internal static CultureInfo GetCultureInfo(string requestPath, out string requestPathWithoutUrlMappingName)
        {
            requestPathWithoutUrlMappingName = requestPath;

            int startIndex = requestPath.IndexOf('/', UrlUtils.PublicRootPath.Length) + 1;
            if (startIndex >= 0 && requestPath.Length > startIndex)
            {
                int endIndex = requestPath.IndexOf('/', startIndex + 1) - 1;
                if (endIndex >= 0)
                {
                    string urlMappingName = requestPath.Substring(startIndex, endIndex - startIndex + 1);

                    if (DataLocalizationFacade.UrlMappingNames.Contains(urlMappingName) == true)
                    {
                        CultureInfo cultureInfo = DataLocalizationFacade.GetCultureInfoByUrlMappingName(urlMappingName);

                        bool exists = DataLocalizationFacade.ActiveLocalizationNames.Contains(cultureInfo.Name);

                        if (exists)
                        {
                            requestPathWithoutUrlMappingName = requestPath.Remove(startIndex - 1, endIndex - startIndex + 2);

                            return cultureInfo;
                        }

                        return null;
                    }
                }
            }

            return DataLocalizationFacade.DefaultUrlMappingCulture;
        }

        internal static PageUrl ParseInternalUrl(UrlBuilder urlBuilder, out NameValueCollection notUsedQueryStringParameters)
        {
            return ParseQueryString(urlBuilder.GetQueryParameters(), out notUsedQueryStringParameters);
        }

        /// <summary>
        /// To be used for handling 'internal' links.
        /// </summary>
        /// <param name="queryString">Query string.</param>
        /// <param name="notUsedQueryParameters">Query string parameters that were not used.</param>
        /// <returns></returns>
        internal static PageUrl ParseQueryString(NameValueCollection queryString, out NameValueCollection notUsedQueryParameters)
        {
            if (string.IsNullOrEmpty(queryString["pageId"])) throw new InvalidOperationException("Illigal query string. Expected param 'pageId' with guid.");

            string dataScopeName = queryString["dataScope"];

            PublicationScope publicationScope = PublicationScope.Published;

            if(dataScopeName != null 
                && string.Compare(dataScopeName, DataScopeIdentifier.AdministratedName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                publicationScope = PublicationScope.Unpublihed;
            }
            

            string cultureInfoStr = queryString["cultureInfo"];
            if (cultureInfoStr.IsNullOrEmpty())
            {
                cultureInfoStr = queryString["CultureInfo"];
            }

            CultureInfo cultureInfo;
            if (!cultureInfoStr.IsNullOrEmpty())
            {
                cultureInfo = new CultureInfo(cultureInfoStr);
            }
            else
            {
                cultureInfo = LocalizationScopeManager.CurrentLocalizationScope;
                if (cultureInfo == CultureInfo.InvariantCulture)
                {
                    cultureInfo = DataLocalizationFacade.DefaultLocalizationCulture;
                }
            }

            Guid pageId = new Guid(queryString["pageId"]);

            notUsedQueryParameters = new NameValueCollection();

            var queryKeys = new[] { "pageId", "dataScope", "cultureInfo", "CultureInfo" };

            var notUsedKeys = queryString.AllKeys.Where(key => !queryKeys.Contains(key, StringComparer.OrdinalIgnoreCase));

            foreach (string key in notUsedKeys)
            {
                notUsedQueryParameters.Add(key, queryString[key]);
            }

            return new PageUrl(publicationScope, cultureInfo, pageId, PageUrlType.Unpublihed);
        }

        internal static bool TryParseFriendlyUrl(string relativeUrl, out PageUrl pageUrl)
        {
            string path;
            CultureInfo cultureInfo = GetCultureInfo(relativeUrl, out path);
            if (cultureInfo == null)
            {
                pageUrl = null;
                return false;
            }

            var invariantCulture = CultureInfo.InvariantCulture;

            string loweredFriendlyPath = path.ToLower(invariantCulture);

            // Getting the site map
            IEnumerable<XElement> siteMap;
            DataScopeIdentifier dataScope = DataScopeIdentifier.GetDefault();
            using (new DataScope(dataScope, cultureInfo))
            {
                siteMap = PageStructureInfo.GetSiteMap();
            }

            // TODO: Optimize
            XAttribute matchingAttributeNode = siteMap.DescendantsAndSelf()
                        .Attributes("FriendlyUrl")
                        .Where(f => f.Value.ToLower(invariantCulture) == loweredFriendlyPath).FirstOrDefault();

            if (matchingAttributeNode == null)
            {
                pageUrl = null;
                return false;
            }

            XElement pageNode = matchingAttributeNode.Parent;

            XAttribute pageIdAttr = pageNode.Attributes("Id").FirstOrDefault();
            Verify.IsNotNull(pageIdAttr, "Failed to get 'Id' attribute from the site map");
            Guid pageId = new Guid(pageIdAttr.Value);

            pageUrl = new PageUrl(dataScope.ToPublicationScope(), cultureInfo, pageId, PageUrlType.Friendly);
            return true;
        }

        internal static bool IsInternalUrl(string url)
        {
            return IsInternalUrl(new UrlBuilder(url));
        }

        internal static bool IsInternalUrl(UrlBuilder url)
        {
            return url.FilePath.EndsWith("Renderers/Page.aspx", true);
        }

        private static string GetLegasyPublicationScopeIdentifier(PublicationScope publicationScope)
        {
            return publicationScope == PublicationScope.Published ? "public" : "administrated";
        }
    }
}
