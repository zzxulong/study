using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vuetest.Core.Models;
using vuetest.Core.ViewModel;

namespace vuetest.Core.Automapper
{
	public class CustomProfile :Profile
	{
		/// <summary>
		/// 配置构造函数，用来创建关系映射
		/// </summary>
		public CustomProfile()
		{
			CreateMap<Advertisement,AdvertisementViewModel>();
		}
	}
}
