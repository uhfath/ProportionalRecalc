using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ProportionalRecalc.Shared
{
	public partial class InputDecimal : ComponentBase
	{
		[Parameter(CaptureUnmatchedValues = true)]
		public IDictionary<string, object> AdditionalAttributes { get; set; }

		[Parameter]
		public decimal? Value { get; set; }

		[Parameter]
		public EventCallback<decimal?> OnChanged { get; set; }

		private Task OnInput(ChangeEventArgs e)
		{
			Value = null;
			if (e.Value != null && decimal.TryParse(e.Value.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out var numberValue))
			{
				Value = numberValue;
			}

			return OnChanged.InvokeAsync(Value);
		}
	}
}
