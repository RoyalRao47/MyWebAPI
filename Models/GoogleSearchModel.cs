using Newtonsoft.Json;

namespace MyWebAPI.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Context
    {
        public string title { get; set; }
    }

    public class CseImage
    {
        public string src { get; set; }
    }

    public class CseThumbnail
    {
        public string src { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }

    public class Document
    {
        public string title { get; set; }
    }

    public class Item
    {
        public string kind { get; set; }
        public string title { get; set; }
        public string htmlTitle { get; set; }
        public string link { get; set; }
        public string displayLink { get; set; }
        public string snippet { get; set; }
        public string htmlSnippet { get; set; }
        public string formattedUrl { get; set; }
        public string htmlFormattedUrl { get; set; }
        public Pagemap pagemap { get; set; }
    }

    public class LiveBlogPosting
    {
    }

    public class Metatag
    {
        [JsonProperty("apple-itunes-app")]
        public string appleitunesapp { get; set; }

        [JsonProperty("og:image")]
        public string ogimage { get; set; }

        [JsonProperty("article:published_time")]
        public DateTime articlepublished_time { get; set; }

        [JsonProperty("twitter:card")]
        public string twittercard { get; set; }

        [JsonProperty("og:site_name")]
        public string ogsite_name { get; set; }

        [JsonProperty("og:image:type")]
        public string ogimagetype { get; set; }

        [JsonProperty("og:description")]
        public string ogdescription { get; set; }

        [JsonProperty("twitter:creator")]
        public string twittercreator { get; set; }

        [JsonProperty("og:image:secure_url")]
        public string ogimagesecure_url { get; set; }

        [JsonProperty("twitter:image")]
        public string twitterimage { get; set; }

        [JsonProperty("next-head-count")]
        public string nextheadcount { get; set; }
        public string baseurl { get; set; }

        [JsonProperty("twitter:site")]
        public string twittersite { get; set; }

        [JsonProperty("article:modified_time")]
        public DateTime articlemodified_time { get; set; }

        [JsonProperty("fb:admins")]
        public string fbadmins { get; set; }

        [JsonProperty("og:type")]
        public string ogtype { get; set; }

        [JsonProperty("twitter:title")]
        public string twittertitle { get; set; }
        public string author { get; set; }

        [JsonProperty("og:title")]
        public string ogtitle { get; set; }
        public string version { get; set; }

        [JsonProperty("twitter:image:src")]
        public string twitterimagesrc { get; set; }
        public string viewport { get; set; }

        [JsonProperty("twitter:description")]
        public string twitterdescription { get; set; }

        [JsonProperty("og:url")]
        public string ogurl { get; set; }
        public string languageurl { get; set; }
        public string referrer { get; set; }

        [JsonProperty("theme-color")]
        public string themecolor { get; set; }

        [JsonProperty("og:image:width")]
        public string ogimagewidth { get; set; }

        [JsonProperty("og:image:height")]
        public string ogimageheight { get; set; }

        [JsonProperty("format-detection")]
        public string formatdetection { get; set; }

        [JsonProperty("twitter:app:url:iphone")]
        public string twitterappurliphone { get; set; }

        [JsonProperty("twitter:app:id:googleplay")]
        public string twitterappidgoogleplay { get; set; }

        [JsonProperty("twitter:url")]
        public string twitterurl { get; set; }

        [JsonProperty("twitter:app:name:googleplay")]
        public string twitterappnamegoogleplay { get; set; }
        public string medium { get; set; }

        [JsonProperty("twitter:app:id:iphone")]
        public string twitterappidiphone { get; set; }
        public string title { get; set; }

        [JsonProperty("article:content_tier")]
        public string articlecontent_tier { get; set; }

        [JsonProperty("fb:pages")]
        public string fbpages { get; set; }

        [JsonProperty("dc.date.issued")]
        public DateTime? dcdateissued { get; set; }

        [JsonProperty("fb:app_id")]
        public string fbapp_id { get; set; }

        [JsonProperty("twitter:app:url:googleplay")]
        public string twitterappurlgoogleplay { get; set; }

        [JsonProperty("twitter:app:name:iphone")]
        public string twitterappnameiphone { get; set; }

        [JsonProperty("article:opinion")]
        public string articleopinion { get; set; }

        [JsonProperty("twitter:image:alt")]
        public string twitterimagealt { get; set; }
        public string news_keywords { get; set; }

        [JsonProperty("og:image:alt")]
        public string ogimagealt { get; set; }

        [JsonProperty("tweetmeme-title")]
        public string tweetmemetitle { get; set; }

        [JsonProperty("og:locale")]
        public string oglocale { get; set; }

        [JsonProperty("ad:rubrique_id")]
        public string adrubrique_id { get; set; }

        [JsonProperty("pbstck_context:page_type")]
        public string pbstck_contextpage_type { get; set; }

        [JsonProperty("pbstck_context:inventory_type")]
        public string pbstck_contextinventory_type { get; set; }

        [JsonProperty("pbstck_context:device")]
        public string pbstck_contextdevice { get; set; }

        [JsonProperty("ad:rub")]
        public string adrub { get; set; }

        [JsonProperty("al:ios:url")]
        public string aliosurl { get; set; }

        [JsonProperty("pbstck_context:user_status")]
        public string pbstck_contextuser_status { get; set; }

        [JsonProperty("ad:article_is_longform")]
        public string adarticle_is_longform { get; set; }

        [JsonProperty("ad:article_id")]
        public string adarticle_id { get; set; }

        [JsonProperty("pbstck_context:site_name")]
        public string pbstck_contextsite_name { get; set; }

        [JsonProperty("og:article:section")]
        public string ogarticlesection { get; set; }

        [JsonProperty("fb:page_id")]
        public string fbpage_id { get; set; }

        [JsonProperty("og:article:author")]
        public string ogarticleauthor { get; set; }

        [JsonProperty("ad:rubriques")]
        public string adrubriques { get; set; }

        [JsonProperty("og:article:content_tier")]
        public string ogarticlecontent_tier { get; set; }

        [JsonProperty("pbstck_context:section")]
        public string pbstck_contextsection { get; set; }

        [JsonProperty("ad:keywords")]
        public string adkeywords { get; set; }

        [JsonProperty("og:article:published_time")]
        public DateTime? ogarticlepublished_time { get; set; }

        [JsonProperty("ad:article_type")]
        public string adarticle_type { get; set; }

        [JsonProperty("pbstck_context:environment")]
        public string pbstck_contextenvironment { get; set; }

        [JsonProperty("al:android:url")]
        public string alandroidurl { get; set; }

        [JsonProperty("pbstck_context:subsection")]
        public string pbstck_contextsubsection { get; set; }

        [JsonProperty("ad:teaser")]
        public string adteaser { get; set; }

        [JsonProperty("og:determiner")]
        public string ogdeterminer { get; set; }
        public string sport { get; set; }
    }

    public class NextPage
    {
        public string title { get; set; }
        public string totalResults { get; set; }
        public string searchTerms { get; set; }
        public int count { get; set; }
        public int startIndex { get; set; }
        public string inputEncoding { get; set; }
        public string outputEncoding { get; set; }
        public string safe { get; set; }
        public string cx { get; set; }
    }

    public class Pagemap
    {
        public List<CseThumbnail> cse_thumbnail { get; set; }
        public List<Metatag> metatags { get; set; }
        public List<CseImage> cse_image { get; set; }
        public List<Thumbnail> thumbnail { get; set; }
        public List<Document> document { get; set; }
        public List<Sitenavigationelement> sitenavigationelement { get; set; }
        public List<LiveBlogPosting> LiveBlogPosting { get; set; }
    }

    public class Queries
    {
        public List<Request> request { get; set; }
        public List<NextPage> nextPage { get; set; }
    }

    public class Request
    {
        public string title { get; set; }
        public string totalResults { get; set; }
        public string searchTerms { get; set; }
        public int count { get; set; }
        public int startIndex { get; set; }
        public string inputEncoding { get; set; }
        public string outputEncoding { get; set; }
        public string safe { get; set; }
        public string cx { get; set; }
    }

    public class GoogleSearchModel
    {
        public string kind { get; set; }
        public Url url { get; set; }
        public Queries queries { get; set; }
        public Context context { get; set; }
        public SearchInformation searchInformation { get; set; }
        public List<Item> items { get; set; }
    }

    public class SearchInformation
    {
        public double searchTime { get; set; }
        public string formattedSearchTime { get; set; }
        public string totalResults { get; set; }
        public string formattedTotalResults { get; set; }
    }

    public class Sitenavigationelement
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Thumbnail
    {
        public string src { get; set; }
        public string width { get; set; }
        public string height { get; set; }
    }

    public class Url
    {
        public string type { get; set; }
        public string template { get; set; }
    }


}
