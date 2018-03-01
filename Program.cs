using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using FenixSendCdlManually.NDInterfacesNEW;
using FenixSendCdlManually.NDMaterialMaster;
using FenixSendCdlManually.SetSent;
using FenixHelper;
using FenixHelper.XMLMessage;

namespace FenixSendCdlManually
{
	class Program
	{
		private static readonly string login = ConfigurationManager.AppSettings["Login"].Trim();
		private static readonly string password = ConfigurationManager.AppSettings["Password"].Trim();
		private static readonly string partnerCode = ConfigurationManager.AppSettings["PartnerCode"].Trim();			
		private static readonly string encoding = ConfigurationManager.AppSettings["Encoding"].Trim();
		private static readonly string logFile = String.Format("NDInterfacesCdlItems_{0}.log", DateTime.Now.ToString("yyyyMMdd_HHmm"));

		private static int SendXmlMessageToND
		{
			get { try { return int.Parse(ConfigurationManager.AppSettings["SendXmlMessageToND"].Trim()); } catch { return 0; } }
		}
		
		private static void Main(string[] args)
		{
			#region Test tvorby rootnode pro zapis do MS SQL

			//string sourceXML = Program.R1withSN();
			//string convertXML = XmlCreator.CreateXMLRootNode(sourceXML);

			//WriteLineToOutputs(String.Format("sourceXML  {1}{0}", sourceXML, Environment.NewLine));
			//WriteLineToOutputs(String.Empty);
			//WriteLineToOutputs(String.Format("convertXML {1}{0}", convertXML, Environment.NewLine));
			//WriteLineToOutputs(String.Empty);

			#endregion			

			string messageType = "Item";			
			string errorMessage = "";
			int itemCount = 1;
			
			try
			{
				CDLItems(messageType, ref itemCount);		//cdlItems  PROGRAMOVE

				//CDLKits(messageType, ref itemCount);		//cdlKits	PROGRAMOVE
				//CDLKitRucne(itemCount, messageType);      //KIT rucne
				//CDLItemRucne(itemCount, messageType);		//ITEM rucne
			}
			catch (Exception ex)
			{
				errorMessage = String.Format("{0}{1}{2}", FenixHelper.AppLog.GetMethodName(), Environment.NewLine, ex.Message);
				if (ex.InnerException != null)
					errorMessage += Environment.NewLine + ex.InnerException.Message;

				Console.WriteLine(errorMessage);
			}

			Console.WriteLine("*** DONE ***");
			Console.ReadLine();
		}

		private static void CDLItemRucne(int itemCount, string messageType)
		{
			string xmlSourceString = Program.GetItemXmlString();	//ITEM rucne
			xmlSourceString = xmlSourceString.Replace("\t", "  ");

			showInfo(itemCount, xmlSourceString);

			WriteLineToOutputs(String.Format("NDLInterfaces() result\n{0}", NDLInterfaces(xmlSourceString, messageType)));
		}

		/// <summary>
		/// Vytvori XML message pro cld KIT  (je to specialni zapis do Items !!! - v ND neexistuje ciselnik KITU)
		/// </summary>
		/// <param name="itemCount"></param>
		/// <param name="messageType"></param>
		private static void CDLKitRucne(int itemCount, string messageType)
		{
			string xmlSourceString = Program.GetXmlStringFroKIT();	//KIT rucne
			xmlSourceString = xmlSourceString.Replace("\t", "  ");

			showInfo(itemCount, xmlSourceString);

			WriteLineToOutputs(String.Format("NDLInterfaces() result\n{0}", NDLInterfaces(xmlSourceString, messageType)));
		}

