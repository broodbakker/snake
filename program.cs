using System;

using static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace game4
{
  public struct SnakeSegment
  {
    public int X { get; }
    public int Y { get; }
    public SnakeSegment(int x, int y)
    {
      X = x;
      Y = y;
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      List<SnakeSegment> snake = new List<SnakeSegment>();

      snake.Add(new SnakeSegment(4, 5));
      snake.Add(new SnakeSegment(4, 6));

      int score = 0;

      int foodX = 30;
      int foodY = 10;

      int snakeDirection = 1;
      bool dead = false;

      int snakeX = 3;
      int snakeY = 2;

      int gameScreenHeight = 10;
      int gameScreenWidth = 10;

      int gameScreenStartX = 1;
      int gameScreenStartY = 1;

      int screenHeight = 20;
      int screenWidth = 100;
      //random
      Random rand = new Random();

      Console.CursorVisible = false;
      while (true)
      {
        while (!dead)
        {
          //fix cursor height/width ratio
          if (snakeDirection % 2 == 1)
          {
            //timing & input
            System.Threading.Thread.Sleep(60);
          }
          else
          {
            //timing & input
            System.Threading.Thread.Sleep(100);
          }

          //clear screen
          Clear();
          //change direction
          if (Console.KeyAvailable)
          {
            ConsoleKey keyPressed = ReadKey(true).Key;
            if (keyPressed == ConsoleKey.UpArrow && (snakeDirection == 1 || snakeDirection == 3))
            {
              snakeDirection = 0;
            }
            if (keyPressed == ConsoleKey.RightArrow && (snakeDirection == 0 || snakeDirection == 2))
            {
              snakeDirection = 1;
            }
            if (keyPressed == ConsoleKey.DownArrow && (snakeDirection == 1 || snakeDirection == 3))
            {
              snakeDirection = 2;
            }
            if (keyPressed == ConsoleKey.LeftArrow && (snakeDirection == 0 || snakeDirection == 2))
            {
              snakeDirection = 3;
            }
          }

          //collision detection
          //collision with food
          if (snake.LastOrDefault().X == foodX && snake.LastOrDefault().Y == foodY)
          {
            //add score
            score++;
            //spawn new food
            int x;
            int y;
            do
            {
              x = rand.Next(0, screenWidth);
              y = rand.Next(0, screenHeight);
            } while (snake.Contains(new SnakeSegment(x, y)));
            foodX = x;
            foodY = y;
          }
          else
          {
            //remove tale
            snake.RemoveAt(0);
          }

          SnakeSegment lastItem = snake.LastOrDefault();
          SnakeSegment newPart;
          switch (snakeDirection)
          {
            case 0://up
              newPart = new SnakeSegment(lastItem.X, lastItem.Y - 1);
              break;
            case 1://right
              newPart = new SnakeSegment(lastItem.X + 1, lastItem.Y);
              break;
            case 2://down
              newPart = new SnakeSegment(lastItem.X, lastItem.Y + 1);
              break;
            case 3://left
              newPart = new SnakeSegment(lastItem.X - 1, lastItem.Y);
              break;
            default:
              newPart = new SnakeSegment(lastItem.X - 1, lastItem.Y);
              break;
          }

          //collision with self
          if (snake.Contains(newPart))
          {
            dead = true;
          }
          //add front
          else
          {
            snake.Add(newPart);
          }

          //collision with walls
          if (snake.LastOrDefault().X < 0 || snake.LastOrDefault().X >= screenWidth)
          {
            dead = true;
            break;
          }
          if (snake.LastOrDefault().Y < 0 || snake.LastOrDefault().Y >= screenHeight)
          {
            dead = true;
            break;
          }

          //draw snake body
          foreach (SnakeSegment snakePart in snake)
          {
            SetCursorPosition(snakePart.X, snakePart.Y);

            if (dead)
            {
              WriteLine("x");
            }
            else
            {
              WriteLine("0");
            }

          }
          //draw snake head
          SetCursorPosition(snake.LastOrDefault().X, snake.LastOrDefault().Y);
          if (dead)
          {
            WriteLine("x");
          }
          else
          {
            WriteLine("1");
          }

          //draw food
          SetCursorPosition(foodX, foodY);
          WriteLine("F");


        }

        if (dead)
        {
          WriteLine("you are dead press space to go again");
        }
        while (Console.ReadKey().Key != ConsoleKey.Spacebar)
        {
          dead = false;
        }

      }
    }
  }
}


