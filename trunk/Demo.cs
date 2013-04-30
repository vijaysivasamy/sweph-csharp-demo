using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swephcs
{
	class Demo
	{
		static int year = 0;
		static int month = 0;
		static int day = 0;
		static int hour = 0;
		static int min = 0;

		static void Main()
		{
			// == 00. Set path to ephemeris data file ==========================
			StringBuilder path = new StringBuilder(@".\ephe");
			int ret = NativeMethods.swe_set_ephe_path(path);

			// == 01. Get Julian Day number ====================================
			ReadDateTime();
			double hourFull = (double)hour + ((double)min / 60.0);

			// Print date and time
			Console.WriteLine();
			Console.WriteLine("Date/Time (yyyy-MM-dd HH:mm): {0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2} UT",
								year, month, day, (int)hour, (int)min);

			// Calculate Julian day number (for further computation)
			double julianDate = NativeMethods.swe_julday(year, month, day, hourFull, 1);
			double deltat = NativeMethods.swe_deltat(julianDate);

			// Calculate ephemeris day number (just for information)
			double ephemerisDate = julianDate + deltat;

			Console.WriteLine("UT: {0:f11}; delta t: {1:f6} sec. ({2:f11})\r\nET: {3:f11}", julianDate,
								(deltat * 86400), deltat, ephemerisDate);


			// == 02. Compute Sun position ================================
			// Position part
			// Array of 6 doubles for longitude, latitude, distance, speed in long., speed in lat., and speed in dist.
			// It will hold the result
			double[] positionArray = new double[6];

			// Initialize positions array
			for (int i = 0; i < positionArray.Length; i++)
			{ positionArray[i] = 0.0; }

			// Error result
			StringBuilder serr = new StringBuilder(256);

			// Calculate SUN position
			int resFlags = NativeMethods.swe_calc_ut(julianDate, 0, 0, positionArray, serr);
			//int resFlags = NativeMethods.swe_calc(ephemerisDate, 0, 0, positionArray, serr);
			if (resFlags != 0)
			{
				Console.WriteLine("Flags = {0}", resFlags);
			}

			if (!string.IsNullOrEmpty(serr.ToString()))
			{
				Console.WriteLine("Error string = '{0}'", serr);
			}

			Console.WriteLine();
			decimal raDegree = (decimal)positionArray[0];

			// Output Sun result
			Console.WriteLine("Sun R.A. degrees = {0}", MakeDegreesString(raDegree));
			Console.WriteLine("Sun R.A. decimal = {0:f10}", raDegree);

			// == 03. Close files if any used ================================
			NativeMethods.swe_close();

			Console.ReadKey();
		}

		/// <summary>
		/// Calculate degrees with minutes an seconds
		/// </summary>
		/// <param name="degrees">Decimal representation of degrees</param>
		/// <returns></returns>
		static string MakeDegreesString(decimal degrees)
		{
			decimal degMain = Math.Floor(degrees);
			decimal degSub = ((degrees - degMain) * 6.0M * 100.0M) / 10.0M;
			decimal degMin = Math.Floor(degSub);
			decimal degSec = ((degSub - degMin) * 6.0M * 100.0M) / 10.0M;

			return String.Format("{0:f0}° {1:f0}'{2:f4}", degMain, degMin, degSec);
		}

		static void ReadDateTime()
		{
			DateTime today = DateTime.Today;
			String formatStr = "{0,-14}: ";
			
			// Date part
			Console.Write(formatStr, "Enter year");
			bool result = int.TryParse(Console.ReadLine(), out year);
			if (!result)
			{
				year = today.Year;
			}

			Console.Write(formatStr, "Enter month");
			result = int.TryParse(Console.ReadLine(), out month);
			if (!result)
			{
				month = today.Month;
			}

			Console.Write(formatStr, "Enter day");
			result = int.TryParse(Console.ReadLine(), out day);
			if (!result)
			{
				day = today.Day;
			}

			// Time part
			Console.Write(formatStr, "Enter hour");
			result = int.TryParse(Console.ReadLine(), out hour);

			Console.Write(formatStr, "Enter minutes");
			result = int.TryParse(Console.ReadLine(), out min);
		}
	}
}
