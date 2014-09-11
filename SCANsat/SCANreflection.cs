/* 
 * [Scientific Committee on Advanced Navigation]
 * 			S.C.A.N. Satellite
 *
 * SCANreflection - assigns reflection methods at startup
 * 
 * Copyright (c)2014 David Grandy <david.grandy@gmail.com>;
 * Copyright (c)2014 technogeeky <technogeeky@gmail.com>;
 * Copyright (c)2014 (Your Name Here) <your email here>; see LICENSE.txt for licensing details.
 */

using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SCANsat
{
	[KSPAddon(KSPAddon.Startup.MainMenu, true)]
	class SCANreflection : MonoBehaviour
	{
		internal static bool ORSFound;

		private const string ORSPlanetDataType = "OpenResourceSystem.ORSPlanetaryResourceMapData";
		private const string ORSPixelAbundanceMethod = "getPixelAbundanceValue";
		private const string ORSAssemblyName = "OpenResourceSystem_1_3_0";
		private const string ORSVersion = "1.3.0.0";

		private static bool ORSRun = false;

		private delegate double ORSpixelAbundance(int body, string resourceName, double lat, double lon);

		private static AssemblyLoader.LoadedAssembly ORSAssembly;

		private static ORSpixelAbundance _ORSpixelAbundance;

		internal static Type _ORSPlanetType;

		internal static double ORSpixelAbundanceValue(int body, string resourceName, double lat, double lon)
		{
			return _ORSpixelAbundance(body, resourceName, lat, lon);
		}


		private void Start()
		{
			ORSFound = ORSReflectionMethod();
		}

		private static bool ORSAssemblyLoaded()
		{
			if (ORSAssembly != null)
				return true;

			AssemblyLoader.LoadedAssembly assembly = AssemblyLoader.loadedAssemblies.FirstOrDefault(a => a.assembly.GetName().Name == ORSAssemblyName);
			if (assembly != null)
			{
				if (assembly.assembly.GetName().Version.ToString() == ORSVersion)
				{
					SCANUtil.SCANlog("ORS Assembly Loaded");
					ORSAssembly = assembly;
					return true;
				}
				else
				{
					Debug.LogWarning(string.Format("[SCANsat] Incompatible ORS Assembly Detected: Version [{0}]; Delete All Copies of ORS Other Than {1}", assembly.assembly.GetName().Version, ORSAssemblyName));
					return false;
				}
			}

			SCANUtil.SCANlog("ORS Assembly Not Found");
			return false;
		}

		private static bool ORSReflectionMethod()
		{
			if (ORSAssemblyLoaded() == false)
				return false;

			if (_ORSpixelAbundance != null)
				return true;

			if (ORSRun)
				return false;

			ORSRun = true;

			try
			{
				Type ORSType = ORSAssembly.assembly.GetExportedTypes()
					.SingleOrDefault(t => t.FullName == ORSPlanetDataType);

				if (ORSType == null)
				{
					SCANUtil.SCANlog("ORS Type Not Found");
					return false;
				}

				_ORSPlanetType = ORSType;

				MethodInfo ORSMethod = ORSType.GetMethod(ORSPixelAbundanceMethod, new Type[] { typeof(int), typeof(string), typeof(double), typeof(double) });

				if (ORSMethod == null)
				{
					SCANUtil.SCANlog("ORS Method Not Found");
					return false;
				}

				_ORSpixelAbundance = (ORSpixelAbundance)Delegate.CreateDelegate(typeof(ORSpixelAbundance), ORSMethod);

				SCANUtil.SCANlog("ORS Reflection Method Assigned");

				return true;
			}
			catch (Exception e)
			{
				Debug.LogWarning("[SCANsat] Exception While Loading ORS Reflection Method: " + e);
			}

			return false;
		}
	}
}
