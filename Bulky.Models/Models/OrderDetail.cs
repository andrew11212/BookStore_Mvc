using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.Models
{
	public class OrderDetail
	{

		public int Id { get; set; }

		public int OrderId { get; set; }
		[ForeignKey(nameof(OrderId))]
		public OrderHeader orderHeader { get; set; } = default!;

		public int ProductId { get; set; }
		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }=default!;

		public int Count { get; set; }
		public double Price {  get; set; }
	}
}
