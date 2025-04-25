namespace laba15
{
    public partial class Form1 : Form
    {


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            movingButton.Location = new Point(100, 120);
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            running = false;
        }

        private Button movingButton;
        private ComboBox comboBox;

        private Thread moveThread;
        private Thread calculateThread;

        private int dx = 5;
        private int dy = 5;

        private object locker = new object();

        private bool running = true;

        private int counter = 0;
        public Form1()
        {
            InitializeComponent();
            StartThreads();
        }

        public void StartThreads()
        {
            moveThread = new Thread(MoveButton);
            calculateThread = new Thread(Calculate);

            moveThread.IsBackground = true;
            calculateThread.IsBackground=true;

            moveThread.Priority = ThreadPriority.Highest;
            calculateThread.Priority = ThreadPriority.Normal;

            moveThread.Start();
            calculateThread.Start();
        }

        private void MoveButton()
        {
            while (running)
            {
                int localDx, localDy;
                lock (locker)
                {
                    localDx = dx;
                    localDy = dy;
                }

                if (movingButton.InvokeRequired)
                { 
                    movingButton.Invoke(new Action(() =>
                    {
                        int newX = movingButton.Left + localDx;
                        int newY = movingButton.Top + localDy;

                        if (newX > this.ClientSize.Width)
                        {
                            newX = -movingButton.Width;
                        }

                        else if (newX + movingButton.Width < 0)
                        {
                            newX = this.ClientSize.Width;
                        }

                        if (newY > this.ClientSize.Height)
                        {
                            newY = -movingButton.Height;
                        }

                        else if (newY + movingButton.Height < 0)
                        {
                            newY = this.ClientSize.Height;
                        }

                        movingButton.Location = new Point(newX, newY);
                    }));
                }
                Thread.Sleep(30);
            }
        }


        private void Calculate()
        {
            string rule;
            while (running)
            {
                if (comboBox.InvokeRequired)
                {
                    rule = comboBox.Invoke(new Func<string>(() => comboBox.SelectedItem?.ToString()));


                    lock (locker)
                    {
                        counter++;
                        switch (rule)
                        {
                            case "не перемещаться":
                                dx = 0;
                                dy = 0;
                                break;
                            case "по прямой":
                                dx = 5;
                                dy = 0;
                                break;
                            case "sin(x)":
                                dx = 5;
                                dy = (int) (5 * Math.Sin(counter * 0.1));
                                break;
                            case "cos(x)":
                                dx = 5;
                                dy = (int)(5 * Math.Cos(counter * 0.1));
                                break;
                            default:
                                dx = 0;
                                dy = 0;
                                break;
                        }
                    }
                }

                Thread.Sleep (30);
            }
        }
    }
}
