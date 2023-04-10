using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Blur_Experiments.Utility;

namespace Blur_Experiments
{
    public static class Test {
        public static void TestKernel() {
            KernelFloat k = new(5);
            k[2, 3] = 1;
        }
    }
}
