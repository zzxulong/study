using AutoMapper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vuetest.Core.Common;
using vuetest.Core.IRespository;
using vuetest.Core.Models;
using vuetest.Core.Respository.sugar;
using vuetest.Core.ViewModel;

namespace vuetest.Core.Respository
{
	public class AdvertisementRepository :BaseRepository<Advertisement>, IAdvertisementRepository
	{
		
		public int Sum(int i,int j)
		{
			return i + j;
		}

	}
}
