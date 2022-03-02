using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace OurFractal.FFI
{
    /// <summary>
    /// Converter for use FFI function.
    /// </summary>
    public static class FFIConverter
    {
        /// <summary>
        /// Get string in C.
        /// </summary>
        /// <param name="s"> Get value(C). </param>
        /// <returns> Get Value(C#) </returns>
        public static string GetCString(string s)
        {
            return s.TrimEnd('\0');
        }

        /// <summary>
        /// Set string by C.
        /// </summary>
        /// <param name="s"> Set value(C#). </param>
        /// <returns> Set value(C) </returns>
        public static byte[] SetCString(string s)
        {
            return Encoding.UTF8.GetBytes(s + '\0');
        }
    }
}