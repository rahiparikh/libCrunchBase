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
    public class ProvidershipInfo
    {
        private dynamic _SerializedProvidershipsInfo;
        private Dictionary<string, string> _providerInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private ProvidershipInfo(dynamic Provider)
        {
            _SerializedProvidershipsInfo = Provider;
            PopulateProvidershipsInfo();
            _Keys = GetKeys();
        }

        public static ProvidershipInfo[] ParseProvidershipsInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int providerships_array_length = _SerializedInfo.providerships.Count;
            List<ProvidershipInfo> rInfo = new List<ProvidershipInfo>();
            for(int i = 0; i < providerships_array_length ; i++)
            {
                rInfo.Add(new ProvidershipInfo(_SerializedInfo.providerships[i]));
            }
            return rInfo.ToArray();
        }

        private void PopulateProvidershipsInfo()
        {
            bool is_past = _SerializedProvidershipsInfo.is_past;
            if(string.IsNullOrEmpty(is_past.ToString()))
                AddToDictionary("is_past", null);
            else
                AddToDictionary("is_past", is_past.ToString());

            string title = _SerializedProvidershipsInfo.title;
            if(string.IsNullOrEmpty(title))
                AddToDictionary("title", null);
            else
                AddToDictionary("title", title);

            string first_name = _SerializedProvidershipsInfo.provider.name;
            if(string.IsNullOrEmpty(first_name))
                AddToDictionary("name", null);
            else
                AddToDictionary("name", first_name);

            string permalink = _SerializedProvidershipsInfo.provider.permalink;
            if(string.IsNullOrEmpty(permalink))
                AddToDictionary("permalink", null);
            else
                AddToDictionary("permalink", permalink);

            if (_SerializedProvidershipsInfo.provider.image == null)
                AddToDictionary("image", null);
            else
                AddToDictionary("image", _SerializedProvidershipsInfo.provider.image.available_sizes[0][1]);

            if (_SerializedProvidershipsInfo.provider.image == null || string.IsNullOrEmpty(_SerializedProvidershipsInfo.provider.image.attribution))
                AddToDictionary("attribution", null);
            else
                AddToDictionary("attribution", _SerializedProvidershipsInfo.provider.image.attribution.Replace("<p>", "").Replace("</p>", ""));
        }

        private void AddToDictionary(string Key, string Value)
        {
            _providerInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_providerInfo.ContainsKey(Key))
                return _providerInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_providerInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _providerInfo;
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
