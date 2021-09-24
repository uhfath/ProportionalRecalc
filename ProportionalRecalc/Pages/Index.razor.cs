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
			CalculationService.AddCalculation();
		}

		private void OnRowInsert(CalculationData calculationData, int index)
		{
			calculationData.Source.Insert(index, null);
		}

		private void OnRowRemove(CalculationData calculationData, int index)
		{
			calculationData.Source.RemoveAt(index);
		}
	}
}
