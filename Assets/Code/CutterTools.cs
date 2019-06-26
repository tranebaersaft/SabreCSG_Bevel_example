using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sabresaurus.SabreCSG;

public static class CookieCutterTools
{
    struct VisitedEdge
    {
        public Vector3 pointA;
        public Vector3 pointB;
    }

    public static void ChamferSharpEdges(GameObject brushGameObject, float chamferAngleThreshold, float chamferDistance, int chamferIterations)
    {
        var brush = brushGameObject.GetComponent<PrimitiveBrush>();
        if (brush != null)
        {
            ChamferSharpEdges(brush, chamferAngleThreshold, chamferDistance, chamferIterations);
        }
    }

    public static void ChamferSharpEdges(PrimitiveBrush brush, float chamferAngleThreshold, float chamferDistance, int chamferIterations)
    {
        var polygons = new List<Polygon>(brush.GetPolygons());
        var polygonsChamfered = new List<Polygon>();

        var edgesVisited = new HashSet<VisitedEdge>();
        var edgesChamfer = new List<Edge>();

        foreach (var polygon in polygons)
        {
            var edges = polygon.GetEdges();
            foreach (var edge in edges)
            {
                var v1 = edge.Vertex1;
                var v2 = edge.Vertex2;

                if (edgesVisited.Contains(new VisitedEdge() { pointA = v1.Position, pointB = v2.Position }))
                    continue;

                edgesVisited.Add(new VisitedEdge() { pointA = v1.Position, pointB = v2.Position });
                edgesVisited.Add(new VisitedEdge() { pointA = v1.Position, pointB = v2.Position });

                var edgePolygons = polygons.Where(p => Polygon.ContainsEdge(p, edge)).ToArray();
                if (edgePolygons.Length == 2)
                {
                    var normalA = edgePolygons[0].Plane.normal;
                    var normalB = edgePolygons[1].Plane.normal;

                    var angle = Vector3.Angle(normalA, normalB);
                    if (angle > chamferAngleThreshold)
                    {
                        edgesChamfer.Add(edge);
                    }
                }
            }
        }

        if (PolygonFactory.ChamferPolygons(polygons, edgesChamfer, chamferDistance, chamferIterations, out polygonsChamfered))
        {
            brush.SetPolygons(polygonsChamfered.ToArray());
            brush.Invalidate(true);
        }
    }
}

// CHAMFER 
//The algorithm is located in Scripts/Core/PolygonFactory.cs and you could mark it as public instead of internal to use it.
//internal static bool ChamferPolygons
//(or make a little wrapper class in the SabreCSG namespace that exposes the method publicly for easier updates without conflicts)
//https://github.com/sabresaurus/SabreCSG/blob/master/Scripts/Tools/VertexEditor.cs#L1469
//Here's how the method is used by the tools. 
//Essentially you pass in the polygons of the brush, the edges you wish to chamfer and if the method returns true,
//it all worked and you assign the new "out" polygons to the brush.
