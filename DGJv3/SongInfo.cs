﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Collections.Generic;

namespace DGJv3
{
    public class SongInfo : INotifyPropertyChanged
    {
        [JsonIgnore]
        public SearchModule Module;

#pragma warning disable CS0067 // 从不使用事件“SongInfo.PropertyChanged”
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS0067 // 从不使用事件“SongInfo.PropertyChanged”

        [JsonProperty("smid")]
        public string ModuleId { get; set; }

        [JsonProperty("siid")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sing")]
        public string[] Singers { get; set; }
        [JsonIgnore]
        public string SingersText { get => string.Join(";", Singers); }

        /// <summary>
        /// Lyric存储的是这个歌曲的歌词文件，为null时，会认为是延迟获取，在下载歌曲时再通过接口尝试获取lrc
        /// </summary>
        [JsonProperty("lrc")]
        public string Lyric { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("extinfo")]
        public IDictionary<string,string> ExtInfo { get; set; }

        [JsonConstructor]
        private SongInfo() { }

        public SongInfo(SearchModule module) : this(module, string.Empty, string.Empty, null) { }
        public SongInfo(SearchModule module, string id, string name, string[] singers) : this(module, id, name, singers, null) { }
        public SongInfo(SearchModule module, string id, string name, string[] singers, string lyric) : this(module, id, name, singers, lyric, string.Empty) { }
        public SongInfo(SearchModule module, string id, string name, string[] singers, string lyric, string note) : this(module, id, name, singers, lyric, string.Empty, new Dictionary<string, string>() { }) { }
        public SongInfo(SearchModule module, string id, string name, string[] singers, string lyric, string note,IDictionary<string,string> info)
        {
            Module = module;
            ModuleId = Module.UniqueId;
            Id = id;
            Name = name;
            Singers = singers;
            Lyric = lyric;
            Note = note;
            ExtInfo = info;
        }
        public string GetInfo(string key)
        {
            if (TryGetInfo(key, out string value))
            {
                return value;
            }
            return null;
        }
        public void SetInfo(string key, string value)
        {
            ExtInfo[key] = value;
        }

        public bool TryGetInfo(string key, out string value)
        {
            return ExtInfo.TryGetValue(key, out value);
        }
    }
}
