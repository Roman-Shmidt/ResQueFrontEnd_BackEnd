namespace Infrastructure.QueryParamsParser;

/// <summary>Extension methods for IContainsAttribute.</summary>
public static class ContainsAttributeExtensionMethods
{
    private const string NameAttribute = "Name";

    /// <summary>Determines whether filtering/sorting should be done by Name property.</summary>
    /// <param name="options">The options.</param>
    /// <returns>
    ///   <c>true</c> if filtering/sorting should be done by Name property; otherwise, <c>false</c>.</returns>
    public static bool ContainsOperationByName(this IContainsAttribute options)
    {
        return options.AttributeName.StartsWith($"{NameAttribute}.",
            StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Gets the name language key.</summary>
    /// <param name="options">The options.</param>
    /// <returns>The language key.</returns>
    public static string GetNameLanguageKey(this IContainsAttribute options)
    {
        return options.AttributeName.Replace($"{NameAttribute}.",
            string.Empty,
            StringComparison.OrdinalIgnoreCase);
    }
}
