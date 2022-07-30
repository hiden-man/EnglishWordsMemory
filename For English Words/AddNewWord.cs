using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace For_English_Words
{
    public partial class AddNewWord : Form
    {
        // Поля
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;
        string pathToFileWords = $@"C:\FEW\English words.mw",
            pathToFileTranslate = $@"C:\FEW\Translate.mw",
            pathToSizeFile = $@"C:\FEW\Number of the words.mw",
            pathToCorecctAnswerFile = $@"C:\FEW\Counter of correct answer.mw";
        string strWord = "", strTranslate = "";
        private int IDWords = 0;
        
        public AddNewWord()
        {
            InitializeComponent();
        }
        private void Settings_Load(object sender, EventArgs e)
        {
            MainWindowLocation();
            SetIDWord();
        }
        private void MainWindowLocation()
        {
            Location = new Point((screenSize.Width/2)-(Size.Width/2),
                (screenSize.Height/2)-(Size.Height/2));
        }
        // Кнопка приховування вікна налаштування
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            await Task.Run(() =>
            {
                if (e.KeyValue == (char)Keys.D1 || e.KeyValue == (char)Keys.D2 ||
    e.KeyValue == (char)Keys.D3 || e.KeyValue == (char)Keys.D4 ||
    e.KeyValue == (char)Keys.D5 || e.KeyValue == (char)Keys.D6 ||
    e.KeyValue == (char)Keys.D7 || e.KeyValue == (char)Keys.D8 ||
    e.KeyValue == (char)Keys.D9 || e.KeyValue == (char)Keys.D0 ||
    e.KeyValue == (char)Keys.NumPad1 || e.KeyValue == (char)Keys.NumPad2 ||
    e.KeyValue == (char)Keys.NumPad3 || e.KeyValue == (char)Keys.NumPad4 ||
    e.KeyValue == (char)Keys.NumPad5 || e.KeyValue == (char)Keys.NumPad6 ||
    e.KeyValue == (char)Keys.NumPad7 || e.KeyValue == (char)Keys.NumPad8 ||
    e.KeyValue == (char)Keys.NumPad9 || e.KeyValue == (char)Keys.NumPad0)
                {
                    MessageBox.Show(
                        "You used a numeric character!", 
                        "Warning", 
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            });
        }

        async void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            await Task.Run(() =>
            {
                if (e.KeyValue == (char)Keys.D1 || e.KeyValue == (char)Keys.D2 ||
    e.KeyValue == (char)Keys.D3 || e.KeyValue == (char)Keys.D4 ||
    e.KeyValue == (char)Keys.D5 || e.KeyValue == (char)Keys.D6 ||
    e.KeyValue == (char)Keys.D7 || e.KeyValue == (char)Keys.D8 ||
    e.KeyValue == (char)Keys.D9 || e.KeyValue == (char)Keys.D0 ||
    e.KeyValue == (char)Keys.NumPad1 || e.KeyValue == (char)Keys.NumPad2 ||
    e.KeyValue == (char)Keys.NumPad3 || e.KeyValue == (char)Keys.NumPad4 ||
    e.KeyValue == (char)Keys.NumPad5 || e.KeyValue == (char)Keys.NumPad6 ||
    e.KeyValue == (char)Keys.NumPad7 || e.KeyValue == (char)Keys.NumPad8 ||
    e.KeyValue == (char)Keys.NumPad9 || e.KeyValue == (char)Keys.NumPad0)
                {
                    MessageBox.Show(
                        "You used a numeric character!",
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            });
        }

        // Метод встановлення кількості англійських слів у файлі
        private void SetIDWord()
        {
            using (StreamReader sr = new StreamReader(pathToSizeFile))
                IDWords = Convert.ToInt32(sr.ReadLine());
        }
        // Кнопка запису слів та перекладу
        private void button2_Click(object sender, EventArgs e)
        {
            WriteWordsAndTranslate();
            label3.Font = new Font("Microsoft Sans Serif", 
                20.25F, FontStyle.Bold, 
                GraphicsUnit.Point, ((byte)(204)));
            label3.Text = "saved";
            label3.ForeColor = Color.LimeGreen;
            textBox1.Text = "";
            textBox2.Text = "";
        }
        // Метод створення файлу та запис кількості англійських слів 
        public void SaveNumberOfSize()
        {
            using (StreamWriter sw = new StreamWriter(pathToSizeFile))
                sw.Write(IDWords);
        }
        // Метод запису нового слова та перекладу у файли
        // та збільшення числа слів на один
        public void WriteWordsAndTranslate()
        {
            strWord = textBox1.Text;
            strTranslate = textBox2.Text;

            string[] strWordArray = strWord.Split(' ');
            string[] strTranslateArray = strTranslate.Split(' ');

            using (StreamWriter sw1 = new StreamWriter(pathToFileWords, true))
                for (int i = 0; i < strWordArray.Length; i++)
                {
                    sw1.Write($"\n{strWordArray[i]}");
                    IDWords++;
                }
            using (StreamWriter sw2 = new StreamWriter(pathToFileTranslate, true))
                for (int i = 0; i < strTranslateArray.Length; i++)
                    sw2.Write($"\n{strTranslateArray[i]}");
            using (StreamWriter sw3 = new StreamWriter(pathToCorecctAnswerFile, true))
                for (int i = 0; i < strWordArray.Length; i++)
                    sw3.Write($"\n{0}");
            SaveNumberOfSize();
        }
    }

}