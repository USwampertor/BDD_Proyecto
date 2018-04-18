using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDD_2
{
    public partial class Form2 : Form
    {
        public int form1state = -1;
        public TextBox
            t1Name = new TextBox(),
            t2Name = new TextBox(),
            t3Email = new TextBox(),
            t3Pass = new TextBox();
        public NumericUpDown
            t1Object = new NumericUpDown(),
            t1Weird = new NumericUpDown(),
            t1Ability = new NumericUpDown(),
            t2Type = new NumericUpDown();
        public Form1.ItemStruct itmcapsule = new Form1.ItemStruct();
        public Form1.ReceiptStruct rcpcapsule = new Form1.ReceiptStruct();
        public Form1.AccountStruct acccapsule = new Form1.AccountStruct();
        public Form2(int state)
        {
            InitializeComponent();
            InitializeDataEntry(state);
        }
        public Form2(Form1.ItemStruct edit,int state)
        {
            InitializeComponent();
            InitializeDataEntry(state);
            EditData(edit);
        }
        public Form2(Form1.ReceiptStruct edit, int state)
        {
            InitializeComponent();
            InitializeDataEntry(state);
            EditData(edit);
        }
        public Form2(Form1.AccountStruct edit, int state)
        {
            InitializeComponent();
            InitializeDataEntry(state);
            EditData(edit);
        }
        public void EditData(Form1.ItemStruct edit)
        {
            t1Object.Text = edit.objecttype.ToString();
            t1Weird.Text = edit.weirdness.ToString();
            t1Ability.Text = edit.ability.ToString();
            t1Name.Text = edit.name;
            this.Text = "Editar item";
        }
        public void EditData(Form1.ReceiptStruct edit)
        {
            t2Type.Value = edit.type;
            t2Name.Text = edit.name;
            this.Text = "Editar receta";

        }
        public void EditData(Form1.AccountStruct edit)
        {
            t3Email.Text = edit.email;
            t3Pass.Text = edit.password;
            this.Text = "Editar cuenta";
        }
        public void InitializeDataEntry(int state)
        {
            form1state = state;
            this.ClientSize = new System.Drawing.Size(300, 300);
            switch(state)
            {
                case -1:
                    this.DialogResult = DialogResult.OK;
                    break;
                case 0:
                    this.Text = "Añadir un nuevo item";
                    Label
                        l1Object = new Label(),
                        lWeird = new Label(),
                        lAbility = new Label(),
                        lName = new Label();
                    l1Object.Text = "Type";
                    lWeird.Text = "Weirdness";
                    lAbility.Text = "Ability";
                    lName.Text = "Name";
                    l1Object.Width = TextRenderer.MeasureText(lWeird.Text, lWeird.Font).Width;
                    lWeird.Width = TextRenderer.MeasureText(lWeird.Text, lWeird.Font).Width;
                    lAbility.Width = TextRenderer.MeasureText(lWeird.Text, lWeird.Font).Width;
                    lName.Width = TextRenderer.MeasureText(lWeird.Text, lWeird.Font).Width;
                    
                    l1Object.Location = new Point
                        (((this.Width / 4) - (t1Name.Width / 2)), 40);
                    lWeird.Location = new Point
                        (l1Object.Location.X, (l1Object.Location.Y + lWeird.Height + 3));
                    lAbility.Location = new Point
                        (l1Object.Location.X, (lWeird.Location.Y + lAbility.Height + 3));
                    lName.Location = new Point
                        (l1Object.Location.X, (lAbility.Location.Y + lName.Height + 3));
                    t1Object.Location = new Point
                        (l1Object.Location.X + l1Object.Width + 5, l1Object.Location.Y);
                    t1Weird.Location = new Point
                        (t1Object.Location.X, (t1Object.Location.Y + t1Weird.Height + 3));
                    t1Ability.Location = new Point
                        (t1Object.Location.X, (t1Weird.Location.Y + t1Ability.Height + 3));
                    t1Name.Location = new Point
                        (t1Object.Location.X, (t1Ability.Location.Y + t1Name.Height + 3));

                    Controls.Add(l1Object);
                    Controls.Add(lWeird);
                    Controls.Add(lAbility);
                    Controls.Add(lName);
                    Controls.Add(t1Object);
                    Controls.Add(t1Weird);
                    Controls.Add(t1Ability);
                    Controls.Add(t1Name);
                    break;
                case 1:
                    this.Text = "Añadir una nueva receta";
                    Label
                        l2Type = new Label(),
                        l2Name = new Label();
                    l2Type.Text = "Type";
                    l2Name.Text = "Name";
                    l2Type.Width = TextRenderer.MeasureText(l2Type.Text, l2Type.Font).Width;
                    l2Name.Width = TextRenderer.MeasureText(l2Type.Text, l2Type.Font).Width;

                    l2Type.Location = new Point
                        (((this.Width / 4) - (t1Name.Width / 2)), 40);
                    l2Name.Location = new Point
                        (l2Type.Location.X, (l2Type.Location.Y + l2Type.Height + 3));
                    t2Type.Location = new Point
                        (l2Type.Location.X + l2Type.Width + 5, l2Type.Location.Y);
                    t2Name.Location = new Point
                        (t2Type.Location.X, (t2Type.Location.Y + t2Type.Height + 3));
                    Controls.Add(l2Type);
                    Controls.Add(l2Name);
                    Controls.Add(t2Type);
                    Controls.Add(t2Name);
                    break;
                case 2:
                    this.Text = "Añadir una nueva cuenta";
                    Label
                        l3Email = new Label(),
                        l3Pass = new Label();
                    l3Email.Text = "Email";
                    l3Pass.Text = "Password";
                    l3Email.Width = TextRenderer.MeasureText(l3Pass.Text, l3Pass.Font).Width;
                    l3Pass.Width = TextRenderer.MeasureText(l3Pass.Text, l3Pass.Font).Width;

                    l3Email.Location = new Point
                        (((this.Width / 4) - (t1Name.Width / 2)), 40);
                    l3Pass.Location = new Point
                        (l3Email.Location.X, (l3Email.Location.Y + l3Email.Height + 3));
                    t3Email.Location = new Point
                        (l3Email.Location.X + l3Email.Width + 5, l3Email.Location.Y);
                    t3Pass.Location = new Point
                        (t3Email.Location.X, (t3Email.Location.Y + t3Email.Height + 3));
                    Controls.Add(l3Email);
                    Controls.Add(l3Pass);
                    Controls.Add(t3Email);
                    Controls.Add(t3Pass);
                    break;
            }
            Button
                eButton = new Button();
            eButton.Text = DialogResult.OK.ToString();
            eButton.Location = new Point
                (((this.Width / 2) - (eButton.Width / 2)), (2 * (this.Height / 3) - (eButton.Height)));


            eButton.Click += new EventHandler(OptionsButton_Click);
            this.FormClosing += new FormClosingEventHandler(Form2_OnClosing);


            Controls.Add(eButton);

        }
        private void Form2_OnClosing(object sender, FormClosingEventArgs e)
        {
        }
        private void CheckData()
        {
            switch(form1state)
            {
                case 0:
                    if (t1Name.TextLength == 0)
                    {
                        MessageBox.Show("Warning! \n All values must be complete!", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        itmcapsule.objecttype = Decimal.ToInt32(t1Object.Value);
                        itmcapsule.weirdness = Decimal.ToInt32(t1Weird.Value);
                        itmcapsule.ability = Decimal.ToInt32(t1Ability.Value);
                        itmcapsule.name = t1Name.Text;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;
                case 1:
                    if (t2Name.TextLength == 0)
                    {
                        MessageBox.Show("Warning! \n All values must be complete!", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        rcpcapsule.type = Decimal.ToInt32(t2Type.Value);
                        rcpcapsule.name = t2Name.Text;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;
                case 2:
                    if (t3Email.TextLength == 0 || t3Pass.TextLength == 0)
                    {
                        MessageBox.Show("Warning! \n All values must be complete!", "Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        acccapsule.email = t3Email.Text;
                        acccapsule.password = t3Pass.Text;
                        this.DialogResult = DialogResult.OK;
                    }
                    break;
            }
            
        }
        private void OptionsButton_Click(object sender, EventArgs e)
        {
            CheckData();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
