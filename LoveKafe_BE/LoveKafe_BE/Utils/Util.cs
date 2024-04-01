using LoveKafe_BE.Models;
using System.Linq.Expressions;

namespace LoveKafe_BE.Utils
{
    public class Util
    {
        public Dictionary<string, string> ParseQueryString(string queryString)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(queryString))
            {
                string[] pairs = queryString.TrimStart('?').Split('&');
                foreach (string pair in pairs)
                {
                    string[] keyValue = pair.Split('=');
                    string key = keyValue[0];
                    string value = keyValue.Length > 1 ? keyValue[1] : null;
                    queryParams.Add(key, value);
                }
            }

            return queryParams;
        }

    }
}
