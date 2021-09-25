using Microsoft.AspNetCore.Components;
using ProportionalRecalc.Services.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Shared
{
	public partial class CalculationNode : ComponentBase
	{
		[Parameter]
		public CalculationData Calculation { get; set; }

		[Parameter]
		public EventCallback<int> OnColumnAdd { get; set; }

		[Parameter]
		public EventCallback<int> OnColumnRemove { get; set; }

		[Parameter]
		public EventCallback<int> OnRowInsert { get; set; }

		[Parameter]
		public EventCallback<int> OnRowRemove { get; set; }

		[Parameter]
		public EventCallback OnCalculationDelete { get; set; }

		[Parameter]
		public EventCallback<int> OnDestinationRecalculate { get; set; }

		private void OnCellChanged(int index, decimal? value)
		{
			Calculation.Source[index] = value;
			StateHasChanged();
		}

		private void OnTargetSumChanged(int index, decimal? value)
		{
			Calculation.Destinations[index].TargetSum = value;
			OnDestinationRecalculate.InvokeAsync(index);
		}
	}
}
