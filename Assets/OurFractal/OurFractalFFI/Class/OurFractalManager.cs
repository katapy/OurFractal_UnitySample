using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace OurFractal
{
    /// <summary>
    /// Our Fractal Manager.
    /// </summary>
    public class OurFractalManager : IDisposable
    {
        #region DllImport
        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "make_manager")]
        static extern IntPtr MakeManager(byte[] filePath, byte[] tableName, byte[] dataName);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "destroy_manager")]
        static extern void DestroyManager(IntPtr managerPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "add_definition")]
        static extern bool AddDefinition(IntPtr managerPtr, uint tag, byte[] name, DataType dataType, bool isMultiple);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "get_def_list")]
        static extern string GetDefList(IntPtr managerPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "write_def")]
        static extern bool WriteDef(IntPtr managerPtr);

        [DllImport("libour_fractal_ffi.dylib", EntryPoint = "read_def")]
        static extern bool ReadDef(IntPtr managerPtr);
        #endregion DllImport

        private IntPtr ptr;
        private Dictionary<uint, Definition> createdDefs = null;

        public OurFractalManager(string filePath, string tableName, string dataName)
        {
            this.ptr = MakeManager(
                FFI.FFIConverter.SetCString(filePath),
                FFI.FFIConverter.SetCString(tableName),
                FFI.FFIConverter.SetCString(dataName));

            createdDefs = new Dictionary<uint, Definition>();
        }

        public void Dispose()
        {
            UnityEngine.Debug.Log("Destoy OurFractal manager");
            foreach (var def in createdDefs.Values)
            {
                def.Dispose();
            }
            DestroyManager(this.ptr);
        }

        /// <summary>
        /// Difined tag list
        /// </summary>
        public string[] DefList
        {
            get
            {
                return FFI.FFIConverter.GetCString(GetDefList(this.ptr)).Split(' ');
            }
        }

        /// <summary>
        /// Add dininition.
        /// </summary>
        /// <param name="tag">tag</param>
        /// <param name="name">name</param>
        /// <param name="dataType">data type</param>
        /// <param name="isMultiple">is multiple</param>
        /// <returns></returns>
        public bool AddDef(uint tag, string name, DataType dataType, bool isMultiple)
        {
            return AddDefinition(this.ptr, tag, FFI.FFIConverter.SetCString(name), dataType, isMultiple);
        }

        /// <summary>
        /// Get definition.
        /// </summary>
        /// <param name="tag">tag of definition.</param>
        /// <returns>definition</returns>
        public Definition GetDefinition(uint tag)
        {
            if (!HasDef(tag))
            {
                throw new Exception($"Tag {tag.ToString("X8")} is not defined");
            }

            if (!createdDefs.ContainsKey(tag))
            {
                var def = new Definition(this.ptr, tag);
                createdDefs.Add(tag, def);
            }

            return createdDefs[tag];
        }

        /// <summary>
        /// Get definition.
        /// </summary>
        /// <param name="tag">tag of definition.</param>
        /// <returns>definition</returns>
        public Definition GetDefinition(string tagStr)
        {
            uint tag = uint.Parse(tagStr,
                    System.Globalization.NumberStyles.HexNumber);
            return GetDefinition(tag);
        }

        /// <summary>
        /// Does manager has definition.
        /// </summary>
        /// <param name="tag">definition tag</param>
        /// <returns> Y/N </returns>
        public bool HasDef(string tag)
        {
            foreach (var def in DefList)
            {
                if (def == tag)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Does manager has definition.
        /// </summary>
        /// <param name="tag">definition tag</param>
        /// <returns> Y/N </returns>
        public bool HasDef(uint tag)
        {
            return HasDef(tag.ToString("X8"));
        }

        /// <summary>
        /// Write definition.
        /// </summary>
        /// <returns>Is success</returns>
        public bool WriteDef()
        {
            return WriteDef(ptr);
        }

        /// <summary>
        /// Read definition.
        /// </summary>
        /// <returns>Is success</returns>
        public bool ReadDef()
        {
            return ReadDef(ptr);
        }
    }
}
