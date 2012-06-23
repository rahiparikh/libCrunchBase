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
using System.Globalization;
using System.Collections;

namespace CrunchBase.Company
{
    public class RelationshipInfo
    {
        private dynamic _SerializedRelationshipInfo;
        private Dictionary<string, string> _relationInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private RelationshipInfo(dynamic Relationship)
        {
            _SerializedRelationshipInfo = Relationship;
            PopulateRelationInfo();
            _Keys = GetKeys();
        }

        public static RelationshipInfo[] ParseRelationshipInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int relationship_array_length = _SerializedInfo.relationships.Count;
            List<RelationshipInfo> rInfo = new List<RelationshipInfo>();
            for(int i = 0; i < relationship_array_length ; i++)
            {
                rInfo.Add(new RelationshipInfo(_SerializedInfo.relationships[i]));
            }
            return rInfo.ToArray();
        }

        private void PopulateRelationInfo()
        {
            bool is_past = _SerializedRelationshipInfo.is_past;
            if(string.IsNullOrEmpty(is_past.ToString()))
                AddToDictionary("is_past", null);
            else
                AddToDictionary("is_past", is_past.ToString());

            string title = _SerializedRelationshipInfo.title;
            if(string.IsNullOrEmpty(title))
                AddToDictionary("title", null);
            else
                AddToDictionary("title", title);

            string first_name = _SerializedRelationshipInfo.person.first_name;
            if(string.IsNullOrEmpty(first_name))
                AddToDictionary("first_name", null);
            else
                AddToDictionary("first_name", first_name);

            string last_name = _SerializedRelationshipInfo.person.last_name;
            if(string.IsNullOrEmpty(last_name))
                AddToDictionary("last_name", null);
            else
                AddToDictionary("last_name", last_name);

            string permalink = _SerializedRelationshipInfo.person.permalink;
            if(string.IsNullOrEmpty(permalink))
                AddToDictionary("permalink", null);
            else
                AddToDictionary("permalink", permalink);

            if(_SerializedRelationshipInfo.person.image == null)
                AddToDictionary("image", null);
            else
                AddToDictionary("image", _SerializedRelationshipInfo.person.image.available_sizes[0][1]);

            if (_SerializedRelationshipInfo.person.image == null || string.IsNullOrEmpty(_SerializedRelationshipInfo.person.image.attribution))
                AddToDictionary("attribution", null);
            else
                AddToDictionary("attribution", _SerializedRelationshipInfo.person.image.attribution.Replace("<p>","").Replace("</p>",""));
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
