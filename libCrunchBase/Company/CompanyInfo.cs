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

namespace CrunchBase.Company
{
    public class CompanyInfo
    {
        private dynamic _SerializedInfo;
        private Dictionary<string, string> _companyInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private CompanyInfo(Company CompanyObject)
        {
            _SerializedInfo = CompanyObject.GetSerializedInfo();
            PopulateCompanyInfo();
            _Keys = GetKeys();
        }

        private void PopulateCompanyInfo()
        {
            AddToDictionary("name", _SerializedInfo.name);
            AddToDictionary("crunchbase_url", _SerializedInfo.crunchbase_url);

            string homepage_url = _SerializedInfo.homepage_url;
            if(string.IsNullOrEmpty(homepage_url))
                AddToDictionary("homepage_url",null);
            else
                AddToDictionary("homepage_url", _SerializedInfo.homepage_url.ToString());

            string blog_url = _SerializedInfo.blog_url;
            if (string.IsNullOrEmpty(blog_url))
                AddToDictionary("blog_url", null);
            else
                AddToDictionary("blog_url", blog_url);
            
            string blog_feed_url = _SerializedInfo.blog_feed_url;
            if(string.IsNullOrEmpty(blog_feed_url))
                AddToDictionary("blog_feed_url", null);
            else
                AddToDictionary("blog_feed_url", blog_feed_url);
            
            string twitter_username = _SerializedInfo.twitter_username;
            if(string.IsNullOrEmpty(twitter_username))
                AddToDictionary("twitter_username", null);
            else
                AddToDictionary("twitter_username", twitter_username);

            string category_code = _SerializedInfo.category_code;
            if (string.IsNullOrEmpty(category_code))
                AddToDictionary("category_code", null);
            else
                AddToDictionary("category_code", category_code);

            int number_of_employees;
            try
            {
                number_of_employees = _SerializedInfo.number_of_employees;
            }
            catch
            {
                number_of_employees = 0;
            }
            AddToDictionary("number_of_employees", number_of_employees.ToString());
            
            try
            {
                DateTime founded_on = new DateTime(_SerializedInfo.founded_year, _SerializedInfo.founded_month, _SerializedInfo.founded_day);
                AddToDictionary("founded_on",founded_on.ToShortDateString());
            }
            catch
            {
                AddToDictionary("founded_on", null);
            }

            try
            {
                DateTime deadpooled_on = new DateTime(_SerializedInfo.deadpooled_year, _SerializedInfo.deadpooled_month, _SerializedInfo.deadpooled_day);
                AddToDictionary("deadpooled_on", deadpooled_on.ToShortDateString());
            }
            catch
            {
                AddToDictionary("deadpooled_on", null);
            }
            
            string deadpooled_url = _SerializedInfo.deadpooled_url;
            if(string.IsNullOrEmpty(deadpooled_url))
                AddToDictionary("deadpooled_url", null);
            else
                AddToDictionary("deadpooled_url", deadpooled_url);

            string tag_list = _SerializedInfo.tag_list;
            if(string.IsNullOrEmpty(tag_list))
                AddToDictionary("tag_list", null);
            else
                AddToDictionary("tag_list", tag_list);

            string alias_list = _SerializedInfo.alias_list;
            if(string.IsNullOrEmpty(alias_list))
                AddToDictionary("alias_list", null);
            else
                AddToDictionary("alias_list", alias_list);

            string email_address = _SerializedInfo.email_address;
            if(string.IsNullOrEmpty(email_address))
                AddToDictionary("email_address", null);
            else
                AddToDictionary("email_address", email_address);

            string phone_number = _SerializedInfo.phone_number;
            if(string.IsNullOrEmpty(phone_number))
                AddToDictionary("phone_number", null);
            else
                AddToDictionary("phone_number", phone_number);

            string description = _SerializedInfo.description;
            if(string.IsNullOrEmpty(description))
                AddToDictionary("description", null);
            else
                AddToDictionary("description", description);

            string dateFormat = "ddd MMM dd HH:mm:ss UTC yyyy";
            try
            {
                DateTime created_at = DateTime.ParseExact(_SerializedInfo.created_at, dateFormat, CultureInfo.InvariantCulture);
                AddToDictionary("created_at", created_at.ToString());
            }
            catch
            {
                AddToDictionary("created_at", null);
            }

            try
            {
                DateTime updated_at = DateTime.ParseExact(_SerializedInfo.updated_at, dateFormat, CultureInfo.InvariantCulture);
                AddToDictionary("updated_at", updated_at.ToString());
            }
            catch
            {
                AddToDictionary("updated_at", null);
            }

            string overview = _SerializedInfo.overview;
            if(string.IsNullOrEmpty(overview))
                AddToDictionary("overview", null);
            else
                AddToDictionary("overview", overview.Replace(@"<p>", "").Replace(@"</p>","").Replace("\r","").Replace("\n", ""));

            if (_SerializedInfo.image == null)
                AddToDictionary("image",null);
            else
                AddToDictionary("image", _SerializedInfo.image.available_sizes[0][1]);

            if (_SerializedInfo.image == null || string.IsNullOrEmpty(_SerializedInfo.image.attribution))
                AddToDictionary("attribution", null);
            else
                AddToDictionary("attribution", _SerializedInfo.image.attribution.Replace("<p>", "").Replace("</p>", ""));

            string total_money_raised = _SerializedInfo.total_money_raised;
            if(string.IsNullOrEmpty(total_money_raised))
                AddToDictionary("total_money_raised",null);
            else
                AddToDictionary("total_money_raised", total_money_raised);

            string acquisition = _SerializedInfo.acquisition;
            if(string.IsNullOrEmpty(acquisition))
                AddToDictionary("acquisition", null);
            else
                AddToDictionary("acquisition", acquisition);

            string ipo = _SerializedInfo.ipo;
            if (string.IsNullOrEmpty(ipo))
                AddToDictionary("ipo", null);
            else
                AddToDictionary("ipo", ipo);
        }

        public static CompanyInfo ParseCompanyInfo(Company CompanyObject)
        {
            return (new CompanyInfo(CompanyObject));
        }

        private void AddToDictionary(string Key, string Value)
        {
            _companyInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_companyInfo.ContainsKey(Key))
                return _companyInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_companyInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _companyInfo;
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
