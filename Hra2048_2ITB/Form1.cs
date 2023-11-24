namespace Hra2048_2ITB
{
    public partial class Form1 : Form
    {
        private int size = 4;
        Number[,] numbers;
        Random generator = new Random();

        int[,] test = new int[4, 4] {
            { 2, 2, 2, 2 },
            { 2, 2, 4, 0 },
            { 16, 16, 4, 0 },
            { 0, 0, 4, 4 },
        };

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            CreateNumbers();
            SetupSizes();
            GenerateStart();

            SetupTest();
        }

        private void CreateNumbers() {
            numbers = new Number[size, size];

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Number n = new Number();
                    n.Setup(i, j);
                    numbers[j, i] = n; // << tady je i a j otoèené!
                    panel1.Controls.Add(n);
                }
            }
        }

        private void SetupSizes() {
            int numberSize = numbers[0, 0].Width;

            panel1.Size = new Size(numberSize * size, numberSize * size);
            this.Size = new Size(panel1.Width + 48, panel1.Height + 80);
        }

        private void GenerateStart() {
            AddNewNumber();
            AddNewNumber();
        }

        private void AddNewNumber() {
            int x, y;
            do {
                x = generator.Next(0, size);
                y = generator.Next(0, size);
            } while (numbers[x, y].CurrentValue != 0);

            numbers[x, y].CurrentValue = generator.Next(10) < 3 ? 4 : 2;
        }

        private void SetupTest() {
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    numbers[j, i].CurrentValue = test[j, i];
                }
            }
        }

    }
}