using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using UPC.Extensions.Convert;
using UPC.Extensions.Enum;
using FenixHelper;

namespace FenixSendCdlManually.SetSent
{
	public class CdlKitsSetSent : ISetSent
	{
		public int KitID { get; set; }

		private CdlKitsSetSent()
		{ }

		public CdlKitsSetSent(int itemID)
		{
			this.KitID = itemID;
		}

		public string SetSent()
		{
			string result = String.Empty;

			using (var db = new FenixCdlsEntities())
			{
				try
				{
					db.Configuration.LazyLoadingEnabled = false;
					db.Configuration.ProxyCreationEnabled = false;
					ObjectParameter retVal = new ObjectParameter("ReturnValue", typeof(int));
					ObjectParameter retMsg = new ObjectParameter("ReturnMessage", typeof(string));

					db.prCdlKitsSetSent(this.KitID, retVal, retMsg);

					result = String.Format("{0} returnValue [{1}] returnMessage [{2}]", AppLog.GetMethodName(), retVal.Value.ToInt32(BC.NOT_OK), retMsg.Value.ToString(String.Empty));
				}
				catch (Exception ex)
				{
					result = String.Format("{0} returnValue{1} returnMessage{2}", AppLog.GetMethodName(), -1, ex.Message);
				}
			}

			return result;
		}
	}
}
