using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using System.Windows.Forms;

namespace tinyTools
{
    [Transaction(TransactionMode.Manual)]
    public class Programs:IExternalCommand
    {
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Selection selection = uidoc.Selection;

            if(selection.GetElementIds().Count() != 1)
            {
                //selection.Dispose();

                var reference = selection.PickObject(ObjectType.PointOnElement, "请选择楼板");
                Element elem = doc.GetElement(reference);
                if(elem != null)
                {
                    TaskDialog.Show("报告楼板坡度", "板的坡度为"+ elem.get_Parameter(BuiltInParameter.ROOF_SLOPE).AsDouble().ToString());

                    TaskDialog.Show("计算角度", FloorTools.FindFloorslope(elem as Floor).ToString());
                    
                    return Result.Succeeded;
                }
                else
                {
                    return Result.Cancelled;
                }
            }
            else
            {
                ElementId id = selection.GetElementIds().First<ElementId>();
                Element elem1 = doc.GetElement(id);
                if (elem1 is Floor)
                {
                    TaskDialog.Show("报告楼板坡度", "板的坡度为"+ elem1.get_Parameter(BuiltInParameter.ROOF_SLOPE).AsDouble().ToString());
                    return Result.Succeeded;
                }
                else
                {
                    var reference = selection.PickObject(ObjectType.Element, new FloorFaceFilter(doc), "请选择楼板");
                    Element elem2 = doc.GetElement(reference);
                    if (elem2 != null)
                    {
                        TaskDialog.Show("报告楼板坡度", "板的坡度为" + elem2.get_Parameter(BuiltInParameter.ROOF_SLOPE).AsDouble().ToString());
                        return Result.Succeeded;
                    }
                    else
                    {
                        return Result.Cancelled;
                    }
                }
            }
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(true);
            //Controllor controllor = new Controllor(new PersonForm());
            //Application.Run(controllor.pView);


            
        }

    }
}
