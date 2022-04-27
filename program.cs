using System;

using static System.Console;
using System.Collections.Generic;
using System.Linq;

namespace snake
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
      while (true)
      {
        //static
        //create snake
        List<SnakeSegment> snake = new List<SnakeSegment>();
        snake.Add(new SnakeSegment(4, 5));
        snake.Add(new SnakeSegment(4, 6));
        snake.Add(new SnakeSegment(4, 7));

        int score = 0;
        bool dead = false;

        int foodX = 5;
        int foodY = 5;

        int snakeDirection = 1;
        CursorVisible = false;

        int startScreenHeight = 3;
        int startScreenWidth = 3;

        int screenHeight = 20;
        int screenWidth = 60;

        int endScreenHeight = screenHeight + startScreenHeight;
        int endScreenWidth = screenWidth + startScreenWidth;
        Clear();
        //draw upper border
        SetCursorPosition(startScreenWidth - 1, startScreenHeight - 1);
        BackgroundColor = ConsoleColor.Red;
        WriteLine($"{String.Concat(Enumerable.Repeat(" ", screenWidth + 2))}");
        //draw lower border
        SetCursorPosition(startScreenWidth - 1, endScreenHeight);
        BackgroundColor = ConsoleColor.Red;
        WriteLine($"{String.Concat(Enumerable.Repeat(" ", screenWidth + 2))}");

        for (int line = startScreenHeight; line < endScreenHeight; line++)
        {
          //draw left border
          SetCursorPosition(startScreenWidth - 1, line);
          BackgroundColor = ConsoleColor.Red;
          WriteLine(" ");
          //drawright border
          SetCursorPosition(endScreenWidth, line);
          BackgroundColor = ConsoleColor.Red;
          WriteLine(" ");
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
            Console.BackgroundColor = ConsoleColor.Blue;
            WriteLine(" ");
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
          Console.BackgroundColor = ConsoleColor.Green;
          WriteLine(" ");
        }
        //draw food
        Console.BackgroundColor = ConsoleColor.DarkYellow;
        SetCursorPosition(foodX, foodY);
        WriteLine(" ");

        Console.BackgroundColor = ConsoleColor.Black;

        //draw score board
        SetCursorPosition(endScreenWidth + 2, startScreenHeight);
        WriteLine($"score {score}");
        //draw rules/tutorial
        SetCursorPosition(endScreenWidth + 2, startScreenHeight + 1);
        WriteLine($"use the keys to navigate:");
        SetCursorPosition(endScreenWidth + 2, startScreenHeight + 2);
        WriteLine($"   ^   ");
        SetCursorPosition(endScreenWidth + 2, startScreenHeight + 3);
        WriteLine($"<     >");
        SetCursorPosition(endScreenWidth + 2, startScreenHeight + 4);
        WriteLine($"   ↓   ");

        //wait for key press
        ReadKey();

        while (!dead)
        {
          Console.BackgroundColor = ConsoleColor.Black;
          //timer
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
            //random
            Random rand = new Random();
            do
            {
              x = rand.Next(startScreenWidth, endScreenWidth);
              y = rand.Next(startScreenHeight, endScreenHeight);
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
          if (snake.LastOrDefault().X < startScreenWidth || snake.LastOrDefault().X >= endScreenWidth)
          {
            dead = true;
            break;
          }
          if (snake.LastOrDefault().Y < startScreenHeight || snake.LastOrDefault().Y >= endScreenHeight)
          {
            dead = true;
            break;
          }
          //clear screen
          Clear();
          //upper border
          SetCursorPosition(startScreenWidth - 1, startScreenHeight - 1);
          BackgroundColor = ConsoleColor.Red;
          WriteLine($"{String.Concat(Enumerable.Repeat(" ", screenWidth + 2))}");
          //lower border
          SetCursorPosition(startScreenWidth - 1, endScreenHeight);
          BackgroundColor = ConsoleColor.Red;
          WriteLine($"{String.Concat(Enumerable.Repeat(" ", screenWidth + 2))}");

          for (int line = startScreenHeight; line < endScreenHeight; line++)
          {
            //left border
            SetCursorPosition(startScreenWidth - 1, line);
            BackgroundColor = ConsoleColor.Red;
            WriteLine(" ");
            //right border
            SetCursorPosition(endScreenWidth, line);
            BackgroundColor = ConsoleColor.Red;
            WriteLine(" ");
          }
          //draw snake body
          foreach (SnakeSegment snakePart in snake)
          {
            SetCursorPosition(snakePart.X, snakePart.Y);
            Console.BackgroundColor = ConsoleColor.Blue;
            WriteLine(" ");
          }
          //draw snake head
          SetCursorPosition(snake.LastOrDefault().X, snake.LastOrDefault().Y);
          Console.BackgroundColor = ConsoleColor.Green;
          WriteLine(" ");
          //draw food
          SetCursorPosition(foodX, foodY);
          Console.BackgroundColor = ConsoleColor.DarkYellow;
          WriteLine(" ");
          //draw score board
          SetCursorPosition(endScreenWidth + 2, startScreenHeight);
          WriteLine($"score {score}");

          Console.BackgroundColor = ConsoleColor.Black;

          //draw rules/tutorial
          SetCursorPosition(endScreenWidth + 2, startScreenHeight + 1);
          WriteLine($"use the arrow keys to navigate");
          SetCursorPosition(endScreenWidth + 2, startScreenHeight + 2);
          WriteLine($"   ↑   ");
          SetCursorPosition(endScreenWidth + 2, startScreenHeight + 3);
          WriteLine($"←     →");
          SetCursorPosition(endScreenWidth + 2, startScreenHeight + 4);
          WriteLine($"   ↓   ");
        }
        if (dead)
        {
          //draw snake body
          foreach (SnakeSegment snakePart in snake)
          {
            SetCursorPosition(snakePart.X, snakePart.Y);
            Console.BackgroundColor = ConsoleColor.Blue;
            WriteLine("x");
          }
          //draw snake head
          SetCursorPosition(snake.LastOrDefault().X, snake.LastOrDefault().Y);
          Console.BackgroundColor = ConsoleColor.Green;
          WriteLine("x");

          Console.BackgroundColor = ConsoleColor.Black;

          //score output
          SetCursorPosition(0, endScreenHeight + 2);
          WriteLine($"your score is: {score}");
          WriteLine("you lost :( press space to go again");
        }

        //wait for spacebar to restart game
        while (Console.ReadKey().Key != ConsoleKey.Spacebar)
        {
        }
        dead = false;
      }
    }
  }
}


