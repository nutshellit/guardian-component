using System.Net.Http.Json;

namespace GuardianComponent.Features.Guardian;

public class GuardianQuery : IGuardianQuery
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly GuardianSettings settings;

    public GuardianQuery(IHttpClientFactory httpClientFactory, GuardianSettings settings)
    {
        this.httpClientFactory = httpClientFactory;
        this.settings = settings;
    }

    public async Task<SearchResult> GetGuardianSearchResults(string searchQuery, string section, int page = 1)
    {
        var client = httpClientFactory.CreateClient("guardian");
        var endpoint = "search";
        var dict = new Dictionary<string, string>
        {
            { "format", "json" },
            { "api-key", settings.key},
            {"order-by","newest"},
            { "page", page.ToString()},
            {"q", searchQuery.Contains(" ") ? $"\"{searchQuery}\"" : searchQuery}
        };

        if (!string.IsNullOrWhiteSpace(section))
        {
            dict.Add("section", section);
        }

        var queryString = await dict.ToQuery();
        var result = await client.GetFromJsonAsync<SearchResult>($"{endpoint}?{queryString}");
        return result!;
    }

    public async Task<SectionResult> GetGuardianSections()
    {
        var client = httpClientFactory.CreateClient("guardian");
        var endPoint = "sections";
        var queryParameters = new Dictionary<string, string>
        {
            { "format", "json" },
            { "api-key", settings.key},
            {"show-references", "all"}
        };

        var queryString = await queryParameters.ToQuery();

        SectionResult section = await client.GetFromJsonAsync<SectionResult>($"{endPoint}?{queryString}");
        return section;
    }

    public async Task<TagResult> GetGuardianTags(TagQuery tagQuery)
    {
        var client = httpClientFactory.CreateClient("guardian");
        var endpoint = "tags";
        var dict = tagQuery.ToTagQuery(settings.key);
        var queryString = await dict.ToQuery();
        var tags = await client.GetFromJsonAsync<TagResult>($"{endpoint}?{queryString}");
        return tags!;
    }
}

public static class GuardianExt
{
    public static async Task<string> ToQuery(this Dictionary<string, string> qParams)
    {
        return await new FormUrlEncodedContent(qParams).ReadAsStringAsync();
    }

    public static Dictionary<string, string> ToTagQuery(this TagQuery query, string apiKey)
    {
        var dict = new Dictionary<string, string>{
        { "format", "json" },
        { "api-key", apiKey}};

        if (!query.query.IsBlank()) dict.Add("q", query.query.Trim());
        if (!query.webTitle.IsBlank()) dict.Add("web-title", query.webTitle.Trim());
        if (!query.section.IsBlank()) dict.Add("section", query.section.Trim());
        if (query.page > 0) dict.Add("page", query.page.ToString());

        return dict;
    }

    public static bool IsBlank(this string str) => string.IsNullOrWhiteSpace(str);

    public static string ToTagTypeQuery(this TagType tagType) =>
        tagType switch
        {
            TagType.newspaperbook => "newspaper-book",
            TagType.newspaperbooksection => "newspaper-book-section",
            TagType t => t.ToString()
        };
}
