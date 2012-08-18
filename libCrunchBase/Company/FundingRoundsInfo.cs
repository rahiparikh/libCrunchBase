using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrunchBase.Company
{
    public class FundingRoundsInfo
    {
        private dynamic _SerializedInfo;
        private Dictionary<string, string> _fundingRoundsInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private FundingRoundsInfo(dynamic Funding)
        {
            _SerializedInfo = Funding;
            PopulateFundingRoundsInfo();
            _Keys = GetKeys();
        }

        private void PopulateFundingRoundsInfo()
        {
            if (string.IsNullOrEmpty(_SerializedInfo.round_code))
                AddToDictionary("round_code", null);
            else
                AddToDictionary("round_code", _SerializedInfo.round_code);

            if (string.IsNullOrEmpty(_SerializedInfo.source_url))
                AddToDictionary("source_url", null);
            else
                AddToDictionary("source_url", _SerializedInfo.source_url);

            if (string.IsNullOrEmpty(_SerializedInfo.source_description))
                AddToDictionary("source_description", null);
            else
                AddToDictionary("source_description", _SerializedInfo.source_description);

            try
            {
                if (string.IsNullOrEmpty(_SerializedInfo.raised_amount.ToString()))
                    AddToDictionary("raised_amount", null);
                else
                    AddToDictionary("raised_amount", _SerializedInfo.raised_amount);
            }
            catch
            {
                AddToDictionary("raised_amount", null);
            }

            if (string.IsNullOrEmpty(_SerializedInfo.raised_currency_code))
                AddToDictionary("raised_currency_code", null);
            else
                AddToDictionary("raised_currency_code", _SerializedInfo.raised_currency_code);

            try
            {
                AddToDictionary("funded_on", new DateTime(_SerializedInfo.funded_year,_SerializedInfo.funded_month,_SerializedInfo.funded_day).ToShortDateString());
            }
            catch
            {
                AddToDictionary("funded_on", null);
            }


        }

        public static FundingRoundsInfo[] ParseFundingRoundsInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int funding_rounds_array_length;
            try
            {
                funding_rounds_array_length = _SerializedInfo.funding_rounds.Count;
            }
            catch
            {
                return null;
            }
            List<FundingRoundsInfo> frInfo = new List<FundingRoundsInfo>();
            for (int i = 0; i < funding_rounds_array_length; i++)
            {
                frInfo.Add(new FundingRoundsInfo(_SerializedInfo.funding_rounds[i]));
            }
            return frInfo.ToArray();
        }

        private void AddToDictionary(string Key, string Value)
        {
            _fundingRoundsInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_fundingRoundsInfo.ContainsKey(Key))
                return _fundingRoundsInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_fundingRoundsInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _fundingRoundsInfo;
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
