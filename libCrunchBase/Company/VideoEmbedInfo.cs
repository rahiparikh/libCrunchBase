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
    public class VideoEmbedInfo
    {
        private dynamic _SerializedVideoEmbedInfo;
        private Dictionary<string, string> _videoEmbedInfo = new Dictionary<string, string>();
        private List<string> _Keys;

        private VideoEmbedInfo(dynamic VideoEmbed)
        {
            _SerializedVideoEmbedInfo = VideoEmbed;
            PopulateVideoEmbedsInfo();
            _Keys = GetKeys();
        }

        public static VideoEmbedInfo[] ParseVideoEmbedsInfo(Company CompanyObject)
        {
            dynamic _SerializedInfo = CompanyObject.GetSerializedInfo();
            int video_embeds_array_length = _SerializedInfo.video_embeds.Count;
            List<VideoEmbedInfo> rInfo = new List<VideoEmbedInfo>();
            for(int i = 0; i < video_embeds_array_length ; i++)
            {
                rInfo.Add(new VideoEmbedInfo(_SerializedInfo.video_embeds[i]));
            }
            return rInfo.ToArray();
        }

        private void PopulateVideoEmbedsInfo()
        {
            string embed_code = _SerializedVideoEmbedInfo.embed_code;
            if(string.IsNullOrEmpty(embed_code))
                AddToDictionary("embed_code", null);
            else
                AddToDictionary("embed_code", embed_code);

            string description = _SerializedVideoEmbedInfo.description;
            if (string.IsNullOrEmpty(description))
                AddToDictionary("description", description);
            else
                AddToDictionary("description", description);
        }

        private void AddToDictionary(string Key, string Value)
        {
            _videoEmbedInfo.Add(Key, Value);
        }

        public string GetValue(string Key)
        {
            if (_videoEmbedInfo.ContainsKey(Key))
                return _videoEmbedInfo[Key];
            else
                return null;
        }

        public List<string> GetKeys()
        {
            return new List<string>(_videoEmbedInfo.Keys);
        }

        public Dictionary<string, string> GetKeyValuePair()
        {
            return _videoEmbedInfo;
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
