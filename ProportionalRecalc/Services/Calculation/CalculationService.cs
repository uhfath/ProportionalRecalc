using ProportionalRecalc.Services.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Services
{
	public class CalculationService
	{
		private ICollection<CalculationData> calculationDatas = new List<CalculationData>();

		public IEnumerable<CalculationData> Calculations { get => calculationDatas; }

		private void Source_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
		}

		public CalculationData AddCalculation()
		{
			var data = new CalculationData();
			data.Source.Add(null);
			data.Source.CollectionChanged += Source_CollectionChanged;

			calculationDatas.Add(data);
			return data;
		}

		public void RemoveCalculation(CalculationData calculationData)
		{
			calculationData.Source.CollectionChanged -= Source_CollectionChanged;
			calculationDatas.Remove(calculationData);
		}
	}
}
