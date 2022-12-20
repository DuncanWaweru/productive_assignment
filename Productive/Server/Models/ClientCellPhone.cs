using System;
using System.ComponentModel.DataAnnotations;
using static Productive.Server.Core.Enums;

namespace Productive.Server.Models
{
	public class ClientCellPhone : AuditableEntity
    {
		[Required]
		[MinLength(10,ErrorMessage = "Cell Phone Number is too short")]
		[MaxLength(12,ErrorMessage ="Invalid Cell Phone Number")]
		public string CellPhone { get; set; }
		public EmailCellPhoneStatus CellPhoneStatus { get; set; }
        public  List<Client> Clients { get; set; }
    }
}

