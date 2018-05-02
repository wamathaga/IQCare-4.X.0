<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControl_Alerts.ascx.cs" Inherits="PresentationApp.ClinicalForms.UserControl.UserControl_Alerts" %>

<script language="javascript" type="text/javascript">
    var AllergiesArray, ChronicArray, CD4, ViralLoad,UrgentLab;

    function myAllergies(allergies) {
        AllergiesArray = allergies.split(',');
    }

    function myChronic(chronic) {
        ChronicArray = chronic.split(',');
    }

    function CD4DueDate(CD4Date) {
        CD4 = CD4Date;
    }

    function ViralLoadDueDate(viralLoadDate) {
        ViralLoad = viralLoadDate;
    }

    function UrgentLabDueDate(urgentLabDate) {
        UrgentLab = urgentLabDate;
    }
    $(function () {
        var notifications = new $.ttwNotificationCenter({
            notificationList: {
                anchor: 'item',
                offset: '0 15'
            }
        });

        notifications.initMenu({
            allergies: '#allergies',
            chronic: '#chronic'
            //messages: '#messages'
        });

        //test
        //notifications.createNotification('This is a notification');
        //notifications.notice('This is an info message');
        //notifications.error('This is a warning message', 'growl', 'allergies');
        //notifications.createNotification({ message: 'CD4 lab test due on dd-mmm-yyyy', type: 'bar', category: 'allergies', icon: 'images/some_icon.png', autoHide: false });
        if (typeof CD4 === 'undefined') {
        }
        else {
            notifications.error('CD4 Due Date : ' + CD4 + '', 'growl', 'allergies');
        }

        if (typeof ViralLoad === 'undefined') {
        }
        else {
            notifications.error('Viral Load Due Date : ' + ViralLoad + '', 'growl', 'allergies');
        }
        if (typeof UrgentLab === 'undefined') {
        }
        else {
            notifications.error('Urgent Laboratory for this Patient Dated : ' + UrgentLab + '', 'growl', 'allergies');
        }
        if (typeof AllergiesArray === 'undefined') {
        }
        else {
            for (i = 0; i < AllergiesArray.length; i++) {
                notifications.createNotification({ message: AllergiesArray[i].toString(), type: 'none', category: 'allergies' });
            }
        }

        if (typeof ChronicArray === 'undefined') {
        }
        else {
            for (i = 0; i < ChronicArray.length; i++) {
                notifications.createNotification({ message: ChronicArray[i].toString(), type: 'none', category: 'chronic' });
            }
        }

    });

 </script>
<%--<div id="div-alerts" style="position:relative;padding-left:30px;">
<ul id="ulAlerts" class="ttw-notification-menu" style="position:relative;">
    <li id="allergies" class="notification-menu-item first-item"><a href="#">Allergies</a></li>
    <%--<li id="chronic" class="notification-menu-item"><a href="#">Chronic Conditions</a></li>--%>
    <%--<li id="messages" class="notification-menu-item"><a href="#" >Messages</a></li>--
</ul>
</div>--%>
