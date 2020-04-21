using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;

namespace DrawBeamsbyFace
{
    public class Controlers
    {

        public PickForm pView;
        public Model pModel;
        public ExternalCommandData commandData;

        public Controlers(PickForm pView, ExternalCommandData _commandData)
        {
            pModel = new Model();
            this.pView = pView;
            this.pView.Controllor = this;
            commandData = _commandData;

            //对pModel中的成员进行初始化
            pModel.curve_id = null;
            pModel.floor_id = null;
            
            //在模型中生成用于controller的模型参数
            Document doc = _commandData.Application.ActiveUIDocument.Document;
            FilteredElementCollector collector_beams = new FilteredElementCollector(doc);
            collector_beams.OfCategory(BuiltInCategory.OST_StructuralFraming);
            pModel.BeamSymbolsName = new List<string>();
            pModel.BeamSymbols = new List<FamilySymbol>();
            FamilySymbol familySymbol = null;
            foreach (var item in collector_beams)
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
            foreach(var item in collector_levels)
            {
                thelevel = item as Level;
                if(thelevel != null)
                {
                    pModel.levelsName.Add(thelevel.Name);
                    //标高本身加入到列表当中
                    pModel.levels.Add(thelevel);
                }
            }

        }

        public void PickModelCurve()
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Selection selection = uidoc.Selection;
            Reference reference_curve = null;
            //选择模型线

            reference_curve = selection.PickObject(ObjectType.Element, new ModelcurveFilter(), "请选择模型线");


            if (reference_curve == null)
            {

            }
            else
            {
                ModelCurve elem_curve = doc.GetElement(reference_curve) as ModelCurve;

                pModel.curve_id = elem_curve.Id;
            }

        }

        public void PickModelFloor()
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Selection selection = uidoc.Selection;

            var reference_floor = selection.PickObject(ObjectType.PointOnElement, new FloorFaceFilter(doc), "请选择楼板");
            if (reference_floor == null)
            {

            }
            else
            {
                Element elem_floor = doc.GetElement(reference_floor);
                Floor thefloor = elem_floor as Floor;
                pModel.floor_id = thefloor.Id;
            }

        }

        public void PickModelFace()
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Selection selection = uidoc.Selection;
            var reference = selection.PickObject(ObjectType.Face, new FaceFloorFilters(doc), "请选择某一个楼板的面");
            pModel.face_reference = reference;
            
        }
    }

    public class ExecuteEvent : IExternalEventHandler
    {
        PickForm pForm;

        public ExecuteEvent(PickForm _pform)
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
            ModelCurve modelCurve = doc.GetElement(pForm.Controllor.pModel.curve_id) as ModelCurve;

            //拿到用户画的线段
            Curve thecurve = modelCurve.GeometryCurve;


            //拿到用户选中的梁类型

            FamilySymbol familySymbol = null;
            familySymbol = pForm.Controllor.pModel.BeamSymbols[pForm.cbox_Beamsymbols.SelectedIndex];


            //拿到用户选择的标高
            Level thepickedlevel = null;
            thepickedlevel = pForm.Controllor.pModel.levels[pForm.cbox_levels.SelectedIndex];
           
            ElementId eleid = pForm.Controllor.pModel.curve_id;
            TaskDialog.Show("ss", "成功点击了提交按钮" + eleid.ToString());
            Element ele = doc.GetElement(pForm.Controllor.pModel.face_reference.ElementId);
            PlanarFace thepickedface = ele.GetGeometryObjectFromReference(pForm.Controllor.pModel.face_reference) as PlanarFace;
            TaskDialog.Show("ss", thepickedface.ToString());
            //拿到模型线到楼板的投影线
            Curve curve_projection = FindFacecurve(thecurve, thepickedface);
            


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
                doc.Create.NewFamilyInstance(curve_projection, familySymbol, thepickedlevel,    Autodesk.Revit.DB.Structure.StructuralType.Beam);

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
            TaskDialog.Show("ss", "已经在函数里面了");
            EdgeArrayArray edgeArrays = face.EdgeLoops;
            List<XYZ> facepoints = new List<XYZ>();
            foreach (EdgeArray edges in edgeArrays)
            {
                foreach (Edge edge in edges)
                {
                    // Get one test point
                    facepoints.Add(edge.Evaluate(0));

                }
            }


            XYZ point_start = Project(facepoints, line.GetEndPoint(0));
            
            XYZ point_end = Project(facepoints, line.GetEndPoint(1));
            Curve curve = Line.CreateBound(point_start, point_end);
            
            return curve;
        }
    }
}
