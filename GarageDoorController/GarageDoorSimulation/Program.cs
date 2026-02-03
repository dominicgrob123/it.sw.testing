using GarageDoorController;
using GarageDoorController.MotorStates;

namespace GarageDoorSimulation
{
    internal class Program
    {
        private const int REFRESH_RATE = 200;

        private const char CORNER_TOP_LEFT = '\u2554';
        private const char CORNER_TOP_RIGHT = '\u2557';
        private const char CORNER_BOTTOM_LEFT = '\u255A';
        private const char HORIZONTAL = '\u2550';
        private const char VERTICAL = '\u2551';
        private const char CORNER_BOTTOM_RIGHT = '\u255D';

        private const int DOOR_HEIGHT = 15;
        private const int DOOR_WIDTH = 20;

        static void Main(string[] args)
        {
            ConsoleKeyInfo KEY_NONE = new((char)ConsoleKey.None, ConsoleKey.None, false, false, false);

            PrintTitle();
            DrawLine();
            PrintInstructions();
            DrawLine();

            var Top = Console.CursorTop;
            var Left = Console.CursorLeft;
            Motor motor = new();
            DoorSim door = new(motor)
            {
                Position = Random.Shared.NextSingle(),
                InvocationPeriod = REFRESH_RATE,
                TimePerPercent = 100
            };
            GarageDoorController.GarageDoorController controller = new(motor);

            ConsoleKeyInfo key;
            do
            {
                key = KEY_NONE;
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey();
                }

                ProcessKey(key, controller);

                controller.Run();
                door.Move();

                PrintStatus(Left, Top, controller, motor);

                DrawLine(' ');
                DrawLine(' ');

                // Hack in order to circumvent a strange behaviour by the PowerShell console
                // in cases, where the console's height is less than the height needed for 
                // painting. The code compensates potential scroll steps.
                int expectedRow = Console.CursorTop + DOOR_HEIGHT;
                int emptyLines = DrawGarageDoor(DOOR_WIDTH, DOOR_HEIGHT, door.Position);
                int rowShift = expectedRow - Console.CursorTop;

                Top -= rowShift;
                if (Top < 0)
                {
                    Console.Clear();
                    Console.WriteLine("Window too small, please resize the window");
                    Top = 0;
                }

                CheckForEndPositions(motor, controller, emptyLines);

                if (key != KEY_NONE)
                {
                    OverwriteLastInputCharacter();
                }
                Thread.Sleep(REFRESH_RATE);
            }
            while (key.Key != ConsoleKey.Escape);
        }

        private static void PrintTitle()
        {
            Console.WriteLine("GarageDoorSimulation");
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Control the door as follows:");
            Console.WriteLine("- Press Enter to control the door");
            Console.WriteLine("- Signal end position reached through:");
            Console.WriteLine("  'u' = upper end   |   'l' = lower end");
            Console.WriteLine();
            Console.WriteLine("Note: Upper and lower end are automatically triggered if the ");
            Console.WriteLine("      animation reaches the position");
            Console.WriteLine();
            Console.WriteLine("To end the simulation by hitting ESC");
        }

        private static void OverwriteLastInputCharacter()
        {
            Console.Write(' ');
        }

        private static void ProcessKey(ConsoleKeyInfo key, GarageDoorController.GarageDoorController controller)
        {
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    controller.KeyPressed();
                    break;

                case ConsoleKey.U:
                    controller.UpperEnd();
                    break;

                case ConsoleKey.L:
                    controller.LowerEnd();
                    break;

                default:
                    break;
            }
        }

        private static void PrintStatus(int Left, int Top, GarageDoorController.GarageDoorController controller, Motor motor)
        {
            var visible = Console.CursorVisible;
            Console.CursorVisible = false;

            Console.SetCursorPosition(Left, Top);
            WriteCompleteLine($"Door State: {controller.DoorStateString}");
            WriteCompleteLine($"Motor State: {motor.MotorStateString}");

            Console.CursorVisible = visible;
        }

        private static void CheckForEndPositions(Motor motor, GarageDoorController.GarageDoorController controller, int emptyLines)
        {
            if (motor.Direction != MotorDirection.Stop)
            {
                if (emptyLines <= 0)
                {
                    controller.LowerEnd();
                }
                else if (emptyLines >= (DOOR_HEIGHT - 2))
                {
                    controller.UpperEnd();
                }
            }
        }

        private static int DrawGarageDoor(int width, int height, float position)
        {
            int numberOfEmptyLines = 0;
            var visible = Console.CursorVisible;
            Console.CursorVisible = false;
            {
                var door_pos = Math.Ceiling(height * position);

                width = width * 3; // 1 line equals approx. 3 characters

                DrawLine(CORNER_TOP_LEFT, HORIZONTAL, CORNER_TOP_RIGHT, width - 2, ConsoleColor.White, ConsoleColor.White);
                for (int row = 1; row < height - 1; row++)
                {
                    char fill = '\u2593';

                    if (door_pos < row)
                    {
                        fill = ' ';
                        numberOfEmptyLines++;
                    }

                    DrawLine(VERTICAL, fill, VERTICAL, width - 2, ConsoleColor.White, ConsoleColor.Red);
                }
                DrawLine(CORNER_BOTTOM_LEFT, HORIZONTAL, CORNER_BOTTOM_RIGHT, width - 2, ConsoleColor.White, ConsoleColor.White);
            }
            Console.CursorVisible = visible;

            return numberOfEmptyLines;
        }

        private static void WriteCompleteLine(string str)
        {
            Console.Write(str);
            ClearRestLine();
            Console.WriteLine();
        }

        private static void ClearRestLine()
        {
            FillRestLineWith(' ');
        }

        private static void DrawLine(char left, char fill, char right, int width, ConsoleColor borderColor = ConsoleColor.White, ConsoleColor fillColor = ConsoleColor.White)
        {
            var oldColor = Console.ForegroundColor;
            {
                Console.ForegroundColor = borderColor;
                Console.Write($"{left}");
                Console.ForegroundColor = fillColor;
                Console.Write($"{new string(fill, width - 2)}");
                Console.ForegroundColor = borderColor;
                Console.WriteLine($"{right}");
            }
            Console.ForegroundColor = oldColor;
        }

        private static void DrawLine(char character = '\u2550')
        {
            FillRestLineWith(character);
            Console.WriteLine();
        }

        private static void FillRestLineWith(char character)
        {
            var start = Console.GetCursorPosition();
            var rest = Console.WindowWidth - start.Left;
            var blankString = new string(character, rest);

            Console.Write(blankString);
            Console.SetCursorPosition(start.Left, start.Top);
        }
    }
}
