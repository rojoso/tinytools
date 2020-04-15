
//————————————————
//版权声明：本文为CSDN博主「万里归来少年心」的原创文章，遵循 CC 4.0 BY - SA 版权协议，转载请附上原文出处链接及本声明。
//原文链接：https://blog.csdn.net/liyazhen2011/java/article/details/88825836

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


namespace DrawBeams
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Programs: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            //Document doc = commandData.Application.ActiveUIDocument.Document;
            //UIDocument uidoc = commandData.Application.ActiveUIDocument;
            //Selection selection = uidoc.Selection;
            ////选择楼板
            //var reference_floor = selection.PickObject(ObjectType.PointOnElement, new FloorFaceFilter(doc),"请选择楼板");
            //Element elem_floor = doc.GetElement(reference_floor);
            //Floor thefloor = elem_floor as Floor;

            ////选择模型线
            //var reference_curve = selection.PickObject(ObjectType.Element,  "请选择模型线");
            //ModelCurve elem_curve = doc.GetElement(reference_curve) as ModelCurve;

            //Curve thecurve = elem_curve.GeometryCurve;

            ////拿到第一种梁类型
            //FilteredElementCollector collector = new FilteredElementCollector(doc);
            //collector.OfCategory(BuiltInCategory.OST_StructuralFraming);
            //FamilySymbol familySymbol = null;
            //foreach (var item in collector)
            //{
            //    familySymbol = item as FamilySymbol;
            //    if (familySymbol != null)
            //        break;
            //}

            ////拿到当前的标高
            //ElementId level_id = thefloor.get_Parameter(BuiltInParameter.SCHEDULE_LEVEL_PARAM).AsElementId();

            //Level level = doc.GetElement(level_id) as Level;
            //if (level == null)
            //{
            //    TaskDialog.Show("错误", "不是PlainView");
            //    return Result.Failed;
            //}

            ////拿到模型线到楼板的投影线
            //Curve curve_projection = FindFloorcurve(thecurve, thefloor);

            ////创建梁
            //using (Transaction tr = new Transaction(doc))
            //{
            //    tr.Start("Create beam");
            //    if (!familySymbol.IsActive)
            //        familySymbol.Activate();
            //    doc.Create.NewFamilyInstance(curve_projection, familySymbol, level, Autodesk.Revit.DB.Structure.StructuralType.Beam);
            //    tr.Commit();
            //}

            Controlers controllor = new Controlers(new PickForm(),commandData);
            controllor.pView.Show();
            return Result.Succeeded;


        }

        
    }
}
