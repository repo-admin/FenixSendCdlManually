﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FenixSendCdlManually.NDMaterialMaster {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UPIE01AdeliaPrcnam00000CREATEITEM00Result", Namespace="http://localhost:8082/upcsw/materialMasterX")]
    [System.SerializableAttribute()]
    public partial class UPIE01AdeliaPrcnam00000CREATEITEM00Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string returnCode2Field;
        
        private string returnDescriptionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string returnCode2 {
            get {
                return this.returnCode2Field;
            }
            set {
                if ((object.ReferenceEquals(this.returnCode2Field, value) != true)) {
                    this.returnCode2Field = value;
                    this.RaisePropertyChanged("returnCode2");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string returnDescription {
            get {
                return this.returnDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.returnDescriptionField, value) != true)) {
                    this.returnDescriptionField = value;
                    this.RaisePropertyChanged("returnDescription");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://localhost:8082/upcsw/materialMasterX", ConfigurationName="NDMaterialMaster.PortTypematerialMasterX")]
    public interface PortTypematerialMasterX {
        
        // CODEGEN: Generating message contract since element name login from namespace http://localhost:8082/upcsw/materialMasterX is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        FenixSendCdlManually.NDMaterialMaster.createItemXResponse createItemX(FenixSendCdlManually.NDMaterialMaster.createItemXRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<FenixSendCdlManually.NDMaterialMaster.createItemXResponse> createItemXAsync(FenixSendCdlManually.NDMaterialMaster.createItemXRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class createItemXRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="createItemX", Namespace="http://localhost:8082/upcsw/materialMasterX", Order=0)]
        public FenixSendCdlManually.NDMaterialMaster.createItemXRequestBody Body;
        
        public createItemXRequest() {
        }
        
        public createItemXRequest(FenixSendCdlManually.NDMaterialMaster.createItemXRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://localhost:8082/upcsw/materialMasterX")]
    public partial class createItemXRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string login;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string partnerCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string messageType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string characterEncoding;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string item;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string description;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string supplier;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string supplierName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string serialNumberManagement;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string component;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string qtyBox;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string qtyPallet;
        
        public createItemXRequestBody() {
        }
        
        public createItemXRequestBody(string login, string password, string partnerCode, string messageType, string characterEncoding, string item, string description, string supplier, string supplierName, string serialNumberManagement, string component, string qtyBox, string qtyPallet) {
            this.login = login;
            this.password = password;
            this.partnerCode = partnerCode;
            this.messageType = messageType;
            this.characterEncoding = characterEncoding;
            this.item = item;
            this.description = description;
            this.supplier = supplier;
            this.supplierName = supplierName;
            this.serialNumberManagement = serialNumberManagement;
            this.component = component;
            this.qtyBox = qtyBox;
            this.qtyPallet = qtyPallet;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class createItemXResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="createItemXResponse", Namespace="http://localhost:8082/upcsw/materialMasterX", Order=0)]
        public FenixSendCdlManually.NDMaterialMaster.createItemXResponseBody Body;
        
        public createItemXResponse() {
        }
        
        public createItemXResponse(FenixSendCdlManually.NDMaterialMaster.createItemXResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://localhost:8082/upcsw/materialMasterX")]
    public partial class createItemXResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public FenixSendCdlManually.NDMaterialMaster.UPIE01AdeliaPrcnam00000CREATEITEM00Result createItemXReturn;
        
        public createItemXResponseBody() {
        }
        
        public createItemXResponseBody(FenixSendCdlManually.NDMaterialMaster.UPIE01AdeliaPrcnam00000CREATEITEM00Result createItemXReturn) {
            this.createItemXReturn = createItemXReturn;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PortTypematerialMasterXChannel : FenixSendCdlManually.NDMaterialMaster.PortTypematerialMasterX, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PortTypematerialMasterXClient : System.ServiceModel.ClientBase<FenixSendCdlManually.NDMaterialMaster.PortTypematerialMasterX>, FenixSendCdlManually.NDMaterialMaster.PortTypematerialMasterX {
        
        public PortTypematerialMasterXClient() {
        }
        
        public PortTypematerialMasterXClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PortTypematerialMasterXClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PortTypematerialMasterXClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PortTypematerialMasterXClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        FenixSendCdlManually.NDMaterialMaster.createItemXResponse FenixSendCdlManually.NDMaterialMaster.PortTypematerialMasterX.createItemX(FenixSendCdlManually.NDMaterialMaster.createItemXRequest request) {
            return base.Channel.createItemX(request);
        }
        
        public FenixSendCdlManually.NDMaterialMaster.UPIE01AdeliaPrcnam00000CREATEITEM00Result createItemX(string login, string password, string partnerCode, string messageType, string characterEncoding, string item, string description, string supplier, string supplierName, string serialNumberManagement, string component, string qtyBox, string qtyPallet) {
            FenixSendCdlManually.NDMaterialMaster.createItemXRequest inValue = new FenixSendCdlManually.NDMaterialMaster.createItemXRequest();
            inValue.Body = new FenixSendCdlManually.NDMaterialMaster.createItemXRequestBody();
            inValue.Body.login = login;
            inValue.Body.password = password;
            inValue.Body.partnerCode = partnerCode;
            inValue.Body.messageType = messageType;
            inValue.Body.characterEncoding = characterEncoding;
            inValue.Body.item = item;
            inValue.Body.description = description;
            inValue.Body.supplier = supplier;
            inValue.Body.supplierName = supplierName;
            inValue.Body.serialNumberManagement = serialNumberManagement;
            inValue.Body.component = component;
            inValue.Body.qtyBox = qtyBox;
            inValue.Body.qtyPallet = qtyPallet;
            FenixSendCdlManually.NDMaterialMaster.createItemXResponse retVal = ((FenixSendCdlManually.NDMaterialMaster.PortTypematerialMasterX)(this)).createItemX(inValue);
            return retVal.Body.createItemXReturn;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<FenixSendCdlManually.NDMaterialMaster.createItemXResponse> FenixSendCdlManually.NDMaterialMaster.PortTypematerialMasterX.createItemXAsync(FenixSendCdlManually.NDMaterialMaster.createItemXRequest request) {
            return base.Channel.createItemXAsync(request);
        }
        
        public System.Threading.Tasks.Task<FenixSendCdlManually.NDMaterialMaster.createItemXResponse> createItemXAsync(string login, string password, string partnerCode, string messageType, string characterEncoding, string item, string description, string supplier, string supplierName, string serialNumberManagement, string component, string qtyBox, string qtyPallet) {
            FenixSendCdlManually.NDMaterialMaster.createItemXRequest inValue = new FenixSendCdlManually.NDMaterialMaster.createItemXRequest();
            inValue.Body = new FenixSendCdlManually.NDMaterialMaster.createItemXRequestBody();
            inValue.Body.login = login;
            inValue.Body.password = password;
            inValue.Body.partnerCode = partnerCode;
            inValue.Body.messageType = messageType;
            inValue.Body.characterEncoding = characterEncoding;
            inValue.Body.item = item;
            inValue.Body.description = description;
            inValue.Body.supplier = supplier;
            inValue.Body.supplierName = supplierName;
            inValue.Body.serialNumberManagement = serialNumberManagement;
            inValue.Body.component = component;
            inValue.Body.qtyBox = qtyBox;
            inValue.Body.qtyPallet = qtyPallet;
            return ((FenixSendCdlManually.NDMaterialMaster.PortTypematerialMasterX)(this)).createItemXAsync(inValue);
        }
    }
}
