using System;
using System.Collections.Generic;

namespace OTUS_ArrayList
{
    class Program
    {
        enum ChessFigures //Перечисление фигур, для которых происходит расчёт
        {
            Rook,
            Knight,
            Bishop,
            Queen,
            King
        }

        static void Main(string[] args)
        {
            List<MoveCoords>[][,] moves;                                   
            int count = Enum.GetNames(typeof(ChessFigures)).Length;        
            moves = new List<MoveCoords>[count][,];                        

            moves[(int)ChessFigures.Rook] = new List<MoveCoords>[8, 8]; //Инициализация элементов зубчатого массива
            moves[(int)ChessFigures.Knight] = new List<MoveCoords>[8, 8];
            moves[(int)ChessFigures.Bishop] = new List<MoveCoords>[8, 8];
            moves[(int)ChessFigures.Queen] = new List<MoveCoords>[8, 8];
            moves[(int)ChessFigures.King] = new List<MoveCoords>[8, 8];

            for (int x = 0; x <= 7; x++)
                for (int y = 0; y <= 7; y++)
                {
                    moves[(int)ChessFigures.Rook][x, y] = RookMoves(x, y); //Заполнение массива возможными ходами фигур
                    moves[(int)ChessFigures.Knight][x, y] = KnightMoves(x, y);
                    moves[(int)ChessFigures.Bishop][x, y] = BishopMoves(x, y);
                    moves[(int)ChessFigures.Queen][x, y] = QueenMoves(x, y);
                    moves[(int)ChessFigures.King][x, y] = KingMoves(x, y);
                }

            int allMoves = 0; //Определение числа всех возможных ходов
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j <= 7; j++)
                {
                    for (int k = 0; k <= 7; k++)
                    {
                        allMoves += moves[i][j, k].Count;
                    }
                }
            }

            int aOneMoves = 0; //Определения числа возможных ходов с позиции а1
            for (int i = 0; i < count; i++)
            {
                aOneMoves += moves[i][0, 0].Count;
            }
            Console.WriteLine($"Общее число возможных ходов: {allMoves}");
            Console.WriteLine($"Число ходов с позиции а1: {aOneMoves}");
            Console.WriteLine();

