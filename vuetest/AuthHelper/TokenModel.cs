using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vuetest.AuthHelper
{
	public class TokenModel
	{
		public TokenModel()

		{

			this.Uid = 0;

		}

		///

		/// 用户Id

		///

		public long Uid { get; set; }

		///

		/// 用户名

		///

		public string Uname { get; set; }

		///

		/// 手机

		///

		public string Phone { get; set; }

		///

		/// 头像

		///

		public string Icon { get; set; }

		///

		/// 昵称

		///

		public string UNickname { get; set; }

		///

		/// 签名

		///

		public string Sub { get; set; }

	}
}