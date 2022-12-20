using System;
using static Productive.Shared.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Productive.Shared.ViewModels
{
	public class ClientViewModel :ClientBaseViewModel
	{
        public Guid ClientCellPhoneId { get; set; }
        public ClientCellPhoneViewModelWithoutLoop ClientCellPhone { get; set; }
        public Guid Id { get; set; }

    }

    public class ClientCreateViewModel : ClientBaseViewModel
    {
        [Required]
        [MinLength(10, ErrorMessage = "Cell Phone Number is too short")]
        [MaxLength(12, ErrorMessage = "Invalid Cell Phone Number")]
        [Phone]
        public string CellPhone { get; set; }
    }

    public class ClientEditViewModel : ClientBaseViewModel
    {
        public Guid Id { get; set; }
    }

    public class ClientBaseViewModel
    {

        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public EmailCellPhoneStatus EmailStatus { get; set; }

    }
}