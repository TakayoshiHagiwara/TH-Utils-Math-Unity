// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/11
// Summary: Utility class for quaternion conversion
// Remarks: Conversions to Euler angles often result in unintended values.
//          This is due to the characteristics of the Euler angle. Quaternion should normally be used to control rotation.
//          If it is absolutely necessary, minimize its use and carefully consider the appropriateness of the result.  
// References:  https://qiita.com/aa_debdeb/items/3d02e28fb9ebfa357eaf
//              https://qiita.com/edo_m18/items/5db35b60112e281f840e
//              https://jp.mathworks.com/help/driving/ref/quaternion.rotmat.html
//              https://jp.mathworks.com/help/driving/ref/quaternion.euler.html
//
// Note (Quaternion -> Euler):
// when sin(theta_x) = Sx, sin(theta_y) = Sy, sin(theta_z) = Sz
//      cos(theta_x) = Cx, cos(theta_y) = Cy, cos(theta_z) = Cz
//
// matrix layout: Rotation matrix Rx, Ry and Rz about the x-axis, y-axis and z-axis
//
//      | 1  0   0  |        | Cy  0  Sy |        | Cz  -Sz  0 |
// Rx = | 0  Cx -Sx |   Ry = | 0   1  0  |   Rz = | Sz  Cz   0 |
//      | 0  Sx  Cx |        | -Sy 0  Cy |        | 0   0    1 |
//
// Note (Euler -> Quaternion):
// The quaternion that rotates the unit vector n by theta on its axis is as follows
// n = (nx, ny, nz)
//
//         | nx*sin(theta / 2) |
// ||n|| = | ny*sin(theta / 2) |
//         | nz*sin(theta / 2) |
//         |  cos(theta / 2)   |
//
// Therefore, the quaternion matrices qx, qy, and qz, representing rotation around the x-, y-, and z-axes, respectively, are as follows
//
//      | sin(theta_x / 2) |         |        0         |         |        0         |
// qx = |        0         |    qy = | sin(theta_y / 2) |    qz = |        0         |
//      |        0         |         |        0         |         | sin(theta_z / 2) |
//      | cos(theta_x / 2) |         | cos(theta_y / 2) |         | cos(theta_z / 2) |
//
// -----------------------------------------------------------------------

using System;
using UnityEngine;

namespace TH.Utils
{
    public static class QuaternionConverter
    {
        /// <summary>
        /// Rotation sequence in Eulerian representation.
        /// The rotation sequence defines the order of rotation around an axis.
        /// For example, if you specify a rotation sequence of 'XYZ', then
        /// the first rotation is made around the x-axis, the second around the new y-axis, and the third around the new z-axis.
        /// </summary>
        public enum Sequence
        {
            XYZ = 0,
            XZY = 1,
            YXZ = 2,
            YZX = 3,
            ZXY = 4,
            ZYX = 5,
            // XYX = 6,
            // XZX = 7,
            // YXY = 8,
            // YZY = 9,
            // ZXZ = 10,
            // ZYZ = 11,
        }

        /// <summary>
        /// In a rotation of a point, the coordinate system is static and the point moves. 
        /// In a rotation of a coordinate system, the point is static and the coordinate system moves. 
        /// Rotation of a point and rotation of the coordinate system define equivalent angular displacements, but in opposite directions.
        /// </summary>
        public enum RotationType
        {
            Frame = 0,
            Point = 1
        }

