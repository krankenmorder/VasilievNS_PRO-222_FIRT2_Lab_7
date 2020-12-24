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
		static int index = 0; //кол-во нарисованных объектов
		static int indexin = 0;
		int figureSelect = 1; //выбранная фигура
		static int razmer = 50; //размер хранилища фигур
		Storage sklad = new Storage(razmer); //хранилище с заданным ранее размером

		class Figures //класс-предок всех фигур
		{
			public int x, y; //координаты объекта
			public Color color = Color.Black; //цвет фигуры по умолчанию
			public Color fillColor = Color.Red; //цвет обводки по умолчанию

			public Figures() //конструктор по умолчанию
            {

            }

			public virtual void addGroup(ref Figures object1)
            {

            }

			public virtual void deleteGroup(ref Storage storage, int c)
            {

            }

			public virtual void paintFigure(Pen pen, Panel panelPaint) 
			{ 

			}
			public virtual void moveX(int x, Panel panelPaint) 
			{

			}
			public virtual void moveY(int y, Panel panelPaint)
			{

			}
			public virtual void changeSize(int size) 
			{ 

			}
			public virtual bool checkFigure(int x, int y) 
			{ 
				return false;
			}
			public virtual void setColor(Color color)
			{

			}

		}

		class Group: Figures //класс группы
        {
			public int count; 
			public int maxCount = 5; //максимальное кол-во объектов в группе
			public Figures[] group; //массив группы

			public Group() //выделяем ячейки для хранилища группы и зануляем их
			{   
				count = 0;
				group = new Figures[maxCount];
				for (int i = 0; i < maxCount; ++i)
				{
					group[i] = null;
				}
			}

			public override void addGroup(ref Figures object1) //метод добавления объектов в группу
			{
				if (count >= maxCount)
				{
					return;
				}
				group[count] = object1;
				++count;
			}

			public override void deleteGroup(ref Storage storage, int c) //метод разгруппировки объектов
			{
				storage.deleteObject(c);
				for (int i = 0; i < count; ++i)
				{
					storage.addObject(index, ref group[i], 50, ref indexin);
				}
			}

			public override void paintFigure(Pen pen, Panel panelPaint)
			{ 
				for (int i = 0; i < count; ++i)
				{
					group[i].paintFigure(pen, panelPaint);
				}
			}

			public override void moveX(int x, Panel panelPaint)
			{
				for (int i = 0; i < count; ++i)
				{
					group[i].moveX(x, panelPaint);
				}
			}
			public override void moveY(int y, Panel panelPaint)
			{
				for (int i = 0; i < count; ++i)
				{
					group[i].moveY(y, panelPaint);
				}
			}
			public override void changeSize(int size)
			{
				for (int i = 0; i < count; ++i)
				{
					group[i].changeSize(size);
				}
			}
			public override bool checkFigure(int x, int y)
			{
				for (int i = 0; i < count; ++i)
				{
					if (group[i].checkFigure(x, y))
					{
						return true;
					}
				}
				return false;
			}
			public override void setColor(Color color)
			{
				for (int i = 0; i < count; ++i)
				{
					group[i].setColor(color);
				}
			}
		}

		class Ellipse : Figures //класс круга (класс-наследник Figures)
		{
			public int rad = 25; // радиус круга
			public Ellipse(int x, int y) //конструктор с параметрами
			{
				this.x = x - rad;
				this.y = y - rad;
			}

			public override void paintFigure(Pen pen, Panel panelPaint)
			{
				SolidBrush figurefillcolor = new SolidBrush(fillColor);
				panelPaint.CreateGraphics().DrawEllipse(pen, x, y, rad * 2, rad * 2);
				panelPaint.CreateGraphics().FillEllipse(figurefillcolor, x, y, rad * 2, rad * 2);
			}

			public override void moveX(int x, Panel panelPaint)
			{
				int c = this.x + x;
				int g = panelPaint.ClientSize.Width - (rad * 2);
				check(c, x, g, g - 2, ref this.x);
			}

			public override void moveY(int y, Panel panel_drawing)
			{
				int c = this.y + y;
				int g = panel_drawing.ClientSize.Height - (rad * 2);
				check(c, y, g, g - 2, ref this.y);
			}

			public override void changeSize(int size)
			{
				rad += size;
			}

			public override bool checkFigure(int x, int y)
			{
				return ((x - this.x - rad) * (x - this.x - rad) + (y - this.y - rad) *	(y - this.y - rad)) < (rad * rad);
			}

			public override void setColor(Color color)
			{
				fillColor = color;
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

			public override void paintFigure(Pen pen, Panel panelPaint)
			{
				SolidBrush figurefillcolor = new SolidBrush(fillColor);
				panelPaint.CreateGraphics().DrawRectangle(pen, x, y, length, width);
				panelPaint.CreateGraphics().FillRectangle(figurefillcolor, x, y, length, width);
			}

			public override void moveX(int x, Panel panelPaint)
			{
				int l = this.x + x;
				int g = panelPaint.ClientSize.Width - length;
				check(l, x, g, --g, ref this.x);
			}

			public override void moveY(int y, Panel panel_drawing)
			{
				int l = this.y + y;
				int gran = panel_drawing.ClientSize.Height - width;
				check(l, y, gran, --gran, ref this.y);
			}

			public override void changeSize(int size)
			{
				length += size;
			}

			public override bool checkFigure(int x, int y)
			{
				return (this.x <= x && x <= (this.x + length) && (this.y - 2) <= y &&
									y <= (this.y + width));
			}

			public override void setColor(Color color)
			{
				fillColor = color;
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

			public override void paintFigure(Pen pen, Panel panelPaint)
			{
				SolidBrush figurefillcolor = new SolidBrush(fillColor);
				panelPaint.CreateGraphics().DrawRectangle(pen, x, y, size, size);
				panelPaint.CreateGraphics().FillRectangle(figurefillcolor,
					x, y, size, size);
			}

			public override void moveX(int x, Panel panel_drawing)
			{
				int s = this.x + x;
				int gran = panel_drawing.ClientSize.Width - size;
				check(s, x, gran, --gran, ref this.x);
			}

			public override void moveY(int y, Panel panel_drawing)
			{
				int s = this.y + y;
				int gran = panel_drawing.ClientSize.Height - size;
				check(s, y, gran, --gran, ref this.y);
			}

			public override void changeSize(int size)
			{
				this.size += size;
			}

			public override bool checkFigure(int x, int y)
			{
				return (this.x <= x && x <= (this.x + size) && this.y <= y && y <= (this.y + size));
			}

			public override void setColor(Color color)
			{
				fillColor = color;
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
				if (index > 0)
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
			if (!storage.checkEmpty(index))
			{
				Pen pen = new Pen(color, size);
				storage.objects[index].color = color;
				storage.objects[index].paintFigure(pen, panelPaint);
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
			if (storage.fill(size) != 0)
			{
				for (int i = 0; i < size; ++i)
				{
					if (!storage.checkEmpty(i))
					{  
						if (storage.objects[i].checkFigure(x, y))
							return i;
					}
				}
			}
			return -1;
		}

		private void moveY(ref Storage storage, int y) //функция переноса объекта на панели по Y
		{
			for (int i = 0; i < 50; ++i)
			{
				if (!storage.checkEmpty(i))
				{
					if (storage.objects[i].color == Color.Black)
					{
						storage.objects[i].moveY(y, panelPaint);
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
					if (storage.objects[i].color == Color.Black)
					{
						storage.objects[i].moveX(x, panelPaint);
					}
				}
			}
		}

		static private void check(int f, int chislo, int gran, int gran1, ref int x) //проверка выхода за пределы полотна
		{
			if (f > 0 && f < gran)
			{
				x += chislo;
			}
			else
			{
				if (f <= 0)
				{
					x = 1;
				}
				else
				{
					if (f >= gran1)
					{
						x = gran1;
					}
				}
			}
		}

		private void changeSize(ref Storage storage, int size) //функция изменения размера объекта
		{
			for (int i = 0; i < 50; ++i)
			{
				if (!storage.checkEmpty(i)) //если ячейка не пуста
				{
					if (storage.objects[i].color == Color.Black) //если объект выделен
					{
						storage.objects[i].changeSize(size);
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
			buttonColor.BackColor = colorDialog1.Color; //изменяем цвет кнопки в зависимости от выбора цвета
			for (int i = 0; i < 50; ++i)
			{
				if (!sklad.checkEmpty(i)) //если ячейка не пуста
				{
					if (sklad.objects[i].color == Color.Black) //если объект выделен
					{
						sklad.objects[i].setColor(colorDialog1.Color); //меняем цвет выделенному объекту
						paintFigure(sklad.objects[i].color, 4, ref sklad, i);
					}
				}
			}
		}

        private void buttonGroup_Click(object sender, EventArgs e) //обработчик события нажатия на кнопку "Группировка"
        {
			Figures group = new Group();
			for (int i = 0; i < 50; ++i)
			{
				if (!sklad.checkEmpty(i)) //если ячейка не пуста
				{
					if (sklad.objects[i].color == Color.Black) //если элемент выделен
					{
						group.addGroup(ref sklad.objects[i]); //добавляем объект в группу
						sklad.deleteObject(i); //удаляем объект из основного склада
					}
				}
			}
			sklad.addObject(index, ref group, 50, ref indexin); //добавляем в основной склад объекты группы
		}

        private void buttonUnGroup_Click(object sender, EventArgs e)
        {
			for (int i = 0; i < 50; ++i)
			{
				if (!sklad.checkEmpty(i)) //если ячейка не пуста
				{
					if (sklad.objects[i].color == Color.Black) //если объект выделен
					{
						sklad.objects[i].deleteGroup(ref sklad, i); //удаляем группу
						return;
					}
				}
			}
		}
    }
}
