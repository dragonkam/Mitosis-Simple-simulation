using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mitosis_Simple_simulation
{
    class Enviroment
    {
        private long _lifeCycles=0;
        private Rectangle _field;
        private Random _myRand;
        private int _numberOfDeathCells = 0;


        public List<Cell> Cells { get; private set; }

       
        public Enviroment(Rectangle field)
        {
            this._field = field;
            _myRand = new Random();

            Cells = new List<Cell>();
            Cells.Add(new Cell(_field,_myRand));
            Cells.Add(new Cell(_field, _myRand));
            Cells.Add(new Cell(_field, _myRand));
            Cells.Add(new Cell(_field, _myRand));
        }

        public void Cycles()
        {
            _lifeCycles++;
            
            for(int i=Cells.Count-1; i>=0;i--)  //moving cell
                Cells[i].Move(Cells);

            for (int i = Cells.Count - 1; i >= 0; i--)      //remove if died
                if (Cells[i].DetectDeath())
                {
                    Cells.RemoveAt(i);
                    _numberOfDeathCells++;
                }
        }

        public void Draw(Graphics g)
        {


            foreach(var item in Cells)
            {
                item.Draw(g);
            }

            g.FillRectangle(Brushes.SeaShell, new Rectangle(0, 0,  _field.Width,20));
            using (Font font = new Font("Times New Roman", 10, FontStyle.Bold))
            {
                g.DrawString("Number of cells: " + Cells.Count,font, Brushes.Red, new Point(2, 2));
                g.DrawString("Number of deaths: " + _numberOfDeathCells, font, Brushes.Red, new Point(200, 2));
                g.DrawString("Cycles: " + _lifeCycles, font, Brushes.Red, new Point(400, 2));

            }
        }

        public void SplitCell(Point cursorPosition)
        {
            for(int i=Cells.Count-1;i>=0;i--)
            {
                if(Cells[i].Bound.Contains(cursorPosition))
                {
                    Cells[i].Split(Cells);
                    
                    break;
                }
            }
        }

        public void MoveCell(Point cursorLocation)
        {
            Cell tmpCell=null;
            for (int i = Cells.Count - 1; i >= 0; i--)
            {
                if (Cells[i].Bound.Contains(cursorLocation))
                {
                    tmpCell = Cells[i];
                    break;
                }
            }

            if(tmpCell !=null)
            tmpCell.MoveCell(cursorLocation);
        }


    }
}