        /// <summary>
        /// Convert quaternion to euler angles with any rotation sequence.
        /// </summary>
        /// <param name="q">Quaternion.</param>
        /// <param name="sequence">Rotation sequence in Eulerian representation. Specify as string.</param>
        /// <param name="type">Type of rotation, specified as RotationType.Point or RotationType.Frame. Default is Point.</param>
        /// <param name="isDeg">Return euler angles in deg. Default is true.</param>
        /// <returns>Euler angles.</returns>
        public static Vector3 ToEulerAngles(this Quaternion q, string sequence, RotationType type = RotationType.Point, bool isDeg = true)
        {
            Vector3 angles;

            switch(sequence)
            {
                case string s when string.Equals("xyz", sequence, StringComparison.InvariantCultureIgnoreCase):
                    angles = ToEulerAnglesXYZ(q, type);
                    break;
                case string s when string.Equals("xzy", sequence, StringComparison.InvariantCultureIgnoreCase):
                    angles = ToEulerAnglesXZY(q, type);
                    break;
                case string s when string.Equals("yxz", sequence, StringComparison.InvariantCultureIgnoreCase):
                    angles = ToEulerAnglesYXZ(q, type);
                    break;
                case string s when string.Equals("yzx", sequence, StringComparison.InvariantCultureIgnoreCase):
                    angles = ToEulerAnglesYZX(q, type);
                    break;
                case string s when string.Equals("zxy", sequence, StringComparison.InvariantCultureIgnoreCase):
                    angles = ToEulerAnglesZXY(q, type);
                    break;
                case string s when string.Equals("zyx", sequence, StringComparison.InvariantCultureIgnoreCase):
                    angles = ToEulerAnglesZYX(q, type);
                    break;
                default:
                    throw new FormatException("Unexpected rotation sequence formatting was applied. Please check the QuaternionConverter.Sequence");
            }

            if (isDeg)
                angles *= Mathf.Rad2Deg;

            return angles;
        }

        /// <summary>
        /// Convert quaternion to euler angles with any rotation sequence.
        /// </summary>
        /// <param name="q">Quaternion.</param>
        /// <param name="sequence">Rotation sequence in Eulerian representation. Specify as QuaternionConverter.Sequence.</param>
        /// <param name="type">Type of rotation, specified as RotationType.Point or RotationType.Frame. Default is Point.</param>
        /// <param name="isDeg">Return euler angles in deg. Default is true.</param>
        /// <returns>Euler angles.</returns>
        public static Vector3 ToEulerAngles(this Quaternion q, Sequence sequence, RotationType type = RotationType.Point, bool isDeg = true)
        {
            Vector3 angles;

            angles = sequence switch
            {
                Sequence.XYZ => ToEulerAnglesXYZ(q, type),
                Sequence.XZY => ToEulerAnglesXZY(q, type),
                Sequence.YXZ => ToEulerAnglesYXZ(q, type),
                Sequence.YZX => ToEulerAnglesYZX(q, type),
                Sequence.ZXY => ToEulerAnglesZXY(q, type),
                Sequence.ZYX => ToEulerAnglesZYX(q, type),
                _ => throw new FormatException("Unexpected rotation sequence formatting was applied. Please check the QuaternionConverter.Sequence")
            };

            if (isDeg)
                angles *= Mathf.Rad2Deg;

            return angles;
        }

        private static Vector3 ToEulerAnglesXYZ(Quaternion q, RotationType type)
        {
            Matrix3x3 m = RotationMatrix(q, type);
            float x, y, z;
            int sign = 1;
            if (type == RotationType.Frame)
                sign = -1;

            if (Mathf.Approximately(m.m02, 1f))
            {
                x = Mathf.Atan2(m.m21, m.m11);
                y = Mathf.PI / 2f;
                z = 0;
            }
            else if(Mathf.Approximately(m.m02, -1f))
            {
                x = Mathf.Atan2(m.m21, m.m11);
                y = -Mathf.PI / 2f;
                z = 0;
            }
            else
            {
                x = Mathf.Atan2(-sign * m.m12, m.m22);
                y = Mathf.Asin(sign * m.m02);
                z = Mathf.Atan2(-sign * m.m01, m.m00);
            }

            return new Vector3(x, y, z);
        }

