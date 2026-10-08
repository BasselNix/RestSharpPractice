namespace RestSharpPractice
{
    internal class Task2
    {
        public static Tuple<Dictionary<string, object>?, Dictionary<string, string>> ResponseFormAndHeaders(HttpbinPostResponse postData, Dictionary<string, string> headers)
        {
            return Tuple.Create(postData.Form, headers);
        }
    }
}
