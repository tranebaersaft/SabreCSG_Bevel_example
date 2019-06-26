using Sabresaurus.SabreCSG;
using UnityEngine;

using System.Collections.Generic;
using System.Linq;

[ExecuteAlways]
public class CreateChip : CookieCutterBase
{
    // All units are in mm

    [SerializeField]
    private float chipDiameter = 45;
    [SerializeField]
    private float chipHeight = 11;
    public Material chipMaterial;

    // Chamfer settings
    public float chamferAngleThreshold = 45.0f;
    public float chamferHeightMultiplier = 0.2f;
    public float chamferHeight { get { return chamferHeightMultiplier * chipHeight; } }
    public int chamferIterations = 5;

    [ContextMenu("Execute")]
    public override void Execute()
    {

        Vector3 chipSize = new Vector3(chipDiameter, chipHeight, chipDiameter);
        // BASIC CHIP
        // CYLENDER COUNT = DEFAULT
        // CHAMFER ON
        CSGModelBase csgModel_chip_1;
        GameObject csgModelGameObject_chip_1 = new GameObject("Basic Chip");
        csgModel_chip_1 = csgModelGameObject_chip_1.AddComponent<CSGModel>();
        GameObject chipObject_1 = csgModel_chip_1.CreateBrush(PrimitiveBrushType.Cylinder, Vector3.zero, chipSize, material: chipMaterial);
        // Adjust Cylinder Count, default 20
        // chipObject.GetComponent<PrimitiveBrush>().cylinderSideCount = 64;
        // chipObject.GetComponent<PrimitiveBrush>().ResetPolygons();
        // Chamfer sharp edges of chip
        CookieCutterTools.ChamferSharpEdges(chipObject_1, chamferAngleThreshold, chamferHeight, chamferIterations);

        // Anchor the brush beneath the CSG Model
        chipObject_1.transform.Rotate(90, 0, 0, Space.Self); // Need to transforl 90deg on X Axis
        chipObject_1.transform.parent = csgModel_chip_1.transform;
        csgModel_chip_1.Build(true, false);
        csgModel_chip_1.transform.position = new Vector3(-50, 0, 0);


        // Chip_64_Bevale_OFF
        // CYLENDER COUNT = 64
        // CHAMFER OFF
        CSGModelBase csgModel_chip_2;
        GameObject csgModelGameObject_chip_2 = new GameObject("Chip_64_Bevale_OFF");
        csgModel_chip_2 = csgModelGameObject_chip_2.AddComponent<CSGModel>();

        GameObject chipObject_2 = csgModel_chip_2.CreateBrush(PrimitiveBrushType.Cylinder, Vector3.zero, chipSize, material: chipMaterial);
        // Adjust Cylinder Count, default 20
        chipObject_2.GetComponent<PrimitiveBrush>().CylinderSideCount = 64;
        chipObject_2.GetComponent<PrimitiveBrush>().ResetPolygons();
        // Chamfer sharp edges of chip
        // CookieCutterTools.ChamferSharpEdges(chipObject, chamferAngleThreshold, chamferHeight, chamferIterations);

        // Anchor the brush beneath the CSG Model
        chipObject_2.transform.Rotate(90, 0, 0, Space.Self); // Need to transforl 90deg on X Axis
        chipObject_2.transform.parent = csgModel_chip_2.transform;
        csgModel_chip_2.Build(true, false);
        csgModel_chip_2.transform.position = new Vector3(0, 0, 0);

        // Chip_64_Bevale_OFF
        // CYLENDER COUNT = 64
        // CHAMFER OFF
        CSGModelBase csgModel_chip_3;
        GameObject csgModelGameObject_chip_3 = new GameObject("Chip_64_Bevale_ON");
        csgModel_chip_3 = csgModelGameObject_chip_3.AddComponent<CSGModel>();

        GameObject chipObject_3 = csgModel_chip_3.CreateBrush(PrimitiveBrushType.Cylinder, Vector3.zero, chipSize, material: chipMaterial);
        // Adjust Cylinder Count, default 20
        chipObject_3.GetComponent<PrimitiveBrush>().CylinderSideCount = 64;
        chipObject_3.GetComponent<PrimitiveBrush>().ResetPolygons();
        // Chamfer sharp edges of chip
        CookieCutterTools.ChamferSharpEdges(chipObject_3, chamferAngleThreshold, chamferHeight, chamferIterations);

        // Anchor the brush beneath the CSG Model
        chipObject_3.transform.Rotate(90, 0, 0, Space.Self); // Need to transforl 90deg on X Axis
        chipObject_3.transform.parent = csgModel_chip_3.transform;
        csgModel_chip_3.Build(true, false);
        csgModel_chip_3.transform.position = new Vector3(50, 0, 0);

    }
}
