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

using Model.ViewModel.Db;
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

    public async Task GetRef(ReferenceViewModel referenceViewModel) {
        HttpResponseMessage response = await Query(referenceViewModel.DOI);
        if(!response.IsSuccessStatusCode) {
            throw new HttpRequestException(response.ToString());
        }
        string jsonRespond = await response.Content.ReadAsStringAsync();
        using JsonDocument doc = JsonDocument.Parse(jsonRespond);
        JsonElement message = doc.RootElement.GetProperty("message");

        referenceViewModel.Id = -1;
        referenceViewModel.Detail = jsonRespond;
        referenceViewModel.Title = message.GetProperty("title")[0].ToString();
        referenceViewModel.Year = message.GetProperty("indexed").GetProperty("date-parts")[0][0].ToString();
        referenceViewModel.Journal = message.GetProperty("short-container-title")[0].ToString();

        JsonElement authorInfoList = message.GetProperty("author");
        StringBuilder authorList = new();
        for(int i = 0; i < authorInfoList.GetArrayLength(); i++) {
            authorList.Append(authorInfoList[i].GetProperty("given"));
            authorList.Append(", ");
            authorList.Append(authorInfoList[i].GetProperty("family"));
            authorList.Append('|');
        }
        referenceViewModel.Author = authorList.ToString();
    }
}