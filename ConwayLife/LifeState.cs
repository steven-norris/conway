using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayLife
{
    public class LifeState
    {
        public LifeState(int LifeRows, int LifeColumns)
        {
            Rows = LifeRows;
            Columns = LifeColumns;
            Values = new bool[Rows,Columns];
            Randomize();
        }

        public int Rows
        {
            get;
            private set;
        }
        public int Columns
        {
            get;
            private set;
        }
        public bool[,] Values
        {
            get;
            private set;
        }

        public void Randomize()
        {
            Random Randomizer = new Random(Environment.TickCount);

            for(int RowCounter = 0; RowCounter < Rows; RowCounter++)
            {
                for(int ColumnCounter = 0; ColumnCounter < Columns; ColumnCounter++)
                {
                    switch(Randomizer.Next(2))
                    {
                        case 1:
                            Values[RowCounter,ColumnCounter] = true;
                            break;
                        default:
                            Values[RowCounter, ColumnCounter] = false;
                            break;
                    }
                }
            }
        }

        public void Advance()
        {
            /*Rules Of Life
            All squares directly orthogonal or diagonal from another square count as neighbors.
            
            1.Any live cell with fewer than two live neighbours dies, as if caused by under-population.
            2.Any live cell with two or three live neighbours lives on to the next generation.
            3.Any live cell with more than three live neighbours dies, as if by overcrowding.
            4.Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
            */

            //Remove the line below and implement as described above.
            Randomize();
        }
    }
}
