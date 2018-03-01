using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FenixWindowsServiceConsumeNDServiceTest
{
	/// <summary>
	/// Třída představující XML message pro ND položky číselníku položek
	/// (co není kit)
	/// </summary>
	[XmlRoot("NewDataSet")]
	public class CodeListItem
	{
		[XmlElement(ElementName = "ItemIntegration")]
		public ItemIntegrationCodeListItem ItemIntegration { get; set; }
	}

	public class ItemIntegrationCodeListItem
	{
		public string ID { get; set; }
		public string MessageID { get; set; }
		public string MessageType { get; set; }
		public string MessageDescription { get; set; }
		public string MessageStatus { get; set; }

		[XmlArrayItem("item", typeof(ItemCodeListItem))]
		[XmlArray("items")]
		public List<ItemCodeListItem> items { get; set; }
	}

	public class ItemCodeListItem
	{
		public string ItemID { get; set; }
		public string ItemDescription { get; set; }
		public string SupplierID { get; set; }
		public string SupplierName { get; set; }
		public string ComponentManagement { get; set; }
		public int QtyBox { get; set; }
		public int QtyPallet { get; set; }
	}
}
