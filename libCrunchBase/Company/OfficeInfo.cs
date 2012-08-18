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
    public class OfficeInfo
    {
        private dynamic _SerializedOfficeInfo;
        private Dictionary<string, string> _relationInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private OfficeInfo(dynamic Office)
        {
            _SerializedOfficeInfo = Office;
            PopulateOfficeInfo();
            _Keys = GetKeys();
        }

        public static OfficeInfo[] ParseOfficeInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int offices_array_length;
            try
            {
                offices_array_length = _SerializedInfo.offices.Count;
            }
            catch
            {
                return null;
            }
            List<OfficeInfo> oInfo = new List<OfficeInfo>();
            for (int i = 0; i < offices_array_length; i++)
            {
                oInfo.Add(new OfficeInfo(_SerializedInfo.offices[i]));
            }
            return oInfo.ToArray();
        }

        private void PopulateOfficeInfo()
        {
            if (string.IsNullOrEmpty(_SerializedOfficeInfo.description))
                AddToDictionary("description", null);
            else
                AddToDictionary("description", _SerializedOfficeInfo.description);

            if (string.IsNullOrEmpty(_SerializedOfficeInfo.address1))
                AddToDictionary("address1", null);
            else
                AddToDictionary("address1", _SerializedOfficeInfo.address1);

            if (string.IsNullOrEmpty(_SerializedOfficeInfo.address2))
                AddToDictionary("address2", null);
            else
                AddToDictionary("address2", _SerializedOfficeInfo.address2);

            if (string.IsNullOrEmpty(_SerializedOfficeInfo.zip_code))
                AddToDictionary("zip_code", null);
            else
                AddToDictionary("zip_code", _SerializedOfficeInfo.zip_code);

            if (string.IsNullOrEmpty(_SerializedOfficeInfo.city))
                AddToDictionary("city", null);
            else
                AddToDictionary("city", _SerializedOfficeInfo.city);

            string state_code = _SerializedOfficeInfo.state_code;
            if (_SerializedOfficeInfo.state_code == null)
                AddToDictionary("state_code", null);
            else
                AddToDictionary("state_code", state_code);

            if (_SerializedOfficeInfo.country_code == null)
                AddToDictionary("country_code", null);
            else
                AddToDictionary("country_code", _SerializedOfficeInfo.country_code);

            if (_SerializedOfficeInfo.latitude == null)
                AddToDictionary("latitude", null);
            else
                AddToDictionary("latitude", _SerializedOfficeInfo.latitude.ToString());

            if (_SerializedOfficeInfo.longitude == null)
                AddToDictionary("longitude", null);
            else
                AddToDictionary("longitude", _SerializedOfficeInfo.longitude.ToString());
        }

        private void AddToDictionary(string Key, string Value)
        {
            _relationInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_relationInfo.ContainsKey(Key))
                return _relationInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_relationInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _relationInfo;
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
