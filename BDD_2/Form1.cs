using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace BDD_2
{
    public partial class Form1 : Form
    {
        public struct DataCapsule
        {
            public string name;
            public int id, objecttype, weirdness, ability;
        };

        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        int totalindatabase = 0;
        public DataCapsule capsule = new DataCapsule();
        private List<DataCapsule> StoredData = new List<DataCapsule>();
        List<ListBox> ShowData = new List<ListBox>();
        bool isadding = false;
        string select = "SELECT * FROM items ";
        string insert = "INSERT INTO items (object_type, weirdness, ability, name) VALUES";
        string delete = "DELETE FROM items WHERE name = ";
        string update = "UPDATE items SET ";
        public Form1()
        {
            InitializeComponent();
            InitializeDynamicComponent();
            MySQLFetch();
        }
        public void InitializeDynamicComponent()
        {
            //mysql
            builder.Server = "138.68.20.16";
            builder.Port = 3306;
            builder.UserID = "mmillan_uad";
            builder.Password = "1234567890";
            builder.Database = "mmillan_final";


            //endofmysql
            ListBox
                objectBox = new ListBox(),
                weirdBox = new ListBox(),
                abilityBox = new ListBox(),
                nameBox = new ListBox();

            Label
                lobject = new Label(),
                lweird = new Label(),
                lability = new Label(),
                lname = new Label();


            Button
                dynamicEdit = new Button(),
                dynamicAdd = new Button(),
                dynamicDelete = new Button();

            this.Width = 800;
            this.Height = 250;
            this.MaximumSize = new Size(800, 1000);
            this.MinimumSize = new Size(800, 250);
            lobject.Text = "Type";
            lweird.Text = "Weirdness";
            lname.Text = "Name";
            lability.Text = "Ability";
            dynamicAdd.Text = "Add";
            dynamicEdit.Text = "Edit";
            dynamicDelete.Text = "Delete";

            
            lobject.Width = TextRenderer.MeasureText(lweird.Text, lobject.Font).Width;
            lweird.Width = TextRenderer.MeasureText(lweird.Text, lobject.Font).Width;
            lname.Width = TextRenderer.MeasureText(lweird.Text, lobject.Font).Width;
            lability.Width = TextRenderer.MeasureText(lweird.Text, lobject.Font).Width;

            nameBox.Location = new Point
                (((this.Width / 5) - (nameBox.Width / 2)), ((this.Height / 3) - (nameBox.Height / 2)));
            objectBox.Location = new Point
                (((nameBox.Location.X) + (nameBox.Width)), (nameBox.Location.Y));
            weirdBox.Location = new Point
                (((objectBox.Location.X) + (objectBox.Width)), (nameBox.Location.Y));
            abilityBox.Location = new Point
                (((weirdBox.Location.X) + (weirdBox.Width)), (nameBox.Location.Y));



            lname.Location = new Point
                (nameBox.Location.X, (nameBox.Location.Y - lobject.Height - 1));
            lobject.Location = new Point
                (objectBox.Location.X, lname.Location.Y);
            lweird.Location = new Point
                (weirdBox.Location.X, lname.Location.Y);
            lability.Location = new Point
                (abilityBox.Location.X, lname.Location.Y);

            dynamicAdd.Location = new Point
               (((abilityBox.Location.X) + (abilityBox.Width)), (nameBox.Location.Y));
            dynamicEdit.Location = new Point
                (((abilityBox.Location.X) + (abilityBox.Width)), (dynamicAdd.Location.Y + dynamicEdit.Height));
            dynamicDelete.Location = new Point
                (((abilityBox.Location.X) + (abilityBox.Width)), (dynamicEdit.Location.Y + dynamicDelete.Height));

            dynamicAdd.Click += new EventHandler(dynamicAdd_Click);

            this.FormClosing += new FormClosingEventHandler(Closing);
            this.SizeChanged += new EventHandler(WindowResize);

            nameBox.DoubleClick += new EventHandler(OnListEdit);
            objectBox.DoubleClick += OnListEdit;
            weirdBox.DoubleClick += OnListEdit;
            abilityBox.DoubleClick += OnListEdit;
            dynamicEdit.Click += OnListEdit;

            nameBox.KeyPress += new KeyPressEventHandler(OnListDelete);
            objectBox.KeyPress += OnListDelete;
            weirdBox.KeyPress += OnListDelete;
            abilityBox.KeyPress += OnListDelete;
            dynamicDelete.Click += new EventHandler(dynamicDelete_Click);

            Controls.Add(dynamicDelete);
            Controls.Add(dynamicAdd);
            Controls.Add(dynamicEdit);
            Controls.Add(objectBox);
            Controls.Add(weirdBox);
            Controls.Add(nameBox);
            Controls.Add(abilityBox);
            Controls.Add(lobject);
            Controls.Add(lweird);
            Controls.Add(lname);
            Controls.Add(lability);


            ShowData.Add(nameBox);
            ShowData.Add(objectBox);
            ShowData.Add(weirdBox);
            ShowData.Add(abilityBox);

            UpdateData();
        }
        private void DataAdd()
        {
            
        }
        private void MySQLFetch()
        {
            List<DataCapsule> DataList = new List<DataCapsule>();
            MySqlConnection conn = new MySqlConnection(builder.ToString());
            MySqlCommand cmd = new MySqlCommand(select, conn);

            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DataCapsule data = new DataCapsule();
                    data.id = DataList.Count + 1;
                    data.objecttype = int.Parse(reader.GetString("object_type"));
                    data.weirdness = int.Parse(reader.GetString("weirdness"));
                    data.ability = int.Parse(reader.GetString("ability"));
                    data.name = reader.GetString("name");
                    DataList.Add(data);
                }
                StoredData = DataList;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            isadding = true;
            UpdateData();
        }
        
        private void dynamicDelete_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }
        private void WindowResize(object sender, EventArgs e)
        {
            if (this.Width > MaximumSize.Width)
            {
                this.Width = 800;
            }
            if (this.Height < (MaximumSize.Height / 2) && this.Height > this.MinimumSize.Height)
            {
                for (int index = 0; index < ShowData.Count; ++index)
                {
                    ShowData[index].Height = (3 * this.Height / 5);
                }

            }
            else
            {
                for (int index = 0; index < ShowData.Count; ++index)
                {
                    ShowData[index].Height = (3 * this.Height / 4);
                }

            }

        }

        new public void Closing(object sender, FormClosingEventArgs e)
        {
            if (StoredData != null)
            {
                MessageBox.Show("Guardado con Exito!");

            }

        }
        public void EditStudent()
        {
            DataCapsule getUser = new DataCapsule();
            int editat = -1;
            for (int index = 0; index < ShowData.Count; ++index)
            {
                if (ShowData[index].SelectedIndex != -1)
                {
                    editat = ShowData[index].SelectedIndex;
                    break;
                }
            }
            if (editat != -1)
            {

                Form2 EditForm = new Form2(StoredData[editat]);
                if (EditForm.ShowDialog() == DialogResult.OK)
                {
                    getUser = EditForm.capsule;
                    getUser.id = editat + 1;

                    string editinto =
                    (update +
                    "object_type = '" + getUser.objecttype +
                    "' , weirdness = '" + getUser.weirdness +
                    "' , ability = '" + getUser.ability +
                    "' , name = '" + getUser.name +
                    "' where name = '" + getUser.name + "'");
                    MySqlConnection conn = new MySqlConnection(builder.ToString());
                    MySqlCommand cmd = new MySqlCommand(editinto, conn);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        StoredData[editat] = getUser;
                        isadding = true;
                        UpdateData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    
                }
            }
            else
                MessageBox.Show("Seleccione un alumno para editar");
        }
        private void OnListEdit(object sender, EventArgs e)
        {
            EditStudent();

        }
        private void DeleteStudent()
        {
            int deleteat = -1;
            for (int index = 0; index < ShowData.Count; ++index)
            {
                if (ShowData[index].SelectedIndex != -1)
                {
                    deleteat = ShowData[index].SelectedIndex;
                    break;
                }
            }
            if (deleteat != -1)
            {
                if (MessageBox.Show("Estas seguro que quieres borrar el alumno " + StoredData[deleteat].name + "?",
                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    DataCapsule todelete = new DataCapsule();
                    todelete = StoredData[deleteat];

                    string deleteinto =
                    (delete + " '" + todelete.name + "' ");
                    MySqlConnection conn = new MySqlConnection(builder.ToString());
                    MySqlCommand cmd = new MySqlCommand(deleteinto, conn);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        StoredData.RemoveAt(deleteat);
                        isadding = true;
                        UpdateData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                   
                }
            }
            else
                MessageBox.Show("Seleccione un alumno para eliminar");
        }
        private void OnListDelete(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                DeleteStudent();

            }
        }
        public void AddStudent()
        {
            DataCapsule getUser = new DataCapsule();
            Form2 addEntry = new Form2();
            if (addEntry.ShowDialog() == DialogResult.OK)
            {
                getUser = addEntry.capsule;
                getUser.id = (totalindatabase + 1);
                ++totalindatabase;

                string insertinto =
                    (insert + "( '" + 
                    getUser.objecttype + "' , '" + 
                    getUser.weirdness + "' , '" + 
                    getUser.ability + "' , '" + 
                    getUser.name + "' )");
                MySqlConnection conn = new MySqlConnection(builder.ToString());
                MySqlCommand cmd = new MySqlCommand(insertinto , conn);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    StoredData.Add(getUser);
                    isadding = true;
                    UpdateData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }


        }
        public void dynamicAdd_Click(object sender, EventArgs e)
        {
            AddStudent();
        }
        private void UpdateData()
        {
            for (int index = 0; index < ShowData.Count; ++index)
            {
                ShowData[index].Items.Clear();
            }
            if (StoredData != null && isadding == true)
            {
                List<DataCapsule> temporaryLoad = StoredData;
                for (int index = 0; index < temporaryLoad.Count; ++index)
                {

                    ShowData[0].Items.Add(temporaryLoad[index].objecttype);
                    ShowData[1].Items.Add(temporaryLoad[index].weirdness);
                    ShowData[2].Items.Add(temporaryLoad[index].ability);
                    ShowData[3].Items.Add(temporaryLoad[index].name);
                }
                isadding = false;
            }

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

