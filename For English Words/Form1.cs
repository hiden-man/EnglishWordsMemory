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
            pathToRandomAsnwer = $@"C:\FEW\Random answer.mw",
            pathToSwitchIndex = $@"C:\FEW\Switch index.mw",
            pathToSizeFile = $@"C:\FEW\Number of the words.mw";

        string[] defaultWords = {
            "white","black","orange","blue","green","red","brown","gray","pink","yellow","magenta","purple",
            "maroon","advice","agree","urgently","continue","meet","rarely","colleagues","classmate","neighbors",
            "husband","wife","get","expensive","perfectly","better","mistakes","effectively","take","useful",
            "workers","offer","ticket","mean","explain","speak","spend","strange","grow","garden","suppliers",
            "situation","answer","clients","hate","swim","promise","refuse"},

            defaultTranslate = {
            "білий","чорний","помаранчевий","блакитний","зелений","червоний","коричневий","сірий","рожевий",
            "жовтий","пурпурний","фіолетовий","бордовий","порада","згоден","терміново","продовжити","зустріч",
            "рідко","колеги","однокласник","сусіди","чоловік","дружина","отримувати","дорого","прекрасно",
            "краще","помилки","ефективно","брати","корисно","робітники","пропозиція","білет","означати",
            "пояснювати","говорити","проводити","дивний","вирощувати","сад","постачальники","ситуація","відповідь",
            "кліенти","ненавидіти","плавати","обіцяти","відмова"};

        private int 
            IDWords = 0, IDTranslate = 0, randomIDWord = 0,
            correctItem = 0, randomChoise = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainWindowLocation();
            Repetition();
        }
        // МЕТОДИ !!!!!
        //---------------------------------------------------------------------------------------------------------
        // Метод повтору
        private void Repetition()
        {
            CreateDirectoryAndFiles();
            SetIDWord();
            OutputRandomWord();
            OutputAnswer();
        }
        //---------------------------------------------------------------------------------------------------------
        private void MainWindowLocation()
        {
            Location = new Point((screenSize.Width / 2) - (Size.Width / 2), 0);
        }
        //---------------------------------------------------------------------------------------------------------
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
                            sw1.Write($"{words.ToLower()}");
                        else
                            sw1.Write($"\n{words.ToLower()}");
                        IDWords++;
                    }

            // Створення файлу для перекладу
            if (!File.Exists(pathToFileTranslate))
                using (StreamWriter sw2 = new StreamWriter(pathToFileTranslate))
                    // запис дефолтних перекладів
                    foreach (string translate in defaultTranslate) 
                    { 
                        if (IDTranslate == 0)
                            sw2.Write($"{translate.ToLower()}");
                        else
                            sw2.Write($"\n{translate.ToLower()}");
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

            // Створення файлу для перемішування відповідей
            if (!File.Exists(pathToRandomAsnwer))
                using (FileStream fs3 = new FileStream(pathToRandomAsnwer, FileMode.Create)) { };

            // Запис кількості слів у текстовий файл
            if (!File.Exists(pathToSizeFile))
                SaveNumberOfSize();
        }
        //---------------------------------------------------------------------------------------------------------
        // Метод створення файлу та запис кількості англійських слів
        public void SaveNumberOfSize()
        {
            using (StreamWriter sw = new StreamWriter(pathToSizeFile))
                sw.Write($"{IDWords}");
            SetIDWord();
        }
        //---------------------------------------------------------------------------------------------------------
        // Метод встановлення кількості англійських слів у файлі
        private void SetIDWord()
        {
            using (StreamReader sr = new StreamReader(pathToSizeFile))
                IDWords = Convert.ToInt32(sr.ReadLine());
        }
        //---------------------------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------------------
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
                if (i < translateArray.Length)
                {
                    newTranslateArray[countIter] = translateArray[i]; // ?????????!!!!!!!!!!!!!
                    countIter++;
                }
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
                    sw.Write($"\n{newTranslateArray[random.Next(IDWords-1)]}");
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
        //---------------------------------------------------------------------------------------------------------
        // Метод запису кількості правельних відповідей
        private void WriteNumberOfCorrectAnswers()
        {
            if (!File.Exists(pathToSwitchIndex))
            {
                using (FileStream fs = new FileStream(pathToSwitchIndex, FileMode.Create)) { };
                string str1 = "", str2 = "";
                using (StreamReader streamReader = new StreamReader(pathToCorecctAnswerFile))
                    str1 = streamReader.ReadToEnd();
                string[] str1Array = str1.Split('\n');

                // запис у окремий рядок потрібної комірки
                for (int i = 0; i < str1Array.GetLength(0); i++)
                {
                    // перевірка досягнутості потрібної комірки
                    if (i == randomIDWord)
                    {
                        // запис значень потрібної комірки у тексовий рядок
                        str2 = str1Array[randomIDWord];
                        // зупинка циклу
                        break;
                    }
                    else
                        // пропущення всіх інших комірок
                        continue;
                }
                // запис другого рядка у окремий масив
                string[] strArray2 = str2.Split(' ');
                // конвертація вказаної комірки в числовий формат, зміна та запис у змінну числового типу
                int digital = Convert.ToInt32(strArray2[1]) + 1;
                // оновлення вказаної комірки
                strArray2[1] = digital.ToString();
                // цикл який проходить по всій довжині першого масиву
                for (int i = 0; i < str1Array.GetLength(0); i++)
                {
                    // перевірка досягнутості потрібної комірки
                    if (i != randomIDWord)
                    {
                        continue;
                    }
                    else
                    {
                        // цикл який проходить по всій довжині другого масиву
                        for (int j = 0; j < strArray2.GetLength(0); j++)
                        {
                            // перезапис потрібної комірки в першому масиві зміненою коміркою другого масиву
                            if (j == 0)
                                str1Array[i] = strArray2[j];
                            else
                                str1Array[i] += $" {strArray2[j]}";
                        }
                    }
                }
                using (StreamWriter sw = new StreamWriter(pathToCorecctAnswerFile))
                {
                    for (int i = 0; i < str1Array.GetLength(0); i++)
                    {
                        if (i == 0)
                            sw.Write(str1Array[i]);
                        else
                            sw.Write($"\n{str1Array[i]}");
                    }
                }
            }
            //--------------------------------------------------------------
            else
            {
                string str1 = "", str2 = "";
                using (StreamReader streamReader = new StreamReader(pathToCorecctAnswerFile))
                    str1 = streamReader.ReadToEnd();
                string[] str1Array = str1.Split('\n');

                // запис у окремий рядок потрібної комірки
                for (int i = 0; i < str1Array.GetLength(0); i++)
                {
                    // перевірка досягнутості потрібної комірки
                    if (i == randomIDWord)
                    {
                        // запис значень потрібної комірки у тексовий рядок
                        str2 = str1Array[randomIDWord];
                        // зупинка циклу
                        break;
                    }
                    else
                        // пропущення всіх інших комірок
                        continue;
                }
                // запис другого рядка у окремий масив
                string[] strArray2 = str2.Split(' ');
                // конвертація вказаної комірки в числовий формат, зміна та запис у змінну числового типу
                int digital = Convert.ToInt32(strArray2[1]) + 1;
                // оновлення вказаної комірки
                strArray2[1] = digital.ToString();
                // цикл який проходить по всій довжині першого масиву
                for (int i = 0; i < str1Array.GetLength(0); i++)
                {
                    // перевірка досягнутості потрібної комірки
                    if (i != randomIDWord)
                    {
                        continue;
                    }
                    else
                    {
                        // цикл який проходить по всій довжині другого масиву
                        for (int j = 0; j < strArray2.GetLength(0); j++)
                        {
                            // перезапис потрібної комірки в першому масиві зміненою коміркою другого масиву
                            if (j == 0)
                                str1Array[i] = strArray2[j];
                            else
                                str1Array[i] += $" {strArray2[j]}";
                        }
                    }
                }

                using (StreamWriter sw = new StreamWriter(pathToCorecctAnswerFile))
                {
                    for (int i = 0; i < str1Array.GetLength(0); i++)
                    {
                        if (i == 0)
                            sw.Write(str1Array[i]);
                        else
                            sw.Write($"\n{str1Array[i]}");
                    }
                }

                //for (int i = 0; i < str1Array.GetLength(0); i++)
                //{
                //    if (i == 0)
                //        richTextBox1.Text = str1Array[i];
                //    else
                //        richTextBox1.Text += $"\n{str1Array[i]}";
                //}

                //string str1 = "";
                //using (StreamReader streamReader = new StreamReader(pathToCorecctAnswerFile))
                //    str1 = streamReader.ReadToEnd();
                //string[] str1Array = str1.Split('\n');
                //str1Array[randomIDWord] = $"{randomIDWord + 1}: {1}";
                //if (randomIDWord == 0)
                //{
                //    int counter2 = 0;
                //    using (StreamWriter streamWriter = new StreamWriter(pathToCorecctAnswerFile))
                //    {
                //        for (int i = 0; i < str1Array.Length; i++)
                //            if (counter2 == 0)
                //            {
                //                streamWriter.Write($"{str1Array[i]}");
                //                counter2++;
                //            }
                //            else
                //                streamWriter.Write($"\n{str1Array[i]}");
                //    }
                //}
                //else
                //    using (StreamWriter streamWriter = new StreamWriter(pathToCorecctAnswerFile))
                //        for (int i = 0; i < str1Array.Length; i++)
                //            if (i == 0)
                //                streamWriter.Write($"{str1Array[i]}");
                //            else
                //                streamWriter.Write($"\n{str1Array[i]}");
            }
        }
        //---------------------------------------------------------------------------------------------------------
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
        // КОНТРОЛЕРИ
        //-------------------------------------------------------------------------------------------
        // Кнопка відповіді
        private void button5_Click(object sender, EventArgs e)
        {
            CheckCorrectAnswer();
            WriteNumberOfCorrectAnswers();
        }
        //---------------------------------------------------------------------------------------------------------
        // Кнопка налаштування
        private void button3_Click(object sender, EventArgs e)
        {
            settingsWindow.Show();
        }
        //---------------------------------------------------------------------------------------------------------
        // Кнопка оновлення
        private void button4_Click(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.RosyBrown;
            radioButton2.ForeColor = Color.RosyBrown;
            radioButton3.ForeColor = Color.RosyBrown;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            Repetition();
        }
        //---------------------------------------------------------------------------------------------------------
        // Close button
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}