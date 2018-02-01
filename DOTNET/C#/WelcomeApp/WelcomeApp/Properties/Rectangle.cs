using System;

namespace WelcomeApp
{
    enum BorderStyle
    {
        SINGLE, DOUBLE, DOTTED
    }
    class Rectangle
    {
        private int _height;
        private int _width;
        private BorderStyle _borderStyle;

        public Rectangle(int height, int width)
        {
            _height = height;
            _width = width;
        }

        public Rectangle(int height, int width, BorderStyle borderStyle)
        {
            _height = height;
            _width = width;
            _borderStyle = borderStyle;
        }
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public int Area
        {
            get
            {
                return _width * _height;
            }
        }

        public BorderStyle BorderStyle
        {
            get
            {
                return _borderStyle;
            }
            set
            {
                _borderStyle = value;
            }
        }
    }
}
