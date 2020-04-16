
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

            XYZ point_in_3d_start;
            XYZ point_in_3d_end;

            //拾取第一个点
            if (SetWorkPlaneAndPickObject(uidoc, out point_in_3d_start))
            {
                TaskDialog.Show("3D Point Selected",
                    "3D point picked on the plane"
                    + " defined by the selected face: "
                    + "X: " + point_in_3d_start.X.ToString()
                    + ", Y: " + point_in_3d_start.Y.ToString()
                    + ", Z: " + point_in_3d_start.Z.ToString());

              
            }
            else
            {
                message = "3D point selection failed";
                return Result.Failed;
            }

            //拾取第二个点
            if (SetWorkPlaneAndPickObject(uidoc, out point_in_3d_end))
            {
                TaskDialog.Show("3D Point Selected",
                    "3D point picked on the plane"
                    + " defined by the selected face: "
                    + "X: " + point_in_3d_end.X.ToString()
                    + ", Y: " + point_in_3d_end.Y.ToString()
                    + ", Z: " + point_in_3d_end.Z.ToString());

                
            }
            else
            {
                message = "3D point selection failed";
                return Result.Failed;
            }

            using (Transaction trans1 = new Transaction(doc))
            {
                trans1.Start("开始绘制三维线段");

                
                Line curve_3d = Line.CreateBound(point_in_3d_start, point_in_3d_end);

                XYZ direction = curve_3d.Direction;

                XYZ normal_direc = direction.CrossProduct(XYZ.BasisZ);

                Plane normal_plane = new Plane(normal_direc, point_in_3d_start);

                SketchPlane sketchPlane = SketchPlane.Create(doc, normal_plane);
                ModelCurve modelCurve = doc.Create.NewModelCurve(curve_3d,sketchPlane);
                
                trans1.Commit();
            }

            return Result.Succeeded;

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
                        plane = new Plane(transform.OfVector(face.FaceNormal), transform.OfPoint(face.Origin));
                    }
                    else
                    {
                        plane = new Plane(face.FaceNormal, face.Origin);
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
