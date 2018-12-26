using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    class CFBDirectoryEntry
    {
        string Name { get; set; }
        ushort NameBufferSize { get; set; }
        byte Type { get; set; }
        byte NodeColor { get; set; }
        uint DirIDLeftChild { get; set; }
        uint DirIDRightChild { get; set; }
        uint DirIDRoot { get; set; }
        string UID { get; set; }
        uint Flags { get; set; }
        ulong CreationTime { get; set; }
        ulong ModificationTime { get; set; }
        uint FirstSecID { get; set; }
        uint StreamSize { get; set; }

        public static uint GetPos(uint DirID)
        {
            return DirID * 128;
        }

        public CFBDirectoryEntry(CFBStream Stream, CFBFile file)
        {
            this.Parse(Stream.Data.ToArray(), file);
        }

        void Parse(byte[] data, CFBFile file)
        {
            this.Name = Encoding.Unicode.GetString(data.Take(64).ToArray());
            this.NameBufferSize = BitConverter.ToUInt16(data, 64);
            this.Type = data[66];
            this.NodeColor = data[67];
            this.DirIDLeftChild = BitConverter.ToUInt32(data, 68);
            this.DirIDRightChild = BitConverter.ToUInt32(data, 72);
            this.DirIDRoot = BitConverter.ToUInt32(data, 76);
            this.UID = Encoding.Unicode.GetString(data.Skip(80).Take(16).ToArray());
            this.Flags = BitConverter.ToUInt32(data, 96);
            this.CreationTime = BitConverter.ToUInt64(data, 100);
            this.ModificationTime = BitConverter.ToUInt64(data, 108);
            this.FirstSecID = BitConverter.ToUInt32(data, 116);
            this.StreamSize = BitConverter.ToUInt32(data, 120);
        }
    }
}
