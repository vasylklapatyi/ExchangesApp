using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskWebApp1.Models
{
	public class EchangeRecord
	{
		public Guid Id { get; set; }
		public short FromCurrency { get; set; }
		public decimal FromAmmount { get; set; }
		public short ToCurrency { get; set; }
		public decimal ToAmmount { get; set; }
		public string Date { get; set; }
	}
}
