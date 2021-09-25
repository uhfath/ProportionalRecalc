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
			var calculation = calculationDatas
				.Single(c => c.Source == sender);

			var values = calculation.Source
				.Where(s => s.HasValue);

			calculation.SourceSum = values.Any()
				? values.Sum(s => s.Value)
				: null;
		}

		public CalculationService()
		{
			AddCalculation();
		}

		public CalculationData AddCalculation()
		{
			var data = new CalculationData();

			for (var i = 0; i < 5; i++)
			{
				data.Source.Add(null);
			}

			data.Source.CollectionChanged += Source_CollectionChanged;

			calculationDatas.Add(data);
			return data;
		}

		public void RemoveCalculation(CalculationData calculationData)
		{
			calculationData.Source.CollectionChanged -= Source_CollectionChanged;
			calculationDatas.Remove(calculationData);
		}

		public CalculationDestinationData AddCalculationDestination(CalculationData calculationData)
		{
			var destinationData = new CalculationDestinationData();

			destinationData.TargetSum = calculationData.SourceSum;
			foreach (var source in calculationData.Source)
			{
				destinationData.Values.Add(source);
			}

			calculationData.Destinations.Add(destinationData);
			return destinationData;
		}

		public void RemoveCalculationDestinationData(CalculationData calculationData, CalculationDestinationData destinationData)
		{
			calculationData.Destinations.Remove(destinationData);
		}
	}
}