        private static Vector3 ToEulerAnglesXZY(Quaternion q, RotationType type)
        {
            Matrix3x3 m = RotationMatrix(q, type);
            float x, y, z;
            int sign = 1;
            if (type == RotationType.Frame)
                sign = -1;

            if (Mathf.Approximately(-m.m01, 1f))
            {
                x = Mathf.Atan2(-m.m12, m.m22);
                y = 0;
                z = Mathf.PI / 2f;
            }
            else if (Mathf.Approximately(-m.m01, -1f))
            {
                x = Mathf.Atan2(-m.m12, m.m22);
                y = 0;
                z = -Mathf.PI / 2f;
            }
            else
            {
                x = Mathf.Atan2(sign * m.m21, m.m11);
                y = Mathf.Atan2(sign * m.m02, m.m00);
                z = Mathf.Asin(-sign * m.m01);
            }

            return new Vector3(x, y, z);
        }

        private static Vector3 ToEulerAnglesYXZ(Quaternion q, RotationType type)
        {
            Matrix3x3 m = RotationMatrix(q, type);
            float x, y, z;
            int sign = 1;
            if (type == RotationType.Frame)
                sign = -1;

            if (Mathf.Approximately(-m.m12, 1f))
            {
                x = Mathf.PI / 2f;
                y = Mathf.Atan2(-m.m20, m.m00);
                z = 0;
            }
            else if (Mathf.Approximately(-m.m12, -1f))
            {
                x = -Mathf.PI / 2f;
                y = Mathf.Atan2(-m.m20, m.m00);
                z = 0;
            }
            else
            {
                x = Mathf.Asin(-sign * m.m12);
                y = Mathf.Atan2(sign * m.m02, m.m22);
                z = Mathf.Atan2(sign * m.m10, m.m11);
            }

            return new Vector3(x, y, z);
        }

        private static Vector3 ToEulerAnglesYZX(Quaternion q, RotationType type)
        {
            Matrix3x3 m = RotationMatrix(q, type);
            float x, y, z;
            int sign = 1;
            if (type == RotationType.Frame)
                sign = -1;

            if (Mathf.Approximately(m.m10, 1f))
            {
                x = 0;
                y = Mathf.Atan2(m.m02, m.m22);
                z = Mathf.PI / 2f;
            }
            else if (Mathf.Approximately(m.m10, -1f))
            {
                x = 0;
                y = Mathf.Atan2(m.m02, m.m22);
                z = -Mathf.PI / 2f;
            }
            else
            {
                x = Mathf.Atan2(-sign * m.m12, m.m11);
                y = Mathf.Atan2(-sign * m.m20, m.m00);
                z = Mathf.Asin(sign * m.m10);
            }

            return new Vector3(x, y, z);
        }

        private static Vector3 ToEulerAnglesZXY(Quaternion q, RotationType type)
        {
            Matrix3x3 m = RotationMatrix(q, type);
            float x, y, z;
            int sign = 1;
            if (type == RotationType.Frame)
                sign = -1;

            if (Mathf.Approximately(m.m21, 1f))
            {
                x = sign * Mathf.PI / 2f;
                y = 0;
                z = Mathf.Atan2(m.m10, m.m00);
            }
            else if (Mathf.Approximately(m.m21, -1f))
            {
                x = -sign * Mathf.PI / 2f;
                y = 0;
                z = Mathf.Atan2(m.m10, m.m00);
            }
            else
            {
                x = Mathf.Asin(sign * m.m21);
                y = Mathf.Atan2(-sign * m.m20, m.m22);
                z = Mathf.Atan2(-sign * m.m01, m.m11);
            }

            return new Vector3(x, y, z);
        }

        private static Vector3 ToEulerAnglesZYX(Quaternion q, RotationType type)
        {
            Matrix3x3 m = RotationMatrix(q, type);
            float x, y, z;
            int sign = 1;
            if (type == RotationType.Frame)
                sign = -1;

            if (Mathf.Approximately(-m.m20, 1f))
            {
                x = 0;
                y = Mathf.PI / 2f;
                z = Mathf.Atan2(-m.m01, m.m11);
            }
            else if (Mathf.Approximately(-m.m20, -1f))
            {
                x = 0;
                y = -Mathf.PI / 2f;
                z = Mathf.Atan2(-m.m01, m.m11);
            }
            else
            {
                x = Mathf.Atan2(sign * m.m21, m.m22);
                y = Mathf.Asin(-sign * m.m20);
                z = Mathf.Atan2(sign * m.m10, m.m00);
            }

            return new Vector3(x, y, z);
        }

