﻿using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace For_English_Words
{
    public partial class Settings : Form
    {
        // Поля
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;
        string pathToFileWords = $@"C:\FEW\English words.mw",
            pathToFileTranslate = $@"C:\FEW\Translate.mw",
            pathToSizeFile = $@"C:\FEW\Number of the words.mw",
            pathToCorecctAnswerFile = $@"C:\FEW\Counter of correct answer.mw",
            pathToUncorrectAnswerFile = $@"C:\FEW\Counter of uncorrect answer.mw";
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
                sw.WriteLine(IDWords);
        }
        // Метод запису нового слова та перекладу у файли
        // та збільшення числа слів на один
        public void WriteWordsAndTranslate()
        {
            using (StreamWriter sw1 = new StreamWriter(pathToFileWords, true))
                sw1.Write($"\n{textBox1.Text.ToLower()}");
            using (StreamWriter sw2 = new StreamWriter(pathToFileTranslate, true))
                sw2.Write($"\n{textBox2.Text.ToLower()}");
            using (StreamWriter sw3 = new StreamWriter(pathToCorecctAnswerFile, true))
                sw3.Write($"\n{IDWords}: {0}");
            using (StreamWriter sw4 = new StreamWriter(pathToUncorrectAnswerFile, true))
                sw4.Write($"\n{IDWords}: {0}");
            IDWords++;
            SaveNumberOfSize();
        }
    }

}