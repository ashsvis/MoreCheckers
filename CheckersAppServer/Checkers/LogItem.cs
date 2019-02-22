using System;
using System.Collections.Generic;

namespace Checkers
{
    [Serializable]
    public class LogItem
    {
        private List<object> _semiSteps;

        /// <summary>
        /// Номер по порядку
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Ход белых
        /// </summary>
        public string White { get; set; }

        /// <summary>
        /// Ход чёрных
        /// </summary>
        public string Black { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object GetLastMap()
        {
            return _semiSteps.Count > 0 ? _semiSteps[_semiSteps.Count - 1] : null;
        }

        public IEnumerable<object> GetMapSemiSteps()
        {
            return _semiSteps;
        }

        public void AddToMap(object value)
        {
            _semiSteps.Add(value);
        }

        public LogItem()
        {
            _semiSteps = new List<object>();
        }

        /// <summary>
        /// Текстовое представления
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}. {1}\t{2}", Number, White, Black);
        }
    }
}