            MinMovesCoords(ChessFigures.Rook.ToString(), (int)ChessFigures.Rook, moves);
            Console.WriteLine();
            MinMovesCoords(ChessFigures.Knight.ToString(), (int)ChessFigures.Knight, moves);
            Console.WriteLine();
            MinMovesCoords(ChessFigures.Bishop.ToString(), (int)ChessFigures.Bishop, moves);
            Console.WriteLine();
            MinMovesCoords(ChessFigures.Queen.ToString(), (int)ChessFigures.Queen, moves);
            Console.WriteLine();
            MinMovesCoords(ChessFigures.King.ToString(), (int)ChessFigures.King, moves);
            Console.ReadKey(true);
        }
        // Определяет координаты с наименьшим количеством ходов и выводит их на экран
        static void MinMovesCoords(string name, int i, List<MoveCoords>[][,] moves) 
        {
            string coords = "";
            int minMoves = int.MaxValue;
            for (int j = 0; j <= 7; j++)
            {
                for (int k = 0; k <= 7; k++)
                {
                    if (moves[i][j, k].Count <= minMoves)
                        minMoves = moves[i][j, k].Count;
                }
            }

            for (int j = 0; j <= 7; j++)
            {
                for (int k = 0; k <= 7; k++)
                {
                    if (minMoves == moves[i][j, k].Count)
                        coords += ($"({j},{k}), ");
                }
            }
            string answer = coords.Trim().Trim(',');
            Console.WriteLine($"Для фигуры {name} координатами с наименьшим количеством ходов являются: {answer}");
        }

        // Моделируют движение фигур
        static List<MoveCoords> KingMoves(int x, int y)
        {
            List<MoveCoords> list = new List<MoveCoords>();
            if (x + 1 <= 7 && y + 1 <= 7)
                list.Add(new MoveCoords(x + 1, y + 1));
            if (x - 1 >= 0 && y - 1 >= 0)
                list.Add(new MoveCoords(x - 1, y - 1));
            if (x - 1 >= 0 && y + 1 <= 7)
                list.Add(new MoveCoords(x - 1, y + 1));
            if (x + 1 <= 7 && y - 1 >= 0)
                list.Add(new MoveCoords(x + 1, y - 1));
            if (y + 1 <= 7)
                list.Add(new MoveCoords(x, y + 1));
            if (y - 1 >= 0)
                list.Add(new MoveCoords(x, y - 1));
            if (x + 1 <= 7)
                list.Add(new MoveCoords(x + 1, y));
            if (x - 1 >= 0)
                list.Add(new MoveCoords(x - 1, y));
            return list;
        }
        static List<MoveCoords> QueenMoves(int x, int y)
        {
            List<MoveCoords> list = new List<MoveCoords>();
            for (int oX = x - 1; oX >= 0; oX--)
                list.Add(new MoveCoords(oX, y));
            for (int oX = x + 1; oX <= 7; oX++)
                list.Add(new MoveCoords(oX, y));
            for (int oY = y - 1; oY >= 0; oY--)
                list.Add(new MoveCoords(x, oY));
            for (int oY = y + 1; oY <= 7; oY++)
                list.Add(new MoveCoords(x, oY));

            int xFirstDirection, xSecondDirection, xThirdDirection, xFourthDirection;
            xFirstDirection = xSecondDirection = xThirdDirection = xFourthDirection = x;
            int yFirstDirection, ySecondDirection, yThirdDirection, yFourthDirection;
            yFirstDirection = ySecondDirection = yThirdDirection = yFourthDirection = y;

            while (xFirstDirection < 7 && yFirstDirection < 7)
            {
                xFirstDirection += 1;
                yFirstDirection += 1;
                list.Add(new MoveCoords(xFirstDirection, yFirstDirection));
            }

            while (xSecondDirection < 7 && ySecondDirection > 0)
            {
                xSecondDirection += 1;
                ySecondDirection -= 1;
                list.Add(new MoveCoords(xSecondDirection, ySecondDirection));
            }

            while (xThirdDirection > 0 && yThirdDirection > 0)
            {
                xThirdDirection -= 1;
                yThirdDirection -= 1;
                list.Add(new MoveCoords(xThirdDirection, yThirdDirection));
            }

            while (xFourthDirection > 0 && yFourthDirection < 7)
            {
                xFourthDirection -= 1;
                yFourthDirection += 1;
                list.Add(new MoveCoords(xFourthDirection, yFourthDirection));
            }

            return list;
        }
        static List<MoveCoords> BishopMoves(int x, int y)
        {
            List<MoveCoords> list = new List<MoveCoords>();
            int xFirstDirection, xSecondDirection, xThirdDirection, xFourthDirection;
            xFirstDirection = xSecondDirection = xThirdDirection = xFourthDirection = x;
            int yFirstDirection, ySecondDirection, yThirdDirection, yFourthDirection;
            yFirstDirection = ySecondDirection = yThirdDirection = yFourthDirection = y;

            while (xFirstDirection < 7 && yFirstDirection < 7)
            {
                xFirstDirection += 1;
                yFirstDirection += 1;
                list.Add(new MoveCoords(xFirstDirection, yFirstDirection));
            }

            while (xSecondDirection < 7 && ySecondDirection > 0)
            {
                xSecondDirection += 1;
                ySecondDirection -= 1;
                list.Add(new MoveCoords(xSecondDirection, ySecondDirection));
            }

            while (xThirdDirection > 0 && yThirdDirection > 0)
            {
                xThirdDirection -= 1;
                yThirdDirection -= 1;
                list.Add(new MoveCoords(xThirdDirection, yThirdDirection));
            }

            while (xFourthDirection > 0 && yFourthDirection < 7)
            {
                xFourthDirection -= 1;
                yFourthDirection += 1;
                list.Add(new MoveCoords(xFourthDirection, yFourthDirection));
            }
            return list;
        }
        static List<MoveCoords> KnightMoves(int x, int y)
        {
            List<MoveCoords> list = new List<MoveCoords>();
            int xRight = x + 2;
            int yUp = y + 2;
            int xLeft = x - 2;
            int yDown = y - 2;
            if (xRight <= 7 && y + 1 <= 7)
                list.Add(new MoveCoords(xRight, y + 1));
            if (xRight <= 7 && y - 1 >= 0)
                list.Add(new MoveCoords(xRight, y - 1));
            if (xLeft >= 0 && y + 1 <= 7)
                list.Add(new MoveCoords(xLeft, y + 1));
            if (xLeft >= 0 && y - 1 >= 0)
                list.Add(new MoveCoords(xLeft, y - 1));
            if (yUp <= 7 && x + 1 <= 7)
                list.Add(new MoveCoords(x + 1, yUp));
            if (yUp <= 7 && x - 1 >= 0)
                list.Add(new MoveCoords(x - 1, yUp));
            if (yDown >= 0 && x + 1 <= 7)
                list.Add(new MoveCoords(x + 1, yDown));
            if (yDown >= 0 && x - 1 >= 0)
                list.Add(new MoveCoords(x - 1, yDown));
            return list;
        }
        static List<MoveCoords> RookMoves(int x, int y)
        {
            List<MoveCoords> list = new List<MoveCoords>();
            for (int oX = x - 1; oX >= 0; oX--)
                list.Add(new MoveCoords(oX, y));
            for (int oX = x + 1; oX <= 7; oX++)
                list.Add(new MoveCoords(oX, y));
            for (int oY = y - 1; oY >= 0; oY--)
                list.Add(new MoveCoords(x, oY));
            for (int oY = y + 1; oY <= 7; oY++)
                list.Add(new MoveCoords(x, oY));
            return list;
        }
        
        //Структура для хранения координат ходов
        struct MoveCoords
        {
            public int x;
            public int y;
            public MoveCoords(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public override string ToString() => $"({x}, {y})";
        }
    }
}
