using Editor;

namespace IconBrowser;

/// <summary>
/// Contains extension methods for <see cref="string"/>s.
/// </summary>
internal static class StringExtensions
{
	/// <summary>
	/// Truncates a string so that it fits into a specified <see cref="Rect"/>.
	/// </summary>
	/// <param name="str">The string to truncate.</param>
	/// <param name="rect">The rect to fit the string into.</param>
	/// <param name="suffix">The suffix to apply to the string if it is truncated.</param>
	/// <param name="textFlag">The text flags to pass to the <see cref="Paint.MeasureText(in Rect, string, TextFlag)"/> calls.</param>
	/// <returns>Either the same string or one that is truncated to fit into the <see cref="Rect"/>.</returns>
	internal static string Truncate( this string str, in Rect rect, string suffix = "...", TextFlag textFlag = TextFlag.Center )
	{
		var text = str;
		var textSize = Paint.MeasureText( rect, text, textFlag );
		while ( textSize.Width >= rect.Width || textSize.Height >= rect.Height )
		{
			text = text[..^1];
			textSize = Paint.MeasureText( rect, text + suffix, textFlag );
		}

		if ( text != str )
			text += suffix;

		return text;
	}
}
