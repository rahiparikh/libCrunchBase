using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrunchBase.Company
{
    public class CompetitorInfo
    {
        		private dynamic _SerializedCompetitorInfo;
		private Dictionary<string, string> _competitorInfo = new Dictionary<string, string>();
		private List<string> _Keys;

        private CompetitorInfo(dynamic Competitor)
		{
            _SerializedCompetitorInfo = Competitor;
			PopulateCompetitorInfo();
			_Keys = GetKeys();
		}

		public static CompetitorInfo[] ParseCompetitorInfo(Company CompanyObject)
		{
			dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
			int competitor_array_length;
			try
			{
                competitor_array_length = _SerializedInfo.competitions.Count;
			}
			catch
			{
				return null;
			}
			List<CompetitorInfo> cInfo = new List<CompetitorInfo>();
			for (int i = 0; i < competitor_array_length; i++)
			{
                cInfo.Add(new CompetitorInfo(_SerializedInfo.competitions[i]));
			}
			return cInfo.ToArray();
		}

		private void PopulateCompetitorInfo()
		{
			if(_SerializedCompetitorInfo.competitor.name == null)
				AddToDictionary("name", null);
			else
				AddToDictionary("name", _SerializedCompetitorInfo.competitor.name);

			if(string.IsNullOrEmpty(_SerializedCompetitorInfo.competitor.permalink))
				AddToDictionary("permalink", null);
			else
				AddToDictionary("permalink", _SerializedCompetitorInfo.competitor.permalink);

			if(_SerializedCompetitorInfo.competitor.image == null)
			{
				AddToDictionary("image", null);
				AddToDictionary("attribution", null);
			}
			else
			{
				AddToDictionary("image", _SerializedCompetitorInfo.competitor.image.available_sizes[0][1]);
				AddToDictionary("attribution",  _SerializedCompetitorInfo.competitor.image.attribution);
			}
		}

		private void AddToDictionary(string Key, string Value)
		{
			_competitorInfo.Add(Key, Value);
		}

		public string GetValue(string Key)
		{
			if (_competitorInfo.ContainsKey(Key))
				return _competitorInfo[Key];
			else
				return null;
		}

		public List<string> GetKeys()
		{
			return new List<string>(_competitorInfo.Keys);
		}

		public Dictionary<string, string> GetKeyValuePair()
		{
			return _competitorInfo;
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
