<%@ Page Language="C#" MasterPageFile="~/MasterPage/IQCare.master" AutoEventWireup="True"
    Inherits="CustomFields_frmConfig_Customfields" Title="Untitled Page" MaintainScrollPositionOnPostback="true"
    CodeBehind="frmConfig_Customfields.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="IQCareContentPlaceHolder" runat="Server">

<script language="javascript" type="text/javascript">
    /**********************************************************
    Function		: SendCodeName	
    Created By		: Amitava Sinha
    Created On		: 01-Feb-2006
    ***********************************************************/
    function SendCodeName() {
        var id = document.getElementById('<%=txtflabel.ClientID%>').value;
        if (id.length <= 0) {
            NotifyMessage('Please enter the field name.');

            if (id.type == "radio") {
                id.value = 0;
            }

            return true;
        }
        else {
            CallServer(id, "This is context from client");
            return true;
        }

    }

    function ReceiveServerData(args, context) {
        if (window.ActiveXObject) {
            //var obj = new ActiveXObject("lt");
            var obj = new ActiveXObject();
            obj.loadXML(args);
            var dsRoot = obj.documentElement;
            var UserCount = dsRoot.getElementsByTagName('NO');
            var text;
            for (var count = 0; count < UserCount.length; count++) {
                text = UserCount[count].text;

                //                if(text > 0)
                //                {
                //				    alert('List Name already exists. Please select different List name');
                //			        document.getElementById('<%=txtflabel.ClientID%>').focus();
                //			        blnUName = false;
                //                }
                //                else
                //                {
                showPopup();
                //                }

            }
        }
    }


    /*
    else
    {
    var parser=new DOMParser();
    var doc=parser.parseFromString(args,"text/xml");
    var val=doc.getElementsByTagName("NO")[0].firstChild.nodeValue;
    if(val>0)
    {
    if(strFromSaveButton == 'N')
    {
    alert('List name already exists. Please select different list name');
    }
    document.getElementById("txtUName").focus();
    blnUName = false;
    }
                
    else
    alert('List name is unique.');
            
    }
    }	
    */


    /**********************************************************
    Author : Amitava Sinha
    Creation Date :5-Jan-2007
    Purpose: Popup window Display 
    ***********************************************************/
    function showPopup() {
        var selectlist = "";
        var intCustomField = '<%=cfieldID%>';
        var lblName = document.getElementById('<%=txtflabel.ClientID%>').value;

        if (lblName == '') {
            NotifyMessage("Please enter the field name.")
            document.getElementById('<%=rbtnselect.ClientID%>').checked = false;
            document.getElementById('<%=rbtnmulti.ClientID%>').checked = false;
            return;
        }
        if (intCustomField == 0) {
            if (document.getElementById('<%=rbtnselect.ClientID%>').checked) {
                selectlist = document.getElementById('<%=rbtnselect.ClientID%>').value;
            }
            if (document.getElementById('<%=rbtnmulti.ClientID%>').checked) {
                selectlist = document.getElementById('<%=rbtnmulti.ClientID%>').value;
            }
            var strqrystring = "&List=" + selectlist
            //var strqrystr="&CFID="+ intCustomField.toString() 
            var url = "frmAdmin_ControlListSelector.aspx?Label=" + lblName
            //url=url+strqrystring+strqrystr;
            url = url + strqrystring;
            //"if (this.checked) {convertAllFields('upper')}"
            window.open(url, 'ControlList', 'toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');
            //window.focus();
        }
    }

    function RearrangePopup() {


        var url = "frmAdmin_RearrangeList.aspx"

        window.open(url, 'ControlList', 'toolbars=no,location=no,directories=no,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');

    }


    function CheckNumericValue(e) {
        var key;
        key = e.which ? e.which : e.keyCode;
        if (key >= 48 && key <= 57) {
            return true;
        }
        else {
            NotifyMessage("Please enter number only !");
            return false;
        }
    }

    function CheckAlphaNumeric(e) {
        var key;
        key = e.which ? e.which : e.keyCode;
        //NotifyMessage(key);
        if ((key >= 48 && key <= 57) || (key == 63) || (key >= 65 && key <= 90) || (key == 32) || (key >= 97 && key <= 122) || (key == 95)) {
            return true;
        }
        else {
            NotifyMessage("Please enter Alpha Numeric only !");
            return false;
        }
    }

    function CheckAlphaFirstLetter()
    //  check for valid numeric strings	
    {
        var strValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-/";
        var strChar;
        var strlength;

        var id = document.getElementById('<%=txtflabel.ClientID%>');
        var idvalue = document.getElementById('<%=txtflabel.ClientID%>').value;

        strlength = idvalue.length;
        for (var count = 0; count < strlength; count++) {
            strChar = idvalue.charAt(0);
            if (strValidChars.indexOf(strChar) == -1) {
                id = idvalue.substring(1, id.length);
                idvalue = id;
                document.getElementById('<%=txtflabel.ClientID%>').value = idvalue;
            }
            else {
                document.getElementById('<%=txtflabel.ClientID%>').value = idvalue;
                return;
            }
        }

    }

    function CheckAlpha(e) {
        var key;
        key = e.which ? e.which : e.keyCode;
        //alert(key);
        if (key != 42) {
            return true;
        }
        else {
            return false;
        }
    }
    /*
    Author : Amitava Sinha
    Creation Date : 8-Jan-2007
    Purpose:Only ione checkbox will selected 
    */
    /*
    function checkbox_checker()
    {
    var tableBody = document.getElementById(Chkformname).childNodes[0];
    for (var i=0;i<tablebody.childnodes.length; i++)
    {
    var currentTd = tableBody.childNodes[i].childNodes[0];
    var listControl = currentTd.childNodes[0];
    if ( listControl.checked == true )
    alert('#' + i + ': is checked');
    }

    var flag=0;
    var chkList1= document.getElementById("Chkformname");
    var arrayOfCheckBoxes= chkList1.getElementsByTagName("input");
    for(counter = 0; counter<arrayOfCheckBoxes.length; counter++)
    {
    if (arrayOfCheckBoxes[counter].checked)
    {
    flag=1;
    }
    else
    {
    alert("not checked");
    return false;
    }
    }
    
    }*/




        </script>
        <div class="content-wrapper">
      <div class="box-body">
      <div class="row">
      <div class="col-xs-12">
      <div class="box box-primary box-solid">
       <div class="box-header">
        <h3 class="box-title">Configure Custom Form Fields</h3>
       </div>
        <!-- /.box-header -->
         <div class="box-body table-responsive no-padding" style="overflow: hidden;margin-left:5px;">
         <%--Main Content Start--%>
         <div class="row">
         <br />
              <div class="col-xs-4">
              <%--left--%>
              <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <label>Choose Forms (select all that apply):</label>
             </div>                                 
    </div>
    <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
             <div class="divborder" style="height: 295px" nowrap="nowrap">
                                <asp:CheckBoxList ID="Chkformname" runat="server" CausesValidation="True" RepeatLayout="Flow"
                                    CssClass="check" Height="184px" Width="100%" OnSelectedIndexChanged="Chkformname_SelectedIndexChanged">
                                </asp:CheckBoxList>
                                
                            </div>
             </div>                                 
    </div>
              </div>
              <div class="col-xs-8">
              <%--right--%>
              <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Field Label:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtflabel" CssClass="form-control" runat="server" Width="300px" MaxLength="90"></asp:TextBox>
             </div>                   
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Field Description:</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <asp:TextBox ID="txtfdesc" runat="server" TextMode="MultiLine" MaxLength="250" Width="300px" CssClass="form-control" Style="resize:none;"></asp:TextBox>
             </div>                   
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <label class="control-label">Field Type:</label>
             </div>
                                
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <input id="rbtntext" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);toggle('textbox');hide('numeric')"
                                                type="radio" name="radiobtn" runat="server" />
                                            <label>
                                                Text</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <div id="textbox" style="display: none">
             <div class="row">
             <div class="col-md-3"><label>Specify type:</label></div>
             <div class="col-md-8"><asp:DropDownList ID="customTxtLines" runat="server" CssClass="form-control" Width="150">
                                                    <asp:ListItem Text="Single Line" Value="1" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Multi-Line" Value="8"></asp:ListItem>
                                                </asp:DropDownList></div>
             </div>
                                                
                                                
                                            </div>
             </div>                   
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <input id="rbtnselect" type="radio" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');hide('numeric');showPopup();"
                                                value="Select List" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnselect" name="hdnselect" value="4" />
                                            <label>
                                                Select List</label>
             </div>
                              
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <input id="rbtndate" type="radio" value="Date" onmouseup="up(this);" onfocus="up(this);"
                                                onclick="down(this);hide('textbox');hide('numeric')" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnDate" name="hdnDate" value="5" />
                                            <label>
                                                Date</label>
             </div>
                             
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <input id="rbtnnumber" type="radio" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');toggle('numeric')"
                                                value="number" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnNumber" name="hdnNumber" value="3" />
                                            <label>
                                                Numeric</label>
             </div>
             <div class="col-md-8 col-sm-12 col-xs-12 form-group">
             <div class="row" id="numeric" style="display: none">
             <div class="col-md-2"><label>Specify:</label></div>
             <div class="col-md-10">
             <%--numeric row start--%>
             <div class="row">
              <div class="col-md-1"><span class="smalllabel">Min</span></div>
              <div class="col-md-3"><asp:TextBox Width="70" ID="txtmin" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox></div>
              <div class="col-md-1"><span class="smalllabel">Max</span></div>
              <div class="col-md-3"><asp:TextBox Width="70" ID="txtmax" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox></div>
              <div class="col-md-1"><span class="smalllabel">Units</span></div>
              <div class="col-md-3"><asp:TextBox Width="90px" ID="txtunits" MaxLength="50" runat="server" CssClass="form-control"></asp:TextBox></div>
             </div>
             <%--numeric row end--%>

             
             
                                                
                                                
             </div>
             </div>
             
             </div>                   
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
            <input id="rbtnyesno" type="radio" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');hide('numeric')"
                                                value="Yes No" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnyesno" name="hdnyesno" value="6" />
                                            <label>
                                                Yes/No</label>
             </div>
                         
    </div>
    <div class="row">
 <div class="col-md-4 col-sm-12 col-xs-12 form-group">
             <input id="rbtnmulti" type="radio" onmouseup="up(this);" onfocus="up(this);" onclick="down(this);hide('textbox');hide('numeric');showPopup();"
                                                value="Multi Select List" name="radiobtn" runat="server" />
                                            <input type="hidden" id="hdnmultiline" name="hdnmultiline" value="9" />
                                            <label>
                                                Multi-Select Checklist</label>
             </div>
                             
    </div>
              </div>
             
                                   
    </div>
     <div class="row">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group" align="center">

  <asp:Button ID="btnAdd" runat="server" Text="Add Custom Field" OnClick="btnAdd_Click"
                                CssClass="btn btn-primary" Height="30px" Width="17%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-plus" style="margin-left: -3%; margin-right: 2%;
                                vertical-align: sub; color: #fff; margin-top: 1%;">
                            </label>
                            <asp:Button ID="BtnEdit" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-primary"
                                Height="30px" Width="9%" Style="text-align: left;" />
                            <label class="glyphicon glyphicon-remove-circle" style="margin-left: -2.5%; margin-right: 2%;
                                vertical-align: sub; color: #fff; margin-top: 1%;">
                            </label>

                            
 <asp:LinkButton ID="btnRearrange" runat="server" Text="Rearrange Fields" OnClientClick="RearrangePopup();" />
 
 </div>
 
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <div class="grid" style="padding-right:5px;">
                                    <div class="rounded">
                                        <div class="top-outer">
                                            <div class="top-inner">
                                                <div class="top">
                                                    <h2>
                                                        Custom Fields</h2>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="mid-outer">
                                            <div class="mid-inner">
                                                <div class="mid" style="cursor: pointer; height: 280px; overflow: auto;">
                                                    <div id="div-gridview" class="gridview whitebg">
                                                        <asp:GridView ID="grdCustomfields" runat="server" AllowSorting="true" OnRowDataBound="grdCustomfields_RowDataBound"
                                                            DataKeyNames="CustomFieldID" AutoGenerateColumns="False" Width="100%" CssClass="table table-bordered table-hover"
                                                            CellPadding="0" CellSpacing="0" BorderWidth="0" GridLines="None" OnRowCommand="grdCustomfields_RowCommand"
                                                            OnSelectedIndexChanging="grdCustomfields_SelectedIndexChanging" OnSorting="grdCustomfields_Sorting"
                                                            OnSelectedIndexChanged="grdCustomfields_SelectedIndexChanged" EnableViewState="False"
                                                            PageSize="1">
                                                            
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="bottom-outer">
                                            <div class="bottom-inner">
                                                <div class="bottom">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
 </div>
 </div>
 <div class="row" align="center">
 <div class="col-md-12 col-sm-12 col-xs-12 form-group">
 <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnback_Click" CssClass="btn btn-primary"
                                    Height="30px" Width="8%" Style="text-align: left;" />
                                <label class="glyphicon glyphicon-backward" style="margin-left: -3%; margin-right: 2%;
                                    vertical-align: sub; color: #fff; margin-top: 1%;">
                                </label>
 </div>
 </div>
	 <%--Main Content End--%>
         </div>
      </div>
      </div>
      </div>
     </div>
     </div>    
</asp:Content>
