using Microsoft.Office.Interop.Excel;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using _Excel = Microsoft.Office.Interop.Excel;

namespace For_English_Words
{
    public partial class Form1 : Form
    {
        // Поля
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;
        Random random = new Random();
        Settings settingsWindow = new Settings();

        string defaultPath = @"C:\FEW",
            pathToFileWords = $@"C:\FEW\English words.mw",
            pathToFileTranslate = $@"C:\FEW\Translate.mw",
            pathToCorecctAnswerFile = $@"C:\FEW\Counter of correct answer.mw",
            pathToUncorrectAnswerFile = $@"C:\FEW\Counter of uncorrect answer.mw",
            pathToRandomAsnwer = $@"C:\FEW\Random answer.mw",
            pathToSizeFile = $@"C:\FEW\Number of the words.mw";

        string[] defaultWords = {"white","black","orange",
            "blue","creen","red","brown","gray","pink",
            "yellow","magenta","purple","maroon" },

            defaultTranslate = {"білий","чорний","помаранчевий",
            "блакитний","зелений","червоний","коричневий","сірий","рожевий",
            "жовтий","пурпуровий","фіолетовий","бордовий"},
            forMixAnswer = new string[3];

        // номер рядка слова
        public int IDWords = 0, randomID = 0, randomiC = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MainWindowLocation();
            CreateDirectoryAndFiles();
            SetIDWord();
            //OutputRandomWord();
        }

        //--------------------------------------------------------------------------------

        private void MainWindowLocation()
        {
            Location = new System.Drawing.Point((screenSize.Width / 2) - (Size.Width / 2), 0);
        }

        // Метод встановлення кількості англійських слів у файлі
        private void SetIDWord()
        {
            using (StreamReader sr = new StreamReader(pathToSizeFile))
                IDWords = Convert.ToInt32(sr.ReadLine());
        }

        // Метод створення директорії та неохідних файлів
        private void CreateDirectoryAndFiles()
        {
            // Створення дерикторії
            Directory.CreateDirectory(defaultPath);

            // перевірка на навність необхідних файлів
            // Створення файлу для слів
            if (!File.Exists(pathToFileWords))
            {
                using (StreamWriter sw1 = new StreamWriter(pathToFileWords))
                {
                    sw1.Write("English Words:");
                    foreach(string words in defaultWords)
                    {
                        sw1.Write($"\n{words}");
                        IDWords++;
                    }

                }
                
            }
            // Створення файлу для перекладу
            if (!File.Exists(pathToFileTranslate))
            {
                using (StreamWriter sw2 = new StreamWriter(pathToFileTranslate))
                {
                    sw2.Write("Translate:");
                    foreach (string translate in defaultTranslate)
                        sw2.Write($"\n{translate}");
                }
            }
            // Створення ексель файлу для вірних відповідей
            if (!File.Exists(pathToCorecctAnswerFile))
            {
                using (FileStream fs1 = new FileStream(pathToCorecctAnswerFile, 
                    FileMode.Create)){};  
            }
            // Створення ексель файлу для невірних відповідей
            if (!File.Exists(pathToUncorrectAnswerFile))
            {
                using (FileStream fs2 = new FileStream(pathToUncorrectAnswerFile,
                    FileMode.Create)){};
            }
            // Створення ексель файлу для перемішування відповідей
            if (!File.Exists(pathToRandomAsnwer))
            {
                using (FileStream fs3 = new FileStream(pathToRandomAsnwer, 
                    FileMode.Create)){};
            }
            // Запис кількості слів у текстовий файл
            if (!File.Exists(pathToSizeFile))
                SaveNumberOfSize();
        }

        // Метод створення файлу та запис кількості англійських слів
        public void SaveNumberOfSize()
        {
            using (StreamWriter sw = new StreamWriter(pathToSizeFile))
                sw.WriteLine(IDWords);
        }

        // Метод випадкової вибірки слова із списку
        private void OutputRandomWord()
        {
            
        }
        
        // записати три відповіді для перемішуання

        //-------------------------------------------------------------------------------------------

        // Кнопка налаштування
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            settingsWindow.Show();
        }

        // Close button
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

//// запис всіх існуючих дисків у системі в список
//DriveInfo[] ListDrives = DriveInfo.GetDrives();
//foreach (DriveInfo driveInfo in ListDrives)
//    if (driveInfo.IsReady)
//        comboBox1.Items.Add(driveInfo.Name);
//comboBox1.SelectedIndex = 0;


//DialogResult messageResult = MessageBox.Show(
//                "Do you want the program to run with Windows?",
//                "Question",
//                MessageBoxButtons.YesNo,
//                MessageBoxIcon.Question);
//if (messageResult == DialogResult.Yes)
//{
//    MessageBox.Show("You pressed the Yes button", "Random", MessageBoxButtons.OK);
//}
//else
//{
//    MessageBox.Show("You pressed the No button", "Random", MessageBoxButtons.OK);
//}