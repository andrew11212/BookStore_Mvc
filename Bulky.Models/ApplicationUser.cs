using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
	public class ApplicationUser:IdentityUser
	{
		[Required]
        public string Name  { get; set; } =string.Empty;
		public string? StreetAddress { get; set; } = string.Empty;
		public string? City { get; set; } = string.Empty;
		public string? State  { get; set; } = string.Empty;
		public string? PostalCode { get; set; } = string.Empty;


	}
}
