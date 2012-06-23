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
            int offices_array_length = _SerializedInfo.offices.Count;
            List<OfficeInfo> oInfo = new List<OfficeInfo>();
            for (int i = 0; i < offices_array_length; i++)
            {
                oInfo.Add(new OfficeInfo(_SerializedInfo.offices[i]));
            }
            return oInfo.ToArray();
        }

        private void PopulateOfficeInfo()
        {
            string description = _SerializedOfficeInfo.description;
            if (string.IsNullOrEmpty(description.ToString()))
                AddToDictionary("is_past", null);
            else
                AddToDictionary("is_past", description);

            string address1 = _SerializedOfficeInfo.address1;
            if (string.IsNullOrEmpty(address1))
                AddToDictionary("title", null);
            else
                AddToDictionary("title", address1);

            string address2 = _SerializedOfficeInfo.address2;
            if (string.IsNullOrEmpty(address2))
                AddToDictionary("first_name", null);
            else
                AddToDictionary("first_name", address2);

            string zip_code = _SerializedOfficeInfo.zip_code;
            if (string.IsNullOrEmpty(zip_code))
                AddToDictionary("last_name", null);
            else
                AddToDictionary("last_name", zip_code);

            string city = _SerializedOfficeInfo.city;
            if (string.IsNullOrEmpty(city))
                AddToDictionary("permalink", null);
            else
                AddToDictionary("permalink", city);

            string state_code = _SerializedOfficeInfo.state_code;
            if (_SerializedOfficeInfo.person.image == null)
                AddToDictionary("image", null);
            else
                AddToDictionary("image", state_code);

            string country_code = _SerializedOfficeInfo.country_code;
            if (_SerializedOfficeInfo.person.image == null)
                AddToDictionary("image", null);
            else
                AddToDictionary("image", country_code);

            string latitude = _SerializedOfficeInfo.latitude;
            if (_SerializedOfficeInfo.person.image == null)
                AddToDictionary("image", null);
            else
                AddToDictionary("image", latitude);

            string longitude = _SerializedOfficeInfo.longitude;
            if (_SerializedOfficeInfo.person.image == null)
                AddToDictionary("image", null);
            else
                AddToDictionary("image", longitude);
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
