using static System.Formats.Asn1.AsnWriter;

namespace Hra2048_2ITB
{
    public partial class Form1 : Form
    {
        private int size = 4;
        Number[,] numbers;
        Random generator = new Random();

        int score = 0;

        int Score {
            get { return score; }
            set {
                score = value;
                label1.Text = "Score: " + score;
            }
        }

        int[,] test = new int[4, 4] {
            { 2, 4, 2, 4 },
            { 4, 2, 4, 2 },
            { 2, 4, 8, 4 },
            { 4, 2, 2, 8 },
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
            this.Size = new Size(panel1.Width + 48, panel1.Height + 110);
        }

        private void GenerateStart() {
            AddNewNumber();
            AddNewNumber();
        }

        private bool ExistsEmptyNumber() {
            foreach (Number number in numbers) {
                if (number.CurrentValue == 0)
                    return true;
            }
            return false;
        }

        private bool MergeIsPossible() {
            if (ExistsEmptyNumber()) return true;

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {

                    if (IsInArray(i + 1, j)) {
                        if (numbers[i, j].CurrentValue == numbers[i + 1, j].CurrentValue) {
                            return true;
                        }
                    }

                    if (IsInArray(i, j + 1)) {
                        if (numbers[i, j].CurrentValue == numbers[i, j + 1].CurrentValue) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsInArray(int i, int j) {
            return i < size && j < size;
        }

        private void AddNewNumber() {
            if (!ExistsEmptyNumber())
                return;

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

        private void Form1_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A) {
                MoveNumbers(-1, 0);
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D) {
                MoveNumbers(1, 0);
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W) {
                MoveNumbers(0, -1);
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S) {
                MoveNumbers(0, 1);
            }
            AddNewNumber();
            if (!MergeIsPossible()) {
                MessageBox.Show("Prohráls!");
            }
        }

        private void MoveNumbers(int x, int y) {
            List<int> nums;
            if (y == 0) { // horizontal move
                for (int i = 0; i < size; i++) {
                    nums = new List<int>();
                    GetRow(nums, i);
                    if (x > 0) nums.Reverse();
                    MergeWhatYouCan(nums);
                    nums.AddRange(new int[size - nums.Count]);
                    if (x > 0) nums.Reverse();
                    ReturnNumbersByRow(nums, i);
                }
            } else { // vertical movement
                for (int i = 0; i < size; i++) {
                    nums = new List<int>();
                    GetColumn(nums, i);
                    if (y > 0) nums.Reverse();
                    MergeWhatYouCan(nums);
                    nums.AddRange(new int[size - nums.Count]);
                    if (y > 0) nums.Reverse();
                    ReturnNumbersByColumn(nums, i);
                }
            }
        }

        private void ReturnNumbersByRow(List<int> nums, int i) {
            for (int j = 0; j < size; j++) {
                numbers[i, j].CurrentValue = nums[j];
            }
        }

        private void ReturnNumbersByColumn(List<int> nums, int i) {
            for (int j = 0; j < size; j++) {
                numbers[j, i].CurrentValue = nums[j];
            }
        }

        private void GetColumn(List<int> nums, int i) {
            for (int j = 0; j < size; j++) {
                if (numbers[j, i].CurrentValue > 0) {
                    nums.Add(numbers[j, i].CurrentValue);
                }
            }
        }

        private void GetRow(List<int> nums, int i) {
            for (int j = 0; j < size; j++) {
                if (numbers[i, j].CurrentValue > 0) {
                    nums.Add(numbers[i, j].CurrentValue);
                }
            }
        }

        private void MergeWhatYouCan(List<int> nums) {
            for (int i = 0; i < nums.Count - 1; i++) {
                if (nums[i] == nums[i + 1]) {
                    nums[i] *= 2;
                    Score += nums[i];
                    nums.RemoveAt(i + 1);
                }
            }
        }
    }
}