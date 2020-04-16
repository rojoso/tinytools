//
// (C) Copyright 2003-2017 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE. AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//


using System;
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

namespace tinyTools
{

    

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

    public class FloorTools
    {
        public static void FindFloorslope(Floor floor,out double degree,out double thousand)
        {
            var floorgeom = floor.get_Geometry(FloorTools.GetgeometryOptions());
            var geomobj = floorgeom.First<GeometryObject>();
            Solid floorobj = geomobj as Solid;
            double facearea = 0;
            double cos_angle = 0;
            
            PlanarFace pf = null;
            foreach(Face face in floorobj.Faces)
            {
                pf = face as PlanarFace;
                if(pf.Area > facearea && pf.FaceNormal.Z >= 0)
                {
                    facearea = pf.Area;
                    cos_angle = pf.FaceNormal.Z/Math.Sqrt(Math.Pow(pf.FaceNormal.X,2)+ Math.Pow(pf.FaceNormal.Y, 2)+ Math.Pow(pf.FaceNormal.Z, 2));
                    
                }
            }
            double angle_degree = Math.Round(Math.Acos(cos_angle) / Math.PI * 180, 3);
            double angle_thousand = Math.Tan(Math.Acos(cos_angle)) * 1000;
            degree = Math.Round( angle_degree,4);
            thousand = Math.Round( angle_thousand,3);

        }


        public static Options GetgeometryOptions()
        {
            Options option = new Options();
            option.ComputeReferences = true;
            option.DetailLevel = ViewDetailLevel.Coarse;
            return option;

        }
    }
}
