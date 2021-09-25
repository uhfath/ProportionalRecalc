using ProportionalRecalc.Services.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Services
{
	public class CalculationService
	{
		public IList<CalculationData> Calculations { get; } = new List<CalculationData>();

		private void Source_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			var calculation = Calculations
				.Single(c => c.Source == sender);

			var values = calculation.Source
				.Where(s => s.HasValue);

			calculation.SourceSum = values.Any()
				? values.Sum(s => s.Value)
				: null;
		}

		public CalculationService()
		{
			AddCalculationDestination(AddCalculation());
		}

		public CalculationData AddCalculation()
		{
			var data = new CalculationData();

			for (var i = 0; i < 5; i++)
			{
				data.Source.Add(null);
			}

			data.Source.CollectionChanged += Source_CollectionChanged;

			Calculations.Add(data);
			return data;
		}

		public void RemoveCalculation(CalculationData calculationData)
		{
			calculationData.Source.CollectionChanged -= Source_CollectionChanged;
			Calculations.Remove(calculationData);
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
