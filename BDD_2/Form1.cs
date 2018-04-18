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
        public struct ItemStruct
        {
            public string name;
            public int id, objecttype, weirdness, ability;
        };
        public struct ReceiptStruct
        {
            public string name;
            public int id, type;
        };
        public struct AccountStruct
        {
            public string email, password;
            public int id;
        };
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        List<ItemStruct> ItemData = new List<ItemStruct>();
        List<ReceiptStruct> ReceiptData = new List<ReceiptStruct>();
        List<AccountStruct> AccountData = new List<AccountStruct>();

        

        List<ListBox> 
            tableList1 = new List<ListBox>(),
            tableList2 = new List<ListBox>(),
            tableList3 = new List<ListBox>();
        List<Label> 
            labelList1 = new List<Label>(),
            labelList2 = new List<Label>(),
            labelList3 = new List<Label>();

        List<List<ListBox>> Tables = new List<List<ListBox>>();
        List<List<Label>> Labels = new List<List<Label>>();

        
        int gameState = -1;
        int totalitems = 0;
        int totalreceipts = 0;
        int totalaccounts = 0;
        bool isadding = false;

        string itselect = "SELECT * FROM items ";
        string itinsert = "INSERT INTO items (object_type, weirdness, ability, name) VALUES";
        string itdelete = "DELETE FROM items WHERE name = ";
        string itupdate = "UPDATE items SET ";

        string rcpselect = "SELECT * FROM receipts ";
        string rcpinsert = "INSERT INTO receipts (type, name) VALUES";
        string rcpdelete = "DELETE FROM receipts WHERE name = ";
        string rcpupdate = "UPDATE receipts SET ";


        string accselect = "SELECT * FROM accounts ";
        string accinsert = "INSERT INTO accounts (email, password) VALUES";
        string accdelete = "DELETE FROM accounts WHERE email = ";
        string accupdate = "UPDATE accounts SET ";

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
                t1nameBox = new ListBox(),
                t1weirdBox = new ListBox(),
                t1objectBox = new ListBox(),
                t1abilityBox = new ListBox(),
                t2nameBox = new ListBox(),
                t2typeBox = new ListBox(),
                t3passBox = new ListBox(),
                t3emailBox = new ListBox();

            Label
                t1lname = new Label(),
                t1lweird = new Label(),
                t1lobject = new Label(),
                t1lability = new Label(),
                t2lname = new Label(),
                t2ltype = new Label(),
                t3lpass = new Label(),
                t3lemail = new Label();

            Button
                dynamicAdd = new Button(),
                itemstable = new Button(),
                dynamicEdit = new Button(),
                dynamicDelete = new Button(),
                receiptstable = new Button(),
                accountstable = new Button();
            
            this.Width = 800;
            this.Height = 250;
            this.MaximumSize = new Size(800, 800);
            this.MinimumSize = new Size(800, 250);
            t1lname.Text = "Name";
            t1lweird.Text = "Weirdness";
            t1lobject.Text = "Type";
            t1lability.Text = "Ability";
            t2lname.Text = "Name";
            t2ltype.Text = "Type";
            t3lemail.Text = "Email";
            t3lpass.Text = "Password";
            dynamicAdd.Text = "Add";
            itemstable.Text = "Items";
            dynamicEdit.Text = "Edit";
            dynamicDelete.Text = "Delete";
            receiptstable.Text = "Receipts";
            accountstable.Text = "Accounts";

            int labewidth = TextRenderer.MeasureText(t1lweird.Text, t1lobject.Font).Width;
            t1lability.Width = labewidth;
            t1lobject.Width = labewidth;
            t1lweird.Width = labewidth;
            t1lname.Width = labewidth;

            t1nameBox.Location = new Point
                (((this.Width / 5) - (t1nameBox.Width / 2)), ((this.Height / 3) - (t1nameBox.Height / 2)));
            t1objectBox.Location = new Point
                (((t1nameBox.Location.X) + (t1nameBox.Width)), (t1nameBox.Location.Y));
            t1weirdBox.Location = new Point
                (((t1objectBox.Location.X) + (t1objectBox.Width)), (t1nameBox.Location.Y));
            t1abilityBox.Location = new Point
                (((t1weirdBox.Location.X) + (t1weirdBox.Width)), (t1nameBox.Location.Y));

            t2nameBox.Location = new Point
                (((this.Width / 5) - (t1nameBox.Width / 2)), ((this.Height / 3) - (t1nameBox.Height / 2)));
            t2typeBox.Location = new Point
                (((t2nameBox.Location.X) + (t2nameBox.Width)), (t2nameBox.Location.Y));


            t3emailBox.Location = new Point
                (((this.Width / 5) - (t1nameBox.Width / 2)), ((this.Height / 3) - (t1nameBox.Height / 2)));
            t3passBox.Location = new Point
                (((t3emailBox.Location.X) + (t3emailBox.Width)), (t3emailBox.Location.Y));


            t1lname.Location = new Point
                (t1nameBox.Location.X, (t1nameBox.Location.Y - t1lobject.Height - 1));
            t1lobject.Location = new Point
                (t1objectBox.Location.X, t1lname.Location.Y);
            t1lweird.Location = new Point
                (t1weirdBox.Location.X, t1lname.Location.Y);
            t1lability.Location = new Point
                (t1abilityBox.Location.X, t1lname.Location.Y);

            t2lname.Location = new Point
                (t2nameBox.Location.X, (t2nameBox.Location.Y - t2lname.Height - 1));
            t2ltype.Location = new Point
                (t2typeBox.Location.X, t2lname.Location.Y);

            t3lemail.Location = new Point
                (t3emailBox.Location.X, (t3emailBox.Location.Y - t3lemail.Height - 1));
            t3lpass.Location = new Point
                (t3passBox.Location.X, t3lemail.Location.Y);

            dynamicAdd.Location = new Point
               (this.Width-dynamicAdd.Width-50, (t1nameBox.Location.Y));
            dynamicEdit.Location = new Point
                (dynamicAdd.Location.X, (dynamicAdd.Location.Y + dynamicEdit.Height));
            dynamicDelete.Location = new Point
                (dynamicAdd.Location.X, (dynamicEdit.Location.Y + dynamicDelete.Height));
            itemstable.Location = new Point
                (dynamicAdd.Location.X, (dynamicDelete.Location.Y + itemstable.Height));
            receiptstable.Location = new Point
                (dynamicAdd.Location.X, (itemstable.Location.Y + receiptstable.Height));
            accountstable.Location = new Point
                (dynamicAdd.Location.X,(receiptstable.Location.Y+accountstable.Height));


            dynamicAdd.Click += new EventHandler(dynamicAdd_Click);
            
            this.FormClosing += new FormClosingEventHandler(Closing);
            this.SizeChanged += new EventHandler(WindowResize);

            t1nameBox.DoubleClick += new EventHandler(OnListEdit);
            t1objectBox.DoubleClick += OnListEdit;
            t1weirdBox.DoubleClick += OnListEdit;
            t1abilityBox.DoubleClick += OnListEdit;
            dynamicEdit.Click += OnListEdit;

            t1nameBox.KeyPress += new KeyPressEventHandler(OnListDelete);
            t1objectBox.KeyPress += OnListDelete;
            t1weirdBox.KeyPress += OnListDelete;
            t1abilityBox.KeyPress += OnListDelete;
            dynamicDelete.Click += new EventHandler(dynamicDelete_Click);
            itemstable.Click += new EventHandler(ItemsButtonClick);
            receiptstable.Click += new EventHandler(ReceiptsButtonClick);
            accountstable.Click += new EventHandler(AccountsButtonClick);


            Controls.Add(dynamicDelete);
            Controls.Add(dynamicAdd);
            Controls.Add(dynamicEdit);
            Controls.Add(receiptstable);
            Controls.Add(accountstable);
            Controls.Add(itemstable);

            Controls.Add(t1objectBox);
            Controls.Add(t1weirdBox);
            Controls.Add(t1nameBox);
            Controls.Add(t1abilityBox);

            Controls.Add(t2nameBox);
            Controls.Add(t2typeBox);

            Controls.Add(t3emailBox);
            Controls.Add(t3passBox);

            Controls.Add(t1lobject);
            Controls.Add(t1lweird);
            Controls.Add(t1lname);
            Controls.Add(t1lability);

            Controls.Add(t2lname);
            Controls.Add(t2ltype);

            Controls.Add(t3lemail);
            Controls.Add(t3lpass);

            tableList1.Add(t1nameBox);
            tableList1.Add(t1objectBox);
            tableList1.Add(t1weirdBox);
            tableList1.Add(t1abilityBox);

            tableList2.Add(t2nameBox);
            tableList2.Add(t2typeBox);

            tableList3.Add(t3emailBox);
            tableList3.Add(t3passBox);

            labelList1.Add(t1lname);
            labelList1.Add(t1lobject);
            labelList1.Add(t1lweird);
            labelList1.Add(t1lability);

            labelList2.Add(t2lname);
            labelList2.Add(t2ltype);

            labelList3.Add(t3lemail);
            labelList3.Add(t3lpass);

            Tables.Add(tableList1);
            Tables.Add(tableList2);
            Tables.Add(tableList3);

            Labels.Add(labelList1);
            Labels.Add(labelList2);
            Labels.Add(labelList3);

            for (int index = 0; index < Tables.Count; ++index)
            {
                for (int i = 0; i < Tables[index].Count; ++i)
                {
                    Tables[index][i].Visible = false;
                }

            }

            for (int index = 0; index < Labels.Count; ++index)
            {
                for (int i = 0; i < Labels[index].Count; ++i)
                {
                    Labels[index][i].Visible = false;
                }

            }

            UpdateData();
        }
        private void FormsState(int state)
        {
            for (int index = 0; index < Tables.Count; ++index)
            {
                for (int i = 0; i < Tables[index].Count; ++i)
                {
                    if(index==state)
                        Tables[index][i].Visible = true;
                    else
                        Tables[index][i].Visible = false;

                }
            }
            for (int index = 0; index < Labels.Count; ++index)
            {
                for (int i = 0; i < Labels[index].Count; ++i)
                {
                    if (index == state)
                        Labels[index][i].Visible = true;
                    else
                        Labels[index][i].Visible = false;

                }
            }
        }
        private void ItemsButtonClick(object sender, EventArgs e)
        {
            gameState = 0;
            FormsState(0);
        }
        private void ReceiptsButtonClick(object sender, EventArgs e)
        {
            gameState = 1;
            FormsState(1);
        }
        private void AccountsButtonClick(object sender, EventArgs e)
        {
            gameState = 2;
            FormsState(2);
        }
        private void DataAdd()
        {
            
        }
        private void MySQLFetch()
        {
            
            List<ItemStruct> ItemList = new List<ItemStruct>();
            List<ReceiptStruct> ReceiptList = new List<ReceiptStruct>();
            List<AccountStruct> AccountList = new List<AccountStruct>();
            MySqlConnection conn = new MySqlConnection(builder.ToString());
            MySqlCommand cmd = new MySqlCommand(itselect, conn);
            
            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ItemStruct data = new ItemStruct();
                    data.id = int.Parse(reader.GetString("id"));
                    data.objecttype = int.Parse(reader.GetString("object_type"));
                    data.weirdness = int.Parse(reader.GetString("weirdness"));
                    data.ability = int.Parse(reader.GetString("ability"));
                    data.name = reader.GetString("name");
                    ItemList.Add(data);
                    if (totalitems < data.id) totalitems = data.id;
                }
                reader.Close();
                cmd = new MySqlCommand(rcpselect, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ReceiptStruct data = new ReceiptStruct();
                    data.id = int.Parse(reader.GetString("id"));
                    data.type = int.Parse(reader.GetString("type"));
                    data.name = reader.GetString("name");
                    ReceiptList.Add(data);
                    if (totalreceipts < data.id) totalreceipts = data.id;
                }
                reader.Close();
                cmd = new MySqlCommand(accselect, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AccountStruct data = new AccountStruct();
                    data.id = int.Parse(reader.GetString("id"));
                    data.email = reader.GetString("email");
                    data.password = reader.GetString("password");
                    AccountList.Add(data);
                    if (totalaccounts < data.id) totalaccounts = data.id;
                }
                reader.Close();
                ItemData = ItemList;
                ReceiptData = ReceiptList;
                AccountData = AccountList;
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
                this.Width = 500;
            }
            
            for (int index = 0; index < Tables.Count; ++index)
            {
                for(int i=0;i<Tables[index].Count;i++)
                {
                Tables[index][i].Height = this.Height-50;

                }
            }
        }

        new public void Closing(object sender, FormClosingEventArgs e)
        {
            //if (ItemData != null)
            //{
            //    MessageBox.Show("Guardado con Exito!");

            //}

        }
        public void EditStudent()
        {
            if (gameState == -1) return;
            int editat = -1;
            for (int index = 0; index < Tables[gameState].Count; ++index)
            {
                if (Tables[gameState][index].SelectedIndex != -1)
                {
                    editat = Tables[gameState][index].SelectedIndex;
                    break;
                }
            }
            if (editat != -1)
            {
                switch(gameState)
                {
                    case 0:
                        ItemStruct getItem = new ItemStruct();
                        Form2 EditItem = new Form2(ItemData[editat],gameState);
                        if (EditItem.ShowDialog() == DialogResult.OK)
                        {
                            getItem = EditItem.itmcapsule;
                            getItem.id = editat + 1;

                            string editinto =
                            (itupdate +
                            "object_type = '" + getItem.objecttype +
                            "' , weirdness = '" + getItem.weirdness +
                            "' , ability = '" + getItem.ability +
                            "' , name = '" + getItem.name +
                            "' where id = '" + getItem.id + "'");
                            MySqlConnection conn = new MySqlConnection(builder.ToString());
                            MySqlCommand cmd = new MySqlCommand(editinto, conn);
                            try
                            {
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                ItemData[editat] = getItem;
                                isadding = true;
                                UpdateData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                isadding = false;
                            }
                        }
                        break;
                    case 1:
                        ReceiptStruct getReceipt = new ReceiptStruct();
                        Form2 EditReceipt = new Form2(ReceiptData[editat], gameState);
                        if (EditReceipt.ShowDialog() == DialogResult.OK)
                        {
                            getReceipt = EditReceipt.rcpcapsule;
                            getReceipt.id = editat + 1;

                            string editinto =
                            (rcpupdate +
                            "type = '" + getReceipt.type +
                            "' , name = '" + getReceipt.name +
                            "' where id = '" + getReceipt.id + "'");
                            MySqlConnection conn = new MySqlConnection(builder.ToString());
                            MySqlCommand cmd = new MySqlCommand(editinto, conn);
                            try
                            {
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                ReceiptData[editat] = getReceipt;
                                isadding = true;
                                UpdateData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                isadding = false;
                            }
                        }
                        break;
                    case 2:
                        AccountStruct getAccount = new AccountStruct();
                        Form2 EditAccount = new Form2(AccountData[editat], gameState);
                        if (EditAccount.ShowDialog() == DialogResult.OK)
                        {
                            getAccount = EditAccount.acccapsule;
                            getAccount.id = editat + 1;

                            string editinto =
                            (accupdate +
                            "email = '" + getAccount.email +
                            "' , password = '" + getAccount.password +
                            "' where id = '" + getAccount.id + "'");
                            MySqlConnection conn = new MySqlConnection(builder.ToString());
                            MySqlCommand cmd = new MySqlCommand(editinto, conn);
                            try
                            {
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                AccountData[editat] = getAccount;
                                isadding = true;
                                UpdateData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                isadding = false;
                            }
                        }
                        break;
                }
            }
            else
                MessageBox.Show("Seleccione una fila para editar");
        }
        private void OnListEdit(object sender, EventArgs e)
        {
            EditStudent();

        }
        private void DeleteStudent()
        {
            if (gameState == -1) return;
            int deleteat = -1;
            for (int index = 0; index < Tables[gameState].Count; ++index)
            {
                if (Tables[gameState][index].SelectedIndex != -1)
                {
                    deleteat = Tables[gameState][index].SelectedIndex;
                    break;
                }
            }
            if (deleteat != -1)
            {
                switch (gameState)
                {
                    case -1:
                        return;
                    case 0:
                        if (MessageBox.Show("Estas seguro que quieres borrar el item " + ItemData[deleteat].name + "?",
                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            ItemStruct todelete = new ItemStruct();
                            todelete = ItemData[deleteat];

                            string deleteinto =
                            (itdelete + " '" + todelete.name + "' ");
                            MySqlConnection conn = new MySqlConnection(builder.ToString());
                            MySqlCommand cmd = new MySqlCommand(deleteinto, conn);

                            try
                            {
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                ItemData.RemoveAt(deleteat);
                                isadding = true;
                                UpdateData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;
                    case 1:
                        if (MessageBox.Show("Estas seguro que quieres borrar la receta " + ReceiptData[deleteat].name + "?",
                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            ReceiptStruct todelete = new ReceiptStruct();
                            todelete = ReceiptData[deleteat];

                            string deleteinto =
                            (rcpdelete + " '" + todelete.name + "' ");
                            MySqlConnection conn = new MySqlConnection(builder.ToString());
                            MySqlCommand cmd = new MySqlCommand(deleteinto, conn);

                            try
                            {
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                ReceiptData.RemoveAt(deleteat);
                                isadding = true;
                                UpdateData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;
                    case 2:
                        if (MessageBox.Show("Estas seguro que quieres borrar la cuenta de " + AccountData[deleteat].email + "?",
                "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            AccountStruct todelete = new AccountStruct();
                            todelete = AccountData[deleteat];

                            string deleteinto =
                            (accdelete + " '" + todelete.email + "' ");
                            MySqlConnection conn = new MySqlConnection(builder.ToString());
                            MySqlCommand cmd = new MySqlCommand(deleteinto, conn);

                            try
                            {
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                conn.Close();
                                AccountData.RemoveAt(deleteat);
                                isadding = true;
                                UpdateData();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        break;
                }
            }
            else
                MessageBox.Show("Seleccione una fila para eliminar");
        }
        public void AddStudent()
        {
            
            switch (gameState)
            {
                case -1:
                    return;
                case 0:
                    ItemStruct getItem = new ItemStruct();
                    Form2 addItem = new Form2(gameState);
                    if (addItem.ShowDialog() == DialogResult.OK)
                    {
                        getItem = addItem.itmcapsule;
                        ++totalitems;
                        getItem.id = totalitems;

                        string insertinto =
                            (itinsert + "( '" +
                            getItem.objecttype + "' , '" +
                            getItem.weirdness + "' , '" +
                            getItem.ability + "' , '" +
                            getItem.name + "' )");
                        MySqlConnection conn = new MySqlConnection(builder.ToString());
                        MySqlCommand cmd = new MySqlCommand(insertinto, conn);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            ItemData.Add(getItem);
                            isadding = true;
                            UpdateData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    break;
                case 1:
                    ReceiptStruct getReceipt = new ReceiptStruct();
                    Form2 addReceipt = new Form2(gameState);
                    if (addReceipt.ShowDialog() == DialogResult.OK)
                    {
                        getReceipt = addReceipt.rcpcapsule;
                        ++totalreceipts;
                        getReceipt.id = totalreceipts;

                        string insertinto =
                            (rcpinsert + "( '" +
                            getReceipt.type + "' , '" +
                            getReceipt.name + "' )");
                        MySqlConnection conn = new MySqlConnection(builder.ToString());
                        MySqlCommand cmd = new MySqlCommand(insertinto, conn);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            ReceiptData.Add(getReceipt);
                            isadding = true;
                            UpdateData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    break;
                case 2:
                     AccountStruct getAccount = new AccountStruct();
                    Form2 addEntry = new Form2(gameState);
                    if (addEntry.ShowDialog() == DialogResult.OK)
                    {
                        getAccount = addEntry.acccapsule;
                        ++totalaccounts;
                        getAccount.id = totalaccounts;

                        string insertinto =
                            (accinsert + "( '" +
                            getAccount.email + "' , '" +
                            getAccount.password + "' )");
                        MySqlConnection conn = new MySqlConnection(builder.ToString());
                        MySqlCommand cmd = new MySqlCommand(insertinto, conn);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            AccountData.Add(getAccount);
                            isadding = true;
                            UpdateData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    break;
                default:
                    return;
            }
            


        }
        private void OnListDelete(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                DeleteStudent();

            }
        }
        public void dynamicAdd_Click(object sender, EventArgs e)
        {
            AddStudent();
        }
        private void UpdateData()
        {
            for (int i = 0; i < Tables.Count; ++i)
            {
                for (int index = 0; index < Tables[i].Count; ++index)
                {
                    Tables[i][index].Items.Clear();
                }
            }
            if (ItemData != null && isadding == true)
            {
                List<ItemStruct> temporaryLoad = ItemData;
                for (int index = 0; index < temporaryLoad.Count; ++index)
                {

                    Tables[0][0].Items.Add(temporaryLoad[index].objecttype);
                    Tables[0][1].Items.Add(temporaryLoad[index].weirdness);
                    Tables[0][2].Items.Add(temporaryLoad[index].ability);
                    Tables[0][3].Items.Add(temporaryLoad[index].name);
                }
            }
            if (ReceiptData != null && isadding == true)
            {
                List<ReceiptStruct> temporaryLoad = ReceiptData;
                for (int index = 0; index < temporaryLoad.Count; ++index)
                {

                    Tables[1][0].Items.Add(temporaryLoad[index].name);
                    Tables[1][1].Items.Add(temporaryLoad[index].type);
                }
            }
            if (AccountData != null && isadding == true)
            {
                List<AccountStruct> temporaryLoad = AccountData;
                for (int index = 0; index < temporaryLoad.Count; ++index)
                {
                    Tables[2][0].Items.Add(temporaryLoad[index].email);
                    Tables[2][1].Items.Add(temporaryLoad[index].password);
                }
            }
            isadding = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

