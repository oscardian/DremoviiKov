namespace Tools;

public class Result
{
    public Boolean IsSuccess => Errors.Length == 0;
    public String[] Errors { get; set; }

    public Result(String error)
    {
        Errors = new[] { error };
    }

    public Result(String[] errors)
    {
        Errors = errors;
    }

    public Result()
    {
        Errors = Array.Empty<String>();
    }

    public static Result Success() => new();

    public static Result Fail(String error) => new(error);

    public static Result Fail(String[] errors) => new(errors);
}
