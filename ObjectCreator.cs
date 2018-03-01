using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FenixHelper.XMLMessage;
using UPC.Extensions.Convert;

#region XML

/*

<?xml version="1.0" encoding="utf-8"?>
<NewDataSet xmlns="http://www.w3.org/2001/XMLSchema">
  <ItemIntegration>
  <ID>201408192</ID>
  <MessageId>1201408192</MessageId>
  <MessageType>Item</MessageType>
  <MessageDescription>ItemIntegration</MessageDescription>
  <MessageStatus>1</MessageStatus>
  <items>
    <item>
      <ItemID>201408193</ItemID>
      <ItemDescription>GSF 101 /Blankom/</ItemDescription>
      <QtyBox>1</QtyBox>
      <QtyPallet>10</QtyPallet>
      <ItemTypeID>1</ItemTypeID>
      <ItemTypeCode>NW</ItemTypeCode>
      <ItemTypeDesc1>Materiál</ItemTypeDesc1>
      <ItemTypeDesc2>Network</ItemTypeDesc2>
    </item>
    <item>
      <ItemID>201408194</ItemID>
      <ItemDescription>TC7200 (Technicolor)</ItemDescription>
      <QtyBox>1</QtyBox>
      <QtyPallet>10</QtyPallet>
      <ItemTypeID>2</ItemTypeID>
      <ItemTypeCode>CPE</ItemTypeCode>
      <ItemTypeDesc1>Zařízení</ItemTypeDesc1>
      <ItemTypeDesc2>CPE</ItemTypeDesc2>
    </item>
    <item>  
      <ItemID>201408195</ItemID>
      <ItemDescription>CT1900-Telekabel</ItemDescription>
      <QtyBox>1</QtyBox>
      <QtyPallet>10</QtyPallet>
      <ItemTypeID>3</ItemTypeID>
      <ItemTypeCode>SPP</ItemTypeCode>
      <ItemTypeDesc1>Náhradní díly</ItemTypeDesc1>
      <ItemTypeDesc2>Spare Parts</ItemTypeDesc2>
    </item>
    <item>  
      <ItemID>201408196</ItemID>
      <ItemDescription>Příručka internet a telefon-mini CD</ItemDescription>
      <QtyBox>1</QtyBox>
      <QtyPallet>10</QtyPallet>
      <ItemTypeID>4</ItemTypeID>
      <ItemTypeCode>MKT</ItemTypeCode>
      <ItemTypeDesc1>Marketing</ItemTypeDesc1>
      <ItemTypeDesc2>Marketing</ItemTypeDesc2>
    </item>
  </items>
  </ItemIntegration>
</NewDataSet> 
 */

#endregion

namespace FenixSendCdlManually
{
	public class ObjectCreator
	{
		private const int MESSAGE_ID_ADDING = 1000000;

		public static List<CDLItems> CreateCDLItems()
		{
			List<CDLItems> result = new List<CDLItems>();

			try
			{
				using (var db = new FenixCdlsEntities())
				{
					try
					{
						db.Configuration.LazyLoadingEnabled = false;
						db.Configuration.ProxyCreationEnabled = false;
												
						var cldItemsList = from b in db.cdlItems
									orderby b.ID ascending
									where b.IsSent == false && b.IsActive == true
									select b;

						foreach (var item in cldItemsList)
						{
							CDLItems cdlItems = new CDLItems();

							cdlItems.ItemIntegration.ID = createCdlID();
							cdlItems.ItemIntegration.MessageID = cdlItems.ItemIntegration.ID + MESSAGE_ID_ADDING;
							cdlItems.ItemIntegration.MessageType = "Item";
							cdlItems.ItemIntegration.MessageDescription = "ItemIntegration";
							cdlItems.ItemIntegration.MessageStatus = 1;

							cdlItems.ItemIntegration.items = new List<CdlItemsItem>();

							CdlItemsItem cdlItemsItem = new CdlItemsItem();

							cdlItemsItem.ItemID = item.ID;
							cdlItemsItem.ItemDescription = item.DescriptionCz;
							cdlItemsItem.SupplierID = "UPC";
							cdlItemsItem.SupplierName = "UPC";

							cdlItemsItem.ComponentManagement = 0;
							cdlItemsItem.QtyBox = 1;
							cdlItemsItem.QtyPallet = 1;
							      
							cdlItemsItem.ItemTypeID = item.ItemTypesId;
							cdlItemsItem.ItemTypeCode = item.ItemType.Trim();
							cdlItemsItem.ItemTypeDesc1 = item.ItemTypeDesc1.Trim();
							cdlItemsItem.ItemTypeDesc2 = item.ItemTypeDesc2.Trim();

							cdlItemsItem.HeliosGroupGoods = item.GroupGoods;
							cdlItemsItem.HeliosCode = item.Code;
							
							cdlItems.ItemIntegration.items.Add(cdlItemsItem);

							result.Add(cdlItems);
						}
					}
					catch (Exception ex)
					{						
						string resultReturnMessage = String.Format("{0}{1}{2}", FenixHelper.AppLog.GetMethodName(), Environment.NewLine, ex.Message);
						if (ex.InnerException != null)
							resultReturnMessage += Environment.NewLine + ex.InnerException.Message;						
					}
				}

			}
			catch (Exception ex)
			{				
				throw ex;
			}

			return result;
		}

