using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BitcoinProcessing
{
    public class HttpHelper
    {
        HttpClient client = new HttpClient() { Timeout = TimeSpan.FromMinutes(60) };
        public async Task<string> ProcessURL(string url, Action<string> resultCallbackAction)
        {
            //using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync(url).ConfigureAwait(false);
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();

                    resultCallbackAction?.Invoke(content);
                    //await Task.Delay(200);
                    return content;
                }
                else //if ((int)result.StatusCode == 429)
                {
                    //HTTP Status is not OK then delay and try again
                    await Task.Delay(new Random().Next(500, 10000));
                    return await ProcessURL(url, resultCallbackAction);
                }
            }
        }
    }
}
