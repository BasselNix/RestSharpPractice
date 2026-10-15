namespace RestSharpPractice
{
    internal class Task2
    {
        public static Tuple<PizzaOrderForm?, Dictionary<string, string>> ResponseFormAndHeaders(HttpbinPostResponse postData, Dictionary<string, string> headers)
        {
            return Tuple.Create(postData.Form, headers);
        }
    }
}
