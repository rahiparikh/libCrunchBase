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

namespace CrunchBase.Company
{
    public class ExternalLinkInfo
    {
        private dynamic _SerializedExternalLinkInfo;
        private Dictionary<string, string> _videoEmbedInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private ExternalLinkInfo(dynamic ExternalLink)
        {
            _SerializedExternalLinkInfo = ExternalLink;
            PopulateVideoEmbedsInfo();
            _Keys = GetKeys();
        }

        public static ExternalLinkInfo[] ParseExternalLinksInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int external_links_array_length = _SerializedInfo.external_links.Count;
            List<ExternalLinkInfo> rInfo = new List<ExternalLinkInfo>();
            for(int i = 0; i < external_links_array_length ; i++)
            {
                rInfo.Add(new ExternalLinkInfo(_SerializedInfo.external_links[i]));
            }
            return rInfo.ToArray();
        }

        private void PopulateVideoEmbedsInfo()
        {
            string external_url = _SerializedExternalLinkInfo.external_url;
            if(string.IsNullOrEmpty(external_url))
                AddToDictionary("external_url", null);
            else
                AddToDictionary("external_url", external_url);

            string title = _SerializedExternalLinkInfo.title;
            if (string.IsNullOrEmpty(title))
                AddToDictionary("title", title);
            else
                AddToDictionary("title", title);
        }

        private void AddToDictionary(string Key, string Value)
        {
            _videoEmbedInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_videoEmbedInfo.ContainsKey(Key))
                return _videoEmbedInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_videoEmbedInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _videoEmbedInfo;
        }

        public string GetNextKey()
        {
            try
            {
                string nextKey = _Keys[0];
                _Keys.RemoveAt(0);
                return nextKey;
            }
            catch
            {
                return null;
            }
        }
    }
}
