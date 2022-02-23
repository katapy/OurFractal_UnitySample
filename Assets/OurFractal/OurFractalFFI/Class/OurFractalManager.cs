using System;
using System.Runtime.InteropServices;
using System.Text;

namespace OurFractal
{
    public class OurFractalManager : IDisposable
    {
        #region DllImport
        [DllImport("libour_fractal_ffi.dylib", CharSet = CharSet.Unicode, EntryPoint = "make_manager")]
        static extern IntPtr MakeManager(byte[] filePath, byte[] tableName, byte[] dataName);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "destroy_manager")]
        static extern void DestroyManager(IntPtr managerPtr);

        [DllImport("libour_fractal_ffi.dylib", CharSet = CharSet.Unicode, EntryPoint = "add_definition")]
        static extern bool AddDefinition(IntPtr managerPtr, uint tag, byte[] name, DataType dataType, bool isMultiple);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "get_def_list")]
        static extern string GetDefList(IntPtr managerPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "write_def")]
        static extern bool WriteDef(IntPtr managerPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "read_def")]
        static extern bool ReadDef(IntPtr managerPtr);
        #endregion DllImport

        private IntPtr ptr;

        public OurFractalManager(string filePath, string tableName, string dataName)
        {
            this.ptr = MakeManager(
                Encoding.UTF8.GetBytes(filePath),
                Encoding.UTF8.GetBytes(tableName),
                Encoding.UTF8.GetBytes(dataName));
        }

        public void Dispose()
        {
            DestroyManager(this.ptr);
        }

        public string[] DefList
        {
            get
            {
                return GetDefList(this.ptr).Split(' ');
            }
        }

        public bool AddDef(uint tag, string name, DataType dataType, bool isMultiple)
        {
            return AddDefinition(this.ptr, tag, Encoding.UTF8.GetBytes(name), dataType, isMultiple);
        }

        public Definition GetDefinition(uint tag)
        {
            return new Definition(this.ptr, tag);
        }


        public bool WriteDef()
        {
            return WriteDef(ptr);
        }

        public bool ReadDef()
        {
            return ReadDef(ptr);
        }
    }
}
