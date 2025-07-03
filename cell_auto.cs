using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cellular_Automata
{
    class Program
    {
        public static int[,] world = new int[20, 20];//sor,oszlop
        public static int[,] bitmask = new int[20, 20];
        public static int sWidth = 20;
        public static int sHeight = 20;
        public static int mili_interval = 100;
		
		public static int simulation_ticks=20;
		
        static void Main(string[] args)
        {
            resetBitmask();
            setScreenSize(20, 20);
            for (int i = 0; i < sWidth; i++)
            {
                world[12, i] = 1;
            }
            for (int i = 0; i < 9; i++)
            {
                world[1+i, 1] = 1;
            }
            world[11, 11] = 1;
            drawScreen();
            Console.WriteLine("Simulation starting in 10 seconds...");
            System.Threading.Thread.Sleep(10000);
            for (int i=0;i<simulation_ticks;i++) {
                updateWorld();
                drawScreen();
                System.Threading.Thread.Sleep(mili_interval);
            }
            
            Console.ReadKey();
        }
        static void setScreenSize(int width,int height) {
            world =new int[height, width];
            sWidth = width;
            sHeight = height;
        }
        static void resetBitmask() {
            for (int i = 0; i < sHeight; i++)
            {
                for (int j = 0; j < sWidth; j++)
                {
                    bitmask[i,j]=world[i, j];
                }
            }
        }
        static void updateWorld()
        {
            resetBitmask();
            for (int i = 0; i < sHeight; i++)
            {
                for (int j = 0; j < sWidth; j++)
                {
                    switch (world[i, j])
                    {
                        case 1:
                            if (i < sHeight - 1)
                            {
                                if (world[i + 1, j] == 0)
                                {
                                    switchElement(i, j, i + 1, j);
                                }
                                else if (j > 0&& world[i + 1, j - 1] == 0)
                                {
                                    switchElement(i, j, i + 1, j - 1);
                                }
                                else if (j < sWidth - 1&& world[i + 1, j + 1] == 0)
                                {
                                    switchElement(i, j, i + 1, j + 1);
                                }
                            }
                            else {
                                bitmask[i, j] = world[i, j];
                            }
                            break;
                    }
                }
            }
            for (int i = 0; i < sHeight; i++)
            {
                for (int j = 0; j < sWidth; j++)
                {
                    world[i, j] = bitmask[i, j];
                }
            }
        }
        static void switchElement(int x1, int y1, int x2, int y2) {
            bitmask[x1, y1] = world[x2, y2];
            bitmask[x2, y2] = world[x1, y1];
        }
        static void drawScreen() {
            Console.Clear();
            for (int i=0;i<sHeight;i++) {
                for (int j=0;j<sWidth;j++) {
                    switch (world[i,j]) {
                        case 0:
                            Console.Write(" ");
                            break;
                        case 1:
                            Console.Write("█");
                            break;
                    }
                }
                Console.Write("\n");
            }
        }
    }
}