		public static List<CDLItemsForKit> CreateCDLKits()
		{
			List<CDLItemsForKit> result = new List<CDLItemsForKit>();

			try
			{
				using (var db = new FenixCdlsEntities())
				{
					try
					{
						db.Configuration.LazyLoadingEnabled = false;
						db.Configuration.ProxyCreationEnabled = false;

						var kits = from b in db.cdlKits
								   orderby b.ID ascending
								   where (b.IsSent == false || b.IsSent == null) && b.IsActive == true 
								   select b;

						foreach (var item in kits)
						{
							CDLItemsForKit cdlItems1 = new CDLItemsForKit();

							cdlItems1.ItemIntegration.ID = createCdlID();
							cdlItems1.ItemIntegration.MessageID = cdlItems1.ItemIntegration.ID + MESSAGE_ID_ADDING;
							cdlItems1.ItemIntegration.MessageType = "Item";
							cdlItems1.ItemIntegration.MessageDescription = "ItemIntegration";
							cdlItems1.ItemIntegration.MessageStatus = 1;

							cdlItems1.ItemIntegration.items = new List<CdlItemsForKitItem>();

							CdlItemsForKitItem cdlIKitsItem = new CdlItemsForKitItem();

							cdlIKitsItem.ItemID = item.ID;
							cdlIKitsItem.ItemDescription = item.DescriptionCz;	
							cdlIKitsItem.SupplierID = "UPC";
							cdlIKitsItem.SupplierName = "UPC";

							cdlIKitsItem.ComponentManagement = 1;

							//Components
							cdlIKitsItem.components = new List<R1ComponentItem>();

							R1ComponentItem r1ComponentItem = new R1ComponentItem();

							var kitItems = from b in db.cdlKitsItems
									   orderby b.ID ascending
									   where b.cdlKitsId == item.ID
									   select b;

							foreach (var item2 in kitItems)
							{
								cdlIKitsItem.components.Add(new R1ComponentItem() { ComponentItemID = item2.ItemOrKitId, ComponentQty = item2.ItemOrKitQuantity });
							}
														
							cdlIKitsItem.QtyBox = 1;
							cdlIKitsItem.QtyPallet = 1;

							cdlItems1.ItemIntegration.items.Add(cdlIKitsItem);

							result.Add(cdlItems1);
						}
					}
					catch (Exception ex)
					{
						string resultReturnMessage = String.Format("{0}{1}{2}", FenixHelper.AppLog.GetMethodName(), Environment.NewLine, ex.Message);
						if (ex.InnerException != null)
							resultReturnMessage += Environment.NewLine + ex.InnerException.Message;
					}
				}

			}
			catch (Exception ex)
			{
				throw ex;
			}

			return result;
		}

		/// <summary>
		/// vytvoří ID (hodnota z tabulky Counter pro CDL)
		/// </summary>
		/// <returns></returns>
		private static int createCdlID()
		{
			int result = 0;

			using (var db = new FenixCdlsEntities())
			{
				try
				{
					db.Configuration.LazyLoadingEnabled = false;
					db.Configuration.ProxyCreationEnabled = false;
					ObjectParameter retVal = new ObjectParameter("NewValue", typeof(int));
					
					db.prGetIntValueFromCounter("CDL", retVal);

					result = retVal.Value.ToInt32(BC.NOT_OK);
				}
				catch
				{
					throw;
				}
			}

			return result; 
		}	
	}
}
