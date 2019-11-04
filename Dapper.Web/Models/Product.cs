using Dapper.Web.DataAbstractions.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Models
{
	public class Product:IEntity
	{
		[Key]
		public int ProductId { get; set; }
		public string Name { get; set; }
		public int Quantity { get; set; }
		public double Price { get; set; }
	}
}
