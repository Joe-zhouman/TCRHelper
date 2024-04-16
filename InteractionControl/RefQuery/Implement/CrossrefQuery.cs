// 
// TCRHelper
// Utilities
// 2024-4-13-12:19
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using Model.Db;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Web;

namespace Utilities.RefQuery.Implement;

public class CrossrefQuery : IRefQueryProduct {
    private async Task<HttpResponseMessage> Query(string doi) {
        string host = $"https://api.crossref.org/works/{HttpUtility.UrlEncode(doi)}";
        Encoding encoding = Encoding.Default;
        using HttpClient client = new();
        using HttpRequestMessage request = new(HttpMethod.Get, host);
        return await client.SendAsync(request);
    }

    public async Task<Reference> GetRef(string doi) {
        HttpResponseMessage response = await Query(doi);
        if(!response.IsSuccessStatusCode) {
            throw new HttpRequestException(response.ToString());
        }
        string jsonRespond = await response.Content.ReadAsStringAsync();
        using JsonDocument doc = JsonDocument.Parse(jsonRespond);
        JsonElement message = doc.RootElement.GetProperty("message");
        Reference reference = new() {
            Id = -1,
            DOI = doi,
            Detail = jsonRespond,
            Title = message.GetProperty("title")[0].ToString(),
            Year = message.GetProperty("published-print").GetProperty("date-parts")[0][0].ToString(),
            Journal = message.GetProperty("short-container-title")[0].ToString()
        };
        JsonElement authorInfoList = message.GetProperty("author");
        StringBuilder authorList = new();
        for(int i = 0; i < authorInfoList.GetArrayLength(); i++) {
            authorList.Append(authorInfoList[i].GetProperty("given"));
            authorList.Append(", ");
            authorList.Append(authorInfoList[i].GetProperty("family"));
            authorList.Append('|');
        }
        reference.Author = authorList.ToString();
        return reference;
    }
}