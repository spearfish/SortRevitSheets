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
using System.Text.RegularExpressions;
using DBLibrary;

namespace SortSheets
{
    class Program
    {
        public void SortSheets(Document doc)
        {
            LibraryGetItems lib = new LibraryGetItems();
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(ViewSheet));
            List<ViewSheet> sheets = new List<ViewSheet>();
            foreach (ViewSheet vs in collector)
                sheets.Add(vs);

            sheets.RemoveAll(x => x.SheetNumber.StartsWith("Z") && x.SheetNumber.StartsWith("Q"));
            sheets.RemoveAll(x => x.get_Parameter(BuiltInParameter.RBS_PANEL_SCHEDULE_SHEET_APPEARANCE_PARAM).AsValueString() == "No");

            var RegexPatt = @"([0-9]|[^-.S])";
            int sortNumber = 0;
            foreach (ViewSheet s in sheets)
            {
                var sheetNumRegx = Regex.Match(s.SheetNumber, RegexPatt, RegexOptions.None);
                string sheetNumber = sheetNumRegx.ToString();

                char[] c = sheetNumber.ToCharArray();
                int Value = 1000;
                while (Value > 0)
                {
                    for (int i = 0; i <= c.Length; i++)
                    {
                        string st = c[i].ToString();
                        bool check = checkNumber(st);
                        if (check == true)
                        {
                            int intergerValue = Convert.ToInt32(c[i]);
                            sortNumber = sortNumber + (intergerValue * Value);
                        }
                        if (check == false)
                        {
                            int x = letterValue(st, Value);
                            sortNumber = sortNumber + (x * Value);
                        }
                        Value = Value - 100;
                    }
                }       
            }
        }

        public bool checkNumber(string s)
        {
            bool result = false;
            int n;
            if (int.TryParse(s, out n))
            {
                result = true;
            }
            return result;
        }

        public int letterValue(string s, int v)
        {
            int result = 0;
            switch (s)
            {
                case "A":
                    result = result - 1;
                    break;
                case "B":
                    result = result - 2;
                    break;
                case "C":
                    result = result - 3;
                    break;
                case "D":
                    result = result - 4;
                    break;
                case "F":
                    result = result - 1;
                    break;
                case "P":
                    result = result - 2;
                    break;
                case "R":
                    result = result - 3;
                    break;
            }
            return result;
        }
    }
}
