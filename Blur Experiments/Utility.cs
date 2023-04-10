using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur_Experiments
{
    public static class Utility {
        /// <summary>A generic Kernel for use in image processing.<br />
        /// For <see cref="int"/>, prefer using <seealso cref="KernelInt"/> instead of <see cref="Kernel{int}"/>.
        /// For <see cref="float"/>, prefer using <seealso cref="KernelFloat"/> instead of <see cref="Kernel{float}"/>.</summary>
        /// <typeparam name="ScalarType">The type of each element in the kernel.</typeparam>
        public class Kernel<ScalarType> {
            /// <summary>Width of the kernel.</summary>
            public readonly int SizeWidth;
            /// <summary>Height of the kernel.</summary>
            public readonly int SizeHeight;
            /// <summary>X coordinate of the center of the kernel.</summary>
            public readonly int CenterX;
            /// <summary>Y coordinate of the center of the kernel.</summary>
            public readonly int CenterY;
            /// <summary>A 2D <typeparamref name="ScalarType" /> array contents of the kernel.
            /// Remember to access in the form of Data[y, x].
            /// The indexer for this class is based on this property.</summary>
            public readonly ScalarType[,] Data; // Remember to access in the form of Data[y, x]

            /// <summary>Creates a square kernel of given width and height.</summary>
            /// <param name="size">The width/height of this square kernel. Must be an odd number.</param>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="size"/> is not positive.</exception>
            /// <exception cref="ArgumentException">Thrown when <paramref name="size"/> is not an odd number.</exception>
            public Kernel(int size) {
                if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size), "Size must be a positive odd number");
                if (size % 2 == 0) throw new ArgumentException("Size must be a positive odd number", nameof(size));

                SizeWidth = size; SizeHeight = size; CenterX = size / 2; CenterY = size / 2;
                Data = new ScalarType[SizeHeight, SizeWidth];
            }

            /// <summary>Creates a kernel of custom width and height, with a given center.</summary>
            /// <param name="sizeWidth"><inheritdoc cref="SizeWidth" path="/summary" /></param>
            /// <param name="sizeHeight"><inheritdoc cref="SizeHeight" path="/summary" /></param>
            /// <param name="centerX"><inheritdoc cref="CenterX" path="/summary" /></param>
            /// <param name="centerY"><inheritdoc cref="CenterY" path="/summary" /></param>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when any of the 4 parameters are out of range.</exception>
            public Kernel(int sizeWidth, int sizeHeight, int centerX, int centerY) {
                if (sizeWidth <= 0) throw new ArgumentOutOfRangeException(nameof(sizeWidth), "sizeWidth must be a positive number");
                if (sizeHeight <= 0) throw new ArgumentOutOfRangeException(nameof(sizeHeight), "sizeHeight must be a positive number");
                if (!(0 <= centerX && centerX < sizeWidth)) throw new ArgumentOutOfRangeException(nameof(centerX), "centerX must be >= 0 and < sizeWidth");
                if (!(0 <= centerY && centerY < sizeHeight)) throw new ArgumentOutOfRangeException(nameof(centerY), "centerY must be >= 0 and < sizeHeight");

                SizeWidth = sizeWidth; SizeHeight = sizeHeight; CenterX = centerX; CenterY = centerY;
                Data = new ScalarType[SizeHeight, SizeWidth];
            }

            /// <summary>Indexer for getting/setting an element in the kernel.</summary>
            /// <param name="y">The Y coordinate.</param>
            /// <param name="x">The X coordinate.</param>
            /// <returns>Returns the <typeparamref name="ScalarType"/> element at the given location.</returns>
            public ScalarType this[int y, int x] {
                get { return Data[y, x]; }
                set { Data[y, x] = value;}
            }
        }

        public class KernelInt : Kernel<int> {
            /// <inheritdoc/>
            public KernelInt(int size) : base(size) {}
            /// <inheritdoc/>
            public KernelInt(int sizeWidth, int sizeHeight, int centerX, int centerY) : base(sizeWidth, sizeHeight, centerX, centerY) { }

            /// <summary>Implicit cast to convert into a <see cref="KernelFloat"/>, by converting all <see cref="int"/> elements to <see cref="float"/>.</summary>
            /// <param name="k">The <see cref="KernelInt"/> to convert.</param>
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
            /// <inheritdoc/>
            public KernelFloat(int size) : base(size) { }
            /// <inheritdoc/>
            public KernelFloat(int sizeWidth, int sizeHeight, int centerX, int centerY) : base(sizeWidth, sizeHeight, centerX, centerY) { }

            /// <summary>Explicit cast to convert into a <see cref="KernelInt"/>, by converting all <see cref="float"/> elements to <see cref="int"/>.</summary>
            /// <param name="k">The <see cref="KernelFloat"/> to convert.</param>
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
