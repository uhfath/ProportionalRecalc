using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProportionalRecalc.Shared
{
	public partial class InputDecimal : ComponentBase
	{
		private static readonly HashSet<char> AllowedSymbols = new[] { '-', '+', '.', ',', }.ToHashSet();
		private static readonly HashSet<string> InsertRowShortcusts = new[] { "Equal", "NumpadAdd" }.ToHashSet();

		private decimal? decimalValue;

		[Parameter(CaptureUnmatchedValues = true)]
		public IDictionary<string, object> AdditionalAttributes { get; set; }

		[Parameter]
		public decimal? Value
		{
			get { return decimalValue; }
			set { decimalValue = value; ValueText = decimalValue?.ToString("G", CultureInfo.CurrentCulture); }
		}

		private string ValueText { get; set; }

		[Parameter]
		public EventCallback<decimal?> OnChanged { get; set; }

		[Parameter]
		public EventCallback OnRowInsertShortcut { get; set; }

		[Parameter]
		public bool IsReadOnly { get; set; }

		private static string GetDigits(string value) =>
			new string(value
				.Where(v => char.IsDigit(v) || AllowedSymbols.Contains(v))
				.ToArray());

		private Task InvokeIfChanged(decimal? value)
		{
			if (Value != value)
			{
				Value = value;
				return OnChanged.InvokeAsync(Value);
			}

			return Task.CompletedTask;
		}

		private Task OnClear()
		{
			ValueText = null;
			return InvokeIfChanged(null);
		}

		private Task OnKeyUp(KeyboardEventArgs args)
		{
			if (args.AltKey && InsertRowShortcusts.Contains(args.Code))
			{
				return OnRowInsertShortcut.InvokeAsync();
			}

			return Task.CompletedTask;
		}

		private Task OnInput(ChangeEventArgs args)
		{
			ValueText = args.Value.ToString();
			if (string.IsNullOrWhiteSpace(ValueText))
			{
				return InvokeIfChanged(null);
			}

			var valueCleaned = GetDigits(ValueText)
				.Replace(',', '.')
			;

			if (decimal.TryParse(valueCleaned, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var numberValue))
			{
				return InvokeIfChanged(numberValue);
			}

			return InvokeIfChanged(null);
		}
	}
}
