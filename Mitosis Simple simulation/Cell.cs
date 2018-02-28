using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mitosis_Simple_simulation
{
    class Cell
    {
        public Rectangle Bound => _bound;
        public int Size => _size;


        private Random _myRand;
        private Color _color;
        private Rectangle _bound;
        private Point _location;
        private Rectangle _field;
        private int _size;
        private float _sizeForRatio;
        private int _life = 1;
        private bool _blockMoving = false;
        

        private const int StartingSize = 10;
        private const int MaximumSize = 100;
        private const float  GrowingRatio =0.15F;
        private const int DeathCycles = 2000;

        public Cell(Rectangle field,Random myRand)
        {
            this._myRand = myRand;
            this._field = field;
            _size = StartingSize;
            _color = Color.FromArgb(50, _myRand.Next(1, 255), _myRand.Next(1, 255), _myRand.Next(1, 255));
            _location = new Point(_myRand.Next(1,_field.Width-_size),_myRand.Next(1,_field.Height-_size));
            _bound = new Rectangle(_location, new Size(_size, _size));
        }

        public Cell(Rectangle field, Random myRand,int size,Point location)
        {
            this._myRand = myRand;
            this._field = field;
            _size = size;
            _sizeForRatio = size;
            _color = Color.FromArgb(50, _myRand.Next(1, 255), _myRand.Next(1, 255), _myRand.Next(1, 255));
            _location = location;
            _bound = new Rectangle(_location, new Size(_size, _size));
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(_color), _bound);
            using (Font font = new Font("Times New Roman", 10, FontStyle.Bold))
                g.DrawString(_life.ToString(), font, Brushes.Black, new Point(_bound.Location.X + 10, _bound.Location.Y + 10));
        }

        public void Move(List<Cell> cells)
        {
            
            _life++;

            if (_blockMoving) return;
            GrowingUp();
            
            Point tmpLocation;

            do
            {
                tmpLocation = new Point(_location.X + _myRand.Next(-5, 6), _location.Y + _myRand.Next(-5, 6));
            }
            while (!_field.Contains(tmpLocation));

            _location = tmpLocation;
            _bound.Location = _location;

            if(_life% 500 == 0)
            {
                if (_myRand.Next(1, 101) > 75)
                    Split(cells);
            }
        }

        public void Split(List<Cell> cells)
        {
            this._size = _size / 2;
            _sizeForRatio = _size;
            _bound.Size = new Size(_size, _size);

            cells.Add(new Cell(_field, _myRand, this._size, this._location));
        }

        public void MoveCell(Point cursiorLocation)
        {
            _blockMoving = true;
            Point tmpLocation;
            tmpLocation = cursiorLocation;
            _location = tmpLocation;
            _bound.Location = _location;
        }
        
        private void GrowingUp()
        {
            if(_size < MaximumSize)
            {
                _sizeForRatio += GrowingRatio;
                _size = (int)_sizeForRatio;
                _bound = new Rectangle(_location, new Size(_size, _size));
            }

        }
              
        public bool DetectDeath()
        {
            if (_life >= DeathCycles) return true;
            else return false;
        }
    }
}
