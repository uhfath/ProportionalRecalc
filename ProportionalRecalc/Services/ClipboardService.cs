using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProportionalRecalc.Services
{
	public class ClipboardService
	{
		private readonly IJSRuntime jsRuntime;

		public ClipboardService(
			IJSRuntime jsRuntime)
		{
			this.jsRuntime = jsRuntime;
		}

		public ValueTask<string> ReadTextAsync(CancellationToken cancellationToken = default) =>
			jsRuntime.InvokeAsync<string>("navigator.clipboard.readText", cancellationToken: cancellationToken);

		public ValueTask WriteTextAsync(string text, CancellationToken cancellationToken = default) =>
			jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", cancellationToken: cancellationToken, text);
	}
}
