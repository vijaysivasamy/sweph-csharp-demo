using System.Runtime.InteropServices;
using System.Text;

namespace swephcs
{
	public static class NativeMethods
	{
		/// <summary>
		/// Set the directory path of the ephemeris files
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		[DllImport(@"lib\swedll32.dll")]
		public static extern int swe_set_ephe_path(StringBuilder path);

		/// <summary>
		/// Computes the Julian day number
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <param name="day"></param>
		/// <param name="hour"></param>
		/// <param name="gregflag"></param>
		/// <returns></returns>
		[DllImport(@"lib\swedll32.dll")]
		public static extern double swe_julday(int year, int month, int day, double hour, int gregflag);

		/// <summary>
		/// Compute positions of planets, asteroids, lunar nodes and apogees
		/// </summary>
		/// <param name="tjd_ut">Julian day, Universal Time</param>
		/// <param name="ipl">Body number. Zero for the Sun</param>
		/// <param name="iflag">The flag bits are defined in such a way that iflag = 0
		/// delivers what one usually wants:
		///		- the default ephemeris (SWISS EPHEMERIS) is used,
		///		- apparent geocentric positions referring to the true equinox of dateare returned.</param>
		/// <param name="xx">Array of 6 doubles for longitude, latitude, distance,
		/// speed in long., speed in lat., and speed in dist. It will hold the result</param>
		/// <param name="serr">Error string</param>
		/// <returns>Error flag</returns>
		[DllImport(@"lib\swedll32.dll")]
		public static extern int swe_calc_ut(double tjd_ut, int ipl, int iflag, double[] xx, StringBuilder serr);
		
		/// <summary>
		/// Compute positions of planets, asteroids, lunar nodes and apogees
		/// </summary>
		/// <param name="tjd">Requires Ephemeris Time ( more accurate: Dynamical Time ) as a parameter</param>
		/// <param name="ipl">Body number. Zero for the Sun</param>
		/// <param name="iflag">A 32 bit integer containing bit flags that indicate what kind of computation is wanted</param>
		/// <param name="xx">Array of 6 doubles for longitude, latitude, distance, speed in long., speed in lat., and speed in dist</param>
		/// <param name="serr">Character string to return error messages in case of error</param>
		/// <returns></returns>
		[DllImport(@"lib\swedll32.dll")]
		public static extern int swe_calc(double tjd, int ipl, int iflag, double[] xx, StringBuilder serr);

		/// <summary>
		/// At the end of your computations close all files and free memory
		/// </summary>
		[DllImport(@"lib\swedll32.dll")]
		public static extern void swe_close();

		/// <summary>
		/// The Julian day number, you compute from a birth date, will be Universal Time (UT,  former GMT)
		/// and can be used to compute the star time and the houses.
		/// However, for the planets and the other factors, you have to convert UT to Ephemeris time (ET)
		/// </summary>
		/// <param name="tjd"></param>
		/// <returns></returns>
		[DllImport(@"lib\swedll32.dll")]
		public static extern double swe_deltat(double tjd);
	}
}
