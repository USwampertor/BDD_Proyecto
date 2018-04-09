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
        public TextBox
            tObject = new TextBox(),
            tWeird = new TextBox(),
            tAbility = new TextBox(),
            tName = new TextBox();
        public Form1.DataCapsule capsule = new Form1.DataCapsule();
        public Form2()
        {
            InitializeComponent();
            InitializeDataEntry();
        }
        public Form2(Form1.DataCapsule edit)
        {
            InitializeComponent();
            InitializeDataEntry();
            EditData(edit);
        }
        public void EditData(Form1.DataCapsule edit)
        {
            tObject.Text = edit.objecttype.ToString();
            tWeird.Text = edit.weirdness.ToString();
            tAbility.Text = edit.ability.ToString();
            tName.Text = edit.name;
            this.Text = "Editar entrada";
        }
        public void InitializeDataEntry()
        {
            this.Text = "Añadir un nuevo alumno";
            this.ClientSize = new System.Drawing.Size(300, 300);
            Label
                lObject = new Label(),
                lWeird = new Label(),
                lAbility = new Label(),
                lName = new Label();


            Button
                eButton = new Button();
            lObject.Text = "Type";
            lWeird.Text = "Weirdness";
            lAbility.Text = "Ability";
            lName.Text = "Name";
            lObject.Width = TextRenderer.MeasureText(lWeird.Text, lWeird.Font).Width;
            lWeird.Width = TextRenderer.MeasureText(lWeird.Text, lWeird.Font).Width;
            lAbility.Width = TextRenderer.MeasureText(lWeird.Text, lWeird.Font).Width;
            lName.Width = TextRenderer.MeasureText(lWeird.Text, lWeird.Font).Width;
            eButton.Text = DialogResult.OK.ToString();




            lObject.Location = new Point
                (((this.Width / 4) - (tName.Width / 2)), 40);
            lWeird.Location = new Point
                (lObject.Location.X, (lObject.Location.Y + lWeird.Height + 3));
            lAbility.Location = new Point
                (lObject.Location.X, (lWeird.Location.Y + lAbility.Height + 3));
            lName.Location = new Point
                (lObject.Location.X, (lAbility.Location.Y + lName.Height + 3));
            tObject.Location = new Point
                (lObject.Location.X + lObject.Width + 5, lObject.Location.Y);
            tWeird.Location = new Point
                (tObject.Location.X, (tObject.Location.Y + tWeird.Height + 3));
            tAbility.Location = new Point
                (tObject.Location.X, (tWeird.Location.Y + tAbility.Height + 3));
            tName.Location = new Point
                (tObject.Location.X, (tAbility.Location.Y + tName.Height + 3));
            eButton.Location = new Point
                (((this.Width / 2) - (eButton.Width / 2)), (2 * (this.Height / 3) - (eButton.Height)));


            eButton.Click += new EventHandler(OptionsButton_Click);
            this.FormClosing += new FormClosingEventHandler(Form2_OnClosing);

            Controls.Add(lObject);
            Controls.Add(lWeird);
            Controls.Add(lAbility);
            Controls.Add(lName);
            Controls.Add(tObject);
            Controls.Add(tWeird);
            Controls.Add(tAbility);
            Controls.Add(tName);
            Controls.Add(eButton);

        }
        private void Form2_OnClosing(object sender, FormClosingEventArgs e)
        {
        }
        private void CheckData()
        {

            string smonth = "", sday = "";

            if (
                tObject.TextLength == 0 ||
                tWeird.TextLength == 0 ||
                tAbility.TextLength == 0 ||
                tName.TextLength == 0)
            {
                MessageBox.Show("Warning! \n All values must be complete!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                capsule.objecttype = int.Parse(tObject.Text);
                capsule.weirdness = int.Parse(tWeird.Text);
                capsule.ability = int.Parse(tAbility.Text);
                capsule.name = tName.Text;


                this.DialogResult = DialogResult.OK;
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
