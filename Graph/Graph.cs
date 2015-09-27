using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public static partial class Graph
    {
        #region Helper
        public static TV GetOrCreate<TK, TV>(this IDictionary<TK, TV> dict, TK key)
            where TV : class, new()
        {
            TV res;
            if (dict.TryGetValue(key, out res))
            {
                return res;
            }
            res = new TV();
            dict.Add(key, res);
            return res;
        }
        #endregion
        
       
       
    }
   
}
