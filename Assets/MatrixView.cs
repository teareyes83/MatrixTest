using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixView : MonoBehaviour
{
    public Vector3 Translation;
    public float Rotation;
    public Vector3 RotationAroundTranslation;
    public float RotationAround;
    public Vector3 Scale;
    public Matrix4x4 Matrix = Matrix4x4.identity;
    public Color Color;
    public Vector3 Point;
    public Bounds Bounds;

    void OnDrawGizmos()
    {
        var rotationAroundTranslationScaled = Matrix4x4.Scale(Scale) * RotationAroundTranslation;
        var rotateAround = Matrix4x4.Translate(rotationAroundTranslationScaled) * Matrix4x4.Rotate(Quaternion.Euler(0, 0, RotationAround)) * Matrix4x4.Translate(-rotationAroundTranslationScaled);
        Matrix = Matrix4x4.Translate(Translation) * rotateAround * Matrix4x4.Rotate(Quaternion.Euler(0, 0, Rotation)) * Matrix4x4.Scale(Scale);

        int range = 10000;
        for (int i = 0; i < range; ++i)
        {
            //if(i % 10 == 0)
            //{
            //    Gizmos.color = Color;
            //}
            //else
            {
                var color = Color;
                color.a = 0.2f;
                Gizmos.color = color;
            }

            Gizmos.DrawLine(Matrix.MultiplyPoint3x4(new Vector3(i, -range, 0)), Matrix.MultiplyPoint3x4(new Vector3(i, range, 0)));
            Gizmos.DrawLine(Matrix.MultiplyPoint3x4(new Vector3(-i, -range, 0)), Matrix.MultiplyPoint3x4(new Vector3(-i, range, 0)));
            Gizmos.DrawLine(Matrix.MultiplyPoint3x4(new Vector3(-range, i, 0)), Matrix.MultiplyPoint3x4(new Vector3(range, i, 0)));
            Gizmos.DrawLine(Matrix.MultiplyPoint3x4(new Vector3(-range, -i, 0)), Matrix.MultiplyPoint3x4(new Vector3(range, -i, 0)));
        }

        Gizmos.DrawSphere(Matrix.MultiplyPoint(Point), 0.5f);

        Gizmos.color = Color;
        var min = Bounds.min;
        var max = Bounds.max;
        Gizmos.DrawLine(Matrix.MultiplyPoint3x4(new Vector3(min.x, min.y, 0)), Matrix.MultiplyPoint3x4(new Vector3(max.x, min.y, 0)));
        Gizmos.DrawLine(Matrix.MultiplyPoint3x4(new Vector3(max.x, max.y, 0)), Matrix.MultiplyPoint3x4(new Vector3(max.x, min.y, 0)));
        Gizmos.DrawLine(Matrix.MultiplyPoint3x4(new Vector3(max.x, max.y, 0)), Matrix.MultiplyPoint3x4(new Vector3(min.x, max.y, 0)));
        Gizmos.DrawLine(Matrix.MultiplyPoint3x4(new Vector3(min.x, min.y, 0)), Matrix.MultiplyPoint3x4(new Vector3(min.x, max.y, 0)));
    }
}
