using System;
using System.Runtime.InteropServices;
using System.Text;
using OurFractal.FFI;

namespace OurFractal
{
    /// <summary>
    /// Our Fractal Definition
    /// </summary>
    public class Definition: IDisposable
    {
        #region DLLImport
        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "make_definition")]
        static extern IntPtr CreateDef(IntPtr managerPtr, uint tag);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "destroy_definition")]
        static extern void DestroyDef(IntPtr defPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "get_def")]
        static extern FFIDef GetDef(IntPtr defPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "get_def_name")]
        static extern string GetName(IntPtr defPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "get_def_exp")]
        static extern string GetExp(IntPtr defPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "set_def_exp")]
        static extern void SetExp(IntPtr defPtr, byte[] exp);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "add_def_children")]
        static extern void AddDefChildren(IntPtr defPtr, uint tag);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "get_def_children")]
        static extern string GetDefChildren(IntPtr defPtr);
        #endregion DLLImport

        private IntPtr defPtr;
        private uint tag;
        private string name;
        private DataType dataType;
        private bool isMultiple;

        public Definition(IntPtr managerPtr, uint tag)
        {
            this.defPtr = CreateDef(managerPtr, tag);
            if (defPtr == IntPtr.Zero)
            {
                throw new Exception("Fail to get definition pointer.");
            }
            FFIDef ffiDef = GetDef(defPtr);
            this.tag = ffiDef.tag;
            this.dataType = ffiDef.dataType;
            this.isMultiple = ffiDef.isMultiple;
        }

        public void Dispose()
        {
            DestroyDef(defPtr);
        }

        /// <summary>
        /// Tag of definition.
        /// </summary>
        public uint Tag
        {
            get
            {
                return this.tag;
            }
        }

        /// <summary>
        /// Name of definition.
        /// </summary>
        public string Name
        {
            get
            {
                return FFIConverter.GetCString(GetName(defPtr));
            }
        }

        public DataType DataType
        {
            get
            {
                return dataType;
            }
        }

        /// <summary>
        /// Explanation of definition.
        /// </summary>
        public string Explanation
        {
            get
            {
                return FFIConverter.GetCString(GetExp(defPtr));
            }
            set
            {
                SetExp(defPtr, FFIConverter.SetCString(value));
            }
        }

        /// <summary>
        /// Children in definition.
        /// </summary>
        public string[] Children
        {
            get
            {
                var childStr = FFIConverter.GetCString(GetDefChildren(defPtr));
                if (string.IsNullOrWhiteSpace(childStr))
                {
                    return null;
                }
                return childStr.Split(' ');
            }
        }

        /// <summary>
        /// Does definition has child this tag.
        /// </summary>
        /// <param name="tag"> tag </param>
        /// <returns> Y/N </returns>
        public bool HasChild(string tag)
        {
            if (Children == null)
            {
                return false;
            }
            foreach (var child in Children)
            {
                if (child == tag)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Does definition has child this tag.
        /// </summary>
        /// <param name="tag"> tag </param>
        /// <returns> Y/N </returns>
        public bool HasChild(uint tag)
        {
            return HasChild(tag.ToString("X8"));
        }

        /// <summary>
        /// Add children in definition.
        /// </summary>
        /// <param name="def"> Append child. </param>
        public void AddChildren(Definition def)
        {
            if (!HasChild(def.tag))
            {
                AddDefChildren(defPtr, def.tag);
            }
        }

        /// <summary>
        /// Tag formatted  (xxxx,xxxx) style.
        /// </summary>
        /// <returns> formatted tag. </returns>
        public string ShowTag()
        {
            string tagStr = tag.ToString("X8");
            return $"({tagStr.Substring(0, 4)},{tagStr.Substring(4, 4)})";
        }
    }
}
