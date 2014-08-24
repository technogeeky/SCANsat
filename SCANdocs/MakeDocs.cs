using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;


namespace SCANdocs
{
	public static class FSIExtensions
	{
		public static string RelFrom (this FileSystemInfo to, FileSystemInfo from)
		{
			return from.RelTo (to);
		}

		public static string RelTo (this FileSystemInfo from, FileSystemInfo to)
		{
			Func<FileSystemInfo, string> getPath = fsi => {
				var d = fsi as DirectoryInfo;
				return d == null ? fsi.FullName : d.FullName.TrimEnd ('\\') + "\\";
			};

			var fromPath = getPath (from);
			var toPath = getPath (to);

			var fromUri = new Uri (fromPath);
			var toUri = new Uri (toPath);

			var relativeUri = fromUri.MakeRelativeUri (toUri);
			var relativePath = Uri.UnescapeDataString (relativeUri.ToString ());

			return relativePath.Replace ('/', Path.DirectorySeparatorChar);
		}

		public static MatchCollection MatchesVerbose (this Regex r, string input)
		{
			var retval = r.Matches (input);
			foreach (Match m in r.Matches(input)) {
				var counter = 0;
				foreach (Group g in m.Groups) {
					if (g.Success)
						Console.WriteLine ("\t\tGroup:\t{0} -> {1}", r.GroupNameFromNumber (counter), g.Value);
					else
						Console.WriteLine ("\t\tGroup:\t{0} -> [NO MATCH]", r.GroupNameFromNumber (counter));
					counter++;
				}
				foreach (Capture c in m.Captures) {
					//Console.WriteLine ("\t\tCapture:\t{0} -> {1}", imgurURLs.GroupNameFromNumber (c.Index), c.Value);
				}
			}
			return retval;

		}
	}

	public class MakeDocs
	{
		const RegexOptions nx = RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture;
		const RegexOptions mn = RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline;


		const SearchOption recurse = SearchOption.AllDirectories;
		const SearchOption here = SearchOption.TopDirectoryOnly;

		const string dotmd = "*.md";
		const string dotbbcode = "*.bbcode";
		const string indirs = "in-*";
		const string templatedirs = "template-*";
		const string outdirs = "out-*";

		public MakeDocs ()
		{
		}

		public static Regex httpURL = new Regex (@"http://");
		public static Regex refURLs = new Regex (@"\[ (?<label>[^\]]+) \]:	(?<space>\s*)	(?<url>[^\n]+)																																																						(?<n>\n)?", nx);
		public static Regex imageURLs =	new Regex (@"\[ (?<label>[^\]]+) \]:	(?<space>\s*)	(?<url>[^\n?]+																												\.	(?<ext> (jpg|png|gif|svg) )                  )	(?<n>\n)?", nx);
		public static Regex imgurURLs = new Regex (@"\[ (?<label>[^\]]+) \]:	(?<space>\s*)	(?<url>[^\n?]+imgur.com[^\n?]* (?<id> \w{7}) (?<size> (s|b|t|m|l|h))?	\.	(?<ext> (jpg|png|gif|svg) ) (?<rev> \? \d+)? )	(?<n>\n)?", nx);
		public static Regex shieldURLs	= new Regex (@"\[ (?<label>[^\]]+) \]:	(?<space>\s*)	(?<url>[^\n?]*img.shields.io/ (?<sep> :|badge/) (?<name> ([^\-]|--)*) - (?<val> ([^\-]|--)*) - (?<col> ([^\-]|--)*) \.	(?<ext> (jpg|png|gif|svg))  )		(?<n>\n)?", nx);

