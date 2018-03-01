using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FenixWindowsServiceConsumeNDServiceTest
{
	/// <summary>
	/// TEST XML message pro : Reception R0	
	/// </summary>
	[XmlRoot("NewDataSet")]
	public class R0Test
	{
		#region Properties
		/// <summary>
		/// 
		/// </summary>
		[XmlElement(ElementName = "CommunicationMessagesSentReception")]
		public R0CommunicationMessagesSentReception CommunicationMessagesSentReception { get; set; }

		#endregion

		#region Contructors

		/// <summary>
		/// 
		/// </summary>
		public R0Test()
		{
			this.CommunicationMessagesSentReception = new R0CommunicationMessagesSentReception();
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string ToXMLString()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<string> Validate()
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class R0CommunicationMessagesSentReception
	{
		/// <summary>
		/// 
		/// </summary>
		public int ID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int MessageId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int MessageType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string MessageDescription { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[XmlElement(DataType = "date")]
		public DateTime MessageDateOfShipment { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int MessageStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? HeliosOrderId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int ItemSupplierID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ItemSupplierDescription { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[XmlElement(DataType = "date")]
		public DateTime ItemDateOfDelivery { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[XmlArrayItem("item", typeof(R0Item))]
		[XmlArray("items")]
		public List<R0Item> items { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public R0CommunicationMessagesSentReception()
		{
			this.items = new List<R0Item>();
		}

	}
		
	/// <summary>
	/// 
	/// </summary>
	public class R0Item
	{
		/// <summary>
		/// 
		/// </summary>
		public int? HeliosOrderRecordID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ItemID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ItemDescription { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public decimal ItemQuantity { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ItemUnitOfMeasure { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ItemQuality { get; set; }

	}
}
