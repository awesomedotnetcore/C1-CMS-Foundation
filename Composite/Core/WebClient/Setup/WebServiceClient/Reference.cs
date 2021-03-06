﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Composite.Core.WebClient.Setup.WebServiceClient
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.composite.net/ns/management", ConfigurationName = "ServiceReference1.SetupSoap")]
    internal interface SetupSoap
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://www.composite.net/ns/management/Ping", ReplyAction = "*")]
        bool Ping();

        // CODEGEN: Generating message contract since element name version from namespace http://www.composite.net/ns/management is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.composite.net/ns/management/GetSetupDescription", ReplyAction = "*")]
        Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionResponse GetSetupDescription(Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionRequest request);

        // CODEGEN: Generating message contract since element name version from namespace http://www.composite.net/ns/management is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.composite.net/ns/management/GetGetLicense", ReplyAction = "*")]
        Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseResponse GetGetLicense(Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseRequest request);

        // CODEGEN: Generating message contract since element name version from namespace http://www.composite.net/ns/management is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.composite.net/ns/management/GetLanguages", ReplyAction = "*")]
        Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesResponse GetLanguages(Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesRequest request);

        // CODEGEN: Generating message contract since element name version from namespace http://www.composite.net/ns/management is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.composite.net/ns/management/GetLanguagePackages", ReplyAction = "*")]
        Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesResponse GetLanguagePackages(Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesRequest request);

        // CODEGEN: Generating message contract since element name version from namespace http://www.composite.net/ns/management is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.composite.net/ns/management/RegisterSetup", ReplyAction = "*")]
        Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupResponse RegisterSetup(Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupRequest request);
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class GetSetupDescriptionRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetSetupDescription", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionRequestBody Body;

        public GetSetupDescriptionRequest()
        {
        }

        public GetSetupDescriptionRequest(Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionRequestBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class GetSetupDescriptionRequestBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public string version;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
        public string installationId;

        public GetSetupDescriptionRequestBody()
        {
        }

        public GetSetupDescriptionRequestBody(string version, string installationId)
        {
            this.version = version;
            this.installationId = installationId;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class GetSetupDescriptionResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetSetupDescriptionResponse", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionResponseBody Body;

        public GetSetupDescriptionResponse()
        {
        }

        public GetSetupDescriptionResponse(Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionResponseBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class GetSetupDescriptionResponseBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public System.Xml.Linq.XElement GetSetupDescriptionResult;

        public GetSetupDescriptionResponseBody()
        {
        }

        public GetSetupDescriptionResponseBody(System.Xml.Linq.XElement GetSetupDescriptionResult)
        {
            this.GetSetupDescriptionResult = GetSetupDescriptionResult;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class GetGetLicenseRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetGetLicense", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseRequestBody Body;

        public GetGetLicenseRequest()
        {
        }

        public GetGetLicenseRequest(Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseRequestBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class GetGetLicenseRequestBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public string version;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
        public string installationId;

        public GetGetLicenseRequestBody()
        {
        }

        public GetGetLicenseRequestBody(string version, string installationId)
        {
            this.version = version;
            this.installationId = installationId;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class GetGetLicenseResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetGetLicenseResponse", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseResponseBody Body;

        public GetGetLicenseResponse()
        {
        }

        public GetGetLicenseResponse(Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseResponseBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class GetGetLicenseResponseBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public System.Xml.Linq.XElement GetGetLicenseResult;

        public GetGetLicenseResponseBody()
        {
        }

        public GetGetLicenseResponseBody(System.Xml.Linq.XElement GetGetLicenseResult)
        {
            this.GetGetLicenseResult = GetGetLicenseResult;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class GetLanguagesRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetLanguages", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesRequestBody Body;

        public GetLanguagesRequest()
        {
        }

        public GetLanguagesRequest(Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesRequestBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class GetLanguagesRequestBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public string version;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
        public string installationId;

        public GetLanguagesRequestBody()
        {
        }

        public GetLanguagesRequestBody(string version, string installationId)
        {
            this.version = version;
            this.installationId = installationId;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class GetLanguagesResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetLanguagesResponse", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesResponseBody Body;

        public GetLanguagesResponse()
        {
        }

        public GetLanguagesResponse(Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesResponseBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class GetLanguagesResponseBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public System.Xml.Linq.XElement GetLanguagesResult;

        public GetLanguagesResponseBody()
        {
        }

        public GetLanguagesResponseBody(System.Xml.Linq.XElement GetLanguagesResult)
        {
            this.GetLanguagesResult = GetLanguagesResult;
        }
    }





    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class GetLanguagePackagesRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetLanguagePackages", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesRequestBody Body;

        public GetLanguagePackagesRequest()
        {
        }

        public GetLanguagePackagesRequest(Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesRequestBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class GetLanguagePackagesRequestBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public string version;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
        public string installationId;

        public GetLanguagePackagesRequestBody()
        {
        }

        public GetLanguagePackagesRequestBody(string version, string installationId)
        {
            this.version = version;
            this.installationId = installationId;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class GetLanguagePackagesResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "GetLanguagePackagesResponse", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesResponseBody Body;

        public GetLanguagePackagesResponse()
        {
        }

        public GetLanguagePackagesResponse(Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesResponseBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class GetLanguagePackagesResponseBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public System.Xml.Linq.XElement GetLanguagePackagesResult;

        public GetLanguagePackagesResponseBody()
        {
        }

        public GetLanguagePackagesResponseBody(System.Xml.Linq.XElement GetLanguagePackagesResult)
        {
            this.GetLanguagePackagesResult = GetLanguagePackagesResult;
        }
    }





    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class RegisterSetupRequest
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "RegisterSetup", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupRequestBody Body;

        public RegisterSetupRequest()
        {
        }

        public RegisterSetupRequest(Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupRequestBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class RegisterSetupRequestBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 0)]
        public string version;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 1)]
        public string installationId;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 2)]
        public string setupDescriptionXml;

        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue = false, Order = 3)]
        public string exception;

        public RegisterSetupRequestBody()
        {
        }

        public RegisterSetupRequestBody(string version, string installationId, string setupDescriptionXml, string exception)
        {
            this.version = version;
            this.installationId = installationId;
            this.setupDescriptionXml = setupDescriptionXml;
            this.exception = exception;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    internal partial class RegisterSetupResponse
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Name = "RegisterSetupResponse", Namespace = "http://www.composite.net/ns/management", Order = 0)]
        public Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupResponseBody Body;

        public RegisterSetupResponse()
        {
        }

        public RegisterSetupResponse(Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupResponseBody Body)
        {
            this.Body = Body;
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace = "http://www.composite.net/ns/management")]
    internal partial class RegisterSetupResponseBody
    {

        [System.Runtime.Serialization.DataMemberAttribute(Order = 0)]
        public bool RegisterSetupResult;

        public RegisterSetupResponseBody()
        {
        }

        public RegisterSetupResponseBody(bool RegisterSetupResult)
        {
            this.RegisterSetupResult = RegisterSetupResult;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    internal interface SetupSoapChannel : Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    internal partial class SetupSoapClient : System.ServiceModel.ClientBase<Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap>, Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap
    {

        public SetupSoapClient()
        {
        }

        public SetupSoapClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public SetupSoapClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SetupSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public SetupSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public bool Ping()
        {
            return base.Channel.Ping();
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionResponse Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap.GetSetupDescription(Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionRequest request)
        {
            return base.Channel.GetSetupDescription(request);
        }

        public System.Xml.Linq.XElement GetSetupDescription(string version, string installationId)
        {
            Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionRequest inValue = new Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionRequest();
            inValue.Body = new Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionRequestBody();
            inValue.Body.version = version;
            inValue.Body.installationId = installationId;
            Composite.Core.WebClient.Setup.WebServiceClient.GetSetupDescriptionResponse retVal = ((Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap)(this)).GetSetupDescription(inValue);
            return retVal.Body.GetSetupDescriptionResult;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseResponse Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap.GetGetLicense(Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseRequest request)
        {
            return base.Channel.GetGetLicense(request);
        }

        public System.Xml.Linq.XElement GetGetLicense(string version, string installationId)
        {
            Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseRequest inValue = new Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseRequest();
            inValue.Body = new Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseRequestBody();
            inValue.Body.version = version;
            inValue.Body.installationId = installationId;
            Composite.Core.WebClient.Setup.WebServiceClient.GetGetLicenseResponse retVal = ((Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap)(this)).GetGetLicense(inValue);
            return retVal.Body.GetGetLicenseResult;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesResponse Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap.GetLanguages(Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesRequest request)
        {
            return base.Channel.GetLanguages(request);
        }

        public System.Xml.Linq.XElement GetLanguages(string version, string installationId)
        {
            Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesRequest inValue = new Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesRequest();
            inValue.Body = new Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesRequestBody();
            inValue.Body.version = version;
            inValue.Body.installationId = installationId;
            Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagesResponse retVal = ((Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap)(this)).GetLanguages(inValue);
            return retVal.Body.GetLanguagesResult;
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesResponse Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap.GetLanguagePackages(Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesRequest request)
        {
            
            return base.Channel.GetLanguagePackages(request);
        }

        public System.Xml.Linq.XElement GetLanguagePackages(string version, string installationId)
        {
            Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesRequest inValue = new Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesRequest();
            inValue.Body = new Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesRequestBody();
            inValue.Body.version = version;
            inValue.Body.installationId = installationId;
            Composite.Core.WebClient.Setup.WebServiceClient.GetLanguagePackagesResponse retVal = ((Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap)(this)).GetLanguagePackages(inValue);
            return retVal.Body.GetLanguagePackagesResult;
        }


        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupResponse Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap.RegisterSetup(Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupRequest request)
        {
            return base.Channel.RegisterSetup(request);
        }

        public bool RegisterSetup(string version, string installationId, string setupDescriptionXml, string exception)
        {
            Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupRequest inValue = new Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupRequest();
            inValue.Body = new Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupRequestBody();
            inValue.Body.version = version;
            inValue.Body.installationId = installationId;
            inValue.Body.setupDescriptionXml = setupDescriptionXml;
            inValue.Body.exception = exception;
            Composite.Core.WebClient.Setup.WebServiceClient.RegisterSetupResponse retVal = ((Composite.Core.WebClient.Setup.WebServiceClient.SetupSoap)(this)).RegisterSetup(inValue);
            return retVal.Body.RegisterSetupResult;
        }
    }
}