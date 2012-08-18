using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrunchBase.Company
{
    public class ProductInfo
    {
        private dynamic _SerializedProductInfo;
        private Dictionary<string, string> _productInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private ProductInfo(dynamic Product)
        {
            _SerializedProductInfo = Product;
            PopulateProductInfo();
            _Keys = GetKeys();
        }

        public static ProductInfo[] ParseProductInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int product_array_length;
            try
            {
                product_array_length = _SerializedInfo.products.Count;
            }
            catch
            {
                return null;
            }
            List<ProductInfo> rInfo = new List<ProductInfo>();
            for (int i = 0; i < product_array_length; i++)
            {
                rInfo.Add(new ProductInfo(_SerializedInfo.products[i]));
            }
            return rInfo.ToArray();
        }

        private void PopulateProductInfo()
        {
            if (_SerializedProductInfo.name == null)
                AddToDictionary("name", null);
            else
                AddToDictionary("name", _SerializedProductInfo.name);

            if (string.IsNullOrEmpty(_SerializedProductInfo.permalink))
                AddToDictionary("permalink", null);
            else
                AddToDictionary("permalink", _SerializedProductInfo.permalink);

            if (string.IsNullOrEmpty(_SerializedProductInfo.image))
            {
                AddToDictionary("image", null);
                AddToDictionary("attribution", null);
            }
            else
            {
                AddToDictionary("image", _SerializedProductInfo.image.available_sizes[0][1]);
                AddToDictionary("attribution", _SerializedProductInfo.image.attribution);
            }
        }

        private void AddToDictionary(string Key, string Value)
        {
            _productInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_productInfo.ContainsKey(Key))
                return _productInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_productInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _productInfo;
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
