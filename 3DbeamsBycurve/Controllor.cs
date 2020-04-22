using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;

namespace _3DbeamsBycurve
{
    public class Controllor
    {
        public ChsForm pView;
        public Model pModel;
        public ExternalCommandData commandData;

        public Controllor(ChsForm pView, ExternalCommandData _commandData)
        {
            pModel = new Model();
            this.pView = pView;
            this.pView.Controllor = this;
            commandData = _commandData;

           

            //在模型中生成用于controller的模型参数
            Document doc = _commandData.Application.ActiveUIDocument.Document;
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfCategory(BuiltInCategory.OST_StructuralFraming);
            pModel.BeamSymbolsName = new List<string>();
            pModel.BeamSymbols = new List<FamilySymbol>();
            FamilySymbol familySymbol = null;
            foreach (var item in collector)
            {
                familySymbol = item as FamilySymbol;
                if (familySymbol != null)
                {
                    pModel.BeamSymbolsName.Add(familySymbol.Name.ToString());
                    //族本身加入到相应的文件中
                    pModel.BeamSymbols.Add(familySymbol);
                }

            }

            //初始化当前标高系统，并将标高初始化到Model中

            FilteredElementCollector collector_levels = new FilteredElementCollector(doc);

            collector_levels.OfClass(typeof(Level));
            pModel.levelsName = new List<string>();
            pModel.levels = new List<Level>();
            Level thelevel = null;
            foreach (var item in collector_levels)
            {
                thelevel = item as Level;
                if (thelevel != null)
                {
                    pModel.levelsName.Add(thelevel.Name);
                    //标高本身加入到列表当中
                    pModel.levels.Add(thelevel);
                }
            }


        }
    }

    public class ExecuteEvent : IExternalEventHandler
    {
        ChsForm pForm;

        public ExecuteEvent(ChsForm _pform)
        {
            pForm = _pform;
        }

        public ExecuteEvent()
        {
            pForm = null;
        }

        public void Execute(UIApplication app)
        {
            Document doc = app.ActiveUIDocument.Document;
            

            //拿到第一种梁类型

            FamilySymbol familySymbol = null;
            familySymbol = pForm.Controllor.pModel.BeamSymbols[pForm.cbox_BeamSymbols.SelectedIndex];


            //拿到用户选择的标高
            Level thepickedlevel = null;
            thepickedlevel = pForm.Controllor.pModel.levels[pForm.cbox_Levels.SelectedIndex];

            UIDocument uidoc = app.ActiveUIDocument;
            Selection selection = uidoc.Selection;

            //选择模型线
            ModelCurve elem_curve = null;
            Reference reference_curve = selection.PickObject(ObjectType.Element, new ModelcurveFilter(), "请选择模型线");
            if (reference_curve == null)
            {

            }
            else
            {
                elem_curve = doc.GetElement(reference_curve) as ModelCurve;
            }

            Curve beamCurve = elem_curve.GeometryCurve;
                //创建梁
            using (Transaction tr = new Transaction(doc))
            {
                TaskDialog.Show("ss", "正在尝试进行事务处理");
                tr.Start("Create beam");
                if (familySymbol == null)
                {
                    tr.Commit();
                }
                if (!familySymbol.IsActive)
                    familySymbol.Activate();
                doc.Create.NewFamilyInstance(beamCurve, familySymbol, thepickedlevel, Autodesk.Revit.DB.Structure.StructuralType.Beam);

                tr.Commit();
            }

        }

        public string GetName()
        {
            return "这是一个创建斜梁的操作";
        }

        /// <summary>
        /// Project a point on a face
        /// </summary>
        /// <param name="xyzArray">the face points, them fix a face </param>
        /// <param name="point">the point</param>
        /// <returns>the projected point on this face</returns>
        private XYZ Project(List<XYZ> xyzArray, XYZ point)
        {
            XYZ a = xyzArray[0] - xyzArray[1];
            XYZ b = xyzArray[0] - xyzArray[2];
            XYZ c = point - xyzArray[0];

            XYZ normal = (a.CrossProduct(b));

            try
            {
                normal = normal.Normalize();
            }
            catch (Exception)
            {
                normal = XYZ.Zero;
            }

            XYZ retProjectedPoint = point - (normal.DotProduct(c)) * normal;
            return retProjectedPoint;
        }

        /// <summary>
        /// 拿到一条线到一个楼板上的投影，此处针对的是某一个楼板 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="floor"></param>
        /// <returns></returns>
        public Curve FindFloorcurve(Curve line, Floor floor)
        {
            var floorgeom = floor.get_Geometry(GetgeometryOptions());
            var geomobj = floorgeom.First<GeometryObject>();
            Solid floorobj = geomobj as Solid;
            double facearea = 0;

            PlanarFace pf = null;
            PlanarFace pf_target = null;
            foreach (Face face in floorobj.Faces)
            {
                pf = face as PlanarFace;
                if (pf.Area > facearea && pf.FaceNormal.Z >= 0)
                {
                    //拿到楼板上面的一个面
                    facearea = pf.Area;
                    pf_target = pf;

                }
            }

            TaskDialog.Show("ss", pf_target.Area.ToString());
            XYZ point_start = pf_target.Project(line.GetEndPoint(0)).XYZPoint;
            XYZ point_end = pf_target.Project(line.GetEndPoint(1)).XYZPoint;
            Curve curve = Line.CreateBound(point_start, point_end);

            return curve;
        }

        public Options GetgeometryOptions()
        {
            Options option = new Options();
            option.ComputeReferences = true;
            option.DetailLevel = ViewDetailLevel.Coarse;
            return option;

        }

        /// <summary>
        /// 拿到一条线投影到一个面上面
        /// </summary>
        /// <param name="line"></param>
        /// <param name="face_refer"></param>
        /// <returns></returns>
        public Curve FindFacecurve(Curve line, PlanarFace face)
        {

            XYZ point_start = face.Project(line.GetEndPoint(0)).XYZPoint;
            XYZ point_end = face.Project(line.GetEndPoint(1)).XYZPoint;
            Curve curve = Line.CreateBound(point_start, point_end);

            return curve;
        }
    }
}
