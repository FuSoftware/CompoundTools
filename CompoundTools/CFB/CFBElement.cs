using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CompoundTools.CFB
{
    class CFBElement
    {
        public CFBDirectoryEntry Entry { get; set; }
        public List<CFBElement> Children = new List<CFBElement>();
        public List<byte> Data = new List<byte>();

        private CFBFile File { get; set; }

        public CFBElement(CFBDirectoryEntry Entry, CFBFile File)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            this.Entry = Entry;
            this.File = File;
            if (this.Entry.FirstSecID > 0)
            {
                if (this.Entry.ShortStream)
                {

                }
                else
                {
                    this.Data = GetData(this, File).ToList();
                }
            } 
            sw.Stop();
            Console.WriteLine("Loaded node {2} ({0}) of {3} bytes in {1}ms {4}kb/s", Entry.DirID, sw.ElapsedMilliseconds, Entry.Name, this.Data.Count, this.Data.Count/(sw.ElapsedMilliseconds>0?sw.ElapsedMilliseconds:1));
        }

        public int TotalChildrenCount()
        {
            int n = this.Children.Count;

            foreach (CFBElement e in Children) n += e.TotalChildrenCount();

            return n;
        }

        bool HashChild(int DirID)
        {
            return Children.Select(x => x.Entry.DirID).Contains(DirID);
        }

        public static IEnumerable<byte> GetData(CFBElement Element, CFBFile File)
        {
            return CFBStream.ParseStream(Element.Entry.FirstSecID, File);
        }

        public static CFBElement ParseHierarchy(int DirID, CFBFile File, CFBElement Parent = null)
        {
            CFBDirectoryEntry Root = new CFBDirectoryEntry(DirID, File);
            CFBElement Elem = new CFBElement(Root, File);
            if(Parent != null) Parent.Children.Add(Elem);

            if (Root.DirIDLeftChild >= 0)
            {
                ParseHierarchy(Root.DirIDLeftChild, File, Parent);
            }

            if (Root.DirIDRightChild >= 0)
            {
                ParseHierarchy(Root.DirIDRightChild, File, Parent);
            }

            if (Root.DirIDRoot >= 0)
            {
                ParseHierarchy(Root.DirIDRoot, File, Elem);
            }

            return Elem;
        }

        public static string PrintableHierarchy(CFBElement Elem, string ParentID = "")
        {
            string result = "";
            string ID = ParentID + "." + Elem.Entry.DirID.ToString();
            result = ID  + " : " + Elem.Entry.Name + "\n";

            foreach(CFBElement Child in Elem.Children)
            {
                result += PrintableHierarchy(Child, ID);
            }

            return result;
        }
    }
}
