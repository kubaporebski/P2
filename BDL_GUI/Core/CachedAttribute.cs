using System;

namespace BDL_GUI.Core
{
    public class CachedAttribute : Attribute
    {
        public ResultList SavedData { get; set; }

        public DateTime LastAccessTime { get; set; } = DateTime.MinValue;

        public int TimeoutSeconds { get; set; } = 60;

        public CachedAttribute()
        {
            // TODO !!!
            System.Diagnostics.Debug.Write("CachedAttribute konstruktor");
        }

        /// <summary>
        /// Czy można pobrać zapamiętane dane z cache?
        /// </summary>
        public bool Valid
        {
            get
            {
                var diff = DateTime.Now - LastAccessTime;
                return diff.TotalSeconds <= TimeoutSeconds;
            }
        }

        /// <summary>
        /// Pobranie danych: albo zapamiętanych w cache albo świeżych.
        /// </summary>
        /// <returns></returns>
        public ResultList GetData()
        {
            var diff = DateTime.Now - LastAccessTime;
            if (diff.Seconds > TimeoutSeconds)
                return null;

            return SavedData;
        }
    }
}