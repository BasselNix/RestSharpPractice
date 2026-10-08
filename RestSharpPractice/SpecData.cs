namespace RestSharpPractice
{
    public class SpecData
    {
        required public Dictionary<string, PathItem> Paths { get; set; }
    }

    public class PathItem : Dictionary<string, Operation>
    {
        // Maps HTTP methods (get, post, put, delete, etc.) to their respective operations
    }

    public class Operation
    {
        required public Dictionary<string, ResponseDetails> Responses { get; set; }
    }

    public class ResponseDetails
    {
        required public string Description { get; set; }
    }
}
