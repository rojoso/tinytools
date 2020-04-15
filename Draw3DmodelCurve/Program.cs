
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;

namespace Draw3DmodelCurve
{
    [Transaction(TransactionMode.Manual)]
    public class Program : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Selection selection = uidoc.Selection;

            XYZ point1 = new XYZ(500, 600, 700);

            XYZ point2 = new XYZ(3500, 3800, 1500);
            Curve curve = Line.CreateBound(point1, point2);

            XYZ point_in_3d;

            if (SetWorkPlaneAndPickObject(uidoc, out point_in_3d))
            {
                TaskDialog.Show("3D Point Selected",
                    "3D point picked on the plane"
                    + " defined by the selected face: "
                    + "X: " + point_in_3d.X.ToString()
                    + ", Y: " + point_in_3d.Y.ToString()
                    + ", Z: " + point_in_3d.Z.ToString());

                return Result.Succeeded;
            }
            else
            {
                message = "3D point selection failed";
                return Result.Failed;
            }

        }

        /// <summary>
        /// 选择三维空间中的某一点，使用的是先拾取面再拾取点的方式
        /// </summary>
        /// <param name="uidoc"></param>
        /// <param name="point_in_3d"></param>
        /// <returns></returns>
        public bool SetWorkPlaneAndPickObject(UIDocument uidoc,out XYZ point_in_3d)
        {
            point_in_3d = null;
            Document doc = uidoc.Document;

            Reference r = uidoc.Selection.PickObject(
                Autodesk.Revit.UI.Selection.ObjectType.Face,
                "请选择一个用于拾取点的面");
            Element e = doc.GetElement(r.ElementId);
            if (null != e)
            {
                PlanarFace face = e.GetGeometryObjectFromReference(r)
                as PlanarFace;

                GeometryElement geoEle = e.get_Geometry(new Options());
                Transform transform = null;

                // 译者注：这段代码应该是基于 Revit 2012。在 Revit 2013 中，geoEle 本身就实现了 IEnumerable 接口，所以直接使用 geoEle 遍历即可
                foreach (GeometryObject gObj in geoEle)
                {
                    GeometryInstance gInst = gObj as GeometryInstance;
                    if (null != gInst)
                    {
                        transform = gInst.Transform;
                    }
                }

                if (face != null)
                {
                    Plane plane = null;

                    // 译者注：这个 transform 很关键。它是表示元素自身的坐标系和当前文档的坐标系是否有差异。
                    // 因为面的法线向量和面的原点的值都是基于元素自身的坐标系的。如果元素自身的坐标系和当前文档的坐标系有差异，则我们必须使用
                    // 坐标系差异(transform)来将面的法线向量和面的原点的值转换成基于当前文档坐标系的值。
                    if (null != transform)
                    {
                        plane = new Plane(transform.OfVector(face.Normal), transform.OfPoint(face.Origin));
                    }
                    else
                    {
                        plane = new Plane(face.Normal, face.Origin);
                    }

                    Transaction t = new Transaction(doc);

                    t.Start("Temporarily set work plane to pick point in 3D");

                    SketchPlane sp = SketchPlane.Create(doc, plane);

                    uidoc.ActiveView.SketchPlane = sp;
                    uidoc.ActiveView.ShowActiveWorkPlane();

                    try
                    {
                        point_in_3d = uidoc.Selection.PickPoint("Please pick a point on the plane defined by the selected face");
                    }
                    catch (OperationCanceledException)
                    {
                    }

                    // 译者注：回滚事务意味着之前创建的草图平面(SketchPlane)也自动被删除了
                    t.RollBack();
                }
            }
            return null != point_in_3d;
        }
    }
}
