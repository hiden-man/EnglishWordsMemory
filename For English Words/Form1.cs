using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
            "жовтий","пурпурний","фіолетовий","бордовий"};

        // номер рядка слова
        private int IDWords = 0, IDTranslate = 0, randomIDWord = 0, 
            correctItem = 0, uncorrectItem = 0, countWordsPosition = 1;

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
            OutputAnswer();
            WriteNumberOfCorrectAndUncorrectAnswers();
        }


        //--------------------------------------------------------------------------------

        // Метод запису кількості правильних відповідей
        private void WriteNumberOfCorrectAndUncorrectAnswers()
        {
            // приклад дроблення рядка в масив та запис як двох мірної таблиці
            string str = $"{randomIDWord+1}: {random.Next(0,101)}";
            string[] strArray = str.Split(' ');
            for (int i = 0; i < strArray.Length; i++)
            {
                if ((countWordsPosition % 2) != 0)
                    richTextBox1.Text += $"{strArray[i]} ";
                if ((countWordsPosition % 2) == 0)
                    richTextBox1.Text += $"{strArray[i]}\n";
                countWordsPosition++;
            }
        }

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

            // Перевірка на навність необхідних файлів
            // Створення файлу для слів
            if (!File.Exists(pathToFileWords))
                using (StreamWriter sw1 = new StreamWriter(pathToFileWords))
                {
                    foreach (string words in defaultWords)
                    {
                        if (IDWords == 0)
                            sw1.Write($"{words.ToUpper()}");
                        else
                            sw1.Write($"\n{words.ToUpper()}");
                        IDWords++;
                    }
                }

            // Створення файлу для перекладу
            if (!File.Exists(pathToFileTranslate))
                using (StreamWriter sw2 = new StreamWriter(pathToFileTranslate))
                    foreach (string translate in defaultTranslate)
                    {
                        if (IDTranslate == 0)
                            sw2.Write($"{translate.ToUpper()}");
                        else
                            sw2.Write($"\n{translate.ToUpper()}");
                        IDTranslate++;
                    }



            // Створення файлу для вірних відповідей
            if (!File.Exists(pathToCorecctAnswerFile))
                using (FileStream fs1 = new FileStream(pathToCorecctAnswerFile, FileMode.Create)){};

            // Створення файлу для невірних відповідей
            if (!File.Exists(pathToUncorrectAnswerFile))
                using (FileStream fs2 = new FileStream(pathToUncorrectAnswerFile, FileMode.Create)){};

            // Створення файлу для перемішування відповідей
            if (!File.Exists(pathToRandomAsnwer))
                using (FileStream sw3 = new FileStream(pathToRandomAsnwer, FileMode.Create)) { };

            // Запис кількості слів у текстовий файл
            if (!File.Exists(pathToSizeFile))
            SaveNumberOfSize();
        }

        // Метод створення файлу та запис кількості англійських слів
        public void SaveNumberOfSize()
        {
            using (StreamWriter sw = new StreamWriter(pathToSizeFile))
                sw.Write($"{IDWords}");
            SetIDWord();
        }

        // Метод випадкової вибірки слова із списка 
        private void OutputRandomWord()
        {
            string stringWord = "";

            using (StreamReader sr1 = new StreamReader(pathToFileWords))
                stringWord = sr1.ReadToEnd();
            randomIDWord = random.Next(0, IDWords);
            string[] wordsArray = stringWord.Split('\n');
            label3.Text = wordsArray[randomIDWord];
        }

        // Метод виводу відповідей 
        private void OutputAnswer()
        {
            using (FileStream fs3 = new FileStream(pathToRandomAsnwer,FileMode.Create)) { };

            string stringTranslate = "";
            // Запис перекладів слів у рядок
            using (StreamReader sr1 = new StreamReader(pathToFileTranslate))
                stringTranslate = sr1.ReadToEnd();

            // Перетворення рядка перекладів слів у масив
            string[] translateArray = stringTranslate.Split('\n');
            // Запис відповідей у файл відповідей
            using (StreamWriter sw1 = new StreamWriter(pathToRandomAsnwer, true))
            {
                if (randomIDWord <= 0)
                {
                    sw1.WriteLine($"{translateArray[randomIDWord]}");
                    sw1.WriteLine($"{translateArray[randomIDWord + 1]}");
                    sw1.Write($"{translateArray[randomIDWord + 2]}");
                }
                if (randomIDWord == IDWords)
                {
                    sw1.WriteLine($"{translateArray[randomIDWord-1]}");
                    sw1.WriteLine($"{translateArray[randomIDWord - 2]}");
                    sw1.Write($"{translateArray[randomIDWord - 3]}");
                }
                if(randomIDWord > 0 & randomIDWord < IDWords)
                {
                    sw1.WriteLine($"{translateArray[randomIDWord - 1]}");
                    sw1.WriteLine($"{translateArray[randomIDWord + 1]}");
                    sw1.Write($"{translateArray[randomIDWord]}");
                }
            }
            // Запис відповідей із файла у рядок
            using (StreamReader sr2 = new StreamReader(pathToRandomAsnwer))
                stringTranslate = sr2.ReadToEnd();
            // Перетворення текстового рядку у масив
            translateArray = stringTranslate.Split('\n');
            // Генерація випадкових неповторних чисел
            random = new Random(DateTime.Now.Millisecond);
            // Перемішування комірок в масиві
            translateArray = translateArray.OrderBy(x => random.Next()).ToArray();
            // Вивод відповідей
            radioButton1.Text = translateArray[0];
            radioButton2.Text = translateArray[1];
            radioButton3.Text = translateArray[2];
        }

        private void CheckCorrectAnswer()
        {
            string str1 = "";
            using (StreamReader sr4 = new StreamReader(pathToFileTranslate))
                str1 = sr4.ReadToEnd();
            string[] corrAnswer = str1.Split('\n');

            if (radioButton1.Checked)
            {
                if (radioButton1.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.LimeGreen;
                    radioButton2.ForeColor = Color.Red;
                    radioButton3.ForeColor = Color.Red;
                }
                if(radioButton2.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.Red;
                    radioButton2.ForeColor = Color.LimeGreen;
                    radioButton3.ForeColor = Color.Red;
                }
                if (radioButton3.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.Red;
                    radioButton3.ForeColor = Color.LimeGreen;
                    radioButton2.ForeColor = Color.Red;
                }

            }

            if (radioButton2.Checked)
            {
                if (radioButton1.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.LimeGreen;
                    radioButton2.ForeColor = Color.Red;
                    radioButton3.ForeColor = Color.Red;
                }
                if (radioButton2.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.Red;
                    radioButton2.ForeColor = Color.LimeGreen;
                    radioButton3.ForeColor = Color.Red;
                }
                if (radioButton3.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.Red;
                    radioButton3.ForeColor = Color.LimeGreen;
                    radioButton2.ForeColor = Color.Red;
                }
            }
            if (radioButton3.Checked)
            {
                if (radioButton1.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.LimeGreen;
                    radioButton2.ForeColor = Color.Red;
                    radioButton3.ForeColor = Color.Red;
                }
                if (radioButton2.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.Red;
                    radioButton2.ForeColor = Color.LimeGreen;
                    radioButton3.ForeColor = Color.Red;
                }
                if (radioButton3.Text == corrAnswer[randomIDWord])
                {
                    radioButton1.ForeColor = Color.Red;
                    radioButton3.ForeColor = Color.LimeGreen;
                    radioButton2.ForeColor = Color.Red;
                }
            }

        }

        //-------------------------------------------------------------------------------------------

        // Кнопка відповіді
        private void button2_Click(object sender, EventArgs e)
        {
            CheckCorrectAnswer();
        }

        // Кнопка налаштування
        private void button3_Click(object sender, EventArgs e)
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

//// перетворення рядка в масив слів
// string str = "hi I am a superman"
//// поділ рядка по символу пробілу
// string[] strArray = str.Split(' ');
// foreach (var words in strArray)
//      Console.WriteLine(words);

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