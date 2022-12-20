using System;
using AutoMapper;
using Productive.Server.Models;
using Productive.Shared.ViewModels;

namespace Productive.Server
{
	public class AutoMapperProfile : Profile
	{
        public AutoMapperProfile()
        {
           
            CreateMap<Models.Client, ClientViewModel>();
            CreateMap<ClientViewModel, Models.Client>();
            CreateMap<Models.Client, ClientEditViewModel>();
            CreateMap<ClientEditViewModel, Models.Client>();
            CreateMap<Models.Client, ClientCreateViewModel>();
            CreateMap<ClientCreateViewModel, Models.Client>();


            CreateMap<ClientCellPhoneViewModel, ClientCellPhone>();
            CreateMap<ClientCellPhoneViewModelWithoutLoop, ClientCellPhone>();
            
            CreateMap<ClientCellPhone, ClientCellPhoneViewModelWithoutLoop > ();
            CreateMap<ClientCellPhone, ClientCellPhoneViewModel>();

            CreateMap<ClientCellPhone, ClientCellPhoneEditViewModel>();
            CreateMap<ClientCellPhoneEditViewModel, ClientCellPhone>();
         
            CreateMap<ClientCellPhone, ClientCellPhoneCreateViewModel>();
            CreateMap<ClientCellPhoneCreateViewModel, ClientCellPhone>();

        }
    }
}

