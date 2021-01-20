using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestTaskWebApp1.Models;

namespace TestTaskWebApp1.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ExchangesController : ControllerBase
	{
		DbEntities _context;
		public ExchangesController(DbEntities context)
		{ 
			_context = context;
		}

		[HttpGet]
		public ActionResult DropDownData_Currency() => new JsonResult(_context.Currencies.ToArray());
		
		public class requestModel
		{
			public int from_currency_id { get; set; }
			public int to_currency_id { get; set; }
			public decimal value { get; set; }
		}
		[HttpPost]
		public async Task<ActionResult> Exchange(requestModel model)
		{
			HttpClient client = new HttpClient();
			decimal result;
			var fromCurrency = _context.Currencies.ToList().Where(x => x.Id == model.from_currency_id).FirstOrDefault();
			var toCurrency = _context.Currencies.ToList().Where(x => x.Id == model.to_currency_id).FirstOrDefault();

			var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.exchangeratesapi.io/latest?base={fromCurrency.Name}");
			var response = await client.SendAsync(request);
			string responseString = ((new StreamReader(await response.Content.ReadAsStreamAsync())).ReadToEnd());
			
			var definition = new { @base = "", rates = new Dictionary<string, string>(), date = "" };
			var _data = JsonConvert.DeserializeAnonymousType(responseString, definition);
			
			decimal toCurrencyRate = 0;
			Decimal.TryParse(_data.rates[toCurrency.Name], System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out toCurrencyRate);

			result = model.value * toCurrencyRate;

			_context.EchangeRecords.Add(new EchangeRecord()
			{
				Date = DateTime.Now.ToString(),
				FromAmmount = model.value,
				FromCurrency = (short)fromCurrency.Id,
				Id = Guid.NewGuid(),
				ToAmmount = result,
				ToCurrency = (short)toCurrency.Id
			});
			_context.SaveChangesAsync();

			return new JsonResult(result);
		}



		[HttpGet]
		public JsonResult GetLogs()
		{
			var currencies = _context.Currencies.ToArray();
			var logs = _context.EchangeRecords.ToList();
			var Data = logs.Select(x => new 
			{
				Date = x.Date,
				FromAmmount = x.FromAmmount,
				ToAmmount = x.ToAmmount,
				Id = x.Id,
				FromCurrency = currencies.Where(y => y.Id == x.FromCurrency).FirstOrDefault()?.Name,
				ToCurrency = currencies.Where(y => y.Id == x.ToCurrency).FirstOrDefault()?.Name
			});

			return new JsonResult(Data);
		}
	}
}
