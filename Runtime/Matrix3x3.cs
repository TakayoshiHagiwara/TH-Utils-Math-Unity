// -----------------------------------------------------------------------
// Author:  Takayoshi Hagiwara (Toyohashi University of Technology)
// Created: 2023/7/11
// Summary: A standard 3x3 transformation matrix.
// -----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace TH.Utils
{
    // A standard 3x3 transformation matrix.
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Matrix3x3 : IEquatable<Matrix3x3>, IFormattable
    {
        // memory layout:
        //
        //                column no (=vertical)
        //               |  0   1   2 
        //            ---+------------
        //            0  | m00 m01 m02
        // row no     1  | m10 m11 m12
        // (=horiz)   2  | m20 m21 m22

        public float m00;
        public float m01;
        public float m02;

        public float m10;
        public float m11;
        public float m12;

        public float m20;
        public float m21;
        public float m22;

        public Matrix3x3(Vector3 row0, Vector3 row1, Vector3 row2)
        {
            this.m00 = row0.x; this.m01 = row0.y; this.m02 = row0.z;
            this.m10 = row1.x; this.m11 = row1.y; this.m12 = row1.z;
            this.m20 = row2.x; this.m21 = row2.y; this.m22 = row2.z;
        }

        // Access element at [row, column].
        public float this[int row, int column]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this[column + row * 3];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this[column + row * 3] = value;
            }
        }

        // Access element at sequential index (0..8 inclusive).
        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return m00;
                    case 1: return m01;
                    case 2: return m02;
                    case 3: return m10;
                    case 4: return m11;
                    case 5: return m12;
                    case 6: return m20;
                    case 7: return m21;
                    case 8: return m22;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: m00 = value; break;
                    case 1: m01 = value; break;
                    case 2: m02 = value; break;
                    case 3: m10 = value; break;
                    case 4: m11 = value; break;
                    case 5: m12 = value; break;
                    case 6: m20 = value; break;
                    case 7: m21 = value; break;
                    case 8: m22 = value; break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }

        // used to allow Matrix3x3s to be used as keys in hash tables
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return GetColumn(0).GetHashCode() ^ (GetColumn(1).GetHashCode() << 2) ^ (GetColumn(2).GetHashCode() >> 2) ^ (GetColumn(3).GetHashCode() >> 1);
        }

        // also required for being able to use Matrix3x3s as keys in hash tables
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other)
        {
            if (!(other is Matrix3x3)) return false;

            return Equals((Matrix3x3)other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Matrix3x3 other)
        {
            return GetColumn(0).Equals(other.GetColumn(0))
                && GetColumn(1).Equals(other.GetColumn(1))
                && GetColumn(2).Equals(other.GetColumn(2));
        }

        // Multiplies two matrices.
        public static Matrix3x3 operator *(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            Matrix3x3 res;
            res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20;
            res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21;
            res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22;

            res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20;
            res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21;
            res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22;

            res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20;
            res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21;
            res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22;

            return res;
        }

        // Transforms a [[Vector3]] by a matrix.
        public static Vector3 operator *(Matrix3x3 lhs, Vector3 vector)
        {
            Vector3 res;
            res.x = lhs.m00 * vector.x + lhs.m01 * vector.y + lhs.m02 * vector.z;
            res.y = lhs.m10 * vector.x + lhs.m11 * vector.y + lhs.m12 * vector.z;
            res.z = lhs.m20 * vector.x + lhs.m21 * vector.y + lhs.m22 * vector.z;

            return res;
        }

        public static bool operator ==(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            // Returns false in the presence of NaN values.
            return lhs.GetColumn(0) == rhs.GetColumn(0)
                && lhs.GetColumn(1) == rhs.GetColumn(1)
                && lhs.GetColumn(2) == rhs.GetColumn(2);
        }

        public static bool operator !=(Matrix3x3 lhs, Matrix3x3 rhs)
        {
            // Returns true in the presence of NaN values.
            return !(lhs == rhs);
        }

        // Get a column of the matrix.
        public Vector3 GetRow(int index)
        {
            switch (index)
            {
                case 0: return new Vector3(m00, m01, m02);
                case 1: return new Vector3(m10, m11, m12);
                case 2: return new Vector3(m20, m21, m22);
                default:
                    throw new IndexOutOfRangeException("Invalid column index!");
            }
        }

        // Returns a row of the matrix.
        public Vector3 GetColumn(int index)
        {
            switch (index)
            {
                case 0: return new Vector3(m00, m10, m20);
                case 1: return new Vector3(m01, m11, m21);
                case 2: return new Vector3(m02, m12, m22);
                default:
                    throw new IndexOutOfRangeException("Invalid row index!");
            }
        }

        // Sets a column of the matrix.
        public void SetColumn(int index, Vector3 column)
        {
            this[0, index] = column.x;
            this[1, index] = column.y;
            this[2, index] = column.z;
        }

        // Sets a row of the matrix.
        public void SetRow(int index, Vector3 row)
        {
            this[index, 0] = row.x;
            this[index, 1] = row.y;
            this[index, 2] = row.z;
        }

        // Creates a scaling matrix.
        public static Matrix3x3 Scale(Vector2 vector)
        {
            Matrix3x3 m;
            m.m00 = vector.x; m.m01 = 0F; m.m02 = 0F;
            m.m10 = 0F; m.m11 = vector.y; m.m12 = 0F;
            m.m20 = 0F; m.m21 = 0F; m.m22 = 1F;
            return m;
        }

        // Creates a translation matrix.
        public static Matrix3x3 Translate(Vector2 vector)
        {
            Matrix3x3 m;
            m.m00 = 1F; m.m01 = 0F; m.m02 = vector.x;
            m.m10 = 0F; m.m11 = 1F; m.m12 = vector.y;
            m.m20 = 0F; m.m21 = 0F; m.m22 = 1F;
            return m;
        }

        // Transpose this matrix.
        public void T()
        {
            Transpose();
        }

        // Transpose this matrix.
        public void Transpose()
        {
            Matrix3x3 tmp = this;

            m01 = tmp.m10; m02 = tmp.m20;
            m10 = tmp.m01; m12 = tmp.m21;
            m20 = tmp.m02; m21 = tmp.m12;
        }

        // Returns the new transposed matrix of the argument.
        public static Matrix3x3 Transpose(Matrix3x3 m)
        {
            Matrix3x3 mT;
            mT.m00 = m.m00; mT.m01 = m.m10; mT.m02 = m.m20;
            mT.m10 = m.m01; mT.m11 = m.m11; mT.m12 = m.m21;
            mT.m20 = m.m02; mT.m21 = m.m12; mT.m22 = m.m22;
            return mT;
        }

        // Returns the new inverse matrix of the argument.
        public static Matrix3x3 Inverse(Matrix3x3 m)
        {
            Matrix3x3 invm;
            
            float detA = m.m00 * m.m11 * m.m22 +
                         m.m01 * m.m12 * m.m20 +
                         m.m02 * m.m10 * m.m21 -
                         m.m02 * m.m11 * m.m20 -
                         m.m00 * m.m12 * m.m21 -
                         m.m01 * m.m10 * m.m22;
            float inversedDetA = 1 / detA;

            invm.m00 = inversedDetA * (m.m11 * m.m22 - m.m12 * m.m21);
            invm.m01 = inversedDetA * (m.m02 * m.m21 - m.m01 * m.m22);
            invm.m02 = inversedDetA * (m.m01 * m.m12 - m.m02 * m.m11);

            invm.m10 = inversedDetA * (m.m12 * m.m20 - m.m10 * m.m22);
            invm.m11 = inversedDetA * (m.m00 * m.m22 - m.m02 * m.m20);
            invm.m12 = inversedDetA * (m.m02 * m.m10 - m.m00 * m.m12);

            invm.m20 = inversedDetA * (m.m10 * m.m21 - m.m11 * m.m20);
            invm.m21 = inversedDetA * (m.m01 * m.m20 - m.m00 * m.m21);
            invm.m22 = inversedDetA * (m.m00 * m.m11 - m.m01 * m.m10);

            return invm;
        }

        // Inverse this matrix.
        public void Inverse()
        {
            Matrix3x3 invm = Inverse(this);

            m00 = invm.m00; m01 = invm.m01; m02 = invm.m02;
            m10 = invm.m10; m11 = invm.m11; m12 = invm.m12;
            m20 = invm.m20; m21 = invm.m21; m22 = invm.m22;
        }


        static readonly Matrix3x3 zeroMatrix = new Matrix3x3(new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0));

        // Returns a matrix with all elements set to zero (RO).
        public static Matrix3x3 zero { get { return zeroMatrix; } }

        static readonly Matrix3x3 identityMatrix = new Matrix3x3(new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1));

        // Returns the identity matrix (RO).
        public static Matrix3x3 identity { [MethodImpl(MethodImplOptions.AggressiveInlining)] get { return identityMatrix; } }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return ToString(null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format)
        {
            return ToString(format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "F5";
            if (formatProvider == null)
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            return String.Format("{0}\t{1}\t{2}\n{3}\t{4}\t{5}\n{6}\t{7}\t{8}\n",
                m00.ToString(format, formatProvider), m01.ToString(format, formatProvider), m02.ToString(format, formatProvider),
                m10.ToString(format, formatProvider), m11.ToString(format, formatProvider), m12.ToString(format, formatProvider),
                m20.ToString(format, formatProvider), m21.ToString(format, formatProvider), m22.ToString(format, formatProvider));
        }
    }
}