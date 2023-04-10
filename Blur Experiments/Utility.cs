using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur_Experiments
{
    public static class Utility {
        public class Kernel<ScalarType> {
            /// <summary>Width of the kernel.</summary>
            public readonly int SizeWidth;
            /// <summary>Height of the kernel.</summary>
            public readonly int SizeHeight;
            /// <summary>X coordinate of the center of the kernel.</summary>
            public readonly int CenterX;
            /// <summary>Y coordinate of the center of the kernel.</summary>
            public readonly int CenterY;
            /// <summary>A 2D array contents of the kernel. Remember to access in the form of Data[y, x].
            /// The indexer for this class is based on this property.</summary>
            public readonly ScalarType[,] Data; // Remember to access in the form of Data[y, x]

            /// <summary>Creates a square Kernel of given width and height.</summary>
            /// <param name="size"></param>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            /// <exception cref="ArgumentException"></exception>
            public Kernel(int size) {
                if (size <= 0) throw new ArgumentOutOfRangeException("Size must be a positive odd number");
                if (size % 2 == 0) throw new ArgumentException("Size must be a positive odd number");

                SizeWidth = size; SizeHeight = size; CenterX = size / 2; CenterY = size / 2;
                Data = new ScalarType[SizeHeight, SizeWidth];
            }

            public Kernel(int sizeWidth, int sizeHeight, int centerX, int centerY) {
                if (sizeWidth <= 0) throw new ArgumentOutOfRangeException("Size must be a positive number");

                SizeWidth = sizeWidth; SizeHeight = sizeHeight; CenterX = centerX; CenterY = centerY;
                Data = new ScalarType[SizeHeight, SizeWidth];
            }

            public ScalarType this[int y, int x] {
                get { return Data[y, x]; }
                set { Data[y, x] = value;}
            }
        }

        public class KernelInt : Kernel<int> {
            public KernelInt(int size) : base(size) {}
            public KernelInt(int sizeWidth, int sizeHeight, int centerX, int centerY) : base(sizeWidth, sizeHeight, centerX, centerY) { }

            public static implicit operator KernelFloat(KernelInt k) {
                KernelFloat final = new(k.SizeWidth, k.SizeHeight, k.CenterX, k.CenterY);
                for (int y = 0; y < k.SizeHeight; y++) {
                    for (int x = 0; x < k.SizeWidth; x++) {
                        final[y,x] = k[y,x];
                    }
                }
                return final;
            }
        }
        public class KernelFloat : Kernel<float> {
            public KernelFloat(int size) : base(size) { }
            public KernelFloat(int sizeWidth, int sizeHeight, int centerX, int centerY) : base(sizeWidth, sizeHeight, centerX, centerY) { }

            public static explicit operator KernelInt(KernelFloat k) {
                KernelInt final = new(k.SizeWidth, k.SizeHeight, k.CenterX, k.CenterY);
                for (int y = 0; y < k.SizeHeight; y++) {
                    for (int x = 0; x < k.SizeWidth; x++) {
                        final[y, x] = (int)k[y, x];
                    }
                }
                return final;
            }

        }
    }
}
