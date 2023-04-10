using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blur_Experiments
{
    /// <summary>A class containing temporary methods, for testing.</summary>
    public static class Test {
        /// <summary>Test creating a kernel.</summary>
        public static void TestKernel() {
            KernelFloat k = new(5);
            k[2, 3] = 1;
        }
    }
}
