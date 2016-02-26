<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="pickerAndCalendar.ascx.cs" Inherits="yeuthietkeweb.cms.Calendar.WebUserControl1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<script type="text/javascript">
<!--
    function Picker_OnSelectionChanged(picker) {
        picker.AssociatedCalendar.SetSelectedDate(picker.GetSelectedDate());
    }
    function Calendar_OnSelectionChanged(calendar) {
        calendar.AssociatedPicker.SetSelectedDate(calendar.GetSelectedDate());
    }
    function Button_OnClick(alignElement, calendar) {
        if (calendar.PopUpObjectShowing) {
            calendar.Hide();
        }
        else {
            calendar.SetSelectedDate(calendar.AssociatedPicker.GetSelectedDate());
            calendar.Show(alignElement);
        }
    }
    function Button_OnMouseUp(calendar) {
        if (calendar.PopUpObjectShowing) {
            event.cancelBubble = true;
            event.returnValue = false;
            return false;
        }
        else {
            return true;
        }
    }
//-->
</script>
<table cellspacing="1" cellpadding="0" border="0">
    <tr>
        <td onmouseup='Button_OnMouseUp(<%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>)'>
            <ComponentArt:Calendar ID="Picker" PickerCssClass="picker" ClientSideOnSelectionChanged="Picker_OnSelectionChanged"
                SelectedDate="" ControlType="Picker" PickerCustomFormat="dd/MM/yyyy " PickerFormat="Custom"
                runat="server">
            </ComponentArt:Calendar>
        </td>
        <td>
            <img onmouseup='Button_OnMouseUp(<%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>)'
                class="calendar_button" onclick='Button_OnClick(this, <%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>)'
                height="22" alt="" src="../calendar/images/btn_calendar.gif" width="25">
        </td>
    </tr>
</table>
<ComponentArt:Calendar ID="Calendar" ClientSideOnSelectionChanged="Calendar_OnSelectionChanged"
    SelectedDate="" ControlType="Calendar" runat="server" NextImageUrl="../calendar/images/cal_nextMonth.gif"
    PrevImageUrl="../calendar/images/cal_prevMonth.gif" DayNameFormat="Short" SwapDuration="300"
    SwapSlide="Linear" MonthCssClass="month" NextPrevCssClass="nextprev" CalendarCssClass="calendar"
    SelectedDayCssClass="selectedday" OtherMonthDayCssClass="othermonthday" DayHoverCssClass="dayhover"
    DayCssClass="day" DayHeaderCssClass="dayheader" CalendarTitleCssClass="title"
    PopUp="Custom" AllowMonthSelection="false" AllowWeekSelection="false" AllowMultipleSelection="false">
</ComponentArt:Calendar>
<script type="text/javascript">
// Associate the picker and the calendar:
function ComponentArt_<%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>_Associate()
{
  if (window.<%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>_loaded && window.<%= Picker.ClientID.Replace("$","_").Replace(":","_") %>_loaded)
  {
    window.<%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>.AssociatedPicker = <%= Picker.ClientID.Replace("$","_").Replace(":","_") %>;
    window.<%= Picker.ClientID.Replace("$","_").Replace(":","_") %>.AssociatedCalendar = <%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>;
  }
  else
  {
    window.setTimeout('ComponentArt_<%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>_Associate()', 100);
  }
}
ComponentArt_<%= Calendar.ClientID.Replace("$","_").Replace(":","_") %>_Associate();
</script>