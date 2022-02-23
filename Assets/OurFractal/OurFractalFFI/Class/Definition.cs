using System;
using System.Runtime.InteropServices;
using System.Text;
using OurFractal.FFI;

namespace OurFractal
{
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

        [DllImport("libour_fractal_ffi.dylib", CharSet = CharSet.Unicode, EntryPoint = "set_def_exp")]
        static extern void SetExp(IntPtr defPtr, byte[] exp);
        #endregion DLLImport

        private IntPtr defPtr;
        private uint tag;
        private string name;
        private DataType dataType;
        private bool isMultiple;

        public Definition(IntPtr managerPtr, uint tag)
        {
            this.defPtr = CreateDef(managerPtr, tag);
            FFIDef ffiDef = GetDef(defPtr);
            this.tag = ffiDef.tag;
            this.dataType = ffiDef.dataType;
            this.isMultiple = ffiDef.isMultiple;
        }

        public void Dispose()
        {
            DestroyDef(defPtr);
        }

        public uint Tag
        {
            get
            {
                return this.tag;
            }
        }

        public string Name
        {
            get
            {
                return GetName(defPtr);
            }
        }

        public string Explanation
        {
            get
            {
                return GetExp(defPtr);
            }
            set
            {
                SetExp(defPtr, Encoding.UTF8.GetBytes(value));
            }
        }

        public string ShowTag()
        {
            string tagStr = tag.ToString("X8");
            return $"({tagStr.Substring(0, 4)},{tagStr.Substring(4, 4)})";
        }
    }
}
