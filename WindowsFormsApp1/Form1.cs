using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
       
      
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.createMap();
            createSnake();
            createFood();
            this.timer1.Start() ;

        }

        int mapY = 30;
        int mapX = 60;
        Label[,] mapArray;
        List<int> snakeX = new List<int>() { 0,1,2,3,4,5,6,7,8,9,10};
        List<int> snakeY = new List<int>() { 0,0,0,0,0,0,0,0,0,0,0};
        int foodX;
        int foodY;
        
        private void createMap()
        {
            mapArray = new Label[mapY, mapX];
            for(int y = 0; y < mapY; y++)
            {
                for(int x = 0; x < mapX; x++)
                {
                    Label lab = new Label();
                    lab.Width = 18;
                    lab.Height = 18;
                    lab.BackColor = Color.LightBlue;
                    lab.Margin=new Padding(1);
                    this.flowLayoutPanel1.Controls.Add(lab);
                    mapArray[y, x] = lab;
                }
            }
        }

        private void createSnake()
        {
            for(int i = 0; i < snakeX.Count; i++)
            {
                int x = snakeX[i];
                int y = snakeY[i];
                mapArray[y, x].BackColor = Color.Blue; 
            }
        }

        private void clearSnake()
        {
            for (int i = 0; i < snakeX.Count; i++)
            {
                int x = snakeX[i];
                int y = snakeY[i];
                mapArray[y, x].BackColor = Color.LightBlue;
            }
        }

        private void createFood()
        {
            bool result;
            do
            {
                result = false;
                Random num = new Random();
                foodX = num.Next(mapX);
                foodY = num.Next(mapY);
                for(int i = 0; i < snakeX.Count; i ++)
                {
                    if (snakeX[i] == foodX && snakeY[i] == foodY)
                    {
                        result = true;
                    }
                }
                mapArray[foodY, foodX].BackColor = Color.Red;
            } while (result);
        }

        string keyCase = "D";//-------------------------------
        
        private void snakeMove()
        {
            clearSnake();
            for(int i = 0; i < snakeX.Count-1; i++)
            {
                snakeX[i] = snakeX[i + 1];
                snakeY[i] = snakeY[i + 1];
               
            }
            switch (keyCase)
            {
                case "A":
                case "Left":
                    snakeX[snakeY.Count - 1]--;
                    break;
                case "W":
                case "Up":
                    snakeY[snakeY.Count - 1]--;
                    break;
                case "D":
                case "Right":
                    snakeX[snakeY.Count - 1]++;
                    break;
                case "S":
                case "Down":
                    snakeY[snakeY.Count - 1]++;
                    break;
            }

            if (eatFood()) {
                snakeX.Add(0);
                snakeY.Add(0);
                for (int i = snakeY.Count - 1; i > 0; i--)
                {
                    snakeX[i] = snakeX[i - 1];
                    snakeY[i] = snakeY[i - 1];
                }
                createFood();
            };

            for (int i = 0; i < snakeY.Count-1; i++)
            {
                if(snakeX [snakeY.Count-1]==snakeX[i]&& snakeY[snakeY.Count - 1] == snakeY[i])
                {
                    this.timer1.Stop();
                    MessageBox.Show("Game Over");
                    return;
                }
            }
            for(int i = 0; i < snakeX.Count - 1; i++)
            {
                if (snakeX[snakeX.Count - 1] ==snakeX[i]&& snakeY[snakeX.Count - 1] == snakeY[i])
                {
                    this.timer1.Stop();
                    MessageBox.Show("Game Over");
                    return;
                }
            }

            if(snakeX[snakeY.Count - 1] < 0 || snakeY[snakeY.Count - 1] < 0 || snakeX[snakeY.Count - 1] >mapX-1 || snakeY[snakeY.Count - 1] >mapY-1)
            {
                this.timer1.Stop();
                MessageBox.Show("Game Over");
                return;
            }
            createSnake();
        }

        private bool eatFood()
        {
            if(snakeX[snakeX.Count-1]==foodX&& snakeY[snakeY.Count - 1] == foodY)
            {
                return true;
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            snakeMove();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
;            string newKey = e.KeyCode.ToString();
            List<string> list = new List<string>() { "A", "Left", "W", "Up", "D", "Right", "S", "Down" };
            if (!list.Contains(e.KeyCode.ToString()))
            {
                return;
            }
            if (
                keyCase == "A" && newKey == "D" ||
                keyCase == "D" && newKey == "A" ||
                keyCase == "W" && newKey == "S" ||
                keyCase == "S" && newKey == "W"||
                keyCase == "Up" && newKey == "Down" ||
                keyCase == "Right" && newKey == "Left" ||
                keyCase == "Left" && newKey == "Right" ||
                keyCase == "Down" && newKey == "Up"
            )
            {
                return;
            }

            keyCase = newKey;
        }

       
    }
}