		public static void MatchInputs ()
		{
			string pwd = Directory.GetCurrentDirectory ();
			string[] inputs = Directory.GetDirectories (pwd, indirs, recurse);

			DirectoryInfo pwd_di = new DirectoryInfo (pwd);

			foreach (string subdir in inputs) {
				DirectoryInfo subdir_di = new DirectoryInfo (subdir);

				Console.WriteLine ("IN:\t{0}\t -> {1}", pwd_di.RelFrom (pwd_di), subdir_di.RelFrom (pwd_di));

				string[] mds = Directory.GetFiles (subdir, dotmd, recurse);
				string[] bbcodes = Directory.GetFiles (subdir, dotbbcode, recurse);

				foreach (string file in mds) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("\t[MD]\t{0}\t -> {1}", subdir_di.RelFrom (pwd_di), file_di.RelFrom (subdir_di));
					string fileRAT = File.ReadAllText (file);
					string[] fileRAL = File.ReadAllLines (file);

					foreach (string line in fileRAL) {
						if (refURLs.IsMatch (line))
							Console.WriteLine ("\t\tREFURL:\t{0}", line);
						if (imageURLs.IsMatch (line))
							Console.WriteLine ("\t\tIMAGE:\t{0}", line);
						if (shieldURLs.IsMatch (line)) {
							Console.WriteLine ("\t\tSHIELD:\t{0}", line);
							var matches = shieldURLs.MatchesVerbose (line);
						}
						if (imgurURLs.IsMatch (line)) {
							var matches = imgurURLs.MatchesVerbose (line); // this is exploiting side effects!
						}
					}
				}

				foreach (string file in bbcodes) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("\t[BB]\t{0}\t -> {1}", subdir_di.RelFrom (pwd_di), file_di.RelFrom (subdir_di));
				}
			}
		}

		public static void MatchTemplates ()
		{
			string pwd = Directory.GetCurrentDirectory ();
			DirectoryInfo pwd_di = new DirectoryInfo (pwd);

			string[] templates = Directory.GetDirectories (pwd, templatedirs, recurse);
			foreach (string subdir in templates) {
				DirectoryInfo subdir_di = new DirectoryInfo (subdir);

				Console.WriteLine ("TEMPLATE:\t{0} -> {1}", pwd_di.RelFrom (pwd_di), subdir_di.RelFrom (pwd_di));

				string[] mds = Directory.GetFiles (subdir, dotmd, recurse);
				string[] bbcodes = Directory.GetFiles (subdir, dotbbcode, recurse);

				foreach (string file in mds) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("\t[MD]\t{0}\t -> {1}", subdir_di.RelFrom (pwd_di), file_di.RelFrom (subdir_di));
				}

				foreach (string file in bbcodes) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("\t[BB]\t{0}\t -> {1}", subdir_di.RelFrom (pwd_di), file_di.RelFrom (subdir_di));
				}
			}
		}

		public static void MatchOutputs ()
		{
			string pwd = Directory.GetCurrentDirectory ();
			DirectoryInfo pwd_di = new DirectoryInfo (pwd);
			string[] outputs = Directory.GetDirectories (pwd, outdirs, recurse);

			foreach (string subdir in outputs) {
				DirectoryInfo subdir_di = new DirectoryInfo (subdir);

				Console.WriteLine ("OUT:\t{0} -> {1}", pwd_di.RelFrom (pwd_di), subdir_di.RelFrom (pwd_di));

				string[] mds = Directory.GetFiles (subdir, dotmd, recurse);
				string[] bbcodes = Directory.GetFiles (subdir, dotbbcode, recurse);

				foreach (string file in mds) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("\t[MD]\t{0}\t -> {1}", subdir_di.RelFrom (pwd_di), file_di.RelFrom (subdir_di));
				}

				foreach (string file in bbcodes) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("\t[BB]\t{0}\t -> {1}", subdir_di.RelFrom (pwd_di), file_di.RelFrom (subdir_di));
				}
			}

		}



		public static void Main ()
		{ 
			Dictionary<string,string> AllRefLinks = new Dictionary<string,string> ();

			MatchInputs ();

			MatchTemplates ();

			MatchOutputs ();












		}
	}
}

