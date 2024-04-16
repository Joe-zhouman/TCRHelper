// 
// TCRHelper
// Utilities
// 2024-3-14-15:9
// *Author*    : Joe, Zhou Man
// *Email*     : man.man.man.man.a@gmail.com
// *Email*     : joe_zhouman@foxmail.com
// *QQ*        : 1592020915
// *Weibo*     : @zhouman_LFC
// *Twitter*   : @zhouman_LFC
// *Website*   : www.joezhouman.com
// *Github*    : https://github.com/Joe-zhouman
// *Bilibili*  : @satisfactions

using Model.Config;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using System.Windows.Media.Imaging;

namespace Utilities.Ocr.Implement;

public class BaiduOcr() : IOcrProduct {
    private static readonly string URL = "https://aip.baidubce.com/rest/2.0/ocr/v1/general_basic?access_token=";
    private string _apiKey;
    private string _secretKey;
    private string _token;
    public BaiduOcr(OcrConfig ocrConfig) : this() {
        _apiKey = ocrConfig.ApiKey;
        _secretKey = ocrConfig.SecretKey;
    }

    public async Task<string> GetToken() {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        using HttpRequestMessage request = new(HttpMethod.Post, $"https://aip.baidubce.com/oauth/2.0/token?client_id={_apiKey}&client_secret={_secretKey}&grant_type=client_credentials");
        request.Content = new StringContent("", Encoding.UTF8, "application/json");
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        try {
            var response = await client.SendAsync(request);
            if(!response.IsSuccessStatusCode) { return response.ToString(); }
            string jsonResponse = await response.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            JsonElement root = doc.RootElement;
            if(root.TryGetProperty("access_token", out JsonElement accessToken)) {
                _token = accessToken.GetString();
                return "";
            }
            return jsonResponse;
        }
        catch(Exception e) {
            return e.ToString();
        }
    }
    public async Task<string> Identify(BitmapSource image) {
        try {
            HttpResponseMessage respond = await Request(image);
            if(!respond.IsSuccessStatusCode) { return respond.ToString(); }
            string jsonRespond = await respond.Content.ReadAsStringAsync();
            using JsonDocument doc = JsonDocument.Parse(jsonRespond);
            JsonElement root = doc.RootElement;
            if(root.TryGetProperty("error_code", out JsonElement errorCode)) {
                return jsonRespond;
            }
            int wordsResultNum = root.GetProperty("words_result_num").GetInt32();
            JsonElement wordsResultArray = root.GetProperty("words_result");
            StringBuilder sb = new();
            for(int i = 0; i < wordsResultNum; i++) {
                sb.Append(wordsResultArray[i].GetProperty("words").ToString());
            }
            return sb.ToString();
        }
        catch(Exception e) {
            return e.ToString();
        }
    }

    private async Task<HttpResponseMessage> Request(BitmapSource image) {
        string host = $"{URL}{_token}";
        Encoding encoding = Encoding.Default;
        byte[] buffer =
            encoding.GetBytes($"image={HttpUtility.UrlEncode(ConversionUtilities.BitmapSource2Base64(image))}");
        MemoryStream postDataStream = new(buffer);
        using HttpClient client = new();
        using HttpRequestMessage request = new(HttpMethod.Post, host);

        request.Content = new StreamContent(postDataStream, buffer.Length);
        return await client.SendAsync(request);
    }
}