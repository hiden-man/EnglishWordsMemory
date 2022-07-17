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
            pathToFileWords = $@"C:\FEW\English words.xlsx",
            pathToFileTranslate = $@"C:\FEW\Translate.xlsx",
            pathCorecctAnswerFile = $@"C:\FEW\Counter of correct answer.xlsx",
            pathToUncorrectAnswerFile = $@"C:\FEW\Counter of uncorrect answer.xlsx",
            pathToRandomAsnwer = $@"C:\FEW\Random answer.xlsx",
            pathToSizeFile = $@"C:\FEW\Number of the words.mw";

        string[] defaultWords = {"white","black","orange",
            "blue","creen","red","brown","gray","pink",
            "yellow","magenta","purple","maroon" },

            defaultTranslate = {"білий","чорний","помаранчевий",
            "блакитний","зелений","червоний","коричневий","сірий","рожевий",
            "жовтий","пурпуровий","фіолетовий","бордовий"},
            forMixAnswer = new string[3];

        // номер рядка слова
        public int IDWords = 0, randomID = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            MainWindowLocation();
            CreateDirectoryAndFiles();
            SetIDWord();
            OutputRandomWord();
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
                Excell excel3 = new Excell();
                excel3.CreateNewFile();
                excel3.SaveAs(pathToFileWords);
                excel3.CloseFile();
                Excell exc2 = new Excell(pathToFileWords, 1);
                for (int i = 0; i < defaultWords.Length; i++)
                {
                    exc2.WriteToCell(i, 0, defaultWords[i].ToUpper());
                    IDWords++;
                }
                exc2.Save();
                exc2.CloseFile();
            }
            // Створення файлу для перекладу
            if (!File.Exists(pathToFileTranslate))
            {
                Excell exc1 = new Excell();
                exc1.CreateNewFile();
                exc1.SaveAs(pathToFileTranslate);
                exc1.CloseFile();
                Excell exc3 = new Excell(pathToFileTranslate, 1);
                for (int i = 0; i < defaultTranslate.Length; i++)
                {
                    exc3.WriteToCell(i, 0, defaultTranslate[i].ToUpper());
                }
                exc3.Save();
                exc3.CloseFile();
            }
            // Створення ексель файлу для вірних відповідей
            if (!File.Exists(pathCorecctAnswerFile))
            {
                Excell excel = new Excell();
                excel.CreateNewFile();
                excel.SaveAs(pathCorecctAnswerFile);
                excel.CloseFile();
            }
            // Створення ексель файлу для невірних відповідей
            if (!File.Exists(pathToUncorrectAnswerFile))
            {
                Excell xlsx = new Excell();
                xlsx.CreateNewFile();
                xlsx.SaveAs(pathToUncorrectAnswerFile);
                xlsx.CloseFile();
            }
            // Створення ексель файлу для перемішування відповідей
            if (!File.Exists(pathToRandomAsnwer))
            {
                Excell excell1 = new Excell();
                excell1.CreateNewFile();
                excell1.SaveAs(pathToRandomAsnwer);
                excell1.CloseFile();
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
            randomID = random.Next(IDWords);
            Excell ex1 = new Excell(pathToFileWords, 1);
            label3.Text = ex1.ReadCell(randomID, 0);
            ex1.CloseFile();
            Excell ex2 = new Excell(pathToFileTranslate, 1);
            if (randomID == 0)
            {
                radioButton1.Text = ex2.ReadCell(randomID, 0);
                radioButton2.Text = ex2.ReadCell(randomID + 1, 0);
                radioButton3.Text = ex2.ReadCell(randomID + 2, 0);
            }
            else if (randomID == IDWords)
            {
                radioButton1.Text = ex2.ReadCell(randomID - 2, 0);
                radioButton2.Text = ex2.ReadCell(randomID - 1, 0);
                radioButton3.Text = ex2.ReadCell(randomID, 0);
            }
            else
            {
                radioButton1.Text = ex2.ReadCell(randomID - 1, 0);
                radioButton2.Text = ex2.ReadCell(randomID, 0);
                radioButton3.Text = ex2.ReadCell(randomID + 1, 0);
            }
            ex2.CloseFile();
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
    //--------------------------------------------------------------------------------------------------
    class Excell
    {
        // простий конструктор 
        public Excell()
        {

        }
        // змінна для зберігання шляху до файлу
        string path = "";

        _Application excel = new _Excel.Application();
        Workbook wb;
        Worksheet ws;
        // конструктор класу який приймає параметри (шлях та номер листа)
        public Excell(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];

        }
        // функція для зчитування данних з файлу
        public string ReadCell(int i, int j)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
                return ws.Cells[i, j].Value2;
            else
                return "";
        }
        // запис в комірку
        public void WriteToCell(int i, int j, string s)
        {
            i++;
            j++;
            ws.Cells[i, j].Value2 = s;
        }
        // зберігання файлу
        public void Save()
        {
            wb.Save();
        }
        // зберігання файлу с іншим іменем
        public void SaveAs(string path1)
        {
            wb.SaveAs(path1);
        }
        // закриття файлу
        public void CloseFile()
        {
            wb.Close();
        }
        // створення нового файлу
        public void CreateNewFile()
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
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