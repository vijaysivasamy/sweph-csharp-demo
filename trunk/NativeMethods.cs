using System.Runtime.InteropServices;
using System.Text;

namespace swephcs
{
	public static class NativeMethods
	{
		[DllImport(@"lib\swedll32.dll")]
		public static extern int swe_set_ephe_path(StringBuilder path);

		[DllImport(@"lib\swedll32.dll")]
		public static extern double swe_julday(int year, int month, int day, double hour, int gregflag);

		[DllImport(@"lib\swedll32.dll")]
		public static extern int swe_calc_ut(double tjd_ut, int ipl, int iflag, double[] xx, StringBuilder serr);
		
		[DllImport(@"lib\swedll32.dll")]
		public static extern int swe_calc(double tjd, int ipl, int iflag, double[] xx, StringBuilder serr);

		[DllImport(@"lib\swedll32.dll")]
		public static extern void swe_close();

		[DllImport(@"lib\swedll32.dll")]
		public static extern double swe_deltat(double tjd);
	}
}