		private static void CDLKits(string messageType, ref int itemCount)
		{
			string xmlSourceString = String.Empty;

			List<CDLItemsForKit> listItems = ObjectCreator.CreateCDLKits();

			foreach (var cdlIKittem in listItems)
			{
				xmlSourceString = XmlCreator.CreateXmlString(cdlIKittem, "http://www.w3.org/2001/XMLSchema", Encoding.UTF8, CreatorSettings.Declaration);

				xmlSourceString = xmlSourceString.Replace("\t", "  ");

				showInfo(itemCount, xmlSourceString);

				WriteLineToOutputs(String.Format("NDLInterfaces() result{1}{0}", NDLInterfaces(xmlSourceString, messageType), Environment.NewLine));

				CdlKitsSetSent cdlKitsSetSent = new CdlKitsSetSent(cdlIKittem.ItemIntegration.items[0].ItemID);
				WriteLineToOutputs(String.Format("{0}", cdlKitsSetSent.SetSent()));

				WriteLineToOutputs(String.Format("cdlKits Kit {0}  .. OK     {1}", cdlIKittem.ItemIntegration.items[0].ItemID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
				itemCount++;
			}
		}

		private static void CDLItems(string messageType, ref int itemCount)
		{
			string xmlSourceString = String.Empty;

			List<CDLItems> listItems = ObjectCreator.CreateCDLItems();

			foreach (var cdlItem in listItems)
			{
				xmlSourceString = XmlCreator.CreateXmlString(cdlItem, "http://www.w3.org/2001/XMLSchema", Encoding.UTF8, CreatorSettings.Declaration);

				xmlSourceString = xmlSourceString.Replace("\t", "  ");

				showInfo(itemCount, xmlSourceString);
												
				WriteLineToOutputs(String.Format("NDLInterfaces() result{1}{0}", NDLInterfaces(xmlSourceString, messageType), Environment.NewLine));

				CdlItemsSetSent cdlItemsSetSent = new CdlItemsSetSent(cdlItem.ItemIntegration.items[0].ItemID);
				WriteLineToOutputs(String.Format("{0}",cdlItemsSetSent.SetSent()));

				WriteLineToOutputs(String.Format("cdlItems Item {0}  .. OK     {1}", cdlItem.ItemIntegration.items[0].ItemID, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
				itemCount++;
			}
		}

		private static void showInfo(int itemCount, string xmlSourceString)
		{
			WriteLineToOutputs(String.Format("{0}. {1}", itemCount.ToString("# ##0"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
			WriteLineToOutputs(String.Empty);
			WriteLineToOutputs(String.Format("xmlSource {1}{0}", xmlSourceString, Environment.NewLine));
			WriteLineToOutputs(String.Empty);
		}

		private static void WriteLineToOutputs(string line)
		{			
			if (!String.IsNullOrEmpty(line))
			{
				Console.WriteLine(line);
				File.AppendAllText(logFile, line + Environment.NewLine);

			}
			else
			{
				Console.WriteLine();
				File.AppendAllText(logFile, Environment.NewLine);
			}
		}

		private static string NDLInterfaces(string sourceXML, string messageType)
		{
			if (Program.SendXmlMessageToND == 0)
			{
				return String.Empty;
			}

			byte[] ndAnswer;
			try
			{
				byte[] image = sourceXML.ToArray(Encoding.UTF8);

				PortTypeNDL_InterfacesClient client = new PortTypeNDL_InterfacesClient();

				client.InnerChannel.OperationTimeout = new TimeSpan(0, 20, 0);
				
				ndAnswer = client.UPWSI0(login, password, partnerCode, messageType, encoding, image);
			}
			catch (Exception ex)
			{
				return ex.Message;				
			}

			return ndAnswer.ToString(Encoding.UTF8, Encoding.Unicode);
		}

		private static string NDMaterialMaster()
		{
			UPIE01AdeliaPrcnam00000CREATEITEM00Result res;
			string messageType = "ITEM";

			try
			{
				PortTypematerialMasterXClient client = new PortTypematerialMasterXClient();

				client.InnerChannel.OperationTimeout = new TimeSpan(0, 20, 0);

				res = client.createItemX(login, password, partnerCode, messageType, encoding, "370036", "KAON KCF-SA700PCO PVR Ready", "", "", "0", "0", "1", "1");				
			}
			catch (Exception ex)
			{
				return ex.Message;				
			}

			return String.Format("{0} {1}", res.returnCode2, res.returnDescription);
		}
		
		private static void ProcessNDResult(byte[] data, string encoding)
		{

			try
			{
				Encoding inputEncoding = null;
				Encoding outputEncoding = Encoding.Unicode;

				switch (encoding.ToLower())
				{
					case "utf8":
					case "utf-8":
						inputEncoding = Encoding.UTF8;
						break;
					case "utf32":
					case "utf-32":
						inputEncoding = Encoding.UTF32;
						break;
					case "unicode":
					case "utf16":
					case "utf-16":
						inputEncoding = outputEncoding;
						break;
					default:
						break;
				}

				byte[] bytesFromUtf8 = Encoding.Convert(inputEncoding, outputEncoding, data);
				string resultAAA = outputEncoding.GetString(bytesFromUtf8);

				using (StreamWriter outfile = new StreamWriter("results.txt", true, Encoding.Unicode))
				{
					outfile.WriteLine(String.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), resultAAA));
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		// OK
		private static string GetItemXmlString()
		{
			return
@"<?xml version=""1.0"" encoding=""utf-8""?>
<NewDataSet xmlns=""http://www.w3.org/2001/XMLSchema"">
  <ItemIntegration>
    <ID>8008698</ID>
    <MessageID>9008698</MessageID>
    <MessageType>Item</MessageType>
    <MessageDescription>ItemIntegration</MessageDescription>
    <MessageStatus>1</MessageStatus>
    <items>
      <item>
        <ItemID>999000666</ItemID>
        <ItemDescription>Test item 999000666</ItemDescription>
        <SupplierID>UPC</SupplierID>
        <SupplierName>UPC</SupplierName>
        <ComponentManagement>0</ComponentManagement>
        <QtyBox>1</QtyBox>
        <QtyPallet>1</QtyPallet>
        <ItemTypeID>5</ItemTypeID>
        <ItemTypeCode>SPP</ItemTypeCode>
        <ItemTypeDesc1>Náhradní díly</ItemTypeDesc1>
        <ItemTypeDesc2>Spare Parts</ItemTypeDesc2>
        <HeliosGroupGoods>999</HeliosGroupGoods>
        <HeliosCode>000666</HeliosCode>
      </item>
    </items>
  </ItemIntegration>
</NewDataSet>
";
		}

		// OK
		private static string GetXmlStringFroKIT()
		{
			return
@"<?xml version=""1.0"" encoding=""utf-8""?>
<NewDataSet xmlns=""http://www.w3.org/2001/XMLSchema"">
	<ItemIntegration>
		<ID>7</ID>
		<MessageId>6002</MessageId>
		<MessageType>Item</MessageType>
		<MessageDescription>ItemIntegration</MessageDescription>
		<MessageStatus>1</MessageStatus>
		<items>
			<item>
				<ItemID>9</ItemID>
				<ItemDescription>CA1</ItemDescription>
				<SupplierID>UPC</SupplierID>
				<SupplierName>UPC</SupplierName>
				<ComponentManagement>1</ComponentManagement>
				<Components>
				  <Component>
					<ComponentItemID>3054</ComponentItemID>
					<ComponentQty>1</ComponentQty>
				  </Component>
				  <Component>
					<ComponentItemID>3236</ComponentItemID>
					<ComponentQty>1</ComponentQty>
				  </Component>
				  <Component>
					<ComponentItemID>4572</ComponentItemID>
					<ComponentQty>1</ComponentQty>
				  </Component>
				</Components>
				<QtyBox>1</QtyBox>
				<QtyPallet>1</QtyPallet>
			</item>
		</items>
	</ItemIntegration>
</NewDataSet>";
		}

		private static string R1withSN()
		{
			return
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<NewDataSet xmlns=""http://www.w3.org/2001/XMLSchema"">
  <CommunicationMessagesReceptionConfirmation>
    <ID>4956</ID>
    <MessageID>01000000032</MessageID>
    <MessageTypeID>2</MessageTypeID>
    <MessageTypeDescription>ReceptionConfirmation</MessageTypeDescription>
    <MessageDateOfReceipt>2014-08-08</MessageDateOfReceipt>
    <ReceptionOrderID>1</ReceptionOrderID>
    <HeliosOrderID>135201</HeliosOrderID>
    <ItemSupplierID>10</ItemSupplierID>
    <ItemSupplierDescription>LICA servis s.r.o.</ItemSupplierDescription>
    <items>
      <item>
        <HeliosOrderRecordID>555422</HeliosOrderRecordID>
        <ItemID>3240</ItemID>
        <ItemDescription>kompletace  Kaon KCF-SA700PCO HD-HDD USB READY</ItemDescription>
        <ItemQuantity>5454</ItemQuantity>
        <ItemUnitOfMeasureID>1</ItemUnitOfMeasureID>
        <ItemUnitOfMeasure>KS</ItemUnitOfMeasure>
        <ItemQualityID>1</ItemQualityID>
        <ItemQuality>New</ItemQuality>
        <ItemSNs>
          <ItemSN SN=""A71865865""> </ItemSN>
          <ItemSN SN=""A71865966""> </ItemSN>
          <ItemSN SN=""A71866067""> </ItemSN>
          <ItemSN SN=""A71871422""> </ItemSN>          
         </ItemSNs>
      </item>
    </items>
  </CommunicationMessagesReceptionConfirmation>
</NewDataSet>";
		}
	}
}
