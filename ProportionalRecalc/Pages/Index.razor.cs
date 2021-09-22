using Microsoft.AspNetCore.Components;
using ProportionalRecalc.Services;
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

		protected override void OnInitialized()
		{
		}
	}
}
