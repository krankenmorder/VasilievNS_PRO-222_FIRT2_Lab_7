using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabOOP_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

		int ctrl = 0; //вспомогательная переменная для проверки нажатия на Control
		static int index = 0;
		int indexin = 0;
		int figureSelect = 1; //выбранная фигура
		static int razmer = 50; //размер хранилища фигур
		Storage sklad = new Storage(razmer); //хранилище с заданным ранее размером

		class Figures //класс-предок всех фигур
		{
			public int x, y; //координаты объекта
			public Color color = Color.Black; //цвет фигуры по умолчанию
			public Color fillColor = Color.Red; //цвет обводки по умолчанию
		}
		class Ellipse : Figures //класс круга (класс-наследник Figures)
		{
			public int rad = 25; // радиус круга
			public Ellipse(int x, int y) //конструктор с параметрами
			{
				this.x = x - rad;
				this.y = y - rad;
			}

			~Ellipse() //деструктор
			{ 

			}
		}

		class Line : Figures //класс отрезка
		{
			public int length = 50; //длина отрезка
			public int width = 5; //ширина отрезка
			public Line(int x, int y) //конструктор с параметрами
			{
				this.x = x - length / 2;
				this.y = y;
			}

            ~Line() //деструктор
            {

            }
		}

		class Rectangle : Figures //класс прямоугольника (квадрата) 
		{
			public int width, height; //переменные ширины и высоты
			public int size = 50; //размер
			public Rectangle(int x, int y)
			{
				this.x = x - size / 2;
				this.y = y - size / 2;
				this.width = size;
				this.height = size;
			}

			~Rectangle() //деструктор
            {

            }
		}

		class Storage //класс-хранилище
		{
			public Figures[] objects; //массив класса "фигуры"

			public Storage(int count) //конструктор с параметром
			{
				objects = new Figures[count]; //задаём размер массиву
				for (int i = 0; i < count; ++i) //зануялем все ячейки массива
				{
					objects[i] = null;
				}
			}

			public void addObject(int ind, ref Figures object1, int count, ref int indexin) //функция добавления объекта в хранилище
			{
				while (objects[ind] != null)
				{
					ind = (ind + 1) % count;
				}
				objects[ind] = object1;
				indexin = ind;
			}

			public void deleteObject(int ind) //функция удаления объекта из хранилища
			{
				objects[ind] = null;
				index--;
			}

			public bool checkEmpty(int index) //проверка хранилища на пустую ячейку
			{
				if (objects[index] == null)
				{
					return true;
				}
				else
				{
					return false;
				}
			}

			public int fill(int size) //подсчёт занятых ячеек в хранилище
			{
				int countFilled = 0;
				for (int i = 0; i < size; ++i)
				{
					if (!checkEmpty(i))
					{
						++countFilled;
					}
				}
				return countFilled;
			}
			~Storage() //деструктор
			{

			} 
		};

		private void radioButtonEllipse_CheckedChanged(object sender, EventArgs e) //обработчик события проверки изменения значения RadioButton круга
		{
			figureSelect = 1;
			if (radioButtonEllipse.Checked == false) // если не выбрана фигура
			{
				figureSelect = 0;
			}
		}

		private void radioButtonLine_CheckedChanged(object sender, EventArgs e) //обработчик события проверки изменения значения RadioButton отрезка
		{
			figureSelect = 2;
			if (radioButtonLine.Checked == false) // если не выбрана фигура
			{
				figureSelect = 0;
			}
		}

		private void radioButtonRectangle_CheckedChanged(object sender, EventArgs e) //обработчик события проверки изменения значения RadioButton квадрата
		{
			figureSelect = 3;
			if (radioButtonRectangle.Checked == false) // если не выбрана фигура
			{
				figureSelect = 0;
			}
		}

		private void panelPaint_MouseClick(object sender, MouseEventArgs e) //обработчик события нажатия на панель мышкой
		{
			Figures figure = new Figures(); //создание объекта класса "Фигуры"
			switch (figureSelect) //проверка переменной figureSelect в зависимости от выбранного radioButton
			{
				case 0:
					return;
				case 1:
					figure = new Ellipse(e.X, e.Y);
					break;
				case 2:
					figure = new Line(e.X, e.Y);
					break;
				case 3:
					figure = new Rectangle(e.X, e.Y);
					break;
			}
			int check = checkFigure(ref sklad, 50, e.X, e.Y); //проверка на схожие координаты объекта

			if (check != -1)
			{
				if (Control.ModifierKeys == Keys.Control) //проверка нажатия на Control
				{
					if (ctrl == 0) //если ранее Control был нажат
					{
						paintFigure(Color.White, 5, ref sklad, indexin);
						ctrl = 1;
					}
					paintFigure(Color.Black, 5, ref sklad, check);
				}
				else // иначе выделяем только один объект
				{
					removeSelectEllipse(ref sklad); //сниммаем выделение у остальных объектов
					paintFigure(Color.Red, 4, ref sklad, check);
				}
				return;
			}
			sklad.addObject(index, ref figure, 50, ref indexin); //добавляем фигуру в хранилище
			removeSelectEllipse(ref sklad);
			sklad.objects[indexin].fillColor = colorDialog1.Color; //меняем цвет у объектов
			paintFigure(Color.Red, 4, ref sklad, indexin);
			++index; //добавляем значение в переменную количества объектов
			ctrl = 0;
		}

		private void paintFigure(Color color, int size, ref Storage storage, int index) //функция отрисовки фигуры на полотне
		{
			Pen pen = new Pen(color, size); //инициализация "карандашей" и "кистей" для рисования
			SolidBrush figureFillColor;
			if (!storage.checkEmpty(index)) //если ячейка в хранилище не пуста
			{
				storage.objects[index].color = color;
				figureFillColor = new SolidBrush(storage.objects[index].fillColor);
				if (storage.objects[index] as Ellipse != null) //если объект - круг
				{
					Ellipse ellipse = storage.objects[index] as Ellipse;
					panelPaint.CreateGraphics().DrawEllipse(pen, ellipse.x, ellipse.y, ellipse.rad * 2, ellipse.rad * 2);
					panelPaint.CreateGraphics().FillEllipse(figureFillColor, ellipse.x, ellipse.y, ellipse.rad * 2, ellipse.rad * 2);
				}
				else
				{
					if (storage.objects[index] as Line != null) //если объект - отрезок
					{
						Line line = storage.objects[index] as Line;
						panelPaint.CreateGraphics().DrawRectangle(pen, line.x, line.y, line.length, line.width);
						panelPaint.CreateGraphics().FillRectangle(figureFillColor, line.x, line.y, line.length, line.width);
					}
					else
					{
						if (storage.objects[index] as Rectangle != null) //если объект - квадрат
						{
							Rectangle rectangle = storage.objects[index] as Rectangle;
							panelPaint.CreateGraphics().DrawRectangle(pen, rectangle.x, rectangle.y, rectangle.width, rectangle.height);
							panelPaint.CreateGraphics().FillRectangle(figureFillColor, rectangle.x, rectangle.y, rectangle.width, rectangle.height);
						}
					}
				}
			}
		}

		private void paintAll(ref Storage stg) //функция отрисовки всех элементов хранилища на полотне
		{
			for (int i = 0; i < 50; ++i)
			{
				if (!stg.checkEmpty(i))
				{
					paintFigure(stg.objects[i].color, 4, ref sklad, i);
				}
			}
		}

		private void removeSelectEllipse(ref Storage stg) //удаляем выделенные элементы
		{
			for (int i = 0; i < 50; ++i)
			{
				if (!stg.checkEmpty(i))
				{
					paintFigure(Color.Red, 4, ref sklad, i);
				}
			}
		}

		private void removeSelectStorage(ref Storage stg)   // удаляем выделенные элементы из хранилища
		{
			for (int i = 0; i < 50; ++i)
			{
				if (!stg.checkEmpty(i))
				{
					if (stg.objects[i].color == Color.Black)
						stg.deleteObject(i);
				}
			}
		}

		private int checkFigure(ref Storage storage, int size, int x, int y) //проверка нахождения на заданных координатах объекта
		{
			if (storage.fill(size) != 0) //если размер занятых ячеек хранилища не равен нулю
			{
				for (int i = 0; i < size; ++i)
				{
					if (!storage.checkEmpty(i)) //если ячейка хранилища не пуста
					{
						if (storage.objects[i] as Ellipse != null) //если объект в хранилище - круг
						{
							Ellipse ellipse = storage.objects[i] as Ellipse;
							if (((x - ellipse.x - ellipse.rad) * (x - ellipse.x - ellipse.rad) + (y - ellipse.y - ellipse.rad) * (y - ellipse.y - ellipse.rad)) < (ellipse.rad * ellipse.rad))
							{
								return i; //возвращаем индекс объекта
							}
						}
						else
						{
							if (storage.objects[i] as Line != null) //если объект в хранилище - отрезок
							{
								Line line = storage.objects[i] as Line;
								if (line.x <= x && x <= (line.x + line.length) && (line.y - 2) <= y && y <= (line.y + line.width))
								{
									return i; //возвращаем индекс объекта
								}
							}
							else
							{
								if (storage.objects[i] as Rectangle != null) //если объект в хранилище - квадрат
								{
									Rectangle rectangle = storage.objects[i] as Rectangle;
									if (rectangle.x <= x && x <= (rectangle.x + rectangle.size) && rectangle.y <= y && y <= (rectangle.y + rectangle.size))
									{
										return i; //возвращаем индекс объекта
									}
								}
							}
						}

					}
				}
			}
			return -1;
		}

		private void moveY(ref Storage storage, int y) //функция переноса объекта на панели по Y
		{
			for (int i = 0; i < 50; ++i)
			{
				if (!storage.checkEmpty(i)) //если ячейка не пуста
				{
					if (storage.objects[i].color == Color.Black) //если объект выделен
					{
						if (storage.objects[i] as Ellipse != null) //если объект - круг
						{
							Ellipse ellipse = storage.objects[i] as Ellipse;
							int c = ellipse.y + y;
							int g = panelPaint.ClientSize.Height - ellipse.rad * 2;
							check(c, y, g, g - 2, ref storage.objects[i], 2); //проверка на выход за границы панели
						}
						else
						{
							if (storage.objects[i] as Line != null) //если объект - линия
							{
								Line line = storage.objects[i] as Line;
								int l = line.y + y;
								int g = panelPaint.ClientSize.Height - line.width;
								check(l, y, g, --g, ref storage.objects[i], 2); //проверка на выход за границы панели
							}
							else
							{
								if (storage.objects[i] as Rectangle != null) //если объект - квадрат
								{
									Rectangle square = storage.objects[i] as Rectangle;
									int s = square.y + y;
									int g = panelPaint.ClientSize.Height - square.size;
									check(s, y, g, --g, ref storage.objects[i], 2); //проверка на выход за границы панели
								}
							}
						}
					}
				}
			}
		}

		private void moveX(ref Storage storage, int x) //функция переноса объекта на панели по X
		{
			for (int i = 0; i < 50; ++i)
			{
				if (!storage.checkEmpty(i))
				{
					if (storage.objects[i].color == Color.Black) //если объект выделен
					{
						if (storage.objects[i] as Ellipse != null) //если объект - круг
						{
							Ellipse ellipse = storage.objects[i] as Ellipse;
							int c = ellipse.x + x;
							int g = panelPaint.ClientSize.Width - (ellipse.rad * 2);
							check(c, x, g, g - 2, ref storage.objects[i], 1); //проверка на выход за границы панели
						}
						else
						{
							if (storage.objects[i] as Line != null) //если объект - отрезок
							{
								Line line = storage.objects[i] as Line;
								int l = line.x + x;
								int g = panelPaint.ClientSize.Width - line.length;
								check(l, x, g, --g, ref storage.objects[i], 1); //проверка на выход за границы панели
							}
							else
							{
								if (storage.objects[i] as Rectangle != null)
								{
									Rectangle rectangle = storage.objects[i] as Rectangle;
									int s = rectangle.x + x;
									int g = panelPaint.ClientSize.Width - rectangle.size;
									check(s, x, g, --g, ref storage.objects[i], 1); //проверка на выход за границы панели
								}
							}
						}
					}
				}
			}
		}

		private void check(int f, int y, int g, int g1, ref Figures figures, int gr)
		{
			ref int b = ref figures.x;
			if (gr == 2)
			{
				b = ref figures.y;
			}
			if (f > 0 && f < g)
			{
				b += y;
			}
			else
			{
				if (f <= 0)
				{
					b = 1;
				}
				else
				{
					if (f >= g1)
					{ 
						b = g1;
					}
				}
			}
		}

		private void changeSize(ref Storage storage, int size) //функция изменения размера объекта
		{
			for (int i = 0; i < 50; ++i)
			{
				if (!storage.checkEmpty(i)) //если ячейка хранилища не пуста
				{
					if (storage.objects[i].color == Color.Black) //если объект выделен
					{
						if (storage.objects[i] as Ellipse != null) //если объект - круг
						{
							Ellipse ellipse = storage.objects[i] as Ellipse ;
							ellipse.rad += size;
						}
						else
						{
							if (storage.objects[i] as Line != null) //если объект - отрезок
							{
								Line line = storage.objects[i] as Line;
								line.length += size;
								line.width += size / 5;
							}
							else
							{
								if (storage.objects[i] as Rectangle != null) //если объект - квадрат
								{
									Rectangle rectangle = storage.objects[i] as Rectangle;
									rectangle.size += size;
									rectangle.width = rectangle.size;
									rectangle.height = rectangle.size;
								}
							}
						}
					}
				}
			}
		}

		private void laba6_KeyDown(object sender, KeyEventArgs e) //обработчк события нажатия на клавиши на клавиатуре
		{
			if (e.KeyCode == Keys.Delete) //удаление - (Delete)
			{
				removeSelectStorage(ref sklad);
			}
			if (e.KeyCode == Keys.W) //перемещение "вверх" - (W)
			{
				moveY(ref sklad, -10);
			}
			if (e.KeyCode == Keys.S) //перемещенеи "вниз" - (S)
			{
				moveY(ref sklad, +10);
			}
			if (e.KeyCode == Keys.A) //перемещение "влево" - (A)
			{
				moveX(ref sklad, -10);
			}
			if (e.KeyCode == Keys.D) //перемещение "вправо" - (D)
			{
				moveX(ref sklad, +10);
			}
			if (e.KeyCode == Keys.Oemplus) //увеличение фигуры - (+)
			{
				changeSize(ref sklad, 10);
			}
			if (e.KeyCode == Keys.OemMinus) //уменьшение фигуры - (-)
			{
				changeSize(ref sklad, -10);
			}
			panelPaint.Refresh(); //стираем полотно
			paintAll(ref sklad); //отрисовываем заново все элементы хранилища
		}

		private void buttonSelectColor_Click(object sender, EventArgs e) //обработчик нажатия на кнопку выбора цвета
		{
			if (colorDialog1.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}
			button1.BackColor = colorDialog1.Color; //изменяем цвет кнопки в зависимости от выбора цвета
			for (int i = 0; i < 50; ++i)
			{
				if (!sklad.checkEmpty(i)) //если ячейка не пуста
				{
					if (sklad.objects[i].color == Color.Black) //если объект выделен
					{
						sklad.objects[i].fillColor = colorDialog1.Color; //меняем цвет выделенному объекту
						paintFigure(sklad.objects[i].color, 4, ref sklad, i);
					}
				}
			}
		}


	}
}
