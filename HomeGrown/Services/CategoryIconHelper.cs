namespace HomeGrown.Services;

/// <summary>
/// Returns minimal monochrome SVG icons for each product/farm category.
/// Icons use stroke="currentColor" so they inherit their parent's color.
/// Call from Razor: @Html.Raw(CategoryIconHelper.Icon("Produce"))
/// </summary>
public static class CategoryIconHelper
{
    private static readonly Dictionary<string, string> Paths = new(StringComparer.OrdinalIgnoreCase)
    {
        // A simple forked leaf — two curved branches meeting at a stem
        ["Produce"] =
            @"<path d=""M12 22V12""/>" +
            @"<path d=""M12 12C10 7 5 5 3 8c1 5 6 6 9 4""/>" +
            @"<path d=""M12 12c2-5 7-7 9-4-1 5-6 6-9 4""/>",

        // A single egg outline, slightly off-centre for character
        ["Dairy & Eggs"] =
            @"<path d=""M12 3C8.5 3 6 7.5 6 12.5C6 17.1 8.7 21 12 21C15.3 21 18 17.1 18 12.5C18 7.5 15.5 3 12 3Z""/>",

        // Rounded loaf dome with one diagonal score line
        ["Bread & Baked Goods"] =
            @"<path d=""M4 19h16v-5c0-3.9-3.6-7-8-7s-8 3.1-8 7v5z""/>" +
            @"<path d=""M8.5 14c1.5-1.2 5.5-1.2 7 0""/>",

        // Squat jar silhouette with lid and a horizontal seam
        ["Honey & Preserves"] =
            @"<rect x=""8"" y=""3"" width=""8"" height=""3"" rx=""1""/>" +
            @"<path d=""M6 6h12l-1.5 15H7.5L6 6Z""/>" +
            @"<line x1=""6.5"" y1=""10"" x2=""17.5"" y2=""10""/>",

        // Classic drumstick — bulb top, angled bone handle
        ["Meat & Poultry"] =
            @"<path d=""M14.5 4.5a5 5 0 014.5 8.5L9.5 20""/>" +
            @"<circle cx=""8"" cy=""20.5"" r=""2.5""/>",

        // Clean wine bottle — neck, shoulder, body, label line
        ["Wine & Beverages"] =
            @"<path d=""M10 2h4v4.5l2 2.5v11a1 1 0 01-1 1H9a1 1 0 01-1-1V9l2-2.5V2z""/>" +
            @"<line x1=""8"" y1=""13"" x2=""16"" y2=""13""/>",
    };

    private const string FallbackPath =
        @"<circle cx=""12"" cy=""12"" r=""9""/>" +
        @"<line x1=""12"" y1=""8"" x2=""12"" y2=""12""/>" +
        @"<line x1=""12"" y1=""16"" x2=""12.01"" y2=""16""/>";

    /// <summary>Returns a full inline SVG element for the given category.</summary>
    public static string Icon(string category, int size = 18, string? cssClass = null)
    {
        var inner = Paths.TryGetValue(category, out var p) ? p : FallbackPath;
        var cls = cssClass != null ? $@" class=""{cssClass}""" : "";
        return $@"<svg width=""{size}"" height=""{size}"" viewBox=""0 0 24 24"" fill=""none"" " +
               $@"stroke=""currentColor"" stroke-width=""1.75"" stroke-linecap=""round"" " +
               $@"stroke-linejoin=""round"" aria-hidden=""true""{cls}>{inner}</svg>";
    }

    /// <summary>
    /// Returns just the inner SVG path strings (no outer svg tag).
    /// Used for embedding in JavaScript template strings.
    /// </summary>
    public static string InnerPaths(string category) =>
        Paths.TryGetValue(category, out var p) ? p : FallbackPath;
}
