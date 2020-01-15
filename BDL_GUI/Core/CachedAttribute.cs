using System;
using System.Collections.Generic;
using System.Reflection;

namespace BDL_GUI.Core
{
    /// <summary>
    /// Niektóre dane możemy swobodnie przechowywać w cache.
    /// Dotyczy to np. jednostek terytorialnych albo jednostek miar. 
    /// </summary>
    public class CachedAttribute : Attribute
    {
        private ResultList data;
        
        private DateTime lastWriteTime = DateTime.MinValue;

        /// <summary>
        /// Czas ważności (sekundy). Domyślnie 300 sekund, co równoważne jest pięciu minutom.
        /// </summary>
        public uint TimeoutSeconds { get; set; } = 300;

        /// <summary>
        /// Czy można pobrać zapamiętane dane z cache?
        /// </summary>
        public bool Valid
        {
            get
            {
                var diff = DateTime.Now - lastWriteTime;
                return diff.TotalSeconds <= TimeoutSeconds;
            }
        }

        /// <summary>
        /// Pobranie lub zapisanie danych do cache.
        /// </summary>
        public ResultList Data
        {
            get => data;

            set
            {
                lastWriteTime = DateTime.Now;
                data = value;
            }
        }
    }
}