using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static Productive.Server.Core.Enums;

namespace Productive.Server.Models
{
	public class Client: AuditableEntity
    {
		[Required]
		public string  Name { get; set; }
        [Required]
		[EmailAddress]
        public string Email { get; set; } //use fluent api to set unique contraint.
		public EmailCellPhoneStatus EmailStatus { get; set; }

		//CREATE A relationship with the cell phone table, so that we shall maintain a single record for each phone number.

		public Guid ClientCellPhoneId { get; set; }
		public ClientCellPhone ClientCellPhone { get; set; }
	}
}

