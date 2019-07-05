using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vuetest.Core.IRespository;
using vuetest.Core.IService;
using vuetest.Core.Models;
using vuetest.Core.Respository;
using vuetest.Core.ViewModel;

namespace vuetest.Core.Service
{
	public class AdvertisementServices :BaseServices<Advertisement>, IAdvertisementServices
	{
		public IAdvertisementRepository _dal { get; set; }
		public IMapper mapper { get; set; }
		public async Task<AdvertisementViewModel> Get(object id)
		{
			var result = await this._dal.QueryById(id);
			var a = mapper.Map<AdvertisementViewModel>(result);
			return a;
		}
		public int Sum(int i,int j)
		{
			return this._dal.Sum(i,j);

		}

		public Task<int> Add(Advertisement model)
		{
			return base.Add(model);
		}

		public Task<bool> Delete(Advertisement model)
		{
			return base.Delete(model);
		}

		public Task<List<Advertisement>> Query(Expression<Func<Advertisement,bool>> whereExpression)
		{
			return base.Query(whereExpression);

		}

		public Task<bool> Update(Advertisement model)
		{
			return base.Update(model);
		}

	}
}
