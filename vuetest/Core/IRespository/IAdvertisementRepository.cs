using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vuetest.Core.Models;
using vuetest.Core.ViewModel;

namespace vuetest.Core.IRespository
{
	public interface IAdvertisementRepository :IBaseRepository<Advertisement>
	{
		int Sum(int i,int j);
	}
}
