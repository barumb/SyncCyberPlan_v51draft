﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SyncCyberPlan_lib.SageX3WS_old {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.adonix.com/WSS", ConfigurationName="SageX3WS_old.CAdxWebServiceXmlCC")]
    public interface CAdxWebServiceXmlCC {
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="runReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml run(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string inputXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="runReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> runAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string inputXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="saveReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml save(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string objectXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="saveReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> saveAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string objectXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="deleteReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml delete(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="deleteReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> deleteAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="readReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml read(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="readReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> readAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="queryReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml query(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, int listSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="queryReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> queryAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, int listSize);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getDescriptionReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml getDescription(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getDescriptionReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> getDescriptionAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="modifyReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml modify(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string objectXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="modifyReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> modifyAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string objectXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="actionObjectReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml actionObject(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string actionCode, string objectXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="actionObjectReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> actionObjectAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string actionCode, string objectXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="actionObjectKeysReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml actionObjectKeys(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string actionCode, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="actionObjectKeysReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> actionObjectKeysAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string actionCode, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getDataXmlSchemaReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml getDataXmlSchema(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="getDataXmlSchemaReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> getDataXmlSchemaAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="insertLinesReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml insertLines(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string blocKey, string lineKey, string lineXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="insertLinesReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> insertLinesAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string blocKey, string lineKey, string lineXml);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxParamKeyValue))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(CAdxMessage))]
        [return: System.ServiceModel.MessageParameterAttribute(Name="deleteLinesReturn")]
        SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml deleteLines(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string blocKey, string[] lineKeys);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="deleteLinesReturn")]
        System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> deleteLinesAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string blocKey, string[] lineKeys);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.adonix.com/WSS")]
    public partial class CAdxCallContext : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string codeLangField;
        
        private string poolAliasField;
        
        private string poolIdField;
        
        private string requestConfigField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string codeLang {
            get {
                return this.codeLangField;
            }
            set {
                this.codeLangField = value;
                this.RaisePropertyChanged("codeLang");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string poolAlias {
            get {
                return this.poolAliasField;
            }
            set {
                this.poolAliasField = value;
                this.RaisePropertyChanged("poolAlias");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string poolId {
            get {
                return this.poolIdField;
            }
            set {
                this.poolIdField = value;
                this.RaisePropertyChanged("poolId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string requestConfig {
            get {
                return this.requestConfigField;
            }
            set {
                this.requestConfigField = value;
                this.RaisePropertyChanged("requestConfig");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.adonix.com/WSS")]
    public partial class CAdxParamKeyValue : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string keyField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
                this.RaisePropertyChanged("key");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
                this.RaisePropertyChanged("value");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.adonix.com/WSS")]
    public partial class CAdxTechnicalInfos : object, System.ComponentModel.INotifyPropertyChanged {
        
        private bool busyField;
        
        private bool changeLanguageField;
        
        private bool changeUserIdField;
        
        private bool flushAdxField;
        
        private double loadWebsDurationField;
        
        private int nbDistributionCycleField;
        
        private double poolDistribDurationField;
        
        private int poolEntryIdxField;
        
        private double poolExecDurationField;
        
        private double poolRequestDurationField;
        
        private double poolWaitDurationField;
        
        private string processReportField;
        
        private int processReportSizeField;
        
        private bool reloadWebsField;
        
        private bool resumitAfterDBOpenField;
        
        private int rowInDistribStackField;
        
        private double totalDurationField;
        
        private string traceRequestField;
        
        private int traceRequestSizeField;
        
        /// <remarks/>
        public bool busy {
            get {
                return this.busyField;
            }
            set {
                this.busyField = value;
                this.RaisePropertyChanged("busy");
            }
        }
        
        /// <remarks/>
        public bool changeLanguage {
            get {
                return this.changeLanguageField;
            }
            set {
                this.changeLanguageField = value;
                this.RaisePropertyChanged("changeLanguage");
            }
        }
        
        /// <remarks/>
        public bool changeUserId {
            get {
                return this.changeUserIdField;
            }
            set {
                this.changeUserIdField = value;
                this.RaisePropertyChanged("changeUserId");
            }
        }
        
        /// <remarks/>
        public bool flushAdx {
            get {
                return this.flushAdxField;
            }
            set {
                this.flushAdxField = value;
                this.RaisePropertyChanged("flushAdx");
            }
        }
        
        /// <remarks/>
        public double loadWebsDuration {
            get {
                return this.loadWebsDurationField;
            }
            set {
                this.loadWebsDurationField = value;
                this.RaisePropertyChanged("loadWebsDuration");
            }
        }
        
        /// <remarks/>
        public int nbDistributionCycle {
            get {
                return this.nbDistributionCycleField;
            }
            set {
                this.nbDistributionCycleField = value;
                this.RaisePropertyChanged("nbDistributionCycle");
            }
        }
        
        /// <remarks/>
        public double poolDistribDuration {
            get {
                return this.poolDistribDurationField;
            }
            set {
                this.poolDistribDurationField = value;
                this.RaisePropertyChanged("poolDistribDuration");
            }
        }
        
        /// <remarks/>
        public int poolEntryIdx {
            get {
                return this.poolEntryIdxField;
            }
            set {
                this.poolEntryIdxField = value;
                this.RaisePropertyChanged("poolEntryIdx");
            }
        }
        
        /// <remarks/>
        public double poolExecDuration {
            get {
                return this.poolExecDurationField;
            }
            set {
                this.poolExecDurationField = value;
                this.RaisePropertyChanged("poolExecDuration");
            }
        }
        
        /// <remarks/>
        public double poolRequestDuration {
            get {
                return this.poolRequestDurationField;
            }
            set {
                this.poolRequestDurationField = value;
                this.RaisePropertyChanged("poolRequestDuration");
            }
        }
        
        /// <remarks/>
        public double poolWaitDuration {
            get {
                return this.poolWaitDurationField;
            }
            set {
                this.poolWaitDurationField = value;
                this.RaisePropertyChanged("poolWaitDuration");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string processReport {
            get {
                return this.processReportField;
            }
            set {
                this.processReportField = value;
                this.RaisePropertyChanged("processReport");
            }
        }
        
        /// <remarks/>
        public int processReportSize {
            get {
                return this.processReportSizeField;
            }
            set {
                this.processReportSizeField = value;
                this.RaisePropertyChanged("processReportSize");
            }
        }
        
        /// <remarks/>
        public bool reloadWebs {
            get {
                return this.reloadWebsField;
            }
            set {
                this.reloadWebsField = value;
                this.RaisePropertyChanged("reloadWebs");
            }
        }
        
        /// <remarks/>
        public bool resumitAfterDBOpen {
            get {
                return this.resumitAfterDBOpenField;
            }
            set {
                this.resumitAfterDBOpenField = value;
                this.RaisePropertyChanged("resumitAfterDBOpen");
            }
        }
        
        /// <remarks/>
        public int rowInDistribStack {
            get {
                return this.rowInDistribStackField;
            }
            set {
                this.rowInDistribStackField = value;
                this.RaisePropertyChanged("rowInDistribStack");
            }
        }
        
        /// <remarks/>
        public double totalDuration {
            get {
                return this.totalDurationField;
            }
            set {
                this.totalDurationField = value;
                this.RaisePropertyChanged("totalDuration");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string traceRequest {
            get {
                return this.traceRequestField;
            }
            set {
                this.traceRequestField = value;
                this.RaisePropertyChanged("traceRequest");
            }
        }
        
        /// <remarks/>
        public int traceRequestSize {
            get {
                return this.traceRequestSizeField;
            }
            set {
                this.traceRequestSizeField = value;
                this.RaisePropertyChanged("traceRequestSize");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.adonix.com/WSS")]
    public partial class CAdxMessage : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string messageField;
        
        private string typeField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
                this.RaisePropertyChanged("message");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
                this.RaisePropertyChanged("type");
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
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2556.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="http://www.adonix.com/WSS")]
    public partial class CAdxResultXml : object, System.ComponentModel.INotifyPropertyChanged {
        
        private CAdxMessage[] messagesField;
        
        private string resultXmlField;
        
        private int statusField;
        
        private CAdxTechnicalInfos technicalInfosField;
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public CAdxMessage[] messages {
            get {
                return this.messagesField;
            }
            set {
                this.messagesField = value;
                this.RaisePropertyChanged("messages");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public string resultXml {
            get {
                return this.resultXmlField;
            }
            set {
                this.resultXmlField = value;
                this.RaisePropertyChanged("resultXml");
            }
        }
        
        /// <remarks/>
        public int status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
                this.RaisePropertyChanged("status");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.SoapElementAttribute(IsNullable=true)]
        public CAdxTechnicalInfos technicalInfos {
            get {
                return this.technicalInfosField;
            }
            set {
                this.technicalInfosField = value;
                this.RaisePropertyChanged("technicalInfos");
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
    public interface CAdxWebServiceXmlCCChannel : SyncCyberPlan_lib.SageX3WS_old.CAdxWebServiceXmlCC, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CAdxWebServiceXmlCCClient : System.ServiceModel.ClientBase<SyncCyberPlan_lib.SageX3WS_old.CAdxWebServiceXmlCC>, SyncCyberPlan_lib.SageX3WS_old.CAdxWebServiceXmlCC {
        
        public CAdxWebServiceXmlCCClient() {
        }
        
        public CAdxWebServiceXmlCCClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CAdxWebServiceXmlCCClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CAdxWebServiceXmlCCClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CAdxWebServiceXmlCCClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml run(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string inputXml) {
            return base.Channel.run(callContext, publicName, inputXml);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> runAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string inputXml) {
            return base.Channel.runAsync(callContext, publicName, inputXml);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml save(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string objectXml) {
            return base.Channel.save(callContext, publicName, objectXml);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> saveAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string objectXml) {
            return base.Channel.saveAsync(callContext, publicName, objectXml);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml delete(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys) {
            return base.Channel.delete(callContext, publicName, objectKeys);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> deleteAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys) {
            return base.Channel.deleteAsync(callContext, publicName, objectKeys);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml read(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys) {
            return base.Channel.read(callContext, publicName, objectKeys);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> readAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys) {
            return base.Channel.readAsync(callContext, publicName, objectKeys);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml query(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, int listSize) {
            return base.Channel.query(callContext, publicName, objectKeys, listSize);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> queryAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, int listSize) {
            return base.Channel.queryAsync(callContext, publicName, objectKeys, listSize);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml getDescription(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName) {
            return base.Channel.getDescription(callContext, publicName);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> getDescriptionAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName) {
            return base.Channel.getDescriptionAsync(callContext, publicName);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml modify(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string objectXml) {
            return base.Channel.modify(callContext, publicName, objectKeys, objectXml);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> modifyAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string objectXml) {
            return base.Channel.modifyAsync(callContext, publicName, objectKeys, objectXml);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml actionObject(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string actionCode, string objectXml) {
            return base.Channel.actionObject(callContext, publicName, actionCode, objectXml);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> actionObjectAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string actionCode, string objectXml) {
            return base.Channel.actionObjectAsync(callContext, publicName, actionCode, objectXml);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml actionObjectKeys(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string actionCode, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys) {
            return base.Channel.actionObjectKeys(callContext, publicName, actionCode, objectKeys);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> actionObjectKeysAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, string actionCode, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys) {
            return base.Channel.actionObjectKeysAsync(callContext, publicName, actionCode, objectKeys);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml getDataXmlSchema(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName) {
            return base.Channel.getDataXmlSchema(callContext, publicName);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> getDataXmlSchemaAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName) {
            return base.Channel.getDataXmlSchemaAsync(callContext, publicName);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml insertLines(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string blocKey, string lineKey, string lineXml) {
            return base.Channel.insertLines(callContext, publicName, objectKeys, blocKey, lineKey, lineXml);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> insertLinesAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string blocKey, string lineKey, string lineXml) {
            return base.Channel.insertLinesAsync(callContext, publicName, objectKeys, blocKey, lineKey, lineXml);
        }
        
        public SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml deleteLines(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string blocKey, string[] lineKeys) {
            return base.Channel.deleteLines(callContext, publicName, objectKeys, blocKey, lineKeys);
        }
        
        public System.Threading.Tasks.Task<SyncCyberPlan_lib.SageX3WS_old.CAdxResultXml> deleteLinesAsync(SyncCyberPlan_lib.SageX3WS_old.CAdxCallContext callContext, string publicName, SyncCyberPlan_lib.SageX3WS_old.CAdxParamKeyValue[] objectKeys, string blocKey, string[] lineKeys) {
            return base.Channel.deleteLinesAsync(callContext, publicName, objectKeys, blocKey, lineKeys);
        }
    }
}
