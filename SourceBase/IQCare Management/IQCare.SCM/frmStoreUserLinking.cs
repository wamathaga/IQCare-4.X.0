using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interface.SCM;
using Application.Common;
using Application.Presentation;
using System.Xml;

namespace IQCare.SCM
{
    public partial class frmStoreUserLinking : Form
    {
        DataSet dsStoreList = new DataSet();
        public frmStoreUserLinking()
        {
            InitializeComponent();
        }

        private void frmStoreUserLinking_Load(object sender, EventArgs e)
        {
            Init_Form();
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);

        }
        private void Init_Form()
        {
             IMasterList objStoreList = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
             dsStoreList = objStoreList.GetStoreUserLink(0);
             BindStoreDropdown();

        }


        private void BindStoreDropdown()
        {
            try
            {
               
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCombo(ddlStore, dsStoreList.Tables[0], "Name", "Id");

                //BindFunctions theBind = new BindFunctions();
                chkItemList.DataSource = null;
                theBind.Win_BindCheckListBox(chkItemList, dsStoreList.Tables[1], "UserName", "UserID");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }
       
        private void chkItemList_SelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        private void BindItemList()
        {
            try
            {
                
                for (int k = 0; k < chkItemList.Items.Count; k++)
                {
                    this.chkItemList.SetItemChecked(k, false);
                }

                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCheckListBox(chkItemList, dsStoreList.Tables[1], "UserName", "UserID");
                
                for (int i = 0; i < dsStoreList.Tables[2].Rows.Count; i++)
                {
                    for (int j = 0; j < chkItemList.Items.Count; j++)
                    {
                        if (Convert.ToInt32(dsStoreList.Tables[2].Rows[i]["UserID"]) == Convert.ToInt32((((System.Data.DataRowView)(chkItemList.Items[j])).Row.ItemArray[0]).ToString()))
                        {
                            this.chkItemList.SetItemChecked(j, true);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {


            try
            {
                
                DataTable dtStoreUserList = new DataTable();
                dtStoreUserList = dsStoreList.Tables[3];

                //arrItemList = new ArrayList();
                for (int i = 0; i < chkItemList.Items.Count; i++)
                {
                    if (chkItemList.GetItemChecked(i) == true)
                    {
                        DataRow theDR = dtStoreUserList.NewRow();
                        theDR["StoreID"] = ddlStore.SelectedValue;
                        theDR["UserID"] = Convert.ToInt32((((System.Data.DataRowView)(chkItemList.Items[i])).Row.ItemArray[0]).ToString());
                        
                        // arrItemList.Add((((System.Data.DataRowView)(chkItemList.Items[i])).Row.ItemArray[0]).ToString());
                        dtStoreUserList.Rows.Add(theDR);

                    }
                }

                //string ItemType = ddlItemType.SelectedValue.ToString();
                IMasterList objMasterlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                int ret = objMasterlist.SaveUpdateStoreUserLinking(dtStoreUserList);
                BindStoreDropdown();
                if (ret > 0)
                {
                    IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                    return;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }





        }

        private void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStore.SelectedIndex != 0 && Convert.ToInt32(ddlStore.SelectedValue.ToString()) > 0)
            {
                IMasterList objStoreList = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                dsStoreList = objStoreList.GetStoreUserLink(Convert.ToInt32(ddlStore.SelectedValue.ToString()));
                BindItemList();
            }
            else
            {
                BindFunctions theBind = new BindFunctions();
                chkItemList.DataSource = null;
                theBind.Win_BindCheckListBox(chkItemList, dsStoreList.Tables[1], "UserName", "UserID");
            }

        }
        //deepika
        private void btnClose_Click(object sender, EventArgs e)
        
         {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }

        }

    }

