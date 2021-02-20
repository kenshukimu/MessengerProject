using KSM.DAO.Models;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Kyobo_Msg_Client
{
    public partial class findUser : Form
    {
        public findUser(string rcvUserList)
        {
            InitializeComponent();

            string[] nameArray = null;

            nameArray = new string[MainProg.CConf.userList.Count];

            for (int i = 0; i < MainProg.CConf.userList.Count; i++)
            {
                nameArray[i] = MainProg.CConf.userList[i].MEMBERNAME;
            }
            AutoCompleteTextBox tb = new AutoCompleteTextBox();
            tb.Name = "txtUserName";
            tb.Values = nameArray;
            tb.Location = new Point(10, 10);
            tb.Size = new Size(200, 75);
            panel1.Controls.Add(tb);

            if (rcvUserList.Length > 0)
            {
                string[] userList = rcvUserList.Split(';');
                lvrcvUserList.Update();
                foreach (string user in userList)
                {
                    if (user.Equals("")) continue;
                    lvrcvUserList.Items.Add(user);
                }
                lvrcvUserList.EndUpdate();
            }
        }

        private void btn_add_Click(object sender, System.EventArgs e)
        {
            Control ctn = panel1.Controls["txtUserName"];
            string userNm = ctn.Text;

            UserDetailInfo uie = MainProg.getUserInfoByKey("MEMBERNAME", userNm);

            if (uie == null || uie.MEMBERID.Equals(""))
            {
                MessageBox.Show("등록되어 있지 않은 사용자입니다.");
                return;
            }
            
            lvrcvUserList.Update();            
            lvrcvUserList.Items.Add(userNm);
            ctn.Text = "";
            lvrcvUserList.EndUpdate();
        }

        private void btn_del_Click(object sender, System.EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedItems = new ListBox.SelectedObjectCollection(lvrcvUserList);
            selectedItems = lvrcvUserList.SelectedItems;

            if (lvrcvUserList.SelectedIndex != -1)
            {
                for (int i = selectedItems.Count - 1; i >= 0; i--)
                    lvrcvUserList.Items.Remove(selectedItems[i]);
            }
        }

        private void btn_set_Click(object sender, System.EventArgs e)
        {
            string items = string.Empty;
            foreach (var item in lvrcvUserList.Items)
            {
                items += item.ToString() + ((item.ToString().IndexOf(";") > -1) ? "" : ";");                    
            }
            ((MessageFormBox)(this.Owner)).setRcvUsers(items);
            this.Close();
        }
    }
}
