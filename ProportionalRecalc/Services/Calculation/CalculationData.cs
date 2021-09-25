using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Services.Calculation
{
	public class CalculationData
	{
		public ObservableCollection<decimal?> Source { get; } = new ObservableCollection<decimal?>();
		public decimal? SourceSum { get; set; }

		public IList<CalculationDestinationData> Destinations { get; } = new List<CalculationDestinationData>();
	}
}
