/*
 * Copyright © Rahil Parikh 2012
 * 
 * Dual-Licensed - you may choose between
 * 
 * 1. Microsoft Reciprocal License
 * 2. CC BY 3.0
 * 
 * This file is part of libCrunchBase.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.ObjectModel;
using System.Dynamic;
using CrunchBase;
using CrunchBase.Exceptions;

namespace CrunchBase.Company
{
    public class Company
    {
        private string _CompanyName;
        private string _ApiUrl = "http://api.crunchbase.com/v/1/";
        private static string _SearchUrl = "http://api.crunchbase.com/v/1/search.js?query=";
        private dynamic _SerializedInfo;

        #region Company
        private Company(string CompanyName, string CompanyPermaLink)
        {
            _CompanyName = CompanyName;

            WebRequest weGetURL = WebRequest.Create(_ApiUrl + @"company/" + CompanyPermaLink + ".js");
            WebResponse weRes;
            try
            {
                weRes = weGetURL.GetResponse();
            }
            catch
            {
                Console.WriteLine("Exception for {0}", CompanyName);
                return;
            }
            StreamReader reader = new StreamReader(weRes.GetResponseStream());
            string jsonCompanyInfo = reader.ReadToEnd();
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            _SerializedInfo = serializer.Deserialize(jsonCompanyInfo, typeof(object)) as dynamic;

            if (_SerializedInfo.funding_rounds.Count > 0)
                Console.WriteLine("funding_rounds found " + CompanyName);

            if (_SerializedInfo.investments.Count > 0)
                Console.WriteLine("investments found " + CompanyName);

            if (_SerializedInfo.acquisitions.Count > 0)
                Console.WriteLine("acquisitions found " + CompanyName);

            if (_SerializedInfo.screenshots.Count > 0)
                Console.WriteLine("screenshots found " + CompanyName);
        }

        public dynamic GetSerializedInfo()
        {
            return _SerializedInfo;
        }

        /// <summary>
        /// Searches for company in CrunchBase
        /// </summary>
        /// <param name="CompanyName"></param>
        /// <returns>
        /// If company exists in CrunchBase 
        /// the method will return <c>Company</c>
        /// class' object or else it will return <c>null</c>.
        /// </returns>
        public static Company FindCompany(string CompanyName)
        {
            WebRequest weGetURL = WebRequest.Create(_SearchUrl + CompanyName);
            WebResponse weRes = weGetURL.GetResponse();
            StreamReader reader = new StreamReader(weRes.GetResponseStream());
            string jsonSearchResponse = reader.ReadToEnd();
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            dynamic serializedSearchResponse = serializer.Deserialize(jsonSearchResponse, typeof(object)) as dynamic;
            if (serializedSearchResponse.total == 1)
            {
                return new Company(serializedSearchResponse.results[0].name, serializedSearchResponse.results[0].permalink);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region CompanyInfo
        private CompanyInfo _CompanyInfo = null;
        public CompanyInfo ParseCompanyInfo()
        {
            CompanyInfo cInfo = CompanyInfo.ParseCompanyInfo(this);
            this._CompanyInfo = cInfo;
            return this._CompanyInfo;
        }

        public CompanyInfo AccessCompanyInfo()
        {
            if (_CompanyInfo == null)
            {
                new InformationNotParsed("Company Information has not been parsed yet. Please parse the information before accessing.");
                return null;
            }
            else
            {
                return _CompanyInfo;
            }
        }
        #endregion

        #region RelationshipInfo
        private RelationshipInfo[] _RelationshipInfo = null;
        public RelationshipInfo[] ParseRelationshipInfo()
        {
            RelationshipInfo[] rInfo = RelationshipInfo.ParseRelationshipInfo(this);
            this._RelationshipInfo = rInfo;
            return this._RelationshipInfo;
        }

        public RelationshipInfo[] AccessRelationshipInfo()
        {
            if (_RelationshipInfo == null)
            {
                new InformationNotParsed("Relationship Information has not been parsed yet. Please parse the information before accessing.");
                return null;
            }
            else
            {
                return _RelationshipInfo;
            }
        }
        #endregion

        #region ProvidershipsInfo
        private ProvidershipInfo[] _ProvidershipsInfo = null;
        public ProvidershipInfo[] ParseProvidershipsInfo()
        {
            ProvidershipInfo[] pInfo = ProvidershipInfo.ParseProvidershipsInfo(this);
            this._ProvidershipsInfo = pInfo;
            return this._ProvidershipsInfo;
        }

        public ProvidershipInfo[] AccessProvidershipsInfo()
        {
            if (_ProvidershipsInfo == null)
            {
                new InformationNotParsed("Providerships Information has not been parsed yet. Please parse the information before accessing.");
                return null;
            }
            else
            {
                return _ProvidershipsInfo;
            }
        }
        #endregion

        #region OfficeInfo
        private OfficeInfo[] _OfficeInfo = null;
        public OfficeInfo[] ParseOfficeInfo()
        {
            OfficeInfo[] oInfo = OfficeInfo.ParseOfficeInfo(this);
            this._OfficeInfo = oInfo;
            return this._OfficeInfo;
        }

        public OfficeInfo[] AccessOfficeInfo()
        {
            if (_OfficeInfo == null)
            {
                new InformationNotParsed("Providerships Information has not been parsed yet. Please parse the information before accessing.");
                return null;
            }
            else
            {
                return _OfficeInfo;
            }
        }
        #endregion

        #region MilestoneInfo
        private MilestoneInfo[] _MilestoneInfo = null;
        public MilestoneInfo[] ParseMilestoneInfo()
        {
            MilestoneInfo[] mInfo = MilestoneInfo.ParseMilestonesInfo(this);
            this._MilestoneInfo = mInfo;
            return this._MilestoneInfo;
        }

        public MilestoneInfo[] AccessMilestoneInfo()
        {
            if (_MilestoneInfo == null)
            {
                new InformationNotParsed("Providerships Information has not been parsed yet. Please parse the information before accessing.");
                return null;
            }
            else
            {
                return _MilestoneInfo;
            }
        }
        #endregion

        #region VideoEmbedInfo
        private VideoEmbedInfo[] _VideoEmbedInfo = null;
        public VideoEmbedInfo[] ParseVideoEmbedInfo()
        {
            VideoEmbedInfo[] vInfo = VideoEmbedInfo.ParseVideoEmbedsInfo(this);
            this._VideoEmbedInfo = vInfo;
            return this._VideoEmbedInfo;
        }

        public VideoEmbedInfo[] AccessVideoEmbedInfo()
        {
            if (_VideoEmbedInfo == null)
            {
                new InformationNotParsed("Providerships Information has not been parsed yet. Please parse the information before accessing.");
                return null;
            }
            else
            {
                return _VideoEmbedInfo;
            }
        }
        #endregion

        #region ExternalLinkInfo
        private ExternalLinkInfo[] _ExternalLinkInfo = null;
        public ExternalLinkInfo[] ParseExternalLinkInfo()
        {
            ExternalLinkInfo[] elInfo = ExternalLinkInfo.ParseExternalLinksInfo(this);
            this._ExternalLinkInfo = elInfo;
            return this._ExternalLinkInfo;
        }

        public ExternalLinkInfo[] AccessExternalLinkInfo()
        {
            if (_ExternalLinkInfo == null)
            {
                new InformationNotParsed("Providerships Information has not been parsed yet. Please parse the information before accessing.");
                return null;
            }
            else
            {
                return _ExternalLinkInfo;
            }
        }
        #endregion

        public void ParseAll()
        {
            this.ParseCompanyInfo();
            this.ParseRelationshipInfo();
            this.ParseProvidershipsInfo();
            this.ParseOfficeInfo();
            this.ParseMilestoneInfo();
            this.ParseVideoEmbedInfo();
            this.ParseExternalLinkInfo();
        }

    }
}
