using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrunchBase.Company
{
	public class AcquisitionInfo
	{
		private dynamic _SerializedAcquisitionInfo;
		private Dictionary<string, string> _acquisitionInfo = new Dictionary<string, string>();
		private List<string> _Keys;

		private AcquisitionInfo(dynamic Acquisition)
		{
			_SerializedAcquisitionInfo = Acquisition;
			PopulateAcquisitionInfo();
			_Keys = GetKeys();
		}

		public static AcquisitionInfo[] ParseAcquisitionInfo(Company CompanyObject)
		{
			dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
			int acquisition_array_length;
			try
			{
				acquisition_array_length = _SerializedInfo.acquisitions.Count;
			}
			catch
			{
				return null;
			}
			List<AcquisitionInfo> rInfo = new List<AcquisitionInfo>();
			for (int i = 0; i < acquisition_array_length; i++)
			{
				rInfo.Add(new AcquisitionInfo(_SerializedInfo.acquisitions[i]));
			}
			return rInfo.ToArray();
		}

		private void PopulateAcquisitionInfo()
		{
			if(_SerializedAcquisitionInfo.price_amount == null)
				AddToDictionary("price_amount", null);
			else
				AddToDictionary("price_amount", _SerializedAcquisitionInfo.price_amount);

			if(string.IsNullOrEmpty(_SerializedAcquisitionInfo.price_currency_code))
				AddToDictionary("price_currency_code", null);
			else
				AddToDictionary("price_currency_code", _SerializedAcquisitionInfo.price_currency_code);

			if(string.IsNullOrEmpty(_SerializedAcquisitionInfo.term_code))
				AddToDictionary("term_code", null);
			else
				AddToDictionary("term_code", _SerializedAcquisitionInfo.term_code);

			if(string.IsNullOrEmpty(_SerializedAcquisitionInfo.source_url))
				AddToDictionary("source_url", null);
			else
				AddToDictionary("source_url", _SerializedAcquisitionInfo.source_url);

			if(string.IsNullOrEmpty(_SerializedAcquisitionInfo.source_description))
				AddToDictionary("source_description", null);
			else
				AddToDictionary("source_description", _SerializedAcquisitionInfo.source_description);

			try
			{
                AddToDictionary("acquired_on", new DateTime(_SerializedAcquisitionInfo.acquired_year, _SerializedAcquisitionInfo.acquired_month, _SerializedAcquisitionInfo.acquired_day).ToShortDateString());
			}
			catch
			{
			    AddToDictionary("acquired_on",null);
			}
				
			
			if(_SerializedAcquisitionInfo.company.name == null)
				AddToDictionary("company_name", null);
			else
				AddToDictionary("company_name", _SerializedAcquisitionInfo.company.name);

			if(_SerializedAcquisitionInfo.company.permalink == null)
				AddToDictionary("company_permalink", null);
			else
				AddToDictionary("company_permalink", _SerializedAcquisitionInfo.company.permalink);

		    if(_SerializedAcquisitionInfo.company.image == null)
			{
				AddToDictionary("company_image", null);
				AddToDictionary("company_image_attribution", null);
			}
			else
			{
				AddToDictionary("company_image", _SerializedAcquisitionInfo.company.image.available_sizes[0][1]);
                AddToDictionary("company_attribution", _SerializedAcquisitionInfo.company.image.attribution);
			}
		}

		private void AddToDictionary(string Key, string Value)
		{
			_acquisitionInfo.Add(Key, Value);
		}

		public string GetValue(string Key)
		{
			if (_acquisitionInfo.ContainsKey(Key))
				return _acquisitionInfo[Key];
			else
				return null;
		}

		public List<string> GetKeys()
		{
			return new List<string>(_acquisitionInfo.Keys);
		}

		public Dictionary<string, string> GetKeyValuePair()
		{
			return _acquisitionInfo;
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
