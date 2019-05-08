using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDL
{
    public class RandomizerRow
    {
        private static readonly Random rnd = new Random();

        public int Id;
        public double Value = rnd.NextDouble();

        public RandomizerRow(int id)
        {
            this.Id = id;
        }
    }
}
