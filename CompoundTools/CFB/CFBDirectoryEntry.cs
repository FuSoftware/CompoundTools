using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    public class CFBDirectoryEntry
    {
        public string Name { get; set; }
        public ushort NameBufferSize { get; set; }
        public EntryType Type { get; set; }
        public byte NodeColor { get; set; }
        public int DirIDLeftChild { get; set; }
        public int DirIDRightChild { get; set; }
        public int DirIDRoot { get; set; }
        public string UID { get; set; }
        public uint Flags { get; set; }
        public ulong CreationTime { get; set; }
        public ulong ModificationTime { get; set; }
        public int FirstSecID { get; set; }
        public uint StreamSize { get; set; }
        public int DirID { get; set; }
        public bool ShortStream
        {
            get
            {
                return this.StreamSize > 0 && this.StreamSize < this.File.Header.StreamMinSize;
            }
        }

        private CFBFile File;

        public enum EntryType
        {
            Empty = 0x00,
            UserStorage= 0x01,
            UserStream = 0x02,
            LockBytes = 0x03,
            Property = 0x04,
            RootStorage = 0x05
        }

        public static int GetPos(int DirID)
        {
            return DirID * 128;
        }

        public CFBDirectoryEntry(int DirID, CFBFile file)
        {
            this.DirID = DirID;
            this.Parse(file.RootStream.Data.Skip(GetPos(DirID)).ToArray(), file);
        }

        void Parse(byte[] data, CFBFile file)
        {
            this.NameBufferSize = BitConverter.ToUInt16(data, 64);
            this.Name = Encoding.Unicode.GetString(data.Take(NameBufferSize-2).ToArray());

            this.Type = (EntryType)data[66];
            this.NodeColor = data[67];
            this.DirIDLeftChild = BitConverter.ToInt32(data, 68);
            this.DirIDRightChild = BitConverter.ToInt32(data, 72);
            this.DirIDRoot = BitConverter.ToInt32(data, 76);
            this.UID = Encoding.Unicode.GetString(data.Skip(80).Take(16).ToArray());
            this.Flags = BitConverter.ToUInt32(data, 96);
            this.CreationTime = BitConverter.ToUInt64(data, 100);
            this.ModificationTime = BitConverter.ToUInt64(data, 108);
            this.FirstSecID = BitConverter.ToInt32(data, 116);
            this.StreamSize = BitConverter.ToUInt32(data, 120);

            this.File = file;
        }
    }
}
