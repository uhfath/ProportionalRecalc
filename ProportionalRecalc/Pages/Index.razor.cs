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
		private int columns;

		[Inject]
		public ClipboardService ClipboardService { get; set; }

		private void AddNewCalculation()
		{
			throw new NotImplementedException();
		}

		private void AddNewColumn()
		{
			++columns;
		}

		private void DeleteColumn()
		{
			--columns;
		}

		protected override void OnInitialized()
		{
		}
	}
}
