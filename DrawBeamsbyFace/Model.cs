﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Autodesk.Revit;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

namespace DrawBeamsbyFace
{
    public class Model
    {
        //存储相应的模型先工作ID
        private ElementId _curve_id;
        public ElementId curve_id
        {
            get
            {
                return _curve_id;
            }
            set
            {
                this._curve_id = value;
            }
        }

        //存储相应的楼板ID
        private ElementId _floor_id;
        public ElementId floor_id
        {
            get
            {
                return _floor_id;
            }
            set
            {
                _floor_id = value;
            }
        }

        //存储相应的面
        private Reference _face_reference;
        public Reference face_reference
        {
            get
            {
                return _face_reference;
            }
            set
            {
                _face_reference = value;
            }
        }

        //存储相应的族类型文件
        private List<string> beamSymbolsName;
        public List<string> BeamSymbolsName
        {
            get
            {
                return beamSymbolsName;
            }
            set
            {
                beamSymbolsName = value;
            }
        }

        //存储相应的族类型文件
        private List<FamilySymbol> beamSymbols;
        public List<FamilySymbol> BeamSymbols
        {
            get
            {
                return beamSymbols;
            }
            set
            {
                beamSymbols = value;
            }
        }

        //存储相应的标高文件
        private List<string> _levelsName;
        public List<string> levelsName
        {
            get
            {
                return _levelsName;
            }
            set
            {
                _levelsName = value;
            }
        }

        //存储相应的标高
        private List<Level> _levels;
        public List<Level> levels
        {
            get
            {
                return _levels;
            }
            set
            {
                _levels = value;
            }
        }
    }

    public class ModelcurveFilter : ISelectionFilter
    {


        public bool AllowElement(Element elem)
        {
            return elem is ModelCurve;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 拾取楼板过滤器
    /// </summary>
    public class FloorFaceFilter : ISelectionFilter
    {
        // Revit document.
        Document m_doc = null;

        /// <summary>
        /// Constructor the filter and initialize the document.
        /// </summary>
        /// <param name="doc">The document.</param>
        public FloorFaceFilter(Document doc)
        {
            m_doc = doc;
        }

        /// <summary>
        /// Allow floor to be selected
        /// </summary>
        /// <param name="element">A candidate element in selection operation.</param>
        /// <returns>Return true for floor. Return false for non floor element.</returns>
        public bool AllowElement(Element element)
        {
            return element is Floor;
        }

        /// <summary>
        /// Allow face reference to be selected
        /// </summary>
        /// <param name="refer">A candidate reference in selection operation.</param>
        /// <param name="point">The 3D position of the mouse on the candidate reference.</param>
        /// <returns>Return true for face reference. Return false for non face reference.</returns>
        public bool AllowReference(Reference refer, XYZ point)
        {
            GeometryObject geoObject = m_doc.GetElement(refer).GetGeometryObjectFromReference(refer);
            return geoObject != null && geoObject is Face;
        }
    }

    public class FaceFloorFilters : ISelectionFilter
    {
        // Revit document.
        Document m_doc = null;

        public FaceFloorFilters(Document doc)
        {
            m_doc = doc;
        }

        public bool AllowElement(Element elem)
        {
            return elem is Floor;
        }

        public bool AllowReference(Reference refer, XYZ position)
        {
            GeometryObject geoObject = m_doc.GetElement(refer).GetGeometryObjectFromReference(refer);
            return geoObject != null && geoObject is Face;
        }
    }

}
