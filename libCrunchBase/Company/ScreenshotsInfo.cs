using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrunchBase.Company
{
    public class ScreenshotsInfo
    {
        private dynamic _SerializedScreenshotInfo;
        private Dictionary<string, string> _screenshotInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private ScreenshotsInfo(dynamic Screenshot)
        {
            _SerializedScreenshotInfo = Screenshot;
            PopulateScreenshotsInfo();
            _Keys = GetKeys();
        }

        public static ScreenshotsInfo[] ParseScreenshotsInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int screenshot_array_length;
            try
            {
                screenshot_array_length = _SerializedInfo.screenshots.Count;
            }
            catch
            {
                return null;
            }
            List<ScreenshotsInfo> sInfo = new List<ScreenshotsInfo>();
            for (int i = 0; i < screenshot_array_length; i++)
            {
                sInfo.Add(new ScreenshotsInfo(_SerializedInfo.screenshots[i]));
            }
            return sInfo.ToArray();
        }

        private void PopulateScreenshotsInfo()
        {
            if (string.IsNullOrEmpty(_SerializedScreenshotInfo.available_sizes[0][1]))
                AddToDictionary("screenshot", null);
            else
                AddToDictionary("screenshot", _SerializedScreenshotInfo.available_sizes[0][1]);

            if (string.IsNullOrEmpty(_SerializedScreenshotInfo.attribution))
                AddToDictionary("attribution", null);
            else
                AddToDictionary("attribution", _SerializedScreenshotInfo.attribution);
        }

        private void AddToDictionary(string Key, string Value)
        {
            _screenshotInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_screenshotInfo.ContainsKey(Key))
                return _screenshotInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_screenshotInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _screenshotInfo;
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
