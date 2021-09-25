using Microsoft.AspNetCore.Components;
using ProportionalRecalc.Services;
using ProportionalRecalc.Services.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Pages
{
	public partial class Index : ComponentBase
	{
		[Inject]
		public ClipboardService ClipboardService { get; set; }

		[Inject]
		public CalculationService CalculationService { get; set; }

		private void AddNewCalculation()
		{
			CalculationService.AddCalculationDestination(CalculationService.AddCalculation(), 0);
		}

		private void RemoveCalculation(CalculationData calculationData)
		{
			CalculationService.RemoveCalculation(calculationData);
		}

		private void OnRowInsert(CalculationData calculationData, int index)
		{
			calculationData.Source.Insert(index + 1, null);
		}

		private void OnRowRemove(CalculationData calculationData, int index)
		{
			calculationData.Source.RemoveAt(index);
		}

		private void AddColumn(CalculationData calculationData, int index)
		{
			CalculationService.AddCalculationDestination(calculationData, index + 1);
		}

		private void RemoveColumn(CalculationData calculationData, int index)
		{
			CalculationService.RemoveCalculationDestinationData(calculationData, index);
		}
	}
}
