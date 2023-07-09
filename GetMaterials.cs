using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;


namespace Optellix_Assignment
{
    [Transaction(TransactionMode.ReadOnly)]
    internal class GetMaterials : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            FilteredElementCollector fec = new FilteredElementCollector(doc, doc.ActiveView.Id).WhereElementIsNotElementType();
            List<Element> materials = fec.ToList();
            StringBuilder sb = new StringBuilder();
           
            
            foreach (Element elem in materials)
            {
                Parameter Volumeprameter = elem.LookupParameter("Volume");
                //string ElementVolume = Volumeprameter.HasValue.ToString();
                string familyCategory=null;
                string familyName=null;
                ICollection<ElementId> materialids =elem.GetMaterialIds(false);
                
                foreach(ElementId ids in materialids)
                {
                    Element material = doc.GetElement(ids);
                    double MaterialArea = elem.GetMaterialArea(ids, false);
                    double MaterialVolume = elem.GetMaterialVolume(ids);
                    FamilyInstance familyInstance = elem as FamilyInstance;
                    if(familyInstance == null)
                    {
                        familyName = elem.Name;
                        familyCategory = elem.Category.Name;
                    }
                    
                    if(familyInstance!= null)
                    {
                        FamilySymbol familySymbol = familyInstance.Symbol;
                        familyCategory = familySymbol.Family.FamilyCategory.Name;
                        familyName = familySymbol.Family.Name;
                       
                    }
                    string materialInfo = string.Format("Category: {0}, Family: {1}, Volume: {5}, Material: {2}, Material Area: {3}, Material Volume: {4}", familyCategory, familyName, material.Name,MaterialArea,MaterialVolume,Volumeprameter);
                    sb.AppendLine(materialInfo);
                }

            }

            TaskDialog.Show("Materials", sb.ToString());
            return Result.Succeeded;
        }
    }
}
