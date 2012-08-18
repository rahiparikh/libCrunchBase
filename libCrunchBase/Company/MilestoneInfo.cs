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
    public class MilestoneInfo
    {
        private dynamic _SerializedMilestoneInfo;
        private Dictionary<string, string> _milestoneInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private MilestoneInfo(dynamic Milestone)
        {
            _SerializedMilestoneInfo = Milestone;
            PopulateMilestonesInfo();
            _Keys = GetKeys();
        }

        public static MilestoneInfo[] ParseMilestonesInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int milestones_array_length;
            try
            {
                milestones_array_length = _SerializedInfo.milestones.Count;
            }
            catch
            {
                return null;
            }
            List<MilestoneInfo> rInfo = new List<MilestoneInfo>();
            for(int i = 0; i < milestones_array_length ; i++)
            {
                rInfo.Add(new MilestoneInfo(_SerializedInfo.milestones[i]));
            }
            return rInfo.ToArray();
        }

        private void PopulateMilestonesInfo()
        {
            string description = _SerializedMilestoneInfo.description;
            if(string.IsNullOrEmpty(description))
                AddToDictionary("description", null);
            else
                AddToDictionary("description", description);

            try
            {
                string stoned_date = new DateTime(_SerializedMilestoneInfo.stoned_year,_SerializedMilestoneInfo.stoned_month, _SerializedMilestoneInfo.stoned_day).ToShortDateString();
                if (string.IsNullOrEmpty(stoned_date))
                    AddToDictionary("stoned_date", null);
                else
                    AddToDictionary("stoned_date", stoned_date);
            }
            catch
            {
                AddToDictionary("title", null);
            }

            string source_url = _SerializedMilestoneInfo.source_url;
            if(string.IsNullOrEmpty(source_url))
                AddToDictionary("source_url", null);
            else
                AddToDictionary("source_url", source_url);

            string source_text = _SerializedMilestoneInfo.source_text;
            if(string.IsNullOrEmpty(source_text))
                AddToDictionary("source_text", null);
            else
                AddToDictionary("source_text", source_text);

            string source_description = _SerializedMilestoneInfo.source_description;
            if(string.IsNullOrEmpty(source_description))
                AddToDictionary("source_description", null);
            else
                AddToDictionary("source_description", source_description);

            string stoned_value = _SerializedMilestoneInfo.stoned_value;
            if (string.IsNullOrEmpty(stoned_value))
                AddToDictionary("stoned_value", null);
            else
                AddToDictionary("stoned_value", stoned_value);

            string stoned_value_type = _SerializedMilestoneInfo.stoned_value_type;
            if (string.IsNullOrEmpty(stoned_value_type))
                AddToDictionary("stoned_value_type", null);
            else
                AddToDictionary("stoned_value_type", stoned_value_type);

            string stoned_acquirer = _SerializedMilestoneInfo.stoned_acquirer;
            if (string.IsNullOrEmpty(stoned_acquirer))
                AddToDictionary("stoned_acquirer", null);
            else
                AddToDictionary("stoned_acquirer", stoned_acquirer);

            if (_SerializedMilestoneInfo.stoneable == null)
            {
                AddToDictionary("name", null);
                AddToDictionary("permalink", null);
            }
            else
            {
                string name = _SerializedMilestoneInfo.stoneable.stoned_acquirer;
                if (string.IsNullOrEmpty(name))
                    AddToDictionary("name", null);
                else
                    AddToDictionary("name", name);

                string permalink = _SerializedMilestoneInfo.stoneable.permalink;
                if (string.IsNullOrEmpty(permalink))
                    AddToDictionary("permalink", null);
                else
                    AddToDictionary("permalink", permalink);
            }
        }

        private void AddToDictionary(string Key, string Value)
        {
            _milestoneInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_milestoneInfo.ContainsKey(Key))
                return _milestoneInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_milestoneInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _milestoneInfo;
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
