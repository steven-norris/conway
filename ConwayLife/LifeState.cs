using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConwayLife
{
    public class LifeState
    {
        public LifeState(int LifeRows, int LifeColumns)
        {
            Rows = LifeRows;
            Columns = LifeColumns;
            Values = new bool[Rows,Columns];
            LoadInitialBoardState("InitialBoardState.xml");
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

        public void Advance()
        {
            /*Rules Of Life
            All squares directly orthogonal or diagonal from another square count as neighbors.
            
            1.Any live cell with fewer than two live neighbours dies, as if caused by under-population.
            2.Any live cell with two or three live neighbours lives on to the next generation.
            3.Any live cell with more than three live neighbours dies, as if by overcrowding.
            4.Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
            */

            bool[,] newValues = new bool[Rows, Columns];

            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    int liveNeighbors = CountLiveNeighbors(row, col);

                    if (Values[row, col])
                    {
                        if (liveNeighbors < 2 || liveNeighbors > 3)
                        {
                            newValues[row, col] = false;
                        }
                        else
                        {
                            newValues[row, col] = true;
                        }
                    }
                    else
                    {
                        if (liveNeighbors == 3)
                        {
                            newValues[row, col] = true;
                        }
                        else
                        {
                            newValues[row, col] = false;
                        }
                    }
                }
            }

            Values = newValues;
        }

        private int CountLiveNeighbors(int row, int col)
        {
            int liveNeighbors = 0;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    int neighborRow = row + i;
                    int neighborCol = col + j;

                    if (neighborRow >= 0 && neighborRow < Rows && neighborCol >= 0 && neighborCol < Columns)
                    {
                        if (Values[neighborRow, neighborCol])
                        {
                            liveNeighbors++;
                        }
                    }
                }
            }

            return liveNeighbors;
        }

        private void LoadInitialBoardState(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            var rows = doc.Root.Elements("Row").ToList();

            for (int row = 0; row < rows.Count; row++)
            {
                var cells = rows[row].Elements("Cell").ToList();
                for (int col = 0; col < cells.Count; col++)
                {
                    Values[row, col] = bool.Parse(cells[col].Value);
                }
            }
        }
    }
}
