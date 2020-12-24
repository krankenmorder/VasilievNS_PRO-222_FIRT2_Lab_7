
namespace LabOOP_6
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelPaint = new System.Windows.Forms.Panel();
            this.groupBoxChoose = new System.Windows.Forms.GroupBox();
            this.radioButtonLine = new System.Windows.Forms.RadioButton();
            this.radioButtonRectangle = new System.Windows.Forms.RadioButton();
            this.radioButtonEllipse = new System.Windows.Forms.RadioButton();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.buttonColor = new System.Windows.Forms.Button();
            this.buttonGroup = new System.Windows.Forms.Button();
            this.buttonUnGroup = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.groupBoxChoose.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelPaint
            // 
            this.panelPaint.BackColor = System.Drawing.Color.White;
            this.panelPaint.Location = new System.Drawing.Point(12, 12);
            this.panelPaint.Name = "panelPaint";
            this.panelPaint.Size = new System.Drawing.Size(600, 610);
            this.panelPaint.TabIndex = 0;
            this.panelPaint.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelPaint_MouseClick);
            // 
            // groupBoxChoose
            // 
            this.groupBoxChoose.Controls.Add(this.radioButtonLine);
            this.groupBoxChoose.Controls.Add(this.radioButtonRectangle);
            this.groupBoxChoose.Controls.Add(this.radioButtonEllipse);
            this.groupBoxChoose.Location = new System.Drawing.Point(618, 12);
            this.groupBoxChoose.Name = "groupBoxChoose";
            this.groupBoxChoose.Size = new System.Drawing.Size(252, 105);
            this.groupBoxChoose.TabIndex = 1;
            this.groupBoxChoose.TabStop = false;
            this.groupBoxChoose.Text = "Выбор элемента рисования:";
            // 
            // radioButtonLine
            // 
            this.radioButtonLine.AutoSize = true;
            this.radioButtonLine.Location = new System.Drawing.Point(7, 77);
            this.radioButtonLine.Name = "radioButtonLine";
            this.radioButtonLine.Size = new System.Drawing.Size(85, 21);
            this.radioButtonLine.TabIndex = 3;
            this.radioButtonLine.TabStop = true;
            this.radioButtonLine.Text = "Отрезок";
            this.radioButtonLine.UseVisualStyleBackColor = true;
            this.radioButtonLine.CheckedChanged += new System.EventHandler(this.radioButtonLine_CheckedChanged);
            // 
            // radioButtonRectangle
            // 
            this.radioButtonRectangle.AutoSize = true;
            this.radioButtonRectangle.Location = new System.Drawing.Point(7, 50);
            this.radioButtonRectangle.Name = "radioButtonRectangle";
            this.radioButtonRectangle.Size = new System.Drawing.Size(84, 21);
            this.radioButtonRectangle.TabIndex = 1;
            this.radioButtonRectangle.TabStop = true;
            this.radioButtonRectangle.Text = "Квадрат";
            this.radioButtonRectangle.UseVisualStyleBackColor = true;
            this.radioButtonRectangle.CheckedChanged += new System.EventHandler(this.radioButtonRectangle_CheckedChanged);
            // 
            // radioButtonEllipse
            // 
            this.radioButtonEllipse.AutoSize = true;
            this.radioButtonEllipse.Location = new System.Drawing.Point(7, 22);
            this.radioButtonEllipse.Name = "radioButtonEllipse";
            this.radioButtonEllipse.Size = new System.Drawing.Size(58, 21);
            this.radioButtonEllipse.TabIndex = 0;
            this.radioButtonEllipse.TabStop = true;
            this.radioButtonEllipse.Text = "Круг";
            this.radioButtonEllipse.UseVisualStyleBackColor = true;
            this.radioButtonEllipse.CheckedChanged += new System.EventHandler(this.radioButtonEllipse_CheckedChanged);
            // 
            // buttonColor
            // 
            this.buttonColor.Location = new System.Drawing.Point(618, 123);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(252, 37);
            this.buttonColor.TabIndex = 5;
            this.buttonColor.Text = "Вызвать палитру цветов";
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonSelectColor_Click);
            // 
            // buttonGroup
            // 
            this.buttonGroup.Location = new System.Drawing.Point(618, 166);
            this.buttonGroup.Name = "buttonGroup";
            this.buttonGroup.Size = new System.Drawing.Size(252, 37);
            this.buttonGroup.TabIndex = 6;
            this.buttonGroup.Text = "Группировка";
            this.buttonGroup.UseVisualStyleBackColor = true;
            this.buttonGroup.Click += new System.EventHandler(this.buttonGroup_Click);
            // 
            // buttonUnGroup
            // 
            this.buttonUnGroup.Location = new System.Drawing.Point(618, 209);
            this.buttonUnGroup.Name = "buttonUnGroup";
            this.buttonUnGroup.Size = new System.Drawing.Size(252, 37);
            this.buttonUnGroup.TabIndex = 7;
            this.buttonUnGroup.Text = "Разгруппировка";
            this.buttonUnGroup.UseVisualStyleBackColor = true;
            this.buttonUnGroup.Click += new System.EventHandler(this.buttonUnGroup_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(618, 252);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(252, 37);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(618, 295);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(252, 37);
            this.buttonLoad.TabIndex = 0;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 634);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonUnGroup);
            this.Controls.Add(this.buttonGroup);
            this.Controls.Add(this.buttonColor);
            this.Controls.Add(this.groupBoxChoose);
            this.Controls.Add(this.panelPaint);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.laba6_KeyDown);
            this.groupBoxChoose.ResumeLayout(false);
            this.groupBoxChoose.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPaint;
        private System.Windows.Forms.GroupBox groupBoxChoose;
        private System.Windows.Forms.RadioButton radioButtonLine;
        private System.Windows.Forms.RadioButton radioButtonRectangle;
        private System.Windows.Forms.RadioButton radioButtonEllipse;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonColor;
        private System.Windows.Forms.Button buttonGroup;
        private System.Windows.Forms.Button buttonUnGroup;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonLoad;
    }
}

