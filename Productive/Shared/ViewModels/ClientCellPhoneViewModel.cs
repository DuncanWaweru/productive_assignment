using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Productive.Shared.Core.Enums;

namespace Productive.Shared.ViewModels
{
	public class ClientCellPhoneViewModel :ClientCellPhoneBaseViewModel
	{
        public virtual List<ClientViewModel> Clients { get; set; } = new List<ClientViewModel>();
        public Guid Id { get; set; }
    }
    public class ClientCellPhoneViewModelWithoutLoop : ClientCellPhoneBaseViewModel
    {
        // public virtual List<ClientViewModel> Clients { get; set; } = new List<ClientViewModel>();
        public Guid Id { get; set; }
    }
    public class ClientCellPhoneCreateViewModel : ClientCellPhoneBaseViewModel
    {

    }

    public class ClientCellPhoneEditViewModel : ClientCellPhoneBaseViewModel
    {
        public Guid Id { get; set; }
    }

    public class ClientCellPhoneBaseViewModel
    {

        [Required]
        [MinLength(10, ErrorMessage = "Cell Phone Number is too short")]
        [MaxLength(12, ErrorMessage = "Invalid Cell Phone Number")]
        public string CellPhone { get; set; }

        public EmailCellPhoneStatus CellPhoneStatus { get; set; }
    }
}
