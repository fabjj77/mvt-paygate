﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.WsNapTheAvgSandbox {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.napthe.avg.com/", ConfigurationName="WsNapTheAvgSandbox.Nap")]
    public interface Nap {
        
        // CODEGEN: Parameter 'return' requires additional schema information that cannot be captured using the parameter mode. The specific attribute is 'System.Xml.Serialization.XmlElementAttribute'.
        [System.ServiceModel.OperationContractAttribute(Action="avg-payment", ReplyAction="http://ws.napthe.avg.com/Nap/submitVoucherByScratchcardResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="return")]
        Web.WsNapTheAvgSandbox.submitVoucherByScratchcardResponse submitVoucherByScratchcard(Web.WsNapTheAvgSandbox.submitVoucherByScratchcardRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.napthe.avg.com/")]
    public partial class response : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string responseDataField;
        
        private string returnCodeField;
        
        private string returnCodeDescriptionField;
        
        private string signatureField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public string responseData {
            get {
                return this.responseDataField;
            }
            set {
                this.responseDataField = value;
                this.RaisePropertyChanged("responseData");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string returnCode {
            get {
                return this.returnCodeField;
            }
            set {
                this.returnCodeField = value;
                this.RaisePropertyChanged("returnCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string returnCodeDescription {
            get {
                return this.returnCodeDescriptionField;
            }
            set {
                this.returnCodeDescriptionField = value;
                this.RaisePropertyChanged("returnCodeDescription");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string signature {
            get {
                return this.signatureField;
            }
            set {
                this.signatureField = value;
                this.RaisePropertyChanged("signature");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="submitVoucherByScratchcard", WrapperNamespace="http://ws.napthe.avg.com/", IsWrapped=true)]
    public partial class submitVoucherByScratchcardRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.napthe.avg.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string stbNumber;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.napthe.avg.com/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string cardNumber;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.napthe.avg.com/", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string clientTransId;
        
        public submitVoucherByScratchcardRequest() {
        }
        
        public submitVoucherByScratchcardRequest(string stbNumber, string cardNumber, string clientTransId) {
            this.stbNumber = stbNumber;
            this.cardNumber = cardNumber;
            this.clientTransId = clientTransId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="submitVoucherByScratchcardResponse", WrapperNamespace="http://ws.napthe.avg.com/", IsWrapped=true)]
    public partial class submitVoucherByScratchcardResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://ws.napthe.avg.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public Web.WsNapTheAvgSandbox.response @return;
        
        public submitVoucherByScratchcardResponse() {
        }
        
        public submitVoucherByScratchcardResponse(Web.WsNapTheAvgSandbox.response @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NapChannel : Web.WsNapTheAvgSandbox.Nap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NapClient : System.ServiceModel.ClientBase<Web.WsNapTheAvgSandbox.Nap>, Web.WsNapTheAvgSandbox.Nap {
        
        public NapClient() {
        }
        
        public NapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Web.WsNapTheAvgSandbox.submitVoucherByScratchcardResponse Web.WsNapTheAvgSandbox.Nap.submitVoucherByScratchcard(Web.WsNapTheAvgSandbox.submitVoucherByScratchcardRequest request) {
            return base.Channel.submitVoucherByScratchcard(request);
        }
        
        public Web.WsNapTheAvgSandbox.response submitVoucherByScratchcard(string stbNumber, string cardNumber, string clientTransId) {
            Web.WsNapTheAvgSandbox.submitVoucherByScratchcardRequest inValue = new Web.WsNapTheAvgSandbox.submitVoucherByScratchcardRequest();
            inValue.stbNumber = stbNumber;
            inValue.cardNumber = cardNumber;
            inValue.clientTransId = clientTransId;
            Web.WsNapTheAvgSandbox.submitVoucherByScratchcardResponse retVal = ((Web.WsNapTheAvgSandbox.Nap)(this)).submitVoucherByScratchcard(inValue);
            return retVal.@return;
        }
    }
}
