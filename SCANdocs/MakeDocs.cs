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

		public static MatchCollection MatchesVerbose (this Regex r, string input, string prefix = "")
		{
			var retval = r.Matches (input);
			foreach (Match m in r.Matches(input)) {
				var counter = 0;
				foreach (Group g in m.Groups) {
					if (counter == 0 || MakeDocs.meta.IsMatch(r.GroupNameFromNumber(counter))) {
						counter++;
						continue;
					}
					if (g.Success)
						Console.WriteLine ("{2}\t\t\t{0} -> {1}", r.GroupNameFromNumber (counter), g.Value,prefix);
					else
						Console.WriteLine ("{1}\t\t\t{0} -> [NO MATCH]", r.GroupNameFromNumber (counter),prefix);
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
		const string dotjson = "*.json";


		const string indirs = "in";
		const string templatedirs = "templates";
		const string outdirs = "out";

		public MakeDocs ()
		{
		}

		public static Regex faq = new Regex(@"(?<indent> [\s]+) (?<li> (\*|\+|\-)) (?<qspace>[\s]+) (?<q>[^\n]+) (?<qn> [\n])  (?<indent> [\s]+) (?<li> (\*|\+|\-)) (?<aspace>[\s]+) (?<shortanswer> \*\*[^\*]*\*\*) (?<saspace>[\s]*)? (?<a>[^\n]+) (?<an> [\n])",nx);

		public static Regex albums2refs = new Regex(@"(?<name>[^:]+):(?<id>[^\n]+)(?<n>[\n])",nx);

		public static Regex albums = new Regex(
			@"(?<id_> ""id"":(?<id>""[^\""]+""))                            (?:[^}{]+?)
				(?<title_> ""title"":(?<title>""[^\""]+""|null))              (?:[^}{]+?)
				(?<desc_> ""description"":(?<desc>""[^\""]+""|null))          (?:[^}{]+?)
				(?<cover_> ""cover"":(?<cover>""[^\""]+""))                   (?:[^}{]+?)
				(?<privacy_> ""privacy"":(?<privacy>""[^\""]+""))             (?:[^}{]+?)
				(?<views_> ""views"":(?<views>[^\,]+))                        (?:[^}{]+?)
				(?<link_> ""link"":(?<link>""[^\""]+""))                      (?:[^}{]+?)
				(?<images_count_> ""images_count"":(?<images_count>[^\,]+))   (?:[^}{]+?)
				(?<images_> ""images"":\[ [^\]]+ \])",
			nx);

		public static Regex album_imgs = new Regex(
			@"(?<id_> ""id"":(?<id>""[^\""]+""))                              (?:[^}{]+?)
				(?<title_> ""title"":(?<title>""[^\""]+""|null))                (?:[^}{]+?)
				(?<desc_> ""description"":(?<desc>""[^\""]+""|null))            (?:[^}{]+?)
				(?<type_> ""type"":(?<type>""[^\""]+""))                        (?:[^}{]+?)
				(?<size_> ""size"":(?<size>[^\""]+))                            (?:[^}{]+?)
				(?<views_> ""views"":(?<views>[^\,]+))                          (?:[^}{]+?)
				(?<link_> ""link"":(?<link>""[^\""]+""))",
			nx);

		public static Regex meta = new Regex(@"_$");
		public static Regex unescape = new Regex(@"(?<fwslash> \/)",nx); // -> /


		public static Regex httpURL = new Regex(@"http://");
		public static Regex imgREFs = new Regex(@"!   \[ (?<alttext>[^\]]* ) \] \[ (?<ref>[^\]]* ) \]",nx);
		public static Regex imgINLs = new Regex(@"!   \[ (?<alttext>[^\]]* ) \] \( (?<ref>[^\)]* ) \)",nx);
		public static Regex urlREFs = new Regex(@"[^!]\[ (?<alttext>[^\]]* ) \] \[ (?<ref>[^\]]* ) \]",nx);
		public static Regex urlINLs = new Regex(@"[^!]\[ (?<alttext>[^\]]* ) \] \( (?<ref>[^\)]* ) \)",nx);

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

				Console.WriteLine ("[DIR]{0}\t -> {1}", pwd_di.RelFrom (pwd_di), subdir_di.RelFrom (pwd_di));

				string[] mds = Directory.GetFiles (subdir, dotmd, recurse);
				string[] bbcodes = Directory.GetFiles (subdir, dotbbcode, recurse);
				string[] jsons = Directory.GetFiles (subdir, dotjson, recurse);

				foreach (string file in mds) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("[MD]\t{0}", file_di.RelFrom (subdir_di));
					//string fileRAT = File.ReadAllText (file);
					string[] fileRAL = File.ReadAllLines (file);

					foreach (string line in fileRAL) {
						if (faq.IsMatch (line)) {
							Console.WriteLine ("[MD]\tFAQQ-A:\t{0}", line);
							var matches = faq.MatchesVerbose (line,"[MD]\tFAQq-a:");
						}
						if (imgREFs.IsMatch (line)) {
							Console.WriteLine ("[MD]\timgREF:\t{0}", line);
							var matches = imgREFs.MatchesVerbose (line,"[MD]\timgREF:");
						}
						if (imgINLs.IsMatch (line))
							Console.WriteLine ("[MD]\timgINL:\t{0}", line);
						if (urlREFs.IsMatch (line)) {
							Console.WriteLine ("[MD]\turlREF:\t{0}", line);
							var matches = urlREFs.MatchesVerbose (line,"[MD]\tURLref:");
						}
						if (urlINLs.IsMatch (line))
							Console.WriteLine ("[MD]\turlINL:\t{0}", line);
						if (refURLs.IsMatch (line))
							Console.WriteLine ("[MD]\tREFURL:\t{0}", line);
						if (imageURLs.IsMatch (line))
							Console.WriteLine ("[MD]\tIMAGE:\t{0}", line);
						if (shieldURLs.IsMatch (line)) {
							Console.WriteLine ("[MD]\tSHIELD:\t{0}", line);
							var matches = shieldURLs.MatchesVerbose (line,"[MD]\tSHIELD:");
						}
						if (imgurURLs.IsMatch (line)) {
							Console.WriteLine ("[MD]\tIMGUR:\t{0}", line);
							var matches = imgurURLs.MatchesVerbose (line,"[MD]\tIMGUR:"); // this is exploiting side effects!
						}
					}
				}

				foreach (string file in bbcodes) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("[BB]\t{0}", file_di.RelFrom (subdir_di));
				}

				foreach (string file in jsons) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("[JSON]\t{0}", file_di.RelFrom (subdir_di));
					//string fileRAT = File.ReadAllText (file);
					string[] fileRAL = File.ReadAllLines (file);

					foreach (string line in fileRAL) {
						if (albums.IsMatch(line)) {
							Console.WriteLine ("[JSON]  ALBUM:\t{0}", line);
							var matches = albums.MatchesVerbose (line,"[JSON]  ALBUM:");
						} 
						if (album_imgs.IsMatch(line)) {
							Console.WriteLine ("[JSON]  entry:\t{0}", line);
							var matches = album_imgs.MatchesVerbose (line,"[JSON]  entry:");
						}
					}
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

				Console.WriteLine ("[DIR] \t{0} -> {1}", pwd_di.RelFrom (pwd_di), subdir_di.RelFrom (pwd_di));

				string[] mds = Directory.GetFiles (subdir, dotmd, recurse);
				string[] bbcodes = Directory.GetFiles (subdir, dotbbcode, recurse);

				foreach (string file in mds) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("[MD]\t{0}", file_di.RelFrom (subdir_di));
				}

				foreach (string file in bbcodes) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("[BB]\t{0}", file_di.RelFrom (subdir_di));
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

				Console.WriteLine ("[DIR]\t{0} -> {1}", pwd_di.RelFrom (pwd_di), subdir_di.RelFrom (pwd_di));

				string[] mds = Directory.GetFiles (subdir, dotmd, recurse);
				string[] bbcodes = Directory.GetFiles (subdir, dotbbcode, recurse);

				foreach (string file in mds) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("[MD]\t{0}", file_di.RelFrom (subdir_di));
				}

				foreach (string file in bbcodes) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("[BB]\t{0}", file_di.RelFrom (subdir_di));
				}
			}
		}



		public static void Main ()
		{ 
			Dictionary<string,string> AllRefLinks = new Dictionary<string,string> ();

			Dictionary<string,MatchCollection[]> matches = new Dictionary<string,MatchCollection[]>();
			MatchInputs ();

			MatchTemplates ();

			MatchOutputs ();












		}
	}
}

