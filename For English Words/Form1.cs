using System;
using System.Drawing;
using System.IO;
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
            pathToSwitchIndex = $@"C:\FEW\Switch index.mw",
            pathToSizeFile = $@"C:\FEW\Number of the words.mw";

        string[] defaultWords = {
            "white","black","orange","blue","green","red","brown","gray","pink","yellow","magenta","purple",
            "maroon","advice","agree","urgently","continue","meet","rarely","colleagues","classmate","neighbors",
            "husband","wife","get","expensive","perfectly","better","mistakes","effectively","take","useful"},

            defaultTranslate = {
            "білий","чорний","помаранчевий","блакитний","зелений","червоний","коричневий","сірий","рожевий",
            "жовтий","пурпурний","фіолетовий","бордовий","порада","згоден","терміново","продовжити","зустріч",
            "рідко","колеги","однокласник","сусіди","чоловік","дружина","отримувати","дорого","прекрасно",
            "краще","помилки","ефективно","брати","корисно"};

        private int IDWords = 0, IDTranslate = 0, randomIDWord = 0,
            correctItem = 0, uncorrectItem = 0, countWordsPosition = 1,
            counter2 = 0, randomChoise = 0;

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
            //WriteNumberOfCorrectAnswers();
        }


        //--------------------------------------------------------------------------------

        // Метод запису кількості правельних відповідей
        private void WriteNumberOfCorrectAnswers()
        {
            if (!File.Exists(pathToSwitchIndex))
            {
                using (FileStream fs = new FileStream(pathToSwitchIndex, FileMode.Create)) { };
                string str1 = "";
                using (StreamReader streamReader = new StreamReader(pathToCorecctAnswerFile))
                    str1 = streamReader.ReadToEnd();
                string[] str1Array = str1.Split('\n');
                str1Array[randomIDWord] = $"{randomIDWord + 1}: {correctItem}";
                using (StreamWriter streamWriter = new StreamWriter(pathToCorecctAnswerFile))
                    for(int i = 0; i < str1Array.Length; i++)
                        if(i == 0)
                            streamWriter.Write(str1Array[i]);
                        else
                            streamWriter.Write($"\n{str1Array[i]}");
            }
            else
            {
                string str1 = "";
                using (StreamReader streamReader = new StreamReader(pathToCorecctAnswerFile))
                    str1 = streamReader.ReadToEnd();
                string[] str1Array = str1.Split('\n');
                str1Array[randomIDWord] = $"{randomIDWord + 1}: {correctItem}";
                if (randomIDWord == 0)
                    using (StreamWriter streamWriter = new StreamWriter(pathToCorecctAnswerFile))
                    {
                        for (int i = 0; i < str1Array.Length; i++)
                            if (counter2 == 0)
                            {
                                streamWriter.Write($"{str1Array[i]}");
                                counter2++;
                            }
                            else
                                streamWriter.Write($"\n{str1Array[i]}");
                    }
                else
                    using (StreamWriter streamWriter = new StreamWriter(pathToCorecctAnswerFile))
                        for (int i = 0; i < str1Array.Length; i++)
                            if(i == 0)
                                streamWriter.Write($"{str1Array[i]}");
                            else
                                streamWriter.Write($"\n{str1Array[i]}");
            }
        }

        // Метод запису кількості неправельних відповідей
        private void WriteNumberOfUncorrectAnswers()
        {
            // приклад дроблення рядка в масив та запис як двох мірної таблиці
            string str = $"{randomIDWord + 1}: {uncorrectItem}";
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
                    // запис дефолтних слів
                    foreach (string words in defaultWords)
                    {
                        if (IDWords == 0)
                            sw1.Write($"{words.ToUpper()}");
                        else
                            sw1.Write($"\n{words.ToUpper()}");
                        IDWords++;
                    }

            // Створення файлу для перекладу
            if (!File.Exists(pathToFileTranslate))
                using (StreamWriter sw2 = new StreamWriter(pathToFileTranslate))
                    // запис дефолтних перекладів
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
                using (StreamWriter sw3 = new StreamWriter(pathToCorecctAnswerFile))
                    // нумерація комірок
                    for (int i = 0; i < IDWords; i++)
                        if (i == 0)
                            sw3.Write($"{i+1}: {correctItem}");
                        else
                            sw3.Write($"\n{i+1}: {correctItem}");

            // Створення файлу для невірних відповідей
            if (!File.Exists(pathToUncorrectAnswerFile))
                using (StreamWriter sw4 = new StreamWriter(pathToUncorrectAnswerFile))
                    // нумерація комірок
                    for (int i = 0; i < IDWords; i++)
                        if (i == 0)
                            sw4.Write($"{i+1}: {uncorrectItem}");
                        else
                            sw4.Write($"\n{i+1}: {uncorrectItem}");

            // Створення файлу для перемішування відповідей
            if (!File.Exists(pathToRandomAsnwer))
                using (FileStream fs3 = new FileStream(pathToRandomAsnwer, FileMode.Create)) { };

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
            randomIDWord = random.Next(IDWords);
            string[] wordsArray = stringWord.Split('\n');
            label3.Text = wordsArray[randomIDWord];
        }

        // Метод виводу відповідей 
        private void OutputAnswer()
        {

            string stringTranslate = "";
            // Запис перекладів слів у рядок
            using (StreamReader sr1 = new StreamReader(pathToFileTranslate))
                stringTranslate = sr1.ReadToEnd();

            // Перетворення рядка перекладів слів у масив
            string[] translateArray = stringTranslate.Split('\n');

            // Запис парвельної відповіді у файл відповідей
            using (StreamWriter sw = new StreamWriter(pathToRandomAsnwer))
                sw.Write(translateArray[randomIDWord]);
            // створення масиву для збереження масиву з видаленим значенням
            string[] newTranslateArray = new string [translateArray.Length-1];
            // лічильник для рахування кількості комірок нового масиву
            int countIter = 0;
            // цикл з перевіркою для видалення потрібної комірки
            for (int i = 0; i < translateArray.Length; i++)
            {
                Deleting:
                if (i == randomIDWord)
                {
                    i++;
                    goto Deleting;
                }
                // запис у новий масив
                newTranslateArray[countIter] = translateArray[i];
                countIter++;
                // перевірка досягнутості кінця нового масиву
                if (i == newTranslateArray.Length)
                {
                    break;
                }
            }
            // доповнює запис двома випадковими відповідями у файлі відповідей
            using (StreamWriter sw = new StreamWriter(pathToRandomAsnwer, true))
            {
                for (int i = 0; i < 2; i++)
                {
                    sw.Write($"\n{newTranslateArray[random.Next(IDWords)]}");
                }
            }
            // випадковий вибір варіанту перемішуваня відповідей
            randomChoise = random.Next(6);
            // запис відповідей з перезаписаного списку відповідей без правельної відповіді у текстовий рядок
            using (StreamReader sr = new StreamReader(pathToRandomAsnwer))
                stringTranslate = sr.ReadToEnd();
            // перетворення рядку у масив
            translateArray = stringTranslate.Split('\n');
            // випадковий вибір перемішування відповідей
            switch (randomChoise)
            {
                case 0:
                    radioButton1.Text = translateArray[0];
                    radioButton2.Text = translateArray[1];
                    radioButton3.Text = translateArray[2];
                    break;
                case 1:
                    radioButton1.Text = translateArray[1];
                    radioButton2.Text = translateArray[0];
                    radioButton3.Text = translateArray[2];
                    break;
                case 2:
                    radioButton1.Text = translateArray[2];
                    radioButton2.Text = translateArray[1];
                    radioButton3.Text = translateArray[0];
                    break;
                case 3:
                    radioButton1.Text = translateArray[0];
                    radioButton2.Text = translateArray[2];
                    radioButton3.Text = translateArray[1];
                    break;
                case 4:
                    radioButton1.Text = translateArray[1];
                    radioButton2.Text = translateArray[2];
                    radioButton3.Text = translateArray[0];
                    break;
                case 5:
                    radioButton1.Text = translateArray[2];
                    radioButton2.Text = translateArray[0];
                    radioButton3.Text = translateArray[1];
                    break;
            }
        }
        // Метод перевірки вірності відповіді
        private void CheckCorrectAnswer()
        {
            // створення пустого рядку
            string str1 = "";
            // запис списку перекладів у рядок
            using (StreamReader sr4 = new StreamReader(pathToFileTranslate))
                str1 = sr4.ReadToEnd();
            // перетворення рядка у масив
            string[] corrAnswer = str1.Split('\n');
            // перевірка вибору відповіді
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