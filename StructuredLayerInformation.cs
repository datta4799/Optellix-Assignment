using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optellix_Assignment
{
    [Transaction(TransactionMode.ReadOnly)]
    internal class StructuredLayerInformation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            Structured_Layer_Info SLI = new Structured_Layer_Info(doc);
            SLI.Show();

            return Result.Succeeded;
        }
    }
}
