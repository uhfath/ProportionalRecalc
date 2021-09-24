using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Services.Calculation
{
	public class CalculationItem
	{
		public decimal? Source { get; set; }
		public ICollection<decimal?> Destinations { get; set; }
	}
}
