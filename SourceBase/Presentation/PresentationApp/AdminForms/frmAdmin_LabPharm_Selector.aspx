<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmAdmin_LabPharm_Selector.aspx.cs" Inherits="PresentationApp.AdminForms.frmAdmin_LabPharm_Selector" %>

<link rel="stylesheet" type="text/css" href="../style/styles.css" />
<head id="Head1" runat="server">
    <title id="lblHeader" runat="server">Untitled Page</title>
    <script type="text/javascript">

        function MoveItemUpDown(goUp) {
            var list = document.getElementById('lstSelected');
            //Get the current selected items index
            var selectedIndex = list.selectedIndex;

            //tell the user to select one if he hasn't
            if (selectedIndex == -1) {
                NotifyMessage("Select an item for re-ordering.");
            }
            else {
                //check if we need to move up or down
                var newIndex = selectedIndex + (goUp ? -1 : 1);

                if (newIndex < 0) {
                    //If we have reached top cycle it to end
                    newIndex = list.length - 1;
                }
                else if (newIndex >= list.length) {
                    //If we have reached end cycle it to top
                    newIndex = 0;
                }

                //Lets take the old items value and text
                var oldVal = list[selectedIndex].value;
                var oldText = list[selectedIndex].text;

                //Swap the value and text of old and new items.  
                list[selectedIndex].value = list[newIndex].value;
                list[selectedIndex].text = list[newIndex].text;
                list[newIndex].value = oldVal;
                list[newIndex].text = oldText;
                list.selectedIndex = newIndex;

                //Done, the element is moved now
            }
        }                                           

    </script>
</head>
<body>
    <form id="LabSelection" class="border" runat="server" style="height: 300px; width: 640px;">
    <div align="Center" style="width: 640px; height: 300px;">
        <table cellpadding="18" width="100%" height="70%" border="0">
            <tbody>
                <tr>
                    <td class="border formbg">
                        <asp:ListBox ID="lstLabList" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                    <td>
                        <div>
                            <asp:Button ID="btnAdd" runat="server" Width="80px" Text="Add >>" OnClick="btnAdd_Click" />
                        </div>
                        <br />
                        <div>
                            <asp:Button ID="btnRemove" runat="server" Width="80px" Text="<< Remove" OnClick="btnRemove_Click" />
                        </div>
                    </td>
                    <td class="border formbg">
                    <table>
                    <tr>
                    <td>
                    <asp:ListBox ID="lstSelected" runat="server" Height="180px" Width="210px"></asp:ListBox>
                    </td>
                    <td valign="middle" align="center">
                    <asp:ImageButton ID="BtnUp" runat="server" Width="10px" Height="10px" OnClientClick="MoveItemUpDown(true)" 
                            ImageUrl="~/Images/btnUp.Image.gif" ImageAlign="AbsMiddle" 
                            BorderStyle="Solid"  />
                    <br />
                    <br />
                    <asp:ImageButton ID="Btndown" runat="server" Width="10px" Height="10px" OnClientClick="MoveItemUpDown(false)"  
                    ImageUrl="~/Images/btnDown.Image.gif" ImageAlign="AbsMiddle" BorderStyle="Solid"  />
                    </td>
                    </tr>
                    </table>
                        
                        
                    </td>
                </tr>
            </tbody>
        </table>
        <div align="left" style="width: 640px">
            <asp:Label ID="Label1" runat="server" Text="Search Criteria"></asp:Label>
            <asp:TextBox ID="txtSearch" AutoPostBack="true" runat="server" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
        </div>
        <br />
        <div class="border" align="Center" style="width: 635px">
            <asp:Button ID="btnSubmit" runat="server" Width="80px" Text="Submit" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Width="80px" Text="Back" OnClick="btnBack_Click" />
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        /*
        Author : Amitava Sinha
        Creation Date : 03-april-2007
        Purpose:atleast one item will selected 
        */

        function listBox_selected(sel) {
            var listBox = document.getElementById(sel);
            var intCount = listBox.options.length;

            for (i = 0; i < intCount; i++) {
                if (listBox.options(i).selected) {
                    return true;
                }

            }
            NotifyMessage("Select atleast one item !");
            return false;

        }    
    </script>
    </form>
</body>
</html>
