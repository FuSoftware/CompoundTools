using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    class CFBHierarchyBuilder
    {
        class CFBElement
        {
            CFBDirectoryEntry Entry;
            List<CFBElement> Children;
        }

        CFBDirectoryEntry Root;

        public CFBHierarchyBuilder(CFBFile File)
        {
            Root = ParseEntry(0, File);
        }

        public static CFBDirectoryEntry ParseEntry(int SecID, CFBFile File)
        {
            return new CFBDirectoryEntry(new CFBStream(SecID, File), File);
        }
    }
}
