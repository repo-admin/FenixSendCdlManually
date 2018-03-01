using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using FenixHelper;
using FenixHelper.Validation;
using FenixHelper.XMLMessage;

namespace FenixWindowsServiceConsumeNDServiceTest
{
	public class XMLMessageCreator
	{
		public static string CreateMessage()
		{
			//CodeListItem codeListItem = CreateCodeListItem();	//OK
			//R0Test r0Test = CreateR0Test();					//OK

			R0Reception r0Reception = CreateR0Reception();

			string res = XmlCreator.CreateXmlString(r0Reception, "http://www.w3.org/2001/XMLSchema", Encoding.UTF8, CreatorSettings.Declaration);

			return res;
		}

		private static R0Reception CreateR0Reception()
		{
			R0Reception r0Reception = new R0Reception();

			r0Reception.Header = new R0Header();

			r0Reception.Header.ID = 14;
			r0Reception.Header.MessageID = 106;
			r0Reception.Header.MessageTypeID = 1;
			r0Reception.Header.MessageTypeDescription = "ReceptionOrder";
			r0Reception.Header.MessageDateOfShipment = DateTime.Now;
			r0Reception.Header.HeliosOrderID = 2;
			r0Reception.Header.ItemSupplierID = 13;
			r0Reception.Header.ItemSupplierDescription = "Corning Optical Communications ApS";
			r0Reception.Header.ItemDateOfDelivery = new DateTime(2014, 10, 1);

			//1 item
			r0Reception.Header.items = new List<R0Items>();

			R0Items r0Item = new R0Items();
			r0Item.ItemID = 1;
			r0Item.ItemDescription = "GSF 101 /Blankom/";
			r0Item.ItemQuantity = 10;
			r0Item.ItemUnitOfMeasureID = 1;
			r0Item.ItemUnitOfMeasure = "KS";
			r0Item.ItemQualityID = 1;
			r0Item.ItemQuality = "New";

			r0Reception.Header.items.Add(r0Item);

			return r0Reception;
		}

		private static R0Test CreateR0Test()
		{
			R0Test r0Test = new R0Test();
			
			r0Test.CommunicationMessagesSentReception = new R0CommunicationMessagesSentReception();

			r0Test.CommunicationMessagesSentReception.ID = 14;
			r0Test.CommunicationMessagesSentReception.MessageId = 106;
			r0Test.CommunicationMessagesSentReception.MessageType = 1;
			r0Test.CommunicationMessagesSentReception.MessageDescription = "ReceptionOrder";
			r0Test.CommunicationMessagesSentReception.MessageDateOfShipment = new DateTime(2014, 9, 1);
			r0Test.CommunicationMessagesSentReception.MessageStatus = 1;
			r0Test.CommunicationMessagesSentReception.HeliosOrderId = 2;
			r0Test.CommunicationMessagesSentReception.ItemSupplierID = 12;
			r0Test.CommunicationMessagesSentReception.ItemSupplierDescription = "TES - SLOVAKIA, s.r.o.";
			r0Test.CommunicationMessagesSentReception.ItemDateOfDelivery = new DateTime(2014, 10, 1);

			r0Test.CommunicationMessagesSentReception.items = new List<R0Item>();

			R0Item r0Item = new R0Item();
			r0Item.ItemID = "1";
			r0Item.ItemDescription = @"GSF 101 /Blankom/";
			r0Item.ItemQuantity = 10;
			r0Item.ItemUnitOfMeasure = "KS";
			r0Item.ItemQuality = "New";

			r0Test.CommunicationMessagesSentReception.items.Add(r0Item);

			return r0Test;
		}

		public static CodeListItem CreateCodeListItem()
		{
			CodeListItem codeListItem = new CodeListItem();

			codeListItem.ItemIntegration = new ItemIntegrationCodeListItem();
			codeListItem.ItemIntegration.ID = "9";
			codeListItem.ItemIntegration.MessageID = "101";
			codeListItem.ItemIntegration.MessageType = "Item";
			codeListItem.ItemIntegration.MessageDescription = "ItemIntegration";
			codeListItem.ItemIntegration.MessageStatus = "1";

			codeListItem.ItemIntegration.items = new List<ItemCodeListItem>();
			ItemCodeListItem icl = new ItemCodeListItem();
			icl.ItemID = "970045";
			icl.ItemDescription = "Test Item 970045 - Příliš žluťoučký kůň ůpěl ďábelské ódy";
			icl.SupplierID = "UPC";
			icl.SupplierName = "UPC";
			icl.ComponentManagement = "0";
			icl.QtyBox = 1;
			icl.QtyPallet = 1;
			codeListItem.ItemIntegration.items.Add(icl);

			return codeListItem;
		}

		public static string CreateXML(Object YourClassObject)
		{
			//string ulrW3orgSchema = "http://www.w3.org/2001/XMLSchema";
			string ulrW3orgSchema = String.Empty;
			string xmlString;

			XmlDocument xmlDoc = new XmlDocument();
			XmlSerializer serializer = new XmlSerializer(YourClassObject.GetType(), ulrW3orgSchema);

			using (MemoryStream xmlStream = new MemoryStream())
			{
				XmlTextWriter stream = new XmlTextWriter(xmlStream, System.Text.Encoding.UTF8);
				using (Stream baseStream = stream.BaseStream)
				{
					stream.Formatting = Formatting.Indented;
					stream.IndentChar = '\t';
					stream.Indentation = 1;

					XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
					ns.Add(String.Empty, ulrW3orgSchema);
					serializer.Serialize(stream, YourClassObject, ns);
										
					xmlStream.Position = 0;
					xmlDoc.Load(xmlStream);

					xmlString = xmlDoc.InnerXml;
				}
			}

			XDocument xd = XDocument.Parse(xmlString);
			xmlString = String.Format("{0}{1}{2}", xd.Declaration.ToString(), Environment.NewLine, xd.Root.ToString());
			
			return xmlString;
		}
	}
}
