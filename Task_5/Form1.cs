namespace Task_5
{
    public partial class Form1 : Form
    {
        BindingSource bind = new BindingSource();
        List<Book> books = new List<Book>()
        {
        new Book("Champion","Rob"),
        new Book("Fight Club","Chuck"),
        new Book("You","Caroline"),

        };
        public Form1()
        {
            InitializeComponent();
            Load += new System.EventHandler(Form1_load);
        }

        public void Form1_load(object sender, EventArgs e)
        {
            bind.DataSource = books;
            listBox1.DataSource = bind;
        }

        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public Book(string title, string author)
            {
                Title = title;
                Author = author;
            }

            public override string ToString()
            {
                return $"{Title}-{Author}";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            books.Add(new Book("New", "Unknown"));
            bind.ResetBindings(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int num = listBox1.SelectedIndex;
            string text = textBox1.Text;

            books[num].Title = text;
            bind.ResetBindings(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            books.RemoveAt(listBox1.SelectedIndex);
            bind.ResetBindings(false);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.Title = "Выберите файл для импорта данных";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                List<Book> importedBooks = new List<Book>();
                try
                {
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('-');
                        if (parts.Length == 2)
                        {
                            string title = parts[0];
                            string author = parts[1];
                            importedBooks.Add(new Book(title, author));
                        }
                    }

                    books.AddRange(importedBooks);
                    bind.ResetBindings(false); 

                    MessageBox.Show("Данные успешно импортированы из файла.", "Импорт данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при импорте данных из файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.Title = "Выберите место для сохранения файла";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (Book book in books)
                        {
                            writer.WriteLine($"{book.Title}-{book.Author}");
                        }
                    }

                    MessageBox.Show("Данные успешно сохранены в файл.", "Сохранение данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных в файл: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}