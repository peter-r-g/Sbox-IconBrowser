using Editor;
using System.Collections.Generic;
using System.IO;

namespace IconBrowser;

/// <summary>
/// Encapsulates data for a codepoint in the material icons font.
/// </summary>
internal readonly struct MaterialIconData
{
	/// <summary>
	/// A readonly sequence of all <see cref="MaterialIconData"/> loaded.
	/// </summary>
	internal static IEnumerable<MaterialIconData> All => all;
	private readonly static List<MaterialIconData> all = new();

	/// <summary>
	/// The human readable name of the icon.
	/// </summary>
	internal string Name { get; }
	/// <summary>
	/// The code name of the icon.
	/// </summary>
	internal string IconName { get; }
	/// <summary>
	/// The codepoint of the icon.
	/// </summary>
	internal string Codepoint { get; }
	/// <summary>
	/// A pascal case name of the icon.
	/// </summary>
	internal string CsName { get; }

	private MaterialIconData( string iconName, string codepoint )
	{
		IconName = iconName;
		Codepoint = codepoint;

		var parts = iconName.Split( '_' );
		for ( var i = 0; i < parts.Length; i++ )
			parts[i] = char.ToUpper( parts[i][0] ) + parts[i][1..];

		Name = string.Join( ' ', parts );

		if ( char.IsDigit( parts[0][0] ) )
			parts[0] = '_' + parts[0];
		CsName = string.Join( string.Empty, parts );
	}

	/// <summary>
	/// Loads all <see cref="MaterialIconData"/>s from a codepoint file.
	/// </summary>
	/// <param name="filePath">The path from the <see cref="FileSystem.Content"/> that the file is located at.</param>
	internal static void LoadFrom( string filePath )
	{
		all.Clear();

		using var fs = FileSystem.Content.OpenRead( filePath );
		using var reader = new StreamReader( fs );

		string lastName = string.Empty;
		while ( reader.ReadLine() is string line )
		{
			var parts = line.Split( ' ' );
			var name = parts[0];
			var codepoint = parts[1];

			if ( name == lastName )
				continue;

			all.Add( new MaterialIconData( name, codepoint ) );
			lastName = name;
		}

		reader.Close();
	}
}
