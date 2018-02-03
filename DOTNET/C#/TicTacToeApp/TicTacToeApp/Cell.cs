using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeApp
{
    public enum GameStatus
    {
        PLAYING, DRAW, CROSS_WON, NOUGHT_WON
    }

    public enum Move
    {
        EMPTY, CROSS, NOUGHT
    }

    public class Cell
    {
        Move content;
        private int row, col;

        public Cell(int row, int col)
        {
            this.row = row;
            this.col = col;
            Clear();
        }

        public void Clear()
        {
            content = Move.EMPTY;
        }

        public char PrintCell()
        {
            switch (content)
            {
                case CROSS:
                    return 'X';
                case NOUGHT:
                    return 'O';
                case EMPTY:
                    return ' ';
            }
            return ' ';
        }

        public Move Content
        {
            get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }
    }
}
