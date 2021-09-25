using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Services.Calculation
{
	public class CalculationDestinationData
	{
		public decimal? TargetSum { get; set; }
		public IList<decimal?> Values { get; } = new List<decimal?>();
	}
}
