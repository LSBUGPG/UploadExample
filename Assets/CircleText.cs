using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleText : BaseMeshEffect
{
    public override void ModifyMesh(VertexHelper vertexHelper) 
    {
        int count = vertexHelper.currentVertCount;
        if (IsActive() && count > 0)
        {
            int pointsInGlyph = 4;
            for (int index = 0; index < count; index += pointsInGlyph)
            {
                Vector3 centre = Vector3.zero;

                UIVertex uiVertex = new UIVertex();
                for (int j = 0; j < pointsInGlyph; ++j)
                {
                    vertexHelper.PopulateUIVertex(ref uiVertex, index + j);
                    centre += uiVertex.position;
                }

                centre = centre / (float)pointsInGlyph;
                float distance = centre.x;
                float radius = centre.y;
                if (radius > 0.0f)
                {
                    float angle = distance / radius; // angle in radians

                    Matrix4x4 translate = Matrix4x4.Translate(Vector3.right * -centre.x);
                    Matrix4x4 rotate = Matrix4x4.Rotate(Quaternion.Euler(Vector3.forward * -angle * Mathf.Rad2Deg));
                    Matrix4x4 combined = rotate * translate;

                    for (int j = 0; j < pointsInGlyph; ++j)
                    {
                        vertexHelper.PopulateUIVertex(ref uiVertex, index + j);
                        uiVertex.position = combined.MultiplyPoint3x4(uiVertex.position);
                        vertexHelper.SetUIVertex(uiVertex, index + j);
                    }
                }
            }
        }
    }    
}