        private static Matrix3x3 RotationMatrix(Quaternion q, RotationType type)
        {
            // matrix layout: type = Frame
            //
            //   ---------------------------------------------
            //  | 2qw^2+2qx^2-1  2qxqy-2qzqw    2qxqz+2qyqw
            //  | 2qxqy+2qzqw    2qw^2+2qy^2-1  2qyqz-2qxqw
            //  | 2qxqz-2qyqw    2qyqz+2qxqw    2qw^2+2qx^2-1
            //
            // matrix layout: type = Point
            //
            //   ---------------------------------------------
            //  | 2qw^2+2qx^2-1  2qxqy+2qzqw    2qxqz-2qyqw
            //  | 2qxqy-2qzqw    2qw^2+2qy^2-1  2qyqz+2qxqw
            //  | 2qxqz+2qyqw    2qyqz-2qxqw    2qw^2+2qx^2-1

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            float x2 = x * x;
            float y2 = y * y;
            float z2 = z * z;
            float w2 = w * w;

            float xy = x * y;
            float xz = x * z;
            float xw = x * w;
            float yz = y * z;
            float yw = y * w;
            float zw = z * w;

            Matrix3x3 m;
            m.m00 = 2 * w2 + 2 * x2 - 1;
            m.m11 = 2 * w2 + 2 * y2 - 1;
            m.m22 = 2 * w2 + 2 * z2 - 1;

            switch(type)
            {
                case RotationType.Frame:
                    m.m01 = 2 * xy + 2 * zw;
                    m.m02 = 2 * xz - 2 * yw;

                    m.m10 = 2 * xy - 2 * zw;
                    m.m12 = 2 * yz + 2 * xw;

                    m.m20 = 2 * xz + 2 * yw;
                    m.m21 = 2 * yz - 2 * xw;
                    break;
                case RotationType.Point:
                    m.m01 = 2 * xy - 2 * zw;
                    m.m02 = 2 * xz + 2 * yw;

                    m.m10 = 2 * xy + 2 * zw;
                    m.m12 = 2 * yz - 2 * xw;

                    m.m20 = 2 * xz - 2 * yw;
                    m.m21 = 2 * yz + 2 * xw;
                    break;
                default:
                    // Default is Point type
                    m.m01 = 2 * xy - 2 * zw;
                    m.m02 = 2 * xz + 2 * yw;

                    m.m10 = 2 * xy + 2 * zw;
                    m.m12 = 2 * yz - 2 * xw;

                    m.m20 = 2 * xz - 2 * yw;
                    m.m21 = 2 * yz + 2 * xw;
                    break;
            }

            return m;
        }

        /// <summary>
        /// Convert euler angles to quaternion with any rotation sequence.
        /// </summary>
        /// <param name="euler">Euler angles.</param>
        /// <param name="sequence">Rotation sequence in Eulerian representation. Specify as QuaternionConverter.Sequence.</param>
        /// <param name="isEulerDeg">Whether the Euler angle to be converted is in Deg units.</param>
        /// <returns>Quaternion.</returns>
        public static Quaternion ToQuaternion(this Vector3 euler, Sequence sequence, bool isEulerDeg = true)
        {
            euler /= 2;
            if (isEulerDeg)
                euler *= Mathf.Deg2Rad;

            Quaternion q;
            q = sequence switch
            {
                Sequence.XYZ => ToQuaternionXYZ(euler),
                Sequence.XZY => ToQuaternionXZY(euler),
                Sequence.YXZ => ToQuaternionYXZ(euler),
                Sequence.YZX => ToQuaternionYZX(euler),
                Sequence.ZXY => ToQuaternionZXY(euler),
                Sequence.ZYX => ToQuaternionZYX(euler),
                _ => throw new FormatException("Unexpected rotation sequence formatting was applied. Please check the QuaternionConverter.Sequence")
            };

            return q;
        }

