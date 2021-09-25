using ProportionalRecalc.Services.Calculation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Services
{
	public class CalculationService
	{
		public IList<CalculationData> Calculations { get; } = new List<CalculationData>();

		private static int GetNumberOfDecimalDigits(decimal value)
		{
			var decimalPart = value - Math.Abs(Math.Truncate(value));
			return decimalPart != 0
				? decimalPart.ToString(CultureInfo.InvariantCulture).Split(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator)[1].Length
				: 0;
		}

		private void Source_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			var calculationData = Calculations
				.Single(c => c.Source == sender);

			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var destination in calculationData.Destinations)
					{
						destination.Values.Insert(e.NewStartingIndex, null);
					}

					break;

				case NotifyCollectionChangedAction.Remove:
					foreach (var destination in calculationData.Destinations)
					{
						destination.Values.RemoveAt(e.OldStartingIndex);
					}

					break;
			}

			Recalculate(calculationData);
		}

		private void Recalculate(CalculationData calculationData)
		{
			var values = calculationData.Source
				.Where(s => s.HasValue);

			calculationData.SourceSum = values.Any()
				? values.Sum(s => s.Value)
				: null;

			foreach (var destinationData in calculationData.Destinations)
			{
				RecalculateDestination(calculationData, destinationData);
			}
		}

		private void RecalculateDestination(CalculationData calculationData, CalculationDestinationData destinationData)
		{
			for (var i = 0; i < destinationData.Values.Count; i++)
			{
				destinationData.Values[i] = null;
			}

			if (destinationData.TargetSum.HasValue && calculationData.SourceSum.HasValue)
			{
				var ratio = destinationData.TargetSum.Value / calculationData.SourceSum.Value;

				var destinationValues = calculationData.Source
					.Select((s, i) => new
					{
						Index = i,
						Value = s,
					})
					.Where(s => s.Value.HasValue)
					.Select(s => new
					{
						s.Index,
						Source = s.Value.Value,
						SourceDecimalDigits = GetNumberOfDecimalDigits(s.Value.Value),
					})
					.Select(v => new
					{
						v.Index,
						v.Source,
						v.SourceDecimalDigits,
						Destination = Math.Round(v.Source * ratio, v.SourceDecimalDigits, MidpointRounding.AwayFromZero),
					})
					.ToArray();

				var sumDiff = destinationValues.Sum(v => v.Destination) - destinationData.TargetSum.Value;

				var destinationResults = destinationValues
					.Select(v => new
					{
						v.Index,
						v.Destination,
						v.SourceDecimalDigits,
						Ratio = v.Source / calculationData.SourceSum.Value * sumDiff,
					})
					.Select(v => new
					{
						v.Index,
						Destination = Math.Round(v.Destination - v.Ratio, v.SourceDecimalDigits, MidpointRounding.AwayFromZero)
					})
					.ToArray();

				foreach (var result in destinationResults)
				{
					destinationData.Values[result.Index] = result.Destination;
				}
			}
		}

		public CalculationService()
		{
			AddCalculationDestination(AddCalculation(), 0);
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

		public CalculationDestinationData AddCalculationDestination(CalculationData calculationData, int index)
		{
			var destinationData = new CalculationDestinationData();

			destinationData.TargetSum = calculationData.SourceSum;
			foreach (var source in calculationData.Source)
			{
				destinationData.Values.Add(source);
			}

			calculationData.Destinations.Insert(index, destinationData);
			RecalculateDestination(calculationData, destinationData);

			return destinationData;
		}

		public void RemoveCalculationDestinationData(CalculationData calculationData, int index)
		{
			calculationData.Destinations.RemoveAt(index);
		}

		public void RecalculateDestination(CalculationData calculationData, int index) =>
			RecalculateDestination(calculationData, calculationData.Destinations[index]);
	}
}
