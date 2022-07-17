using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace For_English_Words
{
    public partial class Settings : Form
    {
        // Поля
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;
        string pathToFileWords = $@"C:\FEW\English words.xlsx",
            pathToFileTranslate = $@"C:\FEW\Translate.xlsx",
            pathToSizeFile = $@"C:\FEW\Number of the words.mw";

        private int IDWords = 0;

        public Settings()
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
            Location = new Point((screenSize.Width/2)-(Size.Width/2),0);
        }

        // Кнопка приховування вікна налаштування
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
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
        }
        // Метод створення файлу та запис кількості англійських слів 
        public void SaveNumberOfSize()
        {
            using (StreamWriter sw = new StreamWriter(pathToSizeFile))
                sw.WriteLine(IDWords);
        }

        // Метод запису нового слова та перекладу у файли
        // та збільшення числа слів на один
        public void WriteWordsAndTranslate()
        {
            Excell excel2 = new Excell(pathToFileWords, 1);
            excel2.WriteToCell(IDWords, 0, textBox2.Text.ToUpper());
            excel2.Save();
            excel2.CloseFile();
            Excell excel3 = new Excell(pathToFileTranslate, 1);
            excel3.WriteToCell(IDWords, 0, textBox1.Text.ToUpper());
            excel3.Save();
            excel3.CloseFile();
            IDWords++;
            SaveNumberOfSize();
        }
    }

}