        private static Quaternion ToQuaternionXYZ(Vector3 euler)
        {
            float x = euler.x; float y = euler.y; float z = euler.z;
            float sx = Mathf.Sin(x); float sy = Mathf.Sin(y); float sz = Mathf.Sin(z);
            float cx = Mathf.Cos(x); float cy = Mathf.Cos(y); float cz = Mathf.Cos(z);

            float qx = cx * sy * sz + sx * cy * cz;
            float qy = -sx * cy * sz + cx * sy * cz;
            float qz = cx * cy * sz + sx * sy * cz;
            float qw = -sx * sy * sz + cx * cy * cz;

            return new Quaternion(qx, qy, qz, qw);
        }

        private static Quaternion ToQuaternionXZY(Vector3 euler)
        {
            float x = euler.x; float y = euler.y; float z = euler.z;
            float sx = Mathf.Sin(x); float sy = Mathf.Sin(y); float sz = Mathf.Sin(z);
            float cx = Mathf.Cos(x); float cy = Mathf.Cos(y); float cz = Mathf.Cos(z);

            float qx = -cx * sy * sz + sx * cy * cz;
            float qy = cx * sy * cz - sx * cy * sz;
            float qz = sx * sy * cz + cx * cy * sz;
            float qw = sx * sy * sz + cx * cy * cz;

            return new Quaternion(qx, qy, qz, qw);
        }

        private static Quaternion ToQuaternionYXZ(Vector3 euler)
        {
            float x = euler.x; float y = euler.y; float z = euler.z;
            float sx = Mathf.Sin(x); float sy = Mathf.Sin(y); float sz = Mathf.Sin(z);
            float cx = Mathf.Cos(x); float cy = Mathf.Cos(y); float cz = Mathf.Cos(z);

            float qx = cx * sy * sz + sx * cy * cz;
            float qy = -sx * cy * sz + cx * sy * cz;
            float qz = cx * cy * sz - sx * sy * cz;
            float qw = sx * sy * sz + cx * cy * cz;

            return new Quaternion(qx, qy, qz, qw);
        }

        private static Quaternion ToQuaternionYZX(Vector3 euler)
        {
            float x = euler.x; float y = euler.y; float z = euler.z;
            float sx = Mathf.Sin(x); float sy = Mathf.Sin(y); float sz = Mathf.Sin(z);
            float cx = Mathf.Cos(x); float cy = Mathf.Cos(y); float cz = Mathf.Cos(z);

            float qx = sx * cy * cz + cx * sy * sz;
            float qy = sx * cy * sz + cx * sy * cz;
            float qz = -sx * sy * cz + cx * cy * sz;
            float qw = -sx * sy * sz + cx * cy * cz;

            return new Quaternion(qx, qy, qz, qw);
        }

        private static Quaternion ToQuaternionZXY(Vector3 euler)
        {
            float x = euler.x; float y = euler.y; float z = euler.z;
            float sx = Mathf.Sin(x); float sy = Mathf.Sin(y); float sz = Mathf.Sin(z);
            float cx = Mathf.Cos(x); float cy = Mathf.Cos(y); float cz = Mathf.Cos(z);

            float qx = -cx * sy * sz + sx * cy * cz;
            float qy = cx * sy * cz + sx * cy * sz;
            float qz = sx * sy * cz + cx * cy * sz;
            float qw = -sx * sy * sz + cx * cy * cz;

            return new Quaternion(qx, qy, qz, qw);
        }

        private static Quaternion ToQuaternionZYX(Vector3 euler)
        {
            float x = euler.x; float y = euler.y; float z = euler.z;
            float sx = Mathf.Sin(x); float sy = Mathf.Sin(y); float sz = Mathf.Sin(z);
            float cx = Mathf.Cos(x); float cy = Mathf.Cos(y); float cz = Mathf.Cos(z);

            float qx = sx * cy * cz - cx * sy * sz;
            float qy = sx * cy * sz + cx * sy * cz;
            float qz = -sx * sy * cz + cx * cy * sz;
            float qw = sx * sy * sz + cx * cy * cz;

            return new Quaternion(qx, qy, qz, qw);
        }
    }
}