using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;


namespace SCANdocs {
	public static class FSIExtensions {
		public static string RelFrom ( this FileSystemInfo to, FileSystemInfo from ) {
			return from.RelTo (to);
		}

		public static string RelTo ( this FileSystemInfo from, FileSystemInfo to ) {
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

		public static MatchCollection MatchesVerbose ( this Regex r, string input, string prefix = "" ) {
			var retval = r.Matches (input);
			foreach (Match m in r.Matches(input)) {
				var counter = 0;
				foreach (Group g in m.Groups) {
					if (counter == 0 || MakeDocs.meta.IsMatch (r.GroupNameFromNumber (counter))) {
						counter++;
						continue;
					}
					if (g.Success) Console.WriteLine ("{2}\t\t\t{0} -> {1}", r.GroupNameFromNumber (counter), g.Value, prefix);
					else Console.WriteLine ("{1}\t\t\t{0} -> [NO MATCH]", r.GroupNameFromNumber (counter), prefix);
					counter++;
				}
				foreach (Capture c in m.Captures) {
					//Console.WriteLine ("\t\tCapture:\t{0} -> {1}", imgurURLs.GroupNameFromNumber (c.Index), c.Value);
				}
			}
			return retval;

		}
	}


	public class MakeDocs {
		const RegexOptions nx = RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture;
		const RegexOptions mnx = RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Multiline;


		const SearchOption recurse = SearchOption.AllDirectories;
		const SearchOption here = SearchOption.TopDirectoryOnly;

		const string dotmd = "*.md";
		const string dotbbcode = "*.bbcode";
		const string dotjson = "*.json";


		const string indirs = "in";
		const string templatedirs = "templates";
		const string outdirs = "out";

		public MakeDocs () {
		}

		public static Regex faq = new Regex (@"(?<indent> [\s]+) (?<li> (\*|\+|\-)) (?<qspace>[\s]+) (?<q>[^\n]+) (?<qn> [\n])  (?<indent> [\s]+) (?<li> (\*|\+|\-)) (?<aspace>[\s]+) (?<shortanswer> \*\*[^\*]*\*\*) (?<saspace>[\s]*)? (?<a>[^\n]+) (?<an> [\n])", mnx);

		public static Regex albums2refs = new Regex (@"(?<name>[^:]+):(?<id>[^\n]+)(?<n>[\n])", nx);

