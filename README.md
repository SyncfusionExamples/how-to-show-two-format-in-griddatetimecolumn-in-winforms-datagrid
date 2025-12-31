# How to Show Two Format in GridDateTimeColumn in WinForms DataGrid?

This example illustrates how to show two format in [GridDateTimeColumn](https://help.syncfusion.com/cr/windowsforms/Syncfusion.WinForms.DataGrid.GridDateTimeColumn.html) in [WinForms DataGrid](https://www.syncfusion.com/winforms-ui-controls/datagrid) (SfDataGrid).

By default, `DataGrid` does not provide the direct support for display datetime value in two format in `GridDateTimeColumn`. You can achieve this by overriding [OnRender](https://help.syncfusion.com/cr/windowsforms/Syncfusion.WinForms.DataGrid.Renderers.GridDateTimeCellRenderer.html#Syncfusion_WinForms_DataGrid_Renderers_GridDateTimeCellRenderer_OnRender_System_Drawing_Graphics_System_Drawing_Rectangle_System_String_Syncfusion_WinForms_DataGrid_Styles_CellStyleInfo_Syncfusion_WinForms_DataGrid_DataColumnBase_Syncfusion_WinForms_GridCommon_ScrollAxis_RowColumnIndex_)  method in [GridDateTimeCellRenderer](https://help.syncfusion.com/cr/windowsforms/Syncfusion.WinForms.DataGrid.Renderers.GridDateTimeCellRenderer.html). 

```C#
this.sfDataGrid.CellRenderers.Remove("DateTime");
this.sfDataGrid.CellRenderers.Add("DateTime", new GridDateTimeCellRendererExt());

public class GridDateTimeCellRendererExt : GridDateTimeCellRenderer
{
   protected override void OnRender(Graphics paint, Rectangle cellRect, string cellValue, CellStyleInfo style, DataColumnBase column, RowColumnIndex rowColumnIndex)
   {
      string[] date = cellValue.Split();
      SizeF size = paint.MeasureString(date[0], style.Font.GetFont());
      float height = (cellRect.Height - size.Height) / 2;
      paint.DrawString(date[0], style.Font.GetFont(), new SolidBrush(style.TextColor), cellRect.X, cellRect.Y + height);
      paint.DrawString(date[1], new Font(style.Font.Facename, style.Font.Size, FontStyle.Bold), new SolidBrush(style.TextColor), cellRect.X + size.Width, cellRect.Y + height);
   }
}
```

The following screenshot shows the two formats in `GridDateTimeColumn`,

![DataGrid with two formats shows in DateTimeColumn](twoformatcell.png)