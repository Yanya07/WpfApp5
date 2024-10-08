using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SortingApp
{
    public partial class MainWindow : Window
    {
        public delegate void SortDelegate(int[] numbers); // Делегат для методов сортировки

        public MainWindow()
        {
            InitializeComponent();
        }
        private void BubbleSort(int[] numbers)// Метод сортировки пузырьком
        {
            int n = numbers.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (numbers[j] > numbers[j + 1])
                    {
                        // Меняем местами
                        int temp = numbers[j];
                        numbers[j] = numbers[j + 1];
                        numbers[j + 1] = temp;
                    }
                }
            }
        }
        private void QuickSort(int[] numbers)// Метод быстрой сортировки
        {
            QuickSortHelper(numbers, 0, numbers.Length - 1);
        }

        private void QuickSortHelper(int[] numbers, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(numbers, low, high);
                QuickSortHelper(numbers, low, pi - 1);
                QuickSortHelper(numbers, pi + 1, high);
            }
        }

        private int Partition(int[] numbers, int low, int high)
        {
            int pivot = numbers[high];
            int i = (low - 1);

            for (int j = low; j < high; j++)
            {
                if (numbers[j] <= pivot)
                {
                    i++;
                    int temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
            }

            int temp1 = numbers[i + 1];
            numbers[i + 1] = numbers[high];
            numbers[high] = temp1;

            return i + 1;
        }
        private void SortButton_Click(object sender, RoutedEventArgs e) // Обработка нажатия кнопки "Сортировать"
        {
            string input = NumbersTextBox.Text;  // Проверяем, что имя элемента правильно указано
            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Введите числа для сортировки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Преобразование входных данных в массив целых чисел
            int[] numbers = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                 .Select(num => int.TryParse(num.Trim(), out var n) ? n : 0)
                                 .ToArray();

            SortDelegate sortMethod;

            if (SortingMethodComboBox.SelectedIndex == 0) // Сортировка пузырьком
            {
                sortMethod = BubbleSort;
            }
            else if (SortingMethodComboBox.SelectedIndex == 1) // Быстрая сортировка
            {
                sortMethod = QuickSort;
            }
            else
            {
                MessageBox.Show("Выберите метод сортировки.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            sortMethod(numbers);// Сортировка чисел
            SortedNumbersTextBox.Text = string.Join(", ", numbers);// Отображение отсортированных чисел
        }
    }
}
