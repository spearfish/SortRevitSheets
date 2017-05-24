using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using DBLibrary;

namespace SortSheets
{
    class Program
    {
        public void SortSheets(Document doc)
        {
            LibraryGetItems lib = new LibraryGetItems();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet));

            foreach (ViewSheet vs in collector)
            {
                string number = vs.SheetNumber;
            }

        }
    }
}