		public static Regex albums = new Regex (@"(?<id_> ""id"":(?<id>""[^\""]+""))                            (?:[^}{]+?)
				(?<title_> ""title"":(?<title>""[^\""]+""|null))              (?:[^}{]+?)
				(?<desc_> ""description"":(?<desc>""[^\""]+""|null))          (?:[^}{]+?)
				(?<cover_> ""cover"":(?<cover>""[^\""]+""))                   (?:[^}{]+?)
				(?<privacy_> ""privacy"":(?<privacy>""[^\""]+""))             (?:[^}{]+?)
				(?<views_> ""views"":(?<views>[^\,]+))                        (?:[^}{]+?)
				(?<link_> ""link"":(?<link>""[^\""]+""))                      (?:[^}{]+?)
				(?<images_count_> ""images_count"":(?<images_count>[^\,]+))   (?:[^}{]+?)
				(?<images_> ""images"":\[ [^\]]+ \])", nx);

		public static Regex album_imgs = new Regex (@"(?<id_> ""id"":(?<id>""[^\""]+""))                              (?:[^}{]+?)
				(?<title_> ""title"":(?<title>""[^\""]+""|null))                (?:[^}{]+?)
				(?<desc_> ""description"":(?<desc>""[^\""]+""|null))            (?:[^}{]+?)
				(?<type_> ""type"":(?<type>""[^\""]+""))                        (?:[^}{]+?)
				(?<size_> ""size"":(?<size>[^\""]+))                            (?:[^}{]+?)
				(?<views_> ""views"":(?<views>[^\,]+))                          (?:[^}{]+?)
				(?<link_> ""link"":(?<link>""[^\""]+""))", nx);


		public static Regex meta = new Regex (@"_$");
		public static Regex unescape = new Regex (@"(?<fwslash> \/)", nx);
		// -> /


		public static Regex httpURL = new Regex (@"http://");
		public static string httpURL_use_https = "https://";

		public static Regex imgREFs = new Regex (@"!   \[ (?<alttext>[^\]]* ) \] \[ (?<ref>[^\]]* ) \]", nx);
		public static string imgREFs_id = "![${alttext}](${ref})";

		public static Regex imgINLs = new Regex (@"!   \[ (?<alttext>[^\]]* ) \] \( (?<ref>[^\)]* ) \)", nx);
		public static string imgINLs_id = "![${alttext}](${ref})";

		public static Regex urlREFs = new Regex (@"[^!]\[ (?<alttext>[^\]]* ) \] \[ (?<ref>[^\]]* ) \]", nx);
		public static string urlREFs_id = " [${alttext}][${ref}]";

		public static Regex urlINLs = new Regex (@"[^!]\[ (?<alttext>[^\]]* ) \] \( (?<ref>[^\)]* ) \)", nx);
		public static string urlINLs_id = " [${alttext}](${ref})";

		public static Regex refURLs = new Regex (@"\[ (?<label>[^\]]+) \]:	(?<space>\s*)	(?<url>[^\n]+)																																																						(?<n>\n)?", nx);
		public static string refURLs_id = "[${label}]:${space}${url}${n}";

		public static Regex imageURLs =	new Regex (@"\[ (?<label>[^\]]+) \]:	(?<space>\s*)	(?<url>[^\n?]+																												\.	(?<ext> (jpg|png|gif|svg) )                  )	(?<n>\n)?", nx);
		public static string imageURLs_id = "[${label}]:${space}${url}.${ext}${n}";

		public static Regex imgurURLs = new Regex (@"\[ (?<label>[^\]]+) \]:	(?<space>\s*)	(?<url>(?<path>[^\n?]+imgur.com[^\n?]* (?<id> \w{7}) (?<size> (s|b|t|m|l|h))?	\.	(?<ext> (jpg|png|gif|svg) )) (?<rev> \? \d+)? )	(?<n>\n)?", nx);
		public static string imgurURLs_id = "[${label}]:${space}${path}${id}${size}.${ext}${rev}${n}";

		public static Regex shieldURLs	= new Regex (@"\[ (?<label>[^\]]+) \]:	(?<space>\s*)	(?<url> (?<path>[^\n?]*img.shields.io/ (?<sep> :|badge/)) (?<name> ([^\-]|--)*) - (?<val> ([^\-]|--)*) - (?<col> ([^\-]|--)*) \.	(?<ext> (jpg|png|gif|svg))  )		(?<n>\n)?", nx);
		public static string shieldURLs_id = "[${label}]:${space}${path}${name}-${val}-${col}.${ext}${size}${ext}${n}";


		public static void MatchInputs ( ref Dictionary<string,List<MatchCollection>> matches ) {
			string pwd = Directory.GetCurrentDirectory ();
			string[] inputs = Directory.GetDirectories (pwd, indirs, recurse);

			DirectoryInfo pwd_di = new DirectoryInfo (pwd);

			foreach (string subdir in inputs) {
				DirectoryInfo subdir_di = new DirectoryInfo (subdir);

				Console.WriteLine ("[DIR]{0}\t -> {1}", pwd_di.RelFrom (pwd_di), subdir_di.RelFrom (pwd_di));

				string[] mds = Directory.GetFiles (subdir, dotmd, recurse);
				string[] bbcodes = Directory.GetFiles (subdir, dotbbcode, recurse);
				string[] jsons = Directory.GetFiles (subdir, dotjson, recurse);
				var blah = new List<MatchCollection> ();

				foreach (string file in mds) {
					FileInfo file_di = new FileInfo (file);
					Console.WriteLine ("[MD]\t{0}", file_di.RelFrom (subdir_di));

					// this is the per-file item
					string fileRAT = File.ReadAllText (file);

					// FAQs are multi-line because they are on two lines
					if (faq.IsMatch (fileRAT)) {
						Console.WriteLine ("[MD]\tFAQQ-A:\t{0} contains FAQ entries", file);
						if (matches.TryGetValue ("FAQ", out blah)) {
							MatchCollection mc = faq.MatchesVerbose(fileRAT, "[MD]\tFAQ-QA:");

							blah.Add(mc);

							matches.Remove ("FAQ");
							matches.Add ("FAQ", blah);
						}
					}

					// this is the per-line item
					string[] fileRAL = File.ReadAllLines (file);

					foreach (string line in fileRAL) {
						if (imgREFs.IsMatch (line)) {
							Console.WriteLine ("[MD]\timgREF:\t{0}", line);
							if (matches.TryGetValue ("imgREF", out blah)) {
								blah.Add (imgREFs.MatchesVerbose (line, "[MD]\timgREF:"));
								matches.Remove ("imgREF");
								matches.Add ("imgREF", blah);
							}
						}

						if (imgINLs.IsMatch (line)) {
							Console.WriteLine ("[MD]\timgINL:\t{0}", line);
							if (matches.TryGetValue ("imgINL", out blah)) {
								blah.Add (imgINLs.MatchesVerbose (line, "[MD]\timgINL:"));
								matches.Remove ("imgINL");
								matches.Add ("imgINL", blah);
							}
						}
						if (urlINLs.IsMatch (line)) {
							Console.WriteLine ("[MD]\turlINL:\t{0}", line);
							if (matches.TryGetValue ("urlINL", out blah)) {
								blah.Add (urlINLs.MatchesVerbose (line, "[MD]\turlINL:"));
								matches.Remove ("urlINL");
								matches.Add ("urlINL", blah);
							}
						}
						if (refURLs.IsMatch (line)) {
							Console.WriteLine ("[MD]\trefURL:\t{0}", line);
							if (matches.TryGetValue ("refURL", out blah)) {
								blah.Add (refURLs.MatchesVerbose (line, "[MD]\trefURL:"));
								matches.Remove ("refURL");
								matches.Add ("refURL", blah);
							}
						}

						if (imageURLs.IsMatch (line)) {
							Console.WriteLine ("[MD]\tIMAGE:\t{0}", line);
							if (matches.TryGetValue ("IMAGE", out blah)) {
								blah.Add (imageURLs.MatchesVerbose (line, "[MD]\tIMAGE:"));
								matches.Remove ("IMAGE");
								matches.Add ("IMAGE", blah);
							}
						}

						if (urlREFs.IsMatch (line)) {
							Console.WriteLine ("[MD]\turlREF:\t{0}", line);
							if (matches.TryGetValue ("urlREF", out blah)) {
								blah.Add (urlREFs.MatchesVerbose (line, "[MD]\turlREF:"));
								matches.Remove ("urlREF");
								matches.Add ("urlREF", blah);
							}
						}

						if (shieldURLs.IsMatch (line)) {
							Console.WriteLine ("[MD]\tSHIELD:\t{0}", line);
							if (matches.TryGetValue ("SHIELD", out blah)) {
								blah.Add (shieldURLs.MatchesVerbose (line, "[MD]\tSHIELD:"));
								matches.Remove ("SHIELD");
								matches.Add ("SHIELD", blah);
							}
						}

						if (imgurURLs.IsMatch (line)) {
							Console.WriteLine ("[MD]\tIMGUR:\t{0}", line);

							if (matches.TryGetValue ("IMGUR", out blah)) {
								var thing = imgurURLs.MatchesVerbose (line, "[MD]\tIMGUR:");
								blah.Add (thing);
								matches.Remove ("IMGUR");
								matches.Add ("IMGUR", blah);
							}
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
						if (albums.IsMatch (line)) {
							Console.WriteLine ("[JSON]  ALBUM:\t{0}", line);
							if (matches.TryGetValue ("ALBUM", out blah)) {
								blah.Add (albums.MatchesVerbose (line, "[MD]\tALBUM:"));
								matches.Remove ("ALBUM");
								matches.Add ("ALBUM", blah);
							}
						}

						if (album_imgs.IsMatch (line)) {
							Console.WriteLine ("[JSON]  ALIMG:\t{0}", line);
							if (matches.TryGetValue ("ALIMG", out blah)) {
								blah.Add (album_imgs.MatchesVerbose (line, "[MD]\tALIMG:"));
								matches.Remove ("ALIMG");
								matches.Add ("ALIMG", blah);
							}
						}
					}
				}
			}
		}

		public static void MatchTemplates ( ref Dictionary<string,List<MatchCollection>> matches ) {
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

		public static void MatchOutputs ( ref Dictionary<string,List<MatchCollection>> matches ) {
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



		public static void Main () { 

			Dictionary<string,List<MatchCollection>> requestedMatches = new Dictionary<string,List<MatchCollection>> ();

			foreach (string type in new[] {"FAQ", "imgREF", "refURL", "imgINL", "urlINL", "IMGUR", "ALBUM", "ALIMG", "SHIELD"}) {
				requestedMatches.Add (type, new List<MatchCollection> ());
			}

			MatchInputs (ref requestedMatches);


			MatchTemplates (ref requestedMatches);
			MatchOutputs (ref requestedMatches);

			foreach (KeyValuePair<string,List<MatchCollection>> kvp in requestedMatches) {
				List<MatchCollection> lmc = kvp.Value;
				int count = 0;

				foreach (MatchCollection mc in lmc) {
					count += mc.Count;
				}

				Console.WriteLine("{0} => {1} collections (with {2} elements)", kvp.Key, kvp.Value.Count, count);
			}


		}
	}
}